using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PrivateCountersValues.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private static class ColumnNames
        {
            public const string ACCOUNT_COLUMN = "Account";
            public const string OWNER_COLUMN = "Owner";
            public const string APARTMENT_COLUMN = "Apartment";
            public const string PHONE_COLUMN = "Phone";
            public const string DEBT_COLUMN = "Debt";
            public const string PAYED_COLUMN = "Payed";
            public const string COUNTER_MODEL_COLUMN = "CounterModel";
            public const string COUNTER_NUMBER_COLUMN = "CounterNumber";
            public const string COUNTER_TYPE_COLUMN = "CounterType";
            public const string COUNTER_CAPACITY_COLUMN = "CounterCapacity";
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
                var raw =
                    db.PrivateCounters
                        .Where(c =>
                            c.Customers.Buildings.ID.ToString() == View.BuildingId)
                        .Select(c =>
                            new
                            {
                                c.Customers.Apartment,
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
                                                        PrevDate = byMonth.Max(z =>
                                                            z.RouteFormPoses.PrevDate)
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
                                                        PrevDate = byMonth.Max(z =>
                                                            z.FillFormPoses.PrevDate)
                                                    })
                                                .ToList(),
                                        })
                                    .ToList(),
                                PCValues = c.PrivateCounterValues
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
                                                        CurrentDate = byMonth.Max(z =>
                                                            z.PrivateValuesFormPoses.CurrentDate)
                                                    })
                                                .ToList(),
                                        })
                                    .ToList(),
                                LastRouteFormValue = c.RouteFormValues
                                    .FirstOrDefault(v => v.Month == c.RouteFormValues.Max(vv => vv.Month)),
                                LastFillFormValue = c.FillFormValues
                                    .FirstOrDefault(v => v.Month == c.FillFormValues.Max(vv => vv.Month)),
                                LastPrivateValuesFormValue = c.PrivateCounterValues
                                    .FirstOrDefault(v => v.Month == c.RouteFormValues.Max(vv => vv.Month))
                            })
                        .Select(c =>
                            new
                            {
                                AccountRouteForm = c.LastRouteFormValue != null
                                    ? c.LastRouteFormValue.RouteFormPoses != null
                                        ? c.LastRouteFormValue.RouteFormPoses.Account
                                        : null
                                    : null,
                                AccountFillForm = c.LastFillFormValue != null
                                    ? c.LastFillFormValue.FillFormPoses != null
                                        ? c.LastFillFormValue.FillFormPoses.Account
                                        : null
                                    : null,

                                Owner = c.LastRouteFormValue != null
                                    ? c.LastRouteFormValue.RouteFormPoses != null
                                        ? c.LastRouteFormValue.RouteFormPoses.Owner
                                        : null
                                    : null,

                                c.Apartment,

                                Phone = c.LastRouteFormValue != null
                                    ? c.LastRouteFormValue.RouteFormPoses != null
                                        ? c.LastRouteFormValue.RouteFormPoses.Phone
                                        : null
                                    : null,

                                Debt = c.LastRouteFormValue != null
                                    ? c.LastRouteFormValue.RouteFormPoses != null
                                        ? c.LastRouteFormValue.RouteFormPoses.Debt
                                        : null
                                    : null,

                                Payed = c.LastRouteFormValue != null
                                    ? c.LastRouteFormValue.RouteFormPoses != null
                                        ? c.LastRouteFormValue.RouteFormPoses.Payed
                                        : null
                                    : null,

                                CounterModel = c.LastFillFormValue != null
                                    ? c.LastFillFormValue.FillFormPoses != null
                                        ? c.LastFillFormValue.FillFormPoses.CounterModel
                                        : null
                                    : null,

                                CounterNumber = c.Number,

                                c.CounterType,

                                CounterCapacityRouteForm = c.LastRouteFormValue != null
                                    ? c.LastRouteFormValue.RouteFormPoses != null
                                        ? c.LastRouteFormValue.RouteFormPoses.CounterCapacity
                                        : null
                                    : null,
                                CounterCapacityFillForm = c.LastFillFormValue != null
                                    ? c.LastFillFormValue.FillFormPoses != null
                                        ? c.LastFillFormValue.FillFormPoses.CounterCapacity
                                        : null
                                    : null,

                                c.RFValues,
                                c.FFValues,
                                c.PCValues,
                            })
                        .ToList();

                    var items = raw
                        .Select(r =>
                            new
                            {
                                Account = r.AccountRouteForm ?? r.AccountFillForm,
                                r.Owner,
                                r.Apartment,
                                r.Phone,
                                r.Debt,
                                r.Payed,
                                r.CounterModel,
                                r.CounterNumber,
                                r.CounterType,
                                CounterCapacity = r.CounterCapacityRouteForm ?? r.CounterCapacityFillForm,

                                RFValues = r.RFValues
                                    .ToDictionary(
                                        byValueType => (PrivateCounterValueType)byValueType.ValueType,
                                        byValueType => byValueType.Months
                                            .ToDictionary(
                                                byMonth => $"{byMonth.Month:MM.yyyy}_RouteForm_PrevValue",
                                                byMonth => new
                                                {
                                                    byMonth.Value,
                                                    byMonth.PrevDate,
                                                })),
                                FFValues = r.FFValues
                                    .ToDictionary(
                                        byValueType => (PrivateCounterValueType)byValueType.ValueType,
                                        byValueType => byValueType.Months
                                            .ToDictionary(
                                                byMonth => $"{byMonth.Month:MM.yyyy}_FillForm_PrevValue",
                                                byMonth => new
                                                {
                                                    byMonth.Value,
                                                    byMonth.PrevDate,
                                                })),
                                PCValues = r.PCValues
                                    .ToDictionary(
                                        byValueType => (PrivateCounterValueType)byValueType.ValueType,
                                        byValueType => byValueType.Months
                                            .ToDictionary(
                                                byMonth => $"{byMonth.Month:MM.yyyy}_PrivateValuesForm_CurrentValue",
                                                byMonth => new
                                                {
                                                    byMonth.Value,
                                                    byMonth.CurrentDate,
                                                })),

                            })
                        .OrderBy(v => v.Apartment, new StringAsNumbersComparer())
                        .ToList();

                foreach (var item in items)
                {
                    if (item.CounterType == PrivateCounterType.Common)
                    {
                        var row = table.NewRow();

                        row[ColumnNames.ACCOUNT_COLUMN] = item.Account;
                        row[ColumnNames.OWNER_COLUMN] = item.Owner;
                        row[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                        row[ColumnNames.PHONE_COLUMN] = item.Phone;
                        row[ColumnNames.DEBT_COLUMN] = item.Debt;
                        row[ColumnNames.PAYED_COLUMN] = item.Payed;
                        row[ColumnNames.COUNTER_MODEL_COLUMN] = item.CounterModel;
                        row[ColumnNames.COUNTER_NUMBER_COLUMN] = item.CounterNumber;
                        row[ColumnNames.COUNTER_TYPE_COLUMN] = "Однотарифный";
                        row[ColumnNames.COUNTER_CAPACITY_COLUMN] = item.CounterCapacity;

                        foreach (var band in bands)
                        {
                            var RFDateColumn = band.RouteFormPrevDate.FieldName;
                            var RFValueColumn = band.RouteFormPrevValue.FieldName;
                            var FFDateColumn = band.FillFormPrevDate.FieldName;
                            var FFValueColumn = band.FillFormPrevValue.FieldName;
                            var PVFDateColumn = band.PrivateValuesFormCurrentDate.FieldName;
                            var PVFValueColumn = band.PrivateValuesFormCurrentValue.FieldName;

                            row[RFDateColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Common)
                                    ? item.RFValues[PrivateCounterValueType.Common].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Common][RFValueColumn]?.PrevDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            row[RFValueColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Common)
                                    ? item.RFValues[PrivateCounterValueType.Common].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Common][RFValueColumn]?.Value
                                        : null
                                    : null;

                            row[FFDateColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Common)
                                    ? item.FFValues[PrivateCounterValueType.Common].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Common][FFValueColumn]?.PrevDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            row[FFValueColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Common)
                                    ? item.FFValues[PrivateCounterValueType.Common].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Common][FFValueColumn]?.Value
                                        : null
                                    : null;

                            row[PVFDateColumn] =
                                item.PCValues.ContainsKey(PrivateCounterValueType.Common)
                                    ? item.PCValues[PrivateCounterValueType.Common].ContainsKey(PVFValueColumn)
                                        ? item.PCValues[PrivateCounterValueType.Common][PVFValueColumn]?.CurrentDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            row[PVFValueColumn] =
                                item.PCValues.ContainsKey(PrivateCounterValueType.Common)
                                    ? item.PCValues[PrivateCounterValueType.Common].ContainsKey(PVFValueColumn)
                                        ? item.PCValues[PrivateCounterValueType.Common][PVFValueColumn]?.Value
                                        : null
                                    : null;
                        }

                        table.Rows.Add(row);
                    }
                    else if (item.CounterType == PrivateCounterType.DayAndNight)
                    {
                        var rowDay = table.NewRow();

                        rowDay[ColumnNames.ACCOUNT_COLUMN] = item.Account;
                        rowDay[ColumnNames.OWNER_COLUMN] = item.Owner;
                        rowDay[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                        rowDay[ColumnNames.PHONE_COLUMN] = item.Phone;
                        rowDay[ColumnNames.DEBT_COLUMN] = item.Debt;
                        rowDay[ColumnNames.PAYED_COLUMN] = item.Payed;
                        rowDay[ColumnNames.COUNTER_MODEL_COLUMN] = item.CounterModel;
                        rowDay[ColumnNames.COUNTER_NUMBER_COLUMN] = item.CounterNumber;
                        rowDay[ColumnNames.COUNTER_TYPE_COLUMN] = "Двухтарифный (день)";
                        rowDay[ColumnNames.COUNTER_CAPACITY_COLUMN] = item.CounterCapacity;

                        foreach (var band in bands)
                        {
                            var RFDateColumn = band.RouteFormPrevDate.FieldName;
                            var RFValueColumn = band.RouteFormPrevValue.FieldName;
                            var FFDateColumn = band.FillFormPrevDate.FieldName;
                            var FFValueColumn = band.FillFormPrevValue.FieldName;
                            var PVFDateColumn = band.PrivateValuesFormCurrentDate.FieldName;
                            var PVFValueColumn = band.PrivateValuesFormCurrentValue.FieldName;

                            rowDay[RFDateColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Day)
                                    ? item.RFValues[PrivateCounterValueType.Day].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Day][RFValueColumn]?.PrevDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            rowDay[RFValueColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Day)
                                    ? item.RFValues[PrivateCounterValueType.Day].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Day][RFValueColumn]?.Value
                                        : null
                                    : null;

                            rowDay[FFDateColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Day)
                                    ? item.FFValues[PrivateCounterValueType.Day].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Day][FFValueColumn]?.PrevDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            rowDay[FFValueColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Day)
                                    ? item.FFValues[PrivateCounterValueType.Day].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Day][FFValueColumn]?.Value
                                        : null
                                    : null;

                            rowDay[PVFDateColumn] =
                                item.PCValues.ContainsKey(PrivateCounterValueType.Day)
                                    ? item.PCValues[PrivateCounterValueType.Day].ContainsKey(PVFValueColumn)
                                        ? item.PCValues[PrivateCounterValueType.Day][PVFValueColumn]?.CurrentDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            rowDay[PVFValueColumn] =
                                item.PCValues.ContainsKey(PrivateCounterValueType.Day)
                                    ? item.PCValues[PrivateCounterValueType.Day].ContainsKey(PVFValueColumn)
                                        ? item.PCValues[PrivateCounterValueType.Day][PVFValueColumn]?.Value
                                        : null
                                    : null;
                        }

                        table.Rows.Add(rowDay);

                        var rowNight = table.NewRow();

                        rowNight[ColumnNames.ACCOUNT_COLUMN] = item.Account;
                        rowNight[ColumnNames.OWNER_COLUMN] = item.Owner;
                        rowNight[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                        rowNight[ColumnNames.PHONE_COLUMN] = item.Phone;
                        rowNight[ColumnNames.DEBT_COLUMN] = item.Debt;
                        rowNight[ColumnNames.PAYED_COLUMN] = item.Payed;
                        rowNight[ColumnNames.COUNTER_MODEL_COLUMN] = item.CounterModel;
                        rowNight[ColumnNames.COUNTER_NUMBER_COLUMN] = item.CounterNumber;
                        rowNight[ColumnNames.COUNTER_TYPE_COLUMN] = "Двухтарифный (ночь)";
                        rowNight[ColumnNames.COUNTER_CAPACITY_COLUMN] = item.CounterCapacity;

                        foreach (var band in bands)
                        {
                            var RFDateColumn = band.RouteFormPrevDate.FieldName;
                            var RFValueColumn = band.RouteFormPrevValue.FieldName;
                            var FFDateColumn = band.FillFormPrevDate.FieldName;
                            var FFValueColumn = band.FillFormPrevValue.FieldName;
                            var PVFValueColumn = band.PrivateValuesFormCurrentValue.FieldName;
                            var PVFDateColumn = band.PrivateValuesFormCurrentDate.FieldName;

                            rowNight[RFDateColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Night)
                                    ? item.RFValues[PrivateCounterValueType.Night].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Night][RFValueColumn]?.PrevDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            rowNight[RFValueColumn] =
                                item.RFValues.ContainsKey(PrivateCounterValueType.Night)
                                    ? item.RFValues[PrivateCounterValueType.Night].ContainsKey(RFValueColumn)
                                        ? item.RFValues[PrivateCounterValueType.Night][RFValueColumn]?.Value
                                        : null
                                    : null;

                            rowNight[FFDateColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Night)
                                    ? item.FFValues[PrivateCounterValueType.Night].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Night][FFValueColumn]?.PrevDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            rowNight[FFValueColumn] =
                                item.FFValues.ContainsKey(PrivateCounterValueType.Night)
                                    ? item.FFValues[PrivateCounterValueType.Night].ContainsKey(FFValueColumn)
                                        ? item.FFValues[PrivateCounterValueType.Night][FFValueColumn]?.Value
                                        : null
                                    : null;

                            rowNight[PVFDateColumn] =
                                item.PCValues.ContainsKey(PrivateCounterValueType.Night)
                                    ? item.PCValues[PrivateCounterValueType.Night].ContainsKey(PVFValueColumn)
                                        ? item.PCValues[PrivateCounterValueType.Night][PVFValueColumn]?.CurrentDate
                                            ?.ToString("dd.MM.yyyy")
                                        : null
                                    : null;

                            rowNight[PVFValueColumn] =
                                item.PCValues.ContainsKey(PrivateCounterValueType.Night)
                                    ? item.PCValues[PrivateCounterValueType.Night].ContainsKey(PVFValueColumn)
                                        ? item.PCValues[PrivateCounterValueType.Night][PVFValueColumn]?.Value
                                        : null
                                    : null;
                        }

                        table.Rows.Add(rowNight);
                    }
                    else if (item.CounterType == PrivateCounterType.Norm)
                    {
                        var row = table.NewRow();

                        row[ColumnNames.ACCOUNT_COLUMN] = item.Account;
                        row[ColumnNames.OWNER_COLUMN] = item.Owner;
                        row[ColumnNames.APARTMENT_COLUMN] = item.Apartment;
                        row[ColumnNames.PHONE_COLUMN] = item.Phone;
                        row[ColumnNames.DEBT_COLUMN] = item.Debt;
                        row[ColumnNames.PAYED_COLUMN] = item.Payed;
                        row[ColumnNames.COUNTER_MODEL_COLUMN] = item.CounterModel;
                        row[ColumnNames.COUNTER_NUMBER_COLUMN] = item.CounterNumber;
                        row[ColumnNames.COUNTER_TYPE_COLUMN] = "Норматив";
                        row[ColumnNames.COUNTER_CAPACITY_COLUMN] = item.CounterCapacity;

                        foreach (var band in bands)
                        {
                            var RFDateColumn = band.RouteFormPrevDate.FieldName;
                            var RFValueColumn = band.RouteFormPrevValue.FieldName;
                            var FFDateColumn = band.FillFormPrevDate.FieldName;
                            var FFValueColumn = band.FillFormPrevValue.FieldName;
                            var PVFValueColumn = band.PrivateValuesFormCurrentValue.FieldName;
                            var PVFDateColumn = band.PrivateValuesFormCurrentDate.FieldName;

                            row[RFDateColumn] = null;

                            row[RFValueColumn] =  null;

                            row[FFDateColumn] = null;

                            row[FFValueColumn] =  null;

                            row[PVFDateColumn] = null;

                            row[PVFValueColumn] = null;
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

            public Column RouteFormPrevDate { get; set; }

            public Column FillFormPrevValue { get; set; }

            public Column FillFormPrevDate { get; set; }

            public Column PrivateValuesFormCurrentValue { get; set; }

            public Column PrivateValuesFormCurrentDate { get; set; }
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
                            RouteFormPrevDate = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_RouteForm_PrevDate",
                                Title = $"{month:MM.yyyy}. МЛ. Дата предыдущих показаний",
                            },
                            RouteFormPrevValue = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_RouteForm_PrevValue",
                                Title = $"{month:MM.yyyy}. МЛ",
                            },
                            FillFormPrevDate = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_FillForm_PrevDate",
                                Title = $"{month:MM.yyyy}. ФЗ. Дата предыдущих показаний",
                            },
                            FillFormPrevValue = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_FillForm_PrevValue",
                                Title = $"{month:MM.yyyy}. ФЗ",
                            },
                            PrivateValuesFormCurrentDate = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_PrivateValuesForm_CurrentDate",
                                Title = $"{month:MM.yyyy}. Дата показаний",
                            },
                            PrivateValuesFormCurrentValue = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}_PrivateValuesForm_CurrentValue",
                                Title = $"{month:MM.yyyy}. Показания",
                            },
                        });
        }

        private void AddColumnsToTable(DataTable table, IEnumerable<Band> extraBands)
        {
            table.Columns.Add(ColumnNames.ACCOUNT_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.OWNER_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.APARTMENT_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.PHONE_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.DEBT_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.PAYED_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.COUNTER_MODEL_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.COUNTER_NUMBER_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.COUNTER_TYPE_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.COUNTER_CAPACITY_COLUMN, typeof(string));

            foreach (Band band in extraBands)
            {
                table.Columns.Add(band.RouteFormPrevDate.FieldName, typeof(string));
                table.Columns.Add(band.RouteFormPrevValue.FieldName, typeof(string));
                table.Columns.Add(band.FillFormPrevDate.FieldName, typeof(string));
                table.Columns.Add(band.FillFormPrevValue.FieldName, typeof(string));
                table.Columns.Add(band.PrivateValuesFormCurrentDate.FieldName, typeof(string));
                table.Columns.Add(band.PrivateValuesFormCurrentValue.FieldName, typeof(string));
            }    
        }

        private void AddColumnsToView(IEnumerable<Band> extraBands)
        {
            View.AddColumn(ColumnNames.ACCOUNT_COLUMN, "Лицевой счет");
            View.AddColumn(ColumnNames.OWNER_COLUMN, "Собственник");
            View.AddColumn(ColumnNames.APARTMENT_COLUMN, "Квартира");
            View.AddColumn(ColumnNames.PHONE_COLUMN, "Телефон");
            View.AddColumn(ColumnNames.DEBT_COLUMN, "Долг");
            View.AddColumn(ColumnNames.PAYED_COLUMN, "Последняя оплата");
            View.AddColumn(ColumnNames.COUNTER_MODEL_COLUMN, "Модель счетика");
            View.AddColumn(ColumnNames.COUNTER_NUMBER_COLUMN, "Номер счетчика");
            View.AddColumn(ColumnNames.COUNTER_TYPE_COLUMN, "Тип счетчика");
            View.AddColumn(ColumnNames.COUNTER_CAPACITY_COLUMN, "Разраядность счетчика");

            foreach (Band band in extraBands)
            {
                View.AddDateColumn(band.RouteFormPrevDate.FieldName, band.RouteFormPrevDate.Title);
                View.AddNumericColumn(band.RouteFormPrevValue.FieldName, band.RouteFormPrevValue.Title);
                View.AddDateColumn(band.FillFormPrevDate.FieldName, band.FillFormPrevDate.Title);
                View.AddNumericColumn(band.FillFormPrevValue.FieldName, band.FillFormPrevValue.Title);
                View.AddDateColumn(band.PrivateValuesFormCurrentDate.FieldName, band.PrivateValuesFormCurrentDate.Title);
                View.AddNumericColumn(band.PrivateValuesFormCurrentValue.FieldName, band.PrivateValuesFormCurrentValue.Title);
            }
        }
    }
}
