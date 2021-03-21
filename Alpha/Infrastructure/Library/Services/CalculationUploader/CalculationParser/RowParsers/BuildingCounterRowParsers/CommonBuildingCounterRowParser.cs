using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .BuildingInfoRowParsers
{
    public static class CommonBuildingCounterRowParser
    {
        const string MODEL_COLUMN = "C";
        const string COUNTER_NUMBER_COLUMN = "D";
        const string COEFFICIENT_COLUMN = "E";

        public static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            string currentValueColumnName,
            string prevValueColumnName,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingCounter,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!BuildingCounterCellParser.TryParseCounterNumber(
                    source.GetCellText($"{COUNTER_NUMBER_COLUMN}{rowNumber}"),
                    out string counterNumber,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        COUNTER_NUMBER_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!BuildingCounterCellParser.TryParseModel(
                    source.GetCellText($"{MODEL_COLUMN}{rowNumber}"),
                    out string model,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        MODEL_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!BuildingCounterCellParser.TryParseCoefficient(
                    source.GetCellText($"{COEFFICIENT_COLUMN}{rowNumber}"),
                    out byte? coefficient,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        COEFFICIENT_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!BuildingCounterCellParser.TryParseCurrentValue(
                    source.GetCellText($"{currentValueColumnName}{rowNumber}"),
                    out decimal? currentValue,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        currentValueColumnName,
                        rowNumber,
                        description);
                    return false;
                }

                if (!BuildingCounterCellParser.TryParsePrevValue(
                    source.GetCellText($"{prevValueColumnName}{rowNumber}"),
                    out decimal? prevValue,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        prevValueColumnName,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingCounter = new CalculationBuildingCounters()
                {
                    CounterNumber = counterNumber,
                    Model = model,
                    Coefficient = coefficient.Value,
                    CurrentValue = currentValue.Value,
                    PrevValue = prevValue.Value,
                };

                row.ProcessingResult = (byte)RowProcessingResult.OK;
            }
            catch (Exception e)
            {
                CalculationRowHandler.SetParsingError(row, e);
                return false;
            }

            return true;
        }

        public static bool IsCounterNumberCellEmpty(
            ExcelSheet source,
            int rowNumber)
        {
            return
                string.IsNullOrEmpty(
                    source.GetCellText($"{COUNTER_NUMBER_COLUMN}{rowNumber}"));
        }
    }
}
