using System;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class BuildingCounterValueHandler
    {
        static public void ClearExistedValues(DateTime month)
        {
            using (var db = new Entities())
            {
                var existedValues =
                    db.BuildingCounterValues
                        .Where(v => v.Month == month)
                        .ToList();

                existedValues.ForEach(v => db.BuildingCounterValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
