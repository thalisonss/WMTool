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

            DataTable rows = await _business.ConsultDB(query.Sql, connectionString, sqlParameters);

            return new DataSourceResult { Alias = reference.Alias, Rows = rows };
        }
    }
}
