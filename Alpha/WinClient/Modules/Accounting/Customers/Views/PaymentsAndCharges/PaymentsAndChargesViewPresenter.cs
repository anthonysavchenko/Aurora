﻿using System.Threading.Tasks;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Infrastructure.Interface.Constants;
using OperTypes = Taumis.Alpha.Infrastructure.Interface.Enums.OperationTypes;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.PaymentsAndCharges
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class PaymentsAndChargesViewPresenter : BaseSimpleListViewPresenter<IBaseSimpleListView, ChargeOper>
    {
        /// <summary>
        /// Класс общих данных операций
        /// </summary>
        private class Operation
        {
            public int LinkOperationID { get; set; }
            public DateTime Period { get; set; }
            public DateTime CreationDateTime { get; set; }
            public int OperationType { get; set; }
            public decimal Value { get; set; }
        }

        /// <summary>
        /// На загрузку вью
        /// </summary>
        public override void OnViewReady()
        {
            RefreshRefBooks();
        }

        /// <summary>
        /// Получить таблицу данных (DataTable) со списком объектов 
        /// для типа домена, указанного в параметре TBusiness класса.
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            int _customerID = int.Parse(WorkItem.State[CommonStateNames.CurrentItemId].ToString());

            DataTable _table = new DataTable();
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_PERIOD, typeof(DateTime));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_OPENING_BALANCE, typeof(decimal));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_CHARGED, typeof(decimal));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_BENEFIT, typeof(decimal));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_ACT, typeof(decimal));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_RECHARGED, typeof(decimal));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_PAYABLE, typeof(decimal));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_PAYED, typeof(decimal));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_DEBT, typeof(decimal));
            _table.Columns.Add(PaymentAndChargesColumnNames.COLUMN_CLOSING_BALANCE, typeof(decimal));

            DataTable _subTable = new DataTable();
            _subTable.Columns.Add(PaymentAndChargesColumnNames.COLUMN_OPERATION_NAME, typeof(string));
            _subTable.Columns.Add(PaymentAndChargesColumnNames.COLUMN_LINK_OPERATION_ID, typeof(int));
            _subTable.Columns.Add(PaymentAndChargesColumnNames.COLUMN_OPERATION_TYPE, typeof(int));
            _subTable.Columns.Add(PaymentAndChargesColumnNames.COLUMN_DATE_CREATED, typeof(DateTime));
            _subTable.Columns.Add(PaymentAndChargesColumnNames.COLUMN_PERIOD_CREATED, typeof(DateTime));
            _subTable.Columns.Add(PaymentAndChargesColumnNames.COLUMN_VALUE, typeof(decimal));

            DataSet _dataSet = new DataSet();
            _dataSet.Tables.Add(_table);
            _dataSet.Tables.Add(_subTable);

            using (Entities _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                #region Запрос данных по всем операциям

                var _operations =
                    _entities.ChargeOpers
                        .Where(c => c.Customers.ID == _customerID)
                        .Select(
                            c =>
                            new Operation
                            {
                                LinkOperationID = c.ID,
                                Period = c.ChargeSets.Period,
                                CreationDateTime = c.CreationDateTime,
                                OperationType = (int)OperTypes.Charge,
                                Value = c.Value
                            })
                        .Concat(
                            _entities.RechargeOpers
                                .Where(r => r.Customers.ID == _customerID)
                                .Select(
                                    r =>
                                    new Operation
                                    {
                                        LinkOperationID = r.ID,
                                        Period = r.RechargeSets.Period,
                                        CreationDateTime = r.CreationDateTime,
                                        OperationType = (int)OperTypes.Recharge,
                                        Value = r.Value
                                    }))
                        .Concat(
                            _entities.ChargeOpers
                                .Where(c => c.ChargeCorrectionOpers != null && c.Customers.ID == _customerID)
                                .Select(
                                    c =>
                                    new Operation
                                    {
                                        LinkOperationID = c.ID,
                                        Period = c.ChargeCorrectionOpers.Period,
                                        CreationDateTime = c.ChargeCorrectionOpers.CreationDateTime,
                                        OperationType = (int)OperTypes.ChargeCorrection,
                                        Value = -1 * c.Value
                                    }))
                        .Concat(
                            _entities.RechargeOpers
                                .Where(r => r.ChildChargeCorrectionOpers != null && r.Customers.ID == _customerID)
                                .Select(
                                    r =>
                                    new Operation
                                    {
                                        LinkOperationID = r.ID,
                                        Period = r.ChildChargeCorrectionOpers.Period,
                                        CreationDateTime = r.ChildChargeCorrectionOpers.CreationDateTime,
                                        OperationType = (int)OperTypes.RechargeCorrection,
                                        Value = -1 * r.Value
                                    }))
                        .Concat(
                            _entities.BenefitOpers
                                .Where(b => b.ChargeOpers.Customers.ID == _customerID)
                                .Select(
                                    b =>
                                    new Operation
                                    {
                                        LinkOperationID = b.ChargeOpers.ID,
                                        Period = b.ChargeOpers.ChargeSets.Period,
                                        CreationDateTime = b.ChargeOpers.CreationDateTime,
                                        OperationType = (int)OperTypes.Benefit,
                                        Value = b.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Where(r => r.RechargeOpers.Customers.ID == _customerID)
                                .Select(
                                    r =>
                                    new Operation
                                    {
                                        LinkOperationID = r.RechargeOpers.ID,
                                        Period = r.RechargeOpers.RechargeSets.Period,
                                        CreationDateTime = r.RechargeOpers.CreationDateTime,
                                        OperationType = (int)OperTypes.Rebenefit,
                                        Value = r.Value
                                    }))
                        .Concat(
                            _entities.BenefitOpers
                                .Where(c => c.BenefitCorrectionOpers != null && c.ChargeOpers.Customers.ID == _customerID)
                                .Select(
                                    c =>
                                    new Operation
                                    {
                                        LinkOperationID = c.ChargeOpers.ID,
                                        Period = c.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        CreationDateTime = c.BenefitCorrectionOpers.ChargeCorrectionOpers.CreationDateTime,
                                        OperationType = (int)OperTypes.BenefitCorrection,
                                        Value = -1 * c.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Where(r => r.BenefitCorrectionOpers != null && r.RechargeOpers.Customers.ID == _customerID)
                                .Select(
                                    r =>
                                    new Operation
                                    {
                                        LinkOperationID = r.RechargeOpers.ID,
                                        Period = r.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        CreationDateTime = r.BenefitCorrectionOpers.ChargeCorrectionOpers.CreationDateTime,
                                        OperationType = (int)OperTypes.RebenefitCorrection,
                                        Value = -1 * r.Value
                                    }))
                        .Concat(
                            _entities.PaymentOpers
                                .Where(p => p.Customers.ID == _customerID)
                                .Select(
                                    p =>
                                        new Operation
                                        {
                                            LinkOperationID = p.ID,
                                            Period = p.CreationDateTime,
                                            CreationDateTime = p.CreationDateTime,
                                            OperationType = (int)OperTypes.Payment,
                                            Value = p.Value
                                        }))
                        .Concat(
                            _entities.PaymentCorrectionOpers
                                .Where(p => p.PaymentOpers.Customers.ID == _customerID)
                                .Select(
                                    p =>
                                        new Operation
                                        {
                                            LinkOperationID = p.PaymentOpers.ID,
                                            Period = p.Period,
                                            CreationDateTime = p.CreationDateTime,
                                            OperationType = (int)OperTypes.PaymentCorrection,
                                            Value = p.Value
                                        }))
                        .OrderBy(c => c.CreationDateTime)
                        .ToList();

                Parallel.ForEach(
                    _operations,
                    o =>
                    {
                        if (o.OperationType == (int)OperTypes.Payment)
                            o.Period = new DateTime(o.Period.Year, o.Period.Month, 1);
                    });

                #endregion

                var _periods =
                    _operations
                        .GroupBy(o => o.Period)
                        .Select(
                            g =>
                            new
                            {
                                Period = g.Key,
                                Charged = g.Sum(o => o.OperationType == (int)OperTypes.Charge ? o.Value : 0),
                                Benefit =
                                    -1 * g.Sum(
                                        o =>
                                        o.OperationType == (int)OperTypes.Benefit ||
                                        o.OperationType == (int)OperTypes.BenefitCorrection ||
                                        o.OperationType == (int)OperTypes.Rebenefit
                                            ? o.Value : 0),
                                Recharged =
                                    g.Sum(
                                        o =>
                                        o.OperationType == (int)OperTypes.Recharge ||
                                        o.OperationType == (int)OperTypes.ChargeCorrection ||
                                        o.OperationType == (int)OperTypes.RechargeCorrection
                                            ? o.Value : 0),
                                Payed =
                                    -1 * g.Sum(
                                        o =>
                                        o.OperationType == (int)OperTypes.Payment ||
                                        o.OperationType == (int)OperTypes.PaymentCorrection
                                            ? o.Value : 0),
                                Balance = g.Sum(o => o.Value)
                            })
                        .OrderBy(o => o.Period)
                        .ToList();

                foreach (var _operation in _operations)
                {
                    string _operatinCaption = String.Empty;

                    switch ((OperTypes)_operation.OperationType)
                    {
                        case OperTypes.Charge:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_CHARGE;
                            break;
                        case OperTypes.Payment:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_PAYMENT;
                            break;
                        case OperTypes.PaymentCorrection:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_PAYMENT_CORRECTION;
                            break;
                        case OperTypes.Benefit:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_BENEFIT;
                            break;
                        case OperTypes.ChargeCorrection:
                        case OperTypes.RechargeCorrection:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_CHARGE_CORRECTION;
                            break;
                        case OperTypes.BenefitCorrection:
                        case OperTypes.RebenefitCorrection:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_BENEFIT_CORRECTION;
                            break;
                        case OperTypes.Recharge:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_RECHARGE;
                            break;
                        case OperTypes.Rebenefit:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_REBENEFIT;
                            break;
                    }

                    if (!String.IsNullOrEmpty(_operatinCaption))
                    {
                        _subTable.Rows.Add(
                            _operatinCaption,
                            _operation.LinkOperationID,
                            _operation.OperationType,
                            _operation.CreationDateTime,
                            _operation.Period,
                            Math.Abs(_operation.Value));
                    }
                }

                decimal _balance = 0;

                foreach (var _period in _periods)
                {
                    decimal _payable = _period.Charged + _period.Recharged - _period.Benefit;

                    _table.Rows.Add(
                        _period.Period,
                        _balance,
                        _period.Charged,
                        _period.Benefit,
                        0,
                        _period.Recharged,
                        _payable,
                        _period.Payed,
                        _payable - _period.Payed,
                        _balance += _period.Balance);
                }

                DataColumn[] keyColumn = new DataColumn[] { _dataSet.Tables[0].Columns[PaymentAndChargesColumnNames.COLUMN_PERIOD] };
                DataColumn[] foreignKeyColumn = new DataColumn[] { _dataSet.Tables[1].Columns[PaymentAndChargesColumnNames.COLUMN_PERIOD_CREATED] };
                _dataSet.Relations.Add(PaymentAndChargesColumnNames.RELATION_PERIOD, keyColumn, foreignKeyColumn);
            }

            return _dataSet.Tables[0];
        }

        /// <summary>
        /// Обрабатывает нажатие на ссылку в названии операции
        /// </summary>
        /// <param name="operationID">ID операции</param>
        /// <param name="operationType">Тип операции</param>
        public void SelectOperationLink(int operationID, int operationType)
        {
            switch ((OperTypes)operationType)
            {
                case OperTypes.Payment:
                case OperTypes.PaymentCorrection:
                    WorkItem.Controller.RunUsecase(
                            ApplicationUsecaseNames.PAYMENTS,
                            new ShowDetailsStartUpParams<PaymentOper>(GetItem<PaymentOper>(operationID.ToString())));
                    break;

                case OperTypes.Charge:
                case OperTypes.ChargeCorrection:
                case OperTypes.Benefit:
                case OperTypes.BenefitCorrection:
                    WorkItem.Controller.RunUsecase(
                            ApplicationUsecaseNames.CHARGES,
                            new ShowDetailsStartUpParams<ChargeOper>(GetItem<ChargeOper>(operationID.ToString())));
                    break;

                case OperTypes.Recharge:
                case OperTypes.Rebenefit:
                case OperTypes.RechargeCorrection:
                case OperTypes.RebenefitCorrection:
                    WorkItem.Controller.RunUsecase(
                            ApplicationUsecaseNames.CHARGES,
                            new ShowDetailsStartUpParams<RechargeOper>(GetItem<RechargeOper>(operationID.ToString())));
                    break;
            }
        }

        /// <summary>
        /// Обрабатывает нажатие на ссылку по созданию квитанции на доплату
        /// </summary>
        public void SelectTotalBillLink()
        {
            WorkItem.Controller.RunUsecase(
                ApplicationUsecaseNames.BILLS,
                new CreateNewItemStartUpParams(((Customer)WorkItem.State[CommonStateNames.CurrentItem]).Account));
        }

        /// <summary>
        /// Обработчик глобального события "Печать"
        /// </summary>
        [EventSubscription(ModuleEventNames.PRINT_PAYMENTS_AND_CHARGES, ThreadOption.UserInterface)]
        public void PrintItemFired(object sender, EventArgs eventArgs)
        {
            string _customerId = ((Customer)WorkItem.State[CommonStateNames.CurrentItem]).ID;

            WorkItem.Controller.RunUsecase(
                ApplicationUsecaseNames.MUTUAL_SETTLEMENT,
                new PrintDocStartUpParams(_customerId));
        }
    }
}