using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class CustomerHandler
    {
        static public void ClearExistedCustomers(int buildingID)
        {
            using (var db = new Entities())
            {
                var existedCustomers =
                    db.Customers
                        .Where(c =>
                            c.Buildings.ID == buildingID
                            && c.PrivateCounters.Count == 0)
                        .ToList();

                existedCustomers.ForEach(c => db.Customers.DeleteObject(c));
                db.SaveChanges();
            }
        }
    }
}
