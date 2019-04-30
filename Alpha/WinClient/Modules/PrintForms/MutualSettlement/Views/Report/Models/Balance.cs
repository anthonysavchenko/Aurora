namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report.Models
{
    public class Balance
    {
        public decimal Charge { get; set; }
        public decimal Benefit { get; set; }
        public decimal Recharge { get; set; }
        public decimal Payable { get; set; }
        public decimal Payment { get; set; }
        public decimal Act { get; set; }
        public decimal Debt { get; set; }
        public decimal PaymentOnCreateDate { get; set; }
        public decimal PaymentOnEnterPeriod { get; set; }

        public void Add(Balance value)
        {
            Charge += value.Charge;
            Benefit += value.Benefit;
            Recharge += value.Recharge;
            Payable += value.Payable;
            Payment += value.Payment;
            Act += value.Act;
            Debt += value.Debt;
            PaymentOnCreateDate += value.PaymentOnCreateDate;
            PaymentOnEnterPeriod += value.PaymentOnEnterPeriod;
        }
    }
}
