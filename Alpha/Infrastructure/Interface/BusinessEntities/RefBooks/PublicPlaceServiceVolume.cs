using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class PublicPlaceServiceVolume : DomainObject
    {
        private Building _building;
        /// <summary>
        /// Дом
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

        private DateTime _period;
        /// <summary>
        /// Учетный период
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

        private decimal _volume;
        /// <summary>
        /// Объем потребления услуги
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
    }
}