using Taumis.Alpha.DataBase;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class PrivateCounterHandler
    {
        static public PrivateCounters CreateCounter(
            Entities db,
            Customers customer,
            PrivateCounterType counterType,
            string number)
        {
            var counter = new PrivateCounters()
            {
                CounterType = (byte)counterType,
                Number = number,
                Customers = customer,
            };

            db.AddToPrivateCounters(counter);

            return counter;
        }

        static public void ClearExistedCounters(Entities db, Buildings building)
        {
            var existedCounters =
                db.PrivateCounters
                    .Where(c =>
                        c.Customers.Buildings.ID == building.ID
                        && c.RouteFormValues.Count == 0
                        && c.FillFormValues.Count == 0)
                    .ToList();

            existedCounters.ForEach(c => db.PrivateCounters.DeleteObject(c));
        }
    }
}
