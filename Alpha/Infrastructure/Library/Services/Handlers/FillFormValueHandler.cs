using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class FillFormValueHandler
    {
        static public void CreateValue(
            DateTime month,
            PrivateCounterValueType valueType,
            int? value,
            int counterID,
            FillFormPoses pos)
        {
            using (var db = new Entities())
            {
                db.FillFormPoses.Attach(pos);

                var counterValue = new FillFormValues()
                {
                    Month = month,
                    ValueType = (byte)valueType,
                    Value = value,
                    PrivateCounters = db.PrivateCounters.First(c => c.ID == counterID),
                    FillFormPoses = pos,
                };

                db.AddToFillFormValues(counterValue);
                db.SaveChanges();
            }
        }

        static public void ClearExistedValues(int buildingID, DateTime month)
        {
            using (var db = new Entities())
            {
                var existedValues =
                    db.FillFormValues
                        .Where(v =>
                            v.PrivateCounters.Customers.Buildings.ID == buildingID
                            && v.Month == month)
                        .ToList();

                existedValues.ForEach(v => db.FillFormValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
