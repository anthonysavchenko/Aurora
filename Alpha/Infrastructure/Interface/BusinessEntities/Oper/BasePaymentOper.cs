using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    public class BasePaymentOper : DomainObject
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

        private DateTime _paymentPeriod;
        /// <summary>
        /// Период внесения платежа
        /// </summary>
        public DateTime PaymentPeriod
        {
            get
            {
                Load();
                return _paymentPeriod;
            }
            set
            {
                Load();
                _paymentPeriod = value;
            }
        }

        private decimal _value;
        /// <summary>
        /// Сумма платежа
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

        private Customer _customer;
        /// <summary>
        /// Абонент
        /// </summary>
        public Customer Customer
        {
            get
            {
                Load();
                return _customer;
            }
            set
            {
                Load();
                _customer = value;
            }
        }
    }
}