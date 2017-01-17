using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Item
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class ItemViewPresenter : BaseMultipleListViewPresenter<ItemView, PaymentOper>
    {
        [InjectionConstructor]
        public ItemViewPresenter()
            : base(
                new BaseListViewParams
                {
                    CurrentItemIdStateName = ModuleStateNames.CURRENT_PAYMENT_OPER_ID,
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
            string _strId = (string)WorkItem.State[CommonStateNames.CurrentItemId];

            return DataMapper<PaymentOper, IPaymentOperDataMapper>().GetList(_strId);
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

        /// <summary>
        /// Выполняет действия для множественного удаления
        /// </summary>
        protected override void DoMultipleDelete()
        {
            string[] _selRowsIds = View.SelectedIds;

            StringBuilder _errMsg = new StringBuilder();
            int _unCount = 0;

            foreach (string _ind in _selRowsIds)
            {
                try
                {
                    Delete(GetItem<PaymentOper>(_ind));
                }
                catch (Exception _ex)
                {
                    _errMsg.AppendLine(_ex.Message);
                    _unCount++;
                }
            }

            View.ShowMessage(
                string.Format("Внесено: {0}\nНе удалось внести: {1}\n\nПодробности:\n{2}",
                    _selRowsIds.Length - _unCount,
                    _unCount,
                    _errMsg),
                "Результат внесения корректировок"
                );
        }

        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <param name="item">объект из списка для удаления</param>
        protected override void Delete(PaymentOper item)
        {
            if ((bool)View.ElemList.Rows.Find(item.ID)["IsCorrected"])
            {
                throw new ApplicationException(
                    string.Format(
                        "Корректировка операция платежа по л/с {0} уже внесена",
                        item.Customer.Account));
            }

            item.PaymentCorrectionOper =
                DataMapper<PaymentCorrectionOper, IPaymentCorrectionOperDataMapper>().Create(int.Parse(item.ID));

            UpdateItem(item);
        }

        /// <summary>
        /// Удалить элемент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public override void OnDeleteItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show(
                    "Вы уверены, что необходимо внести корректировки для выбранных операций ?",
                    "Подтверждение внесения корректировок", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                DoMultipleDelete();

                RefreshList();
            }
        }

        /// <summary>
        /// Обновляет состояние глобальных кнопок, исходя из текущего выбранного элемента
        /// </summary>
        public override void UpdateGlobalButtonsForCurrentItem()
        {
            string _curId = (string)WorkItem.State[ModuleStateNames.CURRENT_PAYMENT_OPER_ID];

            bool _disable = string.IsNullOrEmpty(_curId) || (bool)View.ElemList.Rows.Find(_curId)["IsCorrected"];

            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                _disable ? CommandStatus.Disabled : CommandStatus.Enabled;
        }

        /// <summary>
        /// Выполняет действия при изменении выбранного элемента
        /// </summary>
        /// <param name="_id">Id выбранного элемента списка</param>
        public override void OnRowChanged(string _id)
        {
            base.OnRowChanged(_id);

            UpdateGlobalButtonsForCurrentItem();
        }

        /// <summary>
        /// Подсчитывает количество откорректированных платежей
        /// </summary>
        /// <returns></returns>
        public int CorrectionsCount()
        {
            return View.ElemList.AsEnumerable().Count(r => (bool)r["IsCorrected"]);
        }
    }
}