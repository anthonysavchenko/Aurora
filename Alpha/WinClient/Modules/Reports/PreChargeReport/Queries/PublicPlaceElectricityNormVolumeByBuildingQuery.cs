using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PreChargeReport.Queries
{
    public static class PublicPlaceElectricityNormVolumeByBuildingQuery
    {
        public static Dictionary<int, decimal> GetPublicPlaceElectricityNormVolumeByBuilding(this Entities db,
            DateTime period,
            Dictionary<int, decimal> buildingAreas,
            Dictionary<int, Dictionary<int, decimal>> ppAreaByBuilding,
            int buildingId = 0,
            int streetId = 0)
        {
            return
                (buildingId > 0
                    ? db.CustomerPoses.Where(x => x.Customers.Buildings.ID == buildingId)
                    : streetId > 0
                        ? db.CustomerPoses.Where(x => x.Customers.Buildings.Streets.ID == streetId)
                        : db.CustomerPoses)
                .Where(x => x.Services.ServiceTypes.Code == ServiceTypeConstants.PP_ELECTRICITY
                    && x.Till >= period
                    && db.PrivateCounters.All(y => y.CustomerID != x.Customers.ID && y.ServiceID != x.Services.ID))
                .Select(x =>
                    new
                    {
                        BuildingID = x.Customers.Buildings.ID,
                        x.Customers.Square,
                        ServiceID = x.Services.ID,
                        x.Services.Norm
                    })
                .ToList()
                .GroupBy(x => x.BuildingID)
                .Select(g =>
                    new
                    {
                        BuildingID = g.Key,
                        Norm = g.Sum(x =>
                            (x.Norm ?? 0)
                            * x.Square / buildingAreas[g.Key]
                            * (ppAreaByBuilding.ContainsKey(g.Key) && ppAreaByBuilding[g.Key].ContainsKey(x.ServiceID)
                                ? ppAreaByBuilding[g.Key][x.ServiceID]
                                : 0))
                    })
                .ToDictionary(x => x.BuildingID, x => x.Norm);
        }
    }
}
