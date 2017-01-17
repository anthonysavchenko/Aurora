namespace Taumis.Alpha.Server.Core.Services.Settings
{
    /// <summary>
    /// Интерфейс сервиса настроек
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Коэффициент расчета пени
        /// </summary>
        decimal GetFineCoefficient();

        /// <summary>
        /// Настройки SMTP сервера
        /// </summary>
        SmtpSettings GetSmtpSettings();
    }
}