using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings.Layout;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
//using BaseLayoutView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings.Views.Layout
{
    [SmartPart]
    public partial class LayoutView : BaseLayoutView, ILayoutView
    {
        /// <summary>
        /// Констр6уктор
        /// </summary>
        public LayoutView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new LayoutViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        /// <summary>
        /// Коэффициент расчета пени
        /// </summary>
        public decimal FineCoefficient
        {
            get
            {
                return fineCoefficientNumericUpDown.Value;
            }
            set
            {
                fineCoefficientNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Адрес SMTP сервера
        /// </summary>
        public string SmtpServer
        {
            get
            {
                return smtpServerTextBox.Text;
            }
            set
            {
                smtpServerTextBox.Text = value;
            }
        }

        /// <summary>
        /// Порт SMTP сервера
        /// </summary>
        public int SmtpServerPort
        {
            get
            {
                return (int)portNumericUpDown.Value;
            }
            set
            {
                portNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Логин для входа на SMTP Сервер
        /// </summary>
        public string SmtpServerLogin
        {
            get
            {
                return loginTextBox.Text;
            }
            set
            {
                loginTextBox.Text = value;
            }
        }

        /// <summary>
        /// Пароль для входа на SMTP Сервер
        /// </summary>
        public string SmtpServerPassword
        {
            get
            {
                return passwordTextBox.Text;
            }
            set
            {
                passwordTextBox.Text = value;
            }
        }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string SenderName
        {
            get
            {
                return senderTextBox.Text;
            }
            set
            {
                senderTextBox.Text = value;
            }
        }

        /// <summary>
        /// Email отправителя
        /// </summary>
        public string SenderEmail
        {
            get
            {
                return emailTextBox.Text;
            }
            set
            {
                emailTextBox.Text = value;
            }
        }

        /// <summary>
        /// Путь резервного копирования
        /// </summary>
        public string BackupPath
        {
            get
            {
                return backupPathTextBox.Text;
            }
            set
            {
                backupPathTextBox.Text = value;
            }
        }
    }
}