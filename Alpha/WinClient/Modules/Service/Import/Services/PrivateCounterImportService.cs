using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class PrivateCounterImportService : IImportService
    {
        private static class Columns
        {
            public const int STREET = 1;
            public const int BUILDING = 2;
            public const int APARTMENT = 3;
            public const int COUNTER_MODEL = 4;
            public const int COUNTER_NUMBER = 5;
            public const int FIRST_COLLECT_DATE = 7;
            public const int FIRST_VALUE = 8;
        }

        private class ParsedRow
        {
            public int RowNumber { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public string Apartment { get; set; }
            public string CounterModel { get; set; }
            public string CounterNumber { get; set; }
            public DateTime FirstCollectDate { get; set; }
            public decimal FirstValue { get; set; }
        }

        private IExcelService _excelService;

        public PrivateCounterImportService(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public string ProcessFile(string inputFileName, Action<int> reportProgressAction)
        {
            List<ParsedRow> _parsedRows = ParseFile(inputFileName, reportProgressAction, out string message);

            if (string.IsNullOrEmpty(message) && _parsedRows.Count > 0)
            {
                message = Save(_parsedRows, reportProgressAction);
            }

            return message;
        }

        private List<ParsedRow> ParseFile(string fileName, Action<int> reportProgressAction, out string message)
        {
            int _currentRow = 1;
            List<ParsedRow> _rows = null;

            try
            {
                using (IExcelWorkbook _xwb = _excelService.OpenWorkbook(fileName))
                {
                    IExcelWorksheet _xws = _xwb.Worksheet(1);
                    int _rowCount = _xws.GetRowCount();
                    _rows = new List<ParsedRow>(_rowCount);

                    while (_currentRow < _rowCount)
                    {
                        _rows.Add(ParseRow(++_currentRow, _xws));
                        reportProgressAction(_currentRow * 50 / _rowCount);
                    }
                }

                message = string.Empty;
            }
            catch (Exception _ex)
            {
                message = $"Не удалось прочитать строку {_currentRow}.\r\n\r\n{_ex.Message}";
            }

            return _rows;
        }

        private ParsedRow ParseRow(int row, IExcelWorksheet sheet)
        {
            sheet.Cell(row, Columns.FIRST_COLLECT_DATE).TryGetValue(out DateTime collectDate);

            return
                new ParsedRow
                {
                    RowNumber = row,
                    Street = sheet.Cell(row, Columns.STREET).Value,
                    Building = sheet.Cell(row, Columns.BUILDING).Value,
                    Apartment = sheet.Cell(row, Columns.APARTMENT).Value,
                    CounterModel = sheet.Cell(row, Columns.COUNTER_MODEL).Value,
                    CounterNumber = sheet.Cell(row, Columns.COUNTER_NUMBER).Value,
                    FirstCollectDate = collectDate,
                    FirstValue = Convert.ToDecimal(sheet.Cell(row, Columns.FIRST_VALUE).Value)
                };
        }

        private string Save(List<ParsedRow> rows, Action<int> reportProgressAction)
        {
            int _progress = 1;
            var _errors = new StringBuilder();
            object _locker = new object();

            Parallel.ForEach(rows, row =>
            {
                try
                {
                    using (var _db = new Entities())
                    {
                        Customers _customer = _db.Customers
                            .Where(x =>
                                x.Buildings.Streets.Name == row.Street
                                && x.Buildings.Number == row.Building
                                && x.Apartment == row.Apartment)
                            .FirstOrDefault();

                        if (_customer == null)
                        {
                            throw new ApplicationException($"Абонент не найден.");
                        }

                        PrivateCounters _counter = _db.PrivateCounters
                            .FirstOrDefault(x => x.Number == row.CounterNumber && x.CustomerID == _customer.ID);

                        if (_counter == null)
                        {
                            _counter =
                                new PrivateCounters
                                {
                                    Number = row.CounterNumber,
                                    Model = row.CounterModel,
                                    Customers = _customer,
                                    ServiceID = 58
                                };
                            _db.PrivateCounters.AddObject(_counter);
                        }

                        bool _valueExist = _db.PrivateCounterValues
                            .Any(x => x.CollectDate == row.FirstCollectDate && x.PrivateCounters.ID == _counter.ID);

                        if (!_valueExist)
                        {
                            _db.PrivateCounterValues.AddObject(
                                new PrivateCounterValues
                                {
                                    PrivateCounters = _counter,
                                    CollectDate = row.FirstCollectDate,
                                    Period = new DateTime(row.FirstCollectDate.Year, row.FirstCollectDate.Month, 1),
                                    Value = row.FirstValue
                                });
                        }

                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    lock (_locker)
                    {
                        _errors.AppendLine($"Строка №{row.RowNumber}. {ex.Message}");
                    }
                }

                lock (_locker)
                {
                    reportProgressAction(_progress++ * 50 / rows.Count + 50);
                }
            });

            return _errors.Length > 0 ? _errors.ToString() : "Импорт данных выполнен успешно";
        }
    }
}
