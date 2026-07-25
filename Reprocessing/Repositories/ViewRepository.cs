using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WMTool.Business;
using WMTool.Reprocessing.Exceptions;
using WMTool.Reprocessing.ViewDsl;
using WMTool.Reprocessing.ViewDsl.Models;

namespace WMTool.Reprocessing.Repositories
{
    public class ViewRepository : IViewDefinitionProvider
    {
        private readonly WMBusiness _business;
        private readonly ViewDslXmlParser _xmlParser;

        public ViewRepository(WMBusiness business, ViewDslXmlParser xmlParser)
        {
            _business = business;
            _xmlParser = xmlParser;
        }

        public async Task<ViewDefinition> GetLatestEnabledAsync(string alias, string viewName, string connectionString)
        {
            const string sql = "SELECT TOP 1 cContent FROM MC1_View " +
                                "WHERE cViewName = @cViewName AND mc1Enabled = 1 ORDER BY nVersion DESC";

            var parameters = new Dictionary<string, object> { ["@cViewName"] = viewName };
            DataTable result = await _business.ConsultDB(sql, connectionString, parameters);

            if (result.Rows.Count == 0)
            {
                throw new ViewNotFoundException(alias, viewName);
            }

            return _xmlParser.Parse(result.Rows[0]["cContent"].ToString());
        }
    }
}
