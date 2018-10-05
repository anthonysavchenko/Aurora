using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Model
{
    /// <summary>
    /// Класс с информацией о введенных данных в мастере
    /// </summary>
    public class WizardItem
    {
        public CustomerInfo CustomerInfo { get; set; }

        /// <summary>
        /// ID прибора учета
        /// </summary>
        public int CounterId { get; set; }

        /// <summary>
        /// Номер прибора учета
        /// </summary>
        public string CounterNumber { get; set; }

        /// <summary>
        /// Модель прибора учета
        /// </summary>
        public string CounterModel { get; set; }

        /// <summary>
        /// Дата сбора показания
        /// </summary>
        public DateTime CollectDate { get; set; }

        /// <summary>
        /// Период сбора показния
        /// </summary>
        public DateTime Period { get; set; }

        /// <summary>
        /// Показание
        /// </summary>
        public decimal CounterValue { get; set; }

        /// <summary>
        /// Период снятия пред. показаний
        /// </summary>
        public DateTime PrevCounterValuePeriod { get; set; }

        /// <summary>
        /// Предыдущее показание
        /// </summary>
        public decimal PrevCounterValue { get; set; }

        /// <summary>
        /// Признак наличия ошибки
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// Содержание ошибки
        /// </summary>
        public string ErrorMessage { get; set; } = "Данные корректны";
    }
}