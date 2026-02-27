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
            this.btnCompareCEC = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
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
            this.btnCECOpenFolder = new System.Windows.Forms.Button();
            this.btnCECSaveImage = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
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
            this.btnSaveSettingsDB.Location = new System.Drawing.Point(13, 189);
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
            this.txtServerDB.Location = new System.Drawing.Point(10, 51);
            this.txtServerDB.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerDB.Name = "txtServerDB";
            this.txtServerDB.Size = new System.Drawing.Size(427, 22);
            this.txtServerDB.TabIndex = 35;
            // 
            // txtNameDB
            // 
            this.txtNameDB.Location = new System.Drawing.Point(10, 110);
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
            this.groupBox3.Size = new System.Drawing.Size(523, 250);
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
            this.lblDirectoryCECs.Size = new System.Drawing.Size(447, 23);
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
            this.groupBox2.Size = new System.Drawing.Size(523, 295);
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
            this.lblDirectoryCSV.Size = new System.Drawing.Size(447, 23);
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
            this.lblDirectoryTripExceptionCSV.Size = new System.Drawing.Size(447, 23);
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
            this.txtBucketName.Location = new System.Drawing.Point(18, 175);
            this.txtBucketName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBucketName.Name = "txtBucketName";
            this.txtBucketName.Size = new System.Drawing.Size(427, 22);
            this.txtBucketName.TabIndex = 31;
            // 
            // txtSecretAccessKey
            // 
            this.txtSecretAccessKey.Location = new System.Drawing.Point(18, 115);
            this.txtSecretAccessKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecretAccessKey.Name = "txtSecretAccessKey";
            this.txtSecretAccessKey.Size = new System.Drawing.Size(427, 22);
            this.txtSecretAccessKey.TabIndex = 30;
            // 
            // txtAccessKey
            // 
            this.txtAccessKey.Location = new System.Drawing.Point(18, 56);
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
            this.tabPage3.Controls.Add(this.btnCompareCEC);
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
            this.chkTripException.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTripException.AutoSize = true;
            this.chkTripException.Location = new System.Drawing.Point(950, 356);
            this.chkTripException.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkTripException.Name = "chkTripException";
            this.chkTripException.Size = new System.Drawing.Size(154, 20);
            this.chkTripException.TabIndex = 25;
            this.chkTripException.Text = "Exceção de Viagens";
            this.chkTripException.UseVisualStyleBackColor = true;
            // 
            // btnInsertDataCECTableTemporary
            // 
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
            this.btnCancelCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            // btnCompareCEC
            // 
            this.btnCompareCEC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompareCEC.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompareCEC.Location = new System.Drawing.Point(1217, 345);
            this.btnCompareCEC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCompareCEC.Name = "btnCompareCEC";
            this.btnCompareCEC.Size = new System.Drawing.Size(93, 38);
            this.btnCompareCEC.TabIndex = 1;
            this.btnCompareCEC.Text = "Comparar";
            this.btnCompareCEC.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
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
            // btnCECOpenImage
            // 
            this.btnCECOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCECOpenImage.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCECOpenImage.Location = new System.Drawing.Point(712, 10);
            this.btnCECOpenImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCECOpenImage.Name = "btnCECOpenImage";
            this.btnCECOpenImage.Size = new System.Drawing.Size(122, 26);
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
            this.imgCECs.Location = new System.Drawing.Point(712, 40);
            this.imgCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imgCECs.Name = "imgCECs";
            this.imgCECs.Size = new System.Drawing.Size(614, 682);
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
            this.downloadCEC.Image = global::WMTool.Properties.Resources.down_to_line;
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
            this.txtQueryCECs.Location = new System.Drawing.Point(17, 18);
            this.txtQueryCECs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtQueryCECs.Multiline = true;
            this.txtQueryCECs.Name = "txtQueryCECs";
            this.txtQueryCECs.Size = new System.Drawing.Size(673, 293);
            this.txtQueryCECs.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 5);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1349, 747);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
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
            // btnCECSaveImage
            // 
            this.btnCECSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCECSaveImage.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCECSaveImage.Location = new System.Drawing.Point(974, 10);
            this.btnCECSaveImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCECSaveImage.Name = "btnCECSaveImage";
            this.btnCECSaveImage.Size = new System.Drawing.Size(125, 26);
            this.btnCECSaveImage.TabIndex = 12;
            this.btnCECSaveImage.Text = "Salvar Imagem";
            this.btnCECSaveImage.UseVisualStyleBackColor = true;
            this.btnCECSaveImage.Click += new System.EventHandler(this.btnCECSaveImage_Click);
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
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnCompareCEC;
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
    }
}

