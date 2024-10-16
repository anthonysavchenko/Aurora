﻿using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Constants;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.CounterValue
{
    public class CounterValueViewPresenter : BaseSimpleListViewPresenter<ICounterValueView, CommonCounterValue>
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
        /// Получить таблицу данных (DataTable)
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("Value", typeof(decimal));

            CommonCounter _counter = (CommonCounter)WorkItem.State[ModuleStateNames.COMMON_COUNTER];

            if (_counter != null)
            {
                foreach (CommonCounterValue _counterValue in _counter.Values.Values)
                {
                    _table.Rows.Add(
                        _counterValue.ID,
                        _counterValue.Period,
                        _counterValue.Value);
                }
            }

            return _table;
        }

        /// <summary>
        /// Возвращает текущий объект
        /// </summary>
        /// <returns></returns>
        protected override CommonCounterValue GetCurrentItem()
        {
            CommonCounter _counter = (CommonCounter)WorkItem.State[ModuleStateNames.COMMON_COUNTER];
            string _id = View.GetCurrentItemId();

            return _counter.Values.ContainsKey(_id) ? _counter.Values[_id] : null;
        }

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override CommonCounterValue CreateNewItem()
        {
            CommonCounterValue _curItem = new CommonCounterValue
            {
                CommonCounter = (CommonCounter)WorkItem.State[ModuleStateNames.COMMON_COUNTER]
            };

            return _curItem;
        }

        /// <summary>
        /// Собрать данные с вида в домен.
        /// </summary>
        /// <param name="curItem">Домен</param>
        protected override void GetItemFromView(CommonCounterValue curItem)
        {
            curItem.Period = View.Period;
            curItem.Value = View.Value;
        }

        /// <summary>
        /// Проверка текущего домена на заполненность обязательных полей.
        /// </summary>
        /// <param name="curItem">Домен</param>
        /// <param name="message">сообщение</param>
        /// <returns>true, false</returns>
        protected override bool CheckItem(CommonCounterValue curItem, out string message)
        {
            message = string.Empty;

            if (curItem.Period != ServerTime.GetPeriodInfo().FirstUncharged)
            {
                message = "- Разрешено вводить данные только за первый период без начислений\r\n";
            }

            if (curItem.Value <= 0)
            {
                message = string.Format("{0}- Показание должно быть больше 0", message);
            }

            if (string.IsNullOrEmpty(message) && curItem.CommonCounter.Values.Count > 1)
            {
                if (curItem.CommonCounter.Values.Values.Any(v => v.ID != curItem.ID && v.Period == curItem.Period))
                {
                    message = "- Показание за данный период уже внесены";
                }
                else
                {
                    var _values = curItem.CommonCounter.Values.Values.Where(v => v.Period < curItem.Period);

                    if (_values.Any() && curItem.Value < _values.Max(v => v.Value))
                    {
                        message = "- Новое показание должно быть больше или равно предыдущему";
                    }
                }
            }

            return string.IsNullOrEmpty(message);
        }

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="curItem">Объект домена</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(CommonCounterValue curItem)
        {
            CommonCounter _counter = (CommonCounter)WorkItem.State[ModuleStateNames.COMMON_COUNTER];

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
    }
}