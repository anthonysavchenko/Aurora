using System;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace Taumis.Alpha.Infrastructure.Library.Services
{
    public class ExcelSheet : IDisposable
    {
        public enum FileFormat
        {
            Xls,
            Xlsx,
            Dbf
        }

        private const string ExcelAssemblyFileNotFoundMessage =
            "На компьютере не установлен Microsoft Office Excel 2007 или один из его компонентов - \"Поддержка программирования .NET\"";

        private const string ExcelSheetNotFoundMessage =
            "В файле не найден лист \"{0}\"";

        private Excel.Application _excelApp;
        private readonly Excel.Workbook _workbook;
        private readonly Excel.Worksheet _worksheet;

        public int RowsCount { get; private set; }

        public ExcelSheet(string fileName, string sheetName = "")
        {
            _excelApp = new Excel.Application();
            if (_excelApp == null)
            {
                throw new ApplicationException(ExcelAssemblyFileNotFoundMessage);
            }

            _excelApp.Visible = false;
            _workbook = _excelApp.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            try
            {
                _worksheet = string.IsNullOrEmpty(sheetName)
                    ? _workbook.Sheets[1]
                    : _workbook.Sheets[sheetName];
            }
            catch (Exception _ex)
            {
                throw new ApplicationException(string.Format(ExcelSheetNotFoundMessage, sheetName), _ex);
            }

            RowsCount = _worksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
        }

        public string GetCell(string column, int row)
        {
            object _value = _worksheet.Range[string.Format("{0}{1}", column, row), Type.Missing].Cells.Value;
            return _value == null ? string.Empty : _value.ToString();
        }

        public object[,] GetRange(string columnFrom, int rowFrom, string columnTo, int rowTo)
        {
            Array _range = _worksheet.Range[$"{columnFrom}{rowFrom}:{columnTo}{rowTo}"].Value;
            object[,] _result = new object[_range.GetLength(0), _range.GetLength(1)];
            Array.Copy(_range, 1, _result, 0, _range.Length);
            return _result;
        }

        public void SetCell(string column, int row, string value)
        {
            _worksheet.Range[string.Format("{0}{1}", column, row), Type.Missing].Value = value;
        }

        public void SetRange(string columnFrom, int rowFrom, string columnTo, int rowTo, string value)
        {
            _worksheet.Range[string.Format("{0}{1}:{2}{3}", columnFrom, rowFrom, columnTo, rowTo), Type.Missing].Value = value;
        }

        public void SetRange(string sheetName, string columnFrom, int rowFrom, string columnTo, int rowTo, object value)
        {
            Excel.Worksheet _sheet = (Excel.Worksheet)_workbook.Sheets[sheetName];
            _sheet.Range[$"{columnFrom}{rowFrom}:{columnTo}{rowTo}"].Value = value;
        }

        public void SaveAs(string fileName, FileFormat fileFormat = FileFormat.Xlsx)
        {
            Excel.XlFileFormat _xlFileFormat;
            switch (fileFormat)
            {
                case FileFormat.Xls:
                    _xlFileFormat = Excel.XlFileFormat.xlExcel12;
                    break;
                case FileFormat.Xlsx:
                    _xlFileFormat = Excel.XlFileFormat.xlOpenXMLWorkbook;
                    break;
                //case FileFormat.Dbf:
                default:
                    _xlFileFormat = Excel.XlFileFormat.xlDBF4;
                    break;
            }

            _workbook.SaveAs(
                fileName,
                _xlFileFormat,
                Missing.Value,
                Missing.Value,
                false,
                false,
                Excel.XlSaveAsAccessMode.xlNoChange,
                Excel.XlSaveConflictResolution.xlUserResolution,
                false,
                Missing.Value,
                Missing.Value,
                Missing.Value);
        }

        public void Save()
        {
            _workbook.Save();
        }

        public void Dispose()
        {
            if (_excelApp != null)
            {
                _workbook?.Close(false);

                _excelApp.Quit();
                _excelApp = null;
            }
        }
    }
}