using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItemPos = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos;
using DomServiceSinceTill = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos.ServiceSinceTill;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    public class CustomerPosListViewPresenter : BaseSimplePositionsListViewPresenter<ICustomerPosListView, DomItemPos, DomItem, IUnitOfWork>
    {
        /// <summary>
        /// Список позиций домена
        /// </summary>
        protected override IDictionary<string, DomItemPos> Lines
        {
            get 
            {
                return CurrentDomainWithPositions.CustomerPoses;
            }
        }

        /// <summary>
        /// Уникальные позиции абонентов при множественном редактировании
        /// </summary>
        private List<DomServiceSinceTill> UniquePoses
        {
            set;
            get;
        }

        /// <summary>
        /// Возвращает список элементов
        /// </summary>
        /// <returns>Список элементов</returns>
        public override DataTable GetElemList()
        {
            DataTable _table;
            string[] _selectedIDs = (string[])WorkItem.State[ModuleStateNames.SELECTED_ITEM_IDS];

            if (WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE].ToString() == ModuleEditItemModes.Multiple)
            {
                List<DomServiceSinceTill> _uniquePoses;

                _table = DataMapper<DomItemPos, ICustomerPosDataMapper>().GetList(
                    (DomItem)WorkItem.State[CommonStateNames.CurrentItem],
                    (string[])WorkItem.State[ModuleStateNames.SELECTED_ITEM_IDS],
                    out _uniquePoses);

                UniquePoses = _uniquePoses;
            }
            else if (WorkItem.State[CommonStateNames.EditItemState].ToString() == CommonEditItemStates.Edit)
            {
                _table = DataMapper<DomItemPos, ICustomerPosDataMapper>().GetList((DomItem)WorkItem.State[CommonStateNames.CurrentItem]);
            }
            else
            {
                _table = new DataTable();
            }

            return _table;
        }

        private DataTable GetServices()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (Entities _db = new Entities())
            {
                var _services = _db.Services
                    .Select(s =>
                        new
                        {
                            s.ID,
                            s.Name
                        })
                    .ToList();

                foreach (var _s in _services)
                {
                    _table.Rows.Add(_s.ID, _s.Name);
                }
            }

            return _table;
        }

        private DataTable GetContractors()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (Entities _db = new Entities())
            {
                var _contractors = _db.Contractors
                    .Select(c =>
                        new
                        {
                            c.ID,
                            c.Name
                        })
                    .ToList();

                foreach (var _c in _contractors)
                {
                    _table.Rows.Add(_c.ID, _c.Name);
                }
            }

            return _table;
        }

        /// <summary>
        /// Обновить справочные данные в комбобоксах таблицы
        /// </summary>
        protected override void RefreshRefBooks()
        {
            View.Services = GetServices();
            View.Contractors = GetContractors();
        }

        /// <summary>
        /// Обновляет позиции документа
        /// </summary>
        /// <param name="dictionary">Словарь позиций документа</param>
        protected override void SetDomainPositions(Dictionary<string, DomItemPos> dictionary)
        {
            CurrentDomainWithPositions.CustomerPoses.Clear();
            foreach (var _line in dictionary)
            {
                CurrentDomainWithPositions.CustomerPoses.Add(_line.Key, _line.Value);
            }
        }

        /// <summary>
        /// Устанавливает ссылку на владельца позиции
        /// </summary>
        /// <param name="_curItem">Домен позиции</param>
        /// <param name="_positionOwner">Владелец позиции</param>
        protected override void SetPositionOwner(DomItemPos _curItem, DomItem _positionOwner)
        {
            _curItem.Doc = _positionOwner;
        }

        /// <summary>
        /// Наполняет позицию данными с вида
        /// </summary>
        /// <param name="_curItem">Домен позиции</param>
        protected override void FillCurrentPosition(DomItemPos _curItem)
        {
            _curItem.Service = View.Service;
            _curItem.Contractor = View.Contractor;
            _curItem.Since = View.Since;
            _curItem.Till = View.Till;
            _curItem.Rate =
                _curItem.Service != null &&
                _curItem.Service.ChargeRule != Service.ChargeRuleType.CounterRate
                    ? _curItem.Rate = View.Rate
                    : 0;
        }

        /// <summary>
        /// Проверяет корректность введенных данных
        /// </summary>
        /// <param name="curItem">Объект домена для проверки</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <returns>Признак успешности проверки</returns>
        protected override bool CheckItem(DomItemPos curItem, out string message)
        {
            message = string.Empty;

            if (curItem.Service == null)
            {
                message += "- Услуга\r\n";
            }
            else if (curItem.Service.ChargeRule != Service.ChargeRuleType.CounterRate && curItem.Rate < 0)
            {
                message += "- Тариф\r\n";
            }

            if (curItem.Since == DateTime.MinValue)
            {
                message += "- Начальный период\r\n";
            }

            if (curItem.Till == DateTime.MinValue)
            {
                message += "- Конечный период\r\n";
            }

            if (curItem.Contractor == null)
            {
                message += "- Подрядчик\r\n";
            }

            if (!String.IsNullOrEmpty(message))
            {
                message = String.Format("Отсутствуют обязательные поля:\r\n{0}", message);
                RestoreSavedPositions();
            }
            else if (curItem.Since > curItem.Till)
            {
                message = "Конечный период не должен наступать раньше начального\r\n";
                RestoreSavedPositions();
            }
            else if (curItem.Service.ChargeRule != Service.ChargeRuleType.CounterRate &&
                curItem.PrivateCounters.Any())
            {
                message = "По данной услуге были указаны приборы учета. Нельзя изменить ее на услугу начисляемую без приборов учета\r\n";
                RestoreSavedPositions();
            }
            else if (Lines.Values.Any(_pos => _pos.Service.ID == curItem.Service.ID &&
                ((_pos.Since <= curItem.Since && curItem.Since <= _pos.Till) ||
                (_pos.Since <= curItem.Till && curItem.Till <= _pos.Till))))
            {
                message = "Две одинаковых услуги не могут предоставлятся в один и тот же период\r\n";
                RestoreSavedPositions();
            }
            else
            {
                int _curItemServiceID = Int32.Parse(curItem.Service.ID);

                if (WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE].ToString() == ModuleEditItemModes.Multiple &&
                    UniquePoses.Any(_pos => _pos.ServiceID == _curItemServiceID &&
                    ((_pos.Since <= curItem.Since && curItem.Since <= _pos.Till) ||
                    (_pos.Since <= curItem.Till && curItem.Till <= _pos.Till))))
                {
                    message = "У некоторых из выбранных абонентов уже есть такая услуга, предоставляемая в этот период. Данные по ней не отображаются при выборе нескольких абонентов, так как не совподают для них всех. Две одинаковых услуги не могут предоставлятся в один и тот же период\r\n";
                    RestoreSavedPositions();
                }
            }

            return String.IsNullOrEmpty(message);
        }

        /// <summary>
        /// Производит сохранение элемента
        /// </summary>
        /// <param name="_curItem">Объект домена позиции</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(DomItemPos _curItem)
        {
            bool _result = base.SaveItem(_curItem);
            OnRowChanged(_curItem.ID);
            return _result;
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void UnBindChangeHandlers(Control.ControlCollection _coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().UnBind(_coll, handler);
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindChangeHandlers(Control.ControlCollection _coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().Bind(_coll, handler);
        }

        /// <summary>
        /// Проверяет меньше ли период, чем текущий
        /// </summary>
        /// <param name="period">Период для проверки</param>
        /// <returns>Результат проверки</returns>
        public bool IsLessThanFirstUncharged(DateTime period)
        {
            return period < ServerTime.GetPeriodInfo().FirstUncharged;
        }

        /// <summary>
        /// Выполняет действия при изменении выбранного элемента
        /// </summary>
        /// <param name="id">Id выбранного элемента списка</param>
        public virtual void OnRowChanged(string id)
        {
            if (!string.IsNullOrEmpty(id) && CurrentDomainWithPositions.CustomerPoses.ContainsKey(id))
            {
                WorkItem.State[ModuleStateNames.CURRENT_CUSTOMER_POS] =
                    CurrentDomainWithPositions.CustomerPoses[id];

                ((ICounterView) WorkItem.SmartParts[ModuleViewNames.COUNTER_VIEW]).RefreshList();
            }
        }
    }
}