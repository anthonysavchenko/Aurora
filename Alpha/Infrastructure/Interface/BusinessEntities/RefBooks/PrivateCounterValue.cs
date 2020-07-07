using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Показания индивидуальных приборов учета
    /// </summary>
    public class PrivateCounterValue : DomainObject
    {
        private DateTime _month;
        /// <summary>
        /// Период
        /// </summary>
        public DateTime Month
        {
            get
            {
                Load();
                return _month;
            }
            set
            {
                Load();
                _month = value;
            }
        }

        private decimal _value;
        /// <summary>
        /// Значение, показание
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

        private PrivateCounter _privateCounter;
        /// <summary>
        /// Счетчик
        /// </summary>
        public PrivateCounter PrivateCounter
        {
            get
            {
                Load();
                return _privateCounter;
            }
            set
            {
                Load();
                _privateCounter = value;
            }
        }
    }
}