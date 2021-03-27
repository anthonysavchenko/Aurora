using System;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CommonParsers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers
{
    public static class BuildingInfoCellParser
    {
        const string AVARAGE_TEXT = "по среднему";
        const string NORM_TEXT = "по нормативу";

        public static bool TryParseAddress(
            string source,
            out string street,
            out string building,
            out string errorDescription)
        {
            return
                CommonCellParser.TryParseBuildingAddress(
                    source,
                    out street,
                    out building,
                    out errorDescription);
        }

        public static bool TryParseDebt(
            string source,
            out decimal? debt,
            out string errorDescription)
        {
            debt = null;
            string cellDataName = "задолженность по дому";
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
                out debt,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static CalculationMethod ParseCalculationMethod(string source)
        {
            string sourceNoCR = CommonCellParser.ReplaceCaretReturn(source);

            if (string.IsNullOrEmpty(sourceNoCR)
                || sourceNoCR.Equals(AVARAGE_TEXT, StringComparison.OrdinalIgnoreCase))
            {
                return CalculationMethod.Avarage;
            }
            else if (sourceNoCR.Equals(NORM_TEXT, StringComparison.OrdinalIgnoreCase))
            {
                return CalculationMethod.Norm;
            }

            return CalculationMethod.BuildingCounters;
        }

        public static bool TryParseVolume(
            string source,
            out decimal? volume,
            out string errorDescription)
        {
            volume = null;
            string cellDataName = "расход по дому";
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

        public static bool TryParseNorm(
            string source,
            CalculationMethod calculationMethod,
            out decimal? norm,
            out string errorDescription)
        {
            norm = null;
            errorDescription = null;
            string cellDataName = "норматив ОДН по дому";

            if (calculationMethod == CalculationMethod.Norm)
            {
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
                    out norm,
                    out errorDescription,
                    negativeAllowed: false,
                    zeroAllowed: false))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryParseCollectiveVolume(
            string source,
            out decimal? collectiveVolume,
            out string errorDescription)
        {
            collectiveVolume = null;
            string cellDataName = "ОДН по дому";
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
                out collectiveVolume,
                out errorDescription,
                negativeAllowed: false))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseNotDistributedVolume(
            string source,
            out decimal? collectiveSquare,
            out string errorDescription)
        {
            collectiveSquare = null;
            string cellDataName = "нераспределенная величина по дому";
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
                out collectiveSquare,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseCollectiveSquare(
            string source,
            CalculationMethod calculationMethod,
            out decimal? collectiveSquare,
            out string errorDescription)
        {
            collectiveSquare = null;
            errorDescription = null;
            string cellDataName = "площадь МОП по дому";

            if (calculationMethod == CalculationMethod.Norm)
            {
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
                    out collectiveSquare,
                    out errorDescription,
                    negativeAllowed: false,
                    zeroAllowed: false))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryParsePeriodVolume(
            string source,
            out decimal? periodVolume,
            out string errorDescription)
        {
            periodVolume = null;
            string cellDataName = "ОДН по дому за период";
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
                out periodVolume,
                out errorDescription))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseRest(
            string source,
            out decimal? rest,
            out string errorDescription)
        {
            rest = null;
            string cellDataName = "переходящий остаток";
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
                out rest,
                out errorDescription))
            {
                return false;
            }

            return true;
        }
    }
}
