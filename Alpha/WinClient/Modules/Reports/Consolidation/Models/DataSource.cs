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
        
        public const string VALUE_CELLS_FORMAT_COLUMN = "ValueCellsFormat";

        private const int FIRST_MONTH_COLUMN_INDEX = 3;

        private const string CONTRACT_COLUMN = "Contract";
        private const string BUILDING_COLUMN = "Building";
        private const string PARAMS_COLUMN = "Params";
        private const string AVARAGE_COLUMN = "Avarage";
        private const string SUM_COLUMN = "Sum";

        public static Column[] GetDataSourceColumns(DateTime since)
        {
            var columns = new List<Column>()
            {
                new Column()
                {
                    FieldName = CONTRACT_COLUMN,
                    ColumnType = typeof(string),
                    Visible = true,
                    ContentType = ColumnContentType.Description,
                    GridHeader = "Договор",
                    ExcelHeader = "Договор",
                    ExcelHeaderFormat = "@",
                    ExcelWidth = 10,
                },
                new Column()
                {
                    FieldName = BUILDING_COLUMN,
                    ColumnType = typeof(string),
                    Visible = true,
                    ContentType = ColumnContentType.Description,
                    GridHeader = "Дом",
                    ExcelHeader = "Дом",
                    ExcelHeaderFormat = "@",
                    ExcelWidth = 35,
                },
                new Column()
                {
                    FieldName = PARAMS_COLUMN,
                    ColumnType = typeof(string),
                    Visible = true,
                    ContentType = ColumnContentType.Description,
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
                    .Select(month =>
                        new Column
                        {
                            FieldName = $"{month:MM.yyyy}",
                            ColumnType = typeof(decimal),
                            Visible = true,
                            ContentType = ColumnContentType.Value,
                            GridHeader = $"{month:MM.yyyy}",
                            ExcelHeader = month,
                            ExcelHeaderFormat = "MM.yyyy",
                            ExcelWidth = 10,
                        })
                    .ToList());

            columns.Add(
                new Column()
                {
                    FieldName = AVARAGE_COLUMN,
                    ColumnType = typeof(decimal),
                    Visible = true,
                    ContentType = ColumnContentType.Value,
                    GridHeader = "Среднее",
                    ExcelHeader = "Среднее",
                    ExcelHeaderFormat = "@",
                    ExcelWidth = 10,
                });

            columns.Add(
                new Column()
                {
                    FieldName = SUM_COLUMN,
                    ColumnType = typeof(decimal),
                    Visible = true,
                    ContentType = ColumnContentType.Value,
                    GridHeader = "Сумма",
                    ExcelHeader = "Сумма",
                    ExcelHeaderFormat = "@",
                    ExcelWidth = 10,
                });

            columns.Add(
                new Column()
                {
                    FieldName = VALUE_CELLS_FORMAT_COLUMN,
                    ColumnType = typeof(byte),
                    Visible = false,
                    ContentType = ColumnContentType.Unknown,
                    GridHeader = string.Empty,
                    ExcelHeader = string.Empty,
                    ExcelHeaderFormat = string.Empty,
                    ExcelWidth = 0,
                });

            var excelColumnNames =
                Enumerable
                    .Range('a', columns.Count)
                    .Select(c => ((char)c).ToString().ToUpperInvariant())
                    .ToList();

            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].ExcelName = excelColumnNames[i];
            }

            return columns.ToArray();
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
            CellFormat valueCellsFormat = CellFormat.Numeric,
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


            for (int i = FIRST_MONTH_COLUMN_INDEX; i < FIRST_MONTH_COLUMN_INDEX + MONTH_COUNT; i++)
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

            row[VALUE_CELLS_FORMAT_COLUMN] =
                (byte)valueCellsFormat;

            return row;
        }

        public static string GetGridCellDisplayText(
            object source,
            ColumnContentType columnContentType,
            CellFormat cellFormat)
        {
            if (columnContentType == ColumnContentType.Value)
            {
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
            }
            
            return $"{source}";
        }

        public static Color GetGridCellTextColor(object source, ColumnContentType columnContentType)
        {
            if (columnContentType == ColumnContentType.Value
                && source is decimal
                && (decimal)source < 0)
            {
                return Color.Red;
            }

            return Color.Black;
        }

        public static object GetExcelCellValue(
            object source,
            ColumnContentType columnContentType,
            CellFormat cellFormat)
        {
            if (columnContentType == ColumnContentType.Value)
            {
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
            }

            return source;
        }

        public static string GetExcelCellFormat(ColumnContentType columnContentType, CellFormat cellFormat)
        {
            if (columnContentType == ColumnContentType.Value)
            {
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
            }

            return "@";
        }
    }
}
