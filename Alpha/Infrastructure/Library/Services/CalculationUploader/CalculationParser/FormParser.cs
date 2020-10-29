﻿using System;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser
{
    public static class FormParser
    {
        public static void ParseForm(
            int fileID,
            ExcelSheet source,
            int rowsCount)
        {
            var rows = new List<CalculationRows>();
            var rowNumber = 1;

            try
            {
                if (!AddSkippedRows(
                    1,
                    ref rowNumber,
                    rowsCount,
                    rows))
                {
                    return;
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
                            rows))
                        {
                            var rowsLeft = rowsCount - rowNumber + 1;

                            if (!AddSkippedRows(
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
            }
            catch (Exception exception)
            {
                throw new Exception(
                    $"Программная ошибка при распознавании файла, rowNumber: {rowNumber + 1}",
                    exception);
            }

            CalculationFormHandler.CreateForm(fileID, rows);
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
                BuildingInfoRowParser.TryParseAddressRow(
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
            List<CalculationRows> rows)
        {
            if (!AddSkippedRows(
                6,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            if (rowNumber + 2 <= rowsCount
                && BuildingCounterRowParser.IsCounterNumberCellEmpty(source, rowNumber)
                && !BuildingInfoRowParser.IsDebtCellEmpty(source, rowNumber)
                && !CommonRowParser.IsFirstCellTotal(source, rowNumber + 2))
            {
                if (!TryParseBuildingDebt(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows))
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
                out CalculationMethod calculationMethod))
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
                    rows))
                {
                    return false;
                }
            }

            if (!AddSkippedRows(
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
                rows))
            {
                return false;
            }

            if (!TryParseCustomers(
                source,
                buildingAddressRow,
                ref rowNumber,
                rowsCount,
                rows))
            {
                return false;
            }

            if (rowNumber + 1 <= rowsCount
                && CommonRowParser.IsFirstCellTotal(source, rowNumber + 1))
            {
                if (!AddSkippedRows(
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
                if (!AddSkippedRows(
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
                if (!AddSkippedRows(
                    5,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    buildingAddressRow))
                {
                    return false;
                }
            }

            if (!TryParseBuildingInfo(
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
            List<CalculationRows> rows)
        {
            return
                TryParseRow(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    BuildingInfoRowParser.TryParseDebtRow);
        }

        private static bool TryParseCalculationMethod(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            out CalculationMethod calculationMethod)
        {
            calculationMethod = CalculationMethod.Unknown;

            if (rowNumber > rowsCount)
            {
                AddMissedRow(rows, buildingAddressRow);
                return false;
            }

            var parsedWithNoErrors =
                BuildingInfoRowParser.TryParseCalculationMethodRow(
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
            List<CalculationRows> rows)
        {
            while (!AreBuildingCountersFinished(source, rowNumber, rowsCount))
            {
                if (!TryParseRow(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    BuildingCounterRowParser.TryParseRow))
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
            List<CalculationRows> rows)
        {
            while (!AreLegalEntitiesFinished(source, rowNumber, rowsCount))
            {
                if (!TryParseRow(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    LegalEntityRowParser.TryParseRow))
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
            List<CalculationRows> rows)
        {
            while (!AreCustomersFinished(source, rowNumber, rowsCount))
            {
                if (!TryParseRow(
                    source,
                    buildingAddressRow,
                    ref rowNumber,
                    rowsCount,
                    rows,
                    CustomerRowParser.TryParseRow))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool TryParseBuildingInfo(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            CalculationMethod calculationMethod,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows)
        {
            if (!AddSkippedRows(
                1,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            if (!TryParseRow(
                source,
                buildingAddressRow,
                calculationMethod,
                ref rowNumber,
                rowsCount,
                rows,
                BuildingInfoRowParser.TryParseNormRow))
            {
                return false;
            }

            if (!AddSkippedRows(
                4,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            if (!TryParseRow(
                source,
                buildingAddressRow,
                ref rowNumber,
                rowsCount,
                rows,
                BuildingInfoRowParser.TryParseCollectiveVolumeRow))
            {
                return false;
            }

            if (!TryParseRow(
                source,
                buildingAddressRow,
                calculationMethod,
                ref rowNumber,
                rowsCount,
                rows,
                BuildingInfoRowParser.TryParseCollectiveSquareRow))
            {
                return false;
            }

            if (!AddSkippedRows(
                3,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            return true;
        }

        private static bool TryParseRow(
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

        private static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            CalculationMethod calculationMethod,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows,
            BuildingInfoRowParser.TryParseRowMethod tryParseRowMethod)
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

        private static bool AddSkippedRows(
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
                    && CustomerRowParser.IsAppropriateCounterType(source, rowNumber);
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
