using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Показания частных счетчиков
    /// </summary>
    public class PrivateCounterValue : DomainObject
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

        private bool _byNorm;
        /// <summary>
        /// Показание по норме
        /// </summary>
        public bool ByNorm
        {
            get
            {
                Load();
                return _byNorm;
            }
            set
            {
                Load();
                _byNorm = value;
            }
        }
    }
}