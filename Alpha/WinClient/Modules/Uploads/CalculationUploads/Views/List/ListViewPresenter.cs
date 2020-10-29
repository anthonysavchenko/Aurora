using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.Tabbed;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.List
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class ListViewPresenter : BaseMultipleListViewPresenter<IListView, DecFormsUpload>
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
            DataTable table;

            using (var _db = new Entities())
            {
                table = _db.GetList(View.Since, View.Till);
            }

            return table;
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