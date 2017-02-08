using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Server.PrintForms.DataSets;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report
{
    /// <summary>
    /// Презентер вида с отчетом
    /// </summary>
    public class ReportViewPresenter : BaseReportForReportObjectPresenter<IReportView, EmptyReportParams>
    {
        private class ServiceBalances
        {
            public IDictionary<Key, Balance> Balances
            {
                set;
                get;
            }

            public ServiceBalances()
            {
                Balances = new Dictionary<Key, Balance>();
            }

            public void Add(Key key, Balance value)
            {
                if (!Balances.ContainsKey(key))
                {
                    Balances.Add(key, new Balance());
                }

                Balances[key].Add(value);
            }
        }

        private class Key
        {
            public int ID
            {
                set;
                get;
            }

            public string Name
            {
                set;
                get;
            }

            public override int GetHashCode()
            {
                return ID.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj != null && ((Key)obj).ID == ID;
            }
        }

        private class Balance
        {
            public decimal Charge
            {
                set;
                get;
            }

            public decimal Benefit
            {
                set;
                get;
            }

            public decimal Recharge
            {
                set;
                get;
            }

            public decimal Payable
            {
                set;
                get;
            }

            public decimal Payment
            {
                set;
                get;
            }

            public decimal Debt
            {
                set;
                get;
            }

            public void Add(Balance value)
            {
                Charge += value.Charge;
                Benefit += value.Benefit;
                Recharge += value.Recharge;
                Payable += value.Payable;
                Payment += value.Payment;
                Debt += value.Debt;
            }
        }

        /// <summary>
        /// Данные
        /// </summary>
        private MutualSettlementDataSet _data;

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            View.UpdateReport();
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
                int _customerId = int.Parse(WorkItem.State[ModuleStateNames.START_UP_PARAMS_CUSTOMER_ID].ToString());

                using (Entities _entities = new Entities())
                {
                    _entities.CommandTimeout = 3600;

                    #region Запросы

                    var _periodBalances =
                        _entities.ChargeOperPoses
                            .Select(
                                c =>
                                new
                                {
                                    CustomerID = c.ChargeOpers.Customers.ID,
                                    c.ChargeOpers.ChargeSets.Period,
                                    ServiceTypeID = c.Services.ServiceTypes.ID,
                                    Charge = c.Value,
                                    Recharge = (decimal)0,
                                    Benefit = (decimal)0,
                                    Payment = (decimal)0
                                })
                            .Concat(
                                _entities.RechargeOperPoses
                                    .Select(
                                        c =>
                                        new
                                        {
                                            CustomerID = c.RechargeOpers.Customers.ID,
                                            c.RechargeOpers.RechargeSets.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = c.Value,
                                            Benefit = (decimal)0,
                                            Payment = (decimal)0
                                        }))
                            .Concat(
                                _entities.ChargeOperPoses
                                    .Where(c => c.ChargeOpers.ChargeCorrectionOpers != null)
                                    .Select(
                                        c =>
                                        new
                                        {
                                            CustomerID = c.ChargeOpers.Customers.ID,
                                            c.ChargeOpers.ChargeCorrectionOpers.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = -1 * c.Value,
                                            Benefit = (decimal)0,
                                            Payment = (decimal)0
                                        }))
                            .Concat(
                                _entities.RechargeOperPoses
                                    .Where(r => r.RechargeOpers.ChildChargeCorrectionOpers != null)
                                    .Select(
                                        c =>
                                        new
                                        {
                                            CustomerID = c.RechargeOpers.Customers.ID,
                                            c.RechargeOpers.ChildChargeCorrectionOpers.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = -1 * c.Value,
                                            Benefit = (decimal)0,
                                            Payment = (decimal)0
                                        }))
                            .Concat(
                                _entities.BenefitOperPoses
                                    .Select(
                                        c =>
                                        new
                                        {
                                            CustomerID = c.BenefitOpers.ChargeOpers.Customers.ID,
                                            c.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Benefit = c.Value,
                                            Payment = (decimal)0
                                        }))
                            .Concat(
                                _entities.RebenefitOperPoses
                                    .Select(
                                        c =>
                                        new
                                        {
                                            CustomerID = c.RebenefitOpers.RechargeOpers.Customers.ID,
                                            c.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Benefit = c.Value,
                                            Payment = (decimal)0
                                        }))
                            .Concat(
                                _entities.BenefitOperPoses
                                    .Where(c => c.BenefitOpers.BenefitCorrectionOpers != null)
                                    .Select(
                                        c =>
                                        new
                                        {
                                            CustomerID = c.BenefitOpers.ChargeOpers.Customers.ID,
                                            c.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Benefit = -1 * c.Value,
                                            Payment = (decimal)0
                                        }))
                            .Concat(
                                _entities.RebenefitOperPoses
                                    .Where(r => r.RebenefitOpers.BenefitCorrectionOpers != null)
                                    .Select(c =>
                                        new
                                        {
                                            CustomerID = c.RebenefitOpers.RechargeOpers.Customers.ID,
                                            c.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Benefit = -1 * c.Value,
                                            Payment = (decimal)0
                                        }))
                            .Concat(
                                _entities.PaymentOperPoses
                                    .Select(c =>
                                        new
                                        {
                                            CustomerID = c.PaymentOpers.Customers.ID,
                                            Period = c.PaymentOpers.CreationDateTime,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Benefit = (decimal)0,
                                            Payment = c.Value
                                        }))
                            .Concat(
                                _entities.PaymentCorrectionOperPoses
                                    .Select(c =>
                                        new
                                        {
                                            CustomerID = c.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                                            c.PaymentCorrectionOpers.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Benefit = (decimal)0,
                                            Payment = c.Value
                                        }))
                            .Where(c => c.CustomerID == _customerId)
                            .GroupBy(c =>
                                new
                                {
                                    c.Period.Year,
                                    c.Period.Month,
                                    c.ServiceTypeID
                                })
                            .Select(g =>
                                new
                                {
                                    g.Key.Year,
                                    g.Key.Month,
                                    g.Key.ServiceTypeID,
                                    Charge = g.Sum(c => c.Charge),
                                    Recharge = g.Sum(c => c.Recharge),
                                    Benefit = g.Sum(c => c.Benefit),
                                    Payment = g.Sum(c => c.Payment)
                                })
                            .ToList()
                            .Join(
                                _entities.ServiceTypes
                                    .Select(s =>
                                        new
                                        {
                                            s.ID,
                                            s.Name
                                        })
                                    .ToList(),
                                c => c.ServiceTypeID,
                                s => s.ID,
                                (c, s) =>
                                new
                                {
                                    c.Year,
                                    c.Month,
                                    c.ServiceTypeID,
                                    ServiceTypeName = s.Name,
                                    c.Charge,
                                    c.Recharge,
                                    c.Benefit,
                                    c.Payment,   
                                })
                            .GroupBy(c => new { c.Year, c.Month })
                            .Select(g =>
                                new
                                {
                                    Period = new DateTime(g.Key.Year, g.Key.Month, 1),
                                    ServiceBalances = 
                                        new ServiceBalances
                                        {
                                            Balances = g
                                                .GroupBy(c =>
                                                    new
                                                    {
                                                        c.ServiceTypeID,
                                                        c.ServiceTypeName
                                                    })
                                                    .Select(gs =>
                                                        new
                                                        {
                                                            gs.Key.ServiceTypeID,
                                                            gs.Key.ServiceTypeName,
                                                            Charge = gs.Sum(c => c.Charge),
                                                            Recharge = gs.Sum(c => c.Recharge),
                                                            Benefit = gs.Sum(c => c.Benefit),
                                                            Payment = gs.Sum(c => c.Payment)
                                                        })
                                                    .OrderBy(sb => sb.ServiceTypeName)
                                                    .ToDictionary(
                                                        sb =>
                                                        new Key
                                                        {
                                                            ID = sb.ServiceTypeID,
                                                            Name = sb.ServiceTypeName
                                                        },
                                                        sb =>
                                                        new Balance
                                                        {
                                                            Charge = sb.Charge,
                                                            Benefit = sb.Benefit,
                                                            Recharge = sb.Recharge,
                                                            Payable = sb.Charge + sb.Benefit + sb.Recharge,
                                                            Payment = sb.Payment,
                                                            Debt = sb.Charge + sb.Benefit + sb.Recharge + sb.Payment
                                                        })
                                        }
                                })
                            .OrderBy(c => c.Period)
                            .ToArray();

                    #endregion
                    
                    int _reportNumber = 0;
                    int _groupNumber = 0;
                    DataRow _lastReportRow = null;
                    DateTime _previousPeriod = DateTime.MinValue;
                    DateTime _now = ServerTime.GetDateTimeInfo().Now;
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
                            _lastReportRow = _settlementTable.Rows.Add(
                                ++_reportNumber,
                                _previousPeriod != DateTime.MinValue
                                    ? string.Format("{0} - ", _periodBalance.Period.ToString("MMMM yyyy"))
                                    : "по ",
                                _now,
                                _customer.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson
                                    ? _customer.JuridicalPersonFullName
                                    : _customer.PhysicalPersonShortName,
                                string.Format("ул. {0}, {1}, кв. {2}",
                                     _customer.Buildings.Streets.Name,
                                     _customer.Buildings.Number,
                                     _customer.Apartment),
                                string.Format("{0} кв.м.", _customer.Square),
                                _customer.Residents.Count(),
                                UserHolder.User.Aka);
                        }

                        // Заполнение таблицы балансов по типам услуг за месяц
                        foreach (KeyValuePair<Key, Balance> _serviceTypeBalance in _periodBalance.ServiceBalances.Balances)
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
                                Math.Abs(_serviceTypeBalance.Value.Payment),
                                _serviceTypeBalance.Value.Debt);
                        }

                        _groupNumber++;

                        // Заполенение таблицы балансов по типа услуг за год
                        if (i + 1 == _periodBalances.Length || _periodBalances[i + 1].Period.Year != _periodBalance.Period.Year)
                        {
                            foreach (KeyValuePair<Key, Balance> _serviceTypeBalance in _yearBalances.Balances)
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
                                    Math.Abs(_serviceTypeBalance.Value.Payment),
                                    _serviceTypeBalance.Value.Debt);
                            }

                            _yearBalances.Balances.Clear();
                            _groupNumber++;
                        }

                        // Заполнение таблицы балансов по типам услуг за один отчет
                        if (i + 1 == _periodBalances.Length || _periodBalances[i + 1].Period != _periodBalance.Period.AddMonths(1))
                        {
                            _lastReportRow["SinceTillPeriods"] = 
                                string.Format("{0}{1:MMMM yyyy}", _lastReportRow["SinceTillPeriods"], _periodBalance.Period);

                            foreach (KeyValuePair<Key, Balance> _serviceTypeBalance in _reportBalances.Balances)
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
                                    Math.Abs(_serviceTypeBalance.Value.Payment),
                                    _serviceTypeBalance.Value.Debt);
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