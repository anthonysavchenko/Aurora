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
        /// Тип услуги
        /// </summary>
        private const string GKU = "H";

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

        private const string REPAIR_SERVICE_TYPE_STR = "К";

        private const int REPAIR_SERVICE_TYPE_ID = 1;

        private readonly int[] _serivceTypeIDs =
            new[]
            {
                REPAIR_SERVICE_TYPE_ID
            };

        private class ServiceTypeData
        {
            public int ID { get; set; }
            public decimal Charge { get; set; }
            public decimal Recharge { get; set; }
            public decimal Benefit { get; set; }
            public decimal Rate { get; set; }
        }

        private class CustomerInfo
        {
            public int ID { get; set; }
            public decimal Square { get; set; }
            public int ResidentCount { get; set; }
            public bool DebtsRepayment { get; set; }
            public Dictionary<int, int> DebtMonthCount { get; set; }
            public List<ServiceTypeData> DataByServiceType { get; set; }
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

        private int? ParseServiceType(string serviceType)
        {
            string _firstLetter = serviceType.Trim().Substring(0, 1).ToUpper();

            switch (_firstLetter)
            {
                case REPAIR_SERVICE_TYPE_STR:
                    return REPAIR_SERVICE_TYPE_ID;
                default:
                    return null;
            }
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
                throw new ApplicationException($"Не удалось распознать период отчета: {periodStr}");
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
                                Period = o.ChargeOpers.ChargeSets.Period,
                                Value = o.Value,
                                ServiceType = o.Services.ServiceTypes.ID
                            })
                        .Concat(_db.ChargeOperPoses
                            .Where(o => o.ChargeOpers.ChargeCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.ChargeOpers.Customers.ID,
                                    Period = o.ChargeOpers.ChargeCorrectionOpers.Period,
                                    Value = -1 * o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.RechargeOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RechargeOpers.Customers.ID,
                                    Period = o.RechargeOpers.RechargeSets.Period,
                                    Value = o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.RechargeOperPoses
                            .Where(o => o.RechargeOpers.ChildChargeCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RechargeOpers.Customers.ID,
                                    Period = o.RechargeOpers.RechargeSets.Period,
                                    Value = -1 * o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.BenefitOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.BenefitOpers.ChargeOpers.Customers.ID,
                                    Period = o.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                    Value = o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.BenefitOperPoses
                            .Where(o => o.BenefitOpers.BenefitCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.BenefitOpers.ChargeOpers.Customers.ID,
                                    Period = o.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                    Value = -1 * o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.RebenefitOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RebenefitOpers.RechargeOpers.Customers.ID,
                                    Period = o.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                    Value = o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.RebenefitOperPoses
                            .Where(o => o.RebenefitOpers.BenefitCorrectionOpers != null)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.RebenefitOpers.RechargeOpers.Customers.ID,
                                    Period = o.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                    Value = -1 * o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.PaymentOperPoses
                            .Where(o => o.PaymentOpers.PaymentPeriod <= period)
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.PaymentOpers.Customers.ID,
                                    Period = o.Period,
                                    Value = o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.PaymentCorrectionOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                                    o.PaymentCorrectionOpers.Period,
                                    o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.OverpaymentOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.OverpaymentOpers.Customers.ID,
                                    o.Period,
                                    o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Concat(_db.OverpaymentCorrectionOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.OverpaymentCorrectionOpers.ChargeOpers.Customers.ID,
                                    o.OverpaymentCorrectionOpers.Period,
                                    o.Value,
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Where(o =>
                            _startPeriod <= o.Period && o.Period <= period &&
                            _serivceTypeIDs.Contains(o.ServiceType))
                        .GroupBy(o => new { o.CustomerID, o.ServiceType })
                        .Select(g =>
                            new
                            {
                                g.Key.CustomerID,
                                g.Key.ServiceType,
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
                        .GroupBy(p =>
                            new
                            {
                                CustomerID = p.ChargeOpers.Customers.ID,
                                ServiceTypeID = p.Services.ServiceTypes.ID
                            })
                        .Select(g =>
                            new
                            {
                                g.Key.CustomerID,
                                g.Key.ServiceTypeID,
                                Value = g.Sum(p => p.Value)
                            })
                        .ToList()
                        .GroupBy(p => p.CustomerID)
                        .Select(byCustomer =>
                            new
                            {
                                CustomerID = byCustomer.Key,
                                ByServiceType = byCustomer
                                    .GroupBy(p => p.ServiceTypeID)
                                    .Select(byServiceType =>
                                        new
                                        {
                                            ServiceTypeID = byServiceType.Key,
                                            Value = byServiceType.Sum(p => p.Value)
                                        })
                            })
                        .ToDictionary(
                            r => r.CustomerID,
                            r => r.ByServiceType.ToDictionary(
                                st => st.ServiceTypeID,
                                st => st.Value));

                var _debts = _debtsRaw
                        .GroupBy(o => o.CustomerID)
                        .Select(g =>
                            new
                            {
                                CustomerID = g.Key,
                                ByServiceType = g.GroupBy(x => x.ServiceType)
                                    .Select(gServiceType =>
                                        new
                                        {
                                            ServiceType = gServiceType.Key,
                                            MonthCount = CalculateMonthCount(g.Key, gServiceType.Key, gServiceType.Sum(o => o.Value), _charges)
                                        })
                            })
                        .ToDictionary(x => x.CustomerID, x => x.ByServiceType.ToDictionary(y => y.ServiceType, y => y.MonthCount));

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
                                ServiceType = o.Services.ServiceTypes.ID
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
                                    ServiceType = o.Services.ServiceTypes.ID
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
                                    ServiceType = o.Services.ServiceTypes.ID
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
                                    ServiceType = o.Services.ServiceTypes.ID
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
                                    ServiceType = o.Services.ServiceTypes.ID
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
                                    ServiceType = o.Services.ServiceTypes.ID
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
                                    ServiceType = o.Services.ServiceTypes.ID
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
                                    ServiceType = o.Services.ServiceTypes.ID
                                }))
                        .Where(o => o.Period == period && _serivceTypeIDs.Contains(o.ServiceType))
                        .GroupBy(o => new { o.CustomerID, o.ServiceType })
                        .Select(g =>
                            new
                            {
                                g.Key.CustomerID,
                                g.Key.ServiceType,
                                Charge = g.Sum(o => o.Charge),
                                Recharge = g.Sum(o => o.Recharge),
                                Benefit = g.Sum(o => o.Benefit)
                            })
                        .ToList()
                        .Where(c => customerIds.Contains(c.CustomerID))
                        .GroupBy(o => o.CustomerID)
                        .Select(gCustomer =>
                            new
                            {
                                CustomerID = gCustomer.Key,
                                ByServiceType = gCustomer
                                    .GroupBy(o => o.ServiceType)
                                    .Select(gServiceType =>
                                        new
                                        {
                                            ID = gServiceType.Key,
                                            Charge = gServiceType.Sum(o => o.Charge),
                                            Recharge = gServiceType.Sum(o => o.Recharge),
                                            Benefit = gServiceType.Sum(o => o.Benefit)
                                        })
                            })
                        .Join(
                            _customers,
                            c1 => c1.CustomerID,
                            c2 => c2.ID,
                            (c1, c2) =>
                                new CustomerInfo
                                {
                                    ID = c2.ID,
                                    Square = c2.Square,
                                    ResidentCount = c2.ResidentCount,
                                    DebtsRepayment = c2.DebtsRepayment,
                                    DataByServiceType = c1.ByServiceType
                                        .Select(s =>
                                            new ServiceTypeData
                                            {
                                                ID = s.ID,
                                                Charge = s.Charge,
                                                Recharge = s.Recharge,
                                                Benefit = s.Benefit,
                                                Rate = Math.Round(s.Charge / c2.Square, 2, MidpointRounding.AwayFromZero)
                                            })
                                        .ToList()
                                })
                        .ToList();

                _result.ForEach(x =>
                    {
                        if (_debts.ContainsKey(x.ID))
                        {
                            x.DebtMonthCount = _debts[x.ID];
                        }
                    });

                #endregion
            }

            return _result;
        }

        private int CalculateMonthCount(int customerID, int serviceTypeID, decimal debtValue, Dictionary<int, Dictionary<int, decimal>> charges)
        {
            int _debtMonthCount = 0;

            if (charges.ContainsKey(customerID) && charges[customerID].ContainsKey(serviceTypeID))
            {
                _debtMonthCount = Convert.ToInt32(Math.Round(debtValue / charges[customerID][serviceTypeID], 0, MidpointRounding.AwayFromZero));
            }

            return _debtMonthCount;
        }

        private void FillAccountsDictionaries(Dictionary<int, Dictionary<int, int>> accRowDict, ExcelSheet sheet)
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
                    string _serviceType = sheet.GetCell(GKU, i);
                    int? _serviceTypeId = ParseServiceType(_serviceType);
                    if (_serviceTypeId.HasValue)
                    {
                        if (_serivceTypeIDs.Contains(_serviceTypeId.Value))
                        {
                            if (!accRowDict.ContainsKey(_customerId))
                            {
                                accRowDict.Add(_customerId, new Dictionary<int, int>(4));
                            }

                            if (accRowDict[_customerId].ContainsKey(_serviceTypeId.Value))
                            {
                                accRowDict[_customerId][_serviceTypeId.Value] = i;
                            }
                            else
                            {
                                accRowDict[_customerId].Add(_serviceTypeId.Value, i);
                            }
                        }
                    }
                }
            }
        }

        private void FillSheet(
            List<CustomerInfo> data,
            Dictionary<int, Dictionary<int, int>> accRowDict,
            ExcelSheet sheet)
        {
            foreach (CustomerInfo _customerInfo in data)
            {
                string _squareStr = _customerInfo.Square.ToString().Replace(",", ".");
                string _residentCountStr = _customerInfo.ResidentCount.ToString();
                string _restrDebt = _customerInfo.DebtsRepayment ? "1" : "0";

                if (accRowDict.ContainsKey(_customerInfo.ID))
                {
                    Dictionary<int, int> _rowsByServiceType = accRowDict[_customerInfo.ID];

                    for (int i = 0; i < _serivceTypeIDs.Length; i++)
                    {
                        int _serviceTypeID = _serivceTypeIDs[i];
                        if (_rowsByServiceType.ContainsKey(_serviceTypeID))
                        {
                            FillRow(
                                _rowsByServiceType[_serviceTypeID],
                                _customerInfo.DataByServiceType.Where(s => s.ID == _serviceTypeID),
                                _squareStr,
                                _residentCountStr,
                                _restrDebt,
                                _customerInfo.DebtMonthCount != null && _customerInfo.DebtMonthCount.ContainsKey(_serviceTypeID)
                                    ? _customerInfo.DebtMonthCount[_serviceTypeID].ToString()
                                    : "0",
                                sheet);
                        }
                    }
                }
            }
        }

        private void FillRow(int row, IEnumerable<ServiceTypeData> data, string square, string residentCount, string restrDebt, string monthCount, ExcelSheet sheet)
        {
            sheet.SetCell(PL, row, square);
            sheet.SetCell(NORM, row, square);
            sheet.SetCell(KOLP, row, residentCount);
            sheet.SetCell(RESTRDOLG, row, restrDebt);
            sheet.SetCell(MESD, row, monthCount);

            decimal _charge = 0,
                    _recharge = 0,
                    _rate = 0;

            foreach (ServiceTypeData _data in data)
            {
                _charge += _data.Charge;
                _recharge += _data.Recharge + _data.Charge;
                _rate += _data.Rate;
            }

            sheet.SetCell(FAKTP, row, _charge.ToString(CultureInfo.InvariantCulture));
            sheet.SetCell(FAKTPER, row, _recharge.ToString(CultureInfo.InvariantCulture));
            sheet.SetCell(TARIF, row, _rate.ToString(CultureInfo.InvariantCulture));
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

                    Dictionary<int, Dictionary<int, int>> _accRowDict = new Dictionary<int, Dictionary<int, int>>();
                    FillAccountsDictionaries(_accRowDict, _sheet);

                    List<CustomerInfo> _data = GetCustomerInfoList(_period, _accRowDict.Keys.ToList());

                    FillSheet(_data, _accRowDict, _sheet);

                    _sheet.Save();

                    _result = "Операция выполнена успешно";
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite(
                    $"Benefit Export error: {_ex}");
                _result = "Произошла ошибка. Операция не выполнена";
            }

            return _result;
        }

        #endregion
    }
}
