using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.EnterpriseLibrary.Win.Services;

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
            public const int ACCOUNT = 9;
            public const int DAY_NIGHT = 10;
        }

        private class ParsedRow
        {
            public int RowNumber { get; set; }
            public string Account { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public string Apartment { get; set; }
            public string CounterModel { get; set; }
            public string CounterNumber { get; set; }
            public DateTime FirstCollectDate { get; set; }
            public decimal FirstValue { get; set; }
            public bool IsNightCounter { get; set; }
        }

        private IExcelService _excelService;

        public PrivateCounterImportService(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public string ProcessFile(string inputFileName, Action<int> reportProgressAction, DateTime? period)
        {
            List<ParsedRow> _parsedRows = ParseFile(inputFileName, reportProgressAction, out string message);

            if (string.IsNullOrEmpty(message) && _parsedRows.Count > 0)
            {
                message = Save(_parsedRows, period.Value, reportProgressAction);
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

            string _dayNightChar = sheet.Cell(row, Columns.DAY_NIGHT).Value;

            return
                new ParsedRow
                {
                    RowNumber = row,
                    Account = sheet.Cell(row, Columns.ACCOUNT).Value,
                    Street = sheet.Cell(row, Columns.STREET).Value,
                    Building = sheet.Cell(row, Columns.BUILDING).Value,
                    Apartment = sheet.Cell(row, Columns.APARTMENT).Value,
                    CounterModel = sheet.Cell(row, Columns.COUNTER_MODEL).Value,
                    CounterNumber = sheet.Cell(row, Columns.COUNTER_NUMBER).Value,
                    FirstCollectDate = collectDate,
                    FirstValue = Convert.ToDecimal(sheet.Cell(row, Columns.FIRST_VALUE).Value),
                    IsNightCounter = !string.IsNullOrEmpty(_dayNightChar) && _dayNightChar == "Н"
                };
        }

        private string Save(List<ParsedRow> rows, DateTime period, Action<int> reportProgressAction)
        {
            int _progress = 1;
            var _errors = new StringBuilder();
            object _locker = new object();

            var _grouped = rows
                .GroupBy(x =>
                    new
                    {
                        x.Street,
                        x.Building,
                        x.Apartment,
                        x.Account,
                        x.CounterNumber
                    })
                .Select(g =>
                    new
                    {
                        g.Key.Street,
                        g.Key.Building,
                        g.Key.Apartment,
                        g.Key.Account,
                        g.Key.CounterNumber,
                        Rows = g.Select(x => x).ToList()
                    });

            Parallel.ForEach(_grouped, item =>
            {
                try
                {
                    using (var _db = new Entities())
                    {
                        Customers _customer = string.IsNullOrEmpty(item.Account)
                            ? _db.Customers
                                .Where(x =>
                                    x.Buildings.Streets.Name == item.Street
                                    && x.Buildings.Number == item.Building
                                    && x.Apartment == item.Apartment
                                    && x.CustomerPoses.Any(y => y.Till >= period))
                                .FirstOrDefault()
                            : _db.Customers.FirstOrDefault(x => x.Account == item.Account);

                        if (_customer == null)
                        {
                            throw new ApplicationException("Абонент не найден");
                        }

                        if (item.Rows.Count > 1)
                        {
                            item.Rows[0].CounterNumber = item.Rows[0].IsNightCounter 
                                ? item.Rows[0].CounterNumber + " - Н"
                                : item.Rows[0].CounterNumber + " - Д";

                            item.Rows[1].CounterNumber = item.Rows[1].IsNightCounter
                                ? item.Rows[1].CounterNumber + " - Н"
                                : item.Rows[1].CounterNumber + " - Д";
                        }

                        int _serviceId = _db.CustomerPoses
                            .Where(x => x.Till >= period && x.Customers.ID == _customer.ID && x.Services.ServiceTypes.ID == 39)
                            .Select(x => x.Services.ID)
                            .FirstOrDefault();

                        if (_serviceId <= 0)
                        {
                            throw new ApplicationException($"У абонента {_customer.Account} нет услуг ОДН по э/э");
                        }

                        foreach (ParsedRow _row in item.Rows)
                        {
                            try
                            {
                                PrivateCounters _counter = _db.PrivateCounters
                                    .FirstOrDefault(x => x.Number == _row.CounterNumber && x.CustomerID == _customer.ID);

                                if (_counter == null)
                                {
                                    _counter =
                                        new PrivateCounters
                                        {
                                            Number = _row.CounterNumber,
                                            Model = _row.CounterModel,
                                            Customers = _customer,
                                            ServiceID = _serviceId
                                        };
                                    _db.PrivateCounters.AddObject(_counter);
                                }

                                bool _valueExist = _db.PrivateCounterValues
                                    .Any(x => x.CollectDate == _row.FirstCollectDate && x.PrivateCounters.ID == _counter.ID);

                                if (!_valueExist)
                                {
                                    _db.PrivateCounterValues.AddObject(
                                        new PrivateCounterValues
                                        {
                                            PrivateCounters = _counter,
                                            CollectDate = _row.FirstCollectDate,
                                            Period = period,
                                            Value = _row.FirstValue
                                        });
                                    _db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                lock (_locker)
                                {
                                    _errors.AppendLine($"Строка №{_row.RowNumber}. {ex.Message}");
                                    Logger.SimpleWrite($"Строка {_row.RowNumber}\t{_row.Street}\t{_row.Building}\t{_row.Apartment}\t{_row.CounterModel}\t{_row.CounterNumber}\t0\t{_row.FirstCollectDate}\t{_row.FirstValue}");
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    item.Rows.ForEach(_row =>
                    {
                        lock (_locker)
                        {
                            _errors.AppendLine($"Строка №{_row.RowNumber}. {ex.Message}");
                            Logger.SimpleWrite($"Строка {_row.RowNumber}\t{_row.Street}\t{_row.Building}\t{_row.Apartment}\t{_row.CounterModel}\t{_row.CounterNumber}\t0\t{_row.FirstCollectDate}\t{_row.FirstValue}");
                        }
                    });
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
