namespace WMTool.Screens
{
    partial class ucJsonReprocessor
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

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            this.lblCIDInvoice = new System.Windows.Forms.Label();
            this.txtCIDInvoice = new System.Windows.Forms.TextBox();
            this.lblCSerie = new System.Windows.Forms.Label();
            this.txtCSerie = new System.Windows.Forms.TextBox();
            this.lblCIDBranchInvoice = new System.Windows.Forms.Label();
            this.txtCIDBranchInvoice = new System.Windows.Forms.TextBox();
            this.lblCIDCompany = new System.Windows.Forms.Label();
            this.txtCIDCompany = new System.Windows.Forms.TextBox();
            this.btnDiscoverParameters = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.progressBarReprocess = new System.Windows.Forms.ProgressBar();
            this.dgvParameterOverrides = new System.Windows.Forms.DataGridView();
            this.dgvDataSources = new System.Windows.Forms.DataGridView();
            this.btnSaveJson = new System.Windows.Forms.Button();
            this.btnValidateJson = new System.Windows.Forms.Button();
            this.btnRefreshDataSources = new System.Windows.Forms.Button();
            this.lblDataSourcesLastUpdated = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtResultJson = new System.Windows.Forms.TextBox();
            this.btnConfigureParameterGeneralQuery = new System.Windows.Forms.Button();
            this.lblParameterGeneralQueryPreview = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameterOverrides)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataSources)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCIDInvoice
            // 
            this.lblCIDInvoice.AutoSize = true;
            this.lblCIDInvoice.Location = new System.Drawing.Point(16, 18);
            this.lblCIDInvoice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCIDInvoice.Name = "lblCIDInvoice";
            this.lblCIDInvoice.Size = new System.Drawing.Size(73, 16);
            this.lblCIDInvoice.TabIndex = 0;
            this.lblCIDInvoice.Text = "cIDInvoice:";
            // 
            // txtCIDInvoice
            // 
            this.txtCIDInvoice.Location = new System.Drawing.Point(120, 15);
            this.txtCIDInvoice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCIDInvoice.Name = "txtCIDInvoice";
            this.txtCIDInvoice.Size = new System.Drawing.Size(159, 22);
            this.txtCIDInvoice.TabIndex = 1;
            // 
            // lblCSerie
            // 
            this.lblCSerie.AutoSize = true;
            this.lblCSerie.Location = new System.Drawing.Point(307, 18);
            this.lblCSerie.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSerie.Name = "lblCSerie";
            this.lblCSerie.Size = new System.Drawing.Size(49, 16);
            this.lblCSerie.TabIndex = 2;
            this.lblCSerie.Text = "cSerie:";
            // 
            // txtCSerie
            // 
            this.txtCSerie.Location = new System.Drawing.Point(369, 15);
            this.txtCSerie.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCSerie.Name = "txtCSerie";
            this.txtCSerie.Size = new System.Drawing.Size(105, 22);
            this.txtCSerie.TabIndex = 3;
            // 
            // lblCIDBranchInvoice
            // 
            this.lblCIDBranchInvoice.AutoSize = true;
            this.lblCIDBranchInvoice.Location = new System.Drawing.Point(500, 18);
            this.lblCIDBranchInvoice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCIDBranchInvoice.Name = "lblCIDBranchInvoice";
            this.lblCIDBranchInvoice.Size = new System.Drawing.Size(115, 16);
            this.lblCIDBranchInvoice.TabIndex = 4;
            this.lblCIDBranchInvoice.Text = "cIDBranchInvoice:";
            // 
            // txtCIDBranchInvoice
            // 
            this.txtCIDBranchInvoice.Location = new System.Drawing.Point(633, 15);
            this.txtCIDBranchInvoice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCIDBranchInvoice.Name = "txtCIDBranchInvoice";
            this.txtCIDBranchInvoice.Size = new System.Drawing.Size(132, 22);
            this.txtCIDBranchInvoice.TabIndex = 5;
            // 
            // lblCIDCompany
            // 
            this.lblCIDCompany.AutoSize = true;
            this.lblCIDCompany.Location = new System.Drawing.Point(787, 18);
            this.lblCIDCompany.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCIDCompany.Name = "lblCIDCompany";
            this.lblCIDCompany.Size = new System.Drawing.Size(88, 16);
            this.lblCIDCompany.TabIndex = 6;
            this.lblCIDCompany.Text = "cIDCompany:";
            // 
            // txtCIDCompany
            // 
            this.txtCIDCompany.Location = new System.Drawing.Point(887, 15);
            this.txtCIDCompany.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCIDCompany.Name = "txtCIDCompany";
            this.txtCIDCompany.Size = new System.Drawing.Size(132, 22);
            this.txtCIDCompany.TabIndex = 7;
            // 
            // btnDiscoverParameters
            // 
            this.btnDiscoverParameters.Location = new System.Drawing.Point(1053, 12);
            this.btnDiscoverParameters.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDiscoverParameters.Name = "btnDiscoverParameters";
            this.btnDiscoverParameters.Size = new System.Drawing.Size(200, 31);
            this.btnDiscoverParameters.TabIndex = 8;
            this.btnDiscoverParameters.Text = "Descobrir Parâmetros";
            this.btnDiscoverParameters.UseVisualStyleBackColor = true;
            this.btnDiscoverParameters.Click += new System.EventHandler(this.btnDiscoverParameters_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(1267, 12);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(160, 31);
            this.btnExecute.TabIndex = 9;
            this.btnExecute.Text = "Executar";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // progressBarReprocess
            // 
            this.progressBarReprocess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarReprocess.Location = new System.Drawing.Point(16, 55);
            this.progressBarReprocess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarReprocess.Name = "progressBarReprocess";
            this.progressBarReprocess.Size = new System.Drawing.Size(1411, 22);
            this.progressBarReprocess.TabIndex = 10;
            // 
            // dgvParameterOverrides
            // 
            this.dgvParameterOverrides.AllowUserToAddRows = false;
            this.dgvParameterOverrides.AllowUserToDeleteRows = false;
            this.dgvParameterOverrides.ColumnHeadersHeight = 29;
            this.dgvParameterOverrides.Location = new System.Drawing.Point(16, 86);
            this.dgvParameterOverrides.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvParameterOverrides.Name = "dgvParameterOverrides";
            this.dgvParameterOverrides.RowHeadersWidth = 25;
            this.dgvParameterOverrides.Size = new System.Drawing.Size(693, 209);
            this.dgvParameterOverrides.TabIndex = 11;
            // 
            // dgvDataSources
            // 
            this.dgvDataSources.AllowUserToAddRows = false;
            this.dgvDataSources.AllowUserToDeleteRows = false;
            this.dgvDataSources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDataSources.ColumnHeadersHeight = 29;
            this.dgvDataSources.Location = new System.Drawing.Point(725, 86);
            this.dgvDataSources.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDataSources.Name = "dgvDataSources";
            this.dgvDataSources.RowHeadersWidth = 25;
            this.dgvDataSources.Size = new System.Drawing.Size(701, 209);
            this.dgvDataSources.TabIndex = 15;
            // 
            // btnSaveJson
            // 
            this.btnSaveJson.Location = new System.Drawing.Point(16, 339);
            this.btnSaveJson.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveJson.Name = "btnSaveJson";
            this.btnSaveJson.Size = new System.Drawing.Size(200, 31);
            this.btnSaveJson.TabIndex = 12;
            this.btnSaveJson.Text = "Salvar JSON...";
            this.btnSaveJson.UseVisualStyleBackColor = true;
            this.btnSaveJson.Click += new System.EventHandler(this.btnSaveJson_Click);
            //
            // btnValidateJson
            //
            this.btnValidateJson.Location = new System.Drawing.Point(232, 339);
            this.btnValidateJson.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnValidateJson.Name = "btnValidateJson";
            this.btnValidateJson.Size = new System.Drawing.Size(200, 31);
            this.btnValidateJson.TabIndex = 18;
            this.btnValidateJson.Text = "Validar JSON";
            this.btnValidateJson.UseVisualStyleBackColor = true;
            this.btnValidateJson.Click += new System.EventHandler(this.btnValidateJson_Click);
            //
            // lblDataSourcesLastUpdated
            //
            this.lblDataSourcesLastUpdated.AutoSize = true;
            this.lblDataSourcesLastUpdated.Location = new System.Drawing.Point(522, 346);
            this.lblDataSourcesLastUpdated.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataSourcesLastUpdated.Name = "lblDataSourcesLastUpdated";
            this.lblDataSourcesLastUpdated.Size = new System.Drawing.Size(180, 16);
            this.lblDataSourcesLastUpdated.TabIndex = 17;
            this.lblDataSourcesLastUpdated.Text = "DataSources: nunca atualizado";
            //
            // btnRefreshDataSources
            // 
            this.btnRefreshDataSources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshDataSources.Location = new System.Drawing.Point(757, 339);
            this.btnRefreshDataSources.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefreshDataSources.Name = "btnRefreshDataSources";
            this.btnRefreshDataSources.Size = new System.Drawing.Size(227, 31);
            this.btnRefreshDataSources.TabIndex = 16;
            this.btnRefreshDataSources.Text = "Atualizar DataSources";
            this.btnRefreshDataSources.UseVisualStyleBackColor = true;
            this.btnRefreshDataSources.Click += new System.EventHandler(this.btnRefreshDataSources_Click);
            // 
            // btnConfigureParameterGeneralQuery
            //
            this.btnConfigureParameterGeneralQuery.Location = new System.Drawing.Point(16, 299);
            this.btnConfigureParameterGeneralQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConfigureParameterGeneralQuery.Name = "btnConfigureParameterGeneralQuery";
            this.btnConfigureParameterGeneralQuery.Size = new System.Drawing.Size(280, 31);
            this.btnConfigureParameterGeneralQuery.TabIndex = 19;
            this.btnConfigureParameterGeneralQuery.Text = "Configurar Query Geral (Parâmetros)...";
            this.btnConfigureParameterGeneralQuery.UseVisualStyleBackColor = true;
            this.btnConfigureParameterGeneralQuery.Click += new System.EventHandler(this.btnConfigureParameterGeneralQuery_Click);
            //
            // lblParameterGeneralQueryPreview
            //
            this.lblParameterGeneralQueryPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParameterGeneralQueryPreview.AutoEllipsis = true;
            this.lblParameterGeneralQueryPreview.Location = new System.Drawing.Point(304, 305);
            this.lblParameterGeneralQueryPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParameterGeneralQueryPreview.Name = "lblParameterGeneralQueryPreview";
            this.lblParameterGeneralQueryPreview.Size = new System.Drawing.Size(1121, 18);
            this.lblParameterGeneralQueryPreview.TabIndex = 20;
            this.lblParameterGeneralQueryPreview.Text = "Query Geral: não configurada";
            //
            // lblResult
            //
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(16, 381);
            this.lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(93, 16);
            this.lblResult.TabIndex = 13;
            this.lblResult.Text = "JSON gerado:";
            // 
            // txtResultJson
            // 
            this.txtResultJson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResultJson.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResultJson.Location = new System.Drawing.Point(16, 403);
            this.txtResultJson.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtResultJson.Multiline = true;
            this.txtResultJson.Name = "txtResultJson";
            this.txtResultJson.ReadOnly = true;
            this.txtResultJson.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResultJson.Size = new System.Drawing.Size(1409, 445);
            this.txtResultJson.TabIndex = 14;
            this.txtResultJson.WordWrap = false;
            // 
            // ucJsonReprocessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtResultJson);
            this.Controls.Add(this.lblParameterGeneralQueryPreview);
            this.Controls.Add(this.btnConfigureParameterGeneralQuery);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnRefreshDataSources);
            this.Controls.Add(this.lblDataSourcesLastUpdated);
            this.Controls.Add(this.btnValidateJson);
            this.Controls.Add(this.btnSaveJson);
            this.Controls.Add(this.dgvDataSources);
            this.Controls.Add(this.dgvParameterOverrides);
            this.Controls.Add(this.progressBarReprocess);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnDiscoverParameters);
            this.Controls.Add(this.txtCIDCompany);
            this.Controls.Add(this.lblCIDCompany);
            this.Controls.Add(this.txtCIDBranchInvoice);
            this.Controls.Add(this.lblCIDBranchInvoice);
            this.Controls.Add(this.txtCSerie);
            this.Controls.Add(this.lblCSerie);
            this.Controls.Add(this.txtCIDInvoice);
            this.Controls.Add(this.lblCIDInvoice);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucJsonReprocessor";
            this.Size = new System.Drawing.Size(1443, 862);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameterOverrides)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataSources)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCIDInvoice;
        private System.Windows.Forms.TextBox txtCIDInvoice;
        private System.Windows.Forms.Label lblCSerie;
        private System.Windows.Forms.TextBox txtCSerie;
        private System.Windows.Forms.Label lblCIDBranchInvoice;
        private System.Windows.Forms.TextBox txtCIDBranchInvoice;
        private System.Windows.Forms.Label lblCIDCompany;
        private System.Windows.Forms.TextBox txtCIDCompany;
        private System.Windows.Forms.Button btnDiscoverParameters;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ProgressBar progressBarReprocess;
        private System.Windows.Forms.DataGridView dgvParameterOverrides;
        private System.Windows.Forms.DataGridView dgvDataSources;
        private System.Windows.Forms.Button btnSaveJson;
        private System.Windows.Forms.Button btnValidateJson;
        private System.Windows.Forms.Button btnRefreshDataSources;
        private System.Windows.Forms.Label lblDataSourcesLastUpdated;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtResultJson;
        private System.Windows.Forms.Button btnConfigureParameterGeneralQuery;
        private System.Windows.Forms.Label lblParameterGeneralQueryPreview;
    }
}
