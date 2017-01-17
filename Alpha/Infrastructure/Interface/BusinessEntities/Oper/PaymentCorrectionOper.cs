using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Операция корректировки платежа
    /// </summary>
    public class PaymentCorrectionOper : DomainObject
    {
        private DateTime _creationDateTime;
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDateTime
        {
            get
            {
                Load();
                return _creationDateTime;
            }
            set
            {
                Load();
                _creationDateTime = value;
            }
        }

        private DateTime _period;
        /// <summary>
        /// Период корректировки
        /// </summary>
        public DateTime Period
        {
            get
            {
                Load();
                return _period;
            }
            set
            {
                Load();
                _period = value;
            }
        }

        private decimal _value;
        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal Value
        {
            get
            {
                Load();
                return _value;
            }
            set
            {
                Load();
                _value = value;
            }
        }

        private PaymentOper _paymentOper;
        /// <summary>
        /// Операция платежа
        /// </summary>
        public PaymentOper PaymentOper
        {
            get
            {
                Load();
                return _paymentOper;
            }
            set
            {
                Load();
                _paymentOper = value;
            }
        }
    }
}