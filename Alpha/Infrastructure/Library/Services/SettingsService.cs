using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Interface.Services.Settings;

namespace Taumis.Alpha.Infrastructure.Library.Services
{
    /// <summary>
    /// Сервис настроек
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// Настройки скачивания форм ДЭК.
        /// </summary>
        static public DecFormsDownloadSettings GetDecFormsDownloadSettings()
        {
            DecFormsDownloadSettings result = null;

            using (Entities db = new Entities())
            {
                var settings = db.Settings.ToList();

                result =
                    new DecFormsDownloadSettings(
                        settings.First(s => s.Name == SettingNames.DEC_FORMS_DOWNLOAD_SERVER).Value,
                        int.Parse(db.Settings.First(s => s.Name == SettingNames.DEC_FORMS_DOWNLOAD_PORT).Value),
                        settings.First(s => s.Name == SettingNames.DEC_FORMS_DOWNLOAD_LOGIN).Value,
                        settings.First(s => s.Name == SettingNames.DEC_FORMS_DOWNLOAD_PASSWORD).Value,
                        settings.First(s => s.Name == SettingNames.DEC_FORMS_DOWNLOAD_SENDER).Value);
            }

            return result;
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
                _backupPath = _db.Settings.First(s => s.Name == SettingNames.BACKUP_PATH).Value;
            }

            return _backupPath;
        }
    }
}