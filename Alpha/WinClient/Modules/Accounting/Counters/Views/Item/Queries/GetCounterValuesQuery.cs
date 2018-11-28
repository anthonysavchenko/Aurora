using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Queries
{
    public static class GetCounterValuesQuery
    {
        public static DataTable GetCounterValues(this Entities db, int counterId)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("CollectDate", typeof(DateTime));
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("Value", typeof(decimal));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            var _values = db.PrivateCounterValues
                .Where(x => x.PrivateCounters.ID == counterId)
                .Select(x =>
                    new
                    {
                        x.ID,
                        x.CollectDate,
                        x.Period,
                        x.Value
                    })
                .OrderBy(x => x.Period)
                .ToList();

            foreach (var _value in _values)
            {
                _table.Rows.Add(
                    _value.ID.ToString(),
                    _value.CollectDate,
                    _value.Period,
                    _value.Value);
            }

            return _table;
        }
    }
}
