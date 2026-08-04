namespace WMTool.Screens
{
    partial class frmTableColumnsEditor
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
            this.lblTableName = new System.Windows.Forms.Label();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.btnRemoveColumn = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.SuspendLayout();
            //
            // lblTableName
            //
            this.lblTableName.AutoSize = true;
            this.lblTableName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTableName.Location = new System.Drawing.Point(12, 12);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(70, 15);
            this.lblTableName.TabIndex = 0;
            this.lblTableName.Text = "Tabela:";
            //
            // dgvColumns
            //
            this.dgvColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.AllowUserToDeleteRows = false;
            this.dgvColumns.AutoGenerateColumns = false;
            this.dgvColumns.Location = new System.Drawing.Point(12, 34);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.RowHeadersWidth = 20;
            this.dgvColumns.Size = new System.Drawing.Size(1000, 440);
            this.dgvColumns.TabIndex = 1;
            //
            // btnAddColumn
            //
            this.btnAddColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddColumn.Location = new System.Drawing.Point(12, 484);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(130, 28);
            this.btnAddColumn.TabIndex = 2;
            this.btnAddColumn.Text = "Adicionar Coluna";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
            //
            // btnRemoveColumn
            //
            this.btnRemoveColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveColumn.Location = new System.Drawing.Point(148, 484);
            this.btnRemoveColumn.Name = "btnRemoveColumn";
            this.btnRemoveColumn.Size = new System.Drawing.Size(130, 28);
            this.btnRemoveColumn.TabIndex = 3;
            this.btnRemoveColumn.Text = "Remover Coluna";
            this.btnRemoveColumn.UseVisualStyleBackColor = true;
            this.btnRemoveColumn.Click += new System.EventHandler(this.btnRemoveColumn_Click);
            //
            // btnOk
            //
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(788, 484);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 28);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(894, 484);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // frmTableColumnsEditor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1024, 524);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnRemoveColumn);
            this.Controls.Add(this.btnAddColumn);
            this.Controls.Add(this.dgvColumns);
            this.Controls.Add(this.lblTableName);
            this.MinimizeBox = false;
            this.MaximizeBox = true;
            this.Name = "frmTableColumnsEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Colunas da tabela";
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.Button btnRemoveColumn;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
