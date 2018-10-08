using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.List.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Tabbed;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Top;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;
using DomPrivateCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PrivateCounter;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.List
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class ListViewPresenter : BaseMultipleListViewPresenter<IBaseMultipleListView, DomPrivateCounter>
    {
        public override void OnViewReady()
        {
            base.OnViewReady();
            DateTimeInfo _dateTimeInfo = ServerTime.GetDateTimeInfo();
        }

        /// <summary>
        /// Загрузчик списка операций
        /// </summary>
        /// <returns>Таблица данных</returns>
        public override DataTable GetElemList()
        {
            DataTable _table;
            DateTime _currentPeriod = ServerTime.GetPeriodInfo().FirstUncharged;
            var _topView = ((ITopView)WorkItem.SmartParts.Get("TopView"));
            bool _showOnlyWoPeriodValues = _topView.ShowOnlyWoPeriodValue;

            using (var _db = new Entities())
            {
                _table = _db.GetCountersDataTable(
                    _currentPeriod, 
                    _topView.Filter, 
                    _topView.Account, 
                    _topView.Street, 
                    _topView.Building,
                    _topView.Apartment,
                    _topView.ZipCode,
                    _showOnlyWoPeriodValues);
            }

            return _table;
        }

        /// <summary>
        /// Подписчик на событие "Обновить список".
        /// </summary>
        public override void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            ITabbedView _tabbedView = (ITabbedView)WorkItem.SmartParts[ModuleViewNames.TABBED_VIEW];

            if (WorkItem.Status == WorkItemStatus.Inactive || _tabbedView.CurrentTab != TabNames.LIST)
                return;

            RefreshList();
        }

        /// <summary>
        /// Удалить элемент
        /// </summary>
        public override void OnDeleteItemFired(object sender, EventArgs eventArgs)
        {
        }
    }
}