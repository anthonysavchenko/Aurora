using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Queries
{
    public static class GetCustomerCountersQuery
    {
        public static DataTable GetCustomerCounters(this Entities db, int customerId)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Number");
            _table.Columns.Add("Model");

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            db.PrivateCounters
                .Where(x => x.CustomerID == customerId)
                .Select(x =>
                    new
                    {
                        x.ID,
                        x.Number,
                        x.Model
                    })
                .ToList()
                .ForEach(x => _table.Rows.Add(x.ID, x.Number, x.Model));

            return _table;
        }
    }
}
