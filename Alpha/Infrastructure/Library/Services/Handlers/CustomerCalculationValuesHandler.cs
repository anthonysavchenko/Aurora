using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    public static class CustomerCalculationValuesHandler
    {
        static public void Delete(int formID)
        {
            using (var db = new Entities())
            {
                var values =
                    db.CustomerCalculationValues
                        .Where(v => v.CalculationRows.CalculationForms.ID == formID)
                        .ToList();

                values.ForEach(v =>
                    db.CustomerCalculationValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
