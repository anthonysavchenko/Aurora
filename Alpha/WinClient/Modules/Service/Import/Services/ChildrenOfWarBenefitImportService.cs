using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Queries;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using AccountData = Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Queries.GetCustomersByAddressQuery.AccountData;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class ChildrenOfWarBenefitImportService : IChildrenOfWarBenefitImportService
    {
        private class Columns
        {
            public const int APARTMENT = 7;
            public const int SURNAME = 9;
            public const int NAME = 10;
            public const int PATRONOMIC = 11;
            public const int SHARE = 14;
        }

        private class ParsedRow
        {
            public int RowNumber { get; set; }
            public string Apartment { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Patronymic { get; set; }
            public decimal Share { get; set; }
        }

        private IExcelService _excelService;
        private readonly IServerTimeService _serverTimeService;

        public ChildrenOfWarBenefitImportService(IExcelService excelService, IServerTimeService serverTimeService)
        {
            _excelService = excelService;
            _serverTimeService = serverTimeService;
        }

        public string ProcessFile(string inputFileName, Action<int> reportProgressAction, int buildingId)
        {
            string _resultMessage;

            List<ParsedRow> _rows = ParseFile(inputFileName, reportProgressAction, out _resultMessage);

            if (_rows != null && _rows.Count > 0)
            {
                Process(_rows, reportProgressAction, buildingId, out _resultMessage);
            }

            return _resultMessage;
        }

        private List<ParsedRow> ParseFile(string fileName, Action<int> reportProgressAction, out string message)
        {
            int _currentRow = 1;
            List<ParsedRow> _rows = null;
            var _dt = new DataTable();

            try
            {
                using (IExcelWorkbook _xwb = _excelService.OpenWorkbook(fileName))
                {
                    IExcelWorksheet _xws = _xwb.Worksheet(1);
                    int _rowCount = _xws.GetRowCount();
                    _rows = new List<ParsedRow>(_rowCount);

                    while (_currentRow < _rowCount)
                    {
                        _rows.Add(
                            new ParsedRow
                            {
                                RowNumber = ++_currentRow,
                                Apartment = _xws.Cell(_currentRow, Columns.APARTMENT).Value.Trim(),
                                Surname = _xws.Cell(_currentRow, Columns.SURNAME).Value.Trim(),
                                Name = _xws.Cell(_currentRow, Columns.NAME).Value.Trim(),
                                Patronymic = _xws.Cell(_currentRow, Columns.PATRONOMIC).Value.Trim(),
                                Share =Convert.ToDecimal(_dt.Compute(_xws.Cell(_currentRow, Columns.SHARE).Value.Trim(), ""))
                            });

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

        private void Process(List<ParsedRow> rows, Action<int> reportProgressAction, int buildingId, out string resultMessage)
        {
            resultMessage = string.Empty;
            var _errors = new StringBuilder();

            int _count = 1;
            int _processedCount = 0;

            DateTime _curPeriod = _serverTimeService.GetPeriodInfo().FirstUncharged;

            foreach (var _row in rows)
            {
                try
                {
                    using (var _db = new Entities())
                    {
                        List<AccountData> _customers = _db.GetCustomersByAddress(buildingId, _row.Apartment, _curPeriod.AddMonths(-1));

                        if (_customers.Count > 0)
                        {
                            if (_customers.Count > 1)
                            {
                                ActualizeAccounts(_customers, _row, _curPeriod, _db);
                            }
                            else
                            {
                                SplitAccount(_customers[0], _row, _curPeriod, _db);
                            }

                            _db.SaveChanges();
                        }
                        else
                        {
                            resultMessage += $"{_row.RowNumber}: л/с по указанному адресу не найден (кв. {_row.Apartment})";
                        }
                    }

                    _processedCount++;
                }
                catch (Exception ex)
                {
                    _errors.AppendLine($"Строка {_row.RowNumber}: { ex.Message }");
                }

                reportProgressAction(_count++ * 50 / rows.Count + 50);
            }

            string _processedMessage = $"\r\n\r\nОбработано строк {_processedCount} из {rows.Count}\r\n\r\n";

            resultMessage = _errors.Length > 0
                ? $"Импорт завершен с ошибками.{_processedMessage}Не удалось обработать строки:\r\n{_errors}"
                : $"Импорт успешно завершен. {_processedMessage}";
        }

        private Customers CloneCustomer(AccountData account, ParsedRow row, DateTime curPeriod, Entities db)
        {
            var _newCustomer =
                new Customers
                {
                    Account = db.GetNewAccountNum(),
                    Apartment = account.Customer.Apartment,
                    BillSendingSubscription = account.Customer.BillSendingSubscription,
                    Buildings = account.Building,
                    Comment = account.Customer.Comment,
                    DebtsRepayment = account.Customer.DebtsRepayment,
                    Entrance = account.Customer.Entrance,
                    Floor = account.Customer.Floor,
                    GisZhkhID = account.Customer.GisZhkhID,
                    IsPrivate = account.Customer.IsPrivate,
                    JuridicalPersonFullName = account.Customer.JuridicalPersonFullName,
                    LiftPresence = account.Customer.LiftPresence,
                    OwnerType = account.Customer.OwnerType,
                    PhysicalPersonFullName = $"{row.Surname} {row.Name} {row.Patronymic}",
                    PhysicalPersonShortName = $"{row.Surname}{GetShort(row.Name)}{GetShort(row.Patronymic)}",
                    RoomsCount = account.Customer.RoomsCount,
                    RubbishChutePresence = account.Customer.RubbishChutePresence,
                    Square = account.Customer.Square
                };

            db.Customers.AddObject(_newCustomer);

            List<CustomerPoses> _poses = db.CustomerPoses
                .Include(x => x.Contractors)
                .Include(x => x.Services)
                .Where(x => 
                    x.Customers.ID == account.Customer.ID
                    && x.Till >= curPeriod)
                .ToList();

            DateTime _since = curPeriod;
            DateTime _till = curPeriod;

            foreach (var _pos in _poses)
            {
                var _newPos =
                    new CustomerPoses
                    {
                        Contractors = _pos.Contractors,
                        Customers = _newCustomer,
                        Rate = _pos.Rate,
                        Services = _pos.Services,
                        Since = _since,
                        Till = _till
                    };
                db.CustomerPoses.AddObject(_newPos);
            }

            CreateBenefitResident(_newCustomer, row, db);

            return _newCustomer;

            string GetShort(string fullName) =>
                !string.IsNullOrEmpty(fullName) ? $" {fullName.Substring(0, 1)}." : string.Empty;
        }

        private void CreateBenefitResident(Customers customer, ParsedRow row, Entities db)
        {
            db.Residents.AddObject(
                new Residents
                {
                    Surname = row.Surname,
                    FirstName = row.Name,
                    Patronymic = row.Patronymic,
                    BenefitTypes = db.BenefitTypes.First(x => x.Code == BenefitTypeCodes.CHILDREN_OF_WAR),
                    Customers = customer
                });
        }

        private void SplitAccount(AccountData account, ParsedRow row, DateTime firstUnchargedPeriod, Entities db)
        {
            if (row.Share < 1)
            {
                var _newCustomer = CloneCustomer(account, row, firstUnchargedPeriod, db);
                _newCustomer.Square = Math.Round(account.Customer.Square * row.Share, 2, MidpointRounding.AwayFromZero);
                account.Customer.Square -= _newCustomer.Square;

                var _benefitResident = account.Residents.FirstOrDefault(x => x.BenefitTypeCode == BenefitTypeCodes.CHILDREN_OF_WAR);
                if (_benefitResident != null)
                {
                    db.Residents.DeleteObject(_benefitResident.Resident);
                }
            }
            else
            {
                var _resident = account.Residents.FirstOrDefault(x =>
                    x.Resident.Surname.Trim() == row.Surname
                    && x.Resident.FirstName.Trim() == row.Name
                    && x.Resident.Patronymic.Trim() == row.Patronymic);

                if (_resident == null)
                {
                    CreateBenefitResident(account.Customer, row, db);
                }
                else if (_resident.BenefitTypeCode != BenefitTypeCodes.CHILDREN_OF_WAR)
                {
                    _resident.Resident.BenefitTypes = db.BenefitTypes.First(x => x.Code == BenefitTypeCodes.CHILDREN_OF_WAR);
                }
            }
        }

        private void ActualizeAccounts(List<AccountData> accountsToActualize, ParsedRow row, DateTime firstUnchargedPeriod, Entities db)
        {
            if (accountsToActualize.All(x => x.Residents.All(y => y.BenefitTypeCode != BenefitTypeCodes.CHILDREN_OF_WAR)))
            {
                throw new ApplicationException("По указанному адресу найдено несколько лицевых счетов!");
            }
            else
            {
                var _parentAccount =
                    accountsToActualize.Single(x => x.Residents.All(y => y.BenefitTypeCode != BenefitTypeCodes.CHILDREN_OF_WAR));

                decimal _fullSquare = accountsToActualize.Sum(x => x.Customer.Square);

                decimal _shareSquare = Math.Round(_fullSquare * row.Share, 2, MidpointRounding.AwayFromZero);

                var _benefitAccount = accountsToActualize
                    .SingleOrDefault(x => 
                        x.Residents.Any(y => y.BenefitTypeCode == BenefitTypeCodes.CHILDREN_OF_WAR
                            && y.Resident.Surname == row.Surname
                            && y.Resident.FirstName == row.Name
                            && y.Resident.Patronymic == row.Patronymic));

                if (_benefitAccount == null)
                {
                    var _newCustomer = CloneCustomer(_parentAccount, row, firstUnchargedPeriod, db);
                    _newCustomer.Square = _shareSquare;
                    _parentAccount.Customer.Square -= _newCustomer.Square;
                }
                else
                {
                    if (row.Share < 1)
                    {
                        if (_benefitAccount.Customer.Square != _shareSquare)
                        {
                            _parentAccount.Customer.Square += _benefitAccount.Customer.Square - _shareSquare;
                            _benefitAccount.Customer.Square = _shareSquare;
                        }

                        DateTime _lastCharged = firstUnchargedPeriod.AddMonths(-1);

                        db.CustomerPoses
                            .Where(x => x.Customers.ID == _parentAccount.Customer.ID && x.Till == _lastCharged)
                            .ToList()
                            .ForEach(x => x.Till = firstUnchargedPeriod);
                    }
                    else
                    {
                        _parentAccount.Customer.Square = _fullSquare;
                        CreateBenefitResident(_parentAccount.Customer, row, db);

                        var _benefitAccounts = accountsToActualize
                            .Where(x => x.Residents.Any(y => y.BenefitTypeCode == BenefitTypeCodes.CHILDREN_OF_WAR))
                            .ToList();

                        foreach (var _account in _benefitAccounts)
                        {
                            var _poses = db.CustomerPoses.Where(y => y.Customers.ID == _account.Customer.ID).ToList();

                            if (_poses.All(x => x.Since == firstUnchargedPeriod && x.Till == firstUnchargedPeriod))
                            {
                                _poses.ForEach(x => db.CustomerPoses.DeleteObject(x));
                                _account.Residents.ForEach(x => db.Residents.DeleteObject(x.Resident));
                                db.Customers.DeleteObject(_account.Customer);
                            }
                            else
                            {
                                _poses.Where(x => x.Since == firstUnchargedPeriod && x.Till == firstUnchargedPeriod)
                                    .ToList()
                                    .ForEach(x => db.CustomerPoses.DeleteObject(x));
                            }
                        }
                    }
                }
            }
        }
    }
}
