using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Item
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
                    CurrentItemIdStateName = ModuleStateNames.SELECTED_FILE_ID,
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
                        db.BuildingValuesUploads
                            .Where(x => x.ID.ToString() == itemID)
                            .Select(x =>
                                new
                                {
                                    x.Month,
                                    x.DirectoryPath,
                                    x.Note,
                                    Description =
                                        x.ProcessingResult != (byte)UploadProcessingResult.OK
                                            ? string.IsNullOrEmpty(x.ErrorDescription)
                                                ? "Программная ошибка во время загрузки расшифровок при обработке " +
                                                    "данных. Проверьте подключение к сети и серверу БД."
                                                : x.ErrorDescription
                                            : "ОК",
                                })
                            .First();

                    View.Month = item.Month.ToString("MM.yyyy");
                    View.DirectoryPath = item.DirectoryPath;
                    View.Note = item.Note;
                    View.Description = item.Description;

                    View.MissingBuildings =
                        string.Join(", ",
                            db.Buildings
                                .Where(b =>
                                    !b.IsArchived
                                    && !db.BuildingCounterValues
                                        .Where(bb => bb.Month == item.Month)
                                        .Select(bb => bb.BuildingCounters.Buildings.ID)
                                        .Contains(b.ID))
                                .Select(b => b.Street + ", д. " + b.Number)
                                .ToArray());
                }
            }
            else
            {
                View.Month = string.Empty;
                View.DirectoryPath = string.Empty;
                View.Note = string.Empty;
                View.Description = string.Empty;
                View.MissingBuildings = string.Empty;
            }
        }
    }
}
