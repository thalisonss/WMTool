using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WMTool.Reprocessing.CustomSql;
using WMTool.Reprocessing.Exceptions;
using WMTool.Reprocessing.ViewDsl.Models;

namespace WMTool.Reprocessing.ViewDsl
{
    public class ViewSqlBuilder
    {
        private const int MaxNestedViewDepth = 5;

        private static readonly Regex SimpleColumnPattern = new Regex(@"^\s*[A-Za-z_][A-Za-z0-9_]*\.([A-Za-z_][A-Za-z0-9_]*)\s*$", RegexOptions.Compiled);

        private readonly DslExpressionTranslator _expressionTranslator;
        private readonly EntityExtensionResolver _extensionResolver;
        private readonly IViewDefinitionProvider _viewDefinitionProvider;
        private readonly CustomViewSqlOverrideStore _overrideStore;

        public ViewSqlBuilder(
            DslExpressionTranslator expressionTranslator,
            EntityExtensionResolver extensionResolver,
            IViewDefinitionProvider viewDefinitionProvider,
            CustomViewSqlOverrideStore overrideStore)
        {
            _expressionTranslator = expressionTranslator;
            _extensionResolver = extensionResolver;
            _viewDefinitionProvider = viewDefinitionProvider;
            _overrideStore = overrideStore;
        }

        public Task<TranslatedSqlQuery> BuildAsync(ViewDefinition view, string viewName, string connectionString)
        {
            return BuildAsync(view, viewName, connectionString, 0);
        }

        private async Task<TranslatedSqlQuery> BuildAsync(ViewDefinition view, string viewName, string connectionString, int depth)
        {
            // Uma query customizada cadastrada pelo usuário para essa view (normalmente por performance)
            // substitui totalmente a tradução da DSL — precisa devolver as mesmas colunas de saída. Vale
            // tanto quando a view é um data source de topo quanto quando é referenciada como view aninhada
            // dentro de outra (o mesmo método é usado nos dois casos).
            string customSql = _overrideStore.GetSql(viewName);
            if (customSql != null)
            {
                return new TranslatedSqlQuery
                {
                    Sql = customSql,
                    ParameterNames = BindParameterScanner.ExtractNames(customSql).Distinct().ToList()
                };
            }

            if (depth > MaxNestedViewDepth)
            {
                throw new FormatException($"Aninhamento de views excedeu {MaxNestedViewDepth} níveis a partir de '{viewName}' — possível referência circular entre views.");
            }

            string selectPrefix = "SELECT " + (view.Select.Distinct ? "DISTINCT " : string.Empty) +
                                   (view.Select.Limit.HasValue ? "TOP " + view.Select.Limit.Value + " " : string.Empty);

            List<(string Sql, string RawExpression, bool IsAggregate)> columnResults =
                view.Select.Columns.Select(c => BuildColumn(c, viewName)).ToList();

            string columnsText = string.Join(", ", columnResults.Select(r => r.Sql));

            var joinTexts = new List<string>();
            var nestedViewQualifiers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (ViewJoinDefinition join in view.From.Joins)
            {
                (string text, bool isNestedView) = await BuildJoinTextAsync(join, viewName, connectionString, depth);
                joinTexts.Add(text);

                if (isNestedView)
                {
                    nestedViewQualifiers.Add(join.Alias ?? join.Entity);
                }
            }

            List<string> conditions = BuildConditions(view, viewName);
            string whereText = conditions.Count > 0 ? " WHERE " + string.Join(" AND ", conditions) : string.Empty;

            // A DSL não tem uma tag de GROUP BY — quando alguma coluna do <select> usa uma função de
            // agregação (SUM/COUNT/...), o motor original deve montar implicitamente um GROUP BY com as
            // demais colunas. Reproduzimos isso aqui.
            List<string> nonAggregateExpressions = columnResults.Where(r => !r.IsAggregate).Select(r => r.RawExpression).ToList();
            string groupByText = columnResults.Any(r => r.IsAggregate) && nonAggregateExpressions.Count > 0
                ? " GROUP BY " + string.Join(", ", nonAggregateExpressions)
                : string.Empty;

            IReadOnlyDictionary<string, string> extensionJoinByEntity = await ResolveExtensionJoinsAsync(
                view, nestedViewQualifiers, columnsText + string.Concat(joinTexts) + whereText, connectionString);

            Func<string, string> rewrite = await BuildRewriterAsync(view, nestedViewQualifiers, extensionJoinByEntity, connectionString);

            var sql = new StringBuilder(selectPrefix);
            sql.Append(rewrite(columnsText));
            sql.Append(" FROM ").Append(view.From.Entity);

            if (!string.IsNullOrEmpty(view.From.Alias))
            {
                sql.Append(" AS ").Append(view.From.Alias);
            }

            if (extensionJoinByEntity.TryGetValue(FromQualifier(view), out string fromExtensionJoin))
            {
                sql.Append(fromExtensionJoin);
            }

            for (int i = 0; i < view.From.Joins.Count; i++)
            {
                ViewJoinDefinition join = view.From.Joins[i];
                sql.Append(rewrite(joinTexts[i]));

                if (extensionJoinByEntity.TryGetValue(join.Alias ?? join.Entity, out string joinExtensionJoin))
                {
                    sql.Append(joinExtensionJoin);
                }
            }

            sql.Append(rewrite(whereText));
            sql.Append(rewrite(groupByText));

            string finalSql = sql.ToString();

            IReadOnlyList<string> parameterNames = view.Parameters.Select(p => p.Name)
                .Union(BindParameterScanner.ExtractNames(finalSql))
                .Union(view.Filter.CompanyFilter ? new[] { "cIDCompany" } : Enumerable.Empty<string>())
                .Distinct()
                .ToList();

            return new TranslatedSqlQuery { Sql = finalSql, ParameterNames = parameterNames };
        }

        // Algumas views usam OUTRA MC1_View como se fosse uma entidade/tabela (ex.: Custom_WM_NF_ProductTot
        // faz JOIN direto com Custom_WM_NF_ProductOrder), com parâmetros da view aninhada remapeados via
        // <parameter name="X" value=":Y"/> dentro do próprio join. Tentamos resolver "Entity" como view
        // primeiro; se não existir (ViewNotFoundException), é uma tabela real e seguimos como sempre.
        private async Task<(string Text, bool IsNestedView)> BuildJoinTextAsync(ViewJoinDefinition join, string viewName, string connectionString, int depth)
        {
            string joinKeyword = join.JoinType == "inner-join" ? "INNER JOIN" : "LEFT JOIN";
            string qualifier = join.Alias ?? join.Entity;

            ViewDefinition nestedView = await TryGetNestedViewAsync(join, connectionString);

            if (nestedView == null)
            {
                string alias = string.IsNullOrEmpty(join.Alias) ? string.Empty : " AS " + join.Alias;
                string joinText = " " + joinKeyword + " " + join.Entity + alias + " ON " + _expressionTranslator.Translate(join.OnExpression, viewName);
                return (joinText, false);
            }

            TranslatedSqlQuery nestedQuery = await BuildAsync(nestedView, join.Entity, connectionString, depth + 1);
            string nestedSql = ApplyParameterMappings(nestedQuery.Sql, join.ParameterMappings, viewName);

            string nestedJoinText = " " + joinKeyword + " (" + nestedSql + ") AS " + qualifier +
                                     " ON " + _expressionTranslator.Translate(join.OnExpression, viewName);
            return (nestedJoinText, true);
        }

        private async Task<ViewDefinition> TryGetNestedViewAsync(ViewJoinDefinition join, string connectionString)
        {
            try
            {
                return await _viewDefinitionProvider.GetLatestEnabledAsync(join.Alias ?? join.Entity, join.Entity, connectionString);
            }
            catch (ViewNotFoundException)
            {
                return null;
            }
        }

        // Parâmetros da view aninhada explicitamente remapeados (<parameter name="X" value="Y"/>) trocam
        // seu bind "@X" pela tradução de "Y" (ex.: ":varcIDOrder" -> "@varcIDOrder"); os demais permanecem
        // com o mesmo nome, contando com esse nome já existir no conjunto de parâmetros do escopo externo.
        private string ApplyParameterMappings(string nestedSql, List<ViewParameterMapping> mappings, string viewName)
        {
            foreach (ViewParameterMapping mapping in mappings)
            {
                string replacement = _expressionTranslator.Translate(mapping.Value, viewName);
                nestedSql = Regex.Replace(nestedSql, @"(?<![A-Za-z0-9_])@" + Regex.Escape(mapping.Name) + @"\b", replacement);
            }

            return nestedSql;
        }

        // A plataforma MC1 permite adicionar campos customizados a uma entidade base numa tabela satélite
        // "<Entidade>Ext" (mesma chave primária), e as views referenciam esses campos como se fossem da
        // entidade base. Reproduzimos isso aqui: para cada entidade da FROM/JOIN sem alias próprio,
        // detectamos se algum campo referenciado só existe na extensão e, se sim, preparamos um LEFT JOIN
        // para ela. Esse LEFT JOIN é inserido logo depois de onde a própria entidade é introduzida (FROM
        // ou seu próprio JOIN) — nunca no final da lista — porque o SQL Server não aceita uma cláusula ON
        // referenciando uma tabela que só é declarada por um JOIN posterior.
        private static string FromQualifier(ViewDefinition view)
        {
            return view.From.Alias ?? view.From.Entity;
        }

        // (Entidade real no banco, qualificador usado no SQL) — quando a entidade tem alias (no FROM ou
        // num JOIN), o SQL só reconhece o alias como qualificador; a entidade real só importa pra saber
        // qual é a tabela "<Entidade>Ext" e sua chave primária. Joins que já foram resolvidos como view
        // aninhada (derived table) ficam de fora — não fazem sentido para promoção de extensão.
        private static List<(string RealEntity, string Qualifier)> CollectEntityQualifiers(ViewDefinition view, HashSet<string> nestedViewQualifiers)
        {
            var entities = new List<(string, string)> { (view.From.Entity, FromQualifier(view)) };
            entities.AddRange(view.From.Joins
                .Where(j => !nestedViewQualifiers.Contains(j.Alias ?? j.Entity))
                .Select(j => (j.Entity, j.Alias ?? j.Entity)));
            return entities;
        }

        private async Task<IReadOnlyDictionary<string, string>> ResolveExtensionJoinsAsync(
            ViewDefinition view, HashSet<string> nestedViewQualifiers, string wholeTextForDetection, string connectionString)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach ((string entity, string qualifier) in CollectEntityQualifiers(view, nestedViewQualifiers).Distinct())
            {
                string extensionTable = await _extensionResolver.GetExtensionTableNameAsync(entity, connectionString);
                if (extensionTable == null)
                {
                    continue;
                }

                HashSet<string> baseColumns = await _extensionResolver.GetColumnsAsync(entity, connectionString);
                HashSet<string> extensionColumns = await _extensionResolver.GetColumnsAsync(extensionTable, connectionString);
                var pattern = new Regex(@"\b" + Regex.Escape(qualifier) + @"\.([A-Za-z_][A-Za-z0-9_]*)\b");

                bool usedExtension = pattern.Matches(wholeTextForDetection)
                    .Cast<Match>()
                    .Any(m => !baseColumns.Contains(m.Groups[1].Value) && extensionColumns.Contains(m.Groups[1].Value));

                if (!usedExtension)
                {
                    continue;
                }

                IReadOnlyList<string> keyColumns = await _extensionResolver.GetPrimaryKeyColumnsAsync(entity, connectionString);
                string onClause = string.Join(" AND ", keyColumns.Select(k => $"{qualifier}.{k} = {extensionTable}.{k}"));
                result[qualifier] = " LEFT JOIN " + extensionTable + " ON " + onClause;
            }

            return result;
        }

        private async Task<Func<string, string>> BuildRewriterAsync(
            ViewDefinition view, HashSet<string> nestedViewQualifiers, IReadOnlyDictionary<string, string> extensionJoinByEntity, string connectionString)
        {
            var rewriters = new List<(Regex Pattern, string ExtensionTable, HashSet<string> BaseColumns, HashSet<string> ExtensionColumns)>();
            var entityByQualifier = CollectEntityQualifiers(view, nestedViewQualifiers).ToDictionary(x => x.Qualifier, x => x.RealEntity, StringComparer.OrdinalIgnoreCase);

            foreach (string qualifier in extensionJoinByEntity.Keys)
            {
                string entity = entityByQualifier[qualifier];
                string extensionTable = await _extensionResolver.GetExtensionTableNameAsync(entity, connectionString);
                HashSet<string> baseColumns = await _extensionResolver.GetColumnsAsync(entity, connectionString);
                HashSet<string> extensionColumns = await _extensionResolver.GetColumnsAsync(extensionTable, connectionString);
                var pattern = new Regex(@"\b" + Regex.Escape(qualifier) + @"\.([A-Za-z_][A-Za-z0-9_]*)\b");

                rewriters.Add((pattern, extensionTable, baseColumns, extensionColumns));
            }

            return text =>
            {
                foreach ((Regex pattern, string extensionTable, HashSet<string> baseColumns, HashSet<string> extensionColumns) in rewriters)
                {
                    text = pattern.Replace(text, match =>
                    {
                        string field = match.Groups[1].Value;
                        return !baseColumns.Contains(field) && extensionColumns.Contains(field)
                            ? extensionTable + "." + field
                            : match.Value;
                    });
                }

                return text;
            };
        }

        private (string Sql, string RawExpression, bool IsAggregate) BuildColumn(ViewColumnDefinition column, string viewName)
        {
            string translated = _expressionTranslator.Translate(column.Expression, viewName);
            bool isAggregate = AggregateColumnDetector.ContainsTopLevelAggregate(translated);

            if (!string.IsNullOrEmpty(column.OutputAlias))
            {
                return (translated + " AS [" + column.OutputAlias + "]", translated, isAggregate);
            }

            if (SimpleColumnPattern.IsMatch(column.Expression))
            {
                return (translated, translated, isAggregate);
            }

            throw new MissingColumnAliasException(viewName, column.Expression);
        }

        private List<string> BuildConditions(ViewDefinition view, string viewName)
        {
            var conditions = new List<string>();

            if (view.Filter.CompanyFilter)
            {
                conditions.Add(FromQualifier(view) + ".cIDCompany = @cIDCompany");
            }

            foreach (string enabledEntity in view.Filter.EnabledEntities)
            {
                conditions.Add(enabledEntity + ".mc1Enabled = 1");
            }

            if (!string.IsNullOrWhiteSpace(view.WhereExpression))
            {
                conditions.Add("(" + _expressionTranslator.Translate(view.WhereExpression, viewName) + ")");
            }

            return conditions;
        }
    }
}
