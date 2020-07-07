using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Дом
    /// </summary>
    public class Building : DomainObject
    {
        private string _street;
        /// <summary>
        /// Улица
        /// </summary>
        public string Street
        {
            get
            {
                Load();
                return _street;
            }
            set
            {
                Load();
                _street = value;
            }
        }

        private string _number;
        /// <summary>
        /// Номер дома
        /// </summary>
        public string Number
        {
            get
            {
                Load();
                return _number;
            }
            set
            {
                Load();
                _number = value;
            }
        }
    }
}
