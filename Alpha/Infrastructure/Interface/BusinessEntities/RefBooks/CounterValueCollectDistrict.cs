using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBooks
{
    /// <summary>
    /// Район сбора показаний приборов учета
    /// </summary>
    public class CounterValueCollectDistrict : DomainObject
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
    }
}
