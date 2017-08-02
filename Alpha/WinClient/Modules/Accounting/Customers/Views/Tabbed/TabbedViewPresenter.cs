using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.Constants;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    public class TabbedViewPresenter : BaseTabbedViewPresenter<IBaseTabbedView, DomItem>
    {
        /// <summary>
        /// —оздать новый домен.
        /// </summary>
        protected override DomItem CreateNewItem()
        {
            // —оздать новый текущий элемент (объект домена).
            DomItem _domItem = new DomItem();

            return _domItem;
        }

        public override void OnEnterTabPage(string _tabPageName, out bool _cancelAction)
        {
            string[] _selectedIDs = ((IBaseMultipleListView)WorkItem.SmartParts.Get(Params.ListViewNameStateName)).SelectedIds;

            if (_selectedIDs.Length > 1)
            {
                WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE] = ModuleEditItemModes.Multiple;
                ((CustomersUnitOfWork)(WorkItem.Services.Get<IUnitOfWork>())).EditMode = ModuleEditItemModes.Multiple;
                WorkItem.State[ModuleStateNames.SELECTED_ITEM_IDS] = _selectedIDs;
                ((CustomersUnitOfWork)(WorkItem.Services.Get<IUnitOfWork>())).SelectedIDs = _selectedIDs;
            }
            else
            {
                WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE] = ModuleEditItemModes.Single;
                ((CustomersUnitOfWork)(WorkItem.Services.Get<IUnitOfWork>())).EditMode = ModuleEditItemModes.Single;
                WorkItem.State[ModuleStateNames.SELECTED_ITEM_IDS] = null;
                ((CustomersUnitOfWork)(WorkItem.Services.Get<IUnitOfWork>())).SelectedIDs = null;
            }

            if (WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE].ToString() == ModuleEditItemModes.Multiple &&
                _tabPageName != ModuleTabNames.CUSTOMER_POSES &&
                _tabPageName != ModuleTabNames.LIST)
            {
                _cancelAction = true;
            }
            else
            {
                base.OnEnterTabPage(_tabPageName, out _cancelAction);

                if (!_cancelAction)
                {
                    switch(_tabPageName)
                    {
                        case ModuleTabNames.PAYMENTS_AND_CHARGES:
                            _cancelAction = WorkItem.State[Params.CurrentItemStateName] == null 
                                || WorkItem.State[Params.EditItemStateName].ToString() == CommonEditItemStates.New;

                            if (!_cancelAction)
                            {
                                ((IBaseSimpleListView)WorkItem.SmartParts.Get(ModuleViewNames.PAYMENTS_AND_CHARGES_VIEW)).RefreshList();
                            }
                            break;
                        case ModuleTabNames.CUSTOMER_POSES:
                            ((IBaseSimpleListView)WorkItem.SmartParts.Get(ModuleViewNames.CUSTOMER_POS_VIEW)).RefreshList();
                            break;
                        case ModuleTabNames.PRIVATE_COUNTERS:
                            ((IBaseSimpleListView)WorkItem.SmartParts.Get(ModuleViewNames.COUNTER_VIEW)).RefreshList();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// »зменить доступ к глобальным командам дл¤ закладки вида списка
        /// </summary>
        public override void ManageCommandsForListTab()
        {
            base.ManageCommandsForListTab();
            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status = CommandStatus.Disabled;
        }

        /// <summary>
        /// »зменить доступ к глобальным командам дл¤ закладок видов детализации
        /// </summary>
        public override void ManageCommandsForNotListTab()
        {
            base.ManageCommandsForNotListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.PrintItem].Status =
                View.CurrentTab == ModuleTabNames.PAYMENTS_AND_CHARGES ? CommandStatus.Enabled : CommandStatus.Disabled;
        }

        /// <summary>
        /// ќбработчик глобального событи¤ "ѕечать"
        /// </summary>
        [EventSubscription(CommonEventNames.PrintItemFired, ThreadOption.UserInterface)]
        public void PrintItemFired(object sender, EventArgs eventArgs)
        {
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (View.CurrentTab == LIST_TAB_PAGE_NAME)
            {
                WorkItem.EventTopics[ModuleEventNames.PRINT_LIST].Fire(sender, eventArgs, WorkItem, PublicationScope.WorkItem);
            }
            else if (View.CurrentTab == ModuleTabNames.PAYMENTS_AND_CHARGES)
            {
                WorkItem.EventTopics[ModuleEventNames.PRINT_PAYMENTS_AND_CHARGES].Fire(sender, eventArgs, WorkItem, PublicationScope.WorkItem);
            }
        }
    }
}