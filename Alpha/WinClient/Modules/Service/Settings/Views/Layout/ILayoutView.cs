using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings.Views.Layout
{
    public interface ILayoutView : IBaseLayoutView
    {
        /// <summary>
        /// Коэффициент расчета пени
        /// </summary>
        decimal FineCoefficient { get; set; }

        /// <summary>
        /// Адрес SMTP сервера
        /// </summary>
        string SmtpServer { get; set; }

        /// <summary>
        /// Порт SMTP сервера
        /// </summary>
        int SmtpServerPort { get; set; }

        /// <summary>
        /// Логин для входа на SMTP Сервер
        /// </summary>
        string SmtpServerLogin { get; set; }

        /// <summary>
        /// Пароль для входа на SMTP Сервер
        /// </summary>
        string SmtpServerPassword { get; set; }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        string SenderName { get; set; }

        /// <summary>
        /// Email отправителя
        /// </summary>
        string SenderEmail { get; set; }

        /// <summary>
        /// Путь резервного копирования
        /// </summary>
        string BackupPath { get; set; }
    }
}