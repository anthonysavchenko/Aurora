using System;
//using System.Drawing;
using System.Reflection;

namespace Taumis.Alpha.Infrastructure.Library.Services.Excel
{
    /// <summary>
    /// Класс для работы с Excel-файлами
    /// </summary>
    public class Excel2007Worker
    {
        const string ASSEMBLY2007 = "Microsoft.Office.Interop.Excel, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c";

        /// <summary>
        /// Сборка
        /// </summary>
        Assembly ExcelAssembly;

        public enum ExcelHorizontalAlignment
        {
            xlHAlignCenter = -4108,
            xlHAlignLeft = -4131,
            xlHAlignRight = -4152,
            xlHAlignJustify = -4130
        }

        public enum ExcelVerticalAlignment
        {
            xlBottom = -4107,
            xlCenter = -4108,
            xlTop = -4160
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Excel2007Worker()
        {
            try
            {
                ExcelAssembly = Assembly.Load(ASSEMBLY2007);
            }
            catch
            {
                throw new Exception("На компьютере не установлен MS Office Excel 2007.");
            }
        }

        /// <summary>
        /// Класс закладки
        /// </summary>
        public class ExcelSheet
        {
            /// <summary>
            /// Сборка
            /// </summary>
            Assembly ExcelAssembly;

            /// <summary>
            /// Закладка
            /// </summary>
            object _sheet;

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="_app">Экземпляр приложения Excel</param>
            /// <param name="_index">Название вкладки</param>
            /// <param name="_ExcelAssembly">Сборка</param>
            public ExcelSheet(object _app, string _index, Assembly _ExcelAssembly)
            {
                ExcelAssembly = _ExcelAssembly;

                object[] _indexes = { _index };

                object Worksheets = _app.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, _app, null);

                _sheet = Worksheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, Worksheets, _indexes);
            }

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="_app">Экземпляр приложения Excel</param>
            /// <param name="_index">Номер вкладки</param>
            /// <param name="_ExcelAssembly">Сборка</param>
            public ExcelSheet(object _app, int _index, Assembly _ExcelAssembly)
            {
                ExcelAssembly = _ExcelAssembly;

                object[] _indexes = { _index };

                object Worksheets = _app.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, _app, null);

                _sheet = Worksheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, Worksheets, _indexes);
            }

            /// <summary>
            /// Возвращает значение ячейки по адресу
            /// </summary>
            /// <param name="_cell">Адрес ячейки (напр. "B23")</param>
            /// <returns>Значение ячейки</returns>
            public string GetCellValue(string _cell)
            {
                return Convert.ToString(GetCell(_cell));
            }

            /// <summary>
            /// Возвращает объект содержимого ячейки
            /// </summary>
            /// <param name="_cell">Адрес ячейки (напр. "B23")</param>
            /// <returns>Объект содержимого ячейки</returns>
            public object GetCell(string _cell)
            {
                object[] _params =
                {
                    _cell, Type.Missing
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);

                return _range.GetType().InvokeMember("Value2", BindingFlags.GetProperty, null, _range, null);
            }

            /// <summary>
            /// Устанавливает шрифт для ячейки
            /// </summary>
            /// <param name="_cell">Адрес ячейки (напр. "B23")</param>
            /// <param name="font">Шрифт</param>
            /*public void SetCellFont(string _cell, Font font)
            {
                object[] _params =
                {
                    _cell, Type.Missing
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);

                object _font = _range.GetType().InvokeMember("Font", BindingFlags.GetProperty, null, _range, null);
                _font.GetType().InvokeMember("Name", BindingFlags.SetProperty, null, _font, new object[] { font.Name });
                _font.GetType().InvokeMember("Size", BindingFlags.SetProperty, null, _font, new object[] { font.Size });
                if (font.Bold)
                {
                    _font.GetType().InvokeMember("Bold", BindingFlags.SetProperty, null, _font, new object[] { font.Bold });
                }
                if (font.Italic)
                {
                    _font.GetType().InvokeMember("Italic", BindingFlags.SetProperty, null, _font, new object[] { font.Italic });
                }
            }*/

            /// <summary>
            /// Устанавливает горизонтальное и вертикальное выравнивание для ячейки
            /// </summary>
            /// <param name="_cell">Адрес ячейки (напр. "B23")</param>
            /// <param name="_cell">Адрес ячейки (напр. "B23")</param>
            public void SetCellAlign(string _cell, ExcelHorizontalAlignment horizontalAlignment, ExcelVerticalAlignment verticalAlignment, Boolean wrapText)
            {
                object[] _params =
                {
                    _cell, Type.Missing
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);
                _range.GetType().InvokeMember("HorizontalAlignment", BindingFlags.SetProperty, null, _range, new object[] { horizontalAlignment });
                _range.GetType().InvokeMember("VerticalAlignment", BindingFlags.SetProperty, null, _range, new object[] { verticalAlignment });
                _range.GetType().InvokeMember("WrapText", BindingFlags.SetProperty, null, _range, new object[] { wrapText });

            }

            /// <summary>
            /// Возвращает текст ячейки по адресу
            /// </summary>
            /// <param name="_cell">Адрес ячейки (напр. "B23")</param>
            /// <returns>Текст ячейки</returns>
            public string GetCellText(string _cell)
            {
                object[] _params =
                {
                    _cell, Type.Missing
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);

                return _range.GetType().InvokeMember("Text", BindingFlags.GetProperty, null, _range, null).ToString();
            }

            /// <summary>
            /// Устанавливает значение ячейки по адресу
            /// </summary>
            /// <param name="_cell">Адрес ячейки (напр. "B23")</param>
            /// <param name="_value">Значение ячейки</param>
            public void SetCellValue(string _cell, object _value)
            {
                object[] _params =
                {
                    _cell, Type.Missing
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);

                _range.GetType().InvokeMember("Value2", BindingFlags.SetProperty, null, _range, new object[] { _value });
            }

            /// <summary>
            /// Устанавливает формат ячеек
            /// </summary>
            /// <param name="_cellStart">Адрес начала диапазона (напр. "B23")</param>
            /// <param name="_cellEnd">Адрес конца диапазона (напр. "D26")</param>
            /// <param name="_format">Формат ячейки</param>
            public void SetRangeFormat(string _cellStart, string _cellEnd, string _format)
            {
                object[] _params =
                {
                    _cellStart, _cellEnd
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);

                _range.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, null, _range, new object[] { _format });
            }

            /// <summary>
            /// Удаляет строку
            /// </summary>
            /// <param name="_rowNumber">Номер строки</param>
            public bool DeleteRow(int _rowNumber)
            {
                object[] _params =
                {
                    _rowNumber, Type.Missing
                };

                object _rows = _sheet.GetType().InvokeMember("Rows", BindingFlags.GetProperty, null, _sheet, null);

                Type _RANGE_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.Range");

                object _row = _RANGE_TYPE.GetMethod("get_Item").Invoke(_rows, _params);
                return (bool)_RANGE_TYPE.GetMethod("Delete").Invoke(_row, new object[] { Type.Missing });
            }

            /// <summary>
            /// Устанавливает ширину столбцов
            /// </summary>
            /// <param name="_cellStart">Адрес начала диапазона (напр. "B23")</param>
            /// <param name="_cellEnd">Адрес конца диапазона (напр. "D26")</param>
            /// <param name="_width">Ширина</param>
            public void SetColumnWidth(string _colStart, string _colEnd, double _width)
            {
                object[] _params =
                {
                    String.Format("{0}1", _colStart), String.IsNullOrEmpty(_colEnd) ? Type.Missing : String.Format("{0}1", _colEnd)
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);
                _range.GetType().InvokeMember("EntireColumn", BindingFlags.GetProperty, null, _range, null)
                      .GetType().InvokeMember("ColumnWidth", BindingFlags.SetProperty, null, _range, new object[] { _width });
            }

            /// <summary>
            /// Устанавливает высоту ячеек
            /// </summary>
            /// <param name="_rowStart">Адрес начала диапазона (напр. "B23")</param>
            /// <param name="_rowEnd">Адрес конца диапазона (напр. "D26")</param>
            /// <param name="_height">Высота</param>
            public void SetRowHeight(string _rowStart, string _rowEnd, double _height)
            {
                object[] _params =
                {
                    String.Format("A{0}", _rowStart), String.IsNullOrEmpty(_rowEnd) ? Type.Missing : String.Format("A{0}", _rowEnd)
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);
                _range.GetType().InvokeMember("EntireRow", BindingFlags.GetProperty, null, _range, null)
                      .GetType().InvokeMember("RowHeight", BindingFlags.SetProperty, null, _range, new object[] { _height });
            }

            /// <summary>
            /// Объединяет ячейки
            /// </summary>
            /// <param name="_cellStart">Адрес начала диапазона (напр. "B23")</param>
            /// <param name="_cellEnd">Адрес конца диапазона (напр. "D26")</param>
            public void MergeCells(string _cellStart, string _cellEnd)
            {
                object[] _params =
                {
                    _cellStart, _cellEnd
                };

                Type _WORKSHEET_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Worksheet");

                object _range = _WORKSHEET_TYPE.GetMethod("get_Range").Invoke(_sheet, _params);

                Type _RANGE_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.Range");
                _RANGE_TYPE.GetMethod("Merge").Invoke(_range, new object[] { Type.Missing });
            }

            /// <summary>
            /// Количество использованых строк закладки
            /// </summary>
            public int RowsCount
            {
                get
                {
                    object _UsedRange = _sheet.GetType().InvokeMember("UsedRange", BindingFlags.GetProperty, null, _sheet, null);

                    object _Rows = _UsedRange.GetType().InvokeMember("Rows", BindingFlags.GetProperty, null, _UsedRange, null);

                    return (int)_Rows.GetType().InvokeMember("Count", BindingFlags.GetProperty, null, _Rows, null);
                }
            }
        }

        /// <summary>
        /// Экземпляр приложения
        /// </summary>
        protected Object _app;

        /// <summary>
        /// Открывает файл, создавая экземпляр приложения EXcel 
        /// (После окончания работы с файлом необходимо прибивать экземпляр приложения посредством вызова Close())
        /// </summary>
        /// <param name="_fileName">Абсолютный путь к файлу</param>
        public void OpenFile(string _fileName)
        {
            Type APPLICATION_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.ApplicationClass");
            Type WORKBOOKS_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.Workbooks");

            _app = Activator.CreateInstance(APPLICATION_TYPE);

            object _Workbooks = APPLICATION_TYPE.GetProperty("Workbooks").GetValue(_app, null);
            object[] _params =
            {
                _fileName,  Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
            };

            WORKBOOKS_TYPE.GetMethod("Open").Invoke(_Workbooks, _params);
        }

        /// <summary>
        /// Создает файл и экземпляр приложения Excel 
        /// </summary>
        /// <param name="_fileName">Абсолютный путь к файлу</param>
        public ExcelSheet CreateFile(string _fileName, string _sheetName = "Лист1")
        {
            Type APPLICATION_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.ApplicationClass");
            Type WORKBOOKS_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.Workbooks");

            _app = Activator.CreateInstance(APPLICATION_TYPE);

            object _Workbooks = APPLICATION_TYPE.GetProperty("Workbooks").GetValue(_app, null);
            object _workBook = WORKBOOKS_TYPE.GetMethod("Add").Invoke(_Workbooks, new object[] { -4167 });

            object _sheet = _workBook.GetType().InvokeMember("ActiveSheet", BindingFlags.GetProperty, null, _workBook, null);
            _sheet.GetType().InvokeMember("Name", BindingFlags.SetProperty, null, _sheet, new object[] { _sheetName });

            object[] _saveParams =
            {
                _fileName,  -4143, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 2, Type.Missing, Type.Missing, Type.Missing, Type.Missing
            };

            _app.GetType().InvokeMember("DisplayAlerts", BindingFlags.SetProperty, null, _app, new object[] { false });
            _workBook.GetType().GetMethod("SaveAs").Invoke(_workBook, _saveParams);

            return new ExcelSheet(_app, _sheetName, ExcelAssembly);
        }

        /// <summary>
        /// Возвращает закладку 
        /// </summary>
        /// <param name="_index">Название закладки</param>
        /// <returns>Закладка</returns>
        public ExcelSheet GetSheet(string _index)
        {
            return new ExcelSheet(_app, _index, ExcelAssembly);
        }

        /// <summary>
        /// Возвращает закладку 
        /// </summary>
        /// <param name="_index">Номер закладки</param>
        /// <returns>Закладка</returns>
        public ExcelSheet GetSheet(int _index)
        {
            return new ExcelSheet(_app, _index, ExcelAssembly);
        }

        /// <summary>
        /// Добавляет закладку 
        /// </summary>
        /// <param name="_index">Название закладки</param>
        /// <returns>Закладка</returns>
        public ExcelSheet AddSheet(string _sheetName)
        {
            Type APPLICATION_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.ApplicationClass");
            Type SHEETS_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.Sheets");

            object _activeWorkbook = APPLICATION_TYPE.GetProperty("ActiveWorkbook").GetValue(_app, null);
            object _activeSheet = _activeWorkbook.GetType().InvokeMember("ActiveSheet", BindingFlags.GetProperty, null, _activeWorkbook, null);

            object _Worksheets = _activeWorkbook.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, _activeWorkbook, null);
            object _sheet = SHEETS_TYPE.GetMethod("Add").Invoke(_Worksheets, new object[] { Type.Missing, _activeSheet, Type.Missing, Type.Missing });
            _sheet.GetType().InvokeMember("Name", BindingFlags.SetProperty, null, _sheet, new object[] { _sheetName });

            return new ExcelSheet(_app, _sheetName, ExcelAssembly);
        }

        /// <summary>
        /// Сохраняет изменения в рабочей книге
        /// </summary>
        public bool Save()
        {
            bool _res = false;

            if (null != _app)
            {
                Type APPLICATION_TYPE = ExcelAssembly.GetType("Microsoft.Office.Interop.Excel.ApplicationClass");
                object _Workbook = APPLICATION_TYPE.GetProperty("ActiveWorkbook").GetValue(_app, null);

                bool _readOnly = (bool)_Workbook.GetType().InvokeMember("ReadOnly", BindingFlags.GetProperty, null, _Workbook, null);

                if (!_readOnly)
                {
                    ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Workbook").GetMethod("Save").Invoke(_Workbook, null);
                    _res = true;
                }
                else
                {
                    object[] _params =
                    {
                        false,  Type.Missing, Type.Missing
                    };
                    ExcelAssembly.GetType("Microsoft.Office.Interop.Excel._Workbook").GetMethod("Close").Invoke(_Workbook, _params);
                }
            }

            return _res;
        }

        /// <summary>
        /// Закрывает ранее созданный экземпляр приложения Excel
        /// </summary>
        public void Close()
        {
            if (null != _app)
            {
                _app.GetType().GetMethod("Quit").Invoke(_app, null);
                _app = null;
            }
        }
    }
}
