using System;

using Taumis.Infrastructure.Interface.Services;

using Taumis.EnterpriseLibrary.Win.Common.Services;
using Taumis.Alpha.WinClient.Aurora.Shell.Config;

namespace Taumis.Infrastructure.Shell.Config
{
    /// <summary>
    /// Сервис конфигурации win(толстого) клиента
    /// </summary>
    public class WinConfigService : IConfigService
    {
        #region Implementation of IConfigService

        /// <summary>
        /// Конфигурация БД
        /// </summary>
        public DBConfig DataBase
        {
            get
            {
                return new DBConfig
                {
                    IP = Settings.Default.DBServerIP,
                    Port = Settings.Default.DBServerPort,
                    LogNamespace = Settings.Default.DBLogNamespace,
                    Namespace = Settings.Default.DBNamespace
                };
            }
        }

        /// <summary>
        /// Использовать ли локальный профайл каталог
        /// </summary>
        public bool UseLocalProfileCatalog
        {
            get
            {
                return Settings.Default.UseLocalProfileCatalog;
            }
        }

        /// <summary>
        /// Интервал между очисткой незанятых подключений к БД
        /// </summary>
        public int DBConnectionClearInterval
        {
            get
            {
                return Settings.Default.DBConnectionClearInterval;
            }
        }

        #endregion
    }
}