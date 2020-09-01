using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class PrivateCounterHandler
    {
        static public void ClearExistedCounters(int buildingID)
        {
            using (var db = new Entities())
            {
                var existedCounters =
                    db.PrivateCounters
                        .Where(c =>
                            c.Customers.Buildings.ID == buildingID
                            && c.RouteFormValues.Count == 0
                            && c.FillFormValues.Count == 0
                            && c.PrivateCounterValues.Count == 0)
                        .ToList();

                existedCounters.ForEach(c => db.PrivateCounters.DeleteObject(c));
                db.SaveChanges();
            }
        }
    }
}
