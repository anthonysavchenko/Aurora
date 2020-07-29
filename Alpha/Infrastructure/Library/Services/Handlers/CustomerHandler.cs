using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class CustomerHandler
    {
        static public Customers CreateCustomer(
            Entities db,
            Buildings building,
            string apartment)
        {
            var customer = new Customers()
            {
                Buildings = building,
                Apartment = apartment,
            };

            db.AddToCustomers(customer);

            return customer;
        }

        static public void ClearExistedCustomers(Entities db, Buildings building)
        {
            var existedCustomers =
                db.Customers
                    .Where(c =>
                        c.Buildings.ID == building.ID
                        && c.PrivateCounters.Count == 0)
                    .ToList();

            existedCustomers.ForEach(c => db.Customers.DeleteObject(c));
        }
    }
}
