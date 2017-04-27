using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.Services;
using ChargeRuleType = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service.ChargeRuleType;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class CustomerPosesImportService : IImportService
    {
        private static class Columns
        {
            public const int Street = 1;
            public const int Building = 2;
            public const int ServiceType = 3;
            public const int Service = 4;
            public const int Contractor = 5;
            public const int Rate = 6;
            public const int ChargeRule = 7;
            public const int Since = 8;
            public const int Till = 9;
            public const int ExcludedAccounts = 10;
            public const int ExcludedRate = 11;
        }

        private class ParsedRow
        {
            public int RowNumber { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public string ServiceType { get; set; }
            public string Service { get; set; }
            public string Contractor { get; set; }
            public decimal Rate { get; set; }
            public ChargeRuleType ChargeRule { get; set; }
            public DateTime Since { get; set; }
            public DateTime Till { get; set; }
            public string[] ExcludedAccounts { get; set; }
            public decimal ExcludedRate { get; set; }
        }
        
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

        private void Process(List<ParsedRow> rows, Action<int> reportProgressAction, out string resultMessage)
        {
            resultMessage = string.Empty;
            StringBuilder _errors = new StringBuilder();
            int _count = 1;
            int _processedCount = 0;

            foreach (ParsedRow _row in rows)
            {
                try
                {
                    using (Entities _db = new Entities())
                    {
                        DataBase.Services _service = _db.Services.FirstOrDefault(service => service.Name == _row.Service);
                        ServiceTypes _serviceType = _db.ServiceTypes.FirstOrDefault(serviceType => serviceType.Name == _row.ServiceType);
                        Contractors _contractor = _db.Contractors.FirstOrDefault(contractor => contractor.Name == _row.Contractor);

                        if (_serviceType == null)
                        {
                            _serviceType = new ServiceTypes
                            {
                                Name = _row.ServiceType,
                                Code = _row.ServiceType
                            };
                        }

                        if (_service == null)
                        {
                            _service = new DataBase.Services
                            {
                                Name = _row.Service,
                                Code = _row.Service,
                                ChargeRule = (byte)_row.ChargeRule,
                                ServiceTypes = _serviceType
                            };
                        }

                        if (_contractor == null)
                        {
                            _contractor = new Contractors
                            {
                                Name = _row.Contractor,
                                Code = _row.Contractor
                            };
                        }

                        DateTime _till = new DateTime(_row.Till.Year, _row.Till.Month, _row.Till.Day, 23, 59, 59);

                        var _customerIDs = _db.Customers
                            .Where(c => 
                                c.Buildings.Streets.Name == _row.Street && 
                                c.Buildings.Number == _row.Building && 
                                !_row.ExcludedAccounts.Contains(c.Account))
                            .Select(c => c.ID)
                            .ToArray();

                        AddCustomerPoses(_customerIDs, _row.Rate, _row.Since, _row.Till, _service, _contractor, _db);

                        if(_row.ExcludedRate > 0)
                        {
                            var _excludedCustomerIDs = _db.Customers
                                .Where(c => _row.ExcludedAccounts.Contains(c.Account))
                                .Select(c => c.ID)
                                .ToArray();

                            AddCustomerPoses(_excludedCustomerIDs, _row.ExcludedRate, _row.Since, _row.Till, _service, _contractor, _db);
                        }

                        _db.SaveChanges();
                    }

                    _processedCount++;
                }
                catch (Exception _ex)
                {
                    _errors.AppendLine($"Строка {_row.RowNumber}: { _ex.Message }");
                    Logger.SimpleWrite($"Ошибка при импорте услуг: {_ex}");
                }

                reportProgressAction(_count++ * 50 / rows.Count + 50);
            }

            string _processedMessage = $"\r\n\r\nОбработано строк {_processedCount} из {rows.Count}\r\n\r\n";

            resultMessage = _errors.Length > 0
                ? $"Импорт завершен с ошибками.{_processedMessage}Не удалось обработать строки:\r\n{_errors}"
                : $"Импорт успешно завершен. {_processedMessage}";
        }

        private void AddCustomerPoses(
            int[] customerIDs, 
            decimal rate,
            DateTime since,
            DateTime till,
            DataBase.Services service, 
            Contractors contractor, 
            Entities db)
        {
            foreach (int _id in customerIDs)
            {
                Customers _customer = new Customers { ID = _id };
                db.Customers.Attach(_customer);

                db.CustomerPoses.AddObject(
                    new CustomerPoses
                    {
                        Services = service,
                        Rate = rate,
                        Contractors = contractor,
                        Customers = _customer,
                        Since = since,
                        Till = till
                    });
            }
        }

        private List<ParsedRow> ParseFile(string fileName, Action<int> reportProgressAction, out string message)
        {
            int _currentRow = 1;
            List<ParsedRow> _rows = null;

            try
            {
                using (XLWorkbook _xwb = new XLWorkbook(fileName))
                {
                    IXLWorksheet _xws = _xwb.Worksheet(1);
                    int _rowCount = _xws.LastRowUsed().RowNumber();
                    _rows = new List<ParsedRow>(_rowCount);

                    while (_currentRow < _rowCount)
                    {
                        _rows.Add(ParseRow(_xws.Row(++_currentRow)));
                        reportProgressAction(_currentRow * 50 / _rowCount);
                    }
                }
                message = string.Empty;
            }
            catch (Exception _ex)
            {
                message = $"Не удалось прочитать строку {0}.\r\n\r\n{_ex.Message}";
            }

            return _rows;
        }

        /// <summary>
        /// Переводит начальные буквы во всех словах строки в верхний регистр
        /// </summary>
        /// <param name="source">Исходная строка</param>
        /// <returns>Строка с начальными буквами всех слов в верхнем регистре</returns>
        private string FirstLettersToUpper(string source)
        {
            string[] _words = source.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < _words.Length; i++)
            {
                string[] _subWords = _words[i].Split(new char[] { '-' }, StringSplitOptions.None);

                for (int j = 0; j < _subWords.Length; j++)
                {
                    _subWords[j] = string.Format("{0}{1}", Char.ToUpper(_subWords[j][0]), _subWords[j].Remove(0, 1));
                }

                _words[i] = string.Join("-", _subWords);
            }

            return string.Join(" ", _words);
        }

        private ParsedRow ParseRow(IXLRow row)
        {
            decimal _rate;
            row.Cell(Columns.Rate).TryGetValue(out _rate);

            DateTime _since;
            row.Cell(Columns.Since).TryGetValue(out _since);

            DateTime _till;
            row.Cell(Columns.Till).TryGetValue(out _till);

            byte _rule;
            row.Cell(Columns.ChargeRule).TryGetValue(out _rule);

            decimal _excludedRate;
            row.Cell(Columns.ExcludedRate).TryGetValue(out _excludedRate);

            string [] _excludedAccounts = 
                row.Cell(Columns.ExcludedAccounts).GetString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


            return new ParsedRow
            {
                RowNumber = row.RowNumber(),
                Street = row.Cell(Columns.Street).GetString(),
                Building = row.Cell(Columns.Building).GetString(),
                ServiceType = row.Cell(Columns.ServiceType).GetString(),
                Service = row.Cell(Columns.Service).GetString(),
                Contractor = row.Cell(Columns.Contractor).GetString(),
                Rate = _rate,
                Since = _since,
                Till = _till,
                ChargeRule = (ChargeRuleType)_rule,
                ExcludedAccounts = _excludedAccounts,
                ExcludedRate = _excludedRate
            };
        }
    }
}