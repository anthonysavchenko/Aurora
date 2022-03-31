namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common
{
    public class CustomerPosInfo
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; internal set; }
        public string ServiceTypeName { get; internal set; }
        public string ServiceTypeCode { get; set; }
        public int ContractorId { get; set; }
        public byte ChargeRule { get; set; }
        public decimal Norm { get; set; }
        public decimal CounterVolume { get; set; }
    }
}
