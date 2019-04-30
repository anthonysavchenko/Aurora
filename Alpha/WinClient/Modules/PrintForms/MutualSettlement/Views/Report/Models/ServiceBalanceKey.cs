namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report.Models
{
    public class ServiceBalanceKey
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && ((ServiceBalanceKey)obj).ID == ID;
        }
    }
}
