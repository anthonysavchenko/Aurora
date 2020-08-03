using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.PrivateValuesUploads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.PrivateValuesUploads.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.PrivateValuesUploads.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.PrivateValuesUploads.Views.Item
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class ItemViewPresenter : BaseMultipleListViewPresenter<ItemView, RouteForm>
    {
        [InjectionConstructor]
        public ItemViewPresenter()
            : base(
                new BaseListViewParams
                {
                    CurrentItemIdStateName = ModuleStateNames.CURRENT_POS_ID,
                    UpdateWindowTitleOnRowChanged = false
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
        /// Загрузчик списка операций
        /// </summary>
        /// <returns>Таблица данных</returns>
        public override DataTable GetElemList()
        {
            string itemID = (string)WorkItem.State[CommonStateNames.CurrentItemId];

            DataTable table;

            if (int.TryParse(itemID, out int id))
            {
                using (var _db = new Entities())
                {
                    table = _db.GetItemList(id);
                }
            }
            else
            {
                table = new DataTable();
            }

            return table;
        }

        /// <summary>
        /// Подписчик на событие "Обновить список".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public override void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            ITabbedView _tabbedView = (ITabbedView)WorkItem.SmartParts[ModuleViewNames.TABBED_VIEW];

            if (WorkItem.Status == WorkItemStatus.Inactive || _tabbedView.CurrentTab != TabNames.DETAIL)
                return;

            RefreshList();
        }

        public void ShowDomainOnView()
        {
            string itemID = (string)WorkItem.State[CommonStateNames.CurrentItemId];

            if (int.TryParse(itemID, out int id))
            {
                using (var db = new Entities())
                {
                    var item =
                        db.PrivateValuesUploads
                            .Where(x => x.ID.ToString() == itemID)
                            .Select(x =>
                                new
                                {
                                    x.Month,
                                    x.Directory,
                                    x.Note,
                                    x.ErrorDescription,
                                    PrivateValuesForms = x
                                        .PrivateValuesForms.Count(p => string.IsNullOrEmpty(p.ErrorDescription)),
                                    OuterError = !string.IsNullOrEmpty(x.ErrorDescription),
                                    InnerErrors =
                                        x.PrivateValuesForms.Count > 0
                                            && x.PrivateValuesForms.Any(e =>
                                                !string.IsNullOrEmpty(e.ErrorDescription)),
                                })
                            .First();

                    View.Month = item.Month.ToString("MM.yyyy");
                    View.Directory = item.Directory;
                    View.Note = item.Note;
                    View.Description =
                        item.OuterError && item.InnerErrors
                            ? $"{item.ErrorDescription} А также обнаружены ошибки при распознавании и/или " +
                                "сохранении некоторых файлов."
                            : item.OuterError && !item.InnerErrors
                                ? item.ErrorDescription
                                : !item.OuterError && item.InnerErrors
                                    ? "Обнаружены ошибки при распознавании и/или сохранении некоторых файлов."
                                    : item.PrivateValuesForms > 0
                                        ? "Распознавание и сохранение файлов выполнено успешно."
                                        : "Файлов для распознавания и сохранения не обнаружено.";
                }
            }
            else
            {
                View.Directory = string.Empty;
                View.Note = string.Empty;
                View.Description = string.Empty;
            }
        }
    }
}
