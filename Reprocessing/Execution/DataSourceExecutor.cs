using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WMTool.Business;
using WMTool.Reprocessing.Models;
using WMTool.Reprocessing.ViewDsl;

namespace WMTool.Reprocessing.Execution
{
    public class DataSourceExecutor
    {
        private readonly WMBusiness _business;

        public DataSourceExecutor(WMBusiness business)
        {
            _business = business;
        }

        public async Task<DataSourceResult> ExecuteAsync(
            DataSourceReference reference,
            TranslatedSqlQuery query,
            MasterParameterSet parameters,
            string connectionString)
        {
            IDictionary<string, object> sqlParameters = parameters.ToSqlParameterDictionary(query.ParameterNames);

            string sql = AppendRecompileHint(query.Sql);

            DataTable rows = await _business.ConsultDB(sql, connectionString, sqlParameters);

            return new DataSourceResult { Alias = reference.Alias, Rows = rows };
        }

        private static string AppendRecompileHint(string sql)
        {
            // Views com subqueries correlacionadas (ex.: Custom_WM_NF_ProductOrder) sofrem
            // parameter sniffing ruim quando executadas via SqlCommand parametrizado: o plano
            // compilado para o primeiro conjunto de parâmetros fica em cache e é reaproveitado
            // para pedidos com distribuição de dados bem diferente, gerando planos catastróficos
            // (nested loops/index spool em excesso) mesmo com custo estimado baixo. Rodar o mesmo
            // SQL com variáveis locais (DECLARE) no SSMS é rápido porque o otimizador usa
            // estimativas genéricas em vez de sniff. RECOMPILE reproduz esse mesmo efeito sem
            // abrir mão de parâmetros tipados (mantém a proteção contra SQL injection).
            string trimmed = sql.TrimEnd();

            if (trimmed.EndsWith(";"))
            {
                trimmed = trimmed.Substring(0, trimmed.Length - 1).TrimEnd();
            }

            return trimmed + "\nOPTION (RECOMPILE)";
        }
    }
}
