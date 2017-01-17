using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public class BenefitDataExportService : IBenefitDataExportService
    {
        #region File Columns

        /// <summary>
        /// Номер счета
        /// </summary>
        private const string LS = "A";

        /// <summary>
        /// Площадь
        /// </summary>
        private const string PL = "I";

        /// <summary>
        /// Количество проживающих
        /// </summary>
        private const string KOLP = "J";

        /// <summary>
        /// Норматив
        /// </summary>
        private const string NORM = "K";

        /// <summary>
        /// Фактическое потребление ЖКУ за месяц
        /// </summary>
        private const string FAKTP = "L";

        /// <summary>
        /// Фактическое потребление ЖКУ с учетом перерасчетов за прошлый период
        /// </summary>
        private const string FAKTPER = "M";

        /// <summary>
        /// Тариф
        /// </summary>
        private const string TARIF = "N";

        /// <summary>
        /// Количество месяцев долга
        /// </summary>
        private const string MESD = "S";

        /// <summary>
        /// Флаг соглашения о реструктуризации долга
        /// </summary>
        private const string RESTRDOLG = "T";

        private const string PERIOD_COLUMN = "O";

        #endregion

        private class CustomerInfo
        {
            public int ID { get; set; }
            public decimal Square { get; set; }
            public int ResidentCount { get; set; }
            public bool DebtsRepayment { get; set; }
            public int DebtMonthCount { get; set; }
            public decimal Charge { get; set; }
            public decimal Recharge { get; set; }
            public decimal Benefit { get; set; }
            public decimal Rate { get; set; }
        }

        #region Help Methods

        private string CorrectAccount(string badAccountStr)
        {
            string _goodAccountStr;
            if (!string.IsNullOrEmpty(badAccountStr) && badAccountStr.Length >= 8)
            {
                string _normalizedStr = badAccountStr.Trim().Replace("-", string.Empty);

                string _prefix = _normalizedStr.Substring(0, 2).ToUpper();
                if (_prefix == "EG")
                {
                    _normalizedStr = _normalizedStr.Substring(2);
                }

                if (_normalizedStr.Length == 8)
                {
                    string _accPart1 = _normalizedStr.Substring(0, 4);
                    string _accPart2 = _normalizedStr.Substring(4, 3);
                    string _accPart3 = _normalizedStr.Substring(7, 1);

                    _goodAccountStr = $"EG-{_accPart1}-{_accPart2}-{_accPart3}";
                }
                else
                {
                    _goodAccountStr = string.Empty;
                }
            }
            else
            {
                _goodAccountStr = badAccountStr;
            }
            return _goodAccountStr;
        }

        private DateTime GetPeriod(string periodStr)
        {
            if (string.IsNullOrEmpty(periodStr))
            {
                throw new ApplicationException("В файле нет данных о периоде отчета");
            }

            DateTime _period;
            if (!DateTime.TryParse(periodStr, out _period))
            {
                throw new ApplicationException(string.Format("Не удалось распознать период отчета: {0}", periodStr));
            }

            return _period;
        }

        private List<CustomerInfo> GetCustomerInfoList(DateTime period, List<int> customerIds)
        {
            List<CustomerInfo> _result;

            using (Entities _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                #region Query

                var _customers =
                    _db.Customers
                        .Select(c =>
                            new
                            {
                                c.ID,
                                c.Square,
                                ResidentCount = c.Residents.Count,
                                c.DebtsRepayment
                            })
                        .ToList();

                DateTime _startPeriod = new DateTime(2015, 7, 1);

                var _debtsRaw =
                    _db.ChargeOperPoses
                        .Select(o =>
                            new
                            {
                                CustomerID = o.ChargeOpers.Customers.ID,
                                o.ChargeOpers.ChargeSets.Period,
                                o.Value,
                            })
                        .Concat(_db.ChargeOperPoses
                            .Where(o => o.ChargeOpers.ChargeCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.ChargeOpers.Customers.ID,
                                    o.ChargeOpers.ChargeCorrectionOpers.Period,
                                    Value = -1 * o.Value,
                                }))
                        .Concat(_db.RechargeOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RechargeOpers.Customers.ID,
                                    o.RechargeOpers.RechargeSets.Period,
                                    o.Value,
                                }))
                        .Concat(_db.RechargeOperPoses
                            .Where(o => o.RechargeOpers.ChildChargeCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RechargeOpers.Customers.ID,
                                    o.RechargeOpers.RechargeSets.Period,
                                    Value = -1 * o.Value,
                                }))
                        .Concat(_db.BenefitOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.BenefitOpers.ChargeOpers.Customers.ID,
                                    o.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                    o.Value,
                                }))
                        .Concat(_db.BenefitOperPoses
                            .Where(o => o.BenefitOpers.BenefitCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.BenefitOpers.ChargeOpers.Customers.ID,
                                    o.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                    Value = -1 * o.Value,
                                }))
                        .Concat(_db.RebenefitOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RebenefitOpers.RechargeOpers.Customers.ID,
                                    o.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                    o.Value,
                                }))
                        .Concat(_db.RebenefitOperPoses
                            .Where(o => o.RebenefitOpers.BenefitCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RebenefitOpers.RechargeOpers.Customers.ID,
                                    o.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                    Value = -1 * o.Value,
                                }))
                        .Concat(_db.PaymentOperPoses
                            .Where(o => o.PaymentOpers.PaymentPeriod <= period)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.PaymentOpers.Customers.ID,
                                    o.Period,
                                    o.Value,
                                }))
                        .Concat(_db.PaymentCorrectionOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                                    o.PaymentCorrectionOpers.Period,
                                    o.Value,
                                }))
                        .Concat(_db.OverpaymentOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.OverpaymentOpers.Customers.ID,
                                    o.Period,
                                    o.Value,
                                }))
                        .Concat(_db.OverpaymentCorrectionOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.OverpaymentCorrectionOpers.ChargeOpers.Customers.ID,
                                    o.OverpaymentCorrectionOpers.Period,
                                    o.Value,
                                }))
                        .Where(o => _startPeriod <= o.Period && o.Period <= period)
                        .GroupBy(o => o.CustomerID)
                        .Select(g =>
                            new
                            {
                                CustomerID = g.Key,
                                Value = g.Sum(o => o.Value)
                            })
                        .Where(o => o.Value > 0)
                        .ToList();

                List<int> _customersWithDebts = _debtsRaw
                    .Select(d => d.CustomerID)
                    .ToList();

                var _charges =
                    _db.ChargeOperPoses
                        .Where(p => 
                            p.ChargeOpers.ChargeSets.Period == period && 
                            _customersWithDebts.Contains(p.ChargeOpers.Customers.ID))
                        .GroupBy(p => p.ChargeOpers.Customers.ID)
                        .Select(g =>
                            new
                            {
                                CustomerID = g.Key,
                                Value = g.Sum(p => p.Value)
                            })
                        .ToDictionary(
                            r => r.CustomerID,
                            r => r.Value);

                var _debts = _debtsRaw
                        .Select(g =>
                            new
                            {
                                g.CustomerID,
                                MonthCount = CalculateMonthCount(g.CustomerID, g.Value, _charges)
                            })
                        .ToDictionary(x => x.CustomerID, x => x.MonthCount);

                var _rates =
                    _db.CustomerPoses
                        .Where(p => p.Since <= period && period <= p.Till)
                        .GroupBy(c => c.Customers.ID)
                        .Select(g =>
                            new
                            {
                                CustomerID = g.Key,
                                Rate = g.Sum(c => c.Rate)
                            })
                        .ToList();

                _result =
                    _db.ChargeOperPoses
                        .Select(o =>
                            new
                            {
                                CustomerID = o.ChargeOpers.Customers.ID,
                                o.ChargeOpers.ChargeSets.Period,
                                Charge = o.Value,
                                Recharge = (decimal)0,
                                Benefit = (decimal)0,
                            })
                        .Concat(_db.ChargeOperPoses
                            .Where(o => o.ChargeOpers.ChargeCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.ChargeOpers.Customers.ID,
                                    o.ChargeOpers.ChargeCorrectionOpers.Period,
                                    Charge = (decimal)0,
                                    Recharge = -1 * o.Value,
                                    Benefit = (decimal)0,
                                }))
                        .Concat(_db.RechargeOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RechargeOpers.Customers.ID,
                                    o.RechargeOpers.RechargeSets.Period,
                                    Charge = (decimal)0,
                                    Recharge = o.Value,
                                    Benefit = (decimal)0,
                                }))
                        .Concat(_db.RechargeOperPoses
                            .Where(o => o.RechargeOpers.ChildChargeCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RechargeOpers.Customers.ID,
                                    o.RechargeOpers.RechargeSets.Period,
                                    Charge = (decimal)0,
                                    Recharge = -1 * o.Value,
                                    Benefit = (decimal)0,
                                }))
                        .Concat(_db.BenefitOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.BenefitOpers.ChargeOpers.Customers.ID,
                                    o.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                    Charge = (decimal)0,
                                    Recharge = (decimal)0,
                                    Benefit = o.Value,
                                }))
                        .Concat(_db.BenefitOperPoses
                            .Where(o => o.BenefitOpers.BenefitCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.BenefitOpers.ChargeOpers.Customers.ID,
                                    o.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                    Charge = (decimal)0,
                                    Recharge = (decimal)0,
                                    Benefit = -1 * o.Value,
                                }))
                        .Concat(_db.RebenefitOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RebenefitOpers.RechargeOpers.Customers.ID,
                                    o.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                    Charge = (decimal)0,
                                    Recharge = (decimal)0,
                                    Benefit = o.Value,
                                }))
                        .Concat(_db.RebenefitOperPoses
                            .Where(o => o.RebenefitOpers.BenefitCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RebenefitOpers.RechargeOpers.Customers.ID,
                                    o.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                    Charge = (decimal)0,
                                    Recharge = (decimal)0,
                                    Benefit = -1 * o.Value,
                                }))
                        .Where(o => o.Period == period)
                        .GroupBy(o => o.CustomerID)
                        .Select(g =>
                            new
                            {
                                CustomerID = g.Key,
                                Charge = g.Sum(o => o.Charge),
                                Recharge = g.Sum(o => o.Recharge),
                                Benefit = g.Sum(o => o.Benefit)
                            })
                        .ToList()
                        .Where(c => customerIds.Contains(c.CustomerID))
                        .Join(
                            _customers,
                            c1 => c1.CustomerID,
                            c2 => c2.ID,
                            (c1, c2) =>
                                new
                                {
                                    c2.ID,
                                    c2.Square,
                                    c2.ResidentCount,
                                    c2.DebtsRepayment,
                                    c1.Charge,
                                    c1.Recharge,
                                    c1.Benefit
                                })
                        .Join(
                            _rates,
                            c => c.ID,
                            r => r.CustomerID,
                            (c, r) =>
                                new CustomerInfo
                                {
                                    ID = c.ID,
                                    Square = c.Square,
                                    ResidentCount = c.ResidentCount,
                                    DebtsRepayment = c.DebtsRepayment,
                                    Charge = c.Charge,
                                    Recharge = c.Recharge,
                                    Benefit = c.Benefit,
                                    Rate = r.Rate
                                })
                        .ToList();

                _result.ForEach(x =>
                {
                    x.DebtMonthCount = _debts.ContainsKey(x.ID)
                        ? _debts[x.ID]
                        : 0;
                });

                #endregion
            }

            return _result;
        }

        private int CalculateMonthCount(int customerID, decimal debtValue, Dictionary<int, decimal> charges)
        {
            return
                charges.ContainsKey(customerID)
                    ? Convert.ToInt32(Math.Round(debtValue/charges[customerID], 0, MidpointRounding.AwayFromZero))
                    : 0;
        }

        private void FillAccountsDictionaries(Dictionary<int, int> accRowDict, ExcelSheet sheet)
        {
            Dictionary<string, int> _customerIdByAccount;

            using (Entities _db = new Entities())
            {
                _customerIdByAccount =
                    _db.Customers
                        .Select(c =>
                            new
                            {
                                c.ID,
                                c.Account
                            })
                        .ToDictionary(c => c.Account, c => c.ID);
            }

            for (int i = 2; i <= sheet.RowsCount; i++)
            {
                string _account = CorrectAccount(sheet.GetCell(LS, i));

                if (!string.IsNullOrEmpty(_account) && _customerIdByAccount.ContainsKey(_account))
                {
                    int _customerId = _customerIdByAccount[_account];

                    if (!accRowDict.ContainsKey(_customerId))
                    {
                        accRowDict.Add(_customerId, i);
                    }
                }
            }
        }

        private void FillSheet(
            List<CustomerInfo> data,
            Dictionary<int, int> accRowDict,
            ExcelSheet sheet)
        {
            foreach (CustomerInfo _customerInfo in data)
            {
                if (accRowDict.ContainsKey(_customerInfo.ID))
                {
                    FillRow(
                        accRowDict[_customerInfo.ID],
                        _customerInfo,
                        sheet);
                }
            }
        }

        private void FillRow(int row, CustomerInfo customer, ExcelSheet sheet)
        {
            string _squareStr = customer.Square.ToString().Replace(",", ".");
            string _residentCountStr = customer.ResidentCount.ToString();
            string _restrDebt = customer.DebtsRepayment ? "1" : "0";

            sheet.SetCell(PL, row, _squareStr);
            sheet.SetCell(NORM, row, _squareStr);
            sheet.SetCell(KOLP, row, _residentCountStr);
            sheet.SetCell(RESTRDOLG, row, _restrDebt);
            sheet.SetCell(MESD, row, customer.DebtMonthCount.ToString());

            decimal _recharge = customer.Recharge + customer.Charge;

            sheet.SetCell(FAKTP, row, customer.Charge.ToString(CultureInfo.InvariantCulture));
            sheet.SetCell(FAKTPER, row, _recharge.ToString(CultureInfo.InvariantCulture));
            sheet.SetCell(TARIF, row, customer.Rate.ToString(CultureInfo.InvariantCulture));
        }

        #endregion

        #region Implementation of IBenefitDataExportService

        public string ProcessFile(string inputFileName)
        {
            string _result;
            try
            {
                using (ExcelSheet _sheet = new ExcelSheet(inputFileName))
                {
                    string _periodStr = _sheet.GetCell(PERIOD_COLUMN, 2);
                    DateTime _period = GetPeriod(_periodStr);

                    Dictionary<int, int> _accRowDict = new Dictionary<int, int>();
                    FillAccountsDictionaries(_accRowDict, _sheet);

                    List<CustomerInfo> _data = GetCustomerInfoList(_period, _accRowDict.Keys.ToList());

                    FillSheet(_data, _accRowDict, _sheet);

                    _sheet.Save();

                    _result = "Операция выполнена успешно";
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite(string.Format("Benefit Export error: {0} {1}", _ex, _ex.InnerException != null ? _ex.InnerException.ToString() : string.Empty));
                _result = "Произошла ошибка. Операция не выполнена";
            }

            return _result;
        }

        #endregion
    }
}
