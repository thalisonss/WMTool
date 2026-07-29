using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WMTool.Business;
using WMTool.Reprocessing.Caching;
using WMTool.Reprocessing.CustomSql;
using WMTool.Reprocessing.Execution;
using WMTool.Reprocessing.Models;
using WMTool.Reprocessing.ParameterResolution;
using WMTool.Reprocessing.Repositories;
using WMTool.Reprocessing.Templating;
using WMTool.Reprocessing.ViewDsl;
using WMTool.Reprocessing.ViewDsl.Models;

namespace WMTool.Reprocessing
{
    public class JsonReprocessingEngine
    {
        private const string TemplateName = "WM_Invoice_Generate";

        private readonly DocumentTemplateRepository _templateRepository;
        private readonly ViewRepository _viewRepository;
        private readonly ViewSqlBuilder _sqlBuilder;
        private readonly DataSourceExecutor _dataSourceExecutor;
        private readonly MasterParameterResolver _parameterResolver;
        private readonly TemplateEngine _templateEngine;
        private readonly McParamsBuilder _mcParamsBuilder;
        private readonly CustomViewSqlOverrideStore _overrideStore;
        private readonly DataSourceCacheStore _dataSourceCacheStore;

        public JsonReprocessingEngine(WMBusiness business)
        {
            var functionRegistry = new DslFunctionRegistry();
            var expressionTranslator = new DslExpressionTranslator(functionRegistry);
            var extensionResolver = new EntityExtensionResolver(business);

            _templateRepository = new DocumentTemplateRepository(business);
            _viewRepository = new ViewRepository(business, new ViewDslXmlParser());
            _overrideStore = new CustomViewSqlOverrideStore();
            _dataSourceCacheStore = new DataSourceCacheStore();
            _sqlBuilder = new ViewSqlBuilder(expressionTranslator, extensionResolver, _viewRepository, _overrideStore);
            _dataSourceExecutor = new DataSourceExecutor(business);
            _parameterResolver = new MasterParameterResolver(
                new InvoiceDerivedDataRepository(business),
                new TripVehicleCodeRepository(business),
                business,
                new MasterParameterQueryOverrideStore(),
                new MasterParameterGeneralQueryStore());
            _templateEngine = new TemplateEngine();
            _mcParamsBuilder = new McParamsBuilder();
        }

        public async Task<IReadOnlyList<DataSourceReference>> ListDataSourcesAsync(string connectionString)
        {
            DocumentTemplateDefinition template = await _templateRepository.GetLatestEnabledAsync(TemplateName, connectionString);
            return template.DataSources;
        }

        // Busca a definição atual do template + de cada view no banco, traduz cada uma a partir da DSL
        // (ignorando qualquer SQL customizado já cadastrado — é isso que "atualizar" significa) e grava o
        // resultado como o SQL customizado de cada data source. Dali em diante, rodar o reprocessamento
        // usa esse SQL já pronto (sem re-traduzir a DSL, sem re-consultar MC1_View) até a próxima
        // atualização. Também salva a lista de data sources num cache local pra tela abrir sem precisar
        // consultar o banco.
        public async Task<IReadOnlyList<DataSourceReference>> RefreshDataSourcesAsync(string connectionString)
        {
            DocumentTemplateDefinition template = await _templateRepository.GetLatestEnabledAsync(TemplateName, connectionString);

            foreach (DataSourceReference reference in template.DataSources)
            {
                ViewDefinition view = await _viewRepository.GetLatestEnabledAsync(reference.Alias, reference.ViewName, connectionString);
                TranslatedSqlQuery query = await _sqlBuilder.BuildFromLiveDslAsync(view, reference.ViewName, connectionString);
                _overrideStore.Save(reference.ViewName, query.Sql);
            }

            _dataSourceCacheStore.Save(template.TemplateName, template.DataSources);

            return template.DataSources;
        }

        public async Task<IReadOnlyList<RequiredParameterInfo>> DiscoverRequiredParametersAsync(MasterParameterInputs inputs, string connectionString)
        {
            DocumentTemplateDefinition template = await _templateRepository.GetLatestEnabledAsync(TemplateName, connectionString);
            Dictionary<string, ViewDefinition> views = await LoadViewsAsync(template, connectionString);
            Dictionary<string, TranslatedSqlQuery> queries = await BuildQueriesAsync(template, views, connectionString);
            IReadOnlyList<string> requiredParameterNames = CollectRequiredParameterNames(queries);

            return await _parameterResolver.SuggestAsync(inputs, requiredParameterNames, connectionString);
        }

        public async Task<JObject> ReprocessAsync(MasterParameterInputs inputs, string connectionString)
        {
            DocumentTemplateDefinition template = await _templateRepository.GetLatestEnabledAsync(TemplateName, connectionString);
            Dictionary<string, ViewDefinition> views = await LoadViewsAsync(template, connectionString);
            Dictionary<string, TranslatedSqlQuery> queries = await BuildQueriesAsync(template, views, connectionString);
            IReadOnlyList<string> requiredParameterNames = CollectRequiredParameterNames(queries);

            MasterParameterSet parameters = await _parameterResolver.ResolveAsync(inputs, requiredParameterNames, connectionString);

            var dataSources = new Dictionary<string, DataSourceResult>(StringComparer.OrdinalIgnoreCase);
            foreach (DataSourceReference reference in template.DataSources)
            {
                if (inputs.DisabledAliases != null && inputs.DisabledAliases.Contains(reference.Alias))
                {
                    // Usuário optou por não rodar esse data source (ex.: view sabidamente lenta). Os
                    // campos que viriam dele ficam em branco no JSON final, sem bloquear os demais.
                    dataSources[reference.Alias] = new DataSourceResult { Alias = reference.Alias, Rows = new DataTable() };
                    continue;
                }

                DataSourceResult result = await _dataSourceExecutor.ExecuteAsync(reference, queries[reference.Alias], parameters, connectionString);
                dataSources[reference.Alias] = result;
            }

            JObject nfeBatch = _templateEngine.Expand(template.TemplateRoot, dataSources);
            JObject mcParams = _mcParamsBuilder.Build(parameters);

            return new JObject
            {
                ["mc1_params"] = mcParams,
                ["nfe_batch"] = nfeBatch
            };
        }

        private async Task<Dictionary<string, ViewDefinition>> LoadViewsAsync(DocumentTemplateDefinition template, string connectionString)
        {
            var views = new Dictionary<string, ViewDefinition>(StringComparer.OrdinalIgnoreCase);

            foreach (DataSourceReference reference in template.DataSources)
            {
                views[reference.Alias] = await _viewRepository.GetLatestEnabledAsync(reference.Alias, reference.ViewName, connectionString);
            }

            return views;
        }

        // O SQL final de cada view só é conhecido depois de traduzido (join de extensão, LIMIT embutido
        // em subquery etc.), e é só nesse SQL final que descobrimos os binds ":nome" realmente usados —
        // algumas views usam um parâmetro sem declará-lo em <parameters> (ver BindParameterScanner). Por
        // isso construímos todas as queries ANTES de descobrir/resolver os parâmetros mestre, e reusamos
        // o mesmo TranslatedSqlQuery na hora de executar, sem traduzir a view duas vezes.
        private async Task<Dictionary<string, TranslatedSqlQuery>> BuildQueriesAsync(
            DocumentTemplateDefinition template, Dictionary<string, ViewDefinition> views, string connectionString)
        {
            var queries = new Dictionary<string, TranslatedSqlQuery>(StringComparer.OrdinalIgnoreCase);

            foreach (DataSourceReference reference in template.DataSources)
            {
                queries[reference.Alias] = await _sqlBuilder.BuildAsync(views[reference.Alias], reference.ViewName, connectionString);
            }

            return queries;
        }

        private static IReadOnlyList<string> CollectRequiredParameterNames(Dictionary<string, TranslatedSqlQuery> queries)
        {
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "cIDCompany" };

            foreach (TranslatedSqlQuery query in queries.Values)
            {
                foreach (string parameterName in query.ParameterNames)
                {
                    names.Add(parameterName);
                }
            }

            return names.ToList();
        }
    }
}
