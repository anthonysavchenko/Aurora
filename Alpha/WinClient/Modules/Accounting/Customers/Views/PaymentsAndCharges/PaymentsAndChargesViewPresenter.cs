using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Infrastructure.Interface.Constants;

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
                                OperationType = (int)OperationType.Charge,
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
                                        OperationType = (int)OperationType.Recharge,
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
                                        OperationType = (int)OperationType.ChargeCorrection,
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
                                        OperationType = (int)OperationType.RechargeCorrection,
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
                                        OperationType = (int)OperationType.Benefit,
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
                                        OperationType = (int)OperationType.Rebenefit,
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
                                        OperationType = (int)OperationType.BenefitCorrection,
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
                                        OperationType = (int)OperationType.RebenefitCorrection,
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
                                            OperationType = p.PaymentSets.Intermediaries != null ? (int)OperationType.Payment : (int)OperationType.Act,
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
                                            OperationType = p.PaymentOpers.PaymentSets.Intermediaries != null ? (int)OperationType.PaymentCorrection : (int)OperationType.ActCorrection,
                                            Value = p.Value
                                        }))
                        .OrderBy(c => c.CreationDateTime)
                        .ToList();

                Parallel.ForEach(
                    _operations,
                    o =>
                    {
                        if (o.OperationType == (int)OperationType.Payment || o.OperationType == (int)OperationType.Act)
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
                                Charged = g.Sum(o => o.OperationType == (int)OperationType.Charge ? o.Value : 0),
                                Benefit =
                                    -1 * g.Sum(
                                        o =>
                                        o.OperationType == (int)OperationType.Benefit ||
                                        o.OperationType == (int)OperationType.BenefitCorrection ||
                                        o.OperationType == (int)OperationType.Rebenefit ||
                                        o.OperationType == (int)OperationType.RebenefitCorrection
                                            ? o.Value : 0),
                                Recharged =
                                    g.Sum(
                                        o =>
                                        o.OperationType == (int)OperationType.Recharge ||
                                        o.OperationType == (int)OperationType.ChargeCorrection ||
                                        o.OperationType == (int)OperationType.RechargeCorrection
                                            ? o.Value : 0),
                                Payed =
                                    -1 * g.Sum(
                                        o =>
                                        o.OperationType == (int)OperationType.Payment ||
                                        o.OperationType == (int)OperationType.PaymentCorrection
                                            ? o.Value : 0),
                                Acts =
                                    -1 * g.Sum(
                                        o =>
                                        o.OperationType == (int)OperationType.Act ||
                                        o.OperationType == (int)OperationType.ActCorrection
                                            ? o.Value : 0),
                                Balance = g.Sum(o => o.Value)
                            })
                        .OrderBy(o => o.Period)
                        .ToList();

                foreach (var _operation in _operations)
                {
                    string _operatinCaption = String.Empty;

                    switch ((OperationType)_operation.OperationType)
                    {
                        case OperationType.Charge:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_CHARGE;
                            break;
                        case OperationType.Payment:
                        case OperationType.Act:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_PAYMENT;
                            break;
                        case OperationType.PaymentCorrection:
                        case OperationType.ActCorrection:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_PAYMENT_CORRECTION;
                            break;
                        case OperationType.Benefit:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_BENEFIT;
                            break;
                        case OperationType.ChargeCorrection:
                        case OperationType.RechargeCorrection:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_CHARGE_CORRECTION;
                            break;
                        case OperationType.BenefitCorrection:
                        case OperationType.RebenefitCorrection:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_BENEFIT_CORRECTION;
                            break;
                        case OperationType.Recharge:
                            _operatinCaption = ModuleUIExtensionSiteNames.OPERATION_CAPTION_RECHARGE;
                            break;
                        case OperationType.Rebenefit:
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
                        _period.Acts,
                        _period.Recharged,
                        _payable,
                        _period.Payed,
                        _payable - _period.Payed - _period.Acts,
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
            switch ((OperationType)operationType)
            {
                case OperationType.Payment:
                case OperationType.PaymentCorrection:
                case OperationType.Act:
                case OperationType.ActCorrection:
                    WorkItem.Controller.RunUsecase(
                            ApplicationUsecaseNames.PAYMENTS,
                            new ShowDetailsStartUpParams<PaymentOper>(GetItem<PaymentOper>(operationID.ToString())));
                    break;

                case OperationType.Charge:
                case OperationType.ChargeCorrection:
                case OperationType.Benefit:
                case OperationType.BenefitCorrection:
                    WorkItem.Controller.RunUsecase(
                            ApplicationUsecaseNames.CHARGES,
                            new ShowDetailsStartUpParams<ChargeOper>(GetItem<ChargeOper>(operationID.ToString())));
                    break;

                case OperationType.Recharge:
                case OperationType.Rebenefit:
                case OperationType.RechargeCorrection:
                case OperationType.RebenefitCorrection:
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
            if (int.Parse(UserHolder.User.ID) != 2)
            {
                return;
            }

            string _customerId = ((Customer)WorkItem.State[CommonStateNames.CurrentItem]).ID;

            WorkItem.Controller.RunUsecase(
                ApplicationUsecaseNames.MUTUAL_SETTLEMENT,
                new PrintDocStartUpParams(_customerId));
        }
    }
}