using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WMTool.Business;
using WMTool.InsertScript;
using WMTool.InsertScript.Models;
using WMTool.Utils;

namespace WMTool.Screens
{
    // Caminho inverso do "Reprocessar JSON": pega um JSON e gera o script de texto de INSERT pra
    // popular tabelas (MC1_Invoice, MC1_Order etc.), a partir de regras por tabela configuradas
    // aqui. Mesma composição de tela do ucValidation/ucJsonReprocessor: grid de parâmetros no topo
    // ("Localizar/Substituir"), grid de regras (aqui, por tabela) e save/load de tudo num arquivo
    // JSON portátil + autocarga de um caminho padrão salvo em Settings.
    public partial class ucInsertScriptGenerator : UserControl
    {
        private readonly List<ParameterValue> _parameters = new List<ParameterValue>();
        private readonly List<TableInsertRule> _tables = new List<TableInsertRule>();

        public ucInsertScriptGenerator()
        {
            InitializeComponent();
            SetupParametersGrid();
            SetupTablesGrid();
            LoadDefaultRulesFromSettings();
        }

        // Usado pelo botão "Gerar Script INSERT" da aba Reprocessar JSON, mesmo espírito do
        // ucValidation.LoadJson: recebe o JSON já reprocessado e só cola no campo de entrada.
        public void LoadJson(string json)
        {
            txtJson.Text = json;
        }

        private static string ConnectionString => $"Server={Properties.Settings.Default.configServer};Database={Properties.Settings.Default.configDatabase};Integrated Security=true;";

        // Igual ao ucValidation.LoadDefaultRulesFromSettings: se houver um caminho configurado na
        // aba Settings, carrega automaticamente pra tela já abrir pronta pra gerar o script.
        private void LoadDefaultRulesFromSettings()
        {
            string path = Properties.Settings.Default.configInsertScriptRulesPath;
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (!File.Exists(path))
            {
                MessageBox.Show(
                    "O arquivo de regras padrão configurado em Settings não foi encontrado:\n" + path,
                    "Gerador de Script INSERT");
                return;
            }

            try
            {
                LoadRulesFromFile(path);
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                MessageBox.Show(
                    "Erro ao carregar as regras padrão configuradas em Settings: " + ex.Message,
                    "Gerador de Script INSERT");
            }
        }

        #region |Grid de Parâmetros (Localizar/Substituir)|

        private void SetupParametersGrid()
        {
            dgvParameters.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFind", HeaderText = "Localizar (ex.: {cIDOrderReferencia})", Width = 300 });
            dgvParameters.Columns.Add(new DataGridViewTextBoxColumn { Name = "colReplace", HeaderText = "Substituir", Width = 300 });
        }

        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            SyncParametersFromGrid();
            _parameters.Add(new ParameterValue());
            RefreshParametersGrid();
        }

        private void btnRemoveParameter_Click(object sender, EventArgs e)
        {
            if (dgvParameters.CurrentRow == null)
            {
                return;
            }

            SyncParametersFromGrid();
            int index = dgvParameters.CurrentRow.Index;
            if (index >= 0 && index < _parameters.Count)
            {
                _parameters.RemoveAt(index);
            }

            RefreshParametersGrid();
        }

        private void RefreshParametersGrid()
        {
            dgvParameters.Rows.Clear();

            foreach (ParameterValue parameter in _parameters)
            {
                dgvParameters.Rows.Add(FormatFind(parameter.Name), parameter.Value);
            }
        }

        private static string FormatFind(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            return name.StartsWith("{", StringComparison.Ordinal) ? name : "{" + name + "}";
        }

        // Aceita o nome digitado com ou sem chaves ("{cIDOrderReferencia}" ou "cIDOrderReferencia")
        // e sempre normaliza pro nome canônico sem chaves, que é como o motor de geração espera.
        private static string NormalizeParameterName(string rawFind)
        {
            if (string.IsNullOrWhiteSpace(rawFind))
            {
                return string.Empty;
            }

            return rawFind.Trim().TrimStart('{').TrimEnd('}');
        }

        private void SyncParametersFromGrid()
        {
            for (int i = 0; i < dgvParameters.Rows.Count && i < _parameters.Count; i++)
            {
                DataGridViewRow row = dgvParameters.Rows[i];
                ParameterValue parameter = _parameters[i];

                parameter.Name = NormalizeParameterName(row.Cells["colFind"].Value?.ToString());
                parameter.Value = row.Cells["colReplace"].Value?.ToString();
            }
        }

        #endregion

        #region |Grid de Tabelas|

        private void SetupTablesGrid()
        {
            dgvTables.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTableName", HeaderText = "Tabela", Width = 200 });

            dgvTables.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colRowSourceType",
                HeaderText = "Modo",
                Width = 100,
                DataSource = Enum.GetValues(typeof(RowSourceType))
            });

            dgvTables.Columns.Add(new DataGridViewTextBoxColumn { Name = "colArrayJsonPath", HeaderText = "Array JSON Path (se Modo=JsonArray)", Width = 260 });
            dgvTables.Columns.Add(new DataGridViewCheckBoxColumn { Name = "colEnabled", HeaderText = "Habilitada", Width = 70 });

            dgvTables.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colGeneralQuery",
                HeaderText = "Query Geral",
                Text = "Configurar",
                UseColumnTextForButtonValue = false,
                Width = 130
            });

            dgvTables.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colColumns",
                HeaderText = "Colunas",
                Text = "Editar Colunas",
                UseColumnTextForButtonValue = false,
                Width = 150
            });

            dgvTables.CellContentClick += DgvTables_CellContentClick;
            dgvTables.DataError += DgvGrid_DataError;
        }

        // Mesma guarda usada em ucValidation/ucJsonReprocessor contra combo com valor fora do enum.
        private void DgvGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            LogError.Log(e.Exception);
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void DgvTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string columnName = dgvTables.Columns[e.ColumnIndex].Name;
            if (columnName != "colGeneralQuery" && columnName != "colColumns")
            {
                return;
            }

            SyncTablesFromGrid();
            TableInsertRule table = _tables[e.RowIndex];

            if (columnName == "colGeneralQuery")
            {
                using (var editor = new frmRuleSqlEditor("Query Geral — " + table.TableName, table.GeneralSqlTemplate))
                {
                    if (editor.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    table.GeneralSqlTemplate = editor.ResultSql;
                }
            }
            else
            {
                using (var editor = new frmTableColumnsEditor(table.TableName, table.Columns))
                {
                    if (editor.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    table.Columns = editor.Columns;
                }
            }

            RefreshTablesGrid();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            SyncTablesFromGrid();
            _tables.Add(new TableInsertRule { TableName = "NovaTabela" });
            RefreshTablesGrid();
        }

        private void btnRemoveTable_Click(object sender, EventArgs e)
        {
            if (dgvTables.CurrentRow == null)
            {
                return;
            }

            SyncTablesFromGrid();
            int index = dgvTables.CurrentRow.Index;
            if (index >= 0 && index < _tables.Count)
            {
                _tables.RemoveAt(index);
            }

            RefreshTablesGrid();
        }

        private void RefreshTablesGrid()
        {
            dgvTables.Rows.Clear();

            foreach (TableInsertRule table in _tables)
            {
                int rowIndex = dgvTables.Rows.Add(
                    table.TableName,
                    table.RowSourceType,
                    table.ArrayJsonPath,
                    table.Enabled,
                    string.IsNullOrEmpty(table.GeneralSqlTemplate) ? "Configurar" : "Editar (custom)",
                    "Editar Colunas (" + (table.Columns?.Count ?? 0) + ")");

                dgvTables.Rows[rowIndex].Cells["colGeneralQuery"].ToolTipText = table.GeneralSqlTemplate;
            }
        }

        private void SyncTablesFromGrid()
        {
            for (int i = 0; i < dgvTables.Rows.Count && i < _tables.Count; i++)
            {
                DataGridViewRow row = dgvTables.Rows[i];
                TableInsertRule table = _tables[i];

                table.TableName = row.Cells["colTableName"].Value?.ToString();
                table.RowSourceType = ParseRowSourceType(row.Cells["colRowSourceType"].Value);
                table.ArrayJsonPath = row.Cells["colArrayJsonPath"].Value?.ToString();
                table.Enabled = row.Cells["colEnabled"].Value == null || Convert.ToBoolean(row.Cells["colEnabled"].Value);
            }
        }

        private static RowSourceType ParseRowSourceType(object cellValue)
        {
            if (cellValue is RowSourceType sourceType && Enum.IsDefined(typeof(RowSourceType), sourceType))
            {
                return sourceType;
            }

            if (cellValue != null && Enum.TryParse(cellValue.ToString(), out RowSourceType parsed) && Enum.IsDefined(typeof(RowSourceType), parsed))
            {
                return parsed;
            }

            return RowSourceType.SingleRow;
        }

        #endregion

        #region |JSON de entrada|

        private void btnLoadJsonFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "Arquivos JSON (*.json)|*.json" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtJson.Text = File.ReadAllText(dialog.FileName);
                }
            }
        }

        #endregion

        #region |Salvar/Carregar regras|

        private InsertScriptRuleSet BuildRuleSet()
        {
            SyncParametersFromGrid();
            SyncTablesFromGrid();

            return new InsertScriptRuleSet
            {
                Parameters = _parameters.ToList(),
                Tables = _tables.ToList()
            };
        }

        private void LoadRulesFromFile(string path)
        {
            InsertScriptRuleSet loaded = InsertScriptRuleSetStore.LoadRules(path);

            _parameters.Clear();
            _parameters.AddRange(loaded.Parameters ?? new List<ParameterValue>());

            _tables.Clear();
            _tables.AddRange(loaded.Tables ?? new List<TableInsertRule>());
            NormalizeTableEnums(_tables);

            RefreshParametersGrid();
            RefreshTablesGrid();
        }

        // Mesmo motivo do ucValidation.NormalizeRuleEnums: um arquivo de regras editado à mão pode
        // ter RowSourceType fora do range do enum, o que trava a grid assim que o usuário mexe
        // naquela célula de combo.
        private static void NormalizeTableEnums(List<TableInsertRule> tables)
        {
            foreach (TableInsertRule table in tables)
            {
                if (!Enum.IsDefined(typeof(RowSourceType), table.RowSourceType))
                {
                    table.RowSourceType = RowSourceType.SingleRow;
                }

                foreach (ColumnRule column in table.Columns ?? new List<ColumnRule>())
                {
                    if (!Enum.IsDefined(typeof(ColumnValueSourceType), column.SourceType))
                    {
                        column.SourceType = ColumnValueSourceType.JsonPath;
                    }
                }
            }
        }

        private void btnLoadRules_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "Arquivos JSON (*.json)|*.json" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    LoadRulesFromFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    LogError.Log(ex);
                    MessageBox.Show("Erro ao carregar regras: " + ex.Message);
                }
            }
        }

        private void btnSaveRules_Click(object sender, EventArgs e)
        {
            InsertScriptRuleSet ruleSet = BuildRuleSet();

            using (var dialog = new SaveFileDialog { Filter = "Arquivos JSON (*.json)|*.json", FileName = "insert_script_rules.json" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    InsertScriptRuleSetStore.SaveRules(dialog.FileName, ruleSet);
                    MessageBox.Show("Regras salvas em: " + dialog.FileName);
                }
                catch (Exception ex)
                {
                    LogError.Log(ex);
                    MessageBox.Show("Erro ao salvar regras: " + ex.Message);
                }
            }
        }

        #endregion

        #region |Geração do script|

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            await GenerateAsync();
        }

        private async Task GenerateAsync()
        {
            if (string.IsNullOrWhiteSpace(txtJson.Text))
            {
                MessageBox.Show("Cole o JSON de entrada ou carregue um arquivo.");
                return;
            }

            InsertScriptRuleSet ruleSet = BuildRuleSet();
            if (ruleSet.Tables.Count == 0 || ruleSet.Tables.All(t => !t.Enabled))
            {
                MessageBox.Show("Adicione ao menos uma tabela habilitada.");
                return;
            }

            JObject json;
            try
            {
                json = JObject.Parse(txtJson.Text);
            }
            catch (JsonException ex)
            {
                MessageBox.Show("JSON inválido: " + ex.Message);
                return;
            }

            btnGenerate.Enabled = false;
            progressBarInsertScript.Style = ProgressBarStyle.Marquee;
            txtResultSql.Text = string.Empty;
            txtWarnings.Text = string.Empty;

            try
            {
                var engine = new InsertScriptGenerationEngine(new WMBusiness());
                InsertScriptGenerationResult result = await engine.GenerateAsync(json, ruleSet, ConnectionString);

                txtResultSql.Text = result.Sql;
                txtWarnings.Text = result.Warnings.Count == 0
                    ? "Nenhum aviso."
                    : string.Join(Environment.NewLine, result.Warnings);
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                MessageBox.Show("Erro ao gerar o script: " + ex.Message);
            }
            finally
            {
                btnGenerate.Enabled = true;
                progressBarInsertScript.Style = ProgressBarStyle.Blocks;
            }
        }

        private void btnSaveScript_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResultSql.Text))
            {
                MessageBox.Show("Nada para salvar ainda. Gere o script primeiro.");
                return;
            }

            using (var dialog = new SaveFileDialog { Filter = "Arquivos SQL (*.sql)|*.sql", FileName = "insert_script.sql" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    File.WriteAllText(dialog.FileName, txtResultSql.Text);
                    MessageBox.Show("Script salvo em: " + dialog.FileName);
                }
                catch (Exception ex)
                {
                    LogError.Log(ex);
                    MessageBox.Show("Erro ao salvar arquivo: " + ex.Message);
                }
            }
        }

        #endregion
    }
}
