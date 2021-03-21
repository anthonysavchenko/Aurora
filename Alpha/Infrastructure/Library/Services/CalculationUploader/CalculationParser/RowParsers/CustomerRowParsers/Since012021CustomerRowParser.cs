using Taumis.Alpha.DataBase;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .CustomerRowParsers
{
    public static class Since012021CustomerRowParser
    {
        const string SQUARE_COLUMN = "O";
        const string RESIDENTS_COLUMN = "P";

        public static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row)
        {
            return
                CommonCustomerRowParser.TryParseRow(
                    source,
                    buildingAddressRow,
                    rowNumber,
                    SQUARE_COLUMN,
                    RESIDENTS_COLUMN,
                    out row);
        }
    }
}
