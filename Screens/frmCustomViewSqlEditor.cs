using System;
using System.Windows.Forms;

namespace WMTool.Screens
{
    public partial class frmCustomViewSqlEditor : Form
    {
        public string ResultSql { get; private set; }
        public bool WasRemoved { get; private set; }

        public frmCustomViewSqlEditor(string viewName, string currentSql)
        {
            InitializeComponent();
            lblViewName.Text = "View: " + viewName;
            txtSql.Text = currentSql ?? string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSql.Text))
            {
                MessageBox.Show("Informe o SQL customizado ou use \"Remover Customização\".");
                return;
            }

            ResultSql = txtSql.Text;
            WasRemoved = false;
            DialogResult = DialogResult.OK;
        }

        private void btnRemoveOverride_Click(object sender, EventArgs e)
        {
            WasRemoved = true;
            DialogResult = DialogResult.OK;
        }
    }
}
