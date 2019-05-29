using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Queries
{
    public static class GetCustomersByAddressQuery
    {
        public class ResidentData
        {
            public string BenefitTypeCode { get; set; }
            public Residents Resident { get; set; }
        }

        public class AccountData
        {
            public Customers Customer { get; set; }
            public List<ResidentData> Residents { get; set; }
            public Buildings Building { get; set; }
        }

        public static List<AccountData> GetCustomersByAddress(this Entities db, 
            int buildingId, 
            string apartment, 
            DateTime lastChargedPeriod) =>
            db.Customers
                .Where(x => x.Buildings.ID == buildingId
                    && x.Apartment == apartment
                    && x.CustomerPoses.Any(y => y.Till >= lastChargedPeriod))
                .Select(x =>
                    new AccountData
                    {
                        Customer = x,
                        Building = x.Buildings,
                        Residents = x.Residents
                            .Select(y =>
                                new ResidentData
                                {
                                    BenefitTypeCode = y.BenefitTypes.Code,
                                    Resident = y
                                })
                            .ToList()
                    })
                .ToList();
    }
}
