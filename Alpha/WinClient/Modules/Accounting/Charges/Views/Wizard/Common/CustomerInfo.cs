using System.Collections.Generic;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common
{
    public class CustomerInfo
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Apartment { get; set; }
        public string Owner { get; set; }
        public decimal Area { get; set; }
        public string Street { get; set; }
        public int BuildingId { get; set; }
        public string Building { get; set; }
        public int ResidentsCount { get; set; }
        public int FederalBenefitResidentsCount { get; set; }
        public int LocalBenefitCoefficient { get; set; }
        public List<CustomerPosInfo> Poses { get; set; }
    }
}
