using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.PrivateValuesUploader.PrivateValuesSaver.PrivateValuesFormSaver
{
    static public class FileSaver
    {
        static public int? GetBuilding(PrivateValuesForms form)
        {
            using (var db = new Entities())
            {
                var building =
                    db.Buildings
                        .FirstOrDefault(b =>
                            b.Street.Equals(form.Street, StringComparison.OrdinalIgnoreCase)
                            && b.Number.Equals(form.Building, StringComparison.OrdinalIgnoreCase));

                return building?.ID;
            }
        }

        static public void SaveFile(int formID, int buildingID, DateTime month)
        {
            PrivateCounterValueHandler.ClearExistedValues(buildingID, month);
            PrivateCounterHandler.ClearExistedCounters(buildingID);
            CustomerHandler.ClearExistedCustomers(buildingID);

            CreateNewCustomers(buildingID, formID);
            CreateNewCounters(buildingID, formID);
            CreateNewValues(buildingID, formID, month);
        }

        static private void CreateNewCustomers(int buildingID, int formID)
        {
            using (var db = new Entities())
            {
                var building = db.Buildings.First(b => b.ID == buildingID);
                var newCustomers =
                    db.PrivateValuesFormPoses
                        .Where(p => p.PrivateValuesForms.ID == formID)
                        .Select(p => new
                        {
                            Apartment = p.Apartment.ToLower(),
                            Account = p.Account.ToLower(),
                        })
                        .GroupBy(p => new
                        {
                            p.Apartment,
                            p.Account,
                        })
                        .Select(g => g.Key)
                        .Where(g =>
                            db.Customers
                                .Count(c =>
                                    c.Buildings.ID == buildingID
                                    && c.Apartment.Equals(g.Apartment, StringComparison.OrdinalIgnoreCase)
                                    && c.Account.Equals(g.Account, StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                newCustomers.ForEach(
                    c => db.Customers.AddObject(
                        new Customers()
                        {
                            Buildings = building,
                            Apartment = c.Apartment,
                            Account = c.Account,
                        }));
                db.SaveChanges();
            }
        }

        static private void CreateNewCounters(int buildingID, int formID)
        {
            using (var db = new Entities())
            {
                var newCounters =
                    db.PrivateValuesFormPoses
                        .Where(p => p.PrivateValuesForms.ID == formID)
                        .Select(p => new
                        {
                            Customer = db.Customers
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == buildingID
                                    && c.Apartment.Equals(p.Apartment, StringComparison.OrdinalIgnoreCase)
                                    && c.Account.Equals(p.Account, StringComparison.OrdinalIgnoreCase)),
                            CounterType = (FillFormCounterType)p.CounterType == FillFormCounterType.Common
                                ? PrivateCounterType.Common
                                : (FillFormCounterType)p.CounterType == FillFormCounterType.Day
                                    || (FillFormCounterType)p.CounterType == FillFormCounterType.Night
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

        static private void CreateNewValues(int buildingID, int formID, DateTime month)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.PrivateValuesFormPoses
                        .Where(p => p.PrivateValuesForms.ID == formID)
                        .Select(p => new
                        {
                            Customer = db.Customers
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == buildingID
                                    && c.Apartment.Equals(p.Apartment, StringComparison.OrdinalIgnoreCase)
                                    && c.Account.Equals(p.Account, StringComparison.OrdinalIgnoreCase)),
                            CounterType = (PrivateFormCounterType)p.CounterType == PrivateFormCounterType.Common
                                ? PrivateCounterType.Common
                                : (PrivateFormCounterType)p.CounterType == PrivateFormCounterType.Day
                                    || (PrivateFormCounterType)p.CounterType == PrivateFormCounterType.Night
                                    ? PrivateCounterType.DayAndNight
                                    : PrivateCounterType.Norm,
                            p.CounterNumber,
                            PrivateValuesFormCounterType = (PrivateFormCounterType)p.CounterType,
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
                            p.PrivateValuesFormCounterType,
                            p.Pos,
                        })
                        .ToList();

                foreach (var value in newValues)
                {
                    if (value.PrivateValuesFormCounterType == PrivateFormCounterType.Common)
                    {
                        db.PrivateCounterValues.AddObject(
                            new PrivateCounterValues()
                            {
                                Month = month,
                                ValueType = (byte)PrivateCounterValueType.Common,
                                Value = value.Pos.CurrentValue.Value,
                                PrivateCounters = value.Counter,
                                PrivateValuesFormPoses = value.Pos,
                            });
                    }
                    else if (value.PrivateValuesFormCounterType == PrivateFormCounterType.Day)
                    {
                        db.PrivateCounterValues.AddObject(
                            new PrivateCounterValues()
                            {
                                Month = month,
                                ValueType = (byte)PrivateCounterValueType.Day,
                                Value = value.Pos.CurrentDayValue.Value,
                                PrivateCounters = value.Counter,
                                PrivateValuesFormPoses = value.Pos,
                            });
                    }
                    else if (value.PrivateValuesFormCounterType == PrivateFormCounterType.Night)
                    {
                        db.PrivateCounterValues.AddObject(
                            new PrivateCounterValues()
                            {
                                Month = month,
                                ValueType = (byte)PrivateCounterValueType.Night,
                                Value = value.Pos.CurrentNightValue.Value,
                                PrivateCounters = value.Counter,
                                PrivateValuesFormPoses = value.Pos,
                            });
                    }
                    else if (value.PrivateValuesFormCounterType == PrivateFormCounterType.Norm)
                    {
                        db.PrivateCounterValues.AddObject(
                            new PrivateCounterValues()
                            {
                                Month = month,
                                ValueType = (byte)PrivateCounterValueType.Norm,
                                Value = null,
                                PrivateCounters = value.Counter,
                                PrivateValuesFormPoses = value.Pos,
                            });
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
