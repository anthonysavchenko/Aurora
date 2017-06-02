using Taumis.Alpha.Infrastructure.Interface.Services.Excel;

namespace Taumis.Alpha.Infrastructure.Library.Services.Excel.ClosedXML
{
    public class ExcelService : IExcelService
    {
        public IExcelWorkbook CreateWorkbook()
        {
            return new ExcelWorkbook();
        }

        public IExcelWorkbook OpenWorkbook(string file)
        {
            return new ExcelWorkbook(file);
        }
    }
}
