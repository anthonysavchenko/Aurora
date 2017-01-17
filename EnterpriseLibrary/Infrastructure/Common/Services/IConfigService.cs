namespace Taumis.EnterpriseLibrary.Win.Common.Services
{
    /// <summary>
    /// Настройки БД
    /// </summary>
    public struct DBConfig
    {
        /// <summary>
        /// IP адрес сервера
        /// </summary>
        public string IP;

        /// <summary>
        /// Порт
        /// </summary>
        public string Port;

        /// <summary>
        /// Область БД для логов
        /// </summary>
        public string LogNamespace;

        /// <summary>
        /// Область БД
        /// </summary>
        public string Namespace;
    }

    /// <summary>
    /// Интерфейс сервиса конфигурации
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Конфигурация БД
        /// </summary>
        DBConfig DataBase { get; }

        /// <summary>
        /// Использовать ли локальный профайл каталог
        /// </summary>
        bool UseLocalProfileCatalog { get; }

        /// <summary>
        /// Интервал между очисткой незанятых подключений к БД
        /// </summary>
        int DBConnectionClearInterval { get; }
    }
}