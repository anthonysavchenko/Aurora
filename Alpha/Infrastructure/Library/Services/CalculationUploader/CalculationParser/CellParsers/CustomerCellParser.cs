using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CommonParsers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers
{
    public static class CustomerCellParser
    {
        const int ACCOUNT_DB_LENGTH = 25;
        const int APARTMENT_DB_LENGTH = 10;
        const int OWNER_DB_LENGTH = 50;

        const string HAS_COUNTER_TEXT = "+";
        const string COMMON_COUNTER_TYPE_TEXT = "суточная";
        const string DAY_COUNTER_TYPE_TEXT = "2-х зонн. день";
        const string NIGHT_COUNTER_TYPE_TEXT = "2-х зонн. ночь";

        public static bool TryParseAccount(
            string source,
            out string account,
            out string errorDescription)
        {
            account = null;
            string cellDataName = "номер лицевого счета";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

            if (!CommonCellParser.HasValue(
                sourceNoCR,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            account = sourceNoCR;

            if (!CommonCellParser.HasAppropriateLength(
                account,
                ACCOUNT_DB_LENGTH,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseApartment(
            string source,
            out string apartment,
            out string errorDescription)
        {
            string cellDataName = "номер квартиры";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);
            apartment = !string.IsNullOrEmpty(sourceNoCR) ? sourceNoCR : string.Empty;

            if (!CommonCellParser.HasAppropriateLength(
                apartment,
                APARTMENT_DB_LENGTH,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseOwner(
            string source,
            out string owner,
            out string errorDescription)
        {
            string cellDataName = "ФИО собственника";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);
            owner = sourceNoCR;

            if (!CommonCellParser.HasAppropriateLength(
                owner,
                OWNER_DB_LENGTH,
                cellDataName,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool ParseHasCounter(string source)
        {
            return CommonCellParser.ReplaceCaretReturn(source) == HAS_COUNTER_TEXT;
        }

        public static bool TryParseCounterType(
            string source,
            bool hasCounter,
            out CalculationCounterType counterType,
            out string errorDescription)
        {
            counterType = CalculationCounterType.Unknown;
            errorDescription = null;

            if (!hasCounter)
            {
                counterType = CalculationCounterType.Norm;
            }
            else
            {
                string cellDataName = "тип ИПУ";
                string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

                if (!CommonCellParser.HasValue(
                    sourceNoCR,
                    cellDataName,
                    out errorDescription))
                {
                    return false;
                }

                if (sourceNoCR == COMMON_COUNTER_TYPE_TEXT)
                {
                    counterType = CalculationCounterType.Common;
                }
                else if (sourceNoCR == DAY_COUNTER_TYPE_TEXT)
                {
                    counterType = CalculationCounterType.Day;
                }
                else if (sourceNoCR == NIGHT_COUNTER_TYPE_TEXT)
                {
                    counterType = CalculationCounterType.Night;
                }
                else
                {
                    errorDescription =
                        CommonCellParser.GetEnumParsingErrorDescription(
                            source,
                            cellDataName,
                            $"\"{COMMON_COUNTER_TYPE_TEXT}\", \"{DAY_COUNTER_TYPE_TEXT}\" или " +
                                $"\"{NIGHT_COUNTER_TYPE_TEXT}\"");
                    return false;
                }
            }

            return true;
        }

        public static bool TryParseVolume(
            string source,
            out decimal? volume,
            out string errorDescription)
        {
            volume = null;
            string cellDataName = "расход";
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
                out volume,
                out errorDescription,
                negativeAllowed: false))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseRecalculation(
            string source,
            out decimal? recalculation,
            out string errorDescription)
        {
            recalculation = null;
            string cellDataName = "перерасчет";
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
                out recalculation,
                out errorDescription))
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
            string cellDataName = "площадь";
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

            if (!CommonCellParser.TryParseDecimal(
                source,
                sourceNoCR,
                cellDataName,
                out square,
                out errorDescription,
                negativeAllowed: false,
                zeroAllowed: false))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseResidents(
            string source,
            out byte? residents,
            out string errorDescription)
        {
            residents = null;
            string cellDataName = "количество проживающих";
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
                out residents,
                out errorDescription))
            {
                return false;
            }

            return true;
        }
    }
}
