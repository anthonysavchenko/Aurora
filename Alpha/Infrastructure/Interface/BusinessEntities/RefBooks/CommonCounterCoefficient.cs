using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class CommonCounterCoefficient : DomainObject
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

        private decimal _coefficient;
        /// <summary>
        /// Коэффициент
        /// </summary>
        public decimal Coefficient
        {
            get
            {
                Load();
                return _coefficient;
            }
            set
            {
                Load();
                _coefficient = value;
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