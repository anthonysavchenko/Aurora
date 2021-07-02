using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Server.PrintForms.DataSets;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report.Models;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report
{
    /// <summary>
    /// Презентер вида с отчетом
    /// </summary>
    public class ReportViewPresenter : BaseReportForReportObjectPresenter<IReportView, EmptyReportParams>
    {
        public class Cstmr
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public List<Cstmr> customers;

        public int customerIndex = 0;

        public decimal Debt = 0;

        /// <summary>
        /// Данные
        /// </summary>
        private MutualSettlementDataSet _data;

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            if (customers == null || customers.Count < 1)
            {
                using (Entities db = new Entities())
                {
                    customers =
                        db.Customers
                            .Select(c =>
                                new
                                {
                                    c.ID,
                                    Street = c.Buildings.Streets.Name,
                                    Building = c.Buildings.Number,
                                    c.Apartment,
                                })
                            .ToList()
                            .Select(c =>
                                new Cstmr()
                                {
                                    ID = c.ID,
                                    Name =
                                        $"{c.Street}, д. {c.Building}, кв. {c.Apartment}",
                                })
                            .OrderBy(c => c.Name)
                            .ToList();
                }
            }

            base.OnViewReady();
            WorkItem.State[ModuleStateNames.START_UP_PARAMS] = new MutualSettlementStartUpParams(
                new DateTime(2012, 8, 1),
                new DateTime(2021, 2, 1),
                customers[customerIndex].ID.ToString());
            View.UpdateReport();
        }

        protected override void OnReportGenerationCompleted()
        {
            base.OnReportGenerationCompleted();

            if (Debt >= 500)
            {
                string fileName = customers[customerIndex].Name;
                Path.GetInvalidFileNameChars().ToList().ForEach(c => fileName = fileName.Replace(c, '.'));
                Path.GetInvalidPathChars().ToList().ForEach(c => fileName = fileName.Replace(c, '.'));
                View.ExportReport($"D:\\qvitex\\{fileName}.xlsx");
            }

            Debt = 0;

            customerIndex++;
            if (customerIndex < customers.Count)
            {
                OnViewReady();
            }
        }

        /// <summary>
        /// Обрабатывает данные табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            View.DataSource = _data;
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            try
            {
                _data = new MutualSettlementDataSet();
                DataTable _settlementTable = _data.Tables["MutualSettlement"];
                DataTable _posesTable = _data.Tables["MutualSettlementPoses"];
                var _startUpParams = (MutualSettlementStartUpParams)WorkItem.State[ModuleStateNames.START_UP_PARAMS];
                int _customerId = int.Parse(_startUpParams.CustomerId);

                using (Entities _entities = new Entities())
                {
                    _entities.CommandTimeout = 3600;

                    decimal _debt = _entities.GetDebt(_customerId, _startUpParams.Since);
                    var _periodBalances = _entities.GetReportData(_startUpParams.Since, _startUpParams.Till, _customerId);
                    
                    int _reportNumber = 0;
                    int _groupNumber = 0;
                    DataRow _lastReportRow = null;
                    DateTime _previousPeriod = DateTime.MinValue;
                    DateTime _now = new DateTime(2021, 2, 1);
                    ServiceBalances _yearBalances = new ServiceBalances();
                    ServiceBalances _reportBalances = new ServiceBalances();
                    
                    Customers _customer =
                        _entities.Customers
                            .Include("Residents")
                            .Include("Buildings")
                            .Include("Buildings.Streets")
                            .First(x => x.ID == _customerId);

                    for (int i = 0; i < _periodBalances.Length; i++)
                    {
                        var _periodBalance = _periodBalances[i];

                        // Заполенение таблицы отчетов
                        if (_previousPeriod == DateTime.MinValue || _periodBalance.Period != _previousPeriod.AddMonths(1))
                        {
                            decimal _totalDebt = 
                                Math.Round(_periodBalances.Sum(x => x.ServiceBalances.Balances.Values.Sum(y => y.Debt)), 2, MidpointRounding.AwayFromZero);

                            _lastReportRow = _settlementTable.Rows.Add(
                                ++_reportNumber,
                                _previousPeriod == DateTime.MinValue
                                    ? _startUpParams.Since
                                    : _previousPeriod,
                                _startUpParams.Till,
                                _now,
                                _customer.OwnerType == (int)OwnerType.JuridicalPerson
                                    ? _customer.JuridicalPersonFullName
                                    : _customer.PhysicalPersonShortName,
                                string.Format("ул. {0}, {1}, кв. {2}",
                                     _customer.Buildings.Streets.Name,
                                     _customer.Buildings.Number,
                                     _customer.Apartment),
                                string.Format("{0} кв.м.", _customer.Square),
                                _customer.Residents.Count(),
                                string.Empty,
                                _debt,
                                _totalDebt,
                                _totalDebt + _debt);

                            Debt = _totalDebt + _debt;
                        }

                        // Заполнение таблицы балансов по типам услуг за месяц
                        foreach (KeyValuePair<ServiceBalanceKey, Balance> _serviceTypeBalance in _periodBalance.ServiceBalances.Balances)
                        {
                            _yearBalances.Add(_serviceTypeBalance.Key, _serviceTypeBalance.Value);
                            _reportBalances.Add(_serviceTypeBalance.Key, _serviceTypeBalance.Value);

                            _posesTable.Rows.Add(
                                _reportNumber,
                                _periodBalance.Period.ToString("MMMM yyyy"),
                                _groupNumber,
                                _serviceTypeBalance.Key.Name,
                                _serviceTypeBalance.Value.Charge,
                                Math.Abs(_serviceTypeBalance.Value.Benefit),
                                _serviceTypeBalance.Value.Recharge,
                                _serviceTypeBalance.Value.Payable,
                                Math.Abs(_serviceTypeBalance.Value.Act),
                                Math.Abs(_serviceTypeBalance.Value.Payment),
                                _serviceTypeBalance.Value.Debt,
                                Math.Abs(_serviceTypeBalance.Value.PaymentOnCreateDate),
                                Math.Abs(_serviceTypeBalance.Value.PaymentOnEnterPeriod));
                        }

                        _groupNumber++;

                        // Заполенение таблицы балансов по типа услуг за год
                        if (i + 1 == _periodBalances.Length || _periodBalances[i + 1].Period.Year != _periodBalance.Period.Year)
                        {
                            foreach (KeyValuePair<ServiceBalanceKey, Balance> _serviceTypeBalance in _yearBalances.Balances)
                            {
                                _posesTable.Rows.Add(
                                    _reportNumber,
                                    _periodBalance.Period.ToString("yyyy"),
                                    _groupNumber,
                                    _serviceTypeBalance.Key.Name,
                                    _serviceTypeBalance.Value.Charge,
                                    Math.Abs(_serviceTypeBalance.Value.Benefit),
                                    _serviceTypeBalance.Value.Recharge,
                                    _serviceTypeBalance.Value.Payable,
                                    Math.Abs(_serviceTypeBalance.Value.Act),
                                    Math.Abs(_serviceTypeBalance.Value.Payment),
                                    _serviceTypeBalance.Value.Debt,
                                    Math.Abs(_serviceTypeBalance.Value.PaymentOnCreateDate),
                                    Math.Abs(_serviceTypeBalance.Value.PaymentOnEnterPeriod));
                            }

                            _yearBalances.Balances.Clear();
                            _groupNumber++;
                        }

                        // Заполнение таблицы балансов по типам услуг за один отчет
                        if (i + 1 == _periodBalances.Length || _periodBalances[i + 1].Period != _periodBalance.Period.AddMonths(1))
                        {
                            _lastReportRow["Till"] = $"{_periodBalance.Period:MMMM yyyy}";

                            foreach (KeyValuePair<ServiceBalanceKey, Balance> _serviceTypeBalance in _reportBalances.Balances)
                            {
                                _posesTable.Rows.Add(
                                    _reportNumber,
                                    "Итого",
                                    _groupNumber,
                                    _serviceTypeBalance.Key.Name,
                                    _serviceTypeBalance.Value.Charge,
                                    Math.Abs(_serviceTypeBalance.Value.Benefit),
                                    _serviceTypeBalance.Value.Recharge,
                                    _serviceTypeBalance.Value.Payable,
                                    Math.Abs(_serviceTypeBalance.Value.Act),
                                    Math.Abs(_serviceTypeBalance.Value.Payment),
                                    _serviceTypeBalance.Value.Debt,
                                    Math.Abs(_serviceTypeBalance.Value.PaymentOnCreateDate),
                                    Math.Abs(_serviceTypeBalance.Value.PaymentOnEnterPeriod));
                            }

                            _reportBalances.Balances.Clear();
                            _groupNumber++;
                        }

                        _previousPeriod = _periodBalance.Period;
                    }
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite(string.Format("Mutual settlement error: {0}", _ex));
            }

            return null;
        }       
    }
}