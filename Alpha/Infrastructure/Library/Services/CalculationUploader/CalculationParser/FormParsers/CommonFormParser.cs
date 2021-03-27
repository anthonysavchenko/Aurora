using System;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .BuildingInfoRowParsers;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .CustomerRowParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.FormParsers
{
    public static class CommonFormParser
    {
        public delegate bool TryParseBuildingInfoMethod(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            CalculationMethod calculationMethod,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows);

        public static bool TryParseForm(
            ExcelSheet source,
            ref int rowNumber,
            int rowsCount,
            out List<CalculationRows> rows,
            CommonRowParser.TryParseRowMethod tryParseDebtRow,
            CommonBuildingInfoRowParser.TryParseRowMethod1 tryParseCalculationMethodRow,
            CommonRowParser.TryParseRowMethod tryParseBuildingCounterRow,
            CommonRowParser.TryParseRowMethod tryParseLegalEntityRow,
            CommonRowParser.TryParseRowMethod tryParseCustomerRow,
            TryParseBuildingInfoMethod tryParseBuildingInfo,
            CommonBuildingInfoRowParser.CheckRowMethod isDebtHeaderCellPresent,
            CommonBuildingInfoRowParser.CheckRowMethod isDebtCellEmpty)
        {
            rows = new List<CalculationRows>();
            rowNumber = 1;

            if (!TryAddSkippedRows(
                1,
                ref rowNumber,
                rowsCount,
                rows))
            {
                return false;
            }

            while (rowNumber <= rowsCount)
            {
                if (TryParseBuildingAddress(
                    source,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    out CalculationRows buildingAddressRow))
                {
                    if (TryParseBuilding(
                        source,
                        buildingAddressRow,
                        ref rowNumber,
                        rowsCount,
                        rows,
                        tryParseDebtRow,
                        tryParseCalculationMethodRow,
                        tryParseBuildingCounterRow,
                        tryParseLegalEntityRow,
                        tryParseCustomerRow,
                        tryParseBuildingInfo,
                        isDebtHeaderCellPresent,
                        isDebtCellEmpty))
                    {
                        var rowsLeft = rowsCount - rowNumber + 1;

                        if (!TryAddSkippedRows(
                            rowsLeft < 3 ? rowsLeft : 3,
                            ref rowNumber,
                            rowsCount,
                            rows))
                        {
                            break;
                        }
                    }
                }
            }

            return true;
        }

        private static bool TryParseBuildingAddress(
            ExcelSheet source,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            out CalculationRows buildingAddressRow)
        {
            buildingAddressRow = null;

            if (rowNumber > rowsCount)
            {
                AddMissedRow(rows);
                return false;
            }

            var parsedWithNoErrors =
                CommonBuildingInfoRowParser.TryParseAddressRow(
                    source,
                    rowNumber,
                    out buildingAddressRow);

            rows.Add(buildingAddressRow);
            rowNumber++;

            if (!parsedWithNoErrors)
            {
                return false;
            }

            return true;
        }

        private static bool TryParseBuilding(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            CommonRowParser.TryParseRowMethod tryParseDebtRow,
            CommonBuildingInfoRowParser.TryParseRowMethod1 tryParseCalculationMethodRow,
            CommonRowParser.TryParseRowMethod tryParseBuildingCounterRow,
            CommonRowParser.TryParseRowMethod tryParseLegalEntityRow,
            CommonRowParser.TryParseRowMethod tryParseCustomerRow,
            TryParseBuildingInfoMethod tryParseBuildingInfo,
            CommonBuildingInfoRowParser.CheckRowMethod isDebtHeaderCellPresent,
            CommonBuildingInfoRowParser.CheckRowMethod isDebtCellEmpty)
        {
            if (!TryAddSkippedRows(
                6,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            if (rowNumber + 2 <= rowsCount
                && isDebtHeaderCellPresent(source, rowNumber)
                && !isDebtCellEmpty(source, rowNumber)
                && !CommonRowParser.IsFirstCellTotal(source, rowNumber + 2))
            {
                if (!TryParseBuildingDebt(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    tryParseDebtRow))
                {
                    return false;
                }
            }

            if (!TryParseCalculationMethod(
                source,
                buildingAddressRow,
                ref rowNumber,
                rowsCount,
                rows,
                out CalculationMethod calculationMethod,
                tryParseCalculationMethodRow))
            {
                return false;
            }

            if (calculationMethod == CalculationMethod.BuildingCounters)
            {
                if (!TryParseBuildingCounters(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    tryParseBuildingCounterRow))
                {
                    return false;
                }
            }

            if (!TryAddSkippedRows(
                4,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            if (!TryParseLegalEntities(
                source,
                buildingAddressRow,
                ref rowNumber,
                rowsCount,
                rows,
                tryParseLegalEntityRow))
            {
                return false;
            }

            if (!TryParseCustomers(
                source,
                buildingAddressRow,
                ref rowNumber,
                rowsCount,
                rows,
                tryParseCustomerRow))
            {
                return false;
            }

            if (rowNumber + 1 <= rowsCount
                && CommonRowParser.IsFirstCellTotal(source, rowNumber + 1))
            {
                if (!TryAddSkippedRows(
                    3,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    buildingAddressRow))
                {
                    return false;
                }
            }
            else if (rowNumber + 2 <= rowsCount
                && CommonRowParser.IsFirstCellTotal(source, rowNumber + 2))
            {
                if (!TryAddSkippedRows(
                    4,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    buildingAddressRow))
                {
                    return false;
                }
            }
            else
            {
                if (!TryAddSkippedRows(
                    5,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    buildingAddressRow))
                {
                    return false;
                }
            }

            if (!tryParseBuildingInfo(
                source,
                buildingAddressRow,
                calculationMethod,
                ref rowNumber,
                rowsCount,
                rows))
            {
                return false;
            }

            return true;
        }

        private static bool TryParseBuildingDebt(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            CommonRowParser.TryParseRowMethod tryParseDebtRow)
        {
            return
                TryParseRow(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    tryParseDebtRow);
        }

        private static bool TryParseCalculationMethod(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            out CalculationMethod calculationMethod,
            CommonBuildingInfoRowParser.TryParseRowMethod1 tryParseCalculationMethodRow)
        {
            calculationMethod = CalculationMethod.Unknown;

            if (rowNumber > rowsCount)
            {
                AddMissedRow(rows, buildingAddressRow);
                return false;
            }

            var parsedWithNoErrors =
                tryParseCalculationMethodRow(
                    source,
                    buildingAddressRow,
                    rowNumber,
                    out calculationMethod,
                    out CalculationRows row);

            if (calculationMethod != CalculationMethod.BuildingCounters
                || !parsedWithNoErrors)
            {
                rows.Add(row);
                rowNumber++;
            }

            if (!parsedWithNoErrors)
            {
                return false;
            }

            return true;
        }

        private static bool TryParseBuildingCounters(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            CommonRowParser.TryParseRowMethod tryParseBuildingCounterRow)
        {
            while (!AreBuildingCountersFinished(source, rowNumber, rowsCount))
            {
                if (!TryParseRow(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    tryParseBuildingCounterRow))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool TryParseLegalEntities(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            CommonRowParser.TryParseRowMethod tryParseLegalEntityRow)
        {
            while (!AreLegalEntitiesFinished(source, rowNumber, rowsCount))
            {
                if (!TryParseRow(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    tryParseLegalEntityRow))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool TryParseCustomers(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            CommonRowParser.TryParseRowMethod tryParseCustomerRow)
        {
            while (!AreCustomersFinished(source, rowNumber, rowsCount))
            {
                if (!TryParseRow(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    tryParseCustomerRow))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            CommonRowParser.TryParseRowMethod tryParseRowMethod)
        {
            if (rowNumber > rowsCount)
            {
                AddMissedRow(rows, buildingAddressRow);
                return false;
            }

            var parsedWithNoErrors =
                tryParseRowMethod(
                    source,
                    buildingAddressRow,
                    rowNumber,
                    out CalculationRows row);

            rows.Add(row);
            rowNumber++;

            if (!parsedWithNoErrors)
            {
                return false;
            }

            return true;
        }

        public static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            CalculationMethod calculationMethod,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            CommonBuildingInfoRowParser.TryParseRowMethod2 tryParseRowMethod)
        {
            if (rowNumber > rowsCount)
            {
                AddMissedRow(rows, buildingAddressRow);
                return false;
            }

            var parsedWithNoErrors =
                tryParseRowMethod(
                    source,
                    buildingAddressRow,
                    calculationMethod,
                    rowNumber,
                    out CalculationRows row);

            rows.Add(row);
            rowNumber++;

            if (!parsedWithNoErrors)
            {
                return false;
            }

            return true;
        }

        public static bool TryAddSkippedRows(
            int quantity,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            CalculationRows buildingAddressRow = null)
        {
            for (int i = 0; i < quantity; i++)
            {
                if (rowNumber > rowsCount)
                {
                    AddMissedRow(rows, buildingAddressRow);
                    return false;
                }

                rows.Add(
                    new CalculationRows()
                    {
                        ProcessingResult = (byte)RowProcessingResult.Skipped,
                        RowType = (byte)CalculationRowType.Unknown,
                        BuildingAddressRow = buildingAddressRow,
                    });

                rowNumber++;
            }

            return true;
        }

        private static void AddMissedRow(
            List<CalculationRows> rows,
            CalculationRows buildingAddressRow = null)
        {
            var row =
                new CalculationRows()
                {
                    RowType = (byte)CalculationRowType.Unknown,
                    BuildingAddressRow = buildingAddressRow,
                };

            CalculationRowHandler.SetParsingError(
                row,
                "В файле отсутствует одна или несколько строк, " +
                    "которые обязательно должны быть в соответствии с форматом файла.");
            rows.Add(row);
        }

        private static bool AreBuildingCountersFinished(
            ExcelSheet source,
            int rowNumber,
            int rowsCount)
        {
            return
                rowNumber + 1 <= rowsCount
                    && CommonRowParser.IsFirstCellEmpty(source, rowNumber)
                    && CommonRowParser.IsFirstCellTotal(source, rowNumber + 1);
        }

        private static bool AreLegalEntitiesFinished(
            ExcelSheet source,
            int rowNumber,
            int rowsCount)
        {
            return
                rowNumber <= rowsCount
                    && CommonCustomerRowParser.IsAppropriateCounterType(source, rowNumber);
        }

        private static bool AreCustomersFinished(
            ExcelSheet source,
            int rowNumber,
            int rowsCount)
        {
            return
                (rowNumber + 1 <= rowsCount
                    && CommonRowParser.IsFirstCellEmpty(source, rowNumber)
                    && CommonRowParser.IsFirstCellTotal(source, rowNumber + 1))
                        || (rowNumber + 2 <= rowsCount
                            && CommonRowParser.IsFirstCellEmpty(source, rowNumber)
                            && CommonRowParser.IsFirstCellEmpty(source, rowNumber + 1)
                            && CommonRowParser.IsFirstCellTotal(source, rowNumber + 2))
                        || (rowNumber + 3 <= rowsCount
                            && CommonRowParser.IsFirstCellEmpty(source, rowNumber)
                            && CommonRowParser.IsFirstCellEmpty(source, rowNumber + 1)
                            && CommonRowParser.IsFirstCellEmpty(source, rowNumber + 2)
                            && CommonRowParser.IsFirstCellTotal(source, rowNumber + 3));
        }
    }
}
