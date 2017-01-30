using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.CounterValue;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter
{
    public class CounterViewPresenter : BaseSimpleListViewPresenter<ICounterView, CommonCounter>
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
            RefreshRefBooks();
        }

        /// <summary>
        /// Обновить справочные данные в комбобоксах таблицы
        /// </summary>
        protected override void RefreshRefBooks()
        {
            View.Services = GetServices();
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
            _table.Columns.Add("Service");

            Building _building = (Building)WorkItem.State[CommonStateNames.CurrentItem];

            foreach (CommonCounter _counter in _building.CommonCounters.Values)
            {
                _table.Rows.Add(
                       _counter.ID,
                       _counter.Number,
                       _counter.Service.ID);
            }

            return _table;
        }

        /// <summary>
        /// Возвращает текущий объект
        /// </summary>
        /// <returns></returns>
        protected override CommonCounter GetCurrentItem()
        {
            Building _building = (Building)WorkItem.State[CommonStateNames.CurrentItem];
            string _id = View.GetCurrentItemId();

            return _building.CommonCounters.ContainsKey(_id) ? _building.CommonCounters[_id] : null;
        }

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override CommonCounter CreateNewItem()
        {
            CommonCounter _curItem = new CommonCounter
            {
                Building = (Building)WorkItem.State[CommonStateNames.CurrentItem]
            };

            return _curItem;
        }

        /// <summary>
        /// Собрать данные с вида в домен.
        /// </summary>
        /// <param name="curItem">Домен</param>
        protected override void GetItemFromView(CommonCounter curItem)
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
        protected override bool CheckItem(CommonCounter curItem, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(curItem.Number))
            {
                message = "Не заполнен номер счетчика\r\n";
            }

            if (curItem.Service == null)
            {
                message = string.Format("{0}Не выбрана услуга", message);
            }

            if (string.IsNullOrEmpty(message) &&
                ((Building)WorkItem.State[CommonStateNames.CurrentItem]).CommonCounters.Values
                    .Any(c => c.ID != curItem.ID && c.Service.ID == curItem.Service.ID))
            {
                message = string.Format("С данной услугой уже связан счетчик", curItem.Number);
            }

            return string.IsNullOrEmpty(message);
        }

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="curItem">Объект домена</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(CommonCounter curItem)
        {
            if (curItem.IsNew)
            {
                Building _building = (Building)WorkItem.State[CommonStateNames.CurrentItem];

                if (!_building.CommonCounters.ContainsKey(curItem.ID))
                {
                    _building.CommonCounters.Add(curItem.ID, curItem);
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

        private DataTable GetServices()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (Entities _entities = new Entities())
            {
                var _services =
                    _entities.Services
                        .Where(s => 
                            s.ChargeRule == (int)Service.ChargeRuleType.CounterRate ||
                            s.ChargeRule == (int)Service.ChargeRuleType.CommonCounterByAreaRate)
                        .Select(
                            s =>
                            new
                            {
                                s.ID,
                                s.Name
                            });

                foreach (var _service in _services)
                {
                    _table.Rows.Add(
                        _service.ID,
                        _service.Name);
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
            if (!string.IsNullOrEmpty(id))
            {
                WorkItem.State[ModuleStateNames.COMMON_COUNTER] =
                    ((Building)WorkItem.State[CommonStateNames.CurrentItem]).CommonCounters[id];

                ICounterValueView _view =
                    (ICounterValueView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VALUE_VIEW];

                _view.NavigationButtonsEnabled = true;
                _view.RefreshList();
            }
        }
    }
}