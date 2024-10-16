﻿using Taumis.Alpha.Infrastructure.Interface.Services.Settings;

namespace Taumis.Alpha.Infrastructure.Interface.Services
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

        /// <summary>
        /// Путь резервного копирования
        /// </summary>
        /// <returns></returns>
        string GetBackupPath();

        /// <summary>
        /// Возвращает номер файла экспорта для Почта-Банка и увеличивает его в БД на единицу.
        /// </summary>
        /// <returns></returns>
        int GetAndIncreasePochtabankExportFileNumber();
    }
}