using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.DebtAgency.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private static class ColumnNames
        {
            public const string ACCOUNT = "account";
            public const string ADDRESS = "address";
            public const string PREV_PERIOD_TOTAL = "prevPeriodTotal";
            public const string CUR_PERIOD_TOTAL = "curPeriodTotal";
            public const string OLD_DEBTS = "oldDebts";
            public const string DEBT_AGENCY_TOTAL = "debtAgencyTotal";
            public const string WO_INTERMEDIARY_PAYMENTS = "woIntermediaryPayments";
        }

        private readonly Dictionary<string, string> _serviceTypesDictionary = new Dictionary<string, string>();

        public override void OnViewReady()
        {
            base.OnViewReady();

            View.Period = ServerTime.GetPeriodInfo().LastCharged;
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            DateTime _oldDebtPeriod = new DateTime(2012, 8, 1);

            DataTable _table = new DataTable();
            _table.Columns.Add(ColumnNames.ACCOUNT, typeof(string));
            _table.Columns.Add(ColumnNames.ADDRESS, typeof(string));
            _table.Columns.Add(ColumnNames.OLD_DEBTS, typeof(decimal));
            _table.Columns.Add(ColumnNames.PREV_PERIOD_TOTAL, typeof(decimal));
            _table.Columns.Add(ColumnNames.CUR_PERIOD_TOTAL, typeof(decimal));
            _table.Columns.Add(ColumnNames.DEBT_AGENCY_TOTAL, typeof(decimal));
            _table.Columns.Add(ColumnNames.WO_INTERMEDIARY_PAYMENTS, typeof(decimal));

            DateTime _period = View.Period;
            _period = new DateTime(_period.Year, _period.Month, 1);

            using (var _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                DateTime _periodChargesCreationDateTime = 
                    _entities.ChargeSets
                        .Where(c => c.Period == _period)
                        .Select(c => c.CreationDateTime)
                        .First();

                #region Raw Oper Query

                var _raw =
                    _entities.ChargeOpers
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.Customers.ID,
                                c.CreationDateTime,
                                c.ChargeSets.Period,
                                c.Value
                            })
                        .Concat(
                            _entities.ChargeOpers
                                .Where(c => c.ChargeCorrectionOpers != null)
                                .Select(
                                    c =>
                                    new
                                    {
                                        CustomerID = c.Customers.ID,
                                        c.ChargeCorrectionOpers.CreationDateTime,
                                        c.ChargeCorrectionOpers.Period,
                                        Value = -1 * c.Value
                                    }))
                        .Concat(
                            _entities.RechargeOpers
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.Customers.ID,
                                        r.CreationDateTime,
                                        r.RechargeSets.Period,
                                        r.Value
                                    }))
                        .Concat(
                            _entities.RechargeOpers
                                .Where(r => r.ChildChargeCorrectionOpers != null)
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.Customers.ID,
                                        r.ChildChargeCorrectionOpers.CreationDateTime,
                                        r.ChildChargeCorrectionOpers.Period,
                                        Value = -1 * r.Value
                                    }))
                        .Concat(
                            _entities.BenefitOpers
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.ChargeOpers.Customers.ID,
                                        b.ChargeOpers.CreationDateTime,
                                        b.ChargeOpers.ChargeSets.Period,
                                        b.Value
                                    }))
                        .Concat(
                            _entities.BenefitOpers
                                .Where(b => b.BenefitCorrectionOpers != null)
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.ChargeOpers.Customers.ID,
                                        b.BenefitCorrectionOpers.ChargeCorrectionOpers.CreationDateTime,
                                        b.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        Value = -1 * b.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.RechargeOpers.Customers.ID,
                                        r.RechargeOpers.CreationDateTime,
                                        r.RechargeOpers.RechargeSets.Period,
                                        r.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Where(r => r.BenefitCorrectionOpers != null)
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.RechargeOpers.Customers.ID,
                                        r.BenefitCorrectionOpers.ChargeCorrectionOpers.CreationDateTime,
                                        r.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        Value = -1 * r.Value
                                    }))
                        .Concat(
                            _entities.OverpaymentOperPoses
                                .Select(
                                    o =>
                                    new
                                    {
                                        CustomerID = o.OverpaymentOpers.Customers.ID,
                                        o.OverpaymentOpers.CreationDateTime,
                                        o.Period,
                                        o.Value
                                    }))
                        .Concat(
                            _entities.OverpaymentCorrectionOpers
                                .Select(
                                    o =>
                                    new
                                    {
                                        CustomerID = o.ChargeOpers.Customers.ID,
                                        o.ChargeOpers.CreationDateTime,
                                        o.Period,
                                        o.Value
                                    }))
                        .Concat(
                            _entities.PaymentOperPoses
                                .Select(p =>
                                    new
                                    {
                                        CustomerID = p.PaymentOpers.Customers.ID,
                                        CreationDateTime = p.PaymentOpers.PaymentSets.PaymentDate,
                                        p.Period,
                                        p.Value
                                    }))
                        .Concat(
                            _entities.PaymentCorrectionOpers
                                .Select(p =>
                                    new
                                    {
                                        CustomerID = p.PaymentOpers.Customers.ID,
                                        CreationDateTime = p.PaymentOpers.PaymentSets.PaymentDate,
                                        p.Period,
                                        p.Value
                                    })); 

                #endregion

                //Старые долги
                var _oldDebts = _raw
                    .Where(r => r.Period == _oldDebtPeriod)
                    .GroupBy(r => r.CustomerID)
                    .Select(g =>
                        new
                        {
                            CustomerID = g.Key,
                            Value = g.Sum(r => r.Value)
                        })
                    .Where(x => x.Value != 0)
                    .ToDictionary(x => x.CustomerID, x => x.Value);

                //Было
                var _prevPeriodTotal = _raw
                    .Where(r => r.Period > _oldDebtPeriod && r.Period <= _period && r.CreationDateTime <= _periodChargesCreationDateTime)
                    .GroupBy(r => r.CustomerID)
                    .Select(g =>
                        new
                        {
                            CustomerID = g.Key,
                            Value = g.Sum(r => r.Value)
                        })
                    .Where(x => x.Value != 0)
                    .ToDictionary(x => x.CustomerID, x => x.Value);

                DateTime _leftCollectPeriod = new DateTime(_period.Year, _period.Month, 10);
                DateTime _rightCollectPeriod = _period.AddMonths(1);
                _rightCollectPeriod = new DateTime(_rightCollectPeriod.Year, _rightCollectPeriod.Month, 10);

                //Стало
                var _curPeriodTotal = _raw
                    .Where(r => r.Period > _oldDebtPeriod && r.Period <= _period && r.CreationDateTime <= _rightCollectPeriod)
                    .GroupBy(r => r.CustomerID)
                    .Select(g =>
                        new
                        {
                            CustomerID = g.Key,
                            Value = g.Sum(r => r.Value)
                        })
                    .Where(x => x.Value != 0)
                    .ToDictionary(x => x.CustomerID, x => x.Value);

                var _paymentsRaw =
                    _entities.PaymentOperPoses
                        .Select(p =>
                            new
                            {
                                CustomerID = p.PaymentOpers.Customers.ID,
                                CreationDateTime = p.PaymentOpers.PaymentSets.PaymentDate,
                                p.PaymentOpers.PaymentSets.Intermediaries,
                                p.Period,
                                p.Value
                            })
                    .Concat(
                        _entities.PaymentCorrectionOpers
                            .Select(p =>
                                new
                                {
                                    CustomerID = p.PaymentOpers.Customers.ID,
                                    CreationDateTime = p.CreationDateTime,
                                    p.PaymentOpers.PaymentSets.Intermediaries,
                                    p.Period,
                                    p.Value
                                }))
                    .Where(p => 
                        p.CreationDateTime > _leftCollectPeriod && 
                        p.CreationDateTime <= _rightCollectPeriod &&
                        p.Period > _oldDebtPeriod &&
                        p.Period < _period);

                //Платежи собранные коллекторами
                var _collectedPayments = _paymentsRaw
                    .Where(r => r.Intermediaries != null)
                    .GroupBy(r => r.CustomerID)
                    .Select(g =>
                        new
                        {
                            CustomerID = g.Key,
                            Value = g.Sum(r => r.Value)
                        })
                    .Where(x => x.Value != 0)
                    .ToDictionary(x => x.CustomerID, x => x.Value);

                //Списания
                var _woIntermediaryPayments = _paymentsRaw
                    .Where(r => r.Intermediaries == null)
                    .GroupBy(r => r.CustomerID)
                    .Select(g =>
                        new
                        {
                            CustomerID = g.Key,
                            Value = g.Sum(r => r.Value)
                        })
                    .Where(x => x.Value != 0)
                    .ToDictionary(x => x.CustomerID, x => x.Value);

                var _customers =
                    _entities.Customers
                        .Select(
                            c =>
                            new
                            {
                                c.ID,
                                StreetName = c.Buildings.Streets.Name,
                                BuildingNumber = c.Buildings.Number,
                                c.Apartment,
                                c.Account
                            })
                        .ToList()
                        .OrderBy(c => c.StreetName)
                        .ThenBy(c => c.BuildingNumber, new StringWithNumbersComparer())
                        .ThenBy(c => c.Apartment, new StringWithNumbersComparer())
                        .ToList();

                foreach (var _customer in _customers)
                {
                    if(_oldDebts.ContainsKey(_customer.ID) || 
                        _prevPeriodTotal.ContainsKey(_customer.ID) || 
                        _curPeriodTotal.ContainsKey(_customer.ID) ||
                        _collectedPayments.ContainsKey(_customer.ID) ||
                        _woIntermediaryPayments.ContainsKey(_customer.ID))
                    {
                        _table.Rows.Add(
                            _customer.Account,
                            $"{_customer.StreetName}, {_customer.BuildingNumber}, кв. {_customer.Apartment}",
                            _oldDebts.ContainsKey(_customer.ID) ? _oldDebts[_customer.ID] : 0,
                            _prevPeriodTotal.ContainsKey(_customer.ID) ? _prevPeriodTotal[_customer.ID] : 0,
                            _curPeriodTotal.ContainsKey(_customer.ID) ? _curPeriodTotal[_customer.ID] : 0,
                            _collectedPayments.ContainsKey(_customer.ID) ? Math.Abs(_collectedPayments[_customer.ID]) : 0,
                            _woIntermediaryPayments.ContainsKey(_customer.ID) ? Math.Abs(_woIntermediaryPayments[_customer.ID]) : 0);
                    }
                }
            }

            return _table;
        }
    }
}