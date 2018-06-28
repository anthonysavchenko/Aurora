using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.CounterValue;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
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

        public override void OnViewReady()
        {
        }

        private Dictionary<string, PrivateCounter> Counters => ((Customer)WorkItem.State[CommonStateNames.CurrentItem]).Counters;

        /// <summary>
        /// Получить таблицу данных (DataTable)
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            RefreshRefBooks();

            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Number");
            _table.Columns.Add("Service");

            foreach (PrivateCounter _c in Counters.Values)
            {
                _table.Rows.Add(_c.ID, _c.Number, _c.Service.ID);
            }

            return _table;
        }

        /// <summary>
        /// Возвращает текущий объект
        /// </summary>
        /// <returns></returns>
        protected override PrivateCounter GetCurrentItem()
        {
            string _id = View.GetCurrentItemId();
            return Counters.ContainsKey(_id) ? Counters[_id] : null;
        }

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override PrivateCounter CreateNewItem()
        {
            PrivateCounter _curItem = new PrivateCounter
            {
                Customer = (Customer)WorkItem.State[CommonStateNames.CurrentItem]
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
            curItem.Service = View.Service;
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

            if(curItem.Service == null)
            {
                message += "Не выбрана услуга";
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
                if (!Counters.ContainsKey(curItem.ID))
                {
                    Counters.Add(curItem.ID, curItem);
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
        /// Обновить справочные данные в комбобоксах таблицы
        /// </summary>
        protected override void RefreshRefBooks()
        {
            View.Services = GetServices();
        }

        private DataTable GetServices()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Name");

            using (Entities _db = new Entities())
            {
                var _services = _db.Services.Where(s => s.ChargeRule == (byte)ChargeRuleType.CounterRate).ToList();

                foreach (var _s in _services)
                {
                    _table.Rows.Add(
                        _s.ID.ToString(),
                        _s.Name);
                }
            }

            return _table;
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
            WorkItem.State[ModuleStateNames.CURRENT_PRIVATE_COUNTER] = string.IsNullOrEmpty(id) ? null : Counters[id];

            ICounterValueView _counterValueView = (ICounterValueView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VALUE_VIEW];
            _counterValueView.NavigationButtonsEnabled = true;
            _counterValueView.RefreshList();
        }
    }
}