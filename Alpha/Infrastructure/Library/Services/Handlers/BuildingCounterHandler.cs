using Taumis.Alpha.DataBase;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class BuildingCounterHandler
    {
        static public void ClearExistedCounters()
        {
            using (var db = new Entities())
            {
                var existedCounters =
                    db.BuildingCounters
                        .Where(c =>
                            c.UtilityService == (byte)UtilityService.Electricity
                            && c.CheckedSince == null
                            && c.CheckedTill == null
                            && c.BuildingCounterValues.Count == 0)
                        .ToList();

                existedCounters.ForEach(c => db.BuildingCounters.DeleteObject(c));

                db.SaveChanges();
            }
        }
    }
}
