using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services
{
    public class Cache : ICache
    {
        private Dictionary<int, Dictionary<int, decimal>> _ppServiceVolumeCache;
        private Dictionary<int, Dictionary<int, decimal>> _ppAreaCache;
        private Dictionary<int, decimal> _buildingAreaCache;

        public void Init(DateTime period)
        {
            using (Entities _db = new Entities())
            {
                _ppAreaCache = _db.PublicPlaces
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
                        x => x.ByService.ToDictionary(
                            y => y.ServiceID,
                            y => y.Area));

                _ppServiceVolumeCache = _db.PublicPlaceServiceVolumes
                    .Where(x => x.Period == period)
                    .GroupBy(x => x.BuildingID)
                    .Select(g =>
                        new
                        {
                            BuildingID = g.Key,
                            ByService = g.Select(x =>
                                new
                                {
                                    x.ServiceID,
                                    x.Volume
                                })
                        })
                    .ToDictionary(
                        x => x.BuildingID,
                        x => x.ByService.ToDictionary(
                            y => y.ServiceID,
                            y => y.Volume));

                _buildingAreaCache = _db.Buildings
                    .Select(b =>
                        new
                        {
                            b.ID,
                            Area = 
                                (b.Customers.Where(c => c.CustomerPoses.Any(p => p.Till >= period)).Sum(c => (decimal?)c.Square) ?? 0) 
                                + b.NonResidentialPlaceArea,
                        })
                    .ToDictionary(b => b.ID, b => b.Area);
            }
        }

        public decimal GetPublicPlaceServiceVolume(int buildingId, int serviceId)
        {
            return
                _ppServiceVolumeCache.ContainsKey(buildingId) &&
                _ppServiceVolumeCache[buildingId].ContainsKey(serviceId)
                    ? _ppServiceVolumeCache[buildingId][serviceId] : 0;
        }

        public decimal GetPublicPlaceArea(int buildingId, int serviceId)
        {
            return _ppAreaCache.ContainsKey(buildingId) && _ppAreaCache[buildingId].ContainsKey(serviceId)
                ? _ppAreaCache[buildingId][serviceId] : 0;
        }

        public decimal GetBuildingArea(int buildingId)
        {
            return _buildingAreaCache.ContainsKey(buildingId) ? _buildingAreaCache[buildingId] : 0;
        }
    }
}
