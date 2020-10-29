using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    public static class LegalEntityHandler
    {
        static public void DeleteWithNoValues()
        {
            using (var db = new Entities())
            {
                var entities =
                    db.LegalEntities
                        .Where(c => c.LegalEntityCalculationValues.Count == 0)
                        .ToList();

                entities.ForEach(c =>
                    db.LegalEntities.DeleteObject(c));

                db.SaveChanges();
            }
        }
    }
}
