namespace Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService
{
    /// <summary>
    /// Сервис определения серверного времени
    /// </summary>
    public interface IServerTimeService
    {
        /// <summary>
        /// Возвращает информацию о текущем периоде, дате-времени
        /// </summary>
        DateTimeInfo GetDateTimeInfo();
    }

    /// <summary>
    /// Временный статический класс, содержащий сервис определения серверного времени
    /// </summary>
    /// <remarks>
    /// Не использовать в перзентерах и вью толстого клиента.
    /// Нужен временно и только для датамапперов и веба.
    /// </remarks>
    public static class ServerTimeServiceHolder
    {
        public static IServerTimeService ServerTimeService { get; set; }
    }
}