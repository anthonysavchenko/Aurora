using Taumis.Alpha.Infrastructure.Library.Services.CommonParsers;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser
{
    public static class CellParser
    {
        const int COUNTER_NUMBER_DB_LENGTH = 25;

        public static bool TryParseAddress(
            string source,
            out string street,
            out string building,
            out string errorDescription)
        {
            string cellDataName = "адрес дома";

            return
                CommonCellParser.TryParseBuildingAddress(
                    source,
                    cellDataName,
                    out street,
                    out building,
                    out errorDescription);
        }

        public static bool TryParseCounterNumber(
            string source,
            out string counterNumber,
            out string errorDescription)
        {
            counterNumber = null;
            string cellDataName = "номер счетчика";
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

        public static bool TryParseCurrentValue(
            string source,
            out decimal? currentValue,
            out string errorDescription)
        {
            string cellDataName = "текущие показания";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

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
            string cellDataName = "предыдущие показания";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

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

        public static bool TryParseCoefficient(
            string source,
            out byte? coefficient,
            out string errorDescription)
        {
            coefficient = null;
            string cellDataName = "коэффициент";
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
    }
}
