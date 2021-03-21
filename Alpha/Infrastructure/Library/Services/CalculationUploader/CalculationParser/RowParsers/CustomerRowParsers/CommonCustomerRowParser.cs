using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .CustomerRowParsers
{
    public static class CommonCustomerRowParser
    {
        const string ACCOUNT_COLUMN = "A";
        const string APARTMENT_COLUMN = "C";
        const string OWNER_COLUMN = "D";
        const string HAS_COUNTER_COLUMN = "E";
        const string COUNTER_TYPE_COLUMN = "F";
        const string VOLUME_COLUMN = "G";
        const string RECALCULATION_COLUMN = "H";

        const string COMMON_COUNTER_TYPE_TEXT = "суточная";
        const string DAY_COUNTER_TYPE_TEXT = "2-х зонн. день";
        const string NIGHT_COUNTER_TYPE_TEXT = "2-х зонн. ночь";

        public static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            string squareColumnName,
            string residentsColumnName,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.Customer,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!CustomerCellParser.TryParseAccount(
                    source.GetCellText($"{ACCOUNT_COLUMN}{rowNumber}"),
                    out string account,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        ACCOUNT_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CustomerCellParser.TryParseApartment(
                    source.GetCellText($"{APARTMENT_COLUMN}{rowNumber}"),
                    out string apartment,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        APARTMENT_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CustomerCellParser.TryParseOwner(
                    source.GetCellText($"{OWNER_COLUMN}{rowNumber}"),
                    out string owner,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        OWNER_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                bool hasCounter =
                    CustomerCellParser.ParseHasCounter(
                        source.GetCellText($"{HAS_COUNTER_COLUMN}{rowNumber}"));

                if (!CustomerCellParser.TryParseCounterType(
                    source.GetCellText($"{COUNTER_TYPE_COLUMN}{rowNumber}"),
                    hasCounter,
                    out CalculationCounterType counterType,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        COUNTER_TYPE_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CustomerCellParser.TryParseVolume(
                    source.GetCellText($"{VOLUME_COLUMN}{rowNumber}"),
                    out decimal? volume,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        VOLUME_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CustomerCellParser.TryParseRecalculation(
                    source.GetCellText($"{RECALCULATION_COLUMN}{rowNumber}"),
                    out decimal? recalculation,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        RECALCULATION_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CustomerCellParser.TryParseSquare(
                    source.GetCellText($"{squareColumnName}{rowNumber}"),
                    out decimal? square,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        squareColumnName,
                        rowNumber,
                        description);
                    return false;
                }

                if (!CustomerCellParser.TryParseResidents(
                    source.GetCellText($"{residentsColumnName}{rowNumber}"),
                    out byte? residents,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        residentsColumnName,
                        rowNumber,
                        description);
                    return false;
                }

                row.Customer = new CalculationCustomers()
                {
                    Account = account,
                    Apartment = apartment,
                    Owner = owner,
                    CounterType = (byte)counterType,
                    Volume = volume.Value,
                    Recalculation = recalculation.Value,
                    Square = square,
                    Residents = residents.Value,
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

        public static bool IsAppropriateCounterType(
            ExcelSheet source,
            int rowNumber)
        {
            return
                source.GetCellText($"{COUNTER_TYPE_COLUMN}{rowNumber}") == COMMON_COUNTER_TYPE_TEXT
                    || source.GetCellText($"{COUNTER_TYPE_COLUMN}{rowNumber}") == DAY_COUNTER_TYPE_TEXT
                    || source.GetCellText($"{COUNTER_TYPE_COLUMN}{rowNumber}") == NIGHT_COUNTER_TYPE_TEXT;
        }
    }
}
