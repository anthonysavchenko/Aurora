using System.Linq;
using System.Xml;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class CustomerHandler
    {
        static public int? GetCustomer(int buildingID, string apartment)
        {
            using (var db = new Entities())
            {
                var customer =
                    db.Customers
                        .FirstOrDefault(c =>
                            c.Buildings.ID == buildingID
                            && c.Apartment.ToLower() == apartment.ToLower());

                return customer?.ID;
            }
        }

        static public int CreateCustomer(
            int buildingID,
            string apartment)
        {
            using (var db = new Entities())
            {
                var customer = new Customers()
                {
                    Buildings = db.Buildings.First(b => b.ID == buildingID),
                    Apartment = apartment,
                };

                db.AddToCustomers(customer);
                db.SaveChanges();

                return customer.ID;
            }
        }

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
