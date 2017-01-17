using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Базовая операция начислений
    /// </summary>
    public abstract class BaseChargeOper : DomainObject
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

        private decimal _value;
        /// <summary>
        /// Сумма начисления
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

        private ChargeSet _chargeSet;
        /// <summary>
        /// Набор начислений
        /// </summary>
        public ChargeSet ChargeSet
        {
            get
            {
                Load();
                return _chargeSet;
            }
            set
            {
                Load();
                _chargeSet = value;
            }
        }
    }
}