using System;
using System.Windows.Forms;

namespace WMTool.Screens
{
    public partial class frmRuleSqlEditor : Form
    {
        public string ResultSql { get; private set; }

        public frmRuleSqlEditor(string title, string currentSql)
        {
            InitializeComponent();
            lblRuleName.Text = title;
            txtSql.Text = currentSql ?? string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSql.Text))
            {
                MessageBox.Show("Informe o SQL da regra.");
                return;
            }

            ResultSql = txtSql.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
