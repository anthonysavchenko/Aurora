using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBooks
{
    /// <summary>
    /// Объем услуги по предоставлению электр. энергии по общему счетчику
    /// </summary>
    public class ElectricitySharedCounterVolume : DomainObject
    {
        private decimal _volume;
        /// <summary>
        /// Объем предоставленной услуги
        /// </summary>
        public decimal Volume
        {
            get
            {
                Load();
                return _volume;
            }
            set
            {
                Load();
                _volume = value;
            }
        }

        private DateTime _period;
        /// <summary>
        /// Учетный период, в котором предоставлялась услуга
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

        private Building _building;
        /// <summary>
        /// Дом, жильцам которого предоставлялась услуга
        /// </summary>
        public Building Building
        {
            get
            {
                Load();
                return _building;
            }
            set
            {
                Load();
                _building = value;
            }
        }
    }
}
