using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard
{
    /// <summary>
    /// Класс с информацией о введенных данных в мастере
    /// </summary>
    public class WizardItem
    {
        /// <summary>
        /// Лицевой счет
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// ФИО абонента
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// ID абонента
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Квартира
        /// </summary>
        public string Apartment { get; set; }

        /// <summary>
        /// Площадь квартиры
        /// </summary>
        public decimal Area { get; set; }

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
        /// Показание
        /// </summary>
        public decimal Value { get; set; }

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