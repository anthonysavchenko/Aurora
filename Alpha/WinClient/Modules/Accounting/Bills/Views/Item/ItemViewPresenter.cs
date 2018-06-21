using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Item
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class ItemViewPresenter : BaseMultipleListViewPresenter<ItemView, Bill>
    {
        public ItemViewPresenter()
            : base(new BaseListViewParams()
                {
                    CurrentItemIdStateName = ModuleStateNames.CURRENT_BILL_ID,
                    UpdateWindowTitleOnRowChanged = true,
                })
        {
        }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
        }

        /// <summary>
        /// Выполняет действия при изменении строки
        /// </summary>
        public override void OnRowChanged(string _id)
        {
            WorkItem.State[ModuleStateNames.CURRENT_BILL_ID] = _id;
        }

        /// <summary>
        /// Загрузчик списка
        /// </summary>
        /// <returns>Таблица данных</returns>
        public override DataTable GetElemList()
        {
            DataTable _res = null;
            BillSet _set = (BillSet)WorkItem.State[CommonStateNames.CurrentItem];

            switch (_set.BillType)
            {
                case BillType.Regular:
                    _res = DataMapper<RegularBillDoc, IBaseBillDocDataMapper>().GetList(_set);
                    break;

                case BillType.Debt:
                    _res = DataMapper<DebtBillDoc, IBaseBillDocDataMapper>().GetList(_set);
                    break;


                case BillType.Total:
                    _res = DataMapper<TotalBillDoc, IBaseBillDocDataMapper>().GetList(_set);
                    break;
            }

            return _res;
        }

        /// <summary>
        /// Подписчик на событие "Обновить список".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public override void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            ITabbedView _tabbedView = (ITabbedView)WorkItem.SmartParts[ModuleViewNames.TABBED_VIEW];

            if (WorkItem.Status == WorkItemStatus.Inactive || _tabbedView.CurrentTab != "tabDetail")
                return;

            RefreshList();
        }

        /// <summary>
        /// Обработчик глобального события "Печать"
        /// </summary>
        [EventSubscription(CommonEventNames.PrintItemFired, ThreadOption.UserInterface)]
        public void PrintItemFired(object sender, EventArgs eventArgs)
        {
            ITabbedView _tabbedView = (ITabbedView)WorkItem.SmartParts[ModuleViewNames.TABBED_VIEW];

            if (_tabbedView.CurrentTab == "tabDetail")
            {
                try
                {
                    BillSet _set = (BillSet)WorkItem.State[CommonStateNames.CurrentItem];
                    string[] _ids = View.SelectedIds;

                    if (_ids != null)
                    {
                        switch (_set.BillType)
                        {
                            case BillType.Regular:
                                WorkItem.Controller.RunUsecase(ApplicationUsecaseNames.REGULAR_BILL, new PrintItemsStartUpParams(_ids));
                                break;

                            case BillType.Debt:
                                WorkItem.Controller.RunUsecase(ApplicationUsecaseNames.DEBT_BILL, new PrintItemsStartUpParams(_ids));
                                break;

                            case BillType.Total:
                                WorkItem.Controller.RunUsecase(ApplicationUsecaseNames.TOTAL_BILL, new PrintItemsStartUpParams(_ids));
                                break;
                        }  
                    }
                }
                catch (Exception _ex)
                {
                    Logger.SimpleWrite(_ex.ToString());
                    View.ShowMessage("Не удалось распечатать квитанцию", "Ошибка");
                }
            }
        }
    }
}