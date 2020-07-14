using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings.Views.Layout
{
    public interface ILayoutView : IBaseLayoutView
    {
        /// <summary>
        /// Адрес SMTP сервера
        /// </summary>
        string Server { get; set; }

        /// <summary>
        /// Порт SMTP сервера
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Логин для входа на SMTP Сервер
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// Пароль для входа на SMTP Сервер
        /// </summary>
        string Password { get; set; }

        string Sender { get; set; }

        /// <summary>
        /// Путь резервного копирования
        /// </summary>
        string BackupPath { get; set; }
    }
}
