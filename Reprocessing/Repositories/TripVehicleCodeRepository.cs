using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WMTool.Business;

namespace WMTool.Reprocessing.Repositories
{
    // Custom_WM_NF_Transportadora usa um parâmetro ":cIDUser" que não é declarado em seu próprio
    // <parameters> e não vem dos 4 inputs do reprocessador. O valor real vem do "código do veículo"
    // associado à viagem (cCodeVehicle, campo de extensão de MC1_Trip), usado como identificador do
    // registro em MC1_UserVehicle correspondente.
    public class TripVehicleCodeRepository
    {
        private readonly WMBusiness _business;

        public TripVehicleCodeRepository(WMBusiness business)
        {
            _business = business;
        }

        public async Task<string> GetVehicleCodeAsync(string cIDTrip, string cIDCompany, string connectionString)
        {
            if (string.IsNullOrEmpty(cIDTrip))
            {
                return null;
            }

            const string sql = "SELECT TOP 1 cCodeVehicle FROM MC1_TripExt WHERE cIDTrip = @cIDTrip AND cIDCompany = @cIDCompany";
            var parameters = new Dictionary<string, object> { ["@cIDTrip"] = cIDTrip, ["@cIDCompany"] = cIDCompany };

            DataTable result = await _business.ConsultDB(sql, connectionString, parameters);
            return result.Rows.Count == 0 ? null : result.Rows[0]["cCodeVehicle"]?.ToString();
        }
    }
}
