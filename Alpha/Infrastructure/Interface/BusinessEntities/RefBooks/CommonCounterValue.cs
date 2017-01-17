using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Показания общедомового счетчика
    /// </summary>
    public class CommonCounterValue : DomainObject
    {
        private DateTime _period;
        /// <summary>
        /// Период
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
        /// Значение/показание счетчика
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

        private CommonCounter _commonCounter;
        /// <summary>
        /// Общедомовой счетчик
        /// </summary>
        public CommonCounter CommonCounter
        {
            get
            {
                Load();
                return _commonCounter;
            }
            set
            {
                Load();
                _commonCounter = value;
            }
        }
    }
}