namespace WMTool.Screens
{
    partial class ucValidation
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.rbSingleFile = new System.Windows.Forms.RadioButton();
            this.rbBatchFolder = new System.Windows.Forms.RadioButton();
            this.lblCIDInvoice = new System.Windows.Forms.Label();
            this.txtCIDInvoice = new System.Windows.Forms.TextBox();
            this.lblCSerie = new System.Windows.Forms.Label();
            this.txtCSerie = new System.Windows.Forms.TextBox();
            this.lblCIDBranchInvoice = new System.Windows.Forms.Label();
            this.txtCIDBranchInvoice = new System.Windows.Forms.TextBox();
            this.lblCIDCompany = new System.Windows.Forms.Label();
            this.txtCIDCompany = new System.Windows.Forms.TextBox();
            this.lblJson = new System.Windows.Forms.Label();
            this.btnLoadJsonFile = new System.Windows.Forms.Button();
            this.txtJson = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblRules = new System.Windows.Forms.Label();
            this.dgvRules = new System.Windows.Forms.DataGridView();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnRemoveRule = new System.Windows.Forms.Button();
            this.btnLoadRules = new System.Windows.Forms.Button();
            this.btnSaveRules = new System.Windows.Forms.Button();
            this.btnConfigureGeneralSql = new System.Windows.Forms.Button();
            this.lblGeneralSqlPreview = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.progressBarValidation = new System.Windows.Forms.ProgressBar();
            this.lblResults = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // rbSingleFile
            // 
            this.rbSingleFile.Checked = true;
            this.rbSingleFile.Location = new System.Drawing.Point(10, 10);
            this.rbSingleFile.Name = "rbSingleFile";
            this.rbSingleFile.Size = new System.Drawing.Size(150, 20);
            this.rbSingleFile.TabIndex = 0;
            this.rbSingleFile.TabStop = true;
            this.rbSingleFile.Text = "Arquivo único";
            // 
            // rbBatchFolder
            // 
            this.rbBatchFolder.Location = new System.Drawing.Point(170, 10);
            this.rbBatchFolder.Name = "rbBatchFolder";
            this.rbBatchFolder.Size = new System.Drawing.Size(150, 20);
            this.rbBatchFolder.TabIndex = 1;
            this.rbBatchFolder.Text = "Lote (pasta)";
            // 
            // lblCIDInvoice
            // 
            this.lblCIDInvoice.AutoSize = true;
            this.lblCIDInvoice.Location = new System.Drawing.Point(508, 13);
            this.lblCIDInvoice.Name = "lblCIDInvoice";
            this.lblCIDInvoice.Size = new System.Drawing.Size(62, 13);
            this.lblCIDInvoice.TabIndex = 18;
            this.lblCIDInvoice.Text = "cIDInvoice:";
            // 
            // txtCIDInvoice
            // 
            this.txtCIDInvoice.Location = new System.Drawing.Point(578, 10);
            this.txtCIDInvoice.Name = "txtCIDInvoice";
            this.txtCIDInvoice.Size = new System.Drawing.Size(100, 20);
            this.txtCIDInvoice.TabIndex = 1;
            // 
            // lblCSerie
            // 
            this.lblCSerie.AutoSize = true;
            this.lblCSerie.Location = new System.Drawing.Point(688, 13);
            this.lblCSerie.Name = "lblCSerie";
            this.lblCSerie.Size = new System.Drawing.Size(40, 13);
            this.lblCSerie.TabIndex = 20;
            this.lblCSerie.Text = "cSerie:";
            // 
            // txtCSerie
            // 
            this.txtCSerie.Location = new System.Drawing.Point(738, 10);
            this.txtCSerie.Name = "txtCSerie";
            this.txtCSerie.Size = new System.Drawing.Size(80, 20);
            this.txtCSerie.TabIndex = 2;
            // 
            // lblCIDBranchInvoice
            // 
            this.lblCIDBranchInvoice.AutoSize = true;
            this.lblCIDBranchInvoice.Location = new System.Drawing.Point(828, 13);
            this.lblCIDBranchInvoice.Name = "lblCIDBranchInvoice";
            this.lblCIDBranchInvoice.Size = new System.Drawing.Size(96, 13);
            this.lblCIDBranchInvoice.TabIndex = 22;
            this.lblCIDBranchInvoice.Text = "cIDBranchInvoice:";
            // 
            // txtCIDBranchInvoice
            // 
            this.txtCIDBranchInvoice.Location = new System.Drawing.Point(928, 10);
            this.txtCIDBranchInvoice.Name = "txtCIDBranchInvoice";
            this.txtCIDBranchInvoice.Size = new System.Drawing.Size(100, 20);
            this.txtCIDBranchInvoice.TabIndex = 3;
            // 
            // lblCIDCompany
            // 
            this.lblCIDCompany.AutoSize = true;
            this.lblCIDCompany.Location = new System.Drawing.Point(308, 14);
            this.lblCIDCompany.Name = "lblCIDCompany";
            this.lblCIDCompany.Size = new System.Drawing.Size(71, 13);
            this.lblCIDCompany.TabIndex = 24;
            this.lblCIDCompany.Text = "cIDCompany:";
            // 
            // txtCIDCompany
            // 
            this.txtCIDCompany.Location = new System.Drawing.Point(388, 11);
            this.txtCIDCompany.Name = "txtCIDCompany";
            this.txtCIDCompany.Size = new System.Drawing.Size(100, 20);
            this.txtCIDCompany.TabIndex = 0;
            // 
            // lblJson
            // 
            this.lblJson.Location = new System.Drawing.Point(10, 40);
            this.lblJson.Name = "lblJson";
            this.lblJson.Size = new System.Drawing.Size(300, 20);
            this.lblJson.TabIndex = 2;
            this.lblJson.Text = "Cole o JSON ou carregue um arquivo:";
            // 
            // btnLoadJsonFile
            // 
            this.btnLoadJsonFile.Location = new System.Drawing.Point(10, 64);
            this.btnLoadJsonFile.Name = "btnLoadJsonFile";
            this.btnLoadJsonFile.Size = new System.Drawing.Size(140, 26);
            this.btnLoadJsonFile.TabIndex = 4;
            this.btnLoadJsonFile.Text = "Carregar arquivo...";
            this.btnLoadJsonFile.UseVisualStyleBackColor = true;
            this.btnLoadJsonFile.Click += new System.EventHandler(this.btnLoadJsonFile_Click);
            // 
            // txtJson
            // 
            this.txtJson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJson.Location = new System.Drawing.Point(160, 64);
            this.txtJson.Multiline = true;
            this.txtJson.Name = "txtJson";
            this.txtJson.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJson.Size = new System.Drawing.Size(1170, 66);
            this.txtJson.TabIndex = 5;
            // 
            // lblFolder
            // 
            this.lblFolder.Location = new System.Drawing.Point(10, 140);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(60, 20);
            this.lblFolder.TabIndex = 5;
            this.lblFolder.Text = "Pasta:";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderPath.Location = new System.Drawing.Point(70, 138);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(1090, 20);
            this.txtFolderPath.TabIndex = 6;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Location = new System.Drawing.Point(1170, 136);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(160, 26);
            this.btnSelectFolder.TabIndex = 6;
            this.btnSelectFolder.Text = "Selecionar pasta...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // lblRules
            // 
            this.lblRules.Location = new System.Drawing.Point(10, 176);
            this.lblRules.Name = "lblRules";
            this.lblRules.Size = new System.Drawing.Size(300, 20);
            this.lblRules.TabIndex = 8;
            this.lblRules.Text = "Regras de Verificação:";
            // 
            // dgvRules
            // 
            this.dgvRules.AllowUserToAddRows = false;
            this.dgvRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRules.Location = new System.Drawing.Point(10, 198);
            this.dgvRules.Name = "dgvRules";
            this.dgvRules.RowTemplate.Height = 24;
            this.dgvRules.Size = new System.Drawing.Size(1320, 170);
            this.dgvRules.TabIndex = 9;
            // 
            // btnAddRule
            // 
            this.btnAddRule.Location = new System.Drawing.Point(10, 374);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(110, 26);
            this.btnAddRule.TabIndex = 7;
            this.btnAddRule.Text = "Adicionar regra";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // btnRemoveRule
            // 
            this.btnRemoveRule.Location = new System.Drawing.Point(130, 374);
            this.btnRemoveRule.Name = "btnRemoveRule";
            this.btnRemoveRule.Size = new System.Drawing.Size(110, 26);
            this.btnRemoveRule.TabIndex = 8;
            this.btnRemoveRule.Text = "Remover regra";
            this.btnRemoveRule.UseVisualStyleBackColor = true;
            this.btnRemoveRule.Click += new System.EventHandler(this.btnRemoveRule_Click);
            // 
            // btnLoadRules
            // 
            this.btnLoadRules.Location = new System.Drawing.Point(250, 374);
            this.btnLoadRules.Name = "btnLoadRules";
            this.btnLoadRules.Size = new System.Drawing.Size(140, 26);
            this.btnLoadRules.TabIndex = 9;
            this.btnLoadRules.Text = "Carregar regras...";
            this.btnLoadRules.UseVisualStyleBackColor = true;
            this.btnLoadRules.Click += new System.EventHandler(this.btnLoadRules_Click);
            // 
            // btnSaveRules
            // 
            this.btnSaveRules.Location = new System.Drawing.Point(400, 374);
            this.btnSaveRules.Name = "btnSaveRules";
            this.btnSaveRules.Size = new System.Drawing.Size(140, 26);
            this.btnSaveRules.TabIndex = 10;
            this.btnSaveRules.Text = "Salvar regras...";
            this.btnSaveRules.UseVisualStyleBackColor = true;
            this.btnSaveRules.Click += new System.EventHandler(this.btnSaveRules_Click);
            // 
            // btnConfigureGeneralSql
            // 
            this.btnConfigureGeneralSql.Location = new System.Drawing.Point(560, 374);
            this.btnConfigureGeneralSql.Name = "btnConfigureGeneralSql";
            this.btnConfigureGeneralSql.Size = new System.Drawing.Size(180, 26);
            this.btnConfigureGeneralSql.TabIndex = 11;
            this.btnConfigureGeneralSql.Text = "Configurar Query Geral...";
            this.btnConfigureGeneralSql.UseVisualStyleBackColor = true;
            this.btnConfigureGeneralSql.Click += new System.EventHandler(this.btnConfigureGeneralSql_Click);
            // 
            // lblGeneralSqlPreview
            // 
            this.lblGeneralSqlPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGeneralSqlPreview.AutoEllipsis = true;
            this.lblGeneralSqlPreview.Location = new System.Drawing.Point(750, 378);
            this.lblGeneralSqlPreview.Name = "lblGeneralSqlPreview";
            this.lblGeneralSqlPreview.Size = new System.Drawing.Size(580, 18);
            this.lblGeneralSqlPreview.TabIndex = 27;
            this.lblGeneralSqlPreview.Text = "Query Geral: não configurada";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(10, 410);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(200, 30);
            this.btnExecute.TabIndex = 12;
            this.btnExecute.Text = "Executar Verificações";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // progressBarValidation
            // 
            this.progressBarValidation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarValidation.Location = new System.Drawing.Point(220, 415);
            this.progressBarValidation.Name = "progressBarValidation";
            this.progressBarValidation.Size = new System.Drawing.Size(1110, 20);
            this.progressBarValidation.TabIndex = 15;
            // 
            // lblResults
            // 
            this.lblResults.Location = new System.Drawing.Point(10, 452);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(300, 20);
            this.lblResults.TabIndex = 16;
            this.lblResults.Text = "Resultados:";
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.Location = new System.Drawing.Point(10, 474);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.Size = new System.Drawing.Size(1320, 234);
            this.dgvResults.TabIndex = 17;
            // 
            // ucValidation
            // 
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.progressBarValidation);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.lblGeneralSqlPreview);
            this.Controls.Add(this.btnConfigureGeneralSql);
            this.Controls.Add(this.btnSaveRules);
            this.Controls.Add(this.btnLoadRules);
            this.Controls.Add(this.btnRemoveRule);
            this.Controls.Add(this.btnAddRule);
            this.Controls.Add(this.dgvRules);
            this.Controls.Add(this.lblRules);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.txtJson);
            this.Controls.Add(this.btnLoadJsonFile);
            this.Controls.Add(this.lblJson);
            this.Controls.Add(this.txtCIDCompany);
            this.Controls.Add(this.lblCIDCompany);
            this.Controls.Add(this.txtCIDBranchInvoice);
            this.Controls.Add(this.lblCIDBranchInvoice);
            this.Controls.Add(this.txtCSerie);
            this.Controls.Add(this.lblCSerie);
            this.Controls.Add(this.txtCIDInvoice);
            this.Controls.Add(this.lblCIDInvoice);
            this.Controls.Add(this.rbBatchFolder);
            this.Controls.Add(this.rbSingleFile);
            this.Name = "ucValidation";
            this.Size = new System.Drawing.Size(1341, 718);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbSingleFile;
        private System.Windows.Forms.RadioButton rbBatchFolder;
        private System.Windows.Forms.Label lblCIDInvoice;
        private System.Windows.Forms.TextBox txtCIDInvoice;
        private System.Windows.Forms.Label lblCSerie;
        private System.Windows.Forms.TextBox txtCSerie;
        private System.Windows.Forms.Label lblCIDBranchInvoice;
        private System.Windows.Forms.TextBox txtCIDBranchInvoice;
        private System.Windows.Forms.Label lblCIDCompany;
        private System.Windows.Forms.TextBox txtCIDCompany;
        private System.Windows.Forms.Label lblJson;
        private System.Windows.Forms.Button btnLoadJsonFile;
        private System.Windows.Forms.TextBox txtJson;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblRules;
        private System.Windows.Forms.DataGridView dgvRules;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnRemoveRule;
        private System.Windows.Forms.Button btnLoadRules;
        private System.Windows.Forms.Button btnSaveRules;
        private System.Windows.Forms.Button btnConfigureGeneralSql;
        private System.Windows.Forms.Label lblGeneralSqlPreview;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ProgressBar progressBarValidation;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.DataGridView dgvResults;
    }
}
