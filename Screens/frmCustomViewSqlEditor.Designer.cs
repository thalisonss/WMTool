namespace WMTool.Screens
{
    partial class frmCustomViewSqlEditor
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
            this.lblViewName = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.txtSql = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRemoveOverride = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblViewName
            //
            this.lblViewName.AutoSize = true;
            this.lblViewName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblViewName.Location = new System.Drawing.Point(12, 12);
            this.lblViewName.Name = "lblViewName";
            this.lblViewName.Size = new System.Drawing.Size(35, 15);
            this.lblViewName.TabIndex = 0;
            this.lblViewName.Text = "View:";
            //
            // lblInstructions
            //
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(12, 34);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(300, 13);
            this.lblInstructions.TabIndex = 1;
            this.lblInstructions.Text = "SQL customizado (use @nomeDoParametro). Deixe em branco para remover a customização.";
            //
            // txtSql
            //
            this.txtSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSql.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtSql.Location = new System.Drawing.Point(12, 53);
            this.txtSql.Multiline = true;
            this.txtSql.Name = "txtSql";
            this.txtSql.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSql.Size = new System.Drawing.Size(860, 470);
            this.txtSql.TabIndex = 2;
            this.txtSql.WordWrap = false;
            //
            // btnSave
            //
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(566, 532);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnRemoveOverride
            //
            this.btnRemoveOverride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveOverride.Location = new System.Drawing.Point(672, 532);
            this.btnRemoveOverride.Name = "btnRemoveOverride";
            this.btnRemoveOverride.Size = new System.Drawing.Size(130, 28);
            this.btnRemoveOverride.TabIndex = 4;
            this.btnRemoveOverride.Text = "Remover Customização";
            this.btnRemoveOverride.UseVisualStyleBackColor = true;
            this.btnRemoveOverride.Click += new System.EventHandler(this.btnRemoveOverride_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(772, 532);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // frmCustomViewSqlEditor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(884, 572);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemoveOverride);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSql);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.lblViewName);
            this.MinimizeBox = false;
            this.MaximizeBox = true;
            this.Name = "frmCustomViewSqlEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Query customizada da view";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblViewName;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.TextBox txtSql;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRemoveOverride;
        private System.Windows.Forms.Button btnCancel;
    }
}
