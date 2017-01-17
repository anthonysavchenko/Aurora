using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private const int TOTAL_PERCENT = 10;

        private static class ColumnNames
        {
            public const string CONTRACTOR_COLUMN = "Contractor";
            public const string SERVICE_COLUMN = "Service";
            public const string SERVICE_TYPE_COLUMN = "ServiceType";
            public const string INCOME_SUM_COLUMN = "IncomeValue";
            public const string MC_VALUE_COLUMN = "MCValue";
            public const string TO_TRANSFER_VALUE_COLUMN = "ToTransferValue";
            public const string NUMBER_COLUMN = "Number";
        }

        private class ServiceInfo
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string ServiceTypeName { get; set; }
            public int ServiceTypeID { get; set; }
        }

        private class PeriodCharge
        {
            public decimal ValueSum { get; set; }
            public IEnumerable<ContractorValue> ContractorValue { get; set; }
        }

        private class ContractorValue
        {
            public int ContractorID { get; set; }
            public decimal Value { get; set; }
        }

        private class IntermediaryValue
        {
            public int IntermediaryID { get; set; }
            public decimal Value { get; set; }
        }

        private class ServicePayment
        {
            public int ServiceID { get; set; }
            public DateTime Period { get; set; }
            public decimal ValueSum { get; set; }
            public IEnumerable<IntermediaryValue> IntermediaryValues { get; set; }
        }

        private class ContractorData
        {
            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="intermediaryIDs">Массив с ID посредников</param>
            public ContractorData(int[] intermediaryIDs)
            {
                IntermediaryValues = new Dictionary<int, decimal>(intermediaryIDs.Length);

                foreach (int _id in intermediaryIDs)
                {
                    IntermediaryValues.Add(_id, 0);
                }
            }

            public readonly object lockObject = new object();

            /// <summary>
            /// Сумма УК
            /// </summary>
            public decimal ManagementCompanyValue { get; set; }

            /// <summary>
            /// Сумма у перечислению
            /// </summary>
            public decimal TransferValue { get; set; }

            /// <summary>
            /// Сумма платежей
            /// </summary>
            public decimal PaymentValue { get; set; }

            /// <summary>
            /// Комиссия посредников
            /// </summary>
            public Dictionary<int, decimal> IntermediaryValues { get; private set; }
        }

        private Dictionary<int, Intermediaries> _intermediaries;

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            DateTimeInfo _dateTimeInfo = ServerTime.GetDateTimeInfo();
            View.SinceDateTime = _dateTimeInfo.SinceMonthBeginning;
            View.TillDateTime = _dateTimeInfo.TillToday;
        }

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        override protected void ProcessGridData()
        {
            View.ClearColumns();

            View.AddColumn(ColumnNames.CONTRACTOR_COLUMN, "Подрядчик", true, ColumnNames.CONTRACTOR_COLUMN, false, 0);
            View.AddColumn(ColumnNames.SERVICE_TYPE_COLUMN, "Вид услуги", true, ColumnNames.NUMBER_COLUMN, true, 0);
            View.AddColumn(ColumnNames.SERVICE_COLUMN, "Услуга", false, String.Empty, true, 1);
            View.AddSummaryColumn(ColumnNames.INCOME_SUM_COLUMN, "Сумма поступления");

            foreach (Intermediaries _intermediary in _intermediaries.Values)
            {
                View.AddSummaryColumn(_intermediary.Code, _intermediary.Name);
            }

            View.AddSummaryColumn(ColumnNames.MC_VALUE_COLUMN, "Управляющая компания");
            View.AddSummaryColumn(ColumnNames.TO_TRANSFER_VALUE_COLUMN, "К перечислению");

            base.ProcessGridData();
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add(ColumnNames.NUMBER_COLUMN, typeof(int));
            _table.Columns.Add(ColumnNames.CONTRACTOR_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.SERVICE_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.SERVICE_TYPE_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.INCOME_SUM_COLUMN, typeof(decimal));
            _table.Columns.Add(ColumnNames.MC_VALUE_COLUMN, typeof(decimal));
            _table.Columns.Add(ColumnNames.TO_TRANSFER_VALUE_COLUMN, typeof(decimal));

            DateTime _since = View.SinceDateTime;
            DateTime _till = View.TillDateTime;

            bool _isAnyPaymentsExist;

            Dictionary<int, Dictionary<DateTime, PeriodCharge>> _charges = null;
            List<ServicePayment> _payments = null;
            Dictionary<int, string> _contractors = null;
            Dictionary<int, ServiceInfo> _services = null;

            using (var _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                _intermediaries = _entities.Intermediaries.ToDictionary(i => i.ID);

                var _periods =
                    _entities.PaymentOperPoses
                        .Where(
                            p =>
                            p.PaymentOpers.CreationDateTime >= _since &&
                            p.PaymentOpers.CreationDateTime <= _till)
                        .Select(p => p.Period)
                        .Union(
                            _entities.PaymentCorrectionOpers
                                .Where(
                                    p =>
                                    p.CreationDateTime >= _since &&
                                    p.CreationDateTime <= _till)
                                .Select(p => p.Period))
                        .Distinct()
                        .ToList();

                _isAnyPaymentsExist = _periods.Any();

                if (_isAnyPaymentsExist)
                {
                    DateTime _minPeriod = _periods.Min();
                    DateTime _maxPeriod = _periods.Max();

                    #region Запрос данных

                    _charges =
                        _entities.ChargeOperPoses
                            .Select(
                                c =>
                                new
                                {
                                    c.ChargeOpers.ChargeSets.Period,
                                    ServiceID = c.Services.ID,
                                    ContractorID = c.Contractors.ID,
                                    c.Value
                                })
                            .Where(
                                c =>
                                c.Period >= _minPeriod &&
                                c.Period <= _maxPeriod)
                            .Concat(
                                _entities.ChargeCorrectionOperPoses
                                    .Select(
                                        c =>
                                        new
                                        {
                                            c.ChargeCorrectionOpers.Period,
                                            ServiceID = c.Services.ID,
                                            ContractorID = c.Contractors.ID,
                                            c.Value
                                        })
                                    .Where(
                                        c =>
                                        c.Period >= _minPeriod &&
                                        c.Period <= _maxPeriod))
                            .Concat(
                                _entities.RechargeOperPoses
                                    .Select(
                                        c =>
                                        new
                                        {
                                            c.RechargeOpers.RechargeSets.Period,
                                            ServiceID = c.Services.ID,
                                            ContractorID = c.Contractors.ID,
                                            c.Value
                                        })
                                    .Where(
                                        c =>
                                        c.Period >= _minPeriod &&
                                        c.Period <= _maxPeriod))
                            .GroupBy(
                                c =>
                                new
                                {
                                    c.Period,
                                    c.ServiceID,
                                    c.ContractorID
                                })
                            .Select(
                                g =>
                                new
                                {
                                    g.Key.Period,
                                    g.Key.ServiceID,
                                    g.Key.ContractorID,
                                    Value = g.Sum(c => c.Value)
                                })
                            .ToList()
                            .GroupBy(r => r.ServiceID)
                            .Select(
                                g =>
                                new
                                {
                                    ServiceID = g.Key,
                                    GroupedByPeriod =
                                        g.GroupBy(r => r.Period)
                                            .Select(
                                                groupedByPeriod =>
                                                new
                                                {
                                                    Period = groupedByPeriod.Key,
                                                    ValueSum = groupedByPeriod.Sum(r => r.Value),
                                                    GroupedByConractor =
                                                        groupedByPeriod.GroupBy(r => r.ContractorID)
                                                            .Select(
                                                                groupedByContractor =>
                                                                new ContractorValue
                                                                {
                                                                    ContractorID = groupedByContractor.Key,
                                                                    Value = groupedByContractor.Sum(r => r.Value)
                                                                })
                                                })
                                })
                            .ToDictionary(
                                r => r.ServiceID,
                                r =>
                                r.GroupedByPeriod
                                    .ToDictionary(
                                        g => g.Period,
                                        g =>
                                        new PeriodCharge
                                        {
                                            ValueSum = g.ValueSum,
                                            ContractorValue = g.GroupedByConractor
                                        }));

                    _payments =
                        _entities.PaymentOperPoses
                            .Where(p =>
                                p.PaymentOpers.PaymentSets.Intermediaries != null &&
                                p.PaymentOpers.CreationDateTime >= _since && p.PaymentOpers.CreationDateTime <= _till)
                            .Select(p =>
                                new
                                {
                                    IntermediaryID = p.PaymentOpers.PaymentSets.Intermediaries.ID,
                                    p.Period,
                                    ServiceID = p.Services.ID,
                                    p.Value
                                })
                            .Concat(
                                _entities.PaymentOperPoses
                                    .Where(p =>
                                        p.PaymentOpers.PaymentCorrectionOper != null &&
                                        p.PaymentOpers.PaymentSets.Intermediaries != null &&
                                        p.PaymentOpers.CreationDateTime >= _since && p.PaymentOpers.CreationDateTime <= _till &&
                                        p.PaymentOpers.PaymentCorrectionOper.CreationDateTime >= _since && p.PaymentOpers.PaymentCorrectionOper.CreationDateTime <= _till)
                                    .Select(p =>
                                        new
                                        {
                                            IntermediaryID = p.PaymentOpers.PaymentSets.Intermediaries.ID,
                                            p.Period,
                                            ServiceID = p.Services.ID,
                                            Value = p.Value * -1
                                        }))
                            .GroupBy(
                                p =>
                                new
                                {
                                    p.ServiceID,
                                    p.Period
                                })
                            .Select(
                                groupedByServiceAndPeriod =>
                                new ServicePayment
                                {
                                    ServiceID = groupedByServiceAndPeriod.Key.ServiceID,
                                    Period = groupedByServiceAndPeriod.Key.Period,
                                    ValueSum = -1 * groupedByServiceAndPeriod.Sum(r => r.Value),
                                    IntermediaryValues =
                                        groupedByServiceAndPeriod
                                            .GroupBy(r => r.IntermediaryID)
                                            .Select(
                                                groupedByIntermediary =>
                                                new IntermediaryValue
                                                {
                                                    IntermediaryID = groupedByIntermediary.Key,
                                                    Value = -1 * groupedByIntermediary.Sum(x => x.Value)
                                                })
                                })
                            .ToList();

                    _services =
                        _entities.Services
                            .Include("ServiceTypes")
                            .Select(
                                s =>
                                new ServiceInfo
                                {
                                    ID = s.ID,
                                    Name = s.Name,
                                    ServiceTypeName = s.ServiceTypes.Name,
                                    ServiceTypeID = s.ServiceTypes.ID
                                })
                            .ToDictionary(s => s.ID);

                    _contractors =
                        _entities.Contractors
                                .Select(
                                    c =>
                                    new
                                    {
                                        c.ID,
                                        c.Name
                                    })
                            .ToDictionary(c => c.ID, c => c.Name);

                    #endregion
                }
            }

            if (_isAnyPaymentsExist)
            {
                foreach (var _intermediary in _intermediaries.Values)
                {
                    _table.Columns.Add(_intermediary.Code, typeof(decimal));
                }

                ConcurrentDictionary<int, ConcurrentDictionary<int, ContractorData>> _serviceData =
                        new ConcurrentDictionary<int, ConcurrentDictionary<int, ContractorData>>();

                int[] _intermediaryIDs = _intermediaries.Keys.ToArray();

                #region Вычисления

                Parallel.ForEach(
                    _payments,
                    p => ProcessPayment(p, _charges, _intermediaryIDs, _serviceData));

                #endregion

                #region Заполнение таблицы

                Dictionary<int, ContractorData> _commonContractorData = new Dictionary<int, ContractorData>();

                foreach (KeyValuePair<int, ConcurrentDictionary<int, ContractorData>> _pair in _serviceData)
                {
                    var _serviceInfo = _services[_pair.Key];

                    foreach (KeyValuePair<int, ContractorData> _keyValuePair in _pair.Value)
                    {
                        DataRow _row = _table.NewRow();

                        decimal _paymentValue = Math.Round(_keyValuePair.Value.PaymentValue, 2, MidpointRounding.AwayFromZero);
                        decimal _mcValue = Math.Round(_keyValuePair.Value.ManagementCompanyValue, 2, MidpointRounding.AwayFromZero);
                        decimal _transferValue = Math.Round(_keyValuePair.Value.TransferValue, 2, MidpointRounding.AwayFromZero);
                        decimal _rowPaymentValue = _mcValue + _transferValue;

                        if (!_commonContractorData.ContainsKey(_keyValuePair.Key))
                        {
                            _commonContractorData.Add(_keyValuePair.Key, new ContractorData(_intermediaryIDs));
                        }

                        foreach (KeyValuePair<int, decimal> _intermediaryValue in _keyValuePair.Value.IntermediaryValues)
                        {
                            decimal _value = Math.Round(_intermediaryValue.Value, 2, MidpointRounding.AwayFromZero); ;

                            Intermediaries _intermediary = _intermediaries[_intermediaryValue.Key];
                            _row[_intermediary.Code] = _value;
                            _rowPaymentValue += _value;

                            _commonContractorData[_keyValuePair.Key].IntermediaryValues[_intermediaryValue.Key] += _value;
                        }

                        if (_rowPaymentValue < _paymentValue)
                        {
                            _mcValue += _paymentValue - _rowPaymentValue;
                        }

                        _row[ColumnNames.NUMBER_COLUMN] = _serviceInfo.ServiceTypeID;
                        _row[ColumnNames.CONTRACTOR_COLUMN] = _contractors[_keyValuePair.Key];
                        _row[ColumnNames.SERVICE_COLUMN] = _serviceInfo.Name;
                        _row[ColumnNames.SERVICE_TYPE_COLUMN] = _serviceInfo.ServiceTypeName;
                        _row[ColumnNames.INCOME_SUM_COLUMN] = _paymentValue;
                        _row[ColumnNames.MC_VALUE_COLUMN] = _mcValue;
                        _row[ColumnNames.TO_TRANSFER_VALUE_COLUMN] = _transferValue;

                        _commonContractorData[_keyValuePair.Key].PaymentValue += _paymentValue;
                        _commonContractorData[_keyValuePair.Key].ManagementCompanyValue += _mcValue;
                        _commonContractorData[_keyValuePair.Key].TransferValue += _transferValue;

                        _table.Rows.Add(_row);
                    }
                }

                int _commonNumber = _services.Values.Max(v => v.ServiceTypeID) + 1;

                foreach (var _pair in _commonContractorData)
                {
                    DataRow _row = _table.NewRow();

                    _row[ColumnNames.NUMBER_COLUMN] = _commonNumber;
                    _row[ColumnNames.CONTRACTOR_COLUMN] = _contractors[_pair.Key];
                    _row[ColumnNames.SERVICE_COLUMN] = string.Empty;
                    _row[ColumnNames.SERVICE_TYPE_COLUMN] = "Итого";
                    _row[ColumnNames.INCOME_SUM_COLUMN] = _pair.Value.PaymentValue;
                    _row[ColumnNames.MC_VALUE_COLUMN] = _pair.Value.ManagementCompanyValue;
                    _row[ColumnNames.TO_TRANSFER_VALUE_COLUMN] = _pair.Value.TransferValue;

                    foreach (var _intermediaryValue in _pair.Value.IntermediaryValues)
                    {
                        Intermediaries _intermediary = _intermediaries[_intermediaryValue.Key];
                        _row[_intermediary.Code] = _intermediaryValue.Value;
                    }

                    _table.Rows.Add(_row);
                }

                #endregion
            }

            return _table;
        }

        private void ProcessPayment(
            ServicePayment payment,
            Dictionary<int, Dictionary<DateTime, PeriodCharge>> charges,
            int[] intermediaryIDs,
            ConcurrentDictionary<int, ConcurrentDictionary<int, ContractorData>> serviceData)
        {
            if (charges.ContainsKey(payment.ServiceID) && charges[payment.ServiceID].ContainsKey(payment.Period))
            {
                ConcurrentDictionary<int, ContractorData> _contractorDataDictionary =
                    serviceData.GetOrAdd(payment.ServiceID, key => new ConcurrentDictionary<int, ContractorData>());

                PeriodCharge _chargesByContractor = charges[payment.ServiceID][payment.Period];

                foreach (var _charge in _chargesByContractor.ContractorValue)
                {
                    ContractorData _contractorData =
                        _contractorDataDictionary.GetOrAdd(_charge.ContractorID, key => new ContractorData(intermediaryIDs));

                    decimal _contractorPayment =
                        _chargesByContractor.ValueSum != 0
                            ? ((payment.ValueSum / _chargesByContractor.ValueSum) * _charge.Value)
                            : 0;

                    lock (_contractorData.lockObject)
                    {
                        _contractorData.PaymentValue += _contractorPayment;
                    }

                    decimal _intermediariesSum = 0;

                    foreach (var _pair in payment.IntermediaryValues)
                    {
                        Intermediaries _intermediary =
                            _intermediaries[_pair.IntermediaryID];

                        // Коммисия посредника
                        decimal _value =
                            _chargesByContractor.ValueSum != 0
                                ? ((_pair.Value / _chargesByContractor.ValueSum) * _charge.Value * _intermediary.Rate / 100)
                                : 0;

                        lock (_contractorData.lockObject)
                        {
                            _contractorData.IntermediaryValues[_pair.IntermediaryID] += _value;
                        }
                        _intermediariesSum += _value;
                    }

                    // Сумма УК равна 10% от суммы поступления минус сумма комиссий всех посредников
                    decimal _mcValue = (_contractorPayment * TOTAL_PERCENT / 100) - _intermediariesSum;
                    lock (_contractorData.lockObject)
                    {
                        _contractorData.ManagementCompanyValue += _mcValue;
                        _contractorData.TransferValue += _contractorPayment - _intermediariesSum - _mcValue;
                    }
                }
            }
        }
    }
}