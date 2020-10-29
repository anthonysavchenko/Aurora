using Taumis.Alpha.Infrastructure.Library.Services.CommonParsers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers
{
    public static class BuildingCounterCellParser
    {
        const int COUNTER_NUMBER_DB_LENGTH = 25;
        const int MODEL_DB_LENGTH = 50;

        public static bool TryParseCounterNumber(
            string source,
            out string counterNumber,
            out string errorDescription)
        {
            counterNumber = null;
            string cellDataName = "номер ОДПУ";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

            if (!CommonCellParser.HasValue(
                sourceNoCR,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            counterNumber = sourceNoCR;

            if (!CommonCellParser.HasAppropriateLength(
                counterNumber,
                COUNTER_NUMBER_DB_LENGTH,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseModel(
            string source,
            out string model,
            out string errorDescription)
        {
            string cellDataName = "модель ОДПУ";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);
            model = sourceNoCR;

            if (!CommonCellParser.HasAppropriateLength(
                model,
                MODEL_DB_LENGTH,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseCoefficient(
            string source,
            out byte? coefficient,
            out string errorDescription)
        {
            coefficient = null;
            string cellDataName = "коэффициент ОДПУ";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

            if (!CommonCellParser.HasValue(
                sourceNoCR,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            if (!CommonCellParser.TryParseByte(
                source,
                sourceNoCR,
                cellDataName,
                out coefficient,
                out errorDescription,
                zeroAllowed: false))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseCurrentValue(
            string source,
            out decimal? currentValue,
            out string errorDescription)
        {
            currentValue = null;
            string cellDataName = "текущие показания ОДПУ";
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
                out currentValue,
                out errorDescription,
                negativeAllowed: false))
            {
                return false;
            }

            return true;
        }

        public static bool TryParsePrevValue(
            string source,
            out decimal? prevValue,
            out string errorDescription)
        {
            prevValue = null;
            string cellDataName = "предыдущие показания ОДПУ";
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
                out prevValue,
                out errorDescription,
                negativeAllowed: false))
            {
                return false;
            }

            return true;
        }
    }
}
