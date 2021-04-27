using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser
{
    static public class RowParser
    {
        const string ADDRESS_COLUMN = "C";
        const string COUNTER_NUMBER_COLUMN = "E";
        const string CURRENT_VALUE_COLUMN = "F";
        const string PREV_VALUE_COLUMN = "G";
        const string COEFFICIENT_COLUMN = "I";

        public static bool TryParseRow(
            ExcelSheet source,
            int rowNumber,
            out BuildingValuesRows row)
        {
            row = new BuildingValuesRows();

            try
            {
                if (!CellParser.TryParseAddress(
                    source.GetCellText($"{ADDRESS_COLUMN}{rowNumber}"),
                    out string street,
                    out string building,
                    out string description))
                {
                    BuildingValuesRowHandler.SetParsingError(
                        row,
                        ADDRESS_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CellParser.TryParseCounterNumber(
                    source.GetCellText($"{COUNTER_NUMBER_COLUMN}{rowNumber}"),
                    out string counterNumber,
                    out description))
                {
                    BuildingValuesRowHandler.SetParsingError(
                        row,
                        COUNTER_NUMBER_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CellParser.TryParseCurrentValue(
                    source.GetCellText($"{CURRENT_VALUE_COLUMN}{rowNumber}"),
                    out decimal? currentValue,
                    out description))
                {
                    BuildingValuesRowHandler.SetParsingError(
                        row,
                        CURRENT_VALUE_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CellParser.TryParsePrevValue(
                    source.GetCellText($"{PREV_VALUE_COLUMN}{rowNumber}"),
                    out decimal? prevValue,
                    out description))
                {
                    BuildingValuesRowHandler.SetParsingError(
                        row,
                        PREV_VALUE_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CellParser.TryParseCoefficient(
                    source.GetCellText($"{COEFFICIENT_COLUMN}{rowNumber}"),
                    out byte? coefficient,
                    out description))
                {
                    BuildingValuesRowHandler.SetParsingError(
                        row,
                        COEFFICIENT_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.Street = street;
                row.Building = building;
                row.CounterNumber = counterNumber;
                row.CurrentValue = currentValue;
                row.PrevValue = prevValue;
                row.Coefficient = coefficient;

                row.ProcessingResult = (byte)RowProcessingResult.OK;
            }
            catch (Exception exception)
            {
                BuildingValuesRowHandler.SetParsingError(row, exception);
                return false;
            }

            return true;
        }

        public static bool IsAddressCellEmpty(
            ExcelSheet source,
            int rowNumber)
        {
            return
                string.IsNullOrEmpty(
                    source.GetCellText($"{ADDRESS_COLUMN}{rowNumber}"));
        }
    }
}
