using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Interface.Services.Settings;

namespace Taumis.Alpha.Infrastructure.Library.Services
{
    /// <summary>
    /// Сервис настроек
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private const string FINE_COEFFICIENT_NAME = "FineCoefficient";
        private const string SMTP_SERVER = "SmtpServer";
        private const string SMTP_PORT = "SmtpPort";
        private const string SMTP_LOGIN = "SmtpLogin";
        private const string SMTP_PASSWORD = "SmtpPassword";
        private const string SMTP_SENDER_NAME = "SmtpSenderName";
        private const string SMTP_SENDER_EMAIL = "SmtpSenderEmail";
        private const string BACKUP_PATH = "BackupPath";

        /// <summary>
        /// Возвращает коэффициент расчета пени
        /// </summary>
        public decimal GetFineCoefficient()
        {
            decimal _fineCoefficient;

            using (Entities _entities = new Entities())
            {
                _fineCoefficient = decimal.Parse(_entities.Settings.First(s => s.Name == FINE_COEFFICIENT_NAME).Value);
            }

            return _fineCoefficient;
        }

        /// <summary>
        /// Адрес SMTP сервера
        /// </summary>
        public SmtpSettings GetSmtpSettings()
        {
            SmtpSettings _set = null;

            using (Entities _entities = new Entities())
            {
                var _settings = _entities.Settings.ToList();
                _set = new SmtpSettings(
                    _settings.First(s => s.Name == SMTP_SERVER).Value,
                    int.Parse(_entities.Settings.First(s => s.Name == SMTP_PORT).Value),
                    _settings.First(s => s.Name == SMTP_LOGIN).Value,
                    _settings.First(s => s.Name == SMTP_PASSWORD).Value,
                    _settings.First(s => s.Name == SMTP_SENDER_NAME).Value,
                    _settings.First(s => s.Name == SMTP_SENDER_EMAIL).Value);
            }

            return _set;
        }

        /// <summary>
        /// Путь резервного копирования
        /// </summary>
        /// <returns></returns>
        public string GetBackupPath()
        {
            string _backupPath;

            using (Entities _db = new Entities())
            {
                _backupPath = _db.Settings.First(s => s.Name == BACKUP_PATH).Value;
            }

            return _backupPath;
        }
    }
}