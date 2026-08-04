namespace WMTool.Screens
{
    partial class ucInsertScriptGenerator
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
            this.lblJson = new System.Windows.Forms.Label();
            this.btnLoadJsonFile = new System.Windows.Forms.Button();
            this.txtJson = new System.Windows.Forms.TextBox();
            this.lblParameters = new System.Windows.Forms.Label();
            this.dgvParameters = new System.Windows.Forms.DataGridView();
            this.btnAddParameter = new System.Windows.Forms.Button();
            this.btnRemoveParameter = new System.Windows.Forms.Button();
            this.lblTables = new System.Windows.Forms.Label();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.btnAddTable = new System.Windows.Forms.Button();
            this.btnRemoveTable = new System.Windows.Forms.Button();
            this.btnSaveRules = new System.Windows.Forms.Button();
            this.btnLoadRules = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.progressBarInsertScript = new System.Windows.Forms.ProgressBar();
            this.lblResultSql = new System.Windows.Forms.Label();
            this.txtResultSql = new System.Windows.Forms.TextBox();
            this.btnSaveScript = new System.Windows.Forms.Button();
            this.lblWarnings = new System.Windows.Forms.Label();
            this.txtWarnings = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.SuspendLayout();
            //
            // lblJson
            //
            this.lblJson.Location = new System.Drawing.Point(10, 10);
            this.lblJson.Name = "lblJson";
            this.lblJson.Size = new System.Drawing.Size(400, 20);
            this.lblJson.TabIndex = 0;
            this.lblJson.Text = "Cole o JSON ou carregue um arquivo:";
            //
            // btnLoadJsonFile
            //
            this.btnLoadJsonFile.Location = new System.Drawing.Point(10, 34);
            this.btnLoadJsonFile.Name = "btnLoadJsonFile";
            this.btnLoadJsonFile.Size = new System.Drawing.Size(160, 26);
            this.btnLoadJsonFile.TabIndex = 1;
            this.btnLoadJsonFile.Text = "Carregar arquivo...";
            this.btnLoadJsonFile.UseVisualStyleBackColor = true;
            this.btnLoadJsonFile.Click += new System.EventHandler(this.btnLoadJsonFile_Click);
            //
            // txtJson
            //
            this.txtJson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJson.Location = new System.Drawing.Point(180, 34);
            this.txtJson.Multiline = true;
            this.txtJson.Name = "txtJson";
            this.txtJson.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJson.Size = new System.Drawing.Size(1145, 50);
            this.txtJson.TabIndex = 2;
            //
            // lblParameters
            //
            this.lblParameters.Location = new System.Drawing.Point(10, 96);
            this.lblParameters.Name = "lblParameters";
            this.lblParameters.Size = new System.Drawing.Size(400, 18);
            this.lblParameters.TabIndex = 3;
            this.lblParameters.Text = "Parâmetros (Localizar/Substituir):";
            //
            // dgvParameters
            //
            this.dgvParameters.AllowUserToAddRows = false;
            this.dgvParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvParameters.Location = new System.Drawing.Point(10, 116);
            this.dgvParameters.Name = "dgvParameters";
            this.dgvParameters.RowTemplate.Height = 24;
            this.dgvParameters.Size = new System.Drawing.Size(1315, 90);
            this.dgvParameters.TabIndex = 4;
            //
            // btnAddParameter
            //
            this.btnAddParameter.Location = new System.Drawing.Point(10, 210);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(160, 26);
            this.btnAddParameter.TabIndex = 5;
            this.btnAddParameter.Text = "Adicionar Parâmetro";
            this.btnAddParameter.UseVisualStyleBackColor = true;
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            //
            // btnRemoveParameter
            //
            this.btnRemoveParameter.Location = new System.Drawing.Point(180, 210);
            this.btnRemoveParameter.Name = "btnRemoveParameter";
            this.btnRemoveParameter.Size = new System.Drawing.Size(160, 26);
            this.btnRemoveParameter.TabIndex = 6;
            this.btnRemoveParameter.Text = "Remover Parâmetro";
            this.btnRemoveParameter.UseVisualStyleBackColor = true;
            this.btnRemoveParameter.Click += new System.EventHandler(this.btnRemoveParameter_Click);
            //
            // lblTables
            //
            this.lblTables.Location = new System.Drawing.Point(10, 246);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(300, 18);
            this.lblTables.TabIndex = 7;
            this.lblTables.Text = "Tabelas a Inserir:";
            //
            // dgvTables
            //
            this.dgvTables.AllowUserToAddRows = false;
            this.dgvTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTables.Location = new System.Drawing.Point(10, 266);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.RowTemplate.Height = 24;
            this.dgvTables.Size = new System.Drawing.Size(1315, 140);
            this.dgvTables.TabIndex = 8;
            //
            // btnAddTable
            //
            this.btnAddTable.Location = new System.Drawing.Point(10, 410);
            this.btnAddTable.Name = "btnAddTable";
            this.btnAddTable.Size = new System.Drawing.Size(140, 26);
            this.btnAddTable.TabIndex = 9;
            this.btnAddTable.Text = "Adicionar Tabela";
            this.btnAddTable.UseVisualStyleBackColor = true;
            this.btnAddTable.Click += new System.EventHandler(this.btnAddTable_Click);
            //
            // btnRemoveTable
            //
            this.btnRemoveTable.Location = new System.Drawing.Point(160, 410);
            this.btnRemoveTable.Name = "btnRemoveTable";
            this.btnRemoveTable.Size = new System.Drawing.Size(140, 26);
            this.btnRemoveTable.TabIndex = 10;
            this.btnRemoveTable.Text = "Remover Tabela";
            this.btnRemoveTable.UseVisualStyleBackColor = true;
            this.btnRemoveTable.Click += new System.EventHandler(this.btnRemoveTable_Click);
            //
            // btnSaveRules
            //
            this.btnSaveRules.Location = new System.Drawing.Point(320, 410);
            this.btnSaveRules.Name = "btnSaveRules";
            this.btnSaveRules.Size = new System.Drawing.Size(140, 26);
            this.btnSaveRules.TabIndex = 11;
            this.btnSaveRules.Text = "Salvar Regras...";
            this.btnSaveRules.UseVisualStyleBackColor = true;
            this.btnSaveRules.Click += new System.EventHandler(this.btnSaveRules_Click);
            //
            // btnLoadRules
            //
            this.btnLoadRules.Location = new System.Drawing.Point(470, 410);
            this.btnLoadRules.Name = "btnLoadRules";
            this.btnLoadRules.Size = new System.Drawing.Size(140, 26);
            this.btnLoadRules.TabIndex = 12;
            this.btnLoadRules.Text = "Carregar Regras...";
            this.btnLoadRules.UseVisualStyleBackColor = true;
            this.btnLoadRules.Click += new System.EventHandler(this.btnLoadRules_Click);
            //
            // btnGenerate
            //
            this.btnGenerate.Location = new System.Drawing.Point(10, 446);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(200, 30);
            this.btnGenerate.TabIndex = 13;
            this.btnGenerate.Text = "Gerar Script INSERT";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            //
            // progressBarInsertScript
            //
            this.progressBarInsertScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarInsertScript.Location = new System.Drawing.Point(220, 451);
            this.progressBarInsertScript.Name = "progressBarInsertScript";
            this.progressBarInsertScript.Size = new System.Drawing.Size(1105, 20);
            this.progressBarInsertScript.TabIndex = 14;
            //
            // lblResultSql
            //
            this.lblResultSql.Location = new System.Drawing.Point(10, 484);
            this.lblResultSql.Name = "lblResultSql";
            this.lblResultSql.Size = new System.Drawing.Size(300, 18);
            this.lblResultSql.TabIndex = 15;
            this.lblResultSql.Text = "Script SQL Gerado:";
            //
            // txtResultSql
            //
            this.txtResultSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResultSql.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResultSql.Location = new System.Drawing.Point(10, 504);
            this.txtResultSql.Multiline = true;
            this.txtResultSql.Name = "txtResultSql";
            this.txtResultSql.ReadOnly = true;
            this.txtResultSql.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResultSql.Size = new System.Drawing.Size(895, 170);
            this.txtResultSql.TabIndex = 16;
            this.txtResultSql.WordWrap = false;
            //
            // btnSaveScript
            //
            this.btnSaveScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveScript.Location = new System.Drawing.Point(10, 680);
            this.btnSaveScript.Name = "btnSaveScript";
            this.btnSaveScript.Size = new System.Drawing.Size(180, 26);
            this.btnSaveScript.TabIndex = 17;
            this.btnSaveScript.Text = "Salvar Script (.sql)...";
            this.btnSaveScript.UseVisualStyleBackColor = true;
            this.btnSaveScript.Click += new System.EventHandler(this.btnSaveScript_Click);
            //
            // lblWarnings
            //
            this.lblWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWarnings.Location = new System.Drawing.Point(920, 484);
            this.lblWarnings.Name = "lblWarnings";
            this.lblWarnings.Size = new System.Drawing.Size(300, 18);
            this.lblWarnings.TabIndex = 18;
            this.lblWarnings.Text = "Avisos:";
            //
            // txtWarnings
            //
            this.txtWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWarnings.Location = new System.Drawing.Point(920, 504);
            this.txtWarnings.Multiline = true;
            this.txtWarnings.Name = "txtWarnings";
            this.txtWarnings.ReadOnly = true;
            this.txtWarnings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWarnings.Size = new System.Drawing.Size(405, 202);
            this.txtWarnings.TabIndex = 19;
            //
            // ucInsertScriptGenerator
            //
            this.Controls.Add(this.txtWarnings);
            this.Controls.Add(this.lblWarnings);
            this.Controls.Add(this.btnSaveScript);
            this.Controls.Add(this.txtResultSql);
            this.Controls.Add(this.lblResultSql);
            this.Controls.Add(this.progressBarInsertScript);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnLoadRules);
            this.Controls.Add(this.btnSaveRules);
            this.Controls.Add(this.btnRemoveTable);
            this.Controls.Add(this.btnAddTable);
            this.Controls.Add(this.dgvTables);
            this.Controls.Add(this.lblTables);
            this.Controls.Add(this.btnRemoveParameter);
            this.Controls.Add(this.btnAddParameter);
            this.Controls.Add(this.dgvParameters);
            this.Controls.Add(this.lblParameters);
            this.Controls.Add(this.txtJson);
            this.Controls.Add(this.btnLoadJsonFile);
            this.Controls.Add(this.lblJson);
            this.Name = "ucInsertScriptGenerator";
            this.Size = new System.Drawing.Size(1335, 714);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJson;
        private System.Windows.Forms.Button btnLoadJsonFile;
        private System.Windows.Forms.TextBox txtJson;
        private System.Windows.Forms.Label lblParameters;
        private System.Windows.Forms.DataGridView dgvParameters;
        private System.Windows.Forms.Button btnAddParameter;
        private System.Windows.Forms.Button btnRemoveParameter;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.DataGridView dgvTables;
        private System.Windows.Forms.Button btnAddTable;
        private System.Windows.Forms.Button btnRemoveTable;
        private System.Windows.Forms.Button btnSaveRules;
        private System.Windows.Forms.Button btnLoadRules;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ProgressBar progressBarInsertScript;
        private System.Windows.Forms.Label lblResultSql;
        private System.Windows.Forms.TextBox txtResultSql;
        private System.Windows.Forms.Button btnSaveScript;
        private System.Windows.Forms.Label lblWarnings;
        private System.Windows.Forms.TextBox txtWarnings;
    }
}
