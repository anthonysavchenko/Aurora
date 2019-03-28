using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Queries
{
    public static class GetBuildingsWithCountersOnStreetQuery
    {
        public static DataTable GetBuildingsWithCountersOnStreet(this Entities db, int streetId)
        {
            var values = db.PrivateCounters
                .Where(x => !x.Archived && x.Customers.Buildings.Streets.ID == streetId)
                .Select(x => x.Customers.Buildings.ID)
                .Distinct()
                .Join(db.Buildings,
                    x => x,
                    y => y.ID,
                    (x, y) =>
                        new
                        {
                            y.ID,
                            y.Number
                        })
                .ToArray()
                .OrderBy(x => x.Number, new StringWithNumbersComparer())
                .ToArray();

            DataTable table = CreateDataTable();

            foreach (var v in values)
            {
                table.Rows.Add(v.ID, v.Number);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Number");

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            return _table;
        }
    }
}
