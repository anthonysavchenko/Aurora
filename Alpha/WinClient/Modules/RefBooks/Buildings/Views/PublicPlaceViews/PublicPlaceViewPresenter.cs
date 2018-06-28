using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PublicPlace;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.PublicPlaceViews
{
    public class PublicPlaceViewPresenter : BaseSimpleListViewPresenter<IPublicPlaceView, DomItem>
    {
        private EventHandler _changeEventHandler;

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
            _table.Columns.Add("Area", typeof(decimal));
            _table.Columns.Add("Service");

            Building _building = (Building)WorkItem.State[CommonStateNames.CurrentItem];
            foreach (var _pp in _building.PublicPlaces)
            {
                _table.Rows.Add(
                    _pp.ID,
                    _pp.Area,
                    _pp.Service.ID);
            }

            return _table;
        }

        /// <summary>
        /// Возвращает текущий объект
        /// </summary>
        /// <returns></returns>
        protected override DomItem GetCurrentItem()
        {
            Building _building = (Building)WorkItem.State[CommonStateNames.CurrentItem];
            string _id = View.GetCurrentItemId();

            return _building.PublicPlaces.FirstOrDefault(pp => pp.ID == _id);
        }

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override DomItem CreateNewItem()
        {
            DomItem _curItem = new DomItem
            {
                Building = (Building)WorkItem.State[CommonStateNames.CurrentItem]
            };

            return _curItem;
        }

        /// <summary>
        /// Собрать данные с вида в домен.
        /// </summary>
        /// <param name="curItem">Домен</param>
        protected override void GetItemFromView(DomItem curItem)
        {
            curItem.Area = View.Area;
            curItem.Service = View.Service;
        }

        /// <summary>
        /// Проверка текущего домена на заполненность обязательных полей.
        /// </summary>
        /// <param name="curItem">Домен</param>
        /// <param name="message">сообщение</param>
        /// <returns>true, false</returns>
        protected override bool CheckItem(DomItem curItem, out string message)
        {
            message = string.Empty;

            if (curItem.Service == null)
            {
                message = $"{message}Не выбрана услуга";
            }

            if (string.IsNullOrEmpty(message) &&
                ((Building)WorkItem.State[CommonStateNames.CurrentItem]).PublicPlaces
                    .Any(c => c.ID != curItem.ID && c.Service.ID == curItem.Service.ID))
            {
                message = "Для данной услуги уже указана площадь МОП";
            }

            return string.IsNullOrEmpty(message);
        }

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="curItem">Объект домена</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(DomItem curItem)
        {
            if (curItem.IsNew)
            {
                Building _building = (Building)WorkItem.State[CommonStateNames.CurrentItem];

                if (_building.PublicPlaces.All(pp => pp.ID != curItem.ID))
                {
                    _building.PublicPlaces.Add(curItem);
                    UnitOfWork.registerNew(curItem);
                }
            }
            else
            {
                UnitOfWork.registerDirty(curItem);
            }

            return true;
        }

        public override void DeleteElem()
        {
            UnitOfWork.registerRemoved(new DomItem {ID = View.GetCurrentItemId()});
            _changeEventHandler.Invoke(this, EventArgs.Empty);
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
                            s.ChargeRule == (int)ChargeRuleType.PublicPlaceAreaRate || 
                            s.ChargeRule == (int)ChargeRuleType.PublicPlaceVolumeAreaRate)
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
            _changeEventHandler = handler;
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