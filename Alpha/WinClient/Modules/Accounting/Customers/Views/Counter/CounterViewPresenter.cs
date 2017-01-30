using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.CounterValue;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter
{
    public class CounterViewPresenter : BaseSimpleListViewPresenter<ICounterView, PrivateCounter>
    {
        /// <summary>
        /// Единица работы
        /// </summary>
        [ServiceDependency]
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// На загрузку вью
        /// </summary>
        public override void OnViewReady()
        {
        }

        /// <summary>
        /// Обновить общий список элементов.
        /// </summary>
        public override void RefreshList()
        {
            ICounterValueView _counterValueView =
                ((ICounterValueView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VALUE_VIEW]);
            CustomerPos _currentCustomerPos = ((CustomerPos) WorkItem.State[ModuleStateNames.CURRENT_CUSTOMER_POS]);
            
            WorkItem.State[ModuleStateNames.CURRENT_PRIVATE_COUNTER] = null;

            _counterValueView.NavigationButtonsEnabled = false;
            View.NavigationButtonsEnabled =
                WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE].ToString() == ModuleEditItemModes.Single &&
                _currentCustomerPos.Service.ChargeRule == Service.ChargeRuleType.CounterRate;

            _counterValueView.RefreshList();
            base.RefreshList();
        }

        /// <summary>
        /// Получить таблицу данных (DataTable)
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Number");
            _table.Columns.Add("Rate", typeof(decimal));

            CustomerPos _customerPos = (CustomerPos)WorkItem.State[ModuleStateNames.CURRENT_CUSTOMER_POS];

            if (_customerPos != null)
            {
                foreach (PrivateCounter _counter in _customerPos.PrivateCounters.Values)
                {
                    _table.Rows.Add(
                           _counter.ID,
                           _counter.Number,
                           _counter.Rate);
                }
            }

            return _table;
        }

        /// <summary>
        /// Возвращает текущий объект
        /// </summary>
        /// <returns></returns>
        protected override PrivateCounter GetCurrentItem()
        {
            CustomerPos _customerPos = (CustomerPos)WorkItem.State[ModuleStateNames.CURRENT_CUSTOMER_POS];
            string _id = View.GetCurrentItemId();

            return _customerPos.PrivateCounters.ContainsKey(_id) ? _customerPos.PrivateCounters[_id] : null;
        }

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override PrivateCounter CreateNewItem()
        {
            PrivateCounter _curItem = new PrivateCounter
            {
                CustomerPos = (CustomerPos)WorkItem.State[ModuleStateNames.CURRENT_CUSTOMER_POS]
            };

            return _curItem;
        }

        /// <summary>
        /// Собрать данные с вида в домен.
        /// </summary>
        /// <param name="curItem">Домен</param>
        protected override void GetItemFromView(PrivateCounter curItem)
        {
            curItem.Number = View.Number.Trim();
            curItem.Rate = View.Rate;
        }

        /// <summary>
        /// Проверка текущего домена на заполненность обязательных полей.
        /// </summary>
        /// <param name="curItem">Домен</param>
        /// <param name="message">сообщение</param>
        /// <returns>true, false</returns>
        protected override bool CheckItem(PrivateCounter curItem, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(curItem.Number))
            {
                message = "Не заполнен номер счетчика\n";
            }

            if (curItem.Rate <= 0)
            {
                message = string.Format("{0}Тариф должен быть больше 0", message);
            }

            if (string.IsNullOrEmpty(message) &&
                ((CustomerPos)WorkItem.State[ModuleStateNames.CURRENT_CUSTOMER_POS]).PrivateCounters.Values
                    .Any(c => c.ID != curItem.ID && c.Number == curItem.Number))
            {
                message = string.Format("Счетчик с номером {0} уже связан с данной услугой", curItem.Number);
            }

            return string.IsNullOrEmpty(message);
        }

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="curItem">Объект домена</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(PrivateCounter curItem)
        {
            if (curItem.IsNew)
            {
                CustomerPos _pos = (CustomerPos)WorkItem.State[ModuleStateNames.CURRENT_CUSTOMER_POS];
                if (!_pos.PrivateCounters.ContainsKey(curItem.ID))
                {
                    _pos.PrivateCounters.Add(curItem.ID, curItem);
                    UnitOfWork.registerNew(curItem);
                }
            }
            else
            {
                UnitOfWork.registerDirty(curItem);
            }

            OnRowChanged(curItem.ID);

            return true;
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindChangeHandlers(Control.ControlCollection coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().Bind(coll, handler);
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void UnBindChangeHandlers(Control.ControlCollection coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().UnBind(coll, handler);
        }

        /// <summary>
        /// Выполняет действия при изменении выбранного элемента
        /// </summary>
        /// <param name="id">Id выбранного элемента списка</param>
        public virtual void OnRowChanged(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                WorkItem.State[ModuleStateNames.CURRENT_PRIVATE_COUNTER] =
                    ((CustomerPos)WorkItem.State[ModuleStateNames.CURRENT_CUSTOMER_POS]).PrivateCounters[id];

                ICounterValueView _counterValueView = (ICounterValueView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VALUE_VIEW];
                _counterValueView.NavigationButtonsEnabled = true;
                _counterValueView.RefreshList();
            }
        }
    }
}