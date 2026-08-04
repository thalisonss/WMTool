namespace WMTool
{
    partial class frmHomeScreen
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxValidationRules = new System.Windows.Forms.GroupBox();
            this.lblValidationRulesPath = new System.Windows.Forms.Label();
            this.txtValidationRulesPath = new System.Windows.Forms.TextBox();
            this.btnBrowseValidationRulesPath = new System.Windows.Forms.Button();
            this.btnSaveValidationRulesPath = new System.Windows.Forms.Button();
            this.groupBoxDbComparisonRules = new System.Windows.Forms.GroupBox();
            this.lblDbComparisonRulesPath = new System.Windows.Forms.Label();
            this.txtDbComparisonRulesPath = new System.Windows.Forms.TextBox();
            this.btnBrowseDbComparisonRulesPath = new System.Windows.Forms.Button();
            this.btnSaveDbComparisonRulesPath = new System.Windows.Forms.Button();
            this.groupBoxInsertScriptRules = new System.Windows.Forms.GroupBox();
            this.lblInsertScriptRulesPath = new System.Windows.Forms.Label();
            this.txtInsertScriptRulesPath = new System.Windows.Forms.TextBox();
            this.btnBrowseInsertScriptRulesPath = new System.Windows.Forms.Button();
            this.btnSaveInsertScriptRulesPath = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSaveSettingsDB = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServerDB = new System.Windows.Forms.TextBox();
            this.txtNameDB = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSetDirectoryCECs = new System.Windows.Forms.Button();
            this.lblDirectoryCECs = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetDirectoryCSV = new System.Windows.Forms.Button();
            this.lblDirectoryCSV = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSetDirectoryTripExceptionCSV = new System.Windows.Forms.Button();
            this.lblDirectoryTripExceptionCSV = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBucketName = new System.Windows.Forms.TextBox();
            this.txtSecretAccessKey = new System.Windows.Forms.TextBox();
            this.txtAccessKey = new System.Windows.Forms.TextBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkTripException = new System.Windows.Forms.CheckBox();
            this.btnInsertDataCECTableTemporary = new System.Windows.Forms.Button();
            this.btnCancelCompare = new System.Windows.Forms.Button();
            this.txtSQLQuery = new System.Windows.Forms.TextBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.lblTotalInvoiceWithoutCEC = new System.Windows.Forms.Label();
            this.lblTotalVerified = new System.Windows.Forms.Label();
            this.progressBarCEC = new System.Windows.Forms.ProgressBar();
            this.dgvInvoicesWithoutCEC = new System.Windows.Forms.DataGridView();
            this.cIDCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIDInvoicee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSerie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIDBranchInvoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPathCEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEmission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIDCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIDTrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dExportCECDanf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnbusinessCEC = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnCECSaveImage = new System.Windows.Forms.Button();
            this.btnCECOpenFolder = new System.Windows.Forms.Button();
            this.btnCECOpenImage = new System.Windows.Forms.Button();
            this.imgCECs = new System.Windows.Forms.PictureBox();
            this.btnDesmarcarTodasCECs = new System.Windows.Forms.Button();
            this.btnSelecionarTodasCECs = new System.Windows.Forms.Button();
            this.btnDownloadCECs = new System.Windows.Forms.Button();
            this.btnPesquisarCECs = new System.Windows.Forms.Button();
            this.dgvCECs = new System.Windows.Forms.DataGridView();
            this.CheckCEC = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.downloadCEC = new System.Windows.Forms.DataGridViewImageColumn();
            this.CECcIDCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CECcIDInvoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CECcSerie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CECcIDBranchInvoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPathCECCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Joker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtQueryCECs = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageRequest = new System.Windows.Forms.TabPage();
            this.groupBoxRequestConfig = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRequestUser = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRequestPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRequestDomain = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRequestEnvironment = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRequestURLToken = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRequestURLSync = new System.Windows.Forms.TextBox();
            this.btnRequest = new System.Windows.Forms.Button();
            this.lblRequestLastUpdate = new System.Windows.Forms.Label();
            this.lblRequestStatus = new System.Windows.Forms.Label();
            this.lblRequestLogTitle = new System.Windows.Forms.Label();
            this.btnClearRequestLog = new System.Windows.Forms.Button();
            this.txtRequestLog = new System.Windows.Forms.TextBox();
            this.tabPageJsonReprocessor = new System.Windows.Forms.TabPage();
            this.ucJsonReprocessor1 = new WMTool.Screens.ucJsonReprocessor();
            this.tabPageInsertScript = new System.Windows.Forms.TabPage();
            this.ucInsertScriptGenerator1 = new WMTool.Screens.ucInsertScriptGenerator();
            this.tabPageValidation = new System.Windows.Forms.TabPage();
            this.ucValidation1 = new WMTool.Screens.ucValidation();
            this.tabPageDatabaseComparison = new System.Windows.Forms.TabPage();
            this.ucDatabaseComparison1 = new WMTool.Screens.ucDatabaseComparison();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tabPage2.SuspendLayout();
            this.groupBoxValidationRules.SuspendLayout();
            this.groupBoxDbComparisonRules.SuspendLayout();
            this.groupBoxInsertScriptRules.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoicesWithoutCEC)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCECs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCECs)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageRequest.SuspendLayout();
            this.groupBoxRequestConfig.SuspendLayout();
            this.tabPageJsonReprocessor.SuspendLayout();
            this.tabPageInsertScript.SuspendLayout();
            this.tabPageValidation.SuspendLayout();
            this.tabPageDatabaseComparison.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1233, 754);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 15);
            this.label10.TabIndex = 1;
            this.label10.Text = "github/thalisonss";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBoxValidationRules);
            this.tabPage2.Controls.Add(this.groupBoxDbComparisonRules);
            this.tabPage2.Controls.Add(this.groupBoxInsertScriptRules);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1341, 718);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBoxValidationRules
            // 
            this.groupBoxValidationRules.Controls.Add(this.lblValidationRulesPath);
            this.groupBoxValidationRules.Controls.Add(this.txtValidationRulesPath);
            this.groupBoxValidationRules.Controls.Add(this.btnBrowseValidationRulesPath);
            this.groupBoxValidationRules.Controls.Add(this.btnSaveValidationRulesPath);
            this.groupBoxValidationRules.Location = new System.Drawing.Point(947, 16);
            this.groupBoxValidationRules.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxValidationRules.Name = "groupBoxValidationRules";
            this.groupBoxValidationRules.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxValidationRules.Size = new System.Drawing.Size(389, 130);
            this.groupBoxValidationRules.TabIndex = 36;
            this.groupBoxValidationRules.TabStop = false;
            this.groupBoxValidationRules.Text = "Validação JSON x Banco — Regras";
            // 
            // lblValidationRulesPath
            // 
            this.lblValidationRulesPath.AutoSize = true;
            this.lblValidationRulesPath.Location = new System.Drawing.Point(9, 25);
            this.lblValidationRulesPath.Name = "lblValidationRulesPath";
            this.lblValidationRulesPath.Size = new System.Drawing.Size(277, 16);
            this.lblValidationRulesPath.TabIndex = 0;
            this.lblValidationRulesPath.Text = "Caminho do arquivo de regras padrão (.json):";
            // 
            // txtValidationRulesPath
            // 
            this.txtValidationRulesPath.Location = new System.Drawing.Point(11, 50);
            this.txtValidationRulesPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtValidationRulesPath.Name = "txtValidationRulesPath";
            this.txtValidationRulesPath.Size = new System.Drawing.Size(300, 22);
            this.txtValidationRulesPath.TabIndex = 1;
            // 
            // btnBrowseValidationRulesPath
            // 
            this.btnBrowseValidationRulesPath.Location = new System.Drawing.Point(317, 49);
            this.btnBrowseValidationRulesPath.Name = "btnBrowseValidationRulesPath";
            this.btnBrowseValidationRulesPath.Size = new System.Drawing.Size(60, 23);
            this.btnBrowseValidationRulesPath.TabIndex = 2;
            this.btnBrowseValidationRulesPath.Text = "...";
            this.btnBrowseValidationRulesPath.UseVisualStyleBackColor = true;
            this.btnBrowseValidationRulesPath.Click += new System.EventHandler(this.btnBrowseValidationRulesPath_Click);
            // 
            // btnSaveValidationRulesPath
            // 
            this.btnSaveValidationRulesPath.Location = new System.Drawing.Point(13, 90);
            this.btnSaveValidationRulesPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveValidationRulesPath.Name = "btnSaveValidationRulesPath";
            this.btnSaveValidationRulesPath.Size = new System.Drawing.Size(113, 23);
            this.btnSaveValidationRulesPath.TabIndex = 3;
            this.btnSaveValidationRulesPath.Text = "Salvar";
            this.btnSaveValidationRulesPath.UseVisualStyleBackColor = true;
            this.btnSaveValidationRulesPath.Click += new System.EventHandler(this.btnSaveValidationRulesPath_Click);
            // 
            // groupBoxDbComparisonRules
            // 
            this.groupBoxDbComparisonRules.Controls.Add(this.lblDbComparisonRulesPath);
            this.groupBoxDbComparisonRules.Controls.Add(this.txtDbComparisonRulesPath);
            this.groupBoxDbComparisonRules.Controls.Add(this.btnBrowseDbComparisonRulesPath);
            this.groupBoxDbComparisonRules.Controls.Add(this.btnSaveDbComparisonRulesPath);
            this.groupBoxDbComparisonRules.Location = new System.Drawing.Point(947, 154);
            this.groupBoxDbComparisonRules.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxDbComparisonRules.Name = "groupBoxDbComparisonRules";
            this.groupBoxDbComparisonRules.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxDbComparisonRules.Size = new System.Drawing.Size(389, 130);
            this.groupBoxDbComparisonRules.TabIndex = 37;
            this.groupBoxDbComparisonRules.TabStop = false;
            this.groupBoxDbComparisonRules.Text = "Comparação Banco x Banco — Regras";
            // 
            // lblDbComparisonRulesPath
            // 
            this.lblDbComparisonRulesPath.AutoSize = true;
            this.lblDbComparisonRulesPath.Location = new System.Drawing.Point(9, 25);
            this.lblDbComparisonRulesPath.Name = "lblDbComparisonRulesPath";
            this.lblDbComparisonRulesPath.Size = new System.Drawing.Size(277, 16);
            this.lblDbComparisonRulesPath.TabIndex = 0;
            this.lblDbComparisonRulesPath.Text = "Caminho do arquivo de regras padrão (.json):";
            // 
            // txtDbComparisonRulesPath
            // 
            this.txtDbComparisonRulesPath.Location = new System.Drawing.Point(11, 50);
            this.txtDbComparisonRulesPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtDbComparisonRulesPath.Name = "txtDbComparisonRulesPath";
            this.txtDbComparisonRulesPath.Size = new System.Drawing.Size(300, 22);
            this.txtDbComparisonRulesPath.TabIndex = 1;
            // 
            // btnBrowseDbComparisonRulesPath
            // 
            this.btnBrowseDbComparisonRulesPath.Location = new System.Drawing.Point(317, 49);
            this.btnBrowseDbComparisonRulesPath.Name = "btnBrowseDbComparisonRulesPath";
            this.btnBrowseDbComparisonRulesPath.Size = new System.Drawing.Size(60, 23);
            this.btnBrowseDbComparisonRulesPath.TabIndex = 2;
            this.btnBrowseDbComparisonRulesPath.Text = "...";
            this.btnBrowseDbComparisonRulesPath.UseVisualStyleBackColor = true;
            this.btnBrowseDbComparisonRulesPath.Click += new System.EventHandler(this.btnBrowseDbComparisonRulesPath_Click);
            // 
            // btnSaveDbComparisonRulesPath
            // 
            this.btnSaveDbComparisonRulesPath.Location = new System.Drawing.Point(13, 90);
            this.btnSaveDbComparisonRulesPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveDbComparisonRulesPath.Name = "btnSaveDbComparisonRulesPath";
            this.btnSaveDbComparisonRulesPath.Size = new System.Drawing.Size(113, 23);
            this.btnSaveDbComparisonRulesPath.TabIndex = 3;
            this.btnSaveDbComparisonRulesPath.Text = "Salvar";
            this.btnSaveDbComparisonRulesPath.UseVisualStyleBackColor = true;
            this.btnSaveDbComparisonRulesPath.Click += new System.EventHandler(this.btnSaveDbComparisonRulesPath_Click);
            // 
            // groupBoxInsertScriptRules
            // 
            this.groupBoxInsertScriptRules.Controls.Add(this.lblInsertScriptRulesPath);
            this.groupBoxInsertScriptRules.Controls.Add(this.txtInsertScriptRulesPath);
            this.groupBoxInsertScriptRules.Controls.Add(this.btnBrowseInsertScriptRulesPath);
            this.groupBoxInsertScriptRules.Controls.Add(this.btnSaveInsertScriptRulesPath);
            this.groupBoxInsertScriptRules.Location = new System.Drawing.Point(947, 292);
            this.groupBoxInsertScriptRules.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxInsertScriptRules.Name = "groupBoxInsertScriptRules";
            this.groupBoxInsertScriptRules.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxInsertScriptRules.Size = new System.Drawing.Size(389, 130);
            this.groupBoxInsertScriptRules.TabIndex = 38;
            this.groupBoxInsertScriptRules.TabStop = false;
            this.groupBoxInsertScriptRules.Text = "Gerador de Script INSERT — Regras";
            // 
            // lblInsertScriptRulesPath
            // 
            this.lblInsertScriptRulesPath.AutoSize = true;
            this.lblInsertScriptRulesPath.Location = new System.Drawing.Point(9, 25);
            this.lblInsertScriptRulesPath.Name = "lblInsertScriptRulesPath";
            this.lblInsertScriptRulesPath.Size = new System.Drawing.Size(277, 16);
            this.lblInsertScriptRulesPath.TabIndex = 0;
            this.lblInsertScriptRulesPath.Text = "Caminho do arquivo de regras padrão (.json):";
            // 
            // txtInsertScriptRulesPath
            // 
            this.txtInsertScriptRulesPath.Location = new System.Drawing.Point(11, 50);
            this.txtInsertScriptRulesPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtInsertScriptRulesPath.Name = "txtInsertScriptRulesPath";
            this.txtInsertScriptRulesPath.Size = new System.Drawing.Size(300, 22);
            this.txtInsertScriptRulesPath.TabIndex = 1;
            // 
            // btnBrowseInsertScriptRulesPath
            // 
            this.btnBrowseInsertScriptRulesPath.Location = new System.Drawing.Point(317, 49);
            this.btnBrowseInsertScriptRulesPath.Name = "btnBrowseInsertScriptRulesPath";
            this.btnBrowseInsertScriptRulesPath.Size = new System.Drawing.Size(60, 23);
            this.btnBrowseInsertScriptRulesPath.TabIndex = 2;
            this.btnBrowseInsertScriptRulesPath.Text = "...";
            this.btnBrowseInsertScriptRulesPath.UseVisualStyleBackColor = true;
            this.btnBrowseInsertScriptRulesPath.Click += new System.EventHandler(this.btnBrowseInsertScriptRulesPath_Click);
            // 
            // btnSaveInsertScriptRulesPath
            // 
            this.btnSaveInsertScriptRulesPath.Location = new System.Drawing.Point(13, 90);
            this.btnSaveInsertScriptRulesPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveInsertScriptRulesPath.Name = "btnSaveInsertScriptRulesPath";
            this.btnSaveInsertScriptRulesPath.Size = new System.Drawing.Size(113, 23);
            this.btnSaveInsertScriptRulesPath.TabIndex = 3;
            this.btnSaveInsertScriptRulesPath.Text = "Salvar";
            this.btnSaveInsertScriptRulesPath.UseVisualStyleBackColor = true;
            this.btnSaveInsertScriptRulesPath.Click += new System.EventHandler(this.btnSaveInsertScriptRulesPath_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSaveSettingsDB);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtServerDB);
            this.groupBox4.Controls.Add(this.txtNameDB);
            this.groupBox4.Location = new System.Drawing.Point(16, 16);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(463, 224);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Banco de dados";
            // 
            // btnSaveSettingsDB
            // 
            this.btnSaveSettingsDB.Location = new System.Drawing.Point(13, 190);
            this.btnSaveSettingsDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveSettingsDB.Name = "btnSaveSettingsDB";
            this.btnSaveSettingsDB.Size = new System.Drawing.Size(113, 23);
            this.btnSaveSettingsDB.TabIndex = 34;
            this.btnSaveSettingsDB.Text = "Salvar";
            this.btnSaveSettingsDB.UseVisualStyleBackColor = true;
            this.btnSaveSettingsDB.Click += new System.EventHandler(this.btnSaveSettingsDB_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 16);
            this.label5.TabIndex = 37;
            this.label5.Text = "Nome do banco de dados:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 34;
            this.label6.Text = "Nome do Servidor:";
            // 
            // txtServerDB
            // 
            this.txtServerDB.Location = new System.Drawing.Point(11, 50);
            this.txtServerDB.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerDB.Name = "txtServerDB";
            this.txtServerDB.Size = new System.Drawing.Size(427, 22);
            this.txtServerDB.TabIndex = 35;
            // 
            // txtNameDB
            // 
            this.txtNameDB.Location = new System.Drawing.Point(11, 110);
            this.txtNameDB.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameDB.Name = "txtNameDB";
            this.txtNameDB.Size = new System.Drawing.Size(427, 22);
            this.txtNameDB.TabIndex = 36;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.btnSetDirectoryCECs);
            this.groupBox3.Controls.Add(this.lblDirectoryCECs);
            this.groupBox3.Location = new System.Drawing.Point(487, 319);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(452, 250);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search CEC";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 166);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 16);
            this.label16.TabIndex = 28;
            this.label16.Text = "Salvar CEC\'s em:";
            // 
            // btnSetDirectoryCECs
            // 
            this.btnSetDirectoryCECs.Location = new System.Drawing.Point(15, 210);
            this.btnSetDirectoryCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetDirectoryCECs.Name = "btnSetDirectoryCECs";
            this.btnSetDirectoryCECs.Size = new System.Drawing.Size(64, 23);
            this.btnSetDirectoryCECs.TabIndex = 26;
            this.btnSetDirectoryCECs.Text = "Alterar";
            this.btnSetDirectoryCECs.UseVisualStyleBackColor = true;
            this.btnSetDirectoryCECs.Click += new System.EventHandler(this.btnSetDirectoryCECs_Click);
            // 
            // lblDirectoryCECs
            // 
            this.lblDirectoryCECs.Location = new System.Drawing.Point(17, 185);
            this.lblDirectoryCECs.Name = "lblDirectoryCECs";
            this.lblDirectoryCECs.Size = new System.Drawing.Size(405, 23);
            this.lblDirectoryCECs.TabIndex = 27;
            this.lblDirectoryCECs.Text = "-";
            this.lblDirectoryCECs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnSetDirectoryCSV);
            this.groupBox2.Controls.Add(this.lblDirectoryCSV);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnSetDirectoryTripExceptionCSV);
            this.groupBox2.Controls.Add(this.lblDirectoryTripExceptionCSV);
            this.groupBox2.Location = new System.Drawing.Point(487, 16);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(452, 295);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CEC not exists in the bucket";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Salvar planilha em:";
            // 
            // btnSetDirectoryCSV
            // 
            this.btnSetDirectoryCSV.Location = new System.Drawing.Point(9, 256);
            this.btnSetDirectoryCSV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetDirectoryCSV.Name = "btnSetDirectoryCSV";
            this.btnSetDirectoryCSV.Size = new System.Drawing.Size(64, 23);
            this.btnSetDirectoryCSV.TabIndex = 20;
            this.btnSetDirectoryCSV.Text = "Alterar";
            this.btnSetDirectoryCSV.UseVisualStyleBackColor = true;
            this.btnSetDirectoryCSV.Click += new System.EventHandler(this.btnSetDirectoryCSV_Click);
            // 
            // lblDirectoryCSV
            // 
            this.lblDirectoryCSV.Location = new System.Drawing.Point(12, 230);
            this.lblDirectoryCSV.Name = "lblDirectoryCSV";
            this.lblDirectoryCSV.Size = new System.Drawing.Size(411, 23);
            this.lblDirectoryCSV.TabIndex = 21;
            this.lblDirectoryCSV.Text = "-";
            this.lblDirectoryCSV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 134);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(202, 16);
            this.label11.TabIndex = 25;
            this.label11.Text = "Planilha de exceção de viagens:";
            // 
            // btnSetDirectoryTripExceptionCSV
            // 
            this.btnSetDirectoryTripExceptionCSV.Location = new System.Drawing.Point(11, 178);
            this.btnSetDirectoryTripExceptionCSV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetDirectoryTripExceptionCSV.Name = "btnSetDirectoryTripExceptionCSV";
            this.btnSetDirectoryTripExceptionCSV.Size = new System.Drawing.Size(64, 23);
            this.btnSetDirectoryTripExceptionCSV.TabIndex = 23;
            this.btnSetDirectoryTripExceptionCSV.Text = "Alterar";
            this.btnSetDirectoryTripExceptionCSV.UseVisualStyleBackColor = true;
            this.btnSetDirectoryTripExceptionCSV.Click += new System.EventHandler(this.btnSetDirectoryTripExceptionCSV_Click);
            // 
            // lblDirectoryTripExceptionCSV
            // 
            this.lblDirectoryTripExceptionCSV.Location = new System.Drawing.Point(12, 153);
            this.lblDirectoryTripExceptionCSV.Name = "lblDirectoryTripExceptionCSV";
            this.lblDirectoryTripExceptionCSV.Size = new System.Drawing.Size(411, 23);
            this.lblDirectoryTripExceptionCSV.TabIndex = 24;
            this.lblDirectoryTripExceptionCSV.Text = "-";
            this.lblDirectoryTripExceptionCSV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBucketName);
            this.groupBox1.Controls.Add(this.txtSecretAccessKey);
            this.groupBox1.Controls.Add(this.txtAccessKey);
            this.groupBox1.Controls.Add(this.btnSaveSettings);
            this.groupBox1.Location = new System.Drawing.Point(16, 246);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(464, 322);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Acesso ao Bucket";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Bucket Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "Secret Access Key:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "Access Key:";
            // 
            // txtBucketName
            // 
            this.txtBucketName.Location = new System.Drawing.Point(19, 175);
            this.txtBucketName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBucketName.Name = "txtBucketName";
            this.txtBucketName.Size = new System.Drawing.Size(427, 22);
            this.txtBucketName.TabIndex = 31;
            // 
            // txtSecretAccessKey
            // 
            this.txtSecretAccessKey.Location = new System.Drawing.Point(19, 114);
            this.txtSecretAccessKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecretAccessKey.Name = "txtSecretAccessKey";
            this.txtSecretAccessKey.Size = new System.Drawing.Size(427, 22);
            this.txtSecretAccessKey.TabIndex = 30;
            // 
            // txtAccessKey
            // 
            this.txtAccessKey.Location = new System.Drawing.Point(19, 57);
            this.txtAccessKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccessKey.Name = "txtAccessKey";
            this.txtAccessKey.Size = new System.Drawing.Size(427, 22);
            this.txtAccessKey.TabIndex = 29;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(23, 283);
            this.btnSaveSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(113, 23);
            this.btnSaveSettings.TabIndex = 17;
            this.btnSaveSettings.Text = "Salvar";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chkTripException);
            this.tabPage3.Controls.Add(this.btnInsertDataCECTableTemporary);
            this.tabPage3.Controls.Add(this.btnCancelCompare);
            this.tabPage3.Controls.Add(this.txtSQLQuery);
            this.tabPage3.Controls.Add(this.btnExcel);
            this.tabPage3.Controls.Add(this.lblTotalInvoiceWithoutCEC);
            this.tabPage3.Controls.Add(this.lblTotalVerified);
            this.tabPage3.Controls.Add(this.progressBarCEC);
            this.tabPage3.Controls.Add(this.dgvInvoicesWithoutCEC);
            this.tabPage3.Controls.Add(this.btnbusinessCEC);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1341, 718);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "CEC not exists in the bucket";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chkTripException
            // 
            this.chkTripException.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTripException.AutoSize = true;
            this.chkTripException.Location = new System.Drawing.Point(950, 357);
            this.chkTripException.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkTripException.Name = "chkTripException";
            this.chkTripException.Size = new System.Drawing.Size(154, 20);
            this.chkTripException.TabIndex = 25;
            this.chkTripException.Text = "Exceção de Viagens";
            this.chkTripException.UseVisualStyleBackColor = true;
            // 
            // btnInsertDataCECTableTemporary
            // 
            this.btnInsertDataCECTableTemporary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInsertDataCECTableTemporary.Enabled = false;
            this.btnInsertDataCECTableTemporary.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertDataCECTableTemporary.Location = new System.Drawing.Point(172, 354);
            this.btnInsertDataCECTableTemporary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInsertDataCECTableTemporary.Name = "btnInsertDataCECTableTemporary";
            this.btnInsertDataCECTableTemporary.Size = new System.Drawing.Size(221, 28);
            this.btnInsertDataCECTableTemporary.TabIndex = 3;
            this.btnInsertDataCECTableTemporary.Text = "Inserir na Tabela Temporaria";
            this.btnInsertDataCECTableTemporary.UseVisualStyleBackColor = true;
            // 
            // btnCancelCompare
            // 
            this.btnCancelCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelCompare.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelCompare.Enabled = false;
            this.btnCancelCompare.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelCompare.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelCompare.ForeColor = System.Drawing.Color.Red;
            this.btnCancelCompare.Location = new System.Drawing.Point(1117, 350);
            this.btnCancelCompare.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelCompare.Name = "btnCancelCompare";
            this.btnCancelCompare.Size = new System.Drawing.Size(93, 28);
            this.btnCancelCompare.TabIndex = 11;
            this.btnCancelCompare.Text = "Cancelar";
            this.btnCancelCompare.UseVisualStyleBackColor = false;
            this.btnCancelCompare.Visible = false;
            this.btnCancelCompare.Click += new System.EventHandler(this.btnCancelCompare_Click);
            // 
            // txtSQLQuery
            // 
            this.txtSQLQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQLQuery.Location = new System.Drawing.Point(29, 25);
            this.txtSQLQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSQLQuery.Multiline = true;
            this.txtSQLQuery.Name = "txtSQLQuery";
            this.txtSQLQuery.Size = new System.Drawing.Size(1281, 293);
            this.txtSQLQuery.TabIndex = 0;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExcel.Enabled = false;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Location = new System.Drawing.Point(29, 354);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(136, 27);
            this.btnExcel.TabIndex = 2;
            this.btnExcel.Text = "Extrair para Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lblTotalInvoiceWithoutCEC
            // 
            this.lblTotalInvoiceWithoutCEC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalInvoiceWithoutCEC.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalInvoiceWithoutCEC.Location = new System.Drawing.Point(1109, 649);
            this.lblTotalInvoiceWithoutCEC.Name = "lblTotalInvoiceWithoutCEC";
            this.lblTotalInvoiceWithoutCEC.Size = new System.Drawing.Size(203, 14);
            this.lblTotalInvoiceWithoutCEC.TabIndex = 24;
            this.lblTotalInvoiceWithoutCEC.Text = "0";
            this.lblTotalInvoiceWithoutCEC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalVerified
            // 
            this.lblTotalVerified.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVerified.Location = new System.Drawing.Point(575, 660);
            this.lblTotalVerified.Name = "lblTotalVerified";
            this.lblTotalVerified.Size = new System.Drawing.Size(203, 28);
            this.lblTotalVerified.TabIndex = 23;
            this.lblTotalVerified.Text = "0/0";
            this.lblTotalVerified.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarCEC
            // 
            this.progressBarCEC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarCEC.Location = new System.Drawing.Point(29, 697);
            this.progressBarCEC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBarCEC.Name = "progressBarCEC";
            this.progressBarCEC.Size = new System.Drawing.Size(1281, 17);
            this.progressBarCEC.TabIndex = 3;
            // 
            // dgvInvoicesWithoutCEC
            // 
            this.dgvInvoicesWithoutCEC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInvoicesWithoutCEC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoicesWithoutCEC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cIDCompany,
            this.cIDInvoicee,
            this.cSerie,
            this.cIDBranchInvoice,
            this.cPathCEC,
            this.dEmission,
            this.cIDCustomer,
            this.cIDTrip,
            this.dExportCECDanf});
            this.dgvInvoicesWithoutCEC.Location = new System.Drawing.Point(29, 388);
            this.dgvInvoicesWithoutCEC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvInvoicesWithoutCEC.Name = "dgvInvoicesWithoutCEC";
            this.dgvInvoicesWithoutCEC.RowHeadersWidth = 51;
            this.dgvInvoicesWithoutCEC.RowTemplate.Height = 24;
            this.dgvInvoicesWithoutCEC.Size = new System.Drawing.Size(1281, 258);
            this.dgvInvoicesWithoutCEC.TabIndex = 2;
            this.dgvInvoicesWithoutCEC.TabStop = false;
            // 
            // cIDCompany
            // 
            this.cIDCompany.HeaderText = "cIDCompany";
            this.cIDCompany.MinimumWidth = 6;
            this.cIDCompany.Name = "cIDCompany";
            this.cIDCompany.Width = 125;
            // 
            // cIDInvoicee
            // 
            this.cIDInvoicee.HeaderText = "cIDInvoice";
            this.cIDInvoicee.MinimumWidth = 6;
            this.cIDInvoicee.Name = "cIDInvoicee";
            this.cIDInvoicee.Width = 125;
            // 
            // cSerie
            // 
            this.cSerie.HeaderText = "cSerie";
            this.cSerie.MinimumWidth = 6;
            this.cSerie.Name = "cSerie";
            this.cSerie.Width = 125;
            // 
            // cIDBranchInvoice
            // 
            this.cIDBranchInvoice.HeaderText = "cIDBranchInvoice";
            this.cIDBranchInvoice.MinimumWidth = 6;
            this.cIDBranchInvoice.Name = "cIDBranchInvoice";
            this.cIDBranchInvoice.Width = 125;
            // 
            // cPathCEC
            // 
            this.cPathCEC.HeaderText = "cPathCEC";
            this.cPathCEC.MinimumWidth = 6;
            this.cPathCEC.Name = "cPathCEC";
            this.cPathCEC.Width = 125;
            // 
            // dEmission
            // 
            this.dEmission.HeaderText = "dEmission";
            this.dEmission.MinimumWidth = 6;
            this.dEmission.Name = "dEmission";
            this.dEmission.Width = 125;
            // 
            // cIDCustomer
            // 
            this.cIDCustomer.HeaderText = "cIDCustomer";
            this.cIDCustomer.MinimumWidth = 6;
            this.cIDCustomer.Name = "cIDCustomer";
            this.cIDCustomer.Width = 125;
            // 
            // cIDTrip
            // 
            this.cIDTrip.HeaderText = "cIDTrip";
            this.cIDTrip.MinimumWidth = 6;
            this.cIDTrip.Name = "cIDTrip";
            this.cIDTrip.Width = 125;
            // 
            // dExportCECDanf
            // 
            this.dExportCECDanf.HeaderText = "dExportCECDanf";
            this.dExportCECDanf.MinimumWidth = 6;
            this.dExportCECDanf.Name = "dExportCECDanf";
            this.dExportCECDanf.Width = 125;
            // 
            // btnbusinessCEC
            // 
            this.btnbusinessCEC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnbusinessCEC.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbusinessCEC.Location = new System.Drawing.Point(1217, 345);
            this.btnbusinessCEC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnbusinessCEC.Name = "btnbusinessCEC";
            this.btnbusinessCEC.Size = new System.Drawing.Size(93, 38);
            this.btnbusinessCEC.TabIndex = 1;
            this.btnbusinessCEC.Text = "Comparar";
            this.btnbusinessCEC.UseVisualStyleBackColor = true;
            this.btnbusinessCEC.Click += new System.EventHandler(this.btnbusinessCEC_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.label18);
            this.tabPage6.Controls.Add(this.label17);
            this.tabPage6.Controls.Add(this.label15);
            this.tabPage6.Controls.Add(this.btnCECSaveImage);
            this.tabPage6.Controls.Add(this.btnCECOpenFolder);
            this.tabPage6.Controls.Add(this.btnCECOpenImage);
            this.tabPage6.Controls.Add(this.imgCECs);
            this.tabPage6.Controls.Add(this.btnDesmarcarTodasCECs);
            this.tabPage6.Controls.Add(this.btnSelecionarTodasCECs);
            this.tabPage6.Controls.Add(this.btnDownloadCECs);
            this.tabPage6.Controls.Add(this.btnPesquisarCECs);
            this.tabPage6.Controls.Add(this.dgvCECs);
            this.tabPage6.Controls.Add(this.txtQueryCECs);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage6.Size = new System.Drawing.Size(1341, 718);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.Text = "Search CEC";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(639, 16);
            this.label18.TabIndex = 15;
            this.label18.Text = "> o WMTool usa a coluna cPathCEC para buscar os arquivos no bucket, é obrigatorio" +
    " seu retorno na query";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(637, 16);
            this.label17.TabIndex = 14;
            this.label17.Text = "> Deve conter as colunas cIDCompany, cIDInvoice, cSerie, cIDBranchInvoice, cPathC" +
    "EC em seu resultado";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 16);
            this.label15.TabIndex = 13;
            this.label15.Text = "Query:";
            // 
            // btnCECSaveImage
            // 
            this.btnCECSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCECSaveImage.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCECSaveImage.Location = new System.Drawing.Point(973, 10);
            this.btnCECSaveImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCECSaveImage.Name = "btnCECSaveImage";
            this.btnCECSaveImage.Size = new System.Drawing.Size(125, 26);
            this.btnCECSaveImage.TabIndex = 12;
            this.btnCECSaveImage.Text = "Salvar Imagem";
            this.btnCECSaveImage.UseVisualStyleBackColor = true;
            this.btnCECSaveImage.Click += new System.EventHandler(this.btnCECSaveImage_Click);
            // 
            // btnCECOpenFolder
            // 
            this.btnCECOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCECOpenFolder.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCECOpenFolder.Location = new System.Drawing.Point(840, 10);
            this.btnCECOpenFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCECOpenFolder.Name = "btnCECOpenFolder";
            this.btnCECOpenFolder.Size = new System.Drawing.Size(128, 26);
            this.btnCECOpenFolder.TabIndex = 11;
            this.btnCECOpenFolder.Text = "Abrir Pasta";
            this.btnCECOpenFolder.UseVisualStyleBackColor = true;
            this.btnCECOpenFolder.Click += new System.EventHandler(this.btnCECOpenFolder_Click);
            // 
            // btnCECOpenImage
            // 
            this.btnCECOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCECOpenImage.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCECOpenImage.Location = new System.Drawing.Point(712, 10);
            this.btnCECOpenImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCECOpenImage.Name = "btnCECOpenImage";
            this.btnCECOpenImage.Size = new System.Drawing.Size(123, 26);
            this.btnCECOpenImage.TabIndex = 10;
            this.btnCECOpenImage.Text = "Abrir Imagem";
            this.btnCECOpenImage.UseVisualStyleBackColor = true;
            this.btnCECOpenImage.Click += new System.EventHandler(this.btn_Click);
            // 
            // imgCECs
            // 
            this.imgCECs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgCECs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgCECs.Location = new System.Drawing.Point(712, 39);
            this.imgCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imgCECs.Name = "imgCECs";
            this.imgCECs.Size = new System.Drawing.Size(614, 681);
            this.imgCECs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCECs.TabIndex = 7;
            this.imgCECs.TabStop = false;
            // 
            // btnDesmarcarTodasCECs
            // 
            this.btnDesmarcarTodasCECs.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesmarcarTodasCECs.Location = new System.Drawing.Point(164, 337);
            this.btnDesmarcarTodasCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDesmarcarTodasCECs.Name = "btnDesmarcarTodasCECs";
            this.btnDesmarcarTodasCECs.Size = new System.Drawing.Size(139, 26);
            this.btnDesmarcarTodasCECs.TabIndex = 9;
            this.btnDesmarcarTodasCECs.Text = "Desmarcar Todos";
            this.btnDesmarcarTodasCECs.UseVisualStyleBackColor = true;
            this.btnDesmarcarTodasCECs.Click += new System.EventHandler(this.btnDesmarcarTodasCECs_Click);
            // 
            // btnSelecionarTodasCECs
            // 
            this.btnSelecionarTodasCECs.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarTodasCECs.Location = new System.Drawing.Point(17, 337);
            this.btnSelecionarTodasCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelecionarTodasCECs.Name = "btnSelecionarTodasCECs";
            this.btnSelecionarTodasCECs.Size = new System.Drawing.Size(141, 26);
            this.btnSelecionarTodasCECs.TabIndex = 8;
            this.btnSelecionarTodasCECs.Text = "Selecionar Todos";
            this.btnSelecionarTodasCECs.UseVisualStyleBackColor = true;
            this.btnSelecionarTodasCECs.Click += new System.EventHandler(this.btnSelecionarTodasCECs_Click);
            // 
            // btnDownloadCECs
            // 
            this.btnDownloadCECs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadCECs.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadCECs.Location = new System.Drawing.Point(532, 337);
            this.btnDownloadCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDownloadCECs.Name = "btnDownloadCECs";
            this.btnDownloadCECs.Size = new System.Drawing.Size(159, 26);
            this.btnDownloadCECs.TabIndex = 6;
            this.btnDownloadCECs.Text = "Salvar Selecionados";
            this.btnDownloadCECs.UseVisualStyleBackColor = true;
            this.btnDownloadCECs.Click += new System.EventHandler(this.btnDownloadCECs_Click);
            // 
            // btnPesquisarCECs
            // 
            this.btnPesquisarCECs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisarCECs.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisarCECs.Location = new System.Drawing.Point(419, 337);
            this.btnPesquisarCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPesquisarCECs.Name = "btnPesquisarCECs";
            this.btnPesquisarCECs.Size = new System.Drawing.Size(107, 26);
            this.btnPesquisarCECs.TabIndex = 5;
            this.btnPesquisarCECs.Text = "Pesquisar";
            this.btnPesquisarCECs.UseVisualStyleBackColor = true;
            this.btnPesquisarCECs.Click += new System.EventHandler(this.btnPesquisarCECs_Click);
            // 
            // dgvCECs
            // 
            this.dgvCECs.AllowUserToAddRows = false;
            this.dgvCECs.AllowUserToDeleteRows = false;
            this.dgvCECs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCECs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCECs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCECs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckCEC,
            this.downloadCEC,
            this.CECcIDCompany,
            this.CECcIDInvoice,
            this.CECcSerie,
            this.CECcIDBranchInvoice,
            this.cPathCECCC,
            this.Joker});
            this.dgvCECs.Location = new System.Drawing.Point(17, 369);
            this.dgvCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvCECs.Name = "dgvCECs";
            this.dgvCECs.RowHeadersWidth = 51;
            this.dgvCECs.RowTemplate.Height = 24;
            this.dgvCECs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCECs.Size = new System.Drawing.Size(675, 350);
            this.dgvCECs.TabIndex = 4;
            this.dgvCECs.TabStop = false;
            this.dgvCECs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCECs_CellContentClick);
            this.dgvCECs.SelectionChanged += new System.EventHandler(this.dgvCECs_SelectionChanged);
            // 
            // CheckCEC
            // 
            this.CheckCEC.HeaderText = "";
            this.CheckCEC.MinimumWidth = 6;
            this.CheckCEC.Name = "CheckCEC";
            this.CheckCEC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CheckCEC.Width = 23;
            // 
            // downloadCEC
            // 
            this.downloadCEC.HeaderText = "";
            this.downloadCEC.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.downloadCEC.MinimumWidth = 6;
            this.downloadCEC.Name = "downloadCEC";
            this.downloadCEC.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.downloadCEC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.downloadCEC.Width = 23;
            // 
            // CECcIDCompany
            // 
            this.CECcIDCompany.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CECcIDCompany.DataPropertyName = "cIDCompany";
            this.CECcIDCompany.HeaderText = "cIDCompany";
            this.CECcIDCompany.MinimumWidth = 6;
            this.CECcIDCompany.Name = "CECcIDCompany";
            this.CECcIDCompany.Width = 114;
            // 
            // CECcIDInvoice
            // 
            this.CECcIDInvoice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CECcIDInvoice.DataPropertyName = "cIDInvoice";
            this.CECcIDInvoice.HeaderText = "cIDInvoice";
            this.CECcIDInvoice.MinimumWidth = 6;
            this.CECcIDInvoice.Name = "CECcIDInvoice";
            this.CECcIDInvoice.Width = 99;
            // 
            // CECcSerie
            // 
            this.CECcSerie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CECcSerie.DataPropertyName = "cSerie";
            this.CECcSerie.HeaderText = "cSerie";
            this.CECcSerie.MinimumWidth = 6;
            this.CECcSerie.Name = "CECcSerie";
            this.CECcSerie.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CECcSerie.Width = 75;
            // 
            // CECcIDBranchInvoice
            // 
            this.CECcIDBranchInvoice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CECcIDBranchInvoice.DataPropertyName = "cIDBranchInvoice";
            this.CECcIDBranchInvoice.HeaderText = "cIDBranchInvoice";
            this.CECcIDBranchInvoice.MinimumWidth = 6;
            this.CECcIDBranchInvoice.Name = "CECcIDBranchInvoice";
            this.CECcIDBranchInvoice.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CECcIDBranchInvoice.Width = 141;
            // 
            // cPathCECCC
            // 
            this.cPathCECCC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cPathCECCC.DataPropertyName = "cPathCEC";
            this.cPathCECCC.HeaderText = "cPathCEC";
            this.cPathCECCC.MinimumWidth = 6;
            this.cPathCECCC.Name = "cPathCECCC";
            this.cPathCECCC.Width = 97;
            // 
            // Joker
            // 
            this.Joker.HeaderText = "";
            this.Joker.MinimumWidth = 6;
            this.Joker.Name = "Joker";
            this.Joker.Width = 23;
            // 
            // txtQueryCECs
            // 
            this.txtQueryCECs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueryCECs.Location = new System.Drawing.Point(17, 77);
            this.txtQueryCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtQueryCECs.Multiline = true;
            this.txtQueryCECs.Name = "txtQueryCECs";
            this.txtQueryCECs.Size = new System.Drawing.Size(673, 234);
            this.txtQueryCECs.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPageRequest);
            this.tabControl1.Controls.Add(this.tabPageJsonReprocessor);
            this.tabControl1.Controls.Add(this.tabPageInsertScript);
            this.tabControl1.Controls.Add(this.tabPageValidation);
            this.tabControl1.Controls.Add(this.tabPageDatabaseComparison);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 5);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1349, 747);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPageRequest
            // 
            this.tabPageRequest.Controls.Add(this.groupBoxRequestConfig);
            this.tabPageRequest.Controls.Add(this.btnRequest);
            this.tabPageRequest.Controls.Add(this.lblRequestLastUpdate);
            this.tabPageRequest.Controls.Add(this.lblRequestStatus);
            this.tabPageRequest.Controls.Add(this.lblRequestLogTitle);
            this.tabPageRequest.Controls.Add(this.btnClearRequestLog);
            this.tabPageRequest.Controls.Add(this.txtRequestLog);
            this.tabPageRequest.Location = new System.Drawing.Point(4, 25);
            this.tabPageRequest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageRequest.Name = "tabPageRequest";
            this.tabPageRequest.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageRequest.Size = new System.Drawing.Size(1341, 718);
            this.tabPageRequest.TabIndex = 7;
            this.tabPageRequest.Text = "Request";
            this.tabPageRequest.UseVisualStyleBackColor = true;
            // 
            // groupBoxRequestConfig
            // 
            this.groupBoxRequestConfig.Controls.Add(this.label14);
            this.groupBoxRequestConfig.Controls.Add(this.txtRequestUser);
            this.groupBoxRequestConfig.Controls.Add(this.label7);
            this.groupBoxRequestConfig.Controls.Add(this.txtRequestPassword);
            this.groupBoxRequestConfig.Controls.Add(this.label8);
            this.groupBoxRequestConfig.Controls.Add(this.txtRequestDomain);
            this.groupBoxRequestConfig.Controls.Add(this.label9);
            this.groupBoxRequestConfig.Controls.Add(this.txtRequestEnvironment);
            this.groupBoxRequestConfig.Controls.Add(this.label12);
            this.groupBoxRequestConfig.Controls.Add(this.txtRequestURLToken);
            this.groupBoxRequestConfig.Controls.Add(this.label13);
            this.groupBoxRequestConfig.Controls.Add(this.txtRequestURLSync);
            this.groupBoxRequestConfig.Location = new System.Drawing.Point(16, 16);
            this.groupBoxRequestConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxRequestConfig.Name = "groupBoxRequestConfig";
            this.groupBoxRequestConfig.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxRequestConfig.Size = new System.Drawing.Size(390, 350);
            this.groupBoxRequestConfig.TabIndex = 13;
            this.groupBoxRequestConfig.TabStop = false;
            this.groupBoxRequestConfig.Text = "Configuração da Requisição";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 16);
            this.label14.TabIndex = 39;
            this.label14.Text = "Usuario:";
            // 
            // txtRequestUser
            // 
            this.txtRequestUser.Location = new System.Drawing.Point(7, 48);
            this.txtRequestUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestUser.Name = "txtRequestUser";
            this.txtRequestUser.Size = new System.Drawing.Size(360, 22);
            this.txtRequestUser.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 16);
            this.label7.TabIndex = 26;
            this.label7.Text = "Senha:";
            // 
            // txtRequestPassword
            // 
            this.txtRequestPassword.Location = new System.Drawing.Point(7, 100);
            this.txtRequestPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestPassword.Name = "txtRequestPassword";
            this.txtRequestPassword.Size = new System.Drawing.Size(360, 22);
            this.txtRequestPassword.TabIndex = 44;
            this.txtRequestPassword.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 16);
            this.label8.TabIndex = 50;
            this.label8.Text = "Dominio:";
            // 
            // txtRequestDomain
            // 
            this.txtRequestDomain.Location = new System.Drawing.Point(7, 152);
            this.txtRequestDomain.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestDomain.Name = "txtRequestDomain";
            this.txtRequestDomain.Size = new System.Drawing.Size(360, 22);
            this.txtRequestDomain.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 184);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 16);
            this.label9.TabIndex = 51;
            this.label9.Text = "Enviroment:";
            // 
            // txtRequestEnvironment
            // 
            this.txtRequestEnvironment.Location = new System.Drawing.Point(7, 204);
            this.txtRequestEnvironment.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestEnvironment.Name = "txtRequestEnvironment";
            this.txtRequestEnvironment.Size = new System.Drawing.Size(360, 22);
            this.txtRequestEnvironment.TabIndex = 46;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 236);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 16);
            this.label12.TabIndex = 52;
            this.label12.Text = "URL Token:";
            // 
            // txtRequestURLToken
            // 
            this.txtRequestURLToken.Location = new System.Drawing.Point(7, 256);
            this.txtRequestURLToken.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestURLToken.Name = "txtRequestURLToken";
            this.txtRequestURLToken.Size = new System.Drawing.Size(360, 22);
            this.txtRequestURLToken.TabIndex = 47;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 288);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 16);
            this.label13.TabIndex = 53;
            this.label13.Text = "URL Sync:";
            // 
            // txtRequestURLSync
            // 
            this.txtRequestURLSync.Location = new System.Drawing.Point(7, 308);
            this.txtRequestURLSync.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestURLSync.Name = "txtRequestURLSync";
            this.txtRequestURLSync.Size = new System.Drawing.Size(360, 22);
            this.txtRequestURLSync.TabIndex = 48;
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(16, 376);
            this.btnRequest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(160, 32);
            this.btnRequest.TabIndex = 37;
            this.btnRequest.Text = "Executar Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // lblRequestLastUpdate
            // 
            this.lblRequestLastUpdate.Location = new System.Drawing.Point(184, 384);
            this.lblRequestLastUpdate.Name = "lblRequestLastUpdate";
            this.lblRequestLastUpdate.Size = new System.Drawing.Size(220, 16);
            this.lblRequestLastUpdate.TabIndex = 40;
            // 
            // lblRequestStatus
            // 
            this.lblRequestStatus.Location = new System.Drawing.Point(16, 418);
            this.lblRequestStatus.Name = "lblRequestStatus";
            this.lblRequestStatus.Size = new System.Drawing.Size(390, 60);
            this.lblRequestStatus.TabIndex = 38;
            // 
            // lblRequestLogTitle
            // 
            this.lblRequestLogTitle.AutoSize = true;
            this.lblRequestLogTitle.Location = new System.Drawing.Point(426, 16);
            this.lblRequestLogTitle.Name = "lblRequestLogTitle";
            this.lblRequestLogTitle.Size = new System.Drawing.Size(125, 16);
            this.lblRequestLogTitle.TabIndex = 54;
            this.lblRequestLogTitle.Text = "Log de requisições:";
            // 
            // btnClearRequestLog
            // 
            this.btnClearRequestLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearRequestLog.Location = new System.Drawing.Point(1245, 11);
            this.btnClearRequestLog.Name = "btnClearRequestLog";
            this.btnClearRequestLog.Size = new System.Drawing.Size(80, 23);
            this.btnClearRequestLog.TabIndex = 55;
            this.btnClearRequestLog.Text = "Limpar";
            this.btnClearRequestLog.UseVisualStyleBackColor = true;
            this.btnClearRequestLog.Click += new System.EventHandler(this.btnClearRequestLog_Click);
            // 
            // txtRequestLog
            // 
            this.txtRequestLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequestLog.BackColor = System.Drawing.Color.Black;
            this.txtRequestLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRequestLog.ForeColor = System.Drawing.Color.Lime;
            this.txtRequestLog.Location = new System.Drawing.Point(426, 40);
            this.txtRequestLog.Multiline = true;
            this.txtRequestLog.Name = "txtRequestLog";
            this.txtRequestLog.ReadOnly = true;
            this.txtRequestLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRequestLog.Size = new System.Drawing.Size(899, 662);
            this.txtRequestLog.TabIndex = 56;
            // 
            // tabPageJsonReprocessor
            // 
            this.tabPageJsonReprocessor.Controls.Add(this.ucJsonReprocessor1);
            this.tabPageJsonReprocessor.Location = new System.Drawing.Point(4, 25);
            this.tabPageJsonReprocessor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageJsonReprocessor.Name = "tabPageJsonReprocessor";
            this.tabPageJsonReprocessor.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageJsonReprocessor.Size = new System.Drawing.Size(1341, 718);
            this.tabPageJsonReprocessor.TabIndex = 9;
            this.tabPageJsonReprocessor.Text = "Reprocessar JSON";
            this.tabPageJsonReprocessor.UseVisualStyleBackColor = true;
            // 
            // ucJsonReprocessor1
            // 
            this.ucJsonReprocessor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucJsonReprocessor1.Location = new System.Drawing.Point(3, 2);
            this.ucJsonReprocessor1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucJsonReprocessor1.Name = "ucJsonReprocessor1";
            this.ucJsonReprocessor1.Size = new System.Drawing.Size(1335, 714);
            this.ucJsonReprocessor1.TabIndex = 0;
            // 
            // tabPageInsertScript
            // 
            this.tabPageInsertScript.Controls.Add(this.ucInsertScriptGenerator1);
            this.tabPageInsertScript.Location = new System.Drawing.Point(4, 25);
            this.tabPageInsertScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageInsertScript.Name = "tabPageInsertScript";
            this.tabPageInsertScript.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageInsertScript.Size = new System.Drawing.Size(1341, 718);
            this.tabPageInsertScript.TabIndex = 10;
            this.tabPageInsertScript.Text = "Gerar Script INSERT";
            this.tabPageInsertScript.UseVisualStyleBackColor = true;
            // 
            // ucInsertScriptGenerator1
            // 
            this.ucInsertScriptGenerator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInsertScriptGenerator1.Location = new System.Drawing.Point(3, 2);
            this.ucInsertScriptGenerator1.Name = "ucInsertScriptGenerator1";
            this.ucInsertScriptGenerator1.Size = new System.Drawing.Size(1335, 714);
            this.ucInsertScriptGenerator1.TabIndex = 0;
            // 
            // tabPageValidation
            // 
            this.tabPageValidation.Controls.Add(this.ucValidation1);
            this.tabPageValidation.Location = new System.Drawing.Point(4, 25);
            this.tabPageValidation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageValidation.Name = "tabPageValidation";
            this.tabPageValidation.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageValidation.Size = new System.Drawing.Size(1341, 718);
            this.tabPageValidation.TabIndex = 8;
            this.tabPageValidation.Text = "Validação JSON x Banco";
            this.tabPageValidation.UseVisualStyleBackColor = true;
            // 
            // ucValidation1
            // 
            this.ucValidation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucValidation1.Location = new System.Drawing.Point(3, 2);
            this.ucValidation1.Name = "ucValidation1";
            this.ucValidation1.Size = new System.Drawing.Size(1335, 714);
            this.ucValidation1.TabIndex = 0;
            // 
            // tabPageDatabaseComparison
            // 
            this.tabPageDatabaseComparison.Controls.Add(this.ucDatabaseComparison1);
            this.tabPageDatabaseComparison.Location = new System.Drawing.Point(4, 25);
            this.tabPageDatabaseComparison.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageDatabaseComparison.Name = "tabPageDatabaseComparison";
            this.tabPageDatabaseComparison.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageDatabaseComparison.Size = new System.Drawing.Size(1341, 718);
            this.tabPageDatabaseComparison.TabIndex = 10;
            this.tabPageDatabaseComparison.Text = "Validação Banco x Banco";
            this.tabPageDatabaseComparison.UseVisualStyleBackColor = true;
            // 
            // ucDatabaseComparison1
            // 
            this.ucDatabaseComparison1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDatabaseComparison1.Location = new System.Drawing.Point(3, 2);
            this.ucDatabaseComparison1.Name = "ucDatabaseComparison1";
            this.ucDatabaseComparison1.Size = new System.Drawing.Size(1335, 714);
            this.ucDatabaseComparison1.TabIndex = 0;
            // 
            // frmHomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 773);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmHomeScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WMTool";
            this.Load += new System.EventHandler(this.frmHomeScreen_Load);
            this.tabPage2.ResumeLayout(false);
            this.groupBoxValidationRules.ResumeLayout(false);
            this.groupBoxValidationRules.PerformLayout();
            this.groupBoxDbComparisonRules.ResumeLayout(false);
            this.groupBoxDbComparisonRules.PerformLayout();
            this.groupBoxInsertScriptRules.ResumeLayout(false);
            this.groupBoxInsertScriptRules.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoicesWithoutCEC)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCECs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCECs)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageRequest.ResumeLayout(false);
            this.tabPageRequest.PerformLayout();
            this.groupBoxRequestConfig.ResumeLayout(false);
            this.groupBoxRequestConfig.PerformLayout();
            this.tabPageJsonReprocessor.ResumeLayout(false);
            this.tabPageInsertScript.ResumeLayout(false);
            this.tabPageValidation.ResumeLayout(false);
            this.tabPageDatabaseComparison.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgImageBucket;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSaveSettingsDB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServerDB;
        private System.Windows.Forms.TextBox txtNameDB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSetDirectoryCECs;
        private System.Windows.Forms.Label lblDirectoryCECs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetDirectoryCSV;
        private System.Windows.Forms.Label lblDirectoryCSV;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSetDirectoryTripExceptionCSV;
        private System.Windows.Forms.Label lblDirectoryTripExceptionCSV;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBucketName;
        private System.Windows.Forms.TextBox txtSecretAccessKey;
        private System.Windows.Forms.TextBox txtAccessKey;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkTripException;
        private System.Windows.Forms.Button btnInsertDataCECTableTemporary;
        private System.Windows.Forms.Button btnCancelCompare;
        private System.Windows.Forms.TextBox txtSQLQuery;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Label lblTotalInvoiceWithoutCEC;
        private System.Windows.Forms.Label lblTotalVerified;
        private System.Windows.Forms.ProgressBar progressBarCEC;
        private System.Windows.Forms.DataGridView dgvInvoicesWithoutCEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIDCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIDInvoicee;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSerie;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIDBranchInvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPathCEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEmission;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIDCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIDTrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn dExportCECDanf;
        private System.Windows.Forms.Button btnbusinessCEC;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btnCECOpenImage;
        private System.Windows.Forms.PictureBox imgCECs;
        private System.Windows.Forms.Button btnDesmarcarTodasCECs;
        private System.Windows.Forms.Button btnSelecionarTodasCECs;
        private System.Windows.Forms.Button btnDownloadCECs;
        private System.Windows.Forms.Button btnPesquisarCECs;
        private System.Windows.Forms.DataGridView dgvCECs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckCEC;
        private System.Windows.Forms.DataGridViewImageColumn downloadCEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CECcIDCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn CECcIDInvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CECcSerie;
        private System.Windows.Forms.DataGridViewTextBoxColumn CECcIDBranchInvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPathCECCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Joker;
        private System.Windows.Forms.TextBox txtQueryCECs;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnCECOpenFolder;
        private System.Windows.Forms.Button btnCECSaveImage;
        private System.Windows.Forms.TabPage tabPageRequest;
        private System.Windows.Forms.Label lblRequestStatus;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.TextBox txtRequestUser;
        private System.Windows.Forms.GroupBox groupBoxRequestConfig;
        private System.Windows.Forms.Label lblRequestLogTitle;
        private System.Windows.Forms.Button btnClearRequestLog;
        private System.Windows.Forms.TextBox txtRequestLog;
        private System.Windows.Forms.GroupBox groupBoxValidationRules;
        private System.Windows.Forms.Label lblValidationRulesPath;
        private System.Windows.Forms.TextBox txtValidationRulesPath;
        private System.Windows.Forms.Button btnBrowseValidationRulesPath;
        private System.Windows.Forms.Button btnSaveValidationRulesPath;
        private System.Windows.Forms.GroupBox groupBoxDbComparisonRules;
        private System.Windows.Forms.Label lblDbComparisonRulesPath;
        private System.Windows.Forms.TextBox txtDbComparisonRulesPath;
        private System.Windows.Forms.Button btnBrowseDbComparisonRulesPath;
        private System.Windows.Forms.Button btnSaveDbComparisonRulesPath;
        private System.Windows.Forms.GroupBox groupBoxInsertScriptRules;
        private System.Windows.Forms.Label lblInsertScriptRulesPath;
        private System.Windows.Forms.TextBox txtInsertScriptRulesPath;
        private System.Windows.Forms.Button btnBrowseInsertScriptRulesPath;
        private System.Windows.Forms.Button btnSaveInsertScriptRulesPath;
        private System.Windows.Forms.TextBox txtRequestURLSync;
        private System.Windows.Forms.TextBox txtRequestURLToken;
        private System.Windows.Forms.TextBox txtRequestEnvironment;
        private System.Windows.Forms.TextBox txtRequestDomain;
        private System.Windows.Forms.TextBox txtRequestPassword;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRequestLastUpdate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tabPageValidation;
        private WMTool.Screens.ucValidation ucValidation1;
        private System.Windows.Forms.TabPage tabPageJsonReprocessor;
        private WMTool.Screens.ucJsonReprocessor ucJsonReprocessor1;
        private System.Windows.Forms.TabPage tabPageInsertScript;
        private WMTool.Screens.ucInsertScriptGenerator ucInsertScriptGenerator1;
        private System.Windows.Forms.TabPage tabPageDatabaseComparison;
        private WMTool.Screens.ucDatabaseComparison ucDatabaseComparison1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
    }
}

