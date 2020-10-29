using Taumis.Alpha.DataBase;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
{
    public static class CommonRowParser
    {
        const string FIRST_COLUMN = "A";

        const string TOTAL_TEXT = "ИТОГО";

        public delegate bool TryParseRowMethod(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row);

        public static bool IsFirstCellEmpty(
            ExcelSheet source,
            int rowNumber)
        {
            return
                string.IsNullOrEmpty(
                    source.GetCellText($"{FIRST_COLUMN}{rowNumber}"));
        }

        public static bool IsFirstCellTotal(
            ExcelSheet source,
            int rowNumber)
        {
            return
                source.GetCellText($"{FIRST_COLUMN}{rowNumber}") == TOTAL_TEXT;
        }
    }
}
