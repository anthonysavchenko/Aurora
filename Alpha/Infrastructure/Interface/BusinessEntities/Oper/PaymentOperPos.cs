namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция операции платежа
    /// </summary>
    public class PaymentOperPos : BasePaymentOperPos
    {
        /// <summary>
        /// Операция платежа
        /// </summary>
        public new PaymentOper PaymentOper
        {
            get
            {
                Load();
                return (PaymentOper)base.PaymentOper;
            }
            set
            {
                Load();
                base.PaymentOper = value;
            }
        }
    }
}