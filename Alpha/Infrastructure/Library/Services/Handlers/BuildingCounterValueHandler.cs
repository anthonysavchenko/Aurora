using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class BuildingCounterValueHandler
    {
        static public void Delete(int formID)
        {
            using (var db = new Entities())
            {
                var values =
                    db.BuildingCounterValues
                        .Where(v => v.BuildingValuesRows.BuildingValuesForms.ID == formID)
                        .ToList();

                values.ForEach(v =>
                    db.BuildingCounterValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
