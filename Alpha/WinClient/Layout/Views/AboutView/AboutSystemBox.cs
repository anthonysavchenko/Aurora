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

            TitleLabel.Text = String.Format("Mjolnir {0}",
                ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : String.Empty);
            DescriptionTextBox.Text = "Система учета и анализа расходов на коммунальные услуги, " +
                "предоставляемые ресурсоснабжающими организациями.\r\n\r\n" +
                "Разработано для ООО «Управляющая компания Фрунзенского района».\r\n\r\n" +
                "Разработка: Антон Савченко. 2020 год.";
        }

        private void DeveloperLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto://anton.savchenko@taumis.ru");
        }
    }
}
