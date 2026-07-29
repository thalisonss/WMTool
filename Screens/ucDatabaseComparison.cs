using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WMTool.Business;
using WMTool.Utils;
using WMTool.Validation;
using WMTool.Validation.Models;

namespace WMTool.Screens
{
    public partial class ucDatabaseComparison : UserControl
    {
        private readonly List<DbComparisonRule> _rules = new List<DbComparisonRule>();
        private readonly List<DbRowSetComparisonRule> _rowSetRules = new List<DbRowSetComparisonRule>();
        private readonly List<DbPresenceRule> _presenceRules = new List<DbPresenceRule>();

        private string _generalOriginSqlTemplate = string.Empty;
        private string _generalDestinationSqlTemplate = string.Empty;
        private string _generalOriginRowSetSqlTemplate = string.Empty;
        private string _generalDestinationRowSetSqlTemplate = string.Empty;

        public ucDatabaseComparison()
        {
            InitializeComponent();
            SetupRulesGrid();
            SetupRowSetRulesGrid();
            SetupPresenceRulesGrid();
            SetupResultsGrid();
            LoadDefaultRulesFromSettings();
        }

        // Mesmo mecanismo do ucValidation: carrega automaticamente as regras do caminho configurado
        // na aba Settings, se houver um cadastrado. Avisa (em vez de falhar em silêncio) se o caminho
        // estiver configurado mas o arquivo não existir ou o carregamento falhar.
        private void LoadDefaultRulesFromSettings()
        {
            string path = Properties.Settings.Default.configDbComparisonRulesPath;
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show(
                    "O arquivo de regras padrão configurado em Settings não foi encontrado:\n" + path,
                    "Regras de comparação (Banco x Banco)");
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
                    "Regras de comparação (Banco x Banco)");
            }
        }

        private static string ConnectionString => $"Server={Properties.Settings.Default.configServer};Database={Properties.Settings.Default.configDatabase};Integrated Security=true;";

        #region Grid de regras — valor único

        private void SetupRulesGrid()
        {
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Nome", Width = 130 });

            dgvRules.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colOriginSource",
                HeaderText = "Fonte Origem",
                Width = 100,
                DataSource = Enum.GetValues(typeof(ComparisonSourceType))
            });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOriginSql",
                HeaderText = "SQL Origem (dois cliques)",
                Width = 280,
                ReadOnly = true
            });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOriginColumn", HeaderText = "Coluna Origem", Width = 110 });

            dgvRules.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colDestSource",
                HeaderText = "Fonte Destino",
                Width = 100,
                DataSource = Enum.GetValues(typeof(ComparisonSourceType))
            });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDestSql",
                HeaderText = "SQL Destino (dois cliques)",
                Width = 280,
                ReadOnly = true
            });
            dgvRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDestColumn", HeaderText = "Coluna Destino", Width = 110 });

            dgvRules.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colComparison",
                HeaderText = "Comparação",
                Width = 100,
                DataSource = Enum.GetValues(typeof(ComparisonType))
            });

            dgvRules.CellDoubleClick += DgvRules_CellDoubleClick;
            dgvRules.DataError += DgvGrid_DataError;
        }

        // Guarda contra o "Caixa de Diálogo de Erro Padrão de DataGridView" (ex.: célula de combo com um
        // valor que não bate com nenhum item da lista — acontece com regras carregadas de um arquivo
        // antigo/editado à mão, onde "Fonte"/"Comparação"/"Expectativa" vieram com um valor fora do
        // enum). Compartilhado pelas 3 grids de regra desta tela; sem esse handler, o WinForms mostra um
        // diálogo genérico e trava a edição da grid.
        private void DgvGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            LogError.Log(e.Exception);
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void DgvRules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string columnName = dgvRules.Columns[e.ColumnIndex].Name;
            if (columnName != "colOriginSql" && columnName != "colDestSql")
            {
                return;
            }

            SyncRulesFromGrid();

            DbComparisonRule rule = _rules[e.RowIndex];
            bool isOrigin = columnName == "colOriginSql";
            string title = (isOrigin ? "Origem — Regra: " : "Destino — Regra: ") + rule.Name;
            string currentSql = isOrigin ? rule.OriginSqlTemplate : rule.DestinationSqlTemplate;

            using (var editor = new frmRuleSqlEditor(title, currentSql))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (isOrigin)
                {
                    rule.OriginSqlTemplate = editor.ResultSql;
                }
                else
                {
                    rule.DestinationSqlTemplate = editor.ResultSql;
                }

                RefreshRulesGrid();
            }
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            SyncRulesFromGrid();
            _rules.Add(new DbComparisonRule { Name = "Nova regra" });
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

        private void RefreshRulesGrid()
        {
            dgvRules.Rows.Clear();

            foreach (DbComparisonRule rule in _rules)
            {
                dgvRules.Rows.Add(
                    rule.Name,
                    rule.OriginSourceType,
                    BuildSqlPreview(rule.OriginSqlTemplate),
                    rule.OriginResultColumn,
                    rule.DestinationSourceType,
                    BuildSqlPreview(rule.DestinationSqlTemplate),
                    rule.DestinationResultColumn,
                    rule.Comparison);
            }
        }

        private void SyncRulesFromGrid()
        {
            for (int i = 0; i < dgvRules.Rows.Count && i < _rules.Count; i++)
            {
                DataGridViewRow row = dgvRules.Rows[i];
                DbComparisonRule rule = _rules[i];

                rule.Name = row.Cells["colName"].Value?.ToString();
                rule.OriginSourceType = ParseSourceType(row.Cells["colOriginSource"].Value);
                rule.OriginResultColumn = row.Cells["colOriginColumn"].Value?.ToString();
                rule.DestinationSourceType = ParseSourceType(row.Cells["colDestSource"].Value);
                rule.DestinationResultColumn = row.Cells["colDestColumn"].Value?.ToString();
                rule.Comparison = ParseComparison(row.Cells["colComparison"].Value);
            }
        }

        private void btnConfigureGeneralOrigin_Click(object sender, EventArgs e)
        {
            using (var editor = new frmRuleSqlEditor("Query Geral - Origem (compartilhada entre as regras de valor único)", _generalOriginSqlTemplate))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _generalOriginSqlTemplate = editor.ResultSql;
                UpdateGeneralPreviewLabels();
            }
        }

        private void btnConfigureGeneralDestination_Click(object sender, EventArgs e)
        {
            using (var editor = new frmRuleSqlEditor("Query Geral - Destino (compartilhada entre as regras de valor único)", _generalDestinationSqlTemplate))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _generalDestinationSqlTemplate = editor.ResultSql;
                UpdateGeneralPreviewLabels();
            }
        }

        #endregion

        #region Grid de regras — N itens (linha-a-linha)

        private void SetupRowSetRulesGrid()
        {
            dgvRowSetRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Nome", Width = 110 });

            dgvRowSetRules.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colOriginSource",
                HeaderText = "Fonte Origem",
                Width = 90,
                DataSource = Enum.GetValues(typeof(ComparisonSourceType))
            });
            dgvRowSetRules.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOriginSql",
                HeaderText = "SQL Origem (dois cliques)",
                Width = 180,
                ReadOnly = true
            });
            dgvRowSetRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOriginKeyColumn", HeaderText = "Chave Origem", Width = 90 });
            dgvRowSetRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOriginValueColumn", HeaderText = "Valor Origem", Width = 90 });

            dgvRowSetRules.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colDestSource",
                HeaderText = "Fonte Destino",
                Width = 90,
                DataSource = Enum.GetValues(typeof(ComparisonSourceType))
            });
            dgvRowSetRules.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDestSql",
                HeaderText = "SQL Destino (dois cliques)",
                Width = 180,
                ReadOnly = true
            });
            dgvRowSetRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDestKeyColumn", HeaderText = "Chave Destino", Width = 90 });
            dgvRowSetRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDestValueColumn", HeaderText = "Valor Destino", Width = 90 });

            dgvRowSetRules.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colComparison",
                HeaderText = "Comparação",
                Width = 90,
                DataSource = Enum.GetValues(typeof(ComparisonType))
            });

            dgvRowSetRules.CellDoubleClick += DgvRowSetRules_CellDoubleClick;
            dgvRowSetRules.DataError += DgvGrid_DataError;
        }

        private void DgvRowSetRules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string columnName = dgvRowSetRules.Columns[e.ColumnIndex].Name;
            if (columnName != "colOriginSql" && columnName != "colDestSql")
            {
                return;
            }

            SyncRowSetRulesFromGrid();

            DbRowSetComparisonRule rule = _rowSetRules[e.RowIndex];
            bool isOrigin = columnName == "colOriginSql";
            string title = (isOrigin ? "Origem — Regra (N itens): " : "Destino — Regra (N itens): ") + rule.Name;
            string currentSql = isOrigin ? rule.OriginSqlTemplate : rule.DestinationSqlTemplate;

            using (var editor = new frmRuleSqlEditor(title, currentSql))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (isOrigin)
                {
                    rule.OriginSqlTemplate = editor.ResultSql;
                }
                else
                {
                    rule.DestinationSqlTemplate = editor.ResultSql;
                }

                RefreshRowSetRulesGrid();
            }
        }

        private void btnAddRowSetRule_Click(object sender, EventArgs e)
        {
            SyncRowSetRulesFromGrid();
            _rowSetRules.Add(new DbRowSetComparisonRule { Name = "Nova regra" });
            RefreshRowSetRulesGrid();
        }

        private void btnRemoveRowSetRule_Click(object sender, EventArgs e)
        {
            if (dgvRowSetRules.CurrentRow == null)
            {
                return;
            }

            SyncRowSetRulesFromGrid();
            int index = dgvRowSetRules.CurrentRow.Index;
            if (index >= 0 && index < _rowSetRules.Count)
            {
                _rowSetRules.RemoveAt(index);
            }

            RefreshRowSetRulesGrid();
        }

        private void RefreshRowSetRulesGrid()
        {
            dgvRowSetRules.Rows.Clear();

            foreach (DbRowSetComparisonRule rule in _rowSetRules)
            {
                dgvRowSetRules.Rows.Add(
                    rule.Name,
                    rule.OriginSourceType,
                    BuildSqlPreview(rule.OriginSqlTemplate),
                    rule.OriginKeyColumn,
                    rule.OriginValueColumn,
                    rule.DestinationSourceType,
                    BuildSqlPreview(rule.DestinationSqlTemplate),
                    rule.DestinationKeyColumn,
                    rule.DestinationValueColumn,
                    rule.Comparison);
            }
        }

        private void SyncRowSetRulesFromGrid()
        {
            for (int i = 0; i < dgvRowSetRules.Rows.Count && i < _rowSetRules.Count; i++)
            {
                DataGridViewRow row = dgvRowSetRules.Rows[i];
                DbRowSetComparisonRule rule = _rowSetRules[i];

                rule.Name = row.Cells["colName"].Value?.ToString();
                rule.OriginSourceType = ParseSourceType(row.Cells["colOriginSource"].Value);
                rule.OriginKeyColumn = row.Cells["colOriginKeyColumn"].Value?.ToString();
                rule.OriginValueColumn = row.Cells["colOriginValueColumn"].Value?.ToString();
                rule.DestinationSourceType = ParseSourceType(row.Cells["colDestSource"].Value);
                rule.DestinationKeyColumn = row.Cells["colDestKeyColumn"].Value?.ToString();
                rule.DestinationValueColumn = row.Cells["colDestValueColumn"].Value?.ToString();
                rule.Comparison = ParseComparison(row.Cells["colComparison"].Value);
            }
        }

        private void btnConfigureGeneralOriginRowSet_Click(object sender, EventArgs e)
        {
            using (var editor = new frmRuleSqlEditor("Query Geral Conjunto - Origem (várias linhas, ex.: todos os produtos)", _generalOriginRowSetSqlTemplate))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _generalOriginRowSetSqlTemplate = editor.ResultSql;
                UpdateGeneralPreviewLabels();
            }
        }

        private void btnConfigureGeneralDestinationRowSet_Click(object sender, EventArgs e)
        {
            using (var editor = new frmRuleSqlEditor("Query Geral Conjunto - Destino (várias linhas, ex.: todos os produtos)", _generalDestinationRowSetSqlTemplate))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _generalDestinationRowSetSqlTemplate = editor.ResultSql;
                UpdateGeneralPreviewLabels();
            }
        }

        #endregion

        #region Grid de regras — presença/ausência

        private void SetupPresenceRulesGrid()
        {
            dgvPresenceRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Nome", Width = 220 });
            dgvPresenceRules.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSql",
                HeaderText = "SQL (dois cliques para editar)",
                Width = 550,
                ReadOnly = true
            });
            dgvPresenceRules.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colExpectation",
                HeaderText = "Expectativa",
                Width = 160,
                DataSource = Enum.GetValues(typeof(PresenceExpectation))
            });
            dgvPresenceRules.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMessageColumn", HeaderText = "Coluna de mensagem (opcional)", Width = 220 });

            dgvPresenceRules.CellDoubleClick += DgvPresenceRules_CellDoubleClick;
            dgvPresenceRules.DataError += DgvGrid_DataError;
        }

        private void DgvPresenceRules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvPresenceRules.Columns[e.ColumnIndex].Name != "colSql")
            {
                return;
            }

            SyncPresenceRulesFromGrid();

            DbPresenceRule rule = _presenceRules[e.RowIndex];

            using (var editor = new frmRuleSqlEditor("Checagem: " + rule.Name, rule.SqlTemplate))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                rule.SqlTemplate = editor.ResultSql;
                RefreshPresenceRulesGrid();
            }
        }

        private void btnAddPresenceRule_Click(object sender, EventArgs e)
        {
            SyncPresenceRulesFromGrid();
            _presenceRules.Add(new DbPresenceRule { Name = "Nova checagem" });
            RefreshPresenceRulesGrid();
        }

        private void btnRemovePresenceRule_Click(object sender, EventArgs e)
        {
            if (dgvPresenceRules.CurrentRow == null)
            {
                return;
            }

            SyncPresenceRulesFromGrid();
            int index = dgvPresenceRules.CurrentRow.Index;
            if (index >= 0 && index < _presenceRules.Count)
            {
                _presenceRules.RemoveAt(index);
            }

            RefreshPresenceRulesGrid();
        }

        private void RefreshPresenceRulesGrid()
        {
            dgvPresenceRules.Rows.Clear();

            foreach (DbPresenceRule rule in _presenceRules)
            {
                dgvPresenceRules.Rows.Add(rule.Name, BuildSqlPreview(rule.SqlTemplate), rule.Expectation, rule.MessageColumn);
            }
        }

        private void SyncPresenceRulesFromGrid()
        {
            for (int i = 0; i < dgvPresenceRules.Rows.Count && i < _presenceRules.Count; i++)
            {
                DataGridViewRow row = dgvPresenceRules.Rows[i];
                DbPresenceRule rule = _presenceRules[i];

                rule.Name = row.Cells["colName"].Value?.ToString();
                rule.Expectation = ParseExpectation(row.Cells["colExpectation"].Value);
                rule.MessageColumn = row.Cells["colMessageColumn"].Value?.ToString();
            }
        }

        #endregion

        #region Resultados

        private void SetupResultsGrid()
        {
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCategory", HeaderText = "Categoria", Width = 110 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRule", HeaderText = "Regra", Width = 200 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colExpected", HeaderText = "Origem", Width = 220 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colActual", HeaderText = "Destino", Width = 220 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Status", Width = 80 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMessage", HeaderText = "Detalhe", Width = 420 });

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

        private void PopulateResults(string category, IEnumerable<ValidationRuleResult> results)
        {
            foreach (ValidationRuleResult result in results)
            {
                dgvResults.Rows.Add(category, result.RuleName, result.Expected, result.Actual, result.Passed ? "OK" : "Falhou", result.Message);
            }
        }

        #endregion

        #region Carregar/Salvar/Executar

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
            DbComparisonRuleSet loaded = DatabaseComparisonEngine.LoadRules(path);

            _rules.Clear();
            _rules.AddRange(loaded.Rules);
            _rowSetRules.Clear();
            _rowSetRules.AddRange(loaded.RowSetRules);
            _presenceRules.Clear();
            _presenceRules.AddRange(loaded.PresenceRules);
            NormalizeRuleEnums();

            _generalOriginSqlTemplate = loaded.GeneralOriginSqlTemplate ?? string.Empty;
            _generalDestinationSqlTemplate = loaded.GeneralDestinationSqlTemplate ?? string.Empty;
            _generalOriginRowSetSqlTemplate = loaded.GeneralOriginRowSetSqlTemplate ?? string.Empty;
            _generalDestinationRowSetSqlTemplate = loaded.GeneralDestinationRowSetSqlTemplate ?? string.Empty;

            RefreshRulesGrid();
            RefreshRowSetRulesGrid();
            RefreshPresenceRulesGrid();
            UpdateGeneralPreviewLabels();
        }

        // Mesmo motivo do ucValidation: um arquivo de regras antigo/editado à mão pode ter um enum fora
        // do range válido (Newtonsoft não valida isso na desserialização), o que trava a grid com o
        // diálogo padrão de erro assim que o usuário mexe naquela célula de combo.
        private void NormalizeRuleEnums()
        {
            foreach (DbComparisonRule rule in _rules)
            {
                if (!Enum.IsDefined(typeof(ComparisonSourceType), rule.OriginSourceType))
                {
                    rule.OriginSourceType = ComparisonSourceType.CustomSql;
                }

                if (!Enum.IsDefined(typeof(ComparisonSourceType), rule.DestinationSourceType))
                {
                    rule.DestinationSourceType = ComparisonSourceType.CustomSql;
                }

                if (!Enum.IsDefined(typeof(ComparisonType), rule.Comparison))
                {
                    rule.Comparison = ComparisonType.EqualsTrimmed;
                }
            }

            foreach (DbRowSetComparisonRule rule in _rowSetRules)
            {
                if (!Enum.IsDefined(typeof(ComparisonSourceType), rule.OriginSourceType))
                {
                    rule.OriginSourceType = ComparisonSourceType.CustomSql;
                }

                if (!Enum.IsDefined(typeof(ComparisonSourceType), rule.DestinationSourceType))
                {
                    rule.DestinationSourceType = ComparisonSourceType.CustomSql;
                }

                if (!Enum.IsDefined(typeof(ComparisonType), rule.Comparison))
                {
                    rule.Comparison = ComparisonType.EqualsTrimmed;
                }
            }

            foreach (DbPresenceRule rule in _presenceRules)
            {
                if (!Enum.IsDefined(typeof(PresenceExpectation), rule.Expectation))
                {
                    rule.Expectation = PresenceExpectation.RowsMustNotExist;
                }
            }
        }

        private void btnSaveRules_Click(object sender, EventArgs e)
        {
            SyncRulesFromGrid();
            SyncRowSetRulesFromGrid();
            SyncPresenceRulesFromGrid();

            using (var dialog = new SaveFileDialog { Filter = "Arquivos JSON (*.json)|*.json", FileName = "db_comparison_rules.json" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    var ruleSet = new DbComparisonRuleSet
                    {
                        GeneralOriginSqlTemplate = _generalOriginSqlTemplate,
                        GeneralDestinationSqlTemplate = _generalDestinationSqlTemplate,
                        GeneralOriginRowSetSqlTemplate = _generalOriginRowSetSqlTemplate,
                        GeneralDestinationRowSetSqlTemplate = _generalDestinationRowSetSqlTemplate,
                        Rules = _rules,
                        RowSetRules = _rowSetRules,
                        PresenceRules = _presenceRules
                    };
                    DatabaseComparisonEngine.SaveRules(dialog.FileName, ruleSet);
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
            SyncRowSetRulesFromGrid();
            SyncPresenceRulesFromGrid();

            if (_rules.Count == 0 && _rowSetRules.Count == 0 && _presenceRules.Count == 0)
            {
                MessageBox.Show("Adicione ao menos uma regra em alguma das abas.");
                return;
            }

            var context = new ValidationContextInputs
            {
                CIDInvoice = txtCIDInvoice.Text.Trim(),
                CSerie = txtCSerie.Text.Trim(),
                CIDBranchInvoice = txtCIDBranchInvoice.Text.Trim(),
                CIDCompany = txtCIDCompany.Text.Trim()
            };

            var engine = new DatabaseComparisonEngine(new WMBusiness());

            btnExecute.Enabled = false;
            progressBarComparison.Style = ProgressBarStyle.Marquee;
            dgvResults.Rows.Clear();

            try
            {
                if (_rules.Count > 0)
                {
                    List<ValidationRuleResult> results = await engine.RunAsync(
                        _rules, ConnectionString, context, _generalOriginSqlTemplate, _generalDestinationSqlTemplate);
                    PopulateResults("Valor único", results);
                }

                if (_rowSetRules.Count > 0)
                {
                    List<ValidationRuleResult> results = await engine.RunRowSetRulesAsync(
                        _rowSetRules, ConnectionString, context, _generalOriginRowSetSqlTemplate, _generalDestinationRowSetSqlTemplate);
                    PopulateResults("N itens", results);
                }

                if (_presenceRules.Count > 0)
                {
                    List<ValidationRuleResult> results = await engine.RunPresenceRulesAsync(_presenceRules, ConnectionString, context);
                    PopulateResults("Presença/Ausência", results);
                }
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                MessageBox.Show("Erro ao executar comparações: " + ex.Message);
            }
            finally
            {
                btnExecute.Enabled = true;
                progressBarComparison.Style = ProgressBarStyle.Blocks;
            }
        }

        #endregion

        #region Helpers compartilhados

        private void UpdateGeneralPreviewLabels()
        {
            lblGeneralOriginPreview.Text = string.IsNullOrEmpty(_generalOriginSqlTemplate)
                ? "Query Geral (Origem): não configurada"
                : "Query Geral (Origem): " + BuildSqlPreview(_generalOriginSqlTemplate);

            lblGeneralDestinationPreview.Text = string.IsNullOrEmpty(_generalDestinationSqlTemplate)
                ? "Query Geral (Destino): não configurada"
                : "Query Geral (Destino): " + BuildSqlPreview(_generalDestinationSqlTemplate);

            lblGeneralOriginRowSetPreview.Text = string.IsNullOrEmpty(_generalOriginRowSetSqlTemplate)
                ? "Query Geral Conjunto (Origem): não configurada"
                : "Query Geral Conjunto (Origem): " + BuildSqlPreview(_generalOriginRowSetSqlTemplate);

            lblGeneralDestinationRowSetPreview.Text = string.IsNullOrEmpty(_generalDestinationRowSetSqlTemplate)
                ? "Query Geral Conjunto (Destino): não configurada"
                : "Query Geral Conjunto (Destino): " + BuildSqlPreview(_generalDestinationRowSetSqlTemplate);
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

            const int maxLength = 100;
            return singleLine.Length > maxLength ? singleLine.Substring(0, maxLength) + "..." : singleLine;
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

        private static PresenceExpectation ParseExpectation(object cellValue)
        {
            if (cellValue is PresenceExpectation expectation)
            {
                return expectation;
            }

            if (cellValue != null && Enum.TryParse(cellValue.ToString(), out PresenceExpectation parsed))
            {
                return parsed;
            }

            return PresenceExpectation.RowsMustNotExist;
        }

        #endregion
    }
}
