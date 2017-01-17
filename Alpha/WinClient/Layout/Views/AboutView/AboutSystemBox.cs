using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Windows.Forms;

namespace Taumis.Infrastructure.Layout
{
    partial class AboutSystemBox : Form
    {
        public AboutSystemBox()
        {
            InitializeComponent();

            TitleLabel.Text = String.Format("Aurora {0}",
                ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : String.Empty);
            DescriptionTextBox.Text = "Система учета расчетов с абонентами\r\n\r\nCopyright © Taumis LLC 2011-2016";
        }

        private void DeveloperLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://taumis.ru");
        }
    }
}
