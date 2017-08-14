using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.ChargeDetail;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.List;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Tabbed
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class TabbedViewPresenter : BaseTabbedViewPresenter<ITabbedView, PaymentSet>
    {
        private bool _launchedOutside = false;

        /// <summary>
        /// Обрабатывает событие создания нового элемента
        /// </summary>
        protected override void OnCreateNewItem()
        {
            View.ShowWizardTab();
            View.SelectTab(TabNames.WIZARD);
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
                SetCurrentItem(_curId);
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
                    ManageCommandsForChargeSetListTab();
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

                case TabNames.CHARGE_DETAIL:
                    _cancelAction =
                        !(_launchedOutside || ((string)WorkItem.State[Params.LeavingTabNameStateName]) == TabNames.DETAIL);

                    if (!_cancelAction)
                    {
                        string _curId = (string)WorkItem.State[ModuleStateNames.CURRENT_CHARGE_OPER_ID];
                        _cancelAction = string.IsNullOrEmpty(_curId);
                        if (!_cancelAction)
                        {
                            IChargeDetailView _view = (IChargeDetailView)WorkItem.SmartParts.Get(ModuleViewNames.CHARGE_DETAIL_VIEW);
                            _view.ShowDomainOnView();
                            ManageCommandsForChargeDetailTab();
                        }
                    }
                    break;

                case TabNames.WIZARD:
                    ((IWizardView)WorkItem.SmartParts.Get(ModuleViewNames.WIZARD_VIEW)).StartWizard();
                    break;
            }
        }

        private void SetCurrentItem(string id)
        {
            string _type = id.Substring(0, 1);
            string _id = id.Substring(2);
            WorkItem.State[Params.CurrentItemStateName] =
                _type == ChargeSetTypes.CHARGE_SET_TYPE
                    ? GetItem<ChargeSet>(_id)
                    : GetItem<RechargeSet>(_id);
        }

        /// <summary>
        /// Изменяет доступ к глобальным командам для закладки со списком наборов платежей
        /// </summary>
        private void ManageCommandsForChargeSetListTab()
        {
            base.ManageCommandsForListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.CreateNewItem].Status =
                CommandStatus.Enabled;

            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }
        /// <summary>
        /// Изменяет доступ к глобальным командам для закладки с деталями платежа
        /// </summary>
        private void ManageCommandsForChargeDetailTab()
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

            WorkItem.RootWorkItem.Commands[CommonCommandNames.CreateNewItem].Status =
                CommandStatus.Enabled;

            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }

        /// <summary>
        /// Открывает вкладку "Начисление" после запуска модуля "Начисления" из другого модуля
        /// </summary>
        [EventSubscription(CommonEventNames.ON_MAIN_VIEW_SHOWN, ThreadOption.UserInterface)]
        public void ShowChargeOperDetails(object sender, EventArgs<AnyStartUpParams> eventArgsStartUpParams)
        {
            try
            {
                if (eventArgsStartUpParams.Data != null)
                {
                    RechargeStartUpParams _params =
                        eventArgsStartUpParams.Data as RechargeStartUpParams;

                    if (_params != null)
                    {
                        OnCreateNewItem();
                        IWizardView _wizardView = (IWizardView)WorkItem.SmartParts.Get(ModuleViewNames.WIZARD_VIEW);
                        _wizardView.DoRecharge(_params.CustomerID, _params.Since, _params.Till);
                    }
                    else
                    {
                        string _chargeSetListId;
                        BaseChargeOper _oper;

                        ITabbedView _tabbedView = WorkItem.SmartParts.Get<ITabbedView>(ModuleViewNames.TABBED_VIEW);
                        IListView _listView = WorkItem.SmartParts.Get<IListView>(ModuleViewNames.LIST_VIEW);

                        DateTime _chargeSetTime;
                        ShowDetailsStartUpParams<ChargeOper> _detailsStartUpParams =
                            eventArgsStartUpParams.Data as ShowDetailsStartUpParams<ChargeOper>;
                        if (_detailsStartUpParams != null)
                        {
                            _oper = _detailsStartUpParams.DomainObject;
                            _chargeSetListId = $"{ChargeSetTypes.CHARGE_SET_TYPE}_{_oper.ChargeSet.ID}";
                            _chargeSetTime = _oper.ChargeSet.CreationDateTime;
                        }
                        else
                        {
                            _oper = ((ShowDetailsStartUpParams<RechargeOper>)eventArgsStartUpParams.Data).DomainObject;
                            _chargeSetListId = $"{ChargeSetTypes.RECHARGE_SET_TYPE}_{((RechargeOper)_oper).ChargeSet.ID}";
                            _chargeSetTime = ((RechargeOper)_oper).ChargeSet.CreationDateTime;
                        }

                        DateTime _since =
                            new DateTime(
                                _chargeSetTime.Year,
                                _chargeSetTime.Month,
                                _chargeSetTime.Day,
                                _chargeSetTime.Hour,
                                0,
                                0);

                        WorkItem.State[ModuleStateNames.CURRENT_CHARGE_OPER_ID] = _oper.ID;
                        WorkItem.State[BaseListViewConstants.BaseListViewDefaultParams.CurrentItemIdStateName] =
                            _chargeSetListId;

                        SetCurrentItem(_chargeSetListId);

                        _listView.Since = _since;
                        _listView.Till = _since.AddHours(1);
                        _tabbedView.SelectTab(TabNames.LIST);

                        _launchedOutside = true;
                        _tabbedView.SelectTab(TabNames.CHARGE_DETAIL);
                        _launchedOutside = false;
                    }
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Ошибка при запуске модуля \"Начисления\": {_ex}");
                View.ShowMessage("Произошла ошибка при запуске модуля", "Ошибка");
            }
        }
    }
}