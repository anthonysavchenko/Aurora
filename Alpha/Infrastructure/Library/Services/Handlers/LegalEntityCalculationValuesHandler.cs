using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    public static class LegalEntityCalculationValuesHandler
    {
        static public void Delete(int formID)
        {
            using (var db = new Entities())
            {
                var values =
                    db.LegalEntityCalculationValues
                        .Where(v => v.CalculationRows.CalculationForms.ID == formID)
                        .ToList();

                values.ForEach(v =>
                    db.LegalEntityCalculationValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
