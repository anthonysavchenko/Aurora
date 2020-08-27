using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter
{
    public class CounterViewPresenter : BaseSimpleListViewPresenter<ICounterView, BuildingCounter>
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
            View.UtilityServices = GetServices();
        }

        /// <summary>
        /// Получить таблицу данных (DataTable)
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("CounterNumber", typeof(string));
            table.Columns.Add("UtilityService", typeof(byte));
            table.Columns.Add("Coefficient", typeof(byte));
            table.Columns.Add("CheckedSince", typeof(DateTime));
            table.Columns.Add("CheckedTill", typeof(DateTime));

            Building building = (Building)WorkItem.State[CommonStateNames.CurrentItem];

            foreach (var counter in building.Counters.Values)
            {
                table.Rows.Add(
                    counter.ID,
                    counter.CounterNumber,
                    (byte)counter.UtilityService,
                    counter.Coefficient,
                    counter.CheckedSince,
                    counter.CheckedTill);
            }

            return table;
        }

        /// <summary>
        /// Возвращает текущий объект
        /// </summary>
        /// <returns></returns>
        protected override BuildingCounter GetCurrentItem()
        {
            Building building = (Building)WorkItem.State[CommonStateNames.CurrentItem];
            string id = View.GetCurrentItemId();

            return building.Counters.ContainsKey(id) ? building.Counters[id] : null;
        }

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override BuildingCounter CreateNewItem()
        {
            BuildingCounter _curItem = new BuildingCounter
            {
                Building = (Building)WorkItem.State[CommonStateNames.CurrentItem]
            };

            return _curItem;
        }

        /// <summary>
        /// Собрать данные с вида в домен.
        /// </summary>
        /// <param name="curItem">Домен</param>
        protected override void GetItemFromView(BuildingCounter curItem)
        {
            curItem.CounterNumber = View.CounterNumber;
            curItem.UtilityService = View.UtilityService;
            curItem.Coefficient = View.Coefficient;
            curItem.CheckedSince = View.CheckedSince;
            curItem.CheckedTill = View.CheckedTill;
        }

        /// <summary>
        /// Проверка текущего домена на заполненность обязательных полей.
        /// </summary>
        /// <param name="curItem">Домен</param>
        /// <param name="message">сообщение</param>
        /// <returns>true, false</returns>
        protected override bool CheckItem(BuildingCounter curItem, out string message)
        {
            var error = new StringBuilder();

            if (string.IsNullOrEmpty(curItem.CounterNumber))
            {
                error.AppendLine("- Не заполнен номер счетчика");
            }

            if (curItem.UtilityService == UtilityService.Unknown)
            {
                error.AppendLine("- Не выбрана услуга");
            }

            if (curItem.Coefficient == 0)
            {
                error.AppendLine("- Не указан коэффициент");
            }

            if (curItem.CheckedSince >= curItem.CheckedTill)
            {
                error.AppendLine("- Дата пройденной поверки должна быть раньше даты истечения срока поверки");
            }

            if (!string.IsNullOrEmpty(curItem.CounterNumber) &&
                ((Building)WorkItem.State[CommonStateNames.CurrentItem]).Counters.Values
                    .Count(c => c.ID != curItem.ID && c.CounterNumber == curItem.CounterNumber) > 0)
            {
                error.AppendLine("- Счетчик с таким номером уже был сохранен ранее");
            }

            message = error.ToString();

            return error.Length == 0;
        }

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="curItem">Объект домена</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(BuildingCounter curItem)
        {
            if (curItem.IsNew)
            {
                Building _building = (Building)WorkItem.State[CommonStateNames.CurrentItem];

                if (!_building.Counters.ContainsKey(curItem.ID))
                {
                    _building.Counters.Add(curItem.ID, curItem);
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

        private DataTable GetServices()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(byte));
            table.Columns.Add("Name", typeof(string));

            table.Rows.Add((byte)UtilityService.Electricity, "Электроэнергия");
            table.Rows.Add((byte)UtilityService.ColdWater, "ХВС");
            table.Rows.Add((byte)UtilityService.HotWater, "ГВС");

            return table;
        }
    }
}
