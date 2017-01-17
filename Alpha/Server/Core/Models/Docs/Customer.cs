using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Enums;
using Taumis.Alpha.Server.Core.Models.Opers;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class Customer : Entity
    {
        public Customer()
        {
            CustomerPoses = new List<CustomerPos>();
            Residents = new List<Resident>();
            OverpaymentOpers = new List<OverpaymentOper>();
            PaymentOpers = new List<PaymentOper>();
            OverpaymentOpers = new List<OverpaymentOper>();
            RechargeOpers = new List<RechargeOper>();
            ChargeOpers = new List<ChargeOper>();
            TotalBillDocs = new List<TotalBillDoc>();
            RegularBillDocs = new List<RegularBillDoc>();
        }

        public string Account { get; set; }
        public OwnerTypes OwnerType { get; set; }
        public bool IsPrivate { get; set; }
        public int RoomsCount { get; set; }
        public string Apartment { get; set; }
        public decimal Square { get; set; }
        public string PhysicalPersonFullName { get; set; }
        public string PhysicalPersonShortName { get; set; }
        public string JuridicalPersonFullName { get; set; }
        public int BuildingID { get; set; }
        public string Comment { get; set; }
        public short Floor { get; set; }
        public bool LiftPresence { get; set; }
        public bool RubbishChutePresence { get; set; }
        public bool BillSendingSubscription { get; set; }
        public int? UserID { get; set; }
        public virtual Building Building { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CustomerPos> CustomerPoses { get; set; }
        public virtual ICollection<Resident> Residents { get; set; }
        public virtual ICollection<OverpaymentOper> OverpaymentOpers { get; set; }
        public virtual ICollection<PaymentOper> PaymentOpers { get; set; }
        public virtual ICollection<RechargeOper> RechargeOpers { get; set; }
        public virtual ICollection<ChargeOper> ChargeOpers { get; set; }
        public virtual ICollection<TotalBillDoc> TotalBillDocs { get; set; }
        public virtual ICollection<RegularBillDoc> RegularBillDocs { get; set; }
    }
}