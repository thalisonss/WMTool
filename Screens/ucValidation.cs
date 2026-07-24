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

        public ucValidation()
        {
            InitializeComponent();
            SetupRulesGrid();
            SetupResultsGrid();
        }

        private void SetupRulesGrid()
        {
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Nome", Width = 150 });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colJsonPath", HeaderText = "JSON Path", Width = 220 });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSql", HeaderText = "SQL (use {varNome})", Width = 380 });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colResultColumn", HeaderText = "Coluna Resultado", Width = 130 });

            var comparisonColumn = new DataGridViewComboBoxColumn
            {
                Name = "colComparison",
                HeaderText = "Comparação",
                Width = 110,
                DataSource = Enum.GetValues(typeof(ComparisonType))
            };
            dgvRules.Columns.Add(comparisonColumn);

            dgvRules.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colParameters",
                HeaderText = "Parâmetros (nome=json:caminho;nome2=fixo:valor)",
                Width = 320
            });
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
                    List<ValidationRule> loaded = ValidationEngine.LoadRules(dialog.FileName);
                    _rules.Clear();
                    _rules.AddRange(loaded);
                    RefreshRulesGrid();
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
            SyncRulesFromGrid();

            using (var dialog = new SaveFileDialog { Filter = "Arquivos JSON (*.json)|*.json", FileName = "validation_rules.json" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    ValidationEngine.SaveRules(dialog.FileName, _rules);
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
            SyncRulesFromGrid();

            if (_rules.Count == 0)
            {
                MessageBox.Show("Adicione ao menos uma regra de verificação.");
                return;
            }

            string connectionString = $"Server={Properties.Settings.Default.configServer};Database={Properties.Settings.Default.configDatabase};Integrated Security=true;";
            var engine = new ValidationEngine(new WMBusiness());

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

                    List<FileValidationResult> batchResults = await engine.RunBatchAsync(txtFolderPath.Text, _rules, connectionString);
                    PopulateBatchResults(batchResults);
                }
                else
                {
                    JObject json = JObject.Parse(txtJson.Text);
                    List<ValidationRuleResult> results = await engine.RunAsync(json, _rules, connectionString);
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
                dgvRules.Rows.Add(rule.Name, rule.JsonPath, rule.SqlTemplate, rule.ResultColumn, rule.Comparison, EncodeParameters(rule.Parameters));
            }
        }

        private void SyncRulesFromGrid()
        {
            for (int i = 0; i < dgvRules.Rows.Count && i < _rules.Count; i++)
            {
                DataGridViewRow row = dgvRules.Rows[i];
                ValidationRule rule = _rules[i];

                rule.Name = row.Cells["colName"].Value?.ToString();
                rule.JsonPath = row.Cells["colJsonPath"].Value?.ToString();
                rule.SqlTemplate = row.Cells["colSql"].Value?.ToString();
                rule.ResultColumn = row.Cells["colResultColumn"].Value?.ToString();
                rule.Comparison = ParseComparison(row.Cells["colComparison"].Value);
                rule.Parameters = DecodeParameters(row.Cells["colParameters"].Value?.ToString());
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

        private static string EncodeParameters(List<RuleParameter> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return string.Empty;
            }

            return string.Join(";", parameters.Select(p =>
                $"{p.Name}={(p.SourceType == RuleParameterSource.JsonPath ? "json" : "fixo")}:{p.Value}"));
        }

        private static List<RuleParameter> DecodeParameters(string encoded)
        {
            var parameters = new List<RuleParameter>();

            if (string.IsNullOrWhiteSpace(encoded))
            {
                return parameters;
            }

            foreach (string entry in encoded.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(entry))
                {
                    continue;
                }

                int nameSeparator = entry.IndexOf('=');
                if (nameSeparator < 0)
                {
                    continue;
                }

                string name = entry.Substring(0, nameSeparator).Trim();
                string rest = entry.Substring(nameSeparator + 1);

                int sourceSeparator = rest.IndexOf(':');
                if (sourceSeparator < 0)
                {
                    continue;
                }

                string source = rest.Substring(0, sourceSeparator).Trim();
                string value = rest.Substring(sourceSeparator + 1);

                parameters.Add(new RuleParameter
                {
                    Name = name,
                    SourceType = string.Equals(source, "fixo", StringComparison.OrdinalIgnoreCase)
                        ? RuleParameterSource.FixedValue
                        : RuleParameterSource.JsonPath,
                    Value = value
                });
            }

            return parameters;
        }
    }
}
