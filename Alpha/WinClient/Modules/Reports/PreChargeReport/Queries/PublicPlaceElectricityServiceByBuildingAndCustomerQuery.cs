using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PreChargeReport.Queries
{
    public static class PublicPlaceElectricityServiceByBuildingAndCustomerQuery
    {
        public class Service
        {
            public int CustomerID { get; set; }
            public string Account { get; set; }
            public string Apartment { get; set; }
            public decimal Square { get; set; }
            public int ServiceID { get; set; }
            public decimal Norm { get; set; }
            public decimal Rate { get; set; }
        }

        public class Building
        {
            public int BuildingID { get; set; }
            public string Street { get; set; }
            public string BuildingNum { get; set; }
            public IEnumerable<Service> Services { get; set; }
        }

        public static List<Building> GetPublicPlaceElectricityServiceByBuildingAndCustomer(this Entities db, 
            DateTime currentPeriod, 
            int buildingId = 0, 
            int streetId = 0)
        {
            return (buildingId > 0
                        ? db.CustomerPoses.Where(x => x.Customers.Buildings.ID == buildingId)
                        : streetId > 0 
                            ? db.CustomerPoses.Where(x => x.Customers.Buildings.Streets.ID == streetId)
                            : db.CustomerPoses)
                    .Where(x => x.Services.ServiceTypes.Code == ServiceTypeConstants.PP_ELECTRICITY && x.Till >= currentPeriod)
                    .Select(x =>
                        new
                        {
                            BuildingID = x.Customers.Buildings.ID,
                            Street = x.Customers.Buildings.Streets.Name,
                            BuildingNum = x.Customers.Buildings.Number,
                            x.Customers.Apartment,
                            x.Customers.Account,
                            x.Customers.Square,
                            CustomerID = x.Customers.ID,
                            ServiceID = x.Services.ID,
                            x.Rate,
                            x.Services.Norm
                        })
                    .GroupBy(x => new { x.BuildingID, x.Street, x.BuildingNum })
                    .Select(g =>
                        new Building
                        {
                            BuildingID = g.Key.BuildingID,
                            Street = g.Key.Street,
                            BuildingNum = g.Key.BuildingNum,
                            Services = g.Select(x =>
                                new Service
                                {
                                    CustomerID = x.CustomerID,
                                    Account = x.Account,
                                    Apartment = x.Apartment,
                                    Square = x.Square,
                                    ServiceID = x.ServiceID,
                                    Norm = x.Norm ?? 0,
                                    Rate = x.Rate
                                })
                        })
                    .ToList();
        }
    }
}
