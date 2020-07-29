using Taumis.Alpha.DataBase;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class PrivateCounterHandler
    {
        static public int? GetCounter(
            int customerID,
            PrivateCounterType counterType,
            string counterNumber)
        {
            using (var db = new Entities())
            {
                var counter =
                        db.PrivateCounters
                            .FirstOrDefault(x =>
                                x.Customers.ID == customerID
                                && ((counterType != PrivateCounterType.Norm
                                    && (PrivateCounterType)x.CounterType == counterType
                                    && x.Number.ToLower() == counterNumber.ToLower())
                                    || (counterType == PrivateCounterType.Norm)));

                return counter?.ID;
            }
        }

        static public int CreateCounter(
            int customerID,
            PrivateCounterType counterType,
            string counterNumber)
        {
            using (var db = new Entities())
            {
                var counter = new PrivateCounters()
                {
                    CounterType = (byte)counterType,
                    Number = counterType != PrivateCounterType.Norm ? counterNumber : null,
                    Customers = db.Customers.First(c => c.ID == customerID),
                };

                db.AddToPrivateCounters(counter);
                db.SaveChanges();

                return counter.ID;
            }
        }

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
