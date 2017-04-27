using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class NewCustomersImportService : IImportService
    {
        private const string CHECKED = "*";

        private static class Columns
        {
            public const int ACCOUNT = 1;
            public const int ZIP_CODE = 2;
            public const int STREET = 3;
            public const int BUILDING = 4;
            public const int ENTRANCE = 5;
            public const int FLOOR = 6;
            public const int APARTMENT = 7;
            public const int NAME = 8;
            public const int JUR_PERSON = 9;
            public const int AREA = 10;
            public const int IS_PRIVATE = 11;
            public const int ROOM_COUNT = 12;
            public const int RESIDENT_COUNT = 13;
            public const int FIAS_ID = 14;
        }

        private class ParsedRow
        {
            public int RowNumber { get; set; }
            public string Account { get; set; }
            public string ZipCode { get; set; }
            public string StreetName { get; set; }
            public string BuildingNum { get; set; }
            public byte Entrance { get; set; }
            public short Floor { get; set; }
            public string Apartment { get; set; }
            public string Name { get; set; }
            public Customer.OwnerTypes OwnerType { get; set; }
            public decimal Area { get; set; }
            public bool IsPrivate { get; set; }
            public int RoomCount { get; set; }
            public int ResidentCount { get; set; }
            public string FiasID { get; set; }
        }

        public string ProcessFile(string inputFileName, Action<int> reportProgressAction)
        {
            string _resultMessage;


            List<ParsedRow> _rows = ParseFile(inputFileName, reportProgressAction, out _resultMessage);

            if (_rows != null && _rows.Count > 0)
            {
                Process(_rows, reportProgressAction, out _resultMessage);
            }

            return _resultMessage;
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

        /// <summary>
        /// Возвращает фамилии и инициалы
        /// </summary>
        /// <param name="source">Исходная строка</param>
        /// <returns>Фамилия без изменений (если она есть в исходной строке) и инициалы (если они есть в исходной строке), разделенные пробелами, для каждого имени в строке, разделенные точкой с запятой</returns>
        private string GetLastNameAndInitial(string source)
        {
            string[] _names = source.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < _names.Length; i++)
            {
                string[] _words = _names[i].Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

                if (_words.Length > 2)
                {
                    _names[i] = string.Join(" ", new string[] { _words[0], string.Format("{0}.", _words[1][0]), string.Format("{0}.", _words[2][0]) });
                }
                else if (_words.Length > 1)
                {
                    _names[i] = string.Join(" ", new string[] { _words[0], string.Format("{0}.", _words[1][0]) });
                }
                else if (_words.Length > 0)
                {
                    _names[i] = _words[0];
                }
            }

            return string.Join(";", _names);
        }

        private ParsedRow ParseRow(IXLRow row)
        {
            decimal _area;
            byte _entrance;
            short _floor;
            int _roomCount,
                _residentCount;

            Customer.OwnerTypes _ownerType =
                row.Cell(Columns.JUR_PERSON).GetString() == CHECKED
                    ? Customer.OwnerTypes.JuridicalPerson
                    : Customer.OwnerTypes.PhysicalPerson;

            row.Cell(Columns.AREA).TryGetValue(out _area);
            row.Cell(Columns.ENTRANCE).TryGetValue(out _entrance);
            row.Cell(Columns.FLOOR).TryGetValue(out _floor);
            row.Cell(Columns.ROOM_COUNT).TryGetValue(out _roomCount);
            row.Cell(Columns.RESIDENT_COUNT).TryGetValue(out _residentCount);

            return new ParsedRow
            {
                RowNumber = row.RowNumber(),
                Account = row.Cell(Columns.ACCOUNT).GetString(),
                Apartment = row.Cell(Columns.APARTMENT).GetString(),
                Area = _area,
                ZipCode = row.Cell(Columns.ZIP_CODE).GetString(),
                StreetName = row.Cell(Columns.STREET).GetString(),
                BuildingNum = row.Cell(Columns.BUILDING).GetString(),
                FiasID = row.Cell(Columns.FIAS_ID).GetString(),
                Entrance = _entrance,
                Floor = _floor,
                RoomCount = _roomCount,
                ResidentCount = _residentCount,
                IsPrivate = row.Cell(Columns.IS_PRIVATE).GetString() == CHECKED,
                OwnerType = _ownerType,
                Name = row.Cell(Columns.NAME).GetString()
            };
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

        private void Process(List<ParsedRow> rows, Action<int> reportProgressAction, out string resultMessage)
        {
            int _addedCustomersCount = 0;
            int _progress = 1;
            List<int> _allreadyExistedAccounts = new List<int>();
            List<int> _failedRows = new List<int>();
            StringBuilder _errors = new StringBuilder();

            foreach (ParsedRow _row in rows)
            {
                try
                {
                    using (Entities _db = new Entities())
                    {
                        bool _accountExist;

                        _accountExist = _db.Customers.Any(c =>
                            c.Account == _row.Account ||
                            (_row.OwnerType == Customer.OwnerTypes.PhysicalPerson && c.Buildings.Streets.Name == _row.StreetName && c.Buildings.Number == _row.BuildingNum && c.Apartment == _row.Apartment));

                        if (!_accountExist)
                        {

                            Streets _street = _db.Streets.FirstOrDefault(s => s.Name == _row.StreetName);
                            if (_street == null)
                            {
                                _street = new Streets { Name = _row.StreetName };
                                _db.Streets.AddObject(_street);
                            }

                            Buildings _building = _db.Buildings.FirstOrDefault(b => b.Number == _row.BuildingNum && b.Streets.Name == _row.StreetName);

                            if (_building == null)
                            {
                                _building =
                                    new Buildings
                                    {
                                        Number = _row.BuildingNum,
                                        ZipCode = _row.ZipCode,
                                        Streets = _street,
                                        FiasID = _row.FiasID,
                                        Note = string.Empty
                                    };
                                _db.Buildings.AddObject(_building);
                            }

                            Customers _customer =
                                new Customers
                                {
                                    Account = _row.Account,
                                    Apartment = _row.Apartment,
                                    Buildings = _building,
                                    Entrance = _row.Entrance > 0 ? _row.Entrance : (byte)1,
                                    Floor = _row.Floor > 0 ? _row.Floor : (short)1,
                                    RoomsCount = _row.RoomCount > 0 ? _row.RoomCount : 1,
                                    IsPrivate = _row.IsPrivate,
                                    OwnerType = (int)_row.OwnerType,
                                    Square = _row.Area
                                };


                            if (_row.OwnerType == Customer.OwnerTypes.PhysicalPerson)
                            {
                                _customer.PhysicalPersonFullName = FirstLettersToUpper(_row.Name);
                                _customer.PhysicalPersonShortName = GetLastNameAndInitial(_row.Name);
                                _customer.JuridicalPersonFullName = string.Empty;
                            }
                            else
                            {
                                _customer.JuridicalPersonFullName = _row.Name;
                                _customer.PhysicalPersonFullName = string.Empty;
                                _customer.PhysicalPersonShortName = string.Empty;
                            }

                            if (_row.ResidentCount > 0)
                            {
                                for (int i = 0; i < _row.ResidentCount; i++)
                                {
                                    _db.Residents.AddObject(
                                        new Residents
                                        {
                                            FirstName = string.Empty,
                                            Patronymic = string.Empty,
                                            Surname = string.Empty,
                                            Customers = _customer
                                        });
                                }
                            }

                            _db.SaveChanges();
                            _addedCustomersCount++;
                        }
                        else
                        {
                            _allreadyExistedAccounts.Add(_row.RowNumber);
                        }
                    }
                }
                catch (Exception _ex)
                {
                    _failedRows.Add(_row.RowNumber);
                    _errors.AppendLine($"Строка {_row.RowNumber}: {_ex}");
                    Logger.SimpleWrite($"Ошибка при абонентов: {_ex}");
                }

                reportProgressAction(_progress++ * 50 / rows.Count + 50);
            }

            resultMessage = _allreadyExistedAccounts.Count > 0
                ? $"Строки с данными абонентов, уже существующих в базе данных: {string.Join(",", _allreadyExistedAccounts)}\r\n"
                : string.Empty;

            if (_failedRows.Count > 0)
            {
                resultMessage = $"{resultMessage}Не удалось импортировать данные из строк: {string.Join(",", _failedRows)}";
            }

            resultMessage = _errors.Length == 0 && _allreadyExistedAccounts.Count == 0 && _failedRows.Count == 0
                ? $"Импорт данных выполнен успешно"
                : $"Не удалось полностью обработать данные из файла\r\n\r\nДобавлено {_addedCustomersCount} из {rows.Count} абонентов\r\n\r\n{resultMessage}\r\n\r\nПодробности:\r\n{_errors}";
        }
    }
}
