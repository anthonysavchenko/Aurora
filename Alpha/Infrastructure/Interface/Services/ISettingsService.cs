namespace Taumis.Alpha.Infrastructure.Interface.Services
{
    /// <summary>
    /// Интерфейс сервиса настроек
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Путь резервного копирования
        /// </summary>
        /// <returns></returns>
        string GetBackupPath();
    }
}
