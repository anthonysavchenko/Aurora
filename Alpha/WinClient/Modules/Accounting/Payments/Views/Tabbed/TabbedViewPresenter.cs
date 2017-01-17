using System;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.List;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Payment;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Wizard;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Tabbed
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class TabbedViewPresenter : BaseTabbedViewPresenter<ITabbedView, PaymentSet>
    {
        private bool _launchedOutside;

        /// <summary>
        /// Обрабатывает событие создания нового элемента
        /// </summary>
        protected override void OnCreateNewItem()
        {
            View.ShowWizardTab();
            View.SelectTab(TabNames.WIZARD);
            ((IWizardView)WorkItem.SmartParts.Get(ModuleViewNames.WIZARD_VIEW)).StartWizard();
        }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            View.HideWizardTab();
        }

        /// <summary>
        /// Обрабатывает событие покидания закладки
        /// </summary>
        public override void OnLeaveTabPage(string _tabPageName, out bool _cancelAction)
        {
            _cancelAction = false;
            if (_tabPageName == TabNames.WIZARD)
            {
                IWizardView _wizard = ((IWizardView)WorkItem.SmartParts.Get(ModuleViewNames.WIZARD_VIEW));
                if (_wizard.IsMasterInProgress)
                {
                    _cancelAction = true;
                }
                else
                {
                    if (!_wizard.IsMasterCompleted)
                    {
                        _cancelAction = !View.ShowQuestionDialog(@"Все несохраненные данные будут утеряны. 
Вы действительно хотите продолжить?", "Прекращение работы мастера");
                    }
                }

                if (!_cancelAction && !_wizard.IsMasterCompleted)
                {
                    _cancelAction = true;
                    _wizard.IsMasterCompleted = true;
                    View.SelectTab(TabNames.LIST);
                }
            }
            else
            {
                base.OnLeaveTabPage(_tabPageName, out _cancelAction);
            }
        }

        /// <summary>
        /// Выполняет подготовительные действия перед началом редактирования домена
        /// </summary>
        /// <param name="_cancelAction">Признак отмены действия</param>
        protected override void PrepareDomainEditing(out bool _cancelAction)
        {
            string _curId = (string)WorkItem.State[Params.CurrentItemIdStateName];

            _cancelAction = string.IsNullOrEmpty(_curId);

            if (!_cancelAction)
            {
                WorkItem.State[Params.CurrentItemStateName] = GetItem<PaymentSet>(_curId);
            }
        }

        /// <summary>
        /// Выполняет действия при входе на закладку
        /// </summary>
        /// <param name="_tabPageName">Имя закладки</param>
        /// <param name="_cancelAction">Признак отмены действия выхода с закладки</param>
        public override void OnEnterTabPage(string _tabPageName, out bool _cancelAction)
        {
            _cancelAction = false;

            if (_tabPageName != TabNames.WIZARD)
            {
                View.HideWizardTab();
            }

            switch (_tabPageName)
            {
                case TabNames.LIST:
                    ManageCommandsForPaymentSetListTab();
                    ((IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.LIST_VIEW)).RefreshList();
                    break;

                case TabNames.DETAIL:
                    if (((string)WorkItem.State[Params.LeavingTabNameStateName]) == TabNames.LIST)
                    {
                        PrepareDomainEditing(out _cancelAction);
                    }
                    if (!_cancelAction)
                    {
                        ManageCommandsForListTab();
                        IBaseListView _view = (IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.ITEM_VIEW);
                        _view.RefreshList();
                    }
                    break;

                case TabNames.PAYMENT:
                    _cancelAction =
                        !(_launchedOutside || (string)WorkItem.State[Params.LeavingTabNameStateName] == TabNames.DETAIL);

                    if (!_cancelAction)
                    {
                        string _curId = (string)WorkItem.State[ModuleStateNames.CURRENT_PAYMENT_OPER_ID];
                        _cancelAction = string.IsNullOrEmpty(_curId);
                        if (!_cancelAction)
                        {
                            IPaymentView _view = (IPaymentView)WorkItem.SmartParts.Get(ModuleViewNames.PAYMENT_VIEW);
                            _view.ShowDomainOnView();
                            ManageCommandsForPaymentDetailTab();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Изменяет доступ к глобальным командам для закладки со списком наборов платежей
        /// </summary>
        private void ManageCommandsForPaymentSetListTab()
        {
            base.ManageCommandsForListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }
        /// <summary>
        /// Изменяет доступ к глобальным командам для закладки с деталями платежа
        /// </summary>
        private void ManageCommandsForPaymentDetailTab()
        {
            base.ManageCommandsForNotListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshRefBooks].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }

        /// <summary>
        /// Изменить доступ к глобальным командам для закладки вида списка
        /// </summary>
        public override void ManageCommandsForListTab()
        {
            base.ManageCommandsForListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;
        }

        /// <summary>
        /// Открывает вкладку "Платеж" после запуска модуля "Платежи" из другого модуля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription(CommonEventNames.ON_MAIN_VIEW_SHOWN, ThreadOption.UserInterface)]
        public void ShowPaymentOperDetails(object sender, EventArgs<AnyStartUpParams> eventArgsStartUpParams)
        {
            try
            {
                ShowDetailsStartUpParams<PaymentOper> _startUpParams =
                    eventArgsStartUpParams.Data as ShowDetailsStartUpParams<PaymentOper>;

                if (_startUpParams != null)
                {
                    PaymentOper _paymentOper = _startUpParams.DomainObject;
                    ITabbedView _tabbedView = WorkItem.SmartParts.Get<ITabbedView>(ModuleViewNames.TABBED_VIEW);
                    IListView _listView = WorkItem.SmartParts.Get<IListView>(ModuleViewNames.LIST_VIEW);

                    WorkItem.State[ModuleStateNames.CURRENT_PAYMENT_OPER_ID] = _paymentOper.ID;
                    WorkItem.State[BaseListViewConstants.BaseListViewDefaultParams.CurrentItemIdStateName] =
                        _paymentOper.PaymentSet.ID;
                    WorkItem.State[Params.CurrentItemStateName] = _paymentOper.PaymentSet;

                    _listView.Since =
                        _paymentOper.PaymentSet.CreationDateTime.Date.AddHours(
                            _paymentOper.PaymentSet.CreationDateTime.Hour);
                    _listView.Till =
                        _paymentOper.PaymentSet.CreationDateTime.Date.AddHours(
                            _paymentOper.PaymentSet.CreationDateTime.Hour + 1);

                    _tabbedView.SelectTab(TabNames.LIST);
                    _launchedOutside = true;
                    _tabbedView.SelectTab(TabNames.PAYMENT);
                    _launchedOutside = false;
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Ошибка при запуске модуля \"Платежи\": {_ex}");
                View.ShowMessage("Произошла ошибка при запуске модуля", "Ошибка");
            }
            
        }
    }
}