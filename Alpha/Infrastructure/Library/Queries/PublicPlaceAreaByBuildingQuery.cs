using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Queries
{
    public static class PublicPlaceAreaByBuildingQuery
    {
        public static Dictionary<int, Dictionary<int, decimal>> GetPublicPlaceAreaByBuilding(this Entities db, int buildingId = 0, int streetId = 0)
        {
            return
                (buildingId > 0
                    ? db.PublicPlaces.Where(x => x.BuildingID == buildingId)
                    : streetId > 0
                        ? db.PublicPlaces.Where(x => x.Buildings.Streets.ID == streetId)
                        : db.PublicPlaces)
                .GroupBy(x => x.BuildingID)
                .Select(g =>
                    new
                    {
                        BuildingID = g.Key,
                        ByService = g.Select(x =>
                            new
                            {
                                x.ServiceID,
                                x.Area
                            })
                    })
                .ToDictionary(
                    x => x.BuildingID,
                    x => x.ByService.ToDictionary(y => y.ServiceID, y => y.Area));
        }
    }
}
