namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common
{
    public class CustomerPosInfo
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public int ServiceId { get; set; }
        public string ServiceTypeCodeNumber { get; set; }
        public int ContractorId { get; set; }
        public byte ChargeRule { get; set; }
        public decimal Norm { get; set; }
    }
}
