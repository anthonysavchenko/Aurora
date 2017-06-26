using System;
using ClosedXML.Excel;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;

namespace Taumis.Alpha.Infrastructure.Library.Services.Excel.ClosedXML
{
    public class ExcelWorksheet : IExcelWorksheet
    {
        private IXLWorksheet _ws;

        public ExcelWorksheet(IXLWorksheet ws)
        {
            _ws = ws;
        }

        public void AdjustColumnsToContents()
        {
            _ws.Columns().AdjustToContents();
        }

        public IExcelCell Cell(int row, int column)
        {
            return new ExcelCell(_ws.Cell(row, column));
        }

        public IExcelCell Cell(int row, string column)
        {
            return new ExcelCell(_ws.Cell(row, column));
        }

        public int GetLastUsedColumnNumber()
        {
            return _ws.LastColumnUsed().ColumnNumber();
        }

        public int GetRowCount()
        {
            return _ws.LastRowUsed().RowNumber();
        }
    }
}
