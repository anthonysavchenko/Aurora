using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Domain.Oper;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Payment correction
    /// </summary>
    public class PaymentCorrection : Operation
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

        private DateTime _period;

        /// <summary>
        /// Period
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

        private Payment _payment;
        /// <summary>
        /// Payment
        /// </summary>
        public Payment Payment
        {
            get
            {
                Load();
                return _payment;
            }
            set
            {
                Load();
                _payment = value;
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