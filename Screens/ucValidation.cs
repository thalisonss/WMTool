using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WMTool.Business;
using WMTool.Utils;
using WMTool.Validation;
using WMTool.Validation.Models;

namespace WMTool.Screens
{
    public partial class ucValidation : UserControl
    {
        private readonly List<ValidationRule> _rules = new List<ValidationRule>();
        private string _generalSqlTemplate = string.Empty;

        public ucValidation()
        {
            InitializeComponent();
            SetupRulesGrid();
            SetupResultsGrid();
            LoadDefaultRulesFromSettings();
        }

        // Carrega automaticamente as regras do caminho configurado na aba Settings, se houver um
        // cadastrado — assim a tela já abre pronta pra validar. Se o caminho estiver configurado
        // mas o carregamento falhar (arquivo ausente/inválido), avisa em vez de falhar em silêncio,
        // já que isso é indistinguível de "não carregou nada" pra quem está usando a tela.
        private void LoadDefaultRulesFromSettings()
        {
            string path = Properties.Settings.Default.configValidationRulesPath;
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (!File.Exists(path))
            {
                MessageBox.Show(
                    "O arquivo de regras padrão configurado em Settings não foi encontrado:\n" + path,
                    "Regras de validação (JSON x Banco)");
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
                    "Regras de validação (JSON x Banco)");
            }
        }

        // Usado pelo botão "Validar JSON" da aba Reprocessar JSON: recebe o JSON gerado + os 4
        // identificadores já digitados lá, prepara a tela no modo "arquivo único" e roda a validação
        // com as regras já carregadas.
        public void LoadJson(string json, string cIDInvoice, string cSerie, string cIDBranchInvoice, string cIDCompany)
        {
            txtJson.Text = json;
            rbSingleFile.Checked = true;
            txtCIDInvoice.Text = cIDInvoice;
            txtCSerie.Text = cSerie;
            txtCIDBranchInvoice.Text = cIDBranchInvoice;
            txtCIDCompany.Text = cIDCompany;
        }

        public Task RunValidationAsync()
        {
            return ExecuteValidationAsync();
        }

        private void SetupRulesGrid()
        {
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Nome", Width = 150 });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colJsonPath", HeaderText = "JSON Path", Width = 220 });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSql",
                HeaderText = "SQL (dois cliques para editar)",
                Width = 380,
                ReadOnly = true
            });
            var sourceTypeColumn = new DataGridViewComboBoxColumn
            {
                Name = "colSourceType",
                HeaderText = "Fonte",
                Width = 110,
                DataSource = Enum.GetValues(typeof(ComparisonSourceType))
            };
            dgvRules.Columns.Add(sourceTypeColumn);

            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colResultColumn", HeaderText = "Coluna Resultado", Width = 130 });

            var comparisonColumn = new DataGridViewComboBoxColumn
            {
                Name = "colComparison",
                HeaderText = "Comparação",
                Width = 110,
                DataSource = Enum.GetValues(typeof(ComparisonType))
            };
            dgvRules.Columns.Add(comparisonColumn);

            dgvRules.CellDoubleClick += DgvRules_CellDoubleClick;
        }

        // Igual ao "Configurar" da grid de data sources da aba Reprocessar JSON: a query de uma regra
        // costuma ser grande demais pra editar dentro da célula, então dois cliques abrem um editor
        // dedicado; a célula em si só mostra uma prévia truncada.
        private void DgvRules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvRules.Columns[e.ColumnIndex].Name != "colSql")
            {
                return;
            }

            SyncRulesFromGrid();

            ValidationRule rule = _rules[e.RowIndex];

            using (var editor = new frmRuleSqlEditor("Regra: " + rule.Name, rule.SqlTemplate))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                rule.SqlTemplate = editor.ResultSql;
                RefreshRulesGrid();
            }
        }

        private void btnConfigureGeneralSql_Click(object sender, EventArgs e)
        {
            using (var editor = new frmRuleSqlEditor("Query Geral (compartilhada entre as regras)", _generalSqlTemplate))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _generalSqlTemplate = editor.ResultSql;
                UpdateGeneralSqlPreviewLabel();
            }
        }

        private void UpdateGeneralSqlPreviewLabel()
        {
            lblGeneralSqlPreview.Text = string.IsNullOrEmpty(_generalSqlTemplate)
                ? "Query Geral: não configurada"
                : "Query Geral: " + BuildSqlPreview(_generalSqlTemplate);
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

            const int maxLength = 120;
            return singleLine.Length > maxLength ? singleLine.Substring(0, maxLength) + "..." : singleLine;
        }

        private void SetupResultsGrid()
        {
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFile", HeaderText = "Arquivo", Width = 150 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRule", HeaderText = "Regra", Width = 150 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colExpected", HeaderText = "Esperado", Width = 200 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colActual", HeaderText = "Obtido", Width = 200 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Status", Width = 80 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMessage", HeaderText = "Detalhe", Width = 400 });

            dgvResults.CellFormatting += DgvResults_CellFormatting;
        }

        private void DgvResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvResults.Columns[e.ColumnIndex].Name != "colStatus" || e.Value == null)
            {
                return;
            }

            bool passed = string.Equals(e.Value.ToString(), "OK", StringComparison.OrdinalIgnoreCase);
            e.CellStyle.BackColor = passed ? System.Drawing.Color.LightGreen : System.Drawing.Color.LightCoral;
        }

        private void btnLoadJsonFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "Arquivos JSON (*.json)|*.json" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtJson.Text = File.ReadAllText(dialog.FileName);
                    rbSingleFile.Checked = true;
                }
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = dialog.SelectedPath;
                    rbBatchFolder.Checked = true;
                }
            }
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            SyncRulesFromGrid();
            _rules.Add(new ValidationRule { Name = "Nova regra", Comparison = ComparisonType.EqualsTrimmed });
            RefreshRulesGrid();
        }

        private void btnRemoveRule_Click(object sender, EventArgs e)
        {
            if (dgvRules.CurrentRow == null)
            {
                return;
            }

            SyncRulesFromGrid();
            int index = dgvRules.CurrentRow.Index;
            if (index >= 0 && index < _rules.Count)
            {
                _rules.RemoveAt(index);
            }

            RefreshRulesGrid();
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

        private void LoadRulesFromFile(string path)
        {
            ValidationRuleSet loaded = ValidationEngine.LoadRules(path);
            _rules.Clear();
            _rules.AddRange(loaded.Rules);
            _generalSqlTemplate = loaded.GeneralSqlTemplate ?? string.Empty;
            RefreshRulesGrid();
            UpdateGeneralSqlPreviewLabel();
        }

        private void btnSaveRules_Click(object sender, EventArgs e)
        {
            SyncRulesFromGrid();

            using (var dialog = new SaveFileDialog { Filter = "Arquivos JSON (*.json)|*.json", FileName = "validation_rules.json" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    var ruleSet = new ValidationRuleSet { GeneralSqlTemplate = _generalSqlTemplate, Rules = _rules };
                    ValidationEngine.SaveRules(dialog.FileName, ruleSet);
                    MessageBox.Show("Regras salvas em: " + dialog.FileName);
                }
                catch (Exception ex)
                {
                    LogError.Log(ex);
                    MessageBox.Show("Erro ao salvar regras: " + ex.Message);
                }
            }
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            await ExecuteValidationAsync();
        }

        private async Task ExecuteValidationAsync()
        {
            SyncRulesFromGrid();

            if (_rules.Count == 0)
            {
                MessageBox.Show("Adicione ao menos uma regra de verificação.");
                return;
            }

            string connectionString = $"Server={Properties.Settings.Default.configServer};Database={Properties.Settings.Default.configDatabase};Integrated Security=true;";
            var engine = new ValidationEngine(new WMBusiness());
            var context = new ValidationContextInputs
            {
                CIDInvoice = txtCIDInvoice.Text.Trim(),
                CSerie = txtCSerie.Text.Trim(),
                CIDBranchInvoice = txtCIDBranchInvoice.Text.Trim(),
                CIDCompany = txtCIDCompany.Text.Trim()
            };

            btnExecute.Enabled = false;
            progressBarValidation.Style = ProgressBarStyle.Marquee;
            dgvResults.Rows.Clear();

            try
            {
                if (rbBatchFolder.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtFolderPath.Text) || !Directory.Exists(txtFolderPath.Text))
                    {
                        MessageBox.Show("Selecione uma pasta válida.");
                        return;
                    }

                    List<FileValidationResult> batchResults = await engine.RunBatchAsync(txtFolderPath.Text, _rules, connectionString, context, _generalSqlTemplate);
                    PopulateBatchResults(batchResults);
                }
                else
                {
                    JObject json = JObject.Parse(txtJson.Text);
                    List<ValidationRuleResult> results = await engine.RunAsync(json, _rules, connectionString, context, _generalSqlTemplate);
                    PopulateResults(results, null);
                }
            }
            catch (JsonException ex)
            {
                MessageBox.Show("JSON inválido: " + ex.Message);
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                MessageBox.Show("Erro ao executar verificações: " + ex.Message);
            }
            finally
            {
                btnExecute.Enabled = true;
                progressBarValidation.Style = ProgressBarStyle.Blocks;
            }
        }

        private void PopulateResults(IEnumerable<ValidationRuleResult> results, string fileName)
        {
            foreach (ValidationRuleResult result in results)
            {
                dgvResults.Rows.Add(fileName, result.RuleName, result.Expected, result.Actual, result.Passed ? "OK" : "Falhou", result.Message);
            }
        }

        private void PopulateBatchResults(IEnumerable<FileValidationResult> fileResults)
        {
            foreach (FileValidationResult fileResult in fileResults)
            {
                if (fileResult.ParseError != null)
                {
                    dgvResults.Rows.Add(fileResult.FileName, string.Empty, string.Empty, string.Empty, "Falhou", "Erro ao ler JSON: " + fileResult.ParseError);
                    continue;
                }

                PopulateResults(fileResult.Results, fileResult.FileName);
            }
        }

        private void RefreshRulesGrid()
        {
            dgvRules.Rows.Clear();

            foreach (ValidationRule rule in _rules)
            {
                dgvRules.Rows.Add(rule.Name, rule.JsonPath, BuildSqlPreview(rule.SqlTemplate), rule.SourceType, rule.ResultColumn, rule.Comparison);
            }
        }

        // colSql só mostra uma prévia (somente leitura) — o SQL completo de cada regra fica em
        // _rules[i].SqlTemplate, editado exclusivamente via o dois-cliques (DgvRules_CellDoubleClick).
        private void SyncRulesFromGrid()
        {
            for (int i = 0; i < dgvRules.Rows.Count && i < _rules.Count; i++)
            {
                DataGridViewRow row = dgvRules.Rows[i];
                ValidationRule rule = _rules[i];

                rule.Name = row.Cells["colName"].Value?.ToString();
                rule.JsonPath = row.Cells["colJsonPath"].Value?.ToString();
                rule.SourceType = ParseSourceType(row.Cells["colSourceType"].Value);
                rule.ResultColumn = row.Cells["colResultColumn"].Value?.ToString();
                rule.Comparison = ParseComparison(row.Cells["colComparison"].Value);
            }
        }

        private static ComparisonType ParseComparison(object cellValue)
        {
            if (cellValue is ComparisonType comparison)
            {
                return comparison;
            }

            if (cellValue != null && Enum.TryParse(cellValue.ToString(), out ComparisonType parsed))
            {
                return parsed;
            }

            return ComparisonType.EqualsTrimmed;
        }

        private static ComparisonSourceType ParseSourceType(object cellValue)
        {
            if (cellValue is ComparisonSourceType sourceType)
            {
                return sourceType;
            }

            if (cellValue != null && Enum.TryParse(cellValue.ToString(), out ComparisonSourceType parsed))
            {
                return parsed;
            }

            return ComparisonSourceType.CustomSql;
        }

    }
}
