using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .BuildingCounterRowParsers;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .BuildingInfoRowParsers;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers.CustomerRowParsers;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .LegalEntityRowParsers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.FormParsers
{
    public static class Till012021FormParser
    {
        public static bool TryParseForm(
            ExcelSheet source,
            ref int rowNumber,
            int rowsCount,
            out List<CalculationRows> rows)
        {
            return
                CommonFormParser.TryParseForm(
                    source,
                    ref rowNumber,
                    rowsCount,
                    out rows,
                    Till012021BuildingInfoRowParser.TryParseDebtRow,
                    Till012021BuildingInfoRowParser.TryParseCalculationMethodRow,
                    Till012021BuildingCounterRowParser.TryParseRow,
                    Till012021LegalEntityRowParser.TryParseRow,
                    Till012021CustomerRowParser.TryParseRow,
                    TryParseBuildingInfo,
                    Till012021BuildingInfoRowParser.IsDebtHeaderCellPresent,
                    Till012021BuildingInfoRowParser.IsDebtCellEmpty);
        }

        private static bool TryParseBuildingInfo(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            CalculationMethod calculationMethod,
            ref int rowNumber,
            int rowsCount,
            List<CalculationRows> rows)
        {
            if (!CommonFormParser.TryAddSkippedRows(
                1,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            if (!CommonFormParser.TryParseRow(
                source,
                buildingAddressRow,
                calculationMethod,
                ref rowNumber,
                rowsCount,
                rows,
                CommonBuildingInfoRowParser.TryParseNormRow))
            {
                return false;
            }

            if (!CommonFormParser.TryAddSkippedRows(
                4,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            if (!CommonFormParser.TryParseRow(
                source,
                buildingAddressRow,
                ref rowNumber,
                rowsCount,
                rows,
                Till012021BuildingInfoRowParser.TryParseCollectiveVolumeRow))
            {
                return false;
            }

            if (!CommonFormParser.TryParseRow(
                source,
                buildingAddressRow,
                calculationMethod,
                ref rowNumber,
                rowsCount,
                rows,
                CommonBuildingInfoRowParser.TryParseCollectiveSquareRow))
            {
                return false;
            }

            if (!CommonFormParser.TryAddSkippedRows(
                3,
                ref rowNumber,
                rowsCount,
                rows,
                buildingAddressRow))
            {
                return false;
            }

            return true;
        }
    }
}
