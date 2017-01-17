using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    public class BasePaymentOperPos : DomainObject
    {
        private DateTime _period;
        /// <summary>
        /// Погашаемый период начисления
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
        /// Значение
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

        private Service _service;
        /// <summary>
        /// Услуга
        /// </summary>
        public Service Service
        {
            get
            {
                Load();
                return _service;
            }
            set
            {
                Load();
                _service = value;
            }
        }

        private BasePaymentOper _paymentOper;
        /// <summary>
        /// Операция платежа
        /// </summary>
        public BasePaymentOper PaymentOper
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
