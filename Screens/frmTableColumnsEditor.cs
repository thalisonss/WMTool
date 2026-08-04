using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WMTool.InsertScript.Models;
using WMTool.Utils;

namespace WMTool.Screens
{
    // Dialog modal que edita a lista de ColumnRule de UMA tabela do gerador de script INSERT.
    // Mesmo padrão de dgvRules (ucValidation) para a grid + frmRuleSqlEditor reaproveitado pra
    // editar a query dedicada (SourceType == CustomSql) via duplo clique.
    public partial class frmTableColumnsEditor : Form
    {
        private readonly List<ColumnRule> _columns;

        public List<ColumnRule> Columns => _columns;

        public frmTableColumnsEditor(string tableName, IEnumerable<ColumnRule> columns)
        {
            InitializeComponent();
            lblTableName.Text = "Tabela: " + tableName;

            _columns = (columns ?? Enumerable.Empty<ColumnRule>())
                .Select(c => new ColumnRule
                {
                    ColumnName = c.ColumnName,
                    SourceType = c.SourceType,
                    JsonPath = c.JsonPath,
                    SqlTemplate = c.SqlTemplate,
                    ResultColumn = c.ResultColumn,
                    LiteralValue = c.LiteralValue
                })
                .ToList();

            SetupGrid();
            RefreshGrid();
        }

        private void SetupGrid()
        {
            dgvColumns.Columns.Add(new DataGridViewTextBoxColumn { Name = "colColumnName", HeaderText = "Nome da Coluna", Width = 160 });

            var sourceTypeColumn = new DataGridViewComboBoxColumn
            {
                Name = "colSourceType",
                HeaderText = "Fonte",
                Width = 110,
                DataSource = Enum.GetValues(typeof(ColumnValueSourceType))
            };
            dgvColumns.Columns.Add(sourceTypeColumn);

            dgvColumns.Columns.Add(new DataGridViewTextBoxColumn { Name = "colJsonPath", HeaderText = "JSON Path", Width = 200 });
            dgvColumns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSql",
                HeaderText = "SQL Customizado (2 cliques p/ editar)",
                Width = 260,
                ReadOnly = true
            });
            dgvColumns.Columns.Add(new DataGridViewTextBoxColumn { Name = "colResultColumn", HeaderText = "Coluna (Query Geral)", Width = 150 });
            dgvColumns.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLiteralValue", HeaderText = "Valor Literal (SQL cru)", Width = 150 });

            dgvColumns.CellDoubleClick += DgvColumns_CellDoubleClick;
            dgvColumns.DataError += DgvColumns_DataError;
        }

        // Mesma guarda usada em ucValidation/ucJsonReprocessor contra combo com valor fora do enum
        // (arquivo de regras editado à mão) travando a grid com o diálogo padrão de erro.
        private void DgvColumns_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            LogError.Log(e.Exception);
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void DgvColumns_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvColumns.Columns[e.ColumnIndex].Name != "colSql")
            {
                return;
            }

            SyncColumnsFromGrid();
            ColumnRule column = _columns[e.RowIndex];

            using (var editor = new frmRuleSqlEditor("Coluna: " + column.ColumnName, column.SqlTemplate))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                column.SqlTemplate = editor.ResultSql;
                RefreshGrid();
            }
        }

        private void btnAddColumn_Click(object sender, EventArgs e)
        {
            SyncColumnsFromGrid();
            _columns.Add(new ColumnRule { ColumnName = "NovaColuna", SourceType = ColumnValueSourceType.JsonPath });
            RefreshGrid();
        }

        private void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            if (dgvColumns.CurrentRow == null)
            {
                return;
            }

            SyncColumnsFromGrid();
            int index = dgvColumns.CurrentRow.Index;
            if (index >= 0 && index < _columns.Count)
            {
                _columns.RemoveAt(index);
            }

            RefreshGrid();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SyncColumnsFromGrid();

            if (_columns.Any(c => string.IsNullOrWhiteSpace(c.ColumnName)))
            {
                MessageBox.Show("Toda coluna precisa de um nome.");
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void RefreshGrid()
        {
            dgvColumns.Rows.Clear();

            foreach (ColumnRule column in _columns)
            {
                dgvColumns.Rows.Add(
                    column.ColumnName,
                    column.SourceType,
                    column.JsonPath,
                    BuildSqlPreview(column.SqlTemplate),
                    column.ResultColumn,
                    column.LiteralValue);
            }
        }

        private static string BuildSqlPreview(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return string.Empty;
            }

            string singleLine = sql.Replace("\r", " ").Replace("\n", " ");
            while (singleLine.Contains("  "))
            {
                singleLine = singleLine.Replace("  ", " ");
            }

            const int maxLength = 80;
            return singleLine.Length > maxLength ? singleLine.Substring(0, maxLength) + "..." : singleLine;
        }

        // colSql só mostra uma prévia (somente leitura) — o SQL completo de cada coluna fica em
        // _columns[i].SqlTemplate, editado exclusivamente via o dois-cliques (DgvColumns_CellDoubleClick).
        private void SyncColumnsFromGrid()
        {
            for (int i = 0; i < dgvColumns.Rows.Count && i < _columns.Count; i++)
            {
                DataGridViewRow row = dgvColumns.Rows[i];
                ColumnRule column = _columns[i];

                column.ColumnName = row.Cells["colColumnName"].Value?.ToString();
                column.SourceType = ParseSourceType(row.Cells["colSourceType"].Value);
                column.JsonPath = row.Cells["colJsonPath"].Value?.ToString();
                column.ResultColumn = row.Cells["colResultColumn"].Value?.ToString();
                column.LiteralValue = row.Cells["colLiteralValue"].Value?.ToString();
            }
        }

        private static ColumnValueSourceType ParseSourceType(object cellValue)
        {
            if (cellValue is ColumnValueSourceType sourceType && Enum.IsDefined(typeof(ColumnValueSourceType), sourceType))
            {
                return sourceType;
            }

            if (cellValue != null && Enum.TryParse(cellValue.ToString(), out ColumnValueSourceType parsed) && Enum.IsDefined(typeof(ColumnValueSourceType), parsed))
            {
                return parsed;
            }

            return ColumnValueSourceType.JsonPath;
        }
    }
}
