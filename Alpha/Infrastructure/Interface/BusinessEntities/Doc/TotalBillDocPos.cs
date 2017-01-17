using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Информация о начислениях в квитанциях за период
    /// </summary>
    public class TotalBillDocPos : DomainObject
    {
        private TotalBillDoc _totalBillDoc;
        /// <summary>
        /// Квитанция за период
        /// </summary>
        public TotalBillDoc TotalBillDoc
        {
            get
            {
                Load();
                return _totalBillDoc;
            }
            set
            {
                Load();
                _totalBillDoc = value;
            }
        }

        private string _serviceTypeName;
        /// <summary>
        /// Услуга
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

        private decimal _value;
        /// <summary>
        /// Дополнительные начисления
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

        private decimal _totalCharged;
        /// <summary>
        /// Всего начислено за период
        /// </summary>
        public decimal TotalCharged
        {
            get
            {
                Load();
                return _totalCharged;
            }
            set
            {
                Load();
                _totalCharged = value;
            }
        }

        private decimal _totalPaid;
        /// <summary>
        /// Всего оплачено за период
        /// </summary>
        public decimal TotalPaid
        {
            get
            {
                Load();
                return _totalPaid;
            }
            set
            {
                Load();
                _totalPaid = value;
            }
        }
    }
}
