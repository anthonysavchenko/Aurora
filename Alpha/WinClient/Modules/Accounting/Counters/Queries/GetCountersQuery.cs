using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Constants;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Queries
{
    public static class GetCountersQuery
    {
        public static DataTable GetCounters(this Entities db, int buildingId, DateTime currentPeriod)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add(WizardTableColumnNames.ID, typeof(int));
            _table.Columns.Add(WizardTableColumnNames.ACCOUNT);
            _table.Columns.Add(WizardTableColumnNames.APARTMENT);
            _table.Columns.Add(WizardTableColumnNames.FULL_NAME);
            _table.Columns.Add(WizardTableColumnNames.COUNTER_NUM);
            _table.Columns.Add(WizardTableColumnNames.COUNTER_MODEL);
            _table.Columns.Add(WizardTableColumnNames.PREV_PERIOD, typeof(DateTime));
            _table.Columns.Add(WizardTableColumnNames.PREV_COLLECT_DATE, typeof(DateTime));
            _table.Columns.Add(WizardTableColumnNames.PREV_VALUE, typeof(decimal));
            _table.Columns.Add(WizardTableColumnNames.VALUE, typeof(decimal));
            _table.Columns.Add(WizardTableColumnNames.ERROR_MESSAGE);
            _table.PrimaryKey = new DataColumn[] { _table.Columns[WizardTableColumnNames.ID] };

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            db.PrivateCounters
                .Where(x => !x.Archived && x.Customers.Buildings.ID == buildingId)
                .Select(x =>
                    new
                    {
                        x.ID,
                        x.Customers.Account,
                        x.Customers.Apartment,
                        FullName = x.Customers.OwnerType == (int)OwnerType.PhysicalPerson
                            ? x.Customers.PhysicalPersonFullName
                            : x.Customers.JuridicalPersonFullName,
                        x.Number,
                        x.Model,
                        PrevValue = x.PrivateCounterValues
                            .Where(y => y.Period < currentPeriod)
                            .OrderByDescending(y => y.Period)
                            .FirstOrDefault()
                    })
                .ToList()
                .OrderBy(x => x.Apartment, new StringWithNumbersComparer())
                .ThenBy(x => x.Number)
                .ToList()
                .ForEach(x => _table.Rows.Add(
                    x.ID,
                    x.Account,
                    x.Apartment,
                    x.FullName,
                    x.Number,
                    x.Model,
                    x.PrevValue?.Period ?? DateTime.MinValue,
                    x.PrevValue?.CollectDate ?? DateTime.MinValue,
                    x.PrevValue?.Value ?? 0,
                    DBNull.Value,
                    string.Empty));

            return _table;
        }
    }
}
