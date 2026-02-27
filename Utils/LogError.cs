using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMTool.Utils
{
    static class LogError
    {
        const string NOME_ARQUIVO = "log.txt";

        public static void Log(Exception exception)
        {
            using (var arquivo = new System.IO.StreamWriter(NOME_ARQUIVO, true)) { 
                arquivo.WriteLine($"{DateTime.Now} \t {exception} \n \n");
            }
        }
    }
}
