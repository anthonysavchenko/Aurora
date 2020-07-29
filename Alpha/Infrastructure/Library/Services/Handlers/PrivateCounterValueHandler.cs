using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class PrivateCounterValueHandler
    {
        static public void CreateValue(
            DateTime month,
            PrivateCounterValueType valueType,
            int? value,
            int counterID,
            PrivateValuesFormPoses pos)
        {
            using (var db = new Entities())
            {
                db.PrivateValuesFormPoses.Attach(pos);

                var counterValue = new PrivateCounterValues()
                {
                    Month = month,
                    ValueType = (byte)valueType,
                    Value = value,
                    PrivateCounters = db.PrivateCounters.First(c => c.ID == counterID),
                    PrivateValuesFormPoses = pos,
                };

                db.AddToPrivateCounterValues(counterValue);
                db.SaveChanges();
            }
        }

        static public void ClearExistedValues(int buildingID, DateTime month)
        {
            using (var db = new Entities())
            {
                var existedValues =
                    db.PrivateCounterValues
                        .Where(v =>
                            v.PrivateCounters.Customers.Buildings.ID == buildingID
                            && v.Month == month)
                        .ToList();

                existedValues.ForEach(v => db.PrivateCounterValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
