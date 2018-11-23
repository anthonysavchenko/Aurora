using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Queries
{
    public static class GetPercentCorrectionCustomerDataQuery
    {
        public class CustomerData
        {
            public int ID { get; set; }
            public string Account { get; set; }
            public string Owner { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public string Apartment { get; set; }
        }

        public static List<CustomerData> GetPercentCorrectionCustomerData(this Entities db, int[] customerIDs, int serviceID, DateTime period)
        {
            return db.ChargeOperPoses
                .Where(p => customerIDs.Contains(p.ChargeOpers.Customers.ID) && p.ChargeOpers.ChargeSets.Period == period && p.Services.ID == serviceID)
                .Select(p => p.ChargeOpers.Customers.ID)
                .Distinct()
                .Join(
                    db.Customers,
                    x => x,
                    c => c.ID,
                    (x, c) =>
                    new CustomerData
                    {
                        ID = c.ID,
                        Account = c.Account,
                        Owner = c.OwnerType == (int)OwnerType.PhysicalPerson
                            ? c.PhysicalPersonFullName
                            : c.OwnerType == (int)OwnerType.JuridicalPerson
                                ? c.JuridicalPersonFullName
                                : "Неизвестен",
                        Street = c.Buildings.Streets.Name,
                        Building = c.Buildings.Number,
                        Apartment = c.Apartment
                    })
                .OrderBy(c => c.Street)
                .ThenBy(c => c.Building)
                .ThenBy(c => c.Apartment)
                .ToList();
        }
    }
}
