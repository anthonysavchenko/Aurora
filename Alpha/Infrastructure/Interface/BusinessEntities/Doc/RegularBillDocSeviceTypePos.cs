using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Информация о начислениях в ежемесячных квитанциях
    /// </summary>
    public class RegularBillDocSeviceTypePos : DomainObject
    {
        private RegularBillDoc _regularBillDoc;
        /// <summary>
        /// Ежемесячная квитанция
        /// </summary>
        public RegularBillDoc RegularBillDoc
        {
            get
            {
                Load();
                return _regularBillDoc;
            }
            set
            {
                Load();
                _regularBillDoc = value;
            }
        }

        private ServiceType _serviceType;
        /// <summary>
        /// Вид услуги
        /// </summary>
        public ServiceType ServiceType
        {
            get
            {
                Load();
                return _serviceType;
            }
            set
            {
                Load();
                _serviceType = value;
            }
        }

        private string _serviceTypeName;
        /// <summary>
        /// Наименование вида услуги
        /// </summary>
        public string ServiceTypeName
        {
            get
            {
                Load();
                return _serviceTypeName;
            }
            set
            {
                Load();
                _serviceTypeName = value;
            }
        }

        private decimal _payRate;
        /// <summary>
        /// Ставка
        /// </summary>
        public decimal PayRate
        {
            get
            {
                Load();
                return _payRate;
            }
            set
            {
                Load();
                _payRate = value;
            }
        }

        private decimal _charge;
        /// <summary>
        /// Начисление
        /// </summary>
        public decimal Charge
        {
            get
            {
                Load();
                return _charge;
            }
            set
            {
                Load();
                _charge = value;
            }
        }

        private decimal _benefit;
        /// <summary>
        /// Льгота
        /// </summary>
        public decimal Benefit
        {
            get
            {
                Load();
                return _benefit;
            }
            set
            {
                Load();
                _benefit = value;
            }
        }

        private decimal _recalculation;
        /// <summary>
        /// Перерасчет
        /// </summary>
        public decimal Recalculation
        {
            get
            {
                Load();
                return _recalculation;
            }
            set
            {
                Load();
                _recalculation = value;
            }
        }

        private decimal _payable;
        /// <summary>
        /// К оплате
        /// </summary>
        public decimal Payable
        {
            get
            {
                Load();
                return _payable;
            }
            set
            {
                Load();
                _payable = value;
            }
        }
    }
}
