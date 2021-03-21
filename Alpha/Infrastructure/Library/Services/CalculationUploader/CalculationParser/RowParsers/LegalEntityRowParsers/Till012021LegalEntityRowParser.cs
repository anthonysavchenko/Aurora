using Taumis.Alpha.DataBase;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
    .LegalEntityRowParsers
{
    public static class Till012021LegalEntityRowParser
    {
        const string SQUARE_COLUMN = "Q";

        public static bool TryParseRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row)
        {
            return
                CommonLegalEntityRowParser.TryParseRow(
                    source,
                    buildingAddressRow,
                    rowNumber,
                    SQUARE_COLUMN,
                    out row);
        }
    }
}
