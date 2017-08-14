using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.CounterValue
{
    public class CounterValueViewPresenter : BaseSimpleListViewPresenter<ICounterValueView, PrivateCounterValue>
    {
        private EventHandler _onAnyAttributeChangedEventHandler;

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
        /// Получить таблицу данных (DataTable)
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("Value", typeof(decimal));
            _table.Columns.Add("ByNorm", typeof(bool));

            PrivateCounter _counter = (PrivateCounter)WorkItem.State[ModuleStateNames.CURRENT_PRIVATE_COUNTER];

            if (_counter != null)
            {
                foreach (var _counterValue in _counter.Values.Values.OrderBy(v => v.Period))
                {
                    _table.Rows.Add(
                        _counterValue.ID,
                        _counterValue.Period,
                        _counterValue.Value,
                        _counterValue.ByNorm);
                }
            }

            return _table;
        }

        /// <summary>
        /// Возвращает текущий объект
        /// </summary>
        /// <returns></returns>
        protected override PrivateCounterValue GetCurrentItem()
        {
            PrivateCounter _counter = (PrivateCounter)WorkItem.State[ModuleStateNames.CURRENT_PRIVATE_COUNTER];
            string _id = View.GetCurrentItemId();

            return _counter.Values.ContainsKey(_id) ? _counter.Values[_id] : null;
        }

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override PrivateCounterValue CreateNewItem()
        {
            PrivateCounterValue _curItem = new PrivateCounterValue
            {
                PrivateCounter = (PrivateCounter)WorkItem.State[ModuleStateNames.CURRENT_PRIVATE_COUNTER]
            };

            return _curItem;
        }

        /// <summary>
        /// Собрать данные с вида в домен.
        /// </summary>
        /// <param name="curItem">Домен</param>
        protected override void GetItemFromView(PrivateCounterValue curItem)
        {
            curItem.Period = View.Period;
            curItem.Value = View.Value;
            curItem.ByNorm = View.ByNorm;
        }

        /// <summary>
        /// Проверка текущего домена на заполненность обязательных полей.
        /// </summary>
        /// <param name="curItem">Домен</param>
        /// <param name="message">сообщение</param>
        /// <returns>true, false</returns>
        protected override bool CheckItem(PrivateCounterValue curItem, out string message)
        {
            message = string.Empty;

            if (curItem.Value < 0)
            {
                message = string.Format("{0}- Показание должно быть больше или равно 0", message);
            }

            if (string.IsNullOrEmpty(message) && curItem.PrivateCounter.Values.Count > 1)
            {
                if (curItem.PrivateCounter.Values.Values.Any(v => v.ID != curItem.ID && v.Period == curItem.Period))
                {
                    message = "- Показание за данный период уже внесены";
                }
            }

            return string.IsNullOrEmpty(message);
        }

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="curItem">Объект домена</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(PrivateCounterValue curItem)
        {
            PrivateCounter _counter = (PrivateCounter)WorkItem.State[ModuleStateNames.CURRENT_PRIVATE_COUNTER];

            if (curItem.IsNew)
            {
                if (!_counter.Values.ContainsKey(curItem.ID))
                {
                    _counter.Values.Add(curItem.ID, curItem);
                    UnitOfWork.registerNew(curItem);
                }
            }
            else
            {
                UnitOfWork.registerDirty(curItem);
            }

            return true;
        }

        /// <summary>
        /// Удаление текущего элемента домена из базы данных.
        /// </summary>
        public override void DeleteElem()
        {
            UnitOfWork.registerRemoved(GetCurrentItem());
            _onAnyAttributeChangedEventHandler.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindChangeHandlers(Control.ControlCollection coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().Bind(coll, handler);
            _onAnyAttributeChangedEventHandler = handler;
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void UnBindChangeHandlers(Control.ControlCollection coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().UnBind(coll, handler);
        }

        public decimal GetNormValue(DateTime? period)
        {
            PrivateCounter _counter = (PrivateCounter)WorkItem.State[ModuleStateNames.CURRENT_PRIVATE_COUNTER];
            PrivateCounterValue _value = period.HasValue 
                ? _counter.Values.Values.Where(v => v.Period < period).OrderByDescending(v => v.Period).FirstOrDefault()
                : _counter.Values.Values.OrderByDescending(v => v.Period).FirstOrDefault();
            return _value != null
                ? _value.Value + (_counter.Service.Norm ?? 0)
                : 0;
        }
    }
}