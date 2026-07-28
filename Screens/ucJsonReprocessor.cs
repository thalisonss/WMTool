using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WMTool.Business;
using WMTool.Reprocessing;
using WMTool.Reprocessing.Caching;
using WMTool.Reprocessing.CustomSql;
using WMTool.Reprocessing.Models;
using WMTool.Utils;

namespace WMTool.Screens
{
    public partial class ucJsonReprocessor : UserControl
    {
        // frmHomeScreen assina esse evento pra levar o JSON gerado (+ os 4 identificadores já digitados
        // aqui) até a aba ucValidation — as duas telas são UserControls irmãos, sem referência direta
        // entre si.
        public event Action<string, string, string, string, string> ValidateJsonRequested;

        public ucJsonReprocessor()
        {
            InitializeComponent();
            SetupParameterOverridesGrid();
            SetupDataSourcesGrid();
            LoadDataSourcesFromCache();
            LoadLastInputs();
        }

        // A lista de data sources (alias/view) quase nunca muda, então a tela abre com a grid já
        // populada a partir de um snapshot local (sem consulta ao banco) — só é atualizada de verdade
        // quando o usuário clica em "Atualizar DataSources".
        private void LoadDataSourcesFromCache()
        {
            CachedDataSourceSet cached = new DataSourceCacheStore().Load();
            if (cached != null)
            {
                RefreshDataSourcesGrid(cached.DataSources);
            }

            UpdateDataSourcesLastUpdatedLabel(cached);
        }

        private void UpdateDataSourcesLastUpdatedLabel(CachedDataSourceSet cached)
        {
            lblDataSourcesLastUpdated.Text = cached == null
                ? "DataSources: nunca atualizado"
                : "DataSources atualizados em: " + cached.LastUpdatedUtc.ToLocalTime().ToString("g");
        }

        // Reabrir a tela (ou o app) com os mesmos campos da última nota reprocessada — é raro trocar de
        // nota entre uma sessão de trabalho e outra.
        private void LoadLastInputs()
        {
            LastReprocessInputs last = new LastInputsStore().Load();
            if (last == null)
            {
                return;
            }

            txtCIDInvoice.Text = last.CIDInvoice;
            txtCSerie.Text = last.CSerie;
            txtCIDBranchInvoice.Text = last.CIDBranchInvoice;
            txtCIDCompany.Text = last.CIDCompany;
        }

        private void SaveLastInputs()
        {
            new LastInputsStore().Save(new LastReprocessInputs
            {
                CIDInvoice = txtCIDInvoice.Text.Trim(),
                CSerie = txtCSerie.Text.Trim(),
                CIDBranchInvoice = txtCIDBranchInvoice.Text.Trim(),
                CIDCompany = txtCIDCompany.Text.Trim()
            });
        }

        private void SetupParameterOverridesGrid()
        {
            dgvParameterOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colParameterName",
                HeaderText = "Nome do Parâmetro",
                ReadOnly = true,
                Width = 260
            });

            dgvParameterOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSuggestedValue",
                HeaderText = "Valor Sugerido",
                ReadOnly = true,
                Width = 350
            });

            dgvParameterOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOverrideValue",
                HeaderText = "Override (opcional)",
                Width = 350
            });
        }

        private void SetupDataSourcesGrid()
        {
            dgvDataSources.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDataSourceAlias",
                HeaderText = "Alias",
                ReadOnly = true,
                Width = 120
            });

            dgvDataSources.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDataSourceView",
                HeaderText = "View",
                ReadOnly = true,
                Width = 170
            });

            dgvDataSources.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colDataSourceEnabled",
                HeaderText = "Executar",
                Width = 60
            });

            dgvDataSources.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colDataSourceCustomize",
                HeaderText = "SQL Customizado",
                Text = "Configurar",
                UseColumnTextForButtonValue = false,
                Width = 120
            });

            dgvDataSources.CellContentClick += DgvDataSources_CellContentClick;
        }

        private void DgvDataSources_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvDataSources.Columns[e.ColumnIndex].Name != "colDataSourceCustomize")
            {
                return;
            }

            DataGridViewRow row = dgvDataSources.Rows[e.RowIndex];
            string viewName = row.Cells["colDataSourceView"].Value?.ToString();
            if (string.IsNullOrEmpty(viewName))
            {
                return;
            }

            var store = new CustomViewSqlOverrideStore();
            string currentSql = store.GetSql(viewName);

            using (var editor = new frmCustomViewSqlEditor(viewName, currentSql))
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (editor.WasRemoved)
                {
                    store.Remove(viewName);
                }
                else
                {
                    store.Save(viewName, editor.ResultSql);
                }

                UpdateCustomizeButtonLabel(row, viewName, store);
            }
        }

        private static void UpdateCustomizeButtonLabel(DataGridViewRow row, string viewName, CustomViewSqlOverrideStore store)
        {
            bool hasOverride = store.GetSql(viewName) != null;
            row.Cells["colDataSourceCustomize"].Value = hasOverride ? "Editar (custom)" : "Configurar";
        }

        private static string ConnectionString => $"Server={Properties.Settings.Default.configServer};Database={Properties.Settings.Default.configDatabase};Integrated Security=true;";

        private MasterParameterInputs BuildInputs()
        {
            var overrides = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (DataGridViewRow row in dgvParameterOverrides.Rows)
            {
                string name = row.Cells["colParameterName"].Value?.ToString();
                string overrideValue = row.Cells["colOverrideValue"].Value?.ToString();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(overrideValue))
                {
                    overrides[name] = overrideValue;
                }
            }

            var disabledAliases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataGridViewRow row in dgvDataSources.Rows)
            {
                string alias = row.Cells["colDataSourceAlias"].Value?.ToString();
                bool enabled = row.Cells["colDataSourceEnabled"].Value != null && Convert.ToBoolean(row.Cells["colDataSourceEnabled"].Value);

                if (!string.IsNullOrEmpty(alias) && !enabled)
                {
                    disabledAliases.Add(alias);
                }
            }

            return new MasterParameterInputs
            {
                CIDInvoice = txtCIDInvoice.Text.Trim(),
                CSerie = txtCSerie.Text.Trim(),
                CIDBranchInvoice = txtCIDBranchInvoice.Text.Trim(),
                CIDCompany = txtCIDCompany.Text.Trim(),
                Overrides = overrides,
                DisabledAliases = disabledAliases
            };
        }

        private void RefreshDataSourcesGrid(IReadOnlyList<DataSourceReference> dataSources)
        {
            var previouslyEnabled = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            foreach (DataGridViewRow row in dgvDataSources.Rows)
            {
                string alias = row.Cells["colDataSourceAlias"].Value?.ToString();
                if (!string.IsNullOrEmpty(alias))
                {
                    previouslyEnabled[alias] = row.Cells["colDataSourceEnabled"].Value == null || Convert.ToBoolean(row.Cells["colDataSourceEnabled"].Value);
                }
            }

            var store = new CustomViewSqlOverrideStore();

            dgvDataSources.Rows.Clear();
            foreach (DataSourceReference dataSource in dataSources)
            {
                bool enabled = !previouslyEnabled.TryGetValue(dataSource.Alias, out bool previousValue) || previousValue;
                bool hasOverride = store.GetSql(dataSource.ViewName) != null;
                dgvDataSources.Rows.Add(dataSource.Alias, dataSource.ViewName, enabled, hasOverride ? "Editar (custom)" : "Configurar");
            }
        }

        private bool ValidateRequiredInputs()
        {
            if (string.IsNullOrWhiteSpace(txtCIDInvoice.Text) ||
                string.IsNullOrWhiteSpace(txtCSerie.Text) ||
                string.IsNullOrWhiteSpace(txtCIDBranchInvoice.Text) ||
                string.IsNullOrWhiteSpace(txtCIDCompany.Text))
            {
                MessageBox.Show("Preencha cIDInvoice, cSerie, cIDBranchInvoice e cIDCompany.");
                return false;
            }

            return true;
        }

        private async void btnDiscoverParameters_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredInputs())
            {
                return;
            }

            btnDiscoverParameters.Enabled = false;
            btnExecute.Enabled = false;
            progressBarReprocess.Style = ProgressBarStyle.Marquee;

            try
            {
                var engine = new JsonReprocessingEngine(new WMBusiness());
                IReadOnlyList<RequiredParameterInfo> parameters = await engine.DiscoverRequiredParametersAsync(BuildInputs(), ConnectionString);

                dgvParameterOverrides.Rows.Clear();
                foreach (RequiredParameterInfo parameter in parameters)
                {
                    dgvParameterOverrides.Rows.Add(parameter.ParameterName, parameter.SuggestedValue, string.Empty);
                }
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnDiscoverParameters.Enabled = true;
                btnExecute.Enabled = true;
                progressBarReprocess.Style = ProgressBarStyle.Blocks;
            }
        }

        private async void btnRefreshDataSources_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Isso vai buscar a definição atual do template e de cada view no banco, traduzir a DSL de novo " +
                "e sobrescrever o SQL customizado de cada data source (inclusive os que você já editou manualmente). Continuar?",
                "Atualizar DataSources",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            btnDiscoverParameters.Enabled = false;
            btnExecute.Enabled = false;
            btnRefreshDataSources.Enabled = false;
            progressBarReprocess.Style = ProgressBarStyle.Marquee;

            try
            {
                var engine = new JsonReprocessingEngine(new WMBusiness());
                IReadOnlyList<DataSourceReference> dataSources = await engine.RefreshDataSourcesAsync(ConnectionString);

                RefreshDataSourcesGrid(dataSources);
                UpdateDataSourcesLastUpdatedLabel(new DataSourceCacheStore().Load());
                MessageBox.Show($"{dataSources.Count} data source(s) atualizado(s).");
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnDiscoverParameters.Enabled = true;
                btnExecute.Enabled = true;
                btnRefreshDataSources.Enabled = true;
                progressBarReprocess.Style = ProgressBarStyle.Blocks;
            }
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredInputs())
            {
                return;
            }

            SaveLastInputs();

            btnDiscoverParameters.Enabled = false;
            btnExecute.Enabled = false;
            progressBarReprocess.Style = ProgressBarStyle.Marquee;
            txtResultJson.Text = string.Empty;

            try
            {
                var engine = new JsonReprocessingEngine(new WMBusiness());
                JObject result = await engine.ReprocessAsync(BuildInputs(), ConnectionString);

                txtResultJson.Text = result.ToString(Formatting.Indented);
            }
            catch (Exception ex)
            {
                LogError.Log(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnDiscoverParameters.Enabled = true;
                btnExecute.Enabled = true;
                progressBarReprocess.Style = ProgressBarStyle.Blocks;
            }
        }

        private void btnValidateJson_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResultJson.Text))
            {
                MessageBox.Show("Nada para validar ainda. Execute o reprocessamento primeiro.");
                return;
            }

            ValidateJsonRequested?.Invoke(
                txtResultJson.Text,
                txtCIDInvoice.Text.Trim(),
                txtCSerie.Text.Trim(),
                txtCIDBranchInvoice.Text.Trim(),
                txtCIDCompany.Text.Trim());
        }

        private void btnSaveJson_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResultJson.Text))
            {
                MessageBox.Show("Nada para salvar ainda. Execute o reprocessamento primeiro.");
                return;
            }

            using (var dialog = new SaveFileDialog { Filter = "Arquivos JSON (*.json)|*.json", FileName = "reprocessed_invoice.json" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    File.WriteAllText(dialog.FileName, txtResultJson.Text);
                    MessageBox.Show("JSON salvo em: " + dialog.FileName);
                }
                catch (Exception ex)
                {
                    LogError.Log(ex);
                    MessageBox.Show("Erro ao salvar arquivo: " + ex.Message);
                }
            }
        }
    }
}
