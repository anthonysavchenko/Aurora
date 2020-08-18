using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsSaver.RouteFormSaver
{
    static public class FileSaver
    {
        static public int? GetBuilding(RouteForms form)
        {
            using (var db = new Entities())
            {
                var building = 
                    db.Buildings
                        .FirstOrDefault(b =>
                            b.Street.ToLower() == form.Street.ToLower()
                            && b.Number.ToLower() == form.Building.ToLower());

                return building?.ID;
            }
        }

        static public void SaveFile(int routeFormID, int buildingID, DateTime month)
        {
            RouteFormValueHandler.ClearExistedValues(buildingID, month);
            PrivateCounterHandler.ClearExistedCounters(buildingID);
            CustomerHandler.ClearExistedCustomers(buildingID);

            CreateNewCustomers(buildingID, routeFormID);
            CreateNewCounters(buildingID, routeFormID);
            CreateNewValues(buildingID, routeFormID, month);
        }

        static private void CreateNewCustomers(int buildingID, int routeFormID)
        {
            using (var db = new Entities())
            {
                var building = db.Buildings.First(b => b.ID == buildingID);
                var newCustomers =
                    db.RouteFormPoses
                        .Where(p => p.RouteForms.ID == routeFormID)
                        .Select(p => new
                        {
                            Apartment = p.Apartment.ToLower(),
                        })
                        .GroupBy(p => new
                        {
                            p.Apartment,
                        })
                        .Select(g => g.Key)
                        .Where(g =>
                            db.Customers
                                .Count(c =>
                                    c.Buildings.ID == buildingID
                                    && c.Apartment.Equals(g.Apartment, StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                newCustomers.ForEach(
                    c => db.Customers.AddObject(
                        new Customers()
                        {
                            Buildings = building,
                            Apartment = c.Apartment,
                        }));
                db.SaveChanges();
            }
        }

        static private void CreateNewCounters(int buildingID, int routeFormID)
        {
            using (var db = new Entities())
            {
                var newCounters =
                    db.RouteFormPoses
                        .Where(p => p.RouteForms.ID == routeFormID)
                        .Select(p => new
                        {
                            Customer = db.Customers
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == buildingID
                                    && c.Apartment.Equals(p.Apartment, StringComparison.OrdinalIgnoreCase)),
                            CounterType = (RouteFormCounterType)p.CounterType == RouteFormCounterType.Common
                                ? PrivateCounterType.Common
                                : (RouteFormCounterType)p.CounterType == RouteFormCounterType.DayAndNight
                                    ? PrivateCounterType.DayAndNight
                                    : PrivateCounterType.Norm,
                            CounterNumber = p.CounterNumber.ToUpper(),
                        })
                        .GroupBy(p => new
                        {
                            p.Customer,
                            p.CounterType,
                            p.CounterNumber,
                        })
                        .Select(g => g.Key)
                        .Where(g =>
                            db.PrivateCounters
                                .Count(c =>
                                    c.Customers.ID == g.Customer.ID
                                    && (g.CounterType != PrivateCounterType.Norm
                                        && (PrivateCounterType)c.CounterType == g.CounterType
                                        && c.Number.Equals(g.CounterNumber, StringComparison.OrdinalIgnoreCase)
                                        || (g.CounterType == PrivateCounterType.Norm
                                            && (PrivateCounterType)c.CounterType == PrivateCounterType.Norm))) == 0)
                        .ToList();

                newCounters.ForEach(
                    c => db.PrivateCounters.AddObject(
                        new PrivateCounters()
                        {
                            Customers = c.Customer,
                            CounterType = (byte)c.CounterType,
                            Number = c.CounterType != PrivateCounterType.Norm ? c.CounterNumber : null,
                        }));
                db.SaveChanges();
            }
        }

        static public void CreateNewValues(int buildingID, int routeFormID, DateTime month)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.RouteFormPoses
                        .Where(p => p.RouteForms.ID == routeFormID)
                        .Select(p => new
                        {
                            Customer = db.Customers
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == buildingID
                                    && c.Apartment.Equals(p.Apartment, StringComparison.OrdinalIgnoreCase)),
                            CounterType = (RouteFormCounterType)p.CounterType == RouteFormCounterType.Common
                                ? PrivateCounterType.Common
                                : (RouteFormCounterType)p.CounterType == RouteFormCounterType.DayAndNight
                                    ? PrivateCounterType.DayAndNight
                                    : PrivateCounterType.Norm,
                            p.CounterNumber,
                            RouteFormCounterType = (RouteFormCounterType)p.CounterType,
                            Pos = p,
                        })
                        .Select(p => new
                        {
                            Counter = db.PrivateCounters
                                .FirstOrDefault(c =>
                                    c.Customers.ID == p.Customer.ID
                                    && ((p.CounterType != PrivateCounterType.Norm
                                        && (PrivateCounterType)c.CounterType == p.CounterType
                                        && c.Number.Equals(p.CounterNumber, StringComparison.OrdinalIgnoreCase))
                                        || (p.CounterType == PrivateCounterType.Norm
                                            && (PrivateCounterType)c.CounterType == PrivateCounterType.Norm))),
                            p.RouteFormCounterType,
                            p.Pos,
                        })
                        .ToList();

                foreach (var value in newValues)
                {
                    if (value.RouteFormCounterType == RouteFormCounterType.Common)
                    {
                        db.RouteFormValues.AddObject(
                            new RouteFormValues()
                            {
                                Month = month,
                                ValueType = (byte)PrivateCounterValueType.Common,
                                Value = (int?)decimal.Truncate(value.Pos.PrevValue.Value),
                                PrivateCounters = value.Counter,
                                RouteFormPoses = value.Pos,
                            });
                    }
                    else if (value.RouteFormCounterType == RouteFormCounterType.DayAndNight)
                    {
                        db.RouteFormValues.AddObject(
                            new RouteFormValues()
                            {
                                Month = month,
                                ValueType = (byte)PrivateCounterValueType.Day,
                                Value = (int?)decimal.Truncate(value.Pos.PrevDayValue.Value),
                                PrivateCounters = value.Counter,
                                RouteFormPoses = value.Pos,
                            });
                        db.RouteFormValues.AddObject(
                            new RouteFormValues()
                            {
                                Month = month,
                                ValueType = (byte)PrivateCounterValueType.Night,
                                Value = (int?)decimal.Truncate(value.Pos.PrevNightValue.Value),
                                PrivateCounters = value.Counter,
                                RouteFormPoses = value.Pos,
                            });
                    }
                    else if (value.RouteFormCounterType == RouteFormCounterType.Norm)
                    {
                        db.RouteFormValues.AddObject(
                            new RouteFormValues()
                            {
                                Month = month,
                                ValueType = (byte)PrivateCounterValueType.Norm,
                                Value = null,
                                PrivateCounters = value.Counter,
                                RouteFormPoses = value.Pos,
                            });
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
