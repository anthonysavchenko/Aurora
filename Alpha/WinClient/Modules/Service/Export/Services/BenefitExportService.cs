using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public class BenefitExportService : IBenefitExportService
    {
        private class Columns
        {
            /// <summary>
            /// Номер счета
            /// </summary>
            public const string LS = "A";

            /// <summary>
            /// Тип услуги
            /// </summary>
            public const string GKU = "H";

            /// <summary>
            /// Площадь
            /// </summary>
            public const string PL = "I";

            /// <summary>
            /// Количество проживающих
            /// </summary>
            public const string KOLP = "J";

            /// <summary>
            /// Норматив
            /// </summary>
            public const string NORM = "K";

            /// <summary>
            /// Фактическое потребление ЖКУ за месяц
            /// </summary>
            public const string FAKTP = "L";

            /// <summary>
            /// Фактическое потребление ЖКУ с учетом перерасчетов за прошлый период
            /// </summary>
            public const string FAKTPER = "M";

            /// <summary>
            /// Тариф
            /// </summary>
            public const string TARIF = "N";

            /// <summary>
            /// Количество месяцев долга
            /// </summary>
            public const string MESD = "S";

            /// <summary>
            /// Флаг соглашения о реструктуризации долга
            /// </summary>
            public const string RESTRDOLG = "T";

            public const string PERIOD_COLUMN = "O";
        }

        private class ServiceTypes
        {
            public const string MAINTANCE_SERVICE_TYPE_STR = "С";
            public const string REPAIR_SERVICE_TYPE_STR = "Р";
            public const string PP_COLD_WATER_SERVICE_TYPE_STR = "Х";
            public const string PP_HOT_WATER_SERVICE_TYPE_STR = "Г";
            public const string PP_ELECTRICITY_WATER_SERVICE_TYPE_STR = "Э";

            public const int MAINTANCE_SERVICE_TYPE_ID = 1;
            public const int REPAIR_SERVICE_TYPE_ID = 5;
            public const int PP_COLD_WATER_SERVICE_TYPE_ID = 3;
            public const int PP_HOT_WATER_SERVICE_TYPE_ID = 2;
            public const int PP_ELECTRICITY_WATER_SERVICE_TYPE_ID = 4;

            public static readonly int[] SerivceTypeIDs =
                new[]
                {
                    MAINTANCE_SERVICE_TYPE_ID,
                    REPAIR_SERVICE_TYPE_ID,
                    PP_COLD_WATER_SERVICE_TYPE_ID,
                    PP_HOT_WATER_SERVICE_TYPE_ID,
                    PP_ELECTRICITY_WATER_SERVICE_TYPE_ID
                };
        }

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

        [ServiceDependency]
        public IExcelService ExcelService { get; set; }

        #region Help Methods

        private string CorrectAccount(string badAccountStr)
        {
            return !string.IsNullOrEmpty(badAccountStr) && badAccountStr.Length >= 8
                ? badAccountStr.Trim().Replace("-", string.Empty)
                : badAccountStr;
        }

        private int? ParseServiceType(string serviceType)
        {
            string _firstLetter = serviceType.Trim().Substring(0, 1).ToUpper();

            switch (_firstLetter)
            {
                case ServiceTypes.MAINTANCE_SERVICE_TYPE_STR:
                    return ServiceTypes.MAINTANCE_SERVICE_TYPE_ID;
                case ServiceTypes.REPAIR_SERVICE_TYPE_STR:
                    return ServiceTypes.REPAIR_SERVICE_TYPE_ID;
                case ServiceTypes.PP_COLD_WATER_SERVICE_TYPE_STR:
                    return ServiceTypes.PP_COLD_WATER_SERVICE_TYPE_ID;
                case ServiceTypes.PP_HOT_WATER_SERVICE_TYPE_STR:
                    return ServiceTypes.PP_HOT_WATER_SERVICE_TYPE_ID;
                case ServiceTypes.PP_ELECTRICITY_WATER_SERVICE_TYPE_STR:
                    return ServiceTypes.PP_ELECTRICITY_WATER_SERVICE_TYPE_ID;
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

        private List<CustomerInfo> GetCustomerInfoList(DateTime period, DateTime startPeriod, List<int> customerIds)
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
                            startPeriod <= o.Period && o.Period <= period &&
                            ServiceTypes.SerivceTypeIDs.Contains(o.ServiceType))
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
                        .Where(o => o.Period == period && ServiceTypes.SerivceTypeIDs.Contains(o.ServiceType))
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

        private Dictionary<int, Dictionary<int, int>> Parse(IExcelWorksheet sheet, Action<int> reportProgressAction)
        {
            Dictionary<int, Dictionary<int, int>> _accRowDict = new Dictionary<int, Dictionary<int, int>>();
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

            int _rowCount = sheet.GetRowCount();

            for (int i = 2; i <= _rowCount; i++)
            {
                string _account = CorrectAccount(sheet.Cell(i, Columns.LS).Value);

                if (!string.IsNullOrEmpty(_account) && _customerIdByAccount.ContainsKey(_account))
                {
                    int _customerId = _customerIdByAccount[_account];
                    string _serviceType = sheet.Cell(i, Columns.GKU).Value;
                    int? _serviceTypeId = ParseServiceType(_serviceType);
                    if (_serviceTypeId.HasValue)
                    {
                        if (ServiceTypes.SerivceTypeIDs.Contains(_serviceTypeId.Value))
                        {
                            if (!_accRowDict.ContainsKey(_customerId))
                            {
                                _accRowDict.Add(_customerId, new Dictionary<int, int>(4));
                            }

                            if (_accRowDict[_customerId].ContainsKey(_serviceTypeId.Value))
                            {
                                _accRowDict[_customerId][_serviceTypeId.Value] = i;
                            }
                            else
                            {
                                _accRowDict[_customerId].Add(_serviceTypeId.Value, i);
                            }
                        }
                    }
                }

                reportProgressAction((i - 1) * 50 / _rowCount);
            }

            return _accRowDict;
        }

        private void FillSheet(
            List<CustomerInfo> data,
            Dictionary<int, Dictionary<int, int>> accRowDict,
            IExcelWorksheet sheet,
            Action<int> reportProgressAction)
        {
            int _processed = 0;
            int _count = accRowDict.Values.SelectMany(v => v.Values).Count();
            foreach (CustomerInfo _customerInfo in data)
            {
                string _residentCountStr = _customerInfo.ResidentCount.ToString();
                string _restrDebt = _customerInfo.DebtsRepayment ? "1" : "0";

                if (accRowDict.ContainsKey(_customerInfo.ID))
                {
                    Dictionary<int, int> _rowsByServiceType = accRowDict[_customerInfo.ID];

                    for (int i = 0; i < ServiceTypes.SerivceTypeIDs.Length; i++)
                    {
                        int _serviceTypeID = ServiceTypes.SerivceTypeIDs[i];
                        List<ServiceTypeData> _data = _customerInfo.DataByServiceType.Where(s => s.ID == _serviceTypeID).ToList();
                        if (_rowsByServiceType.ContainsKey(_serviceTypeID) && _data.Count > 0)
                        {
                            FillRow(
                                _rowsByServiceType[_serviceTypeID],
                                _data,
                                _customerInfo.Square,
                                _residentCountStr,
                                _restrDebt,
                                _customerInfo.DebtMonthCount != null && _customerInfo.DebtMonthCount.ContainsKey(_serviceTypeID)
                                    ? _customerInfo.DebtMonthCount[_serviceTypeID].ToString()
                                    : "0",
                                sheet);

                            reportProgressAction(++_processed * 50 / _count + 50);
                        }
                    }
                }
            }
        }

        private void FillRow(
            int row, 
            IEnumerable<ServiceTypeData> data, 
            decimal square, 
            string residentCount, 
            string restrDebt, 
            string monthCount, 
            IExcelWorksheet sheet)
        {
            sheet.Cell(row, Columns.PL).SetValue(square);
            sheet.Cell(row, Columns.NORM).SetValue(square);
            sheet.Cell(row, Columns.KOLP).SetValue(residentCount);
            sheet.Cell(row, Columns.RESTRDOLG).SetValue(restrDebt);
            sheet.Cell(row, Columns.MESD).SetValue(monthCount);

            decimal _charge = 0,
                    _recharge = 0,
                    _rate = 0;

            foreach (ServiceTypeData _data in data)
            {
                _charge += _data.Charge;
                _recharge += _data.Recharge + _data.Charge;
                _rate += _data.Rate;
            }

            sheet.Cell(row, Columns.FAKTP).SetValue(_charge);
            sheet.Cell(row, Columns.FAKTPER).SetValue(_recharge);
            sheet.Cell(row, Columns.TARIF).SetValue(_rate);
        }

        #endregion

        #region Implementation of IBenefitDataExportService

        public ExportResult Export(string outputPath, string templatePath, DateTime startPeriod, Action<int> progressAction)
        {
            ExportResult _result = new ExportResult();
            try
            {
                using (IExcelWorkbook _xwb = ExcelService.OpenWorkbook(templatePath))
                {
                    IExcelWorksheet _xws = _xwb.Worksheet(1);
                    string _periodStr = _xws.Cell(2, Columns.PERIOD_COLUMN).Value;
                    DateTime _period = GetPeriod(_periodStr);

                    Dictionary<int, Dictionary<int, int>> _accRowDict = Parse(_xws, progressAction);
                    List<CustomerInfo> _data = GetCustomerInfoList(_period, startPeriod, _accRowDict.Keys.ToList());

                    FillSheet(_data, _accRowDict, _xws, progressAction);

                    _xwb.Save();

                    _result.Info = "Операция выполнена успешно";
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Benefit Export error: {_ex}");
                _result.Info = "Произошла ошибка. Операция не выполнена";
                progressAction(100);
            }

            return _result;
        }

        #endregion
    }
}
