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
    }
}
