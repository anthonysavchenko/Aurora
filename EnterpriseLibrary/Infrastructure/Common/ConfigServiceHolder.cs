using Taumis.EnterpriseLibrary.Win.Common.Services;

namespace Taumis.Infrastructure.Interface.Services
{
    /// <summary>
    /// Держатель сервиса конфигурации
    /// </summary>
    public static class ConfigServiceHolder
    {
        /// <summary>
        /// Сервис конфигурации
        /// </summary>
        public static IConfigService ConfigService { get; set; }
    }
}
