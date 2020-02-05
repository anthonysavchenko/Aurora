using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Queries
{
    public static class GetNotActualBenefitAccountsSquareQuery
    {
        public class SquareData
        {
            public Customers ParentCustomer { get; set; }
            public decimal BenefitCustomerSquare { get; set; }
        }

        public static List<SquareData> GetNotActualBenefitAccountsSquare(
            this Entities db,
            int buildingId,
            DateTime lastChargedPeriod) =>
            db.Customers
                .Where(x => x.Buildings.ID == buildingId
                    && x.Residents.Count > 0
                    && x.Residents.All(y => y.BenefitTypes.Code == BenefitTypeCodes.CHILDREN_OF_WAR)
                    && x.CustomerPoses.Any(y => y.Till == lastChargedPeriod))
                .Select(x =>
                    new SquareData
                    {
                        ParentCustomer = db.Customers
                            .Where(y => y.Buildings.ID == x.Buildings.ID
                                && y.Apartment == x.Apartment
                                && y.Residents.All(z => z.BenefitTypes == null || z.BenefitTypes.Code != BenefitTypeCodes.CHILDREN_OF_WAR)
                                && y.CustomerPoses.Any(z => z.Till > lastChargedPeriod))
                            .FirstOrDefault(),
                        BenefitCustomerSquare = x.Square,
                    })
                .Where(x => x.ParentCustomer != null)
                .ToList();
    }
}
