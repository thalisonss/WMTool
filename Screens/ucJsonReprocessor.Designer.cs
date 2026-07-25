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
            this.lblResult = new System.Windows.Forms.Label();
            this.txtResultJson = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameterOverrides)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataSources)).BeginInit();
            this.SuspendLayout();
            //
            // lblCIDInvoice
            //
            this.lblCIDInvoice.AutoSize = true;
            this.lblCIDInvoice.Location = new System.Drawing.Point(12, 15);
            this.lblCIDInvoice.Name = "lblCIDInvoice";
            this.lblCIDInvoice.Size = new System.Drawing.Size(63, 13);
            this.lblCIDInvoice.TabIndex = 0;
            this.lblCIDInvoice.Text = "cIDInvoice:";
            //
            // txtCIDInvoice
            //
            this.txtCIDInvoice.Location = new System.Drawing.Point(90, 12);
            this.txtCIDInvoice.Name = "txtCIDInvoice";
            this.txtCIDInvoice.Size = new System.Drawing.Size(120, 20);
            this.txtCIDInvoice.TabIndex = 1;
            //
            // lblCSerie
            //
            this.lblCSerie.AutoSize = true;
            this.lblCSerie.Location = new System.Drawing.Point(230, 15);
            this.lblCSerie.Name = "lblCSerie";
            this.lblCSerie.Size = new System.Drawing.Size(41, 13);
            this.lblCSerie.TabIndex = 2;
            this.lblCSerie.Text = "cSerie:";
            //
            // txtCSerie
            //
            this.txtCSerie.Location = new System.Drawing.Point(277, 12);
            this.txtCSerie.Name = "txtCSerie";
            this.txtCSerie.Size = new System.Drawing.Size(80, 20);
            this.txtCSerie.TabIndex = 3;
            //
            // lblCIDBranchInvoice
            //
            this.lblCIDBranchInvoice.AutoSize = true;
            this.lblCIDBranchInvoice.Location = new System.Drawing.Point(375, 15);
            this.lblCIDBranchInvoice.Name = "lblCIDBranchInvoice";
            this.lblCIDBranchInvoice.Size = new System.Drawing.Size(93, 13);
            this.lblCIDBranchInvoice.TabIndex = 4;
            this.lblCIDBranchInvoice.Text = "cIDBranchInvoice:";
            //
            // txtCIDBranchInvoice
            //
            this.txtCIDBranchInvoice.Location = new System.Drawing.Point(475, 12);
            this.txtCIDBranchInvoice.Name = "txtCIDBranchInvoice";
            this.txtCIDBranchInvoice.Size = new System.Drawing.Size(100, 20);
            this.txtCIDBranchInvoice.TabIndex = 5;
            //
            // lblCIDCompany
            //
            this.lblCIDCompany.AutoSize = true;
            this.lblCIDCompany.Location = new System.Drawing.Point(590, 15);
            this.lblCIDCompany.Name = "lblCIDCompany";
            this.lblCIDCompany.Size = new System.Drawing.Size(69, 13);
            this.lblCIDCompany.TabIndex = 6;
            this.lblCIDCompany.Text = "cIDCompany:";
            //
            // txtCIDCompany
            //
            this.txtCIDCompany.Location = new System.Drawing.Point(665, 12);
            this.txtCIDCompany.Name = "txtCIDCompany";
            this.txtCIDCompany.Size = new System.Drawing.Size(100, 20);
            this.txtCIDCompany.TabIndex = 7;
            //
            // btnDiscoverParameters
            //
            this.btnDiscoverParameters.Location = new System.Drawing.Point(790, 10);
            this.btnDiscoverParameters.Name = "btnDiscoverParameters";
            this.btnDiscoverParameters.Size = new System.Drawing.Size(150, 25);
            this.btnDiscoverParameters.TabIndex = 8;
            this.btnDiscoverParameters.Text = "Descobrir Parâmetros";
            this.btnDiscoverParameters.UseVisualStyleBackColor = true;
            this.btnDiscoverParameters.Click += new System.EventHandler(this.btnDiscoverParameters_Click);
            //
            // btnExecute
            //
            this.btnExecute.Location = new System.Drawing.Point(950, 10);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(120, 25);
            this.btnExecute.TabIndex = 9;
            this.btnExecute.Text = "Executar";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            //
            // progressBarReprocess
            //
            this.progressBarReprocess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right))));
            this.progressBarReprocess.Location = new System.Drawing.Point(12, 45);
            this.progressBarReprocess.Name = "progressBarReprocess";
            this.progressBarReprocess.Size = new System.Drawing.Size(1058, 18);
            this.progressBarReprocess.TabIndex = 10;
            //
            // dgvParameterOverrides
            //
            this.dgvParameterOverrides.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvParameterOverrides.AllowUserToAddRows = false;
            this.dgvParameterOverrides.AllowUserToDeleteRows = false;
            this.dgvParameterOverrides.Location = new System.Drawing.Point(12, 70);
            this.dgvParameterOverrides.Name = "dgvParameterOverrides";
            this.dgvParameterOverrides.RowHeadersWidth = 25;
            this.dgvParameterOverrides.Size = new System.Drawing.Size(520, 170);
            this.dgvParameterOverrides.TabIndex = 11;
            //
            // dgvDataSources
            //
            this.dgvDataSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right))));
            this.dgvDataSources.AllowUserToAddRows = false;
            this.dgvDataSources.AllowUserToDeleteRows = false;
            this.dgvDataSources.Location = new System.Drawing.Point(544, 70);
            this.dgvDataSources.Name = "dgvDataSources";
            this.dgvDataSources.RowHeadersWidth = 25;
            this.dgvDataSources.Size = new System.Drawing.Size(526, 170);
            this.dgvDataSources.TabIndex = 15;
            //
            // btnSaveJson
            //
            this.btnSaveJson.Location = new System.Drawing.Point(12, 248);
            this.btnSaveJson.Name = "btnSaveJson";
            this.btnSaveJson.Size = new System.Drawing.Size(150, 25);
            this.btnSaveJson.TabIndex = 12;
            this.btnSaveJson.Text = "Salvar JSON...";
            this.btnSaveJson.UseVisualStyleBackColor = true;
            this.btnSaveJson.Click += new System.EventHandler(this.btnSaveJson_Click);
            //
            // lblResult
            //
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(12, 282);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(69, 13);
            this.lblResult.TabIndex = 13;
            this.lblResult.Text = "JSON gerado:";
            //
            // txtResultJson
            //
            this.txtResultJson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResultJson.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResultJson.Location = new System.Drawing.Point(12, 300);
            this.txtResultJson.Multiline = true;
            this.txtResultJson.Name = "txtResultJson";
            this.txtResultJson.ReadOnly = true;
            this.txtResultJson.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResultJson.Size = new System.Drawing.Size(1058, 390);
            this.txtResultJson.TabIndex = 14;
            this.txtResultJson.WordWrap = false;
            //
            // ucJsonReprocessor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtResultJson);
            this.Controls.Add(this.lblResult);
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
            this.Name = "ucJsonReprocessor";
            this.Size = new System.Drawing.Size(1082, 700);
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
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtResultJson;
    }
}
