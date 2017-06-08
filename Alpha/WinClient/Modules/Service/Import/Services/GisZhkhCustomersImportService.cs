using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class GisZhkhCustomersImportService : IImportService
    {
        private const int FIRST_ROW_INDEX = 3;
        private const int SHEET_NAME = 1;

        private class Columns
        {
            public const int ACCOUNT = 1;
            public const int GIS_ZHKH_ID = 3;
        }

        private class ParsedRow
        {
            public int RowNumber { get; set; }
            public string Account { get; set; }
            public string GisZhkhID { get; set; }
        }

        private IExcelService _excelService;

        public GisZhkhCustomersImportService(IExcelService excelService)
        {
            _excelService = excelService;
        }

        #region Implementation of IGisZhkhDataImportService

        public string ProcessFile(string inputFileName, Action<int> reportProgressAction)
        {
            string _resultMessage;

            List<ParsedRow> _rows = ParseFile(inputFileName, reportProgressAction, out _resultMessage);

            if(_rows != null && _rows.Count > 0)
            {
                Process(_rows, reportProgressAction, out _resultMessage);
            }

            return _resultMessage;
        }

        #endregion

        #region Help Methods

        private List<ParsedRow> ParseFile(string fileName, Action<int> reportProgressAction, out string resultMessage)
        {
            int _currentRow = FIRST_ROW_INDEX - 1;
            List<ParsedRow> _rows = null;

            try
            {
                using (IExcelWorkbook _xwb = _excelService.OpenWorkbook(fileName))
                {
                    IExcelWorksheet _xws = _xwb.Worksheet(SHEET_NAME);
                    int _rowCount = _xws.GetRowCount();
                    _rows = new List<ParsedRow>(_rowCount);

                    while (_currentRow < _rowCount)
                    {
                        _rows.Add(ParseRow(++_currentRow, _xws));
                        reportProgressAction(_currentRow * 50 / _rowCount);
                    }
                }
                resultMessage = string.Empty;
            }
            catch (Exception _ex)
            {
                resultMessage = $"Не удалось прочитать строку {0}.\r\n\r\n{_ex.Message}";
                Logger.SimpleWrite($"Не удалось прочитать строку {0}.\r\n\r\n{_ex}");
            }

            return _rows;
        }

        private ParsedRow ParseRow(int row, IExcelWorksheet sheet)
        {
            return new ParsedRow
            {
                Account = sheet.Cell(row, Columns.ACCOUNT).Value,
                GisZhkhID = sheet.Cell(row, Columns.GIS_ZHKH_ID).Value
            };
        }

        private void Process(List<ParsedRow> rows, Action<int> reportProgressAction, out string resultMessage)
        {
            resultMessage = string.Empty;
            StringBuilder _errors = new StringBuilder();
            int _count = 1;
            int _processedCount = 0;

            List<string> _accounts = rows.Select(r => r.Account).ToList();
            Dictionary<string, int> _customers;
            using (Entities _db = new Entities())
            {
                _customers = _db.Customers
                    .Where(c => _accounts.Contains(c.Account))
                    .Select(c => 
                        new
                        {
                            c.Account,
                            c.ID
                        })
                    .ToDictionary(c => c.Account, c => c.ID);
            }

            foreach (ParsedRow _row in rows)
            {
                using (Entities _db = new Entities())
                {
                    try
                    {
                        if(_customers.ContainsKey(_row.Account))
                        {
                            Customers _customer = new Customers { ID = _customers[_row.Account] };
                            _db.Customers.Attach(_customer);

                            _customer.GisZhkhID = _row.GisZhkhID;
                            _db.SaveChanges();
                            _processedCount++;
                        }
                        else
                        {
                            _errors.AppendLine($"Строка {_row.RowNumber}: Не найден абонент с номером л/с {_row.Account}");
                        }
                    }
                    catch (Exception _ex)
                    {
                        _errors.AppendLine($"Строка {_row.RowNumber}: Ошибка");
                        Logger.SimpleWrite($"Произошла ошибка при импорте из ГИС ЖКХ. Строка {_row.RowNumber}: {_ex}");
                    }
                }

                reportProgressAction(_count++ * 50 / rows.Count + 50);
            }

            string _processedMessage = $"\r\n\r\nОбработано {_processedCount} из {rows.Count}\r\n\r\n";

            resultMessage = _errors.Length > 0
                ? $"Импорт завершен с ошибками. {_processedMessage} Подробности:\r\n\r\n{_errors}"
                : $"Импорт успешно завершен. {_processedMessage}";
        }

        #endregion
    }
}
