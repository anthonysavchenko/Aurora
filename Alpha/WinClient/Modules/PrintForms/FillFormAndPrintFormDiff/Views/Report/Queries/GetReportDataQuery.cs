using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.FillFormAndPrintFormDiff.DataSets;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.FillFormAndPrintFormDiff.Views.Report.Queries
{
    public static class GetReportDataQuery
    {
        public static CollectFormDataSet GetReportData(this Entities db, int districtId)
        {
            var _data = new CollectFormDataSet();

            DataTable _headerTable = _data.Tables["HeaderTable"];
            DataTable _detailTable = _data.Tables["DetailTable"];

            _headerTable.Rows.Add("Февраль 2020");
            _detailTable.Rows.Add(
                "1234-567-8",
                "Иванов",
                "Океанский проспект 136 - 30",
                "1234",
                "",
                "18.02.2020",
                "500");

            /*
            var _comparer = new StringWithNumbersComparer();

            using (var _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _district = _db.CounterValueCollectDistricts.First(x => x.ID == districtId);

                _headerTable.Rows.Add(period.ToString("MMMM yyyy"), _district.Name);

                var _counters = _db.PrivateCounters
                    .Where(x =>
                        x.Customers.Buildings.CounterValueCollectDistrictID != null
                        && x.Customers.Buildings.CounterValueCollectDistrictID.Value == districtId)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Customers.Account,
                            Owner = x.Customers.OwnerType == (int)OwnerType.PhysicalPerson
                                ? x.Customers.PhysicalPersonFullName
                                : x.Customers.JuridicalPersonFullName,
                            Address =
                                x.Customers.Buildings.Streets.Name + ", " +
                                x.Customers.Buildings.Number + ", кв" +
                                x.Customers.Apartment,
                            Street = x.Customers.Buildings.Streets.Name,
                            Building = x.Customers.Buildings.Number,
                            x.Customers.Apartment,
                            CounterNumber = x.Number,
                            LastValueDate = x.PrivateCounterValues.OrderByDescending(y => y.CollectDate).FirstOrDefault().CollectDate,
                            LastValue = x.PrivateCounterValues.OrderByDescending(y => y.CollectDate).FirstOrDefault().Value
                        })
                    .ToList()
                    .OrderBy(x => x.Street)
                    .ThenBy(x => x.Building, _comparer)
                    .ThenBy(x => x.Apartment, _comparer);

                foreach (var _counter in _counters)
                {
                    _detailTable.Rows.Add(
                        _counter.Account,
                        _counter.Owner,
                        $"{_counter.Street}, {_counter.Building}, кв. {_counter.Apartment}",
                        _counter.CounterNumber,
                        string.Empty,
                        _counter.LastValueDate.ToString("dd.MM.yyyy"),
                        ((int)_counter.LastValue).ToString());
                }
            }
            */

            return _data;
        }
    }
}
