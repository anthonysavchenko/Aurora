using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Domain.Oper;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Начисление
    /// </summary>
    public class Payment : Operation
    {
        private Customer _customer;

        /// <summary>
        /// Customer
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

        private Intermediary _intermediary;

        /// <summary>
        /// Intermediary
        /// </summary>
        public Intermediary Intermediary
        {
            get
            {
                Load();
                return _intermediary;
            }
            set
            {
                Load();
                _intermediary = value;
            }
        }

        private DateTime _paymentPeriod;

        /// <summary>
        /// PaymentPeriod
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
        private decimal _value;

        /// <summary>
        /// Value
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
    }
}