using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Queries
{
    public static class GetResidentsOfNotActualFullShareBenefitAccountsQuery
    {
        public static List<Residents> GetResidentsOfNotActualFullShareBenefitAccounts(
            this Entities db,
            int buildingId,
            List<string> actualAppartments,
            DateTime firstUnchargedPeriod) =>
            db.Residents
                .Where(x => x.Customers.Buildings.ID == buildingId
                    && !actualAppartments.Contains(x.Customers.Apartment)
                    && x.BenefitTypes.Code == BenefitTypeCodes.CHILDREN_OF_WAR
                    && x.Customers.CustomerPoses.Any(y => y.Till >= firstUnchargedPeriod))
                .ToList();
    }
}
