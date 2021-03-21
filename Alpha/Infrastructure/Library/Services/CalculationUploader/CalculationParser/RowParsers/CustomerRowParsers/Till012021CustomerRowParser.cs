using Taumis.Alpha.DataBase;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .CustomerRowParsers
{
    public static class Till012021CustomerRowParser
    {
        const string SQUARE_COLUMN = "Q";
        const string RESIDENTS_COLUMN = "R";

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
