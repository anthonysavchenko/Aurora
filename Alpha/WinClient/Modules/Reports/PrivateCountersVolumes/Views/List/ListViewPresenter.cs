using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
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

            AddColumnsToView(GetColumns());

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

            var columns = GetColumns();
            AddColumnsToTable(table, columns);

            using (var db = new Entities())
            {
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

                foreach (var item in items)
                {
                    var row = table.NewRow();

                    row[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                    row[ColumnNames.COUNTER_MODEL_COLUMN] = item.Model;
                    row[ColumnNames.COUNTER_NUMBER_COLUMN] = item.Number;

                    foreach (var column in columns)
                    {
                        row[column.FieldName] = item.ByMonth.ContainsKey(column.FieldName)
                            ? item.ByMonth[column.FieldName]
                            : 0;
                    }

                    table.Rows.Add(row);
                }
            }

            return table;
        }

        private class Column
        {
            public string FieldName { get; set; }

            public string Title { get; set; }
        }

        private IEnumerable<Column> GetColumns()
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
                        new Column()
                        {
                            FieldName = month.ToString("MM.yyyy"),
                            Title = month.ToString("MM.yyyy"),
                        });
        }

        private void AddColumnsToTable(DataTable table, IEnumerable<Column> extraColumns)
        {
            table.Columns.Add(ColumnNames.APARTMENT_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.COUNTER_MODEL_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.COUNTER_NUMBER_COLUMN, typeof(string));

            foreach (Column column in extraColumns)
            {
                table.Columns.Add(column.FieldName, typeof(string));
            }    
        }

        private void AddColumnsToView(IEnumerable<Column> extraColumns)
        {
            View.AddColumn(ColumnNames.APARTMENT_COLUMN, "Квартира");
            View.AddColumn(ColumnNames.COUNTER_MODEL_COLUMN, "Модель счетика");
            View.AddColumn(ColumnNames.COUNTER_NUMBER_COLUMN, "Номер номер счетчика");

            foreach (Column column in extraColumns)
            {
                View.AddNumericColumn(column.FieldName, column.Title);
            }
        }
    }
}
