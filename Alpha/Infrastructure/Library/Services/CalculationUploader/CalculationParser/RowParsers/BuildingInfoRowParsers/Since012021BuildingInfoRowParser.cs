using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .BuildingInfoRowParsers
{
    public static class Since012021BuildingInfoRowParser
    {
        const string DEBT_COLUMN = "K";
        const string VOLUME_COLUMN = "K";
        const string PERIOD_VOLUME_COLUMN = "J";
        const string REST_COLUMN = "J";
        const string DEBT_HEADER_COLUMN = "D";

        const string DEBT_HEADER = "Расчетный объём";

        public static bool TryParseDebtRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row)
        {
            return
                CommonBuildingInfoRowParser.TryParseDebtRow(
                    source,
                    buildingAddressRow,
                    rowNumber,
                    DEBT_COLUMN,
                    out row);
        }

        public static bool TryParseCalculationMethodRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationMethod calculationMethod,
            out CalculationRows row)
        {
            return
                CommonBuildingInfoRowParser.TryParseCalculationMethodRow(
                    source,
                    buildingAddressRow,
                    rowNumber,
                    VOLUME_COLUMN,
                    out calculationMethod,
                    out row);
        }

        public static bool TryParsePeriodVolumeRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingInfo,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!BuildingInfoCellParser.TryParsePeriodVolume(
                    source.GetCellText($"{PERIOD_VOLUME_COLUMN}{rowNumber}"),
                    out decimal? periodVolume,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        PERIOD_VOLUME_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingInfo = new CalculationBuildingInfos()
                {
                    RowType = (byte)BuildingInfoRowType.PeriodVolume,
                    PeriodVolume = periodVolume.Value,
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

        public static bool TryParseRestRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingInfo,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!BuildingInfoCellParser.TryParseRest(
                    source.GetCellText($"{REST_COLUMN}{rowNumber}"),
                    out decimal? rest,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        REST_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingInfo = new CalculationBuildingInfos()
                {
                    RowType = (byte)BuildingInfoRowType.Rest,
                    Rest = rest.Value,
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

        public static bool IsDebtCellEmpty(
            ExcelSheet source,
            int rowNumber)
        {
            return
                CommonRowParser.IsCellEmpty(
                    source,
                    rowNumber,
                    DEBT_COLUMN);
        }

        public static bool IsDebtHeaderCellPresent(
            ExcelSheet source,
            int rowNumber)
        {
            return
                string.Equals(
                    source.GetCellText($"{DEBT_HEADER_COLUMN}{rowNumber}"),
                    DEBT_HEADER,
                    StringComparison.OrdinalIgnoreCase);
        }
    }
}
