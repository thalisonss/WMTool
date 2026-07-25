using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WMTool.Business;
using WMTool.Reprocessing.Exceptions;
using WMTool.Reprocessing.Models;
using WMTool.Reprocessing.Parsing;

namespace WMTool.Reprocessing.Repositories
{
    public class DocumentTemplateRepository
    {
        private readonly WMBusiness _business;

        public DocumentTemplateRepository(WMBusiness business)
        {
            _business = business;
        }

        public async Task<DocumentTemplateDefinition> GetLatestEnabledAsync(string templateName, string connectionString)
        {
            const string sql = "SELECT TOP 1 nVersion, cContent FROM MC1_DocumentTemplate " +
                                "WHERE cTemplateName = @cTemplateName AND mc1Enabled = 1 ORDER BY nVersion DESC";

            var parameters = new Dictionary<string, object> { ["@cTemplateName"] = templateName };
            DataTable result = await _business.ConsultDB(sql, connectionString, parameters);

            if (result.Rows.Count == 0)
            {
                throw new TemplateNotFoundException(templateName);
            }

            DataRow row = result.Rows[0];
            return DocumentTemplateJsonParser.Parse(templateName, Convert.ToInt32(row["nVersion"]), row["cContent"].ToString());
        }
    }
}
