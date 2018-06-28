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
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.List
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class ListViewPresenter : BaseListViewPresenter<IListView, BillSet>
    {
        public override void OnViewReady()
        {
            base.OnViewReady();

            DateTimeInfo _dateTimeInfo = ServerTime.GetDateTimeInfo();

            View.Since = _dateTimeInfo.SinceMonthBeginning;
            View.Till = _dateTimeInfo.TillToday;
        }

        /// <summary>
        /// Загрузчик списка операций
        /// </summary>
        /// <returns>Таблица данных</returns>
        public override DataTable GetElemList()
        {
            return DataMapper<BillSet, IBillSetDataMapper>().GetList(View.Since, View.Till);
        }

        /// <summary>
        /// Подписчик на событие "Обновить список".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public override void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            ITabbedView _tabbedView = (ITabbedView)WorkItem.SmartParts[ModuleViewNames.TABBED_VIEW];

            if (WorkItem.Status == WorkItemStatus.Inactive || _tabbedView.CurrentTab != "tabList")
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

            if (_tabbedView.CurrentTab == "tabList")
            {
                try
                {
                    string[] _billIds = new string[0];
                    BillSet _billSet = GetItem<BillSet>(View.GetCurrentItemId());

                    switch (_billSet.BillType)
                    {
                        case BillType.Regular:
                            _billIds = DataMapper<RegularBillDoc, IBaseBillDocDataMapper>().GetIdsByBillSet(_billSet);
                            break;

                        case BillType.Debt:
                            _billIds = DataMapper<DebtBillDoc, IBaseBillDocDataMapper>().GetIdsByBillSet(_billSet);
                            break;

                        case BillType.Total:
                            _billIds = DataMapper<TotalBillDoc, IBaseBillDocDataMapper>().GetIdsByBillSet(_billSet);
                            break;
                    }

                    if (_billIds.Length > 0)
                    {
                        switch (_billSet.BillType)
                        {
                            case BillType.Regular:
                                WorkItem.Controller.RunUsecase(ApplicationUsecaseNames.REGULAR_BILL, new PrintItemsStartUpParams(_billIds));
                                break;

                            case BillType.Debt:
                                WorkItem.Controller.RunUsecase(ApplicationUsecaseNames.DEBT_BILL, new PrintItemsStartUpParams(_billIds));
                                break;

                            case BillType.Total:
                                WorkItem.Controller.RunUsecase(ApplicationUsecaseNames.TOTAL_BILL, new PrintItemsStartUpParams(_billIds));
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