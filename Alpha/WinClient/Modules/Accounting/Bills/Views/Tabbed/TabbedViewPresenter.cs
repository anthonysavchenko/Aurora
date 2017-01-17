using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Wizard;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Tabbed
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class TabbedViewPresenter : BaseTabbedViewPresenter<ITabbedView, BillSet>
    {
        /// <summary>
        /// Обрабатывает событие создания нового элемента
        /// </summary>
        protected override void OnCreateNewItem()
        {
            View.ShowWizardTab();
            View.SelectTab("tabWizard");
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
            if (_tabPageName == "tabWizard")
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
                    View.SelectTab(LIST_TAB_PAGE_NAME);
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
                WorkItem.State[Params.CurrentItemStateName] = GetItem<BillSet>(_curId);
            }
        }

        /// <summary>
        /// Выполняет действия при входе на закладку
        /// </summary>
        /// <param name="_tabPageName">Имя закладки</param>
        /// <param name="_cancelAction">Признак отмены действия выхода с закладки</param>
        public override void OnEnterTabPage(string _tabPageName, out bool _cancelAction)
        {
            IBaseListView _view;
            _cancelAction = false;

            if (_tabPageName != "tabWizard")
            {
                View.HideWizardTab();
            }

            switch (_tabPageName)
            {
                case LIST_TAB_PAGE_NAME:
                    _view = (IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.LIST_VIEW);
                    WorkItem.Controller.MainViewTitle = String.Format(
                        "{0} {1}",
                        WorkItem.Controller.DefaultMainViewTitle,
                        _view.GetCurrentItemShortName());
                    ManageCommandsForListTab();
                    break;

                case DETAIL_TAB_PAGE_NAME:
                    if (((string)WorkItem.State[Params.LeavingTabNameStateName]) == LIST_TAB_PAGE_NAME)
                    {
                        PrepareDomainEditing(out _cancelAction);
                    }
                    if (!_cancelAction)
                    {
                        ManageCommandsForListTab();
                        _view = (IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.ITEM_VIEW);
                        _view.RefreshList();
                    }
                    break;
                case "tabWizard":
                    WorkItem.Controller.MainViewTitle = "Печать квитанций";
                    break;
            }
        }

        public override void ManageCommandsForListTab()
        {
            base.ManageCommandsForListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;
            
            WorkItem.RootWorkItem.Commands[CommonCommandNames.PrintItem].Status =
                CommandStatus.Enabled;

            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }
        /// <summary>
        /// Изменить доступ к глобальным командам для закладок видов детализации
        /// </summary>
        public override void ManageCommandsForNotListTab()
        {
            base.ManageCommandsForNotListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshRefBooks].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.PrintItem].Status =
                CommandStatus.Enabled;

            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }

        /// <summary>
        /// Открывает вкладку "Печать квитанций" после запуска модуля "Квитанции" из другого модуля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription(CommonEventNames.ON_MAIN_VIEW_SHOWN, ThreadOption.UserInterface)]
        public void CreateNewBills(object sender, EventArgs<AnyStartUpParams> eventArgsStartUpParams)
        {
            if (eventArgsStartUpParams.Data is CreateNewItemStartUpParams)
            {
                string _customerAccount = ((CreateNewItemStartUpParams)eventArgsStartUpParams.Data).Data;

                OnCreateNewItem();

                IWizardView _wizardView = WorkItem.SmartParts.Get<IWizardView>(ModuleViewNames.WIZARD_VIEW);
                _wizardView.ReceiptType = ReceiptTypes.Total;
                _wizardView.TotalBillAccount = _customerAccount;
            }
        }
    }
}