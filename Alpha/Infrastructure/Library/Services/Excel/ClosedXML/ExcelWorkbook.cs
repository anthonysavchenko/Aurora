using System;
using ClosedXML.Excel;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;

namespace Taumis.Alpha.Infrastructure.Library.Services.Excel.ClosedXML
{
    public class ExcelWorkbook : IExcelWorkbook
    {
        private XLWorkbook _wb;

        public ExcelWorkbook()
        {
            _wb = new XLWorkbook();
        }

        public ExcelWorkbook(string file)
        {
            _wb = new XLWorkbook(file);
        }

        public IExcelWorksheet AddWorksheet(string name)
        {
            return new ExcelWorksheet(_wb.AddWorksheet(name));
        }

        public void Dispose()
        {
            _wb.Dispose();
        }

        public void Save()
        {
            _wb.Save();
        }

        public void SaveAs(string file)
        {
            _wb.SaveAs(file);
        }

        public IExcelWorksheet Worksheet(string name)
        {
            return new ExcelWorksheet(_wb.Worksheet(name));
        }

        public IExcelWorksheet Worksheet(int position)
        {
            return new ExcelWorksheet(_wb.Worksheet(position));
        }
    }
}
