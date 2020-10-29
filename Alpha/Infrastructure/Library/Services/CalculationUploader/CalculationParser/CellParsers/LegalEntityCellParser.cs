using Taumis.Alpha.Infrastructure.Library.Services.CommonParsers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers
{
    public static class LegalEntityCellParser
    {
        const int CONTRACT_DB_LENGTH = 25;
        const int ENTITY_NAME_DB_LENGTH = 200;

        public static bool TryParseContract(
            string source,
            out string contract,
            out string errorDescription)
        {
            contract = null;
            string cellDataName = "номер договора юр. лица";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

            if (!CommonCellParser.HasValue(
                sourceNoCR,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            contract = sourceNoCR;

            if (!CommonCellParser.HasAppropriateLength(
                contract,
                CONTRACT_DB_LENGTH,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseEntityName(
            string source,
            out string entityName,
            out string errorDescription)
        {
            string cellDataName = "название юр. лица";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);
            entityName = sourceNoCR;

            if (!CommonCellParser.HasAppropriateLength(
                entityName,
                ENTITY_NAME_DB_LENGTH,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseChargedVolume(
            string source,
            out decimal? chargedVolume,
            out string errorDescription)
        {
            chargedVolume = null;
            string cellDataName = "расход юр. лица к оплате";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

            if (!CommonCellParser.HasValue(
                sourceNoCR,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            if (!CommonCellParser.TryParseDecimal(
                source,
                sourceNoCR,
                cellDataName,
                out chargedVolume,
                out errorDescription,
                negativeAllowed: false))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseSquare(
            string source,
            out decimal? square,
            out string errorDescription)
        {
            string cellDataName = "площадь юр. лица";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

            if (!CommonCellParser.TryParseDecimal(
                source,
                sourceNoCR,
                cellDataName,
                out square,
                out errorDescription,
                negativeAllowed: false))
            {
                return false;
            }

            return true;
        }
    }
}
