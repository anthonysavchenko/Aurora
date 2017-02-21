using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class RegularBillDoc : Entity
    {
        public RegularBillDoc()
        {
            ChargeOpers = new List<ChargeOper>();
            RegularBillDocCounterPoses = new List<RegularBillDocCounterPos>();
            RegularBillDocSeviceTypePoses = new List<RegularBillDocSeviceTypePos>();
            RegularBillDocSharedCounterPoses = new List<RegularBillDocSharedCounterPos>();
        }

        public int CustomerID { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime PayBeforeDateTime { get; set; }
        public string Account { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }
        public string Square { get; set; }
        public int ResidentsCount { get; set; }
        public decimal OverpaymentValue { get; set; }
        public decimal MonthChargeValue { get; set; }
        public decimal Value { get; set; }
        public int BillSetID { get; set; }
        public DateTime Period { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public string ContractorContactInfo { get; set; }
        public virtual BillSet BillSet { get; set; }
        public virtual ICollection<ChargeOper> ChargeOpers { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<RegularBillDocCounterPos> RegularBillDocCounterPoses { get; set; }
        public virtual ICollection<RegularBillDocSeviceTypePos> RegularBillDocSeviceTypePoses { get; set; }
        public virtual ICollection<RegularBillDocSharedCounterPos> RegularBillDocSharedCounterPoses { get; set; }
    }
}