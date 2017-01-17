using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция операции корректировки переплаты
    /// </summary>
    public class OverpaymentCorrectionOperPos : DomainObject
    {
        private decimal _value;
        /// <summary>
        /// Сумма корректировки
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

        private OverpaymentCorrectionOper _overpaymentCorrectionOper;
        /// <summary>
        /// Операция корректировки переплаты
        /// </summary>
        public OverpaymentCorrectionOper OverpaymentCorrectionOper
        {
            get
            {
                Load();
                return _overpaymentCorrectionOper;
            }
            set
            {
                Load();
                _overpaymentCorrectionOper = value;
            }
        }
    }
}