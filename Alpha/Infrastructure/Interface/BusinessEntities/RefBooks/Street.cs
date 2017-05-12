using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Улица
    /// </summary>
    public class Street : DomainObject
    {
        private string _name;
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get
            {
                Load();
                return _name;
            }
            set
            {
                Load();
                _name = value;
            }
        }

        private string _billName;
        /// <summary>
        /// Наименование в квитанции
        /// </summary>
        public string BillName
        {
            get
            {
                Load();
                return _billName;
            }
            set
            {
                Load();
                _billName = value;
            }
        }
    }
}
