using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Top
{
    public class TopViewPresenter : BasePresenter<ITopView>
    {
        public override void OnViewReady()
        {
            base.OnViewReady();
            RefreshDistrictsList();
        }

        private void RefreshDistrictsList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Name");

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            using (var _db = new Entities())
            {
                _db.CounterValueCollectDistricts
                    .ToList()
                    .ForEach(x => _table.Rows.Add(x.ID.ToString(), x.Name));
            }

            View.Districts = _table;
        }

        public void PrintCollectForm(string districtId)
        {
            WorkItem.Controller.RunUsecase(
                ApplicationUsecaseNames.COUNTER_VALUE_COLLECT_FORM, 
                new PrintItemsStartUpParams(new[] { districtId }));
        }

        /// <summary>
        /// Подписчик на событие "Обновить".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.RefreshItemFired, ThreadOption.UserInterface)]
        public virtual void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            // Обновить список элементов
            RefreshDistrictsList();
        }
    }
}
