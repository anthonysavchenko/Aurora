using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Место общего пользования
    /// </summary>
    public class PublicPlace : DomainObject
    {
        private decimal _area;
        /// <summary>
        /// Площадь
        /// </summary>
        public decimal Area
        {
            get
            {
                Load();
                return _area;
            }
            set
            {
                Load();
                _area = value;
            }
        }

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
    }
}