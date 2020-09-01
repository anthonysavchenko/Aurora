using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Абонент
    /// </summary>
    public class Customer : DomainObject
    {
        private string _apartment;
        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Apartment
        {
            get
            {
                Load();
                return _apartment;
            }
            set
            {
                Load();
                _apartment = value;
            }
        }

        private string _account;

        public string Account
        {
            get
            {
                Load();
                return _account;
            }
            set
            {
                Load();
                _account = value;
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
    }
}
