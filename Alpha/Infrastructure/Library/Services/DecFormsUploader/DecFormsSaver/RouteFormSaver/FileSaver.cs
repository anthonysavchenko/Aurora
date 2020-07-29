using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsSaver.RouteFormSaver
{
    static public class FileSaver
    {
        static public void SaveFile(RouteForms form, Buildings b, DateTime month)
        {
            using (var db = new Entities())
            {
                db.RouteForms.Attach(form);
                var building = db.Buildings.First(x => x.ID == b.ID);

                RouteFormValueHandler.ClearExistedValues(db, building, month);
                PrivateCounterHandler.ClearExistedCounters(db, building);
                CustomerHandler.ClearExistedCustomers(db, building);

                foreach (var row in form.RouteFormPoses)
                {
                    var customer =
                        db.Customers
                            .FirstOrDefault(c =>
                                c.Buildings.ID == building.ID
                                && c.Apartment.ToLower() == row.Apartment.ToLower())
                        ?? CustomerHandler.CreateCustomer(
                            db,
                            building,
                            row.Apartment);

                    var counterType = GetPrivateCounterType((RouteFormCounterType)row.CounterType);

                    var counter =
                        db.PrivateCounters
                            .FirstOrDefault(x =>
                                x.Customers.ID == customer.ID
                                && x.CounterType == (byte)counterType
                                && x.Number.ToLower() == row.CounterNumber.ToLower())
                        ?? PrivateCounterHandler.CreateCounter(
                            db,
                            customer,
                            counterType,
                            counterType != PrivateCounterType.Norm
                                ? row.CounterNumber
                                : null);

                    CreateValues(db, month, counter, row);
                }

                db.SaveChanges();
            }
        }

        static public Buildings GetBuilding(RouteForms form)
        {
            using (var db = new Entities())
            {
                return
                    db.Buildings
                        .FirstOrDefault(b =>
                            b.Street.ToLower() == form.Street.ToLower()
                            && b.Number.ToLower() == form.Building.ToLower());
            }
        }

        static private PrivateCounterType GetPrivateCounterType(
            RouteFormCounterType routeFormCounterType)
        {
            if (routeFormCounterType == RouteFormCounterType.Common)
            {
                return PrivateCounterType.Common;
            }
            else if (routeFormCounterType == RouteFormCounterType.DayAndNight)
            {
                return PrivateCounterType.DayAndNight;
            }
            else
            {
                return PrivateCounterType.Norm;
            }
        }

        static private void CreateValues(
            Entities db,
            DateTime month,
            PrivateCounters counter,
            RouteFormPoses pos)
        {
            if (pos.CounterType == (byte)RouteFormCounterType.Common)
            {
                RouteFormValueHandler.CreateValue(
                    db,
                    month,
                    PrivateCounterValueType.Common,
                    pos.PrevValue,
                    counter,
                    pos);
            }
            else if (pos.CounterType == (byte)RouteFormCounterType.DayAndNight)
            {
                RouteFormValueHandler.CreateValue(
                    db,
                    month,
                    PrivateCounterValueType.Day,
                    pos.PrevDayValue,
                    counter,
                    pos);
                RouteFormValueHandler.CreateValue(
                    db,
                    month,
                    PrivateCounterValueType.Night,
                    pos.PrevNightValue,
                    counter,
                    pos);
            }
            else if (pos.CounterType == (byte)RouteFormCounterType.Norm)
            {
                RouteFormValueHandler.CreateValue(
                    db,
                    month,
                    PrivateCounterValueType.Norm,
                    null,
                    counter,
                    pos);
            }
        }
    }
}
