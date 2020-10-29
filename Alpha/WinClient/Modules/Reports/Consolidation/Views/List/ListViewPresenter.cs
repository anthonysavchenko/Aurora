using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private static class ColumnNames
        {
            public const string BUILDING_COLUMN = "Building";
            public const string PARAMS_COLUMN = "Params";
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
                var rawItems =
                    db.Buildings
                        .Select(b =>
                            new
                            {
                                Address = b.Street + ", д. " + b.Number,

                                BuildingCounterValues =
                                    db.BuildingCounterValues
                                        .Where(v =>
                                            v.BuildingCounters.Buildings.ID == b.ID
                                                && v.Month >= View.Since
                                                && v.Month <= View.Till)
                                        .GroupBy(g => g.Month)
                                        .Select(g =>
                                            new
                                            {
                                                Month = g.Key,
                                                Value = g.Sum(vv => (vv.CurrentValue - vv.PrevValue) *
                                                    vv.BuildingCounters.Coefficient),
                                            })
                                        .ToList(),

                                BuildingCounterCalculationValues =
                                    db.BuildingCounterCalculationValues
                                        .Where(v =>
                                            v.BuildingCounters.Buildings.ID == b.ID
                                                && v.Month >= View.Since
                                                && v.Month <= View.Till)
                                        .GroupBy(g => g.Month)
                                        .Select(g =>
                                            new
                                            {
                                                Month = g.Key,
                                                Value = g.Sum(vv => (vv.CurrentValue - vv.PrevValue) *
                                                    vv.BuildingCounters.Coefficient),
                                            })
                                        .ToList(),

                                LegalEntityCalculationValues =
                                    db.LegalEntityCalculationValues
                                        .Where(v =>
                                            v.LegalEntities.Buildings.ID == b.ID
                                                && v.Month >= View.Since
                                                && v.Month <= View.Till)
                                        .GroupBy(g => g.Month)
                                        .Select(g =>
                                            new
                                            {
                                                Month = g.Key,
                                                Value = g.Sum(vv => vv.ChargedVolume),
                                            })
                                        .ToList(),

                                /*
                                FillFormValues =
                                    db.FillFormValues
                                        .Where(v =>
                                            v.PrivateCounters.Customers.Buildings.ID == b.ID
                                                && v.Month >= View.Since
                                                && v.Month <= View.Till)
                                        .GroupBy(g => g.Month)
                                        .Select(g =>
                                            new
                                            {
                                                Month = g.Key,
                                                Value = g.Sum(vv =>
                                                    vv.Value -
                                                        db.FillFormValues
                                                            .FirstOrDefault(vvv =>
                                                                vvv.Month == g.Key.AddMonths(-1)
                                                                    && vvv.ValueType == vv.ValueType
                                                                    && vvv.PrivateCounters.ID == vv.PrivateCounters.ID)
                                                                        .Value),
                                            })
                                        .ToList(),
                                */

                                CustomerCalculationValuesVolume =
                                    db.CustomerCalculationValues
                                        .Where(v =>
                                            v.Customers.Buildings.ID == b.ID
                                                && v.Month >= View.Since
                                                && v.Month <= View.Till)
                                        .GroupBy(g => g.Month)
                                        .Select(g =>
                                            new
                                            {
                                                Month = g.Key,
                                                Value = g.Sum(vv => vv.Volume),
                                            })
                                        .ToList(),

                                CustomerCalculationValuesRecalculation =
                                    db.CustomerCalculationValues
                                        .Where(v =>
                                            v.Customers.Buildings.ID == b.ID
                                                && v.Month >= View.Since
                                                && v.Month <= View.Till)
                                        .GroupBy(g => g.Month)
                                        .Select(g =>
                                            new
                                            {
                                                Month = g.Key,
                                                Value = g.Sum(vv => vv.Recalculation),
                                            })
                                        .ToList(),

                                BuildingCalculationValues =
                                    db.BuildingCalculationValues
                                        .Where(v =>
                                            v.Buildings.ID == b.ID
                                                && v.Month >= View.Since
                                                && v.Month <= View.Till)
                                        .GroupBy(g => g.Month)
                                        .Select(g =>
                                            new
                                            {
                                                Month = g.Key,
                                                Value = g.FirstOrDefault(),
                                            })
                                        .ToList(),
                            })
                        .ToList();

                var items =
                    rawItems
                        .Select(i =>
                            new
                            {
                                i.Address,

                                BuildingCounterValues =
                                    i.BuildingCounterValues
                                        .ToDictionary(
                                            v => $"{v.Month:MM.yyyy}",
                                            vv => vv.Value),

                                BuildingCounterCalculationValues =
                                    i.BuildingCounterCalculationValues
                                        .ToDictionary(
                                            v => $"{v.Month:MM.yyyy}",
                                            vv => vv.Value),

                                LegalEntityCalculationValues =
                                    i.LegalEntityCalculationValues
                                        .ToDictionary(
                                            v => $"{v.Month:MM.yyyy}",
                                            vv => vv.Value),

                                CustomerCalculationValuesVolume =
                                    i.CustomerCalculationValuesVolume
                                        .ToDictionary(
                                            v => $"{v.Month:MM.yyyy}",
                                            vv => vv.Value),

                                CustomerCalculationValuesRecalculation =
                                    i.CustomerCalculationValuesRecalculation
                                        .ToDictionary(
                                            v => $"{v.Month:MM.yyyy}",
                                            vv => vv.Value),

                                BuildingCalculationValues =
                                    i.BuildingCalculationValues
                                        .ToDictionary(
                                            v => $"{v.Month:MM.yyyy}",
                                            vv => vv.Value),
                            })
                        .OrderBy(b => b.Address)
                        .ToList();

                foreach (var item in items)
                {
                    var row1 = table.NewRow();

                    row1[ColumnNames.BUILDING_COLUMN] = item.Address;
                    row1[ColumnNames.PARAMS_COLUMN] = "ОДПУ УК ФР";

                    for (int j = 0; j < bands.Count(); j++)
                    {
                        var band = bands.ElementAt(j);

                        row1[band.MonthlyData.FieldName] =
                            item.BuildingCounterValues.ContainsKey(band.MonthlyData.FieldName)
                                ? item.BuildingCounterValues[band.MonthlyData.FieldName]
                                : null;
                    }

                    table.Rows.Add(row1);


                    var row2 = table.NewRow();

                    row2[ColumnNames.BUILDING_COLUMN] = item.Address;
                    row2[ColumnNames.PARAMS_COLUMN] = "ОДПУ ДЭК";

                    for (int j = 0; j < bands.Count(); j++)
                    {
                        var band = bands.ElementAt(j);

                        row2[band.MonthlyData.FieldName] =
                            item.BuildingCounterCalculationValues.ContainsKey(band.MonthlyData.FieldName)
                                ? item.BuildingCounterCalculationValues[band.MonthlyData.FieldName]
                                : (decimal?)null;
                    }

                    table.Rows.Add(row2);


                    var row3 = table.NewRow();

                    row3[ColumnNames.BUILDING_COLUMN] = item.Address;
                    row3[ColumnNames.PARAMS_COLUMN] = "К распределению ДЭК";

                    for (int j = 0; j < bands.Count(); j++)
                    {
                        var band = bands.ElementAt(j);

                        var buildingCounterCalculationValue =
                            item.BuildingCounterCalculationValues.ContainsKey(band.MonthlyData.FieldName)
                                ? item.BuildingCounterCalculationValues[band.MonthlyData.FieldName]
                                : (decimal?)null;

                        var legalEntitiesCalculationValue =
                            item.LegalEntityCalculationValues.ContainsKey(band.MonthlyData.FieldName)
                                ? item.LegalEntityCalculationValues[band.MonthlyData.FieldName]
                                : (decimal?)null;

                        row3[band.MonthlyData.FieldName] =
                            buildingCounterCalculationValue.HasValue && legalEntitiesCalculationValue.HasValue
                                ? buildingCounterCalculationValue - legalEntitiesCalculationValue
                                : null;
                    }

                    table.Rows.Add(row3);


                    var row4 = table.NewRow();

                    row4[ColumnNames.BUILDING_COLUMN] = item.Address;
                    row4[ColumnNames.PARAMS_COLUMN] = "ИПУ ДЭК";

                    for (int j = 0; j < bands.Count(); j++)
                    {
                        var band = bands.ElementAt(j);

                        row4[band.MonthlyData.FieldName] =
                            item.CustomerCalculationValuesVolume.ContainsKey(band.MonthlyData.FieldName)
                                ? item.CustomerCalculationValuesVolume[band.MonthlyData.FieldName]
                                : (decimal?)null;
                    }

                    table.Rows.Add(row4);


                    var row5 = table.NewRow();

                    row5[ColumnNames.BUILDING_COLUMN] = item.Address;
                    row5[ColumnNames.PARAMS_COLUMN] = "Перерасчет ИПУ ДЭК";

                    for (int j = 0; j < bands.Count(); j++)
                    {
                        var band = bands.ElementAt(j);

                        row5[band.MonthlyData.FieldName] =
                            item.CustomerCalculationValuesRecalculation.ContainsKey(band.MonthlyData.FieldName)
                                ? item.CustomerCalculationValuesRecalculation[band.MonthlyData.FieldName]
                                : (decimal?)null;
                    }

                    table.Rows.Add(row5);


                    var row6 = table.NewRow();

                    row6[ColumnNames.BUILDING_COLUMN] = item.Address;
                    row6[ColumnNames.PARAMS_COLUMN] = "ОДН ДЭК";

                    for (int j = 0; j < bands.Count(); j++)
                    {
                        var band = bands.ElementAt(j);

                        row6[band.MonthlyData.FieldName] =
                            item.BuildingCalculationValues.ContainsKey(band.MonthlyData.FieldName)
                                ? item.BuildingCalculationValues[band.MonthlyData.FieldName]?.CollectiveVolume
                                : null;
                    }

                    table.Rows.Add(row6);
                }
            }

            return table;
        }

        private class Band
        {
            public Column MonthlyData { get; set; }
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
                            MonthlyData = new Column()
                            {
                                FieldName = $"{month:MM.yyyy}",
                                Title = $"{month:MM.yyyy}",
                            },
                        });
        }

        private void AddColumnsToTable(DataTable table, IEnumerable<Band> extraBands)
        {
            table.Columns.Add(ColumnNames.BUILDING_COLUMN, typeof(string));
            table.Columns.Add(ColumnNames.PARAMS_COLUMN, typeof(string));

            foreach (Band band in extraBands)
            {
                table.Columns.Add(band.MonthlyData.FieldName, typeof(string));
            }    
        }

        private void AddColumnsToView(IEnumerable<Band> extraBands)
        {
            View.AddColumn(ColumnNames.BUILDING_COLUMN, "Дом");
            View.AddColumn(ColumnNames.PARAMS_COLUMN, "Параметры");

            foreach (Band band in extraBands)
            {
                View.AddNumericColumn(band.MonthlyData.FieldName, band.MonthlyData.Title);
            }
        }
    }
}
