using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WMTool.Business;

namespace WMTool.Reprocessing.ViewDsl
{
    // A plataforma MC1 permite estender uma entidade base (ex.: MC1_BranchInvoice) com campos
    // customizados guardados numa tabela satélite "<Entidade>Ext" que compartilha a mesma chave
    // primária. As views referenciam esses campos como se fossem da entidade base
    // (ex.: "MC1_BranchInvoice.cBranchCNPJ"), e o motor de views original resolve isso de forma
    // transparente. Esta classe reproduz essa resolução, com cache em memória por processo.
    public class EntityExtensionResolver
    {
        private static readonly ConcurrentDictionary<string, HashSet<string>> ColumnsCache =
            new ConcurrentDictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        private static readonly ConcurrentDictionary<string, IReadOnlyList<string>> PrimaryKeyCache =
            new ConcurrentDictionary<string, IReadOnlyList<string>>(StringComparer.OrdinalIgnoreCase);

        private static readonly ConcurrentDictionary<string, string> ExtensionTableCache =
            new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private readonly WMBusiness _business;

        public EntityExtensionResolver(WMBusiness business)
        {
            _business = business;
        }

        public async Task<HashSet<string>> GetColumnsAsync(string tableName, string connectionString)
        {
            if (ColumnsCache.TryGetValue(tableName, out HashSet<string> cached))
            {
                return cached;
            }

            const string sql = "SELECT c.name AS ColumnName FROM sys.columns c " +
                                "JOIN sys.tables t ON t.object_id = c.object_id WHERE t.name = @tableName";

            DataTable result = await _business.ConsultDB(sql, connectionString, new Dictionary<string, object> { ["@tableName"] = tableName });

            var columns = new HashSet<string>(result.Rows.Cast<DataRow>().Select(r => r["ColumnName"].ToString()), StringComparer.OrdinalIgnoreCase);
            ColumnsCache[tableName] = columns;
            return columns;
        }

        public async Task<IReadOnlyList<string>> GetPrimaryKeyColumnsAsync(string tableName, string connectionString)
        {
            if (PrimaryKeyCache.TryGetValue(tableName, out IReadOnlyList<string> cached))
            {
                return cached;
            }

            const string sql = "SELECT c.name AS ColumnName FROM sys.tables t " +
                                "JOIN sys.indexes i ON i.object_id = t.object_id AND i.is_primary_key = 1 " +
                                "JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id " +
                                "JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id " +
                                "WHERE t.name = @tableName ORDER BY ic.key_ordinal";

            DataTable result = await _business.ConsultDB(sql, connectionString, new Dictionary<string, object> { ["@tableName"] = tableName });

            List<string> columns = result.Rows.Cast<DataRow>().Select(r => r["ColumnName"].ToString()).ToList();
            PrimaryKeyCache[tableName] = columns;
            return columns;
        }

        public async Task<string> GetExtensionTableNameAsync(string tableName, string connectionString)
        {
            if (ExtensionTableCache.TryGetValue(tableName, out string cached))
            {
                return cached == string.Empty ? null : cached;
            }

            string candidate = tableName + "Ext";
            const string sql = "SELECT name FROM sys.tables WHERE name = @tableName";

            DataTable result = await _business.ConsultDB(sql, connectionString, new Dictionary<string, object> { ["@tableName"] = candidate });

            string resolved = result.Rows.Count > 0 ? candidate : null;
            ExtensionTableCache[tableName] = resolved ?? string.Empty;
            return resolved;
        }
    }
}
