using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services
{
    public interface ICache
    {
        void Init(DateTime period);
        decimal GetPublicPlaceServiceVolume(int buildingId, int serviceId);
        decimal GetPublicPlaceArea(int buildingId, int serviceId);
        decimal GetBuildingArea(int buildingId);
    }
}
