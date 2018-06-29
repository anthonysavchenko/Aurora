using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class ParseDebtFileCommandHandler : ICommandHandler<ParseDebtFileCommand>
    {
        private readonly IExcelService _excelService;

        public ParseDebtFileCommandHandler(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public void Execute(ParseDebtFileCommand command)
        {
            command.Result = new Dictionary<int, decimal>();

            using(IExcelWorkbook _wb = _excelService.OpenWorkbook(command.DebtFile))
            {
                IExcelWorksheet _ws = _wb.Worksheet(1);
                int _rowCount = _ws.GetRowCount();

                using (Entities _db = new Entities())
                {
                    for (int _row = 1; _row <= _rowCount; _row++)
                    {
                        try
                        {
                            string _account = _ws.Cell(_row, 1).Value.Trim();

                            if (!string.IsNullOrEmpty(_account))
                            {
                                if (!_ws.Cell(_row, 2).TryGetValue(out decimal _debt))
                                {
                                    throw new ApplicationException($"Не удалось преобразовать значение {_ws.Cell(_row, 2).Value} к типу decimal");
                                }

                                var _customer =
                                    _db.Customers
                                        .Where(c => c.Account == _account)
                                        .Select(
                                            c =>
                                            new
                                            {
                                                c.ID,
                                                BuildingID = c.Buildings.ID
                                            })
                                        .FirstOrDefault();

                                if (_customer != null)
                                {
                                    if (command.Result.ContainsKey(_customer.ID))
                                    {
                                        command.Result[_customer.ID] += _debt;
                                    }
                                    else
                                    {
                                        command.Result.Add(_customer.ID, _debt);
                                    }
                                }
                                else
                                {
                                    throw new ApplicationException($"Не найден лицевой счет {_account}");
                                }
                            }
                        }
                        catch (Exception _ex)
                        {
                            Logger.SimpleWrite($"ParseDebtFileCommandHandler. Исключение при разборе строки {_row} : {_ex}");
                            // _failCount++;
                        }
                    }
                }
            }
        }
    }
}
