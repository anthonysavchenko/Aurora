namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Операция переплаты
    /// </summary>
    public class OverpaymentOper : BasePaymentOper
    {
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