using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Queries
{
    public static class BuildingAreaQuery
    {
        public static Dictionary<int, decimal> GetBuildingArea(this Entities db, DateTime curPeriod, int buildingId = 0, int streetId = 0)
        {
            return
                (buildingId > 0
                    ? db.Buildings.Where(x => x.ID == buildingId)
                    : streetId > 0
                        ? db.Buildings.Where(x => x.Streets.ID == streetId)
                        : db.Buildings)
                .Select(b =>
                    new
                    {
                        b.ID,
                        Area = (b.Customers.Where(c => c.CustomerPoses.Any(p => p.Till >= curPeriod)).Sum(c => (decimal?)c.Square) ?? 0)
                            + b.NonResidentialPlaceArea,
                    })
                .ToDictionary(b => b.ID, b => b.Area);
        }
    }
}
