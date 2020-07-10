using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Windows.Forms;
using Taumis.Alpha.WinClient.Aurora.Library.Services;

namespace Taumis.Alpha.WinClient.Aurora.Library.Views.AuthenticationView
{
    public partial class AuthenticationView : Form
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationView(AuthenticationService authenticationService)
        {
            InitializeComponent();
            _authenticationService = authenticationService;
            TitleLabel.Text = String.Format("Mjolnir {0}",
                ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : String.Empty);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (_authenticationService.Authenticate(loginTextBox.Text, passwordTextBox.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(
                    "Неверный логин или пароль",
                    "Ошибка аутентификации",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DeveloperLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto://anton.savchenko@taumis.ru");
        }
    }
}