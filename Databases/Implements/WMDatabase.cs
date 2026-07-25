using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WMTool.Databases
{
    class WMDatabase
    {          
        public async Task<System.Data.DataTable> ConsultDB(string sqlQuery, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
                System.Data.DataTable dataTable = new System.Data.DataTable();

                await Task.Run(() => dataAdapter.Fill(dataTable));

                return dataTable;
            }
        }

        public async Task<System.Data.DataTable> ConsultDB(string sqlQuery, string connectionString, IDictionary<string, object> parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.CommandTimeout = 900;

                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            // AddWithValue infere NVarChar para string do .NET, mas as colunas de chave
                            // deste banco (cIDInvoice, cIDCompany, etc.) são varchar. Comparar varchar
                            // (coluna) com nvarchar (parâmetro) força o SQL Server a converter a COLUNA,
                            // o que invalida o uso de índice e pode deixar a query muito mais lenta (ou
                            // até estourar timeout) em tabelas maiores.
                            if (parameter.Value is string stringValue)
                            {
                                command.Parameters.Add(new SqlParameter(parameter.Key, SqlDbType.VarChar) { Value = stringValue });
                            }
                            else
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                            }
                        }
                    }

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        System.Data.DataTable dataTable = new System.Data.DataTable();

                        await Task.Run(() => dataAdapter.Fill(dataTable));

                        return dataTable;
                    }
                }
            }
        }
    }
}
