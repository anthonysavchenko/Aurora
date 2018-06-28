namespace Taumis.Alpha.Infrastructure.Interface.Common
{
    public class Balance
    {
        public decimal Charge { get; set; }
        public decimal Recharge { get; set; }
        public decimal Benefit { get; set; }
        public decimal Payment { get; set; }
        public decimal Overpayment { get; set; }
        public decimal OverpaymentCorrection { get; set; }

        public decimal Total => Charge + Recharge + Benefit + Payment + Overpayment + OverpaymentCorrection;

        public static Balance operator +(Balance left, Balance right)
        {
            return new Balance
            {
                Charge = left.Charge + right.Charge,
                Benefit = left.Benefit + right.Benefit,
                Overpayment = left.Overpayment + right.Overpayment,
                OverpaymentCorrection = left.OverpaymentCorrection + right.OverpaymentCorrection,
                Payment = left.Payment + right.Payment,
                Recharge = left.Recharge + right.Recharge
            };
        }
    }
}
