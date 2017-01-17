using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция операции корректировки платежа
    /// </summary>
    public class PaymentCorrectionOperPos : DomainObject
    {
        private decimal _value;
        /// <summary>
        /// Сумма
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

        private PaymentCorrectionOper _paymentCorrectionOper;
        /// <summary>
        /// Операция-владелец
        /// </summary>
        public PaymentCorrectionOper PaymentCorrectionOper
        {
            get
            {
                Load();
                return _paymentCorrectionOper;
            }
            set
            {
                Load();
                _paymentCorrectionOper = value;
            }
        }
    }
}
