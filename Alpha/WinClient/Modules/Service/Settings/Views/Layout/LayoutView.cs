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
        /// Адрес SMTP сервера
        /// </summary>
        public string Server
        {
            get
            {
                return ServerTextBox.Text;
            }
            set
            {
                ServerTextBox.Text = value;
            }
        }

        /// <summary>
        /// Порт SMTP сервера
        /// </summary>
        public int ServerPort
        {
            get
            {
                return (int)PortNumericUpDown.Value;
            }
            set
            {
                PortNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Логин для входа на SMTP Сервер
        /// </summary>
        public string ServerLogin
        {
            get
            {
                return LoginTextBox.Text;
            }
            set
            {
                LoginTextBox.Text = value;
            }
        }

        /// <summary>
        /// Пароль для входа на SMTP Сервер
        /// </summary>
        public string ServerPassword
        {
            get
            {
                return PasswordTextBox.Text;
            }
            set
            {
                PasswordTextBox.Text = value;
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
