using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser
    .RowParsers.BuildingInfoRowParsers
{
    public static class Till012021BuildingInfoRowParser
    {
        const string DEBT_COLUMN = "J";
        const string VOLUME_COLUMN = "J";
        const string COLLECTIVE_VOLUME_COLUMN = "E";
        const string NOT_DISTRIBUTED_VOLUME_COLUMN = "I";
        const string DEBT_HEADER_COLUMN = "D";

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

        public static bool TryParseCollectiveVolumeRow(
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
                if (!BuildingInfoCellParser.TryParseCollectiveVolume(
                    source.GetCellText($"{COLLECTIVE_VOLUME_COLUMN}{rowNumber}"),
                    out decimal? collectiveVolume,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        COLLECTIVE_VOLUME_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!BuildingInfoCellParser.TryParseNotDistributedVolume(
                    source.GetCellText($"{NOT_DISTRIBUTED_VOLUME_COLUMN}{rowNumber}"),
                    out decimal? notDistributedVolume,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        NOT_DISTRIBUTED_VOLUME_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingInfo = new CalculationBuildingInfos()
                {
                    RowType = (byte)BuildingInfoRowType.CollectiveVolume,
                    CollectiveVolume = collectiveVolume.Value,
                    NotDistributedVolume = notDistributedVolume.Value,
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
                CommonRowParser.IsCellEmpty(
                    source,
                    rowNumber,
                    DEBT_HEADER_COLUMN);
        }
    }
}
