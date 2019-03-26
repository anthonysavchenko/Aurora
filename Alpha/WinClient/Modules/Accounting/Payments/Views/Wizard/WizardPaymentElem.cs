using System;
using System.Text.RegularExpressions;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Wizard
{
    /// <summary>
    /// Класс с информацией о введенных данных в мастере
    /// </summary>
    public class WizardPaymentElement
    {
        /// <summary>
        /// Лицевой счет
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Собственник
        /// </summary>
        public Customer Owner { get; set; }

        /// <summary>
        /// Период платежа
        /// </summary>
        public DateTime Period { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Признак наличия ошибки
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// Содержание ошибки
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public WizardPaymentElement()
        {
            Account = String.Empty;
            ErrorMessage = "Данные корректны";
            Period = ServerTimeServiceHolder.ServerTimeService.GetPeriodInfo().LastCharged;
            Value = 0;
            Owner = null;
            HasError = false;
        }

        /// <summary>
        /// Проверка корректности введенных данных
        /// </summary>
        /// <returns>Признак наличия ошибки в данных</returns>
        public bool Validate()
        {
            bool _res = true;
            ErrorMessage = String.Empty;

            if (!Regex.IsMatch(Account, @"\d{4}-\d{3}-\d{1}"))
            {
                ErrorMessage += "Некорректный Лицевой счет. \r\n";
                _res = false;
            }

            if (Period == DateTime.MinValue)
            {
                ErrorMessage += "Некорректный Период учета. \r\n";
                _res = false;
            }

            if (Value <= 0)
            {
                ErrorMessage += "Некорректная Сумма платежа. \r\n";
                _res = false;
            }

            if (Owner == null)
            {
                ErrorMessage += "Собственник лицевого счета не найден. \r\n";
                _res = false;
            }

            if (_res)
            {
                ErrorMessage = "Данные корректны";
            }

            return HasError = !_res;
        }
    }
}