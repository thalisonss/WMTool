using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WMTool.Databases;

namespace WMTool.Business
{
    class WMBusiness 
    {
        WMDatabase wmDataBase = new WMDatabase();

        public async Task<System.Data.DataTable> ConsultDB(string sqlQuery, string connectionString)
        {
            if (String.IsNullOrEmpty(sqlQuery))
            {
                throw new ArgumentNullException("Campo de query vazia!");
            }

            List<string> dangerousKeywords = new List<string> { "DROP", "DELETE", "--", "INSERT"};
            foreach (string keyword in dangerousKeywords)
            {
                if (sqlQuery.ToUpper().Contains(keyword))
                {
                    throw new ArgumentException($"A query contém uma palavra-chave perigosa: {keyword}");
                }
            }

            if (!Regex.IsMatch(sqlQuery, @"^\s*SELECT\s+", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("A query deve começar com uma cláusula SELECT.");
            }

            {
                DataTable dataTable = await wmDataBase.ConsultDB(sqlQuery, connectionString);
                return dataTable;
            }
        }

    }
}
