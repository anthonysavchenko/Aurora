using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Top;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.List.Queries
{
    public static class GetCoutersDataTableQuery
    {
        public static DataTable GetCountersDataTable(
            this Entities db,
            DateTime currentPeriod,
            FilterType filterType,
            string account,
            string street,
            string building,
            string apartment,
            string zipCode,
            bool showOnlyWoPeriodValues)
        {
            DataTable _table = CreateDataTable();

            var _raw = db.PrivateCounters
                .Where(x => !showOnlyWoPeriodValues || x.PrivateCounterValues.All(y => y.Period != currentPeriod));

            switch (filterType)
            {
                case FilterType.Account:
                    if (!string.IsNullOrEmpty(account))
                    {
                        _raw = _raw.Where(x => x.Customers.Account.Contains(account));
                    }
                    break;

                case FilterType.Address:
                    if (!string.IsNullOrEmpty(street))
                    {
                        _raw = _raw.Where(x => x.Customers.Buildings.Streets.Name.Contains(street));
                    }

                    if (!string.IsNullOrEmpty(building))
                    {
                        _raw = _raw.Where(x => x.Customers.Buildings.Number.Contains(building));
                    }

                    if (!string.IsNullOrEmpty(apartment))
                    {
                        _raw = _raw.Where(x => x.Customers.Apartment.Contains(apartment));
                    }

                    break;

                case FilterType.ZipCode:
                    if (!string.IsNullOrEmpty(zipCode))
                    {
                        _raw = _raw.Where(x => x.Customers.Buildings.ZipCode.Contains(zipCode));
                    }
                    break;
            }

            var _counters = _raw
                .Select(x =>
                    new
                    {
                        x.ID,
                        x.Number,
                        Service = x.Services.Name,
                        x.Customers.Account,
                        Address = x.Customers.Buildings.Streets.Name + ", " + x.Customers.Buildings.Number,
                        IsPrivate = true
                    })
                .OrderBy(x => x.Address)
                .ThenBy(x => x.Account)
                .ToList();

            foreach (var _c in _counters)
            {
                _table.Rows.Add(
                    _c.ID.ToString(),
                    _c.Number,
                    _c.Service,
                    _c.Account,
                    _c.Address);
            }

            return _table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Number");
            _table.Columns.Add("Service");
            _table.Columns.Add("Account");
            _table.Columns.Add("Address");

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
