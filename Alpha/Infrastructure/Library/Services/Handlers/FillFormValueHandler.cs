using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class FillFormValueHandler
    {
        static public void CreateValue(
            Entities db,
            DateTime month,
            PrivateCounterValueType valueType,
            int? value,
            PrivateCounters counter,
            FillFormPoses pos)
        {
            var counterValue = new FillFormValues()
            {
                Month = month,
                ValueType = (byte)valueType,
                Value = value,
                PrivateCounters = counter,
                FillFormPoses = pos,
            };

            db.AddToFillFormValues(counterValue);
        }

        static public void ClearExistedValues(Entities db, Buildings building, DateTime month)
        {
            var existedValues =
                db.FillFormValues
                    .Where(v =>
                        v.PrivateCounters.Customers.Buildings.ID == building.ID
                        && v.Month == month)
                    .ToList();

            existedValues.ForEach(v => db.FillFormValues.DeleteObject(v));
        }
    }
}
