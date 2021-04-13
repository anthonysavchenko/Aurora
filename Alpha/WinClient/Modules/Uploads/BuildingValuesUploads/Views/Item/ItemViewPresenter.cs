﻿using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
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
                        db.BuildingValuesUploads
                            .Where(x => x.ID.ToString() == itemID)
                            .Select(x =>
                                new
                                {
                                    x.Month,
                                    x.FilePath,
                                    x.Note,
                                    x.ErrorDescription,
                                    BuildingCounterValues = x
                                        .BuildingValuesUploadPoses
                                            .Count(p => string.IsNullOrEmpty(p.ErrorDescription)),
                                    OuterError = !string.IsNullOrEmpty(x.ErrorDescription),
                                    InnerErrors =
                                        x.BuildingValuesUploadPoses.Count > 0
                                            && x.BuildingValuesUploadPoses.Any(e =>
                                                !string.IsNullOrEmpty(e.ErrorDescription)),
                                })
                            .First();

                    View.Month = item.Month.ToString("MM.yyyy");
                    View.FilePath = item.FilePath;
                    View.Note = item.Note;
                    View.Description =
                        item.OuterError && item.InnerErrors
                            ? $"{item.ErrorDescription} А также обнаружены ошибки при распознавании и/или " +
                                "сохранении некоторых показаний ОДПУ."
                            : item.OuterError && !item.InnerErrors
                                ? item.ErrorDescription
                                : !item.OuterError && item.InnerErrors
                                    ? "Обнаружены ошибки при распознавании и/или сохранении некоторых показаний ОДПУ."
                                    : item.BuildingCounterValues > 0
                                        ? "Распознавание и сохранение показаний ОДПУ выполнено успешно."
                                        : "Показаний ОДПУ для распознавания и сохранения не обнаружено.";
                }
            }
            else
            {
                View.FilePath = string.Empty;
                View.Note = string.Empty;
                View.Description = string.Empty;
            }
        }
    }
}