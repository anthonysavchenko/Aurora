using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .BuildingInfoRowParsers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .BuildingCounterRowParsers
{
    public static class Since012021BuildingCounterRowParser
    {
        const string CURRENT_VALUE_COLUMN = "H";
        const string PREV_VALUE_COLUMN = "J";

        public static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row)
        {
            return
                CommonBuildingCounterRowParser.TryParseRow(
                    source,
                    buildingAddressRow,
                    rowNumber,
                    CURRENT_VALUE_COLUMN,
                    PREV_VALUE_COLUMN,
                    out row);
        }
    }
}
