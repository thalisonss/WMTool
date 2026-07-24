using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WMTool.Screens
{
    public partial class FormLogin : Form
    {
        public CookieContainer Cookies { get; private set; } = new CookieContainer();
        public bool Logado { get; private set; } = false;

        private WebView2 webView;
        private const string DefaultLoginUrl = "https://prodweb-whitemartins.mc1.com.br/Account/Login?ReturnUrl=%2FForm%2FRun%2FCustom_Reenviar_NF_WM";
        private const string DefaultLoginSuccessUrl = "https://prodweb-whitemartins.mc1.com.br/Form/Run/Custom_Reenviar_NF_WM";

        public FormLogin()
        {
            InitializeComponent();
        }

        

        private async Task Inicializar()
        {
            webView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(webView);

            await webView.EnsureCoreWebView2Async();

            webView.CoreWebView2.NavigationCompleted += WebView_NavigationCompleted;

            var loginUrl = WMTool.Properties.Settings.Default.configLoginUrl;
            if (string.IsNullOrWhiteSpace(loginUrl))
            {
                loginUrl = DefaultLoginUrl;
            }

            webView.CoreWebView2.Navigate(loginUrl);
        }

        private async void WebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            var url = webView.Source.ToString();

            var successUrl = WMTool.Properties.Settings.Default.configLoginSuccessUrl;
            if (string.IsNullOrWhiteSpace(successUrl))
            {
                successUrl = DefaultLoginSuccessUrl;
            }

            // Alguns fluxos podem variar com "/" no final ou parâmetros.
            var normalizedCurrent = (url ?? string.Empty).Trim().TrimEnd('/');
            var normalizedSuccess = successUrl.Trim().TrimEnd('/');

            if (string.Equals(normalizedCurrent, normalizedSuccess, StringComparison.OrdinalIgnoreCase))
            {
                var manager = webView.CoreWebView2.CookieManager;
                var cookies = await manager.GetCookiesAsync(null);

                foreach (var c in cookies)
                {
                    Cookies.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
                }

                Logado = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private async void FormLogin_Load(object sender, EventArgs e)
        {
            await Inicializar();
        }
    }
}
