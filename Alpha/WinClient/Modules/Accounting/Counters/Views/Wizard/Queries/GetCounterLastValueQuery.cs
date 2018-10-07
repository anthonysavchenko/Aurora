using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Model;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Queries
{
    public static class GetCounterLastValueQuery
    {
        public static CounterLastValue GetCounterLastValue(this Entities db, int counterId)
        {
            return db.PrivateCounterValues
                .Where(x => x.PrivateCounters.ID == counterId)
                .Select(x =>
                    new CounterLastValue
                    {
                        Period = x.Period,
                        Value = x.Value
                    })
                .OrderByDescending(x => x.Period)
                .FirstOrDefault();
        }
    }
}
