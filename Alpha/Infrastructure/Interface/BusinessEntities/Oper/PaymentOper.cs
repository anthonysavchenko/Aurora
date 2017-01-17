using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    public class PaymentOper : BasePaymentOper
    {
        private PaymentSet _paymentSet;
        /// <summary>
        /// Набор платежей
        /// </summary>
        public PaymentSet PaymentSet
        {
            get
            {
                Load();
                return _paymentSet;
            }
            set
            {
                Load();
                _paymentSet = value;
            }
        }

        private PaymentCorrectionOper _paymentCorrectionOper;
        /// <summary>
        /// Операция корректировки платежа
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