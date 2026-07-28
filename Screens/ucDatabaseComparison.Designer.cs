namespace WMTool.Screens
{
    partial class ucDatabaseComparison
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
            this.tabControlRuleTypes = new System.Windows.Forms.TabControl();
            this.tabPageSingleValue = new System.Windows.Forms.TabPage();
            this.btnConfigureGeneralOrigin = new System.Windows.Forms.Button();
            this.lblGeneralOriginPreview = new System.Windows.Forms.Label();
            this.btnConfigureGeneralDestination = new System.Windows.Forms.Button();
            this.lblGeneralDestinationPreview = new System.Windows.Forms.Label();
            this.dgvRules = new System.Windows.Forms.DataGridView();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnRemoveRule = new System.Windows.Forms.Button();
            this.tabPageRowSet = new System.Windows.Forms.TabPage();
            this.btnConfigureGeneralOriginRowSet = new System.Windows.Forms.Button();
            this.lblGeneralOriginRowSetPreview = new System.Windows.Forms.Label();
            this.btnConfigureGeneralDestinationRowSet = new System.Windows.Forms.Button();
            this.lblGeneralDestinationRowSetPreview = new System.Windows.Forms.Label();
            this.dgvRowSetRules = new System.Windows.Forms.DataGridView();
            this.btnAddRowSetRule = new System.Windows.Forms.Button();
            this.btnRemoveRowSetRule = new System.Windows.Forms.Button();
            this.tabPagePresence = new System.Windows.Forms.TabPage();
            this.dgvPresenceRules = new System.Windows.Forms.DataGridView();
            this.btnAddPresenceRule = new System.Windows.Forms.Button();
            this.btnRemovePresenceRule = new System.Windows.Forms.Button();
            this.btnLoadRules = new System.Windows.Forms.Button();
            this.btnSaveRules = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.progressBarComparison = new System.Windows.Forms.ProgressBar();
            this.lblResults = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.tabControlRuleTypes.SuspendLayout();
            this.tabPageSingleValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).BeginInit();
            this.tabPageRowSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRowSetRules)).BeginInit();
            this.tabPagePresence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresenceRules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            //
            // lblCIDInvoice
            //
            this.lblCIDInvoice.AutoSize = true;
            this.lblCIDInvoice.Location = new System.Drawing.Point(10, 12);
            this.lblCIDInvoice.Name = "lblCIDInvoice";
            this.lblCIDInvoice.Size = new System.Drawing.Size(63, 13);
            this.lblCIDInvoice.TabIndex = 0;
            this.lblCIDInvoice.Text = "cIDInvoice:";
            //
            // txtCIDInvoice
            //
            this.txtCIDInvoice.Location = new System.Drawing.Point(80, 9);
            this.txtCIDInvoice.Name = "txtCIDInvoice";
            this.txtCIDInvoice.Size = new System.Drawing.Size(100, 20);
            this.txtCIDInvoice.TabIndex = 1;
            //
            // lblCSerie
            //
            this.lblCSerie.AutoSize = true;
            this.lblCSerie.Location = new System.Drawing.Point(190, 12);
            this.lblCSerie.Name = "lblCSerie";
            this.lblCSerie.Size = new System.Drawing.Size(41, 13);
            this.lblCSerie.TabIndex = 2;
            this.lblCSerie.Text = "cSerie:";
            //
            // txtCSerie
            //
            this.txtCSerie.Location = new System.Drawing.Point(240, 9);
            this.txtCSerie.Name = "txtCSerie";
            this.txtCSerie.Size = new System.Drawing.Size(80, 20);
            this.txtCSerie.TabIndex = 3;
            //
            // lblCIDBranchInvoice
            //
            this.lblCIDBranchInvoice.AutoSize = true;
            this.lblCIDBranchInvoice.Location = new System.Drawing.Point(330, 12);
            this.lblCIDBranchInvoice.Name = "lblCIDBranchInvoice";
            this.lblCIDBranchInvoice.Size = new System.Drawing.Size(93, 13);
            this.lblCIDBranchInvoice.TabIndex = 4;
            this.lblCIDBranchInvoice.Text = "cIDBranchInvoice:";
            //
            // txtCIDBranchInvoice
            //
            this.txtCIDBranchInvoice.Location = new System.Drawing.Point(430, 9);
            this.txtCIDBranchInvoice.Name = "txtCIDBranchInvoice";
            this.txtCIDBranchInvoice.Size = new System.Drawing.Size(100, 20);
            this.txtCIDBranchInvoice.TabIndex = 5;
            //
            // lblCIDCompany
            //
            this.lblCIDCompany.AutoSize = true;
            this.lblCIDCompany.Location = new System.Drawing.Point(540, 12);
            this.lblCIDCompany.Name = "lblCIDCompany";
            this.lblCIDCompany.Size = new System.Drawing.Size(69, 13);
            this.lblCIDCompany.TabIndex = 6;
            this.lblCIDCompany.Text = "cIDCompany:";
            //
            // txtCIDCompany
            //
            this.txtCIDCompany.Location = new System.Drawing.Point(610, 9);
            this.txtCIDCompany.Name = "txtCIDCompany";
            this.txtCIDCompany.Size = new System.Drawing.Size(100, 20);
            this.txtCIDCompany.TabIndex = 7;
            //
            // tabControlRuleTypes
            //
            this.tabControlRuleTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlRuleTypes.Controls.Add(this.tabPageSingleValue);
            this.tabControlRuleTypes.Controls.Add(this.tabPageRowSet);
            this.tabControlRuleTypes.Controls.Add(this.tabPagePresence);
            this.tabControlRuleTypes.Location = new System.Drawing.Point(10, 40);
            this.tabControlRuleTypes.Name = "tabControlRuleTypes";
            this.tabControlRuleTypes.SelectedIndex = 0;
            this.tabControlRuleTypes.Size = new System.Drawing.Size(1320, 260);
            this.tabControlRuleTypes.TabIndex = 8;
            //
            // tabPageSingleValue
            //
            this.tabPageSingleValue.Controls.Add(this.btnConfigureGeneralOrigin);
            this.tabPageSingleValue.Controls.Add(this.lblGeneralOriginPreview);
            this.tabPageSingleValue.Controls.Add(this.btnConfigureGeneralDestination);
            this.tabPageSingleValue.Controls.Add(this.lblGeneralDestinationPreview);
            this.tabPageSingleValue.Controls.Add(this.dgvRules);
            this.tabPageSingleValue.Controls.Add(this.btnAddRule);
            this.tabPageSingleValue.Controls.Add(this.btnRemoveRule);
            this.tabPageSingleValue.Location = new System.Drawing.Point(4, 22);
            this.tabPageSingleValue.Name = "tabPageSingleValue";
            this.tabPageSingleValue.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSingleValue.Size = new System.Drawing.Size(1312, 234);
            this.tabPageSingleValue.TabIndex = 0;
            this.tabPageSingleValue.Text = "Valor único";
            this.tabPageSingleValue.UseVisualStyleBackColor = true;
            //
            // btnConfigureGeneralOrigin
            //
            this.btnConfigureGeneralOrigin.Location = new System.Drawing.Point(8, 8);
            this.btnConfigureGeneralOrigin.Name = "btnConfigureGeneralOrigin";
            this.btnConfigureGeneralOrigin.Size = new System.Drawing.Size(220, 26);
            this.btnConfigureGeneralOrigin.TabIndex = 0;
            this.btnConfigureGeneralOrigin.Text = "Configurar Query Geral (Origem)...";
            this.btnConfigureGeneralOrigin.UseVisualStyleBackColor = true;
            this.btnConfigureGeneralOrigin.Click += new System.EventHandler(this.btnConfigureGeneralOrigin_Click);
            //
            // lblGeneralOriginPreview
            //
            this.lblGeneralOriginPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGeneralOriginPreview.AutoEllipsis = true;
            this.lblGeneralOriginPreview.Location = new System.Drawing.Point(236, 12);
            this.lblGeneralOriginPreview.Name = "lblGeneralOriginPreview";
            this.lblGeneralOriginPreview.Size = new System.Drawing.Size(1060, 18);
            this.lblGeneralOriginPreview.TabIndex = 1;
            this.lblGeneralOriginPreview.Text = "Query Geral (Origem): não configurada";
            //
            // btnConfigureGeneralDestination
            //
            this.btnConfigureGeneralDestination.Location = new System.Drawing.Point(8, 40);
            this.btnConfigureGeneralDestination.Name = "btnConfigureGeneralDestination";
            this.btnConfigureGeneralDestination.Size = new System.Drawing.Size(220, 26);
            this.btnConfigureGeneralDestination.TabIndex = 2;
            this.btnConfigureGeneralDestination.Text = "Configurar Query Geral (Destino)...";
            this.btnConfigureGeneralDestination.UseVisualStyleBackColor = true;
            this.btnConfigureGeneralDestination.Click += new System.EventHandler(this.btnConfigureGeneralDestination_Click);
            //
            // lblGeneralDestinationPreview
            //
            this.lblGeneralDestinationPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGeneralDestinationPreview.AutoEllipsis = true;
            this.lblGeneralDestinationPreview.Location = new System.Drawing.Point(236, 44);
            this.lblGeneralDestinationPreview.Name = "lblGeneralDestinationPreview";
            this.lblGeneralDestinationPreview.Size = new System.Drawing.Size(1060, 18);
            this.lblGeneralDestinationPreview.TabIndex = 3;
            this.lblGeneralDestinationPreview.Text = "Query Geral (Destino): não configurada";
            //
            // dgvRules
            //
            this.dgvRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRules.AllowUserToAddRows = false;
            this.dgvRules.Location = new System.Drawing.Point(8, 72);
            this.dgvRules.Name = "dgvRules";
            this.dgvRules.RowTemplate.Height = 24;
            this.dgvRules.Size = new System.Drawing.Size(1296, 120);
            this.dgvRules.TabIndex = 4;
            //
            // btnAddRule
            //
            this.btnAddRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddRule.Location = new System.Drawing.Point(8, 196);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(110, 26);
            this.btnAddRule.TabIndex = 5;
            this.btnAddRule.Text = "Adicionar regra";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            //
            // btnRemoveRule
            //
            this.btnRemoveRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveRule.Location = new System.Drawing.Point(124, 196);
            this.btnRemoveRule.Name = "btnRemoveRule";
            this.btnRemoveRule.Size = new System.Drawing.Size(110, 26);
            this.btnRemoveRule.TabIndex = 6;
            this.btnRemoveRule.Text = "Remover regra";
            this.btnRemoveRule.UseVisualStyleBackColor = true;
            this.btnRemoveRule.Click += new System.EventHandler(this.btnRemoveRule_Click);
            //
            // tabPageRowSet
            //
            this.tabPageRowSet.Controls.Add(this.btnConfigureGeneralOriginRowSet);
            this.tabPageRowSet.Controls.Add(this.lblGeneralOriginRowSetPreview);
            this.tabPageRowSet.Controls.Add(this.btnConfigureGeneralDestinationRowSet);
            this.tabPageRowSet.Controls.Add(this.lblGeneralDestinationRowSetPreview);
            this.tabPageRowSet.Controls.Add(this.dgvRowSetRules);
            this.tabPageRowSet.Controls.Add(this.btnAddRowSetRule);
            this.tabPageRowSet.Controls.Add(this.btnRemoveRowSetRule);
            this.tabPageRowSet.Location = new System.Drawing.Point(4, 22);
            this.tabPageRowSet.Name = "tabPageRowSet";
            this.tabPageRowSet.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRowSet.Size = new System.Drawing.Size(1312, 234);
            this.tabPageRowSet.TabIndex = 1;
            this.tabPageRowSet.Text = "N itens (linha-a-linha)";
            this.tabPageRowSet.UseVisualStyleBackColor = true;
            //
            // btnConfigureGeneralOriginRowSet
            //
            this.btnConfigureGeneralOriginRowSet.Location = new System.Drawing.Point(8, 8);
            this.btnConfigureGeneralOriginRowSet.Name = "btnConfigureGeneralOriginRowSet";
            this.btnConfigureGeneralOriginRowSet.Size = new System.Drawing.Size(240, 26);
            this.btnConfigureGeneralOriginRowSet.TabIndex = 0;
            this.btnConfigureGeneralOriginRowSet.Text = "Configurar Query Geral Conjunto (Origem)...";
            this.btnConfigureGeneralOriginRowSet.UseVisualStyleBackColor = true;
            this.btnConfigureGeneralOriginRowSet.Click += new System.EventHandler(this.btnConfigureGeneralOriginRowSet_Click);
            //
            // lblGeneralOriginRowSetPreview
            //
            this.lblGeneralOriginRowSetPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGeneralOriginRowSetPreview.AutoEllipsis = true;
            this.lblGeneralOriginRowSetPreview.Location = new System.Drawing.Point(256, 12);
            this.lblGeneralOriginRowSetPreview.Name = "lblGeneralOriginRowSetPreview";
            this.lblGeneralOriginRowSetPreview.Size = new System.Drawing.Size(1040, 18);
            this.lblGeneralOriginRowSetPreview.TabIndex = 1;
            this.lblGeneralOriginRowSetPreview.Text = "Query Geral Conjunto (Origem): não configurada";
            //
            // btnConfigureGeneralDestinationRowSet
            //
            this.btnConfigureGeneralDestinationRowSet.Location = new System.Drawing.Point(8, 40);
            this.btnConfigureGeneralDestinationRowSet.Name = "btnConfigureGeneralDestinationRowSet";
            this.btnConfigureGeneralDestinationRowSet.Size = new System.Drawing.Size(240, 26);
            this.btnConfigureGeneralDestinationRowSet.TabIndex = 2;
            this.btnConfigureGeneralDestinationRowSet.Text = "Configurar Query Geral Conjunto (Destino)...";
            this.btnConfigureGeneralDestinationRowSet.UseVisualStyleBackColor = true;
            this.btnConfigureGeneralDestinationRowSet.Click += new System.EventHandler(this.btnConfigureGeneralDestinationRowSet_Click);
            //
            // lblGeneralDestinationRowSetPreview
            //
            this.lblGeneralDestinationRowSetPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGeneralDestinationRowSetPreview.AutoEllipsis = true;
            this.lblGeneralDestinationRowSetPreview.Location = new System.Drawing.Point(256, 44);
            this.lblGeneralDestinationRowSetPreview.Name = "lblGeneralDestinationRowSetPreview";
            this.lblGeneralDestinationRowSetPreview.Size = new System.Drawing.Size(1040, 18);
            this.lblGeneralDestinationRowSetPreview.TabIndex = 3;
            this.lblGeneralDestinationRowSetPreview.Text = "Query Geral Conjunto (Destino): não configurada";
            //
            // dgvRowSetRules
            //
            this.dgvRowSetRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRowSetRules.AllowUserToAddRows = false;
            this.dgvRowSetRules.Location = new System.Drawing.Point(8, 72);
            this.dgvRowSetRules.Name = "dgvRowSetRules";
            this.dgvRowSetRules.RowTemplate.Height = 24;
            this.dgvRowSetRules.Size = new System.Drawing.Size(1296, 120);
            this.dgvRowSetRules.TabIndex = 4;
            //
            // btnAddRowSetRule
            //
            this.btnAddRowSetRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddRowSetRule.Location = new System.Drawing.Point(8, 196);
            this.btnAddRowSetRule.Name = "btnAddRowSetRule";
            this.btnAddRowSetRule.Size = new System.Drawing.Size(110, 26);
            this.btnAddRowSetRule.TabIndex = 5;
            this.btnAddRowSetRule.Text = "Adicionar regra";
            this.btnAddRowSetRule.UseVisualStyleBackColor = true;
            this.btnAddRowSetRule.Click += new System.EventHandler(this.btnAddRowSetRule_Click);
            //
            // btnRemoveRowSetRule
            //
            this.btnRemoveRowSetRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveRowSetRule.Location = new System.Drawing.Point(124, 196);
            this.btnRemoveRowSetRule.Name = "btnRemoveRowSetRule";
            this.btnRemoveRowSetRule.Size = new System.Drawing.Size(110, 26);
            this.btnRemoveRowSetRule.TabIndex = 6;
            this.btnRemoveRowSetRule.Text = "Remover regra";
            this.btnRemoveRowSetRule.UseVisualStyleBackColor = true;
            this.btnRemoveRowSetRule.Click += new System.EventHandler(this.btnRemoveRowSetRule_Click);
            //
            // tabPagePresence
            //
            this.tabPagePresence.Controls.Add(this.dgvPresenceRules);
            this.tabPagePresence.Controls.Add(this.btnAddPresenceRule);
            this.tabPagePresence.Controls.Add(this.btnRemovePresenceRule);
            this.tabPagePresence.Location = new System.Drawing.Point(4, 22);
            this.tabPagePresence.Name = "tabPagePresence";
            this.tabPagePresence.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePresence.Size = new System.Drawing.Size(1312, 234);
            this.tabPagePresence.TabIndex = 2;
            this.tabPagePresence.Text = "Presença / Ausência";
            this.tabPagePresence.UseVisualStyleBackColor = true;
            //
            // dgvPresenceRules
            //
            this.dgvPresenceRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPresenceRules.AllowUserToAddRows = false;
            this.dgvPresenceRules.Location = new System.Drawing.Point(8, 8);
            this.dgvPresenceRules.Name = "dgvPresenceRules";
            this.dgvPresenceRules.RowTemplate.Height = 24;
            this.dgvPresenceRules.Size = new System.Drawing.Size(1296, 184);
            this.dgvPresenceRules.TabIndex = 0;
            //
            // btnAddPresenceRule
            //
            this.btnAddPresenceRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddPresenceRule.Location = new System.Drawing.Point(8, 198);
            this.btnAddPresenceRule.Name = "btnAddPresenceRule";
            this.btnAddPresenceRule.Size = new System.Drawing.Size(110, 26);
            this.btnAddPresenceRule.TabIndex = 1;
            this.btnAddPresenceRule.Text = "Adicionar regra";
            this.btnAddPresenceRule.UseVisualStyleBackColor = true;
            this.btnAddPresenceRule.Click += new System.EventHandler(this.btnAddPresenceRule_Click);
            //
            // btnRemovePresenceRule
            //
            this.btnRemovePresenceRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemovePresenceRule.Location = new System.Drawing.Point(124, 198);
            this.btnRemovePresenceRule.Name = "btnRemovePresenceRule";
            this.btnRemovePresenceRule.Size = new System.Drawing.Size(110, 26);
            this.btnRemovePresenceRule.TabIndex = 2;
            this.btnRemovePresenceRule.Text = "Remover regra";
            this.btnRemovePresenceRule.UseVisualStyleBackColor = true;
            this.btnRemovePresenceRule.Click += new System.EventHandler(this.btnRemovePresenceRule_Click);
            //
            // btnLoadRules
            //
            this.btnLoadRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadRules.Location = new System.Drawing.Point(10, 308);
            this.btnLoadRules.Name = "btnLoadRules";
            this.btnLoadRules.Size = new System.Drawing.Size(140, 26);
            this.btnLoadRules.TabIndex = 9;
            this.btnLoadRules.Text = "Carregar regras...";
            this.btnLoadRules.UseVisualStyleBackColor = true;
            this.btnLoadRules.Click += new System.EventHandler(this.btnLoadRules_Click);
            //
            // btnSaveRules
            //
            this.btnSaveRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveRules.Location = new System.Drawing.Point(160, 308);
            this.btnSaveRules.Name = "btnSaveRules";
            this.btnSaveRules.Size = new System.Drawing.Size(140, 26);
            this.btnSaveRules.TabIndex = 10;
            this.btnSaveRules.Text = "Salvar regras...";
            this.btnSaveRules.UseVisualStyleBackColor = true;
            this.btnSaveRules.Click += new System.EventHandler(this.btnSaveRules_Click);
            //
            // btnExecute
            //
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecute.Location = new System.Drawing.Point(10, 344);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(200, 30);
            this.btnExecute.TabIndex = 11;
            this.btnExecute.Text = "Executar Comparação";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            //
            // progressBarComparison
            //
            this.progressBarComparison.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarComparison.Location = new System.Drawing.Point(220, 349);
            this.progressBarComparison.Name = "progressBarComparison";
            this.progressBarComparison.Size = new System.Drawing.Size(1110, 20);
            this.progressBarComparison.TabIndex = 12;
            //
            // lblResults
            //
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(10, 386);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(65, 13);
            this.lblResults.TabIndex = 13;
            this.lblResults.Text = "Resultados:";
            //
            // dgvResults
            //
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Location = new System.Drawing.Point(10, 408);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.Size = new System.Drawing.Size(1320, 300);
            this.dgvResults.TabIndex = 14;
            //
            // ucDatabaseComparison
            //
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.progressBarComparison);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnSaveRules);
            this.Controls.Add(this.btnLoadRules);
            this.Controls.Add(this.tabControlRuleTypes);
            this.Controls.Add(this.txtCIDCompany);
            this.Controls.Add(this.lblCIDCompany);
            this.Controls.Add(this.txtCIDBranchInvoice);
            this.Controls.Add(this.lblCIDBranchInvoice);
            this.Controls.Add(this.txtCSerie);
            this.Controls.Add(this.lblCSerie);
            this.Controls.Add(this.txtCIDInvoice);
            this.Controls.Add(this.lblCIDInvoice);
            this.Name = "ucDatabaseComparison";
            this.Size = new System.Drawing.Size(1341, 718);
            this.tabControlRuleTypes.ResumeLayout(false);
            this.tabPageSingleValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).EndInit();
            this.tabPageRowSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRowSetRules)).EndInit();
            this.tabPagePresence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresenceRules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
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
        private System.Windows.Forms.TabControl tabControlRuleTypes;
        private System.Windows.Forms.TabPage tabPageSingleValue;
        private System.Windows.Forms.Button btnConfigureGeneralOrigin;
        private System.Windows.Forms.Label lblGeneralOriginPreview;
        private System.Windows.Forms.Button btnConfigureGeneralDestination;
        private System.Windows.Forms.Label lblGeneralDestinationPreview;
        private System.Windows.Forms.DataGridView dgvRules;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnRemoveRule;
        private System.Windows.Forms.TabPage tabPageRowSet;
        private System.Windows.Forms.Button btnConfigureGeneralOriginRowSet;
        private System.Windows.Forms.Label lblGeneralOriginRowSetPreview;
        private System.Windows.Forms.Button btnConfigureGeneralDestinationRowSet;
        private System.Windows.Forms.Label lblGeneralDestinationRowSetPreview;
        private System.Windows.Forms.DataGridView dgvRowSetRules;
        private System.Windows.Forms.Button btnAddRowSetRule;
        private System.Windows.Forms.Button btnRemoveRowSetRule;
        private System.Windows.Forms.TabPage tabPagePresence;
        private System.Windows.Forms.DataGridView dgvPresenceRules;
        private System.Windows.Forms.Button btnAddPresenceRule;
        private System.Windows.Forms.Button btnRemovePresenceRule;
        private System.Windows.Forms.Button btnLoadRules;
        private System.Windows.Forms.Button btnSaveRules;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ProgressBar progressBarComparison;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.DataGridView dgvResults;
    }
}
