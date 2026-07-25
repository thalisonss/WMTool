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
    public class WMBusiness
    {
        WMDatabase wmDataBase = new WMDatabase();

        public async Task<System.Data.DataTable> ConsultDB(string sqlQuery, string connectionString)
        {
            if (String.IsNullOrEmpty(sqlQuery))
            {
                throw new ArgumentNullException("Campo de query vazia!");
            }

            EnsureQueryIsSafe(sqlQuery);

            {
                DataTable dataTable = await wmDataBase.ConsultDB(sqlQuery, connectionString);
                return dataTable;
            }
        }

        public async Task<System.Data.DataTable> ConsultDB(string sqlQuery, string connectionString, IDictionary<string, object> parameters)
        {
            if (String.IsNullOrEmpty(sqlQuery))
            {
                throw new ArgumentNullException("Campo de query vazia!");
            }

            EnsureQueryIsSafe(sqlQuery);

            return await wmDataBase.ConsultDB(sqlQuery, connectionString, parameters);
        }

        private static readonly string[] DangerousKeywords = { "DROP", "DELETE", "INSERT" };

        private static void EnsureQueryIsSafe(string sqlQuery)
        {
            string upperQuery = sqlQuery.ToUpper();

            foreach (string keyword in DangerousKeywords)
            {
                if (upperQuery.Contains(keyword))
                {
                    throw new ArgumentException($"A query contém uma palavra-chave perigosa: {keyword}");
                }
            }

            // "--" (comentário SQL) só é perigoso fora de um literal de string — dentro de um literal
            // (ex.: um valor de negócio como '---') é apenas dado, não um comentário injetado.
            if (ContainsOutsideStringLiterals(sqlQuery, "--"))
            {
                throw new ArgumentException("A query contém uma palavra-chave perigosa: --");
            }

            if (!Regex.IsMatch(sqlQuery, @"^\s*SELECT\s+", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("A query deve começar com uma cláusula SELECT.");
            }
        }

        private static bool ContainsOutsideStringLiterals(string sqlQuery, string token)
        {
            int i = 0;
            while (i < sqlQuery.Length)
            {
                if (sqlQuery[i] == '\'')
                {
                    i++;
                    while (i < sqlQuery.Length)
                    {
                        if (sqlQuery[i] == '\'')
                        {
                            if (i + 1 < sqlQuery.Length && sqlQuery[i + 1] == '\'')
                            {
                                i += 2;
                                continue;
                            }

                            i++;
                            break;
                        }

                        i++;
                    }

                    continue;
                }

                if (i + token.Length <= sqlQuery.Length && sqlQuery.Substring(i, token.Length) == token)
                {
                    return true;
                }

                i++;
            }

            return false;
        }
    }
}
