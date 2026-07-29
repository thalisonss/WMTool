using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;
using WMTool.Business;
using WMTool.Databases;
using WMTool.Utils;
using Application = System.Windows.Forms.Application;

namespace WMTool
{
    public partial class frmHomeScreen : Form
    {
        private string tempImagePath;

        public frmHomeScreen()
        {
            InitializeComponent();

            LoadInitial();

            ucJsonReprocessor1.ValidateJsonRequested += OnValidateJsonRequested;
        }

        // Botão "Validar JSON" da aba Reprocessar JSON: manda o JSON gerado (+ os 4 identificadores)
        // direto para a aba de Validação JSON x Banco, troca de aba e já executa a validação com as
        // regras carregadas.
        private async void OnValidateJsonRequested(string json, string cIDInvoice, string cSerie, string cIDBranchInvoice, string cIDCompany)
        {
            tabControl1.SelectedTab = tabPageValidation;
            ucValidation1.LoadJson(json, cIDInvoice, cSerie, cIDBranchInvoice, cIDCompany);
            await ucValidation1.RunValidationAsync();
        }

        #region |Variables|

        
        string directoryPathCSV = Properties.Settings.Default.configSaveCSVFolder;
        string directoryPathTripExceptionCSV = Properties.Settings.Default.configSaveTripExceptionCSVFolder;
        string directoryPathCECs = Properties.Settings.Default.configSaveCECFolder;
        bool preProd = Properties.Settings.Default.configPreProd;
        string sqlQuery = Properties.Settings.Default.configQuerySQL;
        string sqlQuerySearchCEC = Properties.Settings.Default.configQuerySearchCEC;
        string awsAccessKeyId = Properties.Settings.Default.configAccessKey;
        string awsSecretAccessKey = Properties.Settings.Default.configSecretKey;   
        string bucketName = Properties.Settings.Default.configBucketName;
        string database = Properties.Settings.Default.configDatabase;
        string server = Properties.Settings.Default.configServer;
        string connectionString = string.Empty;
        WMBusiness _business;
        int verifiedCount = 0;
        int totalCount = 0;
        int totalInvoiceWithoutCEC = 0;
        System.Data.DataTable dataTableCECs;
        private CancellationTokenSource _cancellationTokenSource;

        string urlToken = Properties.Settings.Default.configURLToken;
        string urlSync = Properties.Settings.Default.configURLSync;
        string domain = Properties.Settings.Default.configDomain;
        string enviroment = Properties.Settings.Default.configEnvironment;
        string login = Properties.Settings.Default.configLogin;
        string password = Properties.Settings.Default.configPassword;


        #endregion

        #region |Load Inicial|

        private async void frmHomeScreen_Load(object sender, EventArgs e)
        {
            await CheckForUpdates();
        }

        private void LoadInitial()
        {
            //Carrega as variaveis nos controles
            LoadConfig();

            dgvInvoicesWithoutCEC.AutoGenerateColumns = true;
        }

        private void LoadConfig()
        {
            // Carrega as configurações salvas nas variaveis nos controles
            lblDirectoryCSV.Text = directoryPathCSV;
            txtServerDB.Text = server;
            txtNameDB.Text = database;
            txtSQLQuery.Text = sqlQuery;
            txtQueryCECs.Text = sqlQuerySearchCEC;
            lblDirectoryTripExceptionCSV.Text = directoryPathTripExceptionCSV;
            lblDirectoryCECs.Text = directoryPathCECs;
            txtRequestDomain.Text = domain;
            txtRequestEnvironment.Text = enviroment;
            txtRequestURLSync.Text = urlSync;
            txtRequestURLToken.Text = urlToken;
            txtRequestUser.Text = login;
            txtRequestPassword.Text = password;
            txtValidationRulesPath.Text = Properties.Settings.Default.configValidationRulesPath;
            txtDbComparisonRulesPath.Text = Properties.Settings.Default.configDbComparisonRulesPath;
        }

      

        #endregion

        #region |Functions|
        #region |Bucket Functions (BO x CEC)|
        private const string S3FiscalDocPrefix = "FiscalDoc: ";
        private const string S3FiscalDocPath = "FiscalDoc/";

        //Função para conectar no bucket e carregar imagem na tela
        private async void LoadImageFromS3(string key)
        {
            try
            {
                var fileContent = await DownloadS3ObjectAsBytesAsync(key);
                SaveTempImage(fileContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar a imagem: {ex.Message}");
            }
        }

       

        #endregion

        #region |Bucket Functions (CEC exists in the bucket)|
        private async Task<bool> FileExistsInBucketAsync(string fileName)
        {
            try
            {
                using (var client = CreateS3Client())
                {
                    var request = new GetObjectMetadataRequest
                    {
                        BucketName = bucketName,
                        Key = fileName
                    };

                    await client.GetObjectMetadataAsync(request);
                }

                return true;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }

                throw;
            }
        }
        #endregion

        #region |Bucket Functions (Search CEC)|
        private void SetCheckStateForAllRows(bool state)
        {
            foreach (DataGridViewRow row in dgvCECs.Rows)
            {
                row.Cells["CheckCEC"].Value = state;
            }
        }
        private async Task<bool> DownloadFileFromS3Async(string key, string saveFilePath)
        {
            try
            {
                var fileContent = await DownloadS3ObjectAsBytesAsync(key);
                File.WriteAllBytes(saveFilePath, fileContent);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao baixar o arquivo {key}: {ex.Message}");
                return false;
            }
        }
        private async Task LoadImageFromS3Async(string key, PictureBox pictureBox)
        {
            try
            {
                var fileContent = await DownloadS3ObjectAsBytesAsync(key);

                using (var memoryStream = new MemoryStream(fileContent))
                {
                    pictureBox.Image = System.Drawing.Image.FromStream(memoryStream);
                }

                SaveTempImage(fileContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar a imagem: {ex.Message}");
            }
        }

        private AmazonS3Client CreateS3Client()
        {
            return new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.USEast1);
        }

        private string NormalizeS3Key(string key)
        {
            return key.Replace(S3FiscalDocPrefix, S3FiscalDocPath);
        }

        private async Task<byte[]> DownloadS3ObjectAsBytesAsync(string key)
        {
            string normalizedKey = NormalizeS3Key(key);

            using (var client = CreateS3Client())
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = normalizedKey
                };

                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await responseStream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        private void SaveTempImage(byte[] fileContent)
        {
            tempImagePath = Path.Combine(Path.GetTempPath(), "tempImage.png");
            File.WriteAllBytes(tempImagePath, fileContent);
        }

        private string GenerateFileNameFromRow(DataGridViewRow row)
        {
            string branch = row.Cells["CECcIDBranchInvoice"]?.Value?.ToString();
            string invoice = row.Cells["CECcIDInvoice"]?.Value?.ToString();
            string serie = row.Cells["CECcSerie"]?.Value?.ToString();

            if (string.IsNullOrWhiteSpace(branch) || string.IsNullOrWhiteSpace(invoice) || string.IsNullOrWhiteSpace(serie))
            {
                return null; // ou lançar uma exceção, conforme desejado
            }

            return $"{branch}-{invoice}-{serie}.png";
        }

        #endregion

        #endregion

        #region |Control events (BO X CEC)|
        #region|Control events (CEC exists in the bucket)|

        private void btnCancelCompare_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        private async void btn_businessCEC_Click(object sender, EventArgs e)
        {
           
        }

        private void FinallyBlock()
        {
            if (totalInvoiceWithoutCEC > 0)
            {
                btnExcel.Enabled = true;
                btnInsertDataCECTableTemporary.Enabled = true;
            }
            txtSQLQuery.Enabled = true;
            btnbusinessCEC.Enabled = true;
            btnCancelCompare.Enabled = false;
            btnCancelCompare.Visible = false;
            chkTripException.Enabled = true;
        }

        private void AppendRecordsToCsv(System.Data.DataTable records, DataGridView dgvInvoicesWithoutCEC)
        {
            // Caminho onde o CSV será salvo
            string filePathTripException = Path.Combine(directoryPathTripExceptionCSV, "WM_Trip_Exception.csv");
            string filePath = filePathTripException;
            var csv = new StringBuilder();

            HashSet<string> existingCsvRecords = new HashSet<string>();
            HashSet<string> excludedRecords = new HashSet<string>();

            // Carregar registros existentes no CSV
            if (File.Exists(filePath))
            {
                existingCsvRecords = LoadCsvRecords(filePath);
            }
            else
            {
                // Adicionar cabeçalho apenas se o arquivo não existe
                csv.AppendLine("cIDTrip;Data");
            }

            // Obter os cIDTrips do dgvInvoicesWithoutCEC
            foreach (DataGridViewRow row in dgvInvoicesWithoutCEC.Rows)
            {
                // Verificar se a célula não é nula e se possui um valor
                if (row.Cells["cIDTrip"] != null && row.Cells["cIDTrip"].Value != null)
                {
                    string cIDTrip = row.Cells["cIDTrip"].Value.ToString();
                    excludedRecords.Add(cIDTrip);
                }
            }

            // Percorrer o DataTable e obter os valores cIDTrip e a data atual
            foreach (DataRow row in records.Rows)
            {
                if (row["cIDTrip"] != null)
                {
                    string cIDTrip = row["cIDTrip"].ToString();

                    // Verificar se o cIDTrip já existe no conjunto ou se está no excludedRecords
                    if (!existingCsvRecords.Contains(cIDTrip) && !excludedRecords.Contains(cIDTrip))
                    {
                        string data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        csv.AppendLine($"{cIDTrip};{data}");
                        existingCsvRecords.Add(cIDTrip); // Adicionar ao conjunto para evitar duplicadas no futuro
                    }
                }
            }

            // Append o CSV ao arquivo
            File.AppendAllText(filePath, csv.ToString());
        }



        private HashSet<string> LoadCsvRecords(string filePath)
        {
            var existingRecords = new HashSet<string>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1)) // Pular o cabeçalho
            {
                var parts = line.Split(';');
                if (parts.Length > 0)
                {
                    existingRecords.Add(parts[0]);
                }
            }

            return existingRecords;
        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            // Formatar a data e hora conforme desejado (aaaaMMdd_HHmmss)
            string formattedDateTime = now.ToString("yyyyMMdd_HHmmss");

            // Inicializar datas mínima e máxima
            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;

            // Iterar pelas linhas para encontrar as datas mínima e máxima na coluna dEmission
            foreach (DataGridViewRow row in dgvInvoicesWithoutCEC.Rows)
            {
                if (!row.IsNewRow)
                {
                    DateTime emissionDate;
                    if (DateTime.TryParse(row.Cells["dEmission"].Value.ToString(), out emissionDate))
                    {
                        if (emissionDate < minDate) minDate = emissionDate;
                        if (emissionDate > maxDate) maxDate = emissionDate;
                    }
                }
            }

            // Verificar se minDate e maxDate foram atualizadas
            if (minDate == DateTime.MaxValue || maxDate == DateTime.MinValue)
            {
                MessageBox.Show("Erro: Nenhuma data válida encontrada na coluna dEmission.");
                return;
            }

            // Formatar as datas mínima e máxima
            string minDateString = minDate.ToString("dd-MM-yy");
            string maxDateString = maxDate.ToString("dd-MM-yy");

            // Concatenar a string inicial com as datas formatadas
            string fileName = $"WM_NF_WITHOUT_CEC_{minDateString}_a_{maxDateString}_{formattedDateTime}.csv";

            string destinationPath = Path.Combine(directoryPathCSV, fileName);

            try
            {
                if (!Directory.Exists(directoryPathCSV))
                {
                    Directory.CreateDirectory(directoryPathCSV);
                }

                StringBuilder sb = new StringBuilder();

                string[] colunas = new string[dgvInvoicesWithoutCEC.Columns.Count];
                for (int i = 0; i < dgvInvoicesWithoutCEC.Columns.Count; i++)
                {
                    colunas[i] = dgvInvoicesWithoutCEC.Columns[i].HeaderText;
                }
                sb.AppendLine(string.Join(";", colunas));

                foreach (DataGridViewRow row in dgvInvoicesWithoutCEC.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string[] cells = new string[dgvInvoicesWithoutCEC.Columns.Count];
                        for (int i = 0; i < dgvInvoicesWithoutCEC.Columns.Count; i++)
                        {
                            var cellValue = row.Cells[i].Value;
                            cells[i] = cellValue != null ? cellValue.ToString() : string.Empty;
                        }
                        sb.AppendLine(string.Join(";", cells));
                    }
                }

                File.WriteAllText(destinationPath, sb.ToString(), Encoding.UTF8);

                MessageBox.Show("Dados exportados com sucesso para " + destinationPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exportar dados: " + ex.Message);
            }
        }

        #endregion

        #region |Control events (Settings)|
        private void btnSetDirectoryCECs_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    directoryPathCECs = folderBrowserDialog.SelectedPath;

                    WMTool.Properties.Settings.Default.configSaveCECFolder = directoryPathCECs;
                    WMTool.Properties.Settings.Default.Save();

                    MessageBox.Show("Diretório salvo com sucesso!");

                    lblDirectoryCECs.Text = directoryPathCECs;
                }
            }
        }

        private void btnSaveSettingsDB_Click(object sender, EventArgs e)
        {
            WMTool.Properties.Settings.Default.configServer = txtServerDB.Text;
            WMTool.Properties.Settings.Default.configDatabase = txtNameDB.Text;

            server = txtServerDB.Text;
            database = txtNameDB.Text;

            WMTool.Properties.Settings.Default.Save();
        }

        private void btnBrowseValidationRulesPath_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "Arquivos JSON (*.json)|*.json" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtValidationRulesPath.Text = dialog.FileName;
                }
            }
        }

        private void btnSaveValidationRulesPath_Click(object sender, EventArgs e)
        {
            WMTool.Properties.Settings.Default.configValidationRulesPath = txtValidationRulesPath.Text;
            WMTool.Properties.Settings.Default.Save();

            MessageBox.Show("Caminho das regras de validação salvo com sucesso!");
        }

        private void btnBrowseDbComparisonRulesPath_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "Arquivos JSON (*.json)|*.json" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtDbComparisonRulesPath.Text = dialog.FileName;
                }
            }
        }

        private void btnSaveDbComparisonRulesPath_Click(object sender, EventArgs e)
        {
            WMTool.Properties.Settings.Default.configDbComparisonRulesPath = txtDbComparisonRulesPath.Text;
            WMTool.Properties.Settings.Default.Save();

            MessageBox.Show("Caminho das regras de comparação Banco x Banco salvo com sucesso!");
        }

        private void btnSetDirectoryTripExceptionCSV_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    directoryPathTripExceptionCSV = folderBrowserDialog.SelectedPath;

                    WMTool.Properties.Settings.Default.configSaveTripExceptionCSVFolder = directoryPathTripExceptionCSV;
                    WMTool.Properties.Settings.Default.Save();

                    MessageBox.Show("Diretório salvo com sucesso!");

                    lblDirectoryTripExceptionCSV.Text = directoryPathTripExceptionCSV;
                }
            }
        }

        private void btnSetDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    directoryPathCECs = folderBrowserDialog.SelectedPath;

                    WMTool.Properties.Settings.Default.configSaveImageFolder = directoryPathCECs;
                    WMTool.Properties.Settings.Default.Save();

                    MessageBox.Show("Diretório salvo com sucesso!");

                    //lblDirectory.Text = directoryPath;
                }
            }
        }

        private void btnSetDirectoryCSV_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    directoryPathCSV = folderBrowserDialog.SelectedPath;

                    WMTool.Properties.Settings.Default.configSaveCSVFolder = directoryPathCSV;
                    WMTool.Properties.Settings.Default.Save();

                    MessageBox.Show("Diretório salvo com sucesso!");

                    lblDirectoryCSV.Text = directoryPathCSV;
                }
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            WMTool.Properties.Settings.Default.configBucketName = txtBucketName.Text;
            WMTool.Properties.Settings.Default.configAccessKey = txtAccessKey.Text;
            WMTool.Properties.Settings.Default.configSecretKey = txtSecretAccessKey.Text;

            awsAccessKeyId = txtAccessKey.Text;
            awsSecretAccessKey = txtSecretAccessKey.Text;
            bucketName = txtBucketName.Text;

            WMTool.Properties.Settings.Default.Save();
        }

        private void chkPreProd_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region |Control events (Request)|
        private async void btnRequest_Click(object sender, EventArgs e)
        {
            lblRequestLastUpdate.Text = "Última execução: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            WMTool.Properties.Settings.Default.configURLSync = txtRequestURLSync.Text;
            WMTool.Properties.Settings.Default.configURLToken = txtRequestURLToken.Text;
            WMTool.Properties.Settings.Default.configDomain = txtRequestDomain.Text;
            WMTool.Properties.Settings.Default.configEnvironment = txtRequestEnvironment.Text;
            WMTool.Properties.Settings.Default.configLogin = txtRequestUser.Text;
            WMTool.Properties.Settings.Default.configPassword = txtRequestPassword.Text;
            WMTool.Properties.Settings.Default.Save();

            string urlToken = txtRequestURLToken.Text;
            string urlSync = txtRequestURLSync.Text;
            string domain = txtRequestDomain.Text;
            string requestLogin = txtRequestUser.Text;
            string password = txtRequestPassword.Text;

            btnRequest.Enabled = false;
            AppendRequestLog($"Iniciando requisição para o usuário '{requestLogin}' (domínio '{domain}', ambiente '{txtRequestEnvironment.Text}').");

            try
            {
                var environment = int.Parse(txtRequestEnvironment.Text);

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);

                    var tokenPayload = new
                    {
                        cProjectGUI = domain,
                        nEnvironment = environment,
                        cLogin = requestLogin,
                        cPassword = password
                    };

                    var tokenJson = JsonConvert.SerializeObject(tokenPayload);
                    var tokenContent = new StringContent(tokenJson, Encoding.UTF8, "application/json");

                    AppendRequestLog("Solicitando token em " + urlToken + "...");
                    var tokenResponse = await httpClient.PostAsync(urlToken, tokenContent);
                    tokenResponse.EnsureSuccessStatusCode();

                    var tokenResponseString = await tokenResponse.Content.ReadAsStringAsync();

                    dynamic tokenObj = JsonConvert.DeserializeObject(tokenResponseString);
                    string token = tokenObj.token;

                    if (string.IsNullOrEmpty(token))
                    {
                        lblRequestStatus.Text = "Token não retornado.";
                        AppendRequestLog("Falha: o endpoint de token não retornou um token.");
                        return;
                    }

                    AppendRequestLog("Token obtido com sucesso.");

                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    var syncPayload = tokenPayload;
                    var syncJson = JsonConvert.SerializeObject(syncPayload);
                    var syncContent = new StringContent(syncJson, Encoding.UTF8, "application/json");

                    AppendRequestLog("Enviando sync em " + urlSync + "...");
                    var syncResponse = await httpClient.PostAsync(urlSync, syncContent);
                    syncResponse.EnsureSuccessStatusCode();

                    lblRequestStatus.Text = "Refresh para o usuario " + requestLogin + " realizado com sucesso. - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    AppendRequestLog("Sucesso: refresh concluído para o usuário '" + requestLogin + "'.");
                }
            }
            catch (Exception ex)
            {
                lblRequestStatus.Text = "Erro na requisição do usuario " + requestLogin;
                AppendRequestLog("Erro: " + ex.Message);

                MessageBox.Show("Erro na requisição: " + ex.Message);
            }
            finally
            {
                btnRequest.Enabled = true;
            }
        }

        private void btnClearRequestLog_Click(object sender, EventArgs e)
        {
            txtRequestLog.Clear();
        }

        private void AppendRequestLog(string message)
        {
            txtRequestLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + message + Environment.NewLine);
        }
        #endregion

        #region |Control events (Search CEC)|
        private async void dgvCECs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var row = dgvCECs.Rows[e.RowIndex];

                var key = row.Cells["cPathCECCC"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(key)) return;

                var fileName = GenerateFileNameFromRow(row);
                if (fileName == null) return;

                string savePath = Path.Combine(directoryPathCECs, fileName);

                bool success = await DownloadFileFromS3Async(key, savePath);

                if (success)
                    MessageBox.Show($"Arquivo salvo em: {savePath}");
            }
        }
        private void btnSelecionarTodasCECs_Click(object sender, EventArgs e) => SetCheckStateForAllRows(true);

        private void btnDesmarcarTodasCECs_Click(object sender, EventArgs e) => SetCheckStateForAllRows(false);

        private async void btnPesquisarCECs_Click(object sender, EventArgs e)
        {
            try
            {
                WMTool.Properties.Settings.Default.configQuerySearchCEC = txtQueryCECs.Text;
                WMTool.Properties.Settings.Default.Save();

                connectionString = $"Server={server};Database={database};Integrated Security=true;";

                Business.WMBusiness business = new Business.WMBusiness();

                dataTableCECs = await business.ConsultDB(txtQueryCECs.Text, connectionString);

                dgvCECs.DataSource = dataTableCECs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar CECs: " + ex.Message);
            }
        }


        private async void btnDownloadCECs_Click(object sender, EventArgs e)
        {
            string savePath = string.Empty;

            foreach (DataGridViewRow row in dgvCECs.Rows)
            {
                bool isChecked = row.Cells["CheckCEC"].Value != null && Convert.ToBoolean(row.Cells["CheckCEC"].Value);
                if (!isChecked) continue;

                var key = row.Cells["cPathCECCC"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(key)) continue;

                var fileName = GenerateFileNameFromRow(row);
                if (fileName == null) continue;

                savePath = Path.Combine(directoryPathCECs, fileName);

                await DownloadFileFromS3Async(key, savePath);
            }

            MessageBox.Show($"Arquivo salvo em: " + savePath);
        }

        private async void dgvCECs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCECs.SelectedRows.Count > 0)
            {
                var selectedRow = dgvCECs.SelectedRows[0];
                var key = selectedRow.Cells["cPathCECCC"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(key))
                    await LoadImageFromS3Async(key, imgCECs);
            }
        }
        #endregion

        #endregion

        private void btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tempImagePath) && File.Exists(tempImagePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = tempImagePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("A imagem não está disponível.");
            }
        }

        private void btnCECOpenFolder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(directoryPathCECs) && Directory.Exists(directoryPathCECs))
            {
                Process.Start("explorer.exe", directoryPathCECs);
            }
            else
            {
                MessageBox.Show("O diretório não está configurado ou não existe.");
            }
        }

        private void btnCECSaveImage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tempImagePath) && File.Exists(tempImagePath))
            {
                string fileName = "image.png";
                string destinationPath = Path.Combine(directoryPathCECs, fileName);

                try
                {
                    if (!Directory.Exists(directoryPathCECs))
                    {
                        Directory.CreateDirectory(directoryPathCECs);
                    }

                    File.Copy(tempImagePath, destinationPath, true);
                    MessageBox.Show($"Arquivo salvo em: {destinationPath}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar a imagem: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("A imagem não está disponível.");
            }
        }
        private async Task CheckForUpdates()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "WMTool");

                    string url = "https://api.github.com/repos/thalisonss/WMTool/releases/latest";

                    var response = await client.GetStringAsync(url);

                    var release = JsonConvert.DeserializeObject<GitHubRelease>(response);

                    string latestVersionTag = release.tag_name.Replace("v", "");

                    Version currentVersion = new Version(Application.ProductVersion);
                    Version latestVersion = new Version(latestVersionTag);

                    if (latestVersion > currentVersion)
                    {
                        var result = MessageBox.Show(
                            $"Nova versão disponível: {latestVersion}\n\n{release.body}\n\nDeseja atualizar?",
                            "Atualização disponível",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            string downloadUrl = release.assets?
                                .FirstOrDefault(a => !string.IsNullOrWhiteSpace(a.browser_download_url))?
                                .browser_download_url;

                            if (string.IsNullOrWhiteSpace(downloadUrl))
                            {
                                downloadUrl = release.html_url;
                            }

                            if (!string.IsNullOrWhiteSpace(downloadUrl))
                            {
                                Process.Start(downloadUrl);
                                Application.Exit();
                            }
                            else
                            {
                                MessageBox.Show("Não foi possível encontrar o link de download da atualização.", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch
            {
                
            }
        }

        public class GitHubRelease
        {
            public string tag_name { get; set; }
            public string body { get; set; }
            public string html_url { get; set; }
            public List<GitHubAsset> assets { get; set; }
        }

        public class GitHubAsset
        {
            public string browser_download_url { get; set; }
        }

        private async void btnbusinessCEC_Click(object sender, EventArgs e)
        {
            try
            {
                _business = new WMBusiness();
                _cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = _cancellationTokenSource.Token;

                // Salvar a query nas configurações da aplicação
                WMTool.Properties.Settings.Default.configQuerySQL = txtSQLQuery.Text;
                WMTool.Properties.Settings.Default.Save();

                // Limpar e resetar a dgvInvoicesWithoutCEC e variáveis de contagem
                dgvInvoicesWithoutCEC.Rows.Clear();
                totalCount = 0;
                verifiedCount = 0;
                totalInvoiceWithoutCEC = 0;

                // Desabilitar os controles para impedir bugs no momento da execução
                btnbusinessCEC.Enabled = false;
                btnExcel.Enabled = false;
                txtSQLQuery.Enabled = false;
                btnInsertDataCECTableTemporary.Enabled = false;
                chkTripException.Enabled = false;
                btnCancelCompare.Enabled = true;
                btnCancelCompare.Visible = true;

                // Retorna os dados consultados
                System.Data.DataTable records = await _business.ConsultDB(txtSQLQuery.Text, connectionString);

                // Carregar registros existentes no CSV, se o checkbox estiver marcado
                HashSet<string> existingCsvRecords = new HashSet<string>();
                string filePathTripException = Path.Combine(directoryPathTripExceptionCSV, "WM_Trip_Exception.csv");
                if (chkTripException.Checked && File.Exists(filePathTripException))
                {
                    existingCsvRecords = LoadCsvRecords(filePathTripException);
                }

                // Filtrar registros se chkTripException estiver marcado
                if (chkTripException.Checked)
                {
                    var filteredRecords = from DataRow row in records.Rows
                                          where !existingCsvRecords.Contains(row["cIDTrip"]?.ToString())
                                          select row;

                    // Atualizar DataTable após filtragem
                    try
                    {
                        records = filteredRecords.Any() ? filteredRecords.CopyToDataTable() : new System.Data.DataTable();
                    }
                    catch (InvalidOperationException)
                    {
                        throw new InvalidOperationException("A Origem não contem DataRows");
                    }
                }

                // Atualizar totalCount com a quantidade de registros após a filtragem
                totalCount = records.Rows.Count;
                lblTotalVerified.Text = $"{verifiedCount} / {totalCount}";

                // Validação caso não retornar nenhum registro habilita os botões novamente e mostra a mensagem.
                if (totalCount == 0)
                {
                    throw new InvalidOperationException("Nenhum registro encontrado.");
                }

                // Inserir o valor máximo da barra de progresso com a quantidade retornada dos registros e colocar seu valor inicial igual a 0 
                progressBarCEC.Maximum = totalCount;
                progressBarCEC.Value = 0;

                try
                {
                    List<DataRow> recordsWithPathCec = new List<DataRow>();

                    // Primeiro, adicione todas as linhas com cPathCec ou dExportCECDanf nulo ou em branco
                    foreach (DataRow record in records.Rows)
                    {
                        // Verifica se o botão de cancelar foi acionado, se sim ele cai no catch e para a verificação
                        cancellationToken.ThrowIfCancellationRequested();

                        string cPathCec = record["cPathCec"]?.ToString();
                        string dExportCECDanf = record["dExportCECDanf"]?.ToString();

                        if (string.IsNullOrEmpty(cPathCec) || string.IsNullOrEmpty(dExportCECDanf))
                        {
                            dgvInvoicesWithoutCEC.Rows.Add(
                                record["cIDCompany"],
                                record["cIDInvoice"],
                                record["cSerie"],
                                record["cIDBranchInvoice"],
                                record["cPathCec"],
                                record["dEmission"],
                                record["cIDCustomer"],
                                record["cIDTrip"]
                            );

                            totalInvoiceWithoutCEC++;
                            verifiedCount++;
                        }
                        else
                        {
                            recordsWithPathCec.Add(record);
                        }

                        progressBarCEC.Value = verifiedCount;
                        lblTotalVerified.Text = $"{verifiedCount} / {totalCount}";
                        lblTotalInvoiceWithoutCEC.Text = $"{totalInvoiceWithoutCEC}";
                    }

                    // Agora, verifique os registros com cPathCec e dExportCECDanf preenchido
                    foreach (DataRow record in recordsWithPathCec)
                    {
                        // Verifica se o botão de cancelar foi acionado, se sim ele cai no catch e para a verificação
                        cancellationToken.ThrowIfCancellationRequested();

                        string cIDTrip = record["cIDTrip"]?.ToString();

                        if (chkTripException.Checked && existingCsvRecords.Contains(cIDTrip))
                        {
                            // Pula a verificação, pois já existe no CSV e o chkTripException está marcado
                            continue;
                        }

                        string cPathCec = record["cPathCec"]?.ToString();
                        string fileName = "FiscalDoc/" + cPathCec.Replace("FiscalDoc: ", "").ToLower();

                        // Se não existir no bucket, insere a linha no DataGridView
                        bool exists = await FileExistsInBucketAsync(fileName);
                        if (!exists)
                        {
                            dgvInvoicesWithoutCEC.Rows.Add(
                                record["cIDCompany"],
                                record["cIDInvoice"],
                                record["cSerie"],
                                record["cIDBranchInvoice"],
                                record["cPathCec"],
                                record["dEmission"],
                                record["cIDCustomer"],
                                record["cIDTrip"]
                            );

                            totalInvoiceWithoutCEC++;
                        }

                        verifiedCount++;
                        progressBarCEC.Value = verifiedCount;
                        lblTotalVerified.Text = $"{verifiedCount} / {totalCount}";
                        lblTotalInvoiceWithoutCEC.Text = $"{totalInvoiceWithoutCEC}";
                    }
                }
                catch (OperationCanceledException)
                {
                    // Mensagem que aparece caso o botão de cancelar for clicado
                    MessageBox.Show("Operação cancelada pelo usuário.", "WMTool", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    if (totalInvoiceWithoutCEC > 0)
                    {
                        btnExcel.Enabled = true;
                        btnInsertDataCECTableTemporary.Enabled = true;
                    }
                    txtSQLQuery.Enabled = true;
                    btnbusinessCEC.Enabled = true;
                    btnCancelCompare.Enabled = false;
                    btnCancelCompare.Visible = false;
                    chkTripException.Enabled = true;
                }

                // Pergunta ao usuário se deseja salvar os dados em um arquivo CSV
                DialogResult result = MessageBox.Show("Deseja salvar as viagens verificadas na planilha de exceção?", "WMTool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    AppendRecordsToCsv(records, dgvInvoicesWithoutCEC);
                }

                MessageBox.Show("Comparação concluída.");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Comparação concluída.");
                FinallyBlock();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                FinallyBlock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FinallyBlock();
            }
        }
    }
}
