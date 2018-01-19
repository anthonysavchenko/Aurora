using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;

namespace Taumis.Alpha.Infrastructure.Library.Services.Excel.Epplus
{
    public class ExcelWorksheet : IExcelWorksheet
    {
        private OfficeOpenXml.ExcelWorksheet _ws;

        public ExcelWorksheet(OfficeOpenXml.ExcelWorksheet ws)
        {
            _ws = ws;
        }

        public void AdjustColumnsToContents()
        {
            _ws.Cells.AutoFitColumns();
        }

        public IExcelCell Cell(int row, int column)
        {
            return new ExcelCell(_ws.Cells[row, column]);
        }

        public IExcelCell Cell(int row, string column)
        {
            return new ExcelCell(_ws.Cells[$"{column}{row}"]);
        }

        public void ClearDataValidations()
        {
            _ws.DataValidations.Clear();
        }

        public int GetLastUsedColumnNumber()
        {
            return _ws.Dimension.End.Column;
        }

        public int GetRowCount()
        {
            return _ws.Dimension.End.Row;
        }
    }
}
