using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Tabbed;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.List
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class ListViewPresenter : BaseMultipleListViewPresenter<IListView, PaymentSet>
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
            return DataMapper<PaymentSet, IPaymentSetDataMapper>().GetList(View.Since, View.Till);
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
            // Do Nothing
        }
    }
}