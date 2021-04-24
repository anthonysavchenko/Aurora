using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Models
{
    public static class DataSource
    {
        public const int MONTH_COUNT = 12;
        
        public const string SPECIAL_CELLS_FORMAT_COLUMN = "SpecialCellsFormat";

        private const int FIRST_MONTH_COLUMN_INDEX = 3;

        private const string CONTRACT_COLUMN = "Contract";
        private const string BUILDING_COLUMN = "Building";
        private const string PARAMS_COLUMN = "Params";
        private const string NORM_COLUMN = "Norm";
        private const string AVARAGE_COLUMN = "Avarage";
        private const string SUM_COLUMN = "Sum";

        public static Column[] GetDataSourceColumns(DateTime since)
        {
            var columns = new List<Column>
            {
                new Column
                {
                    FieldName = CONTRACT_COLUMN,
                    ColumnType = typeof(string),
                    Visible = true,
                    IsNote = false,
                    Format = ColumnFormat.Default,
                    GridHeader = "Договор",
                    ExcelHeader = "Договор",
                    ExcelHeaderFormat = "@",
                    ExcelWidth = 10,
                },
                new Column
                {
                    FieldName = BUILDING_COLUMN,
                    ColumnType = typeof(string),
                    Visible = true,
                    IsNote = false,
                    Format = ColumnFormat.Default,
                    GridHeader = "Дом",
                    ExcelHeader = "Дом",
                    ExcelHeaderFormat = "@",
                    ExcelWidth = 35,
                },
                new Column
                {
                    FieldName = PARAMS_COLUMN,
                    ColumnType = typeof(string),
                    Visible = true,
                    IsNote = false,
                    Format = ColumnFormat.Default,
                    GridHeader = "Параметры",
                    ExcelHeader = "Параметры",
                    ExcelHeaderFormat = "@",
                    ExcelWidth = 35,
                },
            };

            columns.AddRange(
                Enumerable
                    .Range(0, MONTH_COUNT)
                    .Select(month => since.AddMonths(month))
                    .SelectMany(month =>
                        new List<Column>
                        {
                            new Column
                            {
                                FieldName = $"{month:MM.yyyy}",
                                ColumnType = typeof(decimal),
                                Visible = true,
                                IsNote = false,
                                Format = ColumnFormat.Special,
                                GridHeader = $"{month:MM.yyyy}",
                                ExcelHeader = month,
                                ExcelHeaderFormat = "MM.yyyy",
                                ExcelWidth = 10,
                            },
                            new Column
                            {
                                FieldName = $"{month:MM.yyyy}-Draft",
                                ColumnType = typeof(decimal),
                                Visible = false,
                                IsNote = true,
                                Format = ColumnFormat.Decimal,
                                GridHeader = string.Empty,
                                ExcelHeader = string.Empty,
                                ExcelHeaderFormat = string.Empty,
                                ExcelWidth = 0,
                            },
                        })
                    .ToList());

            columns.AddRange(
                new List<Column>
                {
                    new Column
                    {
                        FieldName = NORM_COLUMN,
                        ColumnType = typeof(decimal),
                        Visible = true,
                        IsNote = false,
                        Format = ColumnFormat.Decimal,
                        GridHeader = "Норматив",
                        ExcelHeader = "Норматив",
                        ExcelHeaderFormat = "@",
                        ExcelWidth = 10,
                    },
                    new Column
                    {
                        FieldName = AVARAGE_COLUMN,
                        ColumnType = typeof(decimal),
                        Visible = true,
                        IsNote = false,
                        Format = ColumnFormat.Special,
                        GridHeader = "Среднее",
                        ExcelHeader = "Среднее",
                        ExcelHeaderFormat = "@",
                        ExcelWidth = 10,
                    },
                    new Column
                    {
                        FieldName = SUM_COLUMN,
                        ColumnType = typeof(decimal),
                        Visible = true,
                        IsNote = false,
                        Format = ColumnFormat.Special,
                        GridHeader = "Сумма",
                        ExcelHeader = "Сумма",
                        ExcelHeaderFormat = "@",
                        ExcelWidth = 10,
                    },
                    new Column
                    {
                        FieldName = SPECIAL_CELLS_FORMAT_COLUMN,
                        ColumnType = typeof(byte),
                        Visible = false,
                        IsNote = false,
                        Format = ColumnFormat.Default,
                        GridHeader = string.Empty,
                        ExcelHeader = string.Empty,
                        ExcelHeaderFormat = string.Empty,
                        ExcelWidth = 0,
                    },
                });

            var excelColumnNames =
                Enumerable
                    .Range('a', columns.Where(c => c.Visible).Count())
                    .Select(c => ((char)c).ToString().ToUpperInvariant())
                    .ToList();

            for (int i = 0, j = 0; i < columns.Count; i++)
            {
                if (columns[i].Visible)
                {
                    columns[i].ExcelName = excelColumnNames[j];
                    j++;
                }
                else if (!columns[i].Visible && columns[i].IsNote)
                {
                    columns[i].ExcelName = excelColumnNames[j - 1];
                }
            }

            return columns.ToArray();
        }

        public static int FindIndex(int visibleIndex)
        {
            if (visibleIndex < FIRST_MONTH_COLUMN_INDEX)
                return visibleIndex;

            if (visibleIndex < FIRST_MONTH_COLUMN_INDEX + MONTH_COUNT)
                return visibleIndex * 2 - FIRST_MONTH_COLUMN_INDEX;

            return visibleIndex;
        }

        public static void CreateDataTableColumns(DataTable table, Column[] columns)
        {
            foreach (var column in columns)
            {
                table.Columns.Add(column.FieldName, column.ColumnType);
            }
        }

        public static DataRow CreateDataTableRow(
            DataTable table,
            Column[] columns,
            string contract,
            string building,
            string param,
            decimal? norm = null,
            CellFormat specialCellsFormat = CellFormat.Numeric,
            Dictionary<string, decimal?> values = null,
            Dictionary<string, decimal?> secondValues = null,
            Dictionary<string, decimal?> thirdValues = null,
            Func<decimal?, decimal?, decimal?> operation = null,
            Func<decimal?, decimal?, decimal?> secondOperation = null,
            Dictionary<string, decimal?> checkValues = null,
            Dictionary<string, decimal?> alternativeValues = null,
            Func<decimal?, decimal?, decimal?, decimal?> comparison = null,
            Dictionary<string, decimal?> drafts = null,
            bool replaceNegativeValues = false,
            bool replacePositiveValues = false,
            bool calculateAvarageAndSum = true)
        {
            int avarageCount = 0;
            decimal? sum = null;

            var row = table.NewRow();

            row[CONTRACT_COLUMN] =
                !string.IsNullOrEmpty(contract)
                    ? contract
                    : (object)DBNull.Value;

            row[BUILDING_COLUMN] =
                !string.IsNullOrEmpty(building)
                    ? building
                    : (object)DBNull.Value;

            row[PARAMS_COLUMN] =
                !string.IsNullOrEmpty(param)
                    ? param
                    : (object)DBNull.Value;


            for (int i = FIRST_MONTH_COLUMN_INDEX; i < FIRST_MONTH_COLUMN_INDEX + MONTH_COUNT * 2; i += 2)
            {
                var value =
                    values != null
                        ? values.ContainsKey(columns[i].FieldName)
                            ? values[columns[i].FieldName]
                            : null
                        : null;

                var secondValue =
                    secondValues != null
                        ? secondValues.ContainsKey(columns[i].FieldName)
                            ? secondValues[columns[i].FieldName]
                            : null
                        : null;

                var thirdValue =
                    thirdValues != null
                        ? thirdValues.ContainsKey(columns[i].FieldName)
                            ? thirdValues[columns[i].FieldName]
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
                        ? checkValues.ContainsKey(columns[i].FieldName)
                            ? checkValues[columns[i].FieldName]
                            : null
                        : null;

                var alternativeValue =
                    alternativeValues != null
                        ? alternativeValues.ContainsKey(columns[i].FieldName)
                            ? alternativeValues[columns[i].FieldName]
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

                row[columns[i].FieldName] =
                    cellValue.HasValue
                        ? cellValue
                        : (object)DBNull.Value;

                var draft =
                    drafts != null
                        ? drafts.ContainsKey(columns[i + 1].FieldName)
                            ? drafts[columns[i + 1].FieldName]
                            : null
                        : null;

                row[columns[i + 1].FieldName] =
                    draft.HasValue
                        ? draft.Value
                        : (object)DBNull.Value;
            }

            row[NORM_COLUMN] =
                norm.HasValue
                    ? norm
                    : (object)DBNull.Value;

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

            row[SPECIAL_CELLS_FORMAT_COLUMN] =
                (byte)specialCellsFormat;

            return row;
        }

        public static string GetGridCellDisplayText(
            object source,
            ColumnFormat columnFormat,
            CellFormat cellFormat)
        {
            switch (columnFormat)
            {
                case ColumnFormat.Special:
                    switch (cellFormat)
                    {
                        case CellFormat.CalculationMethod:
                            return
                                source is decimal && (decimal)source == (decimal)CalculationMethod.BuildingCounters
                                    ? "ОДПУ"
                                    : source is decimal && (decimal)source == (decimal)CalculationMethod.Norm
                                        ? "Норматив"
                                        : source is decimal && (decimal)source == (decimal)CalculationMethod.Avarage
                                            ? "Среднее"
                                            : "Не определено";
                        case CellFormat.Percent:
                            return $"{source:n2} %";
                        case CellFormat.Numeric:
                        default:
                            return $"{source:n2}";
                    }
                case ColumnFormat.Decimal:
                    return $"{source:n2}";
                case ColumnFormat.Default:
                default:
                    return $"{source}";
            }
        }

        public static Color GetGridCellTextColor(object source)
        {
            return
                source is decimal && (decimal)source < 0
                    ? Color.Red
                    : Color.Black;
        }

        public static object GetExcelCellValue(
            object source,
            ColumnFormat columnFormat,
            CellFormat cellFormat)
        {
            switch (columnFormat)
            {
                case ColumnFormat.Special:
                    switch (cellFormat)
                    {
                        case CellFormat.CalculationMethod:
                            return
                                source is decimal && (decimal)source == (decimal)CalculationMethod.BuildingCounters
                                    ? "ОДПУ"
                                    : source is decimal && (decimal)source == (decimal)CalculationMethod.Norm
                                        ? "Норматив"
                                        : source is decimal && (decimal)source == (decimal)CalculationMethod.Avarage
                                            ? "Среднее"
                                            : "Не определено";
                        case CellFormat.Percent:
                        case CellFormat.Numeric:
                        default:
                            return source;
                    }
                case ColumnFormat.Default:
                case ColumnFormat.Decimal:
                default:
                    return source;
            }
        }

        public static string GetExcelCellFormat(ColumnFormat columnFormat, CellFormat cellFormat)
        {
            switch (columnFormat)
            {
                case ColumnFormat.Special:
                    switch (cellFormat)
                    {
                        case CellFormat.CalculationMethod:
                            return "@";
                        case CellFormat.Percent:
                            return "0.00 \"%\"";
                        case CellFormat.Numeric:
                        default:
                            return "0.00;[Red]-0.00";
                    }
                case ColumnFormat.Decimal:
                    return "0.00;[Red]-0.00";
                case ColumnFormat.Default:
                default:
                    return "@";
            }
        }

        public static string GetNoteValue(object source, ColumnFormat columnFormat)
        {
            switch (columnFormat)
            {
                case ColumnFormat.Decimal:
                    return $"{source:0.00}";
                case ColumnFormat.Special:
                case ColumnFormat.Default:
                default:
                    return $"{source}";
            }
        }
    }
}
