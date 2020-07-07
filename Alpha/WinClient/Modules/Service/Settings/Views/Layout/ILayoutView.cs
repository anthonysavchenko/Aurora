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
        int ServerPort { get; set; }

        /// <summary>
        /// Логин для входа на SMTP Сервер
        /// </summary>
        string ServerLogin { get; set; }

        /// <summary>
        /// Пароль для входа на SMTP Сервер
        /// </summary>
        string ServerPassword { get; set; }

        /// <summary>
        /// Путь резервного копирования
        /// </summary>
        string BackupPath { get; set; }
    }
}
