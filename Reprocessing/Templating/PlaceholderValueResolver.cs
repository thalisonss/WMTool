using System;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using WMTool.Reprocessing.Exceptions;
using WMTool.Reprocessing.Models;

namespace WMTool.Reprocessing.Templating
{
    public class PlaceholderValueResolver
    {
        private readonly IReadOnlyDictionary<string, DataSourceResult> _dataSources;
        private readonly string _overrideAlias;
        private readonly DataRow _overrideRow;

        public PlaceholderValueResolver(IReadOnlyDictionary<string, DataSourceResult> dataSources)
            : this(dataSources, null, null)
        {
        }

        private PlaceholderValueResolver(IReadOnlyDictionary<string, DataSourceResult> dataSources, string overrideAlias, DataRow overrideRow)
        {
            _dataSources = dataSources;
            _overrideAlias = overrideAlias;
            _overrideRow = overrideRow;
        }

        public PlaceholderValueResolver WithRowOverride(string alias, DataRow row)
        {
            return new PlaceholderValueResolver(_dataSources, alias, row);
        }

        public string Resolve(string alias, string field)
        {
            DataRow row = string.Equals(alias, _overrideAlias, StringComparison.OrdinalIgnoreCase)
                ? _overrideRow
                : GetFirstRow(alias);

            // Data source desabilitado pelo usuário (pulado por ser lento) ou que legitimamente não
            // retornou linha: preferimos deixar o campo em branco no JSON final a interromper todo o
            // reprocessamento — assim dá pra validar o restante do pipeline mesmo com uma view pendente.
            if (row == null)
            {
                return string.Empty;
            }

            // Um campo pedido pelo template que não existe na view (descompasso de nome entre
            // MC1_DocumentTemplate e MC1_View, cadastro do cliente — não é algo que o WMTool controla)
            // também vira string vazia, pelo mesmo motivo do data source vazio acima: não faz sentido
            // travar o reprocessamento inteiro por causa de um campo isolado desalinhado.
            if (!row.Table.Columns.Contains(field))
            {
                return string.Empty;
            }

            object value = row[field];
            return FormatValue(value);
        }

        // Colunas datetime "cruas" (sem passar por date.toString na view) chegam aqui como DateTime do
        // .NET; Convert.ToString(DateTime, InvariantCulture) usa o padrão "MM/dd/yyyy HH:mm:ss" da
        // cultura invariante, que não bate com o que a plataforma real produz para esses campos
        // (verificado em dhEmi/dhSaiEnt/dVenc: sempre "yyyy-MM-dd HH:mm:ss"). Strings vindas de colunas
        // CHAR (largura fixa) chegam com espaços de preenchimento à direita — SqlClient não recorta isso
        // sozinho, então aparecia um "RODRIGO BENDER DETTENBORN  " em vez do valor real.
        private static string FormatValue(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return string.Empty;
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }

            if (value is string text)
            {
                return text.Trim();
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        private DataRow GetFirstRow(string alias)
        {
            if (!_dataSources.TryGetValue(alias, out DataSourceResult dataSource) || dataSource.Rows.Rows.Count == 0)
            {
                return null;
            }

            return dataSource.Rows.Rows[0];
        }
    }
}
