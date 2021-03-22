using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Queries
{
    public static class ListViewQuery
    {
        private const string BUILDING_COLUMN = "Building";
        private const string PARAMS_COLUMN = "Params";
        private const string AVARAGE_COLUMN = "Avarage";
        private const string SUM_COLUMN = "Sum";

        private const int MAX_MONTH_COUNT = 12;
        private const int FIRST_EXTRA_COLUMN_INDEX = 2;
        private const int FIXED_COLUMNS_COUNT = 2;

        public static List<Column> GetGridColumns(DateTime since, DateTime till)
        {
            var columns = new List<Column>()
            {
                new Column()
                {
                    Caption = "Дом",
                    ColumnFormat = ColumnFormat.String,
                    FieldName = BUILDING_COLUMN,
                    ColumnType = typeof(string),
                },
                new Column()
                {
                    Caption = "Параметры",
                    ColumnFormat = ColumnFormat.String,
                    FieldName = PARAMS_COLUMN,
                    ColumnType = typeof(string),
                },
            };

            columns.AddRange(
                Enumerable
                    .Range(0, MAX_MONTH_COUNT)
                    .Select(month => since.AddMonths(month))
                    .TakeWhile(month => month <= till)
                    .Select(month =>
                        new Column
                        {
                            Caption = $"{month:MM.yyyy}",
                            ColumnFormat = ColumnFormat.Numeric,
                            FieldName = $"{month:MM.yyyy}",
                            ColumnType = typeof(decimal),
                        })
                    .ToList());

            columns.Add(
                new Column()
                {
                    Caption = "Среднее",
                    ColumnFormat = ColumnFormat.Numeric,
                    FieldName = AVARAGE_COLUMN,
                    ColumnType = typeof(decimal),
                });

            columns.Add(
                new Column()
                {
                    Caption = "Сумма",
                    ColumnFormat = ColumnFormat.Numeric,
                    FieldName = SUM_COLUMN,
                    ColumnType = typeof(decimal),
                });

            return columns;
        }

        public static DataTable GetGridRows(this Entities db, List<Column> columns, DateTime since, DateTime till)
        {
            DateTime sinceMinusOneMonth = since.AddMonths(-1);

            var dbQueriedItems =
                db.Buildings
                    .Select(b =>
                        new
                        {
                            Address = b.Street + ", д. " + b.Number,

                            BuildingCounterValueVolumes =
                                db.BuildingCounterValues
                                    .Where(v =>
                                        v.BuildingCounters.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => (vv.CurrentValue - vv.PrevValue) *
                                                vv.BuildingCounters.Coefficient),
                                        })
                                    .ToList(),

                            BuildingCounterCalculationValueVolumes =
                                db.BuildingCounterCalculationValues
                                    .Where(v =>
                                        v.BuildingCounters.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => (vv.CurrentValue - vv.PrevValue) *
                                                vv.BuildingCounters.Coefficient),
                                        })
                                    .ToList(),

                            LegalEntityCalculationValueChargedVolumes =
                                db.LegalEntityCalculationValues
                                    .Where(v =>
                                        v.LegalEntities.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => vv.ChargedVolume),
                                        })
                                    .ToList(),

                            /*FillFormValues =
                                db.FillFormValues
                                    .Where(v =>
                                        v.PrivateCounters.Customers.Buildings.ID == b.ID
                                            && v.Month >= sinceMinusOneMonth
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Values = g.Select(vv =>
                                                new
                                                {
                                                    Counter = vv.PrivateCounters.ID,
                                                    vv.ValueType,
                                                    vv.Value,
                                                })
                                            .ToList(),
                                        })
                                    .ToList(),*/

                            CustomerCalculationValueVolumes =
                                db.CustomerCalculationValues
                                    .Where(v =>
                                        v.Customers.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => vv.Volume),
                                        })
                                    .ToList(),

                            CustomerCalculationValueRecalculations =
                                db.CustomerCalculationValues
                                    .Where(v =>
                                        v.Customers.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
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
                                            && v.Month >= since
                                            && v.Month <= till)
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

            var toDictionariesItems =
                dbQueriedItems
                    .Select(i =>
                        new
                        {
                            i.Address,

                            BuildingCounterVolumes =
                                i.BuildingCounterValueVolumes
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => vv.Value),

                            DecBuildingCounterVolumes =
                                i.BuildingCounterCalculationValueVolumes
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value),

                            DecLegalEntityVolumes =
                                i.LegalEntityCalculationValueChargedVolumes
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value),

                            /*FillFormValues =
                                i.FillFormValues
                                    .ToDictionary(
                                        v => v.Month,
                                        vv => vv.Values
                                            .ToDictionary(
                                                vvv => $"{vvv.Counter}-{vvv.ValueType}",
                                                vvv => vvv.Value)),*/

                            DecCustomerVolumes =
                                i.CustomerCalculationValueVolumes
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value),

                            DecCustomerRecalculations =
                                i.CustomerCalculationValueRecalculations
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value),

                            DecCollectiveVolumes =
                                i.BuildingCalculationValues
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => vv.Value?.CollectiveVolume),

                            DecCollectiveSquares =
                                i.BuildingCalculationValues
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => vv.Value?.CollectiveSquare),

                            DecCalculationMethods =
                                i.BuildingCalculationValues
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value?.CalculationMethod),
                        })
                    .ToList();

            var processedItems =
                toDictionariesItems
                    .Select(i =>
                        new
                        {
                            i.Address,
                            i.BuildingCounterVolumes,
                            i.DecBuildingCounterVolumes,
                            i.DecLegalEntityVolumes,

                            /*FillFormVolumes =
                                i.FillFormValues
                                    .Select(v =>
                                        new
                                        {
                                            Month = v.Key,
                                            Volumes =
                                                v.Value
                                                    .Select(vv =>
                                                        vv.Value.HasValue
                                                            ? i.FillFormValues.ContainsKey(v.Key.AddMonths(-1))
                                                                ? i.FillFormValues[v.Key.AddMonths(-1)].ContainsKey(vv.Key)
                                                                    ? i.FillFormValues[v.Key.AddMonths(-1)][vv.Key].HasValue
                                                                        ? vv.Value - 
                                                                            i.FillFormValues[v.Key.AddMonths(-1)][vv.Key]
                                                                        : null
                                                                    : null
                                                                : null
                                                            : null)
                                                .ToList(),
                                        })
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv =>
                                            vv.Volumes.Count(vvv => !vvv.HasValue) == 0
                                                ? (decimal?)vv.Volumes.Sum()
                                                : null),*/

                            i.DecCustomerVolumes,
                            i.DecCustomerRecalculations,
                            i.DecCollectiveVolumes,
                            i.DecCollectiveSquares,
                            i.DecCalculationMethods,
                        })
                        .OrderBy(b => b.Address)
                        .ToList();

            var table = new DataTable();

            columns.ForEach(c => table.Columns.Add(c.FieldName, c.ColumnType));

            foreach (var item in processedItems)
            {
                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "Расчет ОДН",
                        values: item.DecCalculationMethods,
                        calculateAvarageAndSum: false));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "ОДПУ УК",
                        values: item.BuildingCounterVolumes));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "ОДПУ ДЭК (объем для норм. и сред.)",
                        values: item.DecBuildingCounterVolumes,
                        checkValues: item.DecCalculationMethods,
                        alternativeValues: item.DecCollectiveVolumes,
                        comparison: (x, y, z) =>
                        {
                            return
                                x == (decimal?)CalculationMethod.BuildingCounters
                                    ? y
                                    : z;
                        },
                        replaceNegativeValues: true));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "Показания юр. лиц",
                        values: item.DecLegalEntityVolumes));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "К распределению ДЭК",
                        values: item.DecBuildingCounterVolumes,
                        secondValues: item.DecLegalEntityVolumes,
                        operation: (x, y) =>
                        {
                            return
                                x.HasValue
                                    ? x - (y ?? 0)
                                    : null;
                        },
                        checkValues: item.DecCalculationMethods,
                        alternativeValues: item.DecCollectiveVolumes,
                        comparison: (x, y, z) =>
                        {
                            return
                                x == (decimal?)CalculationMethod.BuildingCounters
                                    ? y
                                    : z;
                        },
                        replaceNegativeValues: true));

                /*table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "ИПУ УК",
                        values: item.FillFormVolumes));*/

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "ИПУ ДЭК",
                        values: item.DecCustomerVolumes));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "Перерасчет ИПУ ДЭК",
                        values: item.DecCustomerRecalculations));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "Процент ОДН ДЭК от ИПУ ДЭК",
                        values: item.DecCollectiveVolumes,
                        secondValues: item.DecCustomerVolumes,
                        operation: (x, y) =>
                        {
                            return
                                x.HasValue && y.HasValue && y != 0
                                    ? x / y * 100
                                    : null;
                        },
                        replaceNegativeValues: true));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "ОДН ДЭК",
                        values: item.DecCollectiveVolumes,
                        replaceNegativeValues: true));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "Баланс",
                        values: item.DecCollectiveVolumes,
                        replacePositiveValues: true));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "Проверка",
                        values: item.DecBuildingCounterVolumes,
                        secondValues: item.DecLegalEntityVolumes,
                        thirdValues: item.DecCustomerVolumes,
                        operation: (x, y) =>
                        {
                            return
                                x.HasValue
                                    ? x - (y ?? 0)
                                    : null;
                        },
                        secondOperation: (x, y) =>
                        {
                            return
                                x.HasValue
                                    ? x - (y ?? 0)
                                    : null;
                        }));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        "Площадь МОП",
                        values: item.DecCollectiveSquares));

                table.Rows.Add(
                    CreateRow(
                        table,
                        columns,
                        item.Address,
                        null));
            }

            return table;
        }

        private static DataRow CreateRow(
            DataTable table,
            List<Column> columns,
            string building,
            string param,
            Dictionary<string, decimal?> values = null,
            Dictionary<string, decimal?> secondValues = null,
            Dictionary<string, decimal?> thirdValues = null,
            Func<decimal?, decimal?, decimal?> operation = null,
            Func<decimal?, decimal?, decimal?> secondOperation = null,
            Dictionary<string, decimal?> checkValues = null,
            Dictionary<string, decimal?> alternativeValues = null,
            Func<decimal?, decimal?, decimal?, decimal?> comparison = null,
            bool replaceNegativeValues = false,
            bool replacePositiveValues = false,
            bool calculateAvarageAndSum = true)
        {
            int extraColumnsCount = columns.Count - FIXED_COLUMNS_COUNT;
            int avarageCount = 0;
            decimal? sum = null;

            var row = table.NewRow();

            row[BUILDING_COLUMN] =
                !string.IsNullOrEmpty(building)
                    ? building
                    : (object)DBNull.Value;

            row[PARAMS_COLUMN] =
                !string.IsNullOrEmpty(param)
                    ? param
                    : (object)DBNull.Value;


            for (int i = FIRST_EXTRA_COLUMN_INDEX; i < extraColumnsCount; i++)
            {
                var columnName = columns.ElementAt(i).FieldName;

                var value =
                    values != null
                        ? values.ContainsKey(columnName)
                            ? values[columnName]
                            : null
                        : null;

                var secondValue =
                    secondValues != null
                        ? secondValues.ContainsKey(columnName)
                            ? secondValues[columnName]
                            : null
                        : null;

                var thirdValue =
                    thirdValues != null
                        ? thirdValues.ContainsKey(columnName)
                            ? thirdValues[columnName]
                            : null
                        : null;

                var cellValue =
                    operation != null
                        ? operation(value, secondValue)
                        : value;

                cellValue =
                    secondOperation != null
                        ? secondOperation(cellValue, thirdValue)
                        : cellValue;

                var checkValue =
                    checkValues != null
                        ? checkValues.ContainsKey(columnName)
                            ? checkValues[columnName]
                            : null
                        : null;

                var alternativeValue =
                    alternativeValues != null
                        ? alternativeValues.ContainsKey(columnName)
                            ? alternativeValues[columnName]
                            : null
                        : null;

                cellValue =
                    comparison != null
                        ? comparison(checkValue, cellValue, alternativeValue)
                        : cellValue;

                cellValue =
                    cellValue.HasValue
                        ? replaceNegativeValues && cellValue < 0
                            ? null
                            : cellValue
                        : null;

                cellValue =
                    cellValue.HasValue
                        ? replacePositiveValues && cellValue > 0
                            ? null
                            : cellValue
                        : null;

                if (calculateAvarageAndSum)
                {
                    avarageCount += cellValue.HasValue ? 1 : 0;

                    sum =
                        sum.HasValue
                            ? sum + (cellValue ?? 0)
                            : cellValue;
                }

                row[columnName] =
                    cellValue.HasValue
                        ? cellValue
                        : (object)DBNull.Value;
            }

            var avarage =
                avarageCount > 0
                    ? sum / avarageCount
                    : null;

            row[AVARAGE_COLUMN] =
                avarage.HasValue
                    ? avarage
                        : (object)DBNull.Value;

            row[SUM_COLUMN] =
                sum.HasValue
                    ? sum
                    : (object)DBNull.Value;

            return row;
        }
    }
}
