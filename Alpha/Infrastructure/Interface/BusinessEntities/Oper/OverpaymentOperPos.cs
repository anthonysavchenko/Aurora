namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    public class OverpaymentOperPos : BasePaymentOperPos
    {
        /// <summary>
        /// Операция переплаты
        /// </summary>
        public new OverpaymentOper PaymentOper
        {
            get
            {
                Load();
                return (OverpaymentOper)base.PaymentOper;
            }
            set
            {
                Load();
                base.PaymentOper = value;
            }
        }
    }
}