using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WMTool.Business;
using WMTool.Reprocessing.Models;

namespace WMTool.Reprocessing.Repositories
{
    public class InvoiceDerivedDataRepository
    {
        private readonly WMBusiness _business;

        public InvoiceDerivedDataRepository(WMBusiness business)
        {
            _business = business;
        }

        public async Task<DerivedInvoiceData> GetInvoiceDataAsync(MasterParameterInputs inputs, string connectionString)
        {
            const string sql = "SELECT TOP 1 cIDCustomer, cIDTrip, cForm FROM MC1_Invoice " +
                                "WHERE cIDInvoice = @cIDInvoice AND cSerie = @cSerie " +
                                "AND cIDBranchInvoice = @cIDBranchInvoice AND cIDCompany = @cIDCompany";

            var parameters = new Dictionary<string, object>
            {
                ["@cIDInvoice"] = inputs.CIDInvoice,
                ["@cSerie"] = inputs.CSerie,
                ["@cIDBranchInvoice"] = inputs.CIDBranchInvoice,
                ["@cIDCompany"] = inputs.CIDCompany
            };

            DataTable result = await _business.ConsultDB(sql, connectionString, parameters);
            if (result.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = result.Rows[0];
            return new DerivedInvoiceData
            {
                CIDCustomer = row["cIDCustomer"]?.ToString(),
                CIDTrip = row["cIDTrip"]?.ToString(),
                CForm = row["cForm"]?.ToString()
            };
        }

        public async Task<string> GetOrderIdAsync(MasterParameterInputs inputs, string cForm, string connectionString)
        {
            const string sql = "SELECT TOP 1 cIDOrder FROM MC1_OrderInvoice " +
                                "WHERE cIDInvoice = @cIDInvoice AND cSerie = @cSerie " +
                                "AND cIDBranchInvoice = @cIDBranchInvoice AND cForm = @cForm AND cIDCompany = @cIDCompany";

            var parameters = new Dictionary<string, object>
            {
                ["@cIDInvoice"] = inputs.CIDInvoice,
                ["@cSerie"] = inputs.CSerie,
                ["@cIDBranchInvoice"] = inputs.CIDBranchInvoice,
                ["@cForm"] = cForm,
                ["@cIDCompany"] = inputs.CIDCompany
            };

            DataTable result = await _business.ConsultDB(sql, connectionString, parameters);
            return result.Rows.Count == 0 ? null : result.Rows[0]["cIDOrder"]?.ToString();
        }
    }
}
