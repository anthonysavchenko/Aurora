using System.Linq;
using Taumis.Alpha.Server.Core.Services.Settings;
using Taumis.Alpha.Server.Infrastructure.Data;

namespace Taumis.Alpha.Server.Infrastructure.Services
{
    /// <summary>
    /// Сервис настроек
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private readonly IAlphaDbContext _db;

        public SettingsService(IAlphaDbContext db)
        {
            _db = db;
        }

        private const string FINE_COEFFICIENT_NAME = "FineCoefficient";
        private const string SMTP_SERVER = "SmtpServer";
        private const string SMTP_PORT = "SmtpPort";
        private const string SMTP_LOGIN = "SmtpLogin";
        private const string SMTP_PASSWORD = "SmtpPassword";
        private const string SMTP_SENDER_NAME = "SmtpSenderName";
        private const string SMTP_SENDER_EMAIL = "SmtpSenderEmail";

        /// <summary>
        /// Возвращает коэффициент расчета пени
        /// </summary>
        public decimal GetFineCoefficient()
        {
            return decimal.Parse(_db.Settings.First(s => s.Name == FINE_COEFFICIENT_NAME).Value);
        }

        /// <summary>
        /// Адрес SMTP сервера
        /// </summary>
        public SmtpSettings GetSmtpSettings()
        {
            var _settings = _db.Settings.ToList();
            
            SmtpSettings _set = 
                new SmtpSettings(
                    _settings.First(s => s.Name == SMTP_SERVER).Value,
                    int.Parse(_settings.First(s => s.Name == SMTP_PORT).Value),
                    _settings.First(s => s.Name == SMTP_LOGIN).Value,
                    _settings.First(s => s.Name == SMTP_PASSWORD).Value,
                    _settings.First(s => s.Name == SMTP_SENDER_NAME).Value,
                    _settings.First(s => s.Name == SMTP_SENDER_EMAIL).Value);
        
            return _set;
        }
    }
}