using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class RouteFormValueHandler
    {
        static public void CreateValue(
            DateTime month,
            PrivateCounterValueType valueType,
            int? value,
            int counterID,
            RouteFormPoses pos)
        {
            using (var db = new Entities())
            {
                db.RouteFormPoses.Attach(pos);

                var counterValue = new RouteFormValues()
                {
                    Month = month,
                    ValueType = (byte)valueType,
                    Value = value,
                    PrivateCounters = db.PrivateCounters.First(c => c.ID == counterID),
                    RouteFormPoses = pos,
                };

                db.AddToRouteFormValues(counterValue);
                db.SaveChanges();
            }
        }

        static public void ClearExistedValues(int buildingID, DateTime month)
        {
            using (var db = new Entities())
            {
                var existedValues =
                    db.RouteFormValues
                        .Where(v =>
                            v.PrivateCounters.Customers.Buildings.ID == buildingID
                            && v.Month == month)
                        .ToList();

                existedValues.ForEach(v => db.RouteFormValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
