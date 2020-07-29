using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class RouteFormValueHandler
    {
        static public void CreateValue(
            Entities db,
            DateTime month,
            PrivateCounterValueType valueType,
            int? value,
            PrivateCounters counter,
            RouteFormPoses pos)
        {
            var counterValue = new RouteFormValues()
            {
                Month = month,
                ValueType = (byte)valueType,
                Value = value,
                PrivateCounters = counter,
                RouteFormPoses = pos,
            };

            db.AddToRouteFormValues(counterValue);
        }

        static public void ClearExistedValues(Entities db, Buildings building, DateTime month)
        {
            var existedValues =
                db.RouteFormValues
                    .Where(v =>
                        v.PrivateCounters.Customers.Buildings.ID == building.ID
                        && v.Month == month)
                    .ToList();

            existedValues.ForEach(v => db.RouteFormValues.DeleteObject(v));
        }
    }
}
