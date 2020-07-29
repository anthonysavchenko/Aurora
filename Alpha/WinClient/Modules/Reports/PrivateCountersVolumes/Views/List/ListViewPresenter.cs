using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PrivateCountersVolumes.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private static class ColumnNames
        {
            public const string APARTMENT_COLUMN = "Apartment";
            public const string COUNTER_MODEL_COLUMN = "CounterModel";
            public const string COUNTER_NUMBER_COLUMN = "CounterNumber";
        }

        private class StringAsNumbersComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                int _x;
                int _y;

                int.TryParse(x, out _x);
                int.TryParse(y, out _y);

                return _x - _y;
            }
        }

        public override void OnViewReady()
        {
            base.OnViewReady();

            DataTable _buildings = new DataTable();

            _buildings.Columns.Add("ID", typeof(string));
            _buildings.Columns.Add("Building", typeof(string));

            using (Entities _entities = new Entities())
            {
                var _buildingList =
                    _entities.Buildings
                        .ToList()
                        .Select(x =>
                            new
                            {
                                x.ID,
                                Building = $"{x.Street}, д. {x.Number}",
                            });

                foreach (var _building in _buildingList)
                {
                    _buildings.Rows.Add(_building.ID.ToString(), _building.Building);
                }
            }

            View.Buildings = _buildings;

            DateTimeInfo _dateTimeInfo = ServerTime.GetDateTimeInfo();

            View.Since = _dateTimeInfo.SinceMonthBeginning;
            View.Till = _dateTimeInfo.TillToday;
        }

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            View.ClearColumns();

            AddColumnsToView(GetColumnBands());

            base.ProcessGridData();
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            var table = new DataTable();

            var bands = GetColumnBands();
            AddColumnsToTable(table, bands);

            using (var db = new Entities())
            {
                var items =
                    db.PrivateCounters
                        .Where(c =>
                            c.Customers.Buildings.ID.ToString() == View.BuildingId)
                        .Select(c =>
                            new
                            {
                                c.Customers.Apartment,
                                c.Model,
                                c.Number,
                                CounterType = (PrivateCounterType)c.CounterType,
                                RFValues = c.RouteFormValues
                                    .GroupBy(x => x.ValueType)
                                    .Select(byValueType =>
                                        new
                                        {
                                            ValueType = byValueType.Key,
                                            Months = byValueType
                                                .GroupBy(y => y.Month)
                                                .Select(byMonth =>
                                                    new
                                                    {
                                                        Month = byMonth.Key,
                                                        Value = byMonth.Max(z => z.Value),
                                                    })
                                                .ToList(),
                                        })
                                    .ToList(),
                                FFValues = c.FillFormValues
                                    .GroupBy(x => x.ValueType)
                                    .Select(byValueType =>
                                        new
                                        {
                                            ValueType = byValueType.Key,
                                            Months = byValueType
                                                .GroupBy(y => y.Month)
                                                .Select(byMonth =>
                                                    new
                                                    {
                                                        Month = byMonth.Key,
                                                        Value = byMonth.Max(z => z.Value),
                                                    })
                                                .ToList(),
                                        })
                                    .ToList(),
                                /*LastRouteFormValue = c.RouteFormValues
                                    .FirstOrDefault(v => v.Month == c.RouteFormValues.Max(vv => vv.Month))*/
                            })
                        .ToList()
                        .Select(r =>
                            new
                            {
                                r.Apartment,
                                r.Model,
                                r.Number,
                                r.CounterType,
                                RFValues = r.RFValues
                                    .ToDictionary(
                                        byValueType => (PrivateCounterValueType)byValueType.ValueType,
                                        byValueType => byValueType.Months
                                            .ToDictionary(
                                                byMonth => $"{byMonth.Month:MM.yyyy}_RouteForm_PrevValue",
                                                byMonth => byMonth.Value)),
                                FFValues = r.FFValues
                                    .ToDictionary(
                                        byValueType => (PrivateCounterValueType)byValueType.ValueType,
                                        byValueType => byValueType.Months
                                            .ToDictionary(
                                                byMonth => $"{byMonth.Month:MM.yyyy}_FillForm_PrevValue",
                                                byMonth => byMonth.Value)),
                            })
                        .OrderBy(v => v.Apartment, new StringAsNumbersComparer())
                        .ToList();

                /*
                var items = db.PrivateCounterValues
                    .Where(v => v.PrivateCounters.Customers.Buildings.ID.ToString() == View.BuildingId)
                    .GroupBy(v => v.PrivateCounters)
                    .Select(g =>
                        new
                        {
                            g.Key.Customers.Apartment,
                            g.Key.Model,
                            g.Key.Number,
                            ByMonth = g
                                .GroupBy(x => x.Month)
                                .Select(byMonth =>
                                    new
                                    {
                                        Month = byMonth.Key,
                                        Value = byMonth.Max(y => y.Value),
                                    })
                                .ToList()
                        })
                    .ToList()
                    .Select(r =>
                        new
                        {
                            r.Apartment,
                            r.Model,
                            r.Number,
                            ByMonth = r.ByMonth.ToDictionary(z => z.Month.ToString("MM.yyyy"), zz => zz.Value)
                        })
                    .OrderBy(v => v.Apartment, new StringAsNumbersComparer())
                    .ToList();
                */

                foreach (var item in items)
                {
                    if (item.CounterType == PrivateCounterType.Common)
                    {
                        var row = table.NewRow();

                        row[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                        row[ColumnNames.COUNTER_MODEL_COLUMN] = item.Model;
                        row[ColumnNames.COUNTER_NUMBER_COLUMN] = item.Number;

                        foreach (var band in bands)
                        {
                            var RFValueColumn = band.RouteFormPrevValue.FieldName;
                            var FFValueColumn = band.FillFormPrevValue.FieldName;

                            row[RFValueColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Common)
                                    ? item.RFValues[PrivateCounterValueType.Common].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Common][RFValueColumn]
                                        : null
                                    : null;

                            row[FFValueColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Common)
                                    ? item.FFValues[PrivateCounterValueType.Common].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Common][FFValueColumn]
                                        : null
                                    : null;
                        }

                        table.Rows.Add(row);
                    }
                    else if (item.CounterType == PrivateCounterType.DayAndNight)
                    {
                        var rowDay = table.NewRow();

                        rowDay[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                        rowDay[ColumnNames.COUNTER_MODEL_COLUMN] = item.Model;
                        rowDay[ColumnNames.COUNTER_NUMBER_COLUMN] = item.Number;

                        foreach (var band in bands)
                        {
                            var RFValueColumn = band.RouteFormPrevValue.FieldName;
                            var FFValueColumn = band.FillFormPrevValue.FieldName;

                            rowDay[RFValueColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Day)
                                    ? item.RFValues[PrivateCounterValueType.Day].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Day][RFValueColumn]
                                        : null
                                    : null;

                            rowDay[FFValueColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Day)
                                    ? item.FFValues[PrivateCounterValueType.Day].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Day][FFValueColumn]
                                        : null
                                    : null;
                        }

                        table.Rows.Add(rowDay);

                        var rowNight = table.NewRow();

                        rowNight[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                        rowNight[ColumnNames.COUNTER_MODEL_COLUMN] = item.Model;
                        rowNight[ColumnNames.COUNTER_NUMBER_COLUMN] = item.Number;

                        foreach (var band in bands)
                        {
                            var RFValueColumn = band.RouteFormPrevValue.FieldName;
                            var FFValueColumn = band.FillFormPrevValue.FieldName;

                            rowNight[RFValueColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Night)
                                    ? item.RFValues[PrivateCounterValueType.Night].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Night][RFValueColumn]
                                        : null
                                    : null;

                            rowNight[FFValueColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Night)
                                    ? item.FFValues[PrivateCounterValueType.Night].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Night][FFValueColumn]
                                        : null
                                    : null;
                        }

                        table.Rows.Add(rowNight);
                    }
                    else if (item.CounterType == PrivateCounterType.Norm)
                    {
                        var row = table.NewRow();

                        row[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                        row[ColumnNames.COUNTER_MODEL_COLUMN] = item.Model;
                        row[ColumnNames.COUNTER_NUMBER_COLUMN] = item.Number;

                        foreach (var band in bands)
                        {
                            var RFValueColumn = band.RouteFormPrevValue.FieldName;
                            var FFValueColumn = band.FillFormPrevValue.FieldName;

                            row[RFValueColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Norm)
                                    ? item.RFValues[PrivateCounterValueType.Norm].ContainsKey(RFValueColumn)
                                        ? (int?)0
                                        : null
                                    : null;

                            row[FFValueColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Norm)
                                    ? item.FFValues[PrivateCounterValueType.Norm].ContainsKey(FFValueColumn)
                                        ? (int?)0
                                        : null
                                    : null;
                        }

                        table.Rows.Add(row);
                    }
                }
            }

            return table;
        }

        private class Band
        {
            public Column RouteFormPrevValue { get; set; }

            public Column FillFormPrevValue { get; set; }

            public Column ValuesFormPrevValue { get; set; }
        }

        private class Column
        {
            public string FieldName { get; set; }

            public string Title { get; set; }
        }

        private IEnumerable<Band> GetColumnBands()
        {
            int maxMonths = 12;

            DateTime since = new DateTime(View.Since.Year, View.Since.Month, 1);
            DateTime till = new DateTime(View.Till.Year, View.Till.Month, 1);

            return
                Enumerable
                    .Range(0, maxMonths)
                    .Select(month => since.AddMonths(month))
                    .TakeWhile(month => month <= till)
                    .Select(month =>
                        new Band
                        {
                            RouteFormPrevValue = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_RouteForm_PrevValue",
                                Title = $"{month:MM.yyyy}. МЛ",
                            },
                            FillFormPrevValue = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_FillForm_PrevValue",
                                Title = $"{month:MM.yyyy}. ФЗ",
                            },
                            ValuesFormPrevValue = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_ValuesForm_Value",
                                Title = $"{month:MM.yyyy}. Показания",
                            }
                        });
        }

        private void AddColumnsToTable(DataTable table, IEnumerable<Band> extraBands)
        {
            table.Columns.Add(ColumnNames.APARTMENT_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.COUNTER_MODEL_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.COUNTER_NUMBER_COLUMN, typeof(string));

            foreach (Band band in extraBands)
            {
                table.Columns.Add(band.RouteFormPrevValue.FieldName, typeof(string));
                table.Columns.Add(band.FillFormPrevValue.FieldName, typeof(string));
                table.Columns.Add(band.ValuesFormPrevValue.FieldName, typeof(string));
            }    
        }

        private void AddColumnsToView(IEnumerable<Band> extraBands)
        {
            View.AddColumn(ColumnNames.APARTMENT_COLUMN, "Квартира");
            View.AddColumn(ColumnNames.COUNTER_MODEL_COLUMN, "Модель счетика");
            View.AddColumn(ColumnNames.COUNTER_NUMBER_COLUMN, "Номер счетчика");

            foreach (Band band in extraBands)
            {
                View.AddNumericColumn(band.RouteFormPrevValue.FieldName, band.RouteFormPrevValue.Title);
                View.AddNumericColumn(band.FillFormPrevValue.FieldName, band.FillFormPrevValue.Title);
                View.AddNumericColumn(band.ValuesFormPrevValue.FieldName, band.ValuesFormPrevValue.Title);
            }
        }
    }
}
