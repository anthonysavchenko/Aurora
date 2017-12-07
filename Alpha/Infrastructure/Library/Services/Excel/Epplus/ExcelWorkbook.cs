using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using OfficeOpenXml;
using System.IO;

namespace Taumis.Alpha.Infrastructure.Library.Services.Excel.Epplus
{
    public class ExcelWorkbook : IExcelWorkbook
    {
        private ExcelPackage _ep;

        public ExcelWorkbook()
        {
            _ep = new ExcelPackage();
        }

        public ExcelWorkbook(string file)
        {
            FileInfo _fi = new FileInfo(file);
            _ep = new ExcelPackage(_fi);
        }

        public IExcelWorksheet AddWorksheet(string name)
        {
            return new ExcelWorksheet(_ep.Workbook.Worksheets.Add(name));
        }

        public void Dispose()
        {
            _ep.Dispose();
        }

        public void Save()
        {
            _ep.Save();
        }

        public void SaveAs(string file)
        {
            FileInfo _fi = new FileInfo(file);
            _ep.SaveAs(_fi);
        }

        public IExcelWorksheet Worksheet(string name)
        {
            return new ExcelWorksheet(_ep.Workbook.Worksheets[name]);
        }

        public IExcelWorksheet Worksheet(int position)
        {
            return new ExcelWorksheet(_ep.Workbook.Worksheets[position]);
        }
    }
}