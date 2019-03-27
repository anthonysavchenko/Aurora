using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Queries
{
    public static class GetCountersStreetsQuery
    {
        public static DataTable GetCountersStreets(this Entities db)
        {
            var values = db.PrivateCounters
                .Where(x => !x.Archived)
                .Select(x => x.Customers.Buildings.Streets.ID)
                .Distinct()
                .Join(db.Streets,
                    x => x,
                    y => y.ID,
                    (x, y) =>
                        new
                        {
                            y.ID,
                            y.Name
                        })
                .OrderBy(x => x.Name)
                .ToArray();

            DataTable table = CreateDataTable();

            foreach (var v in values)
            {
                table.Rows.Add(
                    v.ID.ToString(),
                    v.Name);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Name");

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
