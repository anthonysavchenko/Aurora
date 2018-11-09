using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PreChargeReport.Queries
{
    public static class PublicPlaceElectricityCounterVolumeByBuildingQuery
    {
        public static Dictionary<int, decimal> GetPublicPlaceElectricityCounterVolumeByBuilding(this Entities db, 
            DateTime curPeriod, 
            DateTime prevPeriod, 
            int buildingId = 0, 
            int streetId = 0)
        {
            return
                (buildingId > 0
                    ? db.PrivateCounterValues.Where(x => x.PrivateCounters.Customers.Buildings.ID == buildingId)
                    : streetId > 0
                        ? db.PrivateCounterValues.Where(x => x.PrivateCounters.Customers.Buildings.Streets.ID == streetId)
                        : db.PrivateCounterValues)
                .Where(x => x.Period == curPeriod)
                .GroupJoin(db.PrivateCounterValues.Where(x => x.Period == prevPeriod),
                    cur => cur.PrivateCounters.ID,
                    prev => prev.PrivateCounters.ID,
                    (cur, prev) => new { cur, prev })
                .SelectMany(
                    x => x.prev.DefaultIfEmpty(),
                    (x, prev) =>
                        new
                        {
                            BuildingID = x.cur.PrivateCounters.Customers.Buildings.ID,
                            Volume = x.cur.Value - ((decimal?)prev.Value ?? 0)
                        })
                .GroupBy(x => x.BuildingID)
                .Select(g =>
                    new
                    {
                        BuildingID = g.Key,
                        CountersVolume = g.Sum(x => x.Volume)
                    })
                .ToDictionary(x => x.BuildingID, x => x.CountersVolume);
        }
    }
}
