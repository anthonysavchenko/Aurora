using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .LegalEntityRowParsers
{
    public static class CommonLegalEntityRowParser
    {
        const string CONTRACT_COLUMN = "A";
        const string ENTITY_NAME_COLUMN = "D";
        const string CHARGED_VOLUME_COLUMN = "M";

        public static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            string squareColumnName,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.LegalEntity,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!LegalEntityCellParser.TryParseContract(
                    source.GetCellText($"{CONTRACT_COLUMN}{rowNumber}"),
                    out string contract,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        CONTRACT_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!LegalEntityCellParser.TryParseEntityName(
                    source.GetCellText($"{ENTITY_NAME_COLUMN}{rowNumber}"),
                    out string entityName,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        ENTITY_NAME_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!LegalEntityCellParser.TryParseChargedVolume(
                    source.GetCellText($"{CHARGED_VOLUME_COLUMN}{rowNumber}"),
                    out decimal? chargedVolume,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        CHARGED_VOLUME_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!LegalEntityCellParser.TryParseSquare(
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

                row.LegalEntity = new CalculationLegalEntities()
                {
                    Contract = contract,
                    EntityName = entityName,
                    ChargedVolume = chargedVolume.Value,
                    Square = square,
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
    }
}
