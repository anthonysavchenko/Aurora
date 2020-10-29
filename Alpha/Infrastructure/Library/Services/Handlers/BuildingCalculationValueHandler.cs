using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    public static class BuildingCalculationValueHandler
    {
        static public void Delete(int formID)
        {
            using (var db = new Entities())
            {
                var values =
                    db.BuildingCalculationValues
                        .Where(v => v.CalculationRows.CalculationForms.ID == formID)
                        .ToList();

                values.ForEach(v =>
                    db.BuildingCalculationValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
