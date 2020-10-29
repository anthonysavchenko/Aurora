using Taumis.Alpha.DataBase;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class BuildingCounterHandler
    {
        static public void DeleteWithNoValues()
        {
            using (var db = new Entities())
            {
                var counters =
                    db.BuildingCounters
                        .Where(c =>
                            c.UtilityService == (byte)UtilityService.Electricity
                            && c.CheckedSince == null
                            && c.CheckedTill == null
                            && c.BuildingCounterValues.Count == 0
                            && c.BuildingCounterCalculationValues.Count == 0)
                        .ToList();

                counters.ForEach(c =>
                    db.BuildingCounters.DeleteObject(c));

                db.SaveChanges();
            }
        }
    }
}
