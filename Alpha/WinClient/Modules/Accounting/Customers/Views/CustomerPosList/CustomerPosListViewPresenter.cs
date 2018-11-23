using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItemPos = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;
using DomServiceSinceTill = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos.ServiceSinceTill;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    public class CustomerPosListViewPresenter : BaseSimplePositionsListViewPresenter<ICustomerPosListView, DomItemPos, DomItem, IUnitOfWork>
    {
        /// <summary>
        /// Список позиций домена
        /// </summary>
        protected override IDictionary<string, DomItemPos> Lines => CurrentDomainWithPositions.CustomerPoses;

        /// <summary>
        /// Уникальные позиции абонентов при множественном редактировании
        /// </summary>
        private List<DomServiceSinceTill> UniquePoses { get; set; }

        private DateTime _firstUnchargedPeriod;

        private string EditItemMode => WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE].ToString();

        public override void OnViewReady()
        {
        }

        /// <summary>
        /// Возвращает список элементов
        /// </summary>
        /// <returns>Список элементов</returns>
        public override DataTable GetElemList()
        {
            DataTable _table;
            string[] _selectedIDs = (string[])WorkItem.State[ModuleStateNames.SELECTED_ITEM_IDS];

            if (EditItemMode == ModuleEditItemModes.Multiple)
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

            RefreshRefBooks();

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
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Name
                        });

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
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Name
                        });

                foreach (var _c in _contractors)
                {
                    _table.Rows.Add(_c.ID, _c.Name);
                }
            }

            return _table;
        }

        public DataTable GetCounters(string serviceId = null)
        {
            DomService _srv = View.Service;
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("Number", typeof(string));
            _table.Columns.Add("ServiceName", typeof(string));
            _table.Columns.Add("ServiceID", typeof(string));

            if (EditItemMode == ModuleEditItemModes.Single)
            {
                _table.Rows.Add(string.Empty, string.Empty, string.Empty);

                List<PrivateCounter> _counters = ((DomItem)WorkItem.State[CommonStateNames.CurrentItem]).Counters.Values.ToList();

                if (!string.IsNullOrEmpty(serviceId))
                {
                    _counters = _counters.Where(c => c.Service.ID == serviceId).ToList();
                }

                foreach (var _c in _counters)
                {
                    _table.Rows.Add(_c.ID, _c.Number, _c.Service.Name, _c.Service.ID);
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
            View.Counters = GetCounters();
            _firstUnchargedPeriod = ServerTime.GetPeriodInfo().FirstUncharged;
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
            _curItem.Rate = _curItem.Rate = View.Rate;

            string _counterID = View.CounterID;
            _curItem.PrivateCounter = string.IsNullOrEmpty(_counterID) ? null : CurrentDomainWithPositions.Counters[View.CounterID];
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
            else if (curItem.Rate < 0)
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

            if (!string.IsNullOrEmpty(message))
            {
                message = String.Format("Отсутствуют обязательные поля:\r\n{0}", message);
            }
            else if (
                curItem.Service.ChargeRule == ChargeRuleType.CounterRate 
                && curItem.PrivateCounter == null 
                && EditItemMode == ModuleEditItemModes.Single)
            {
                message = "Укажите прибор учета";
            }
            else if (curItem.Since > curItem.Till)
            {
                message = "Конечный период не должен наступать раньше начального\r\n";
            }
            else if (curItem.PrivateCounter != null && curItem.Service.ID != curItem.PrivateCounter.Service.ID)
            {
                message = "Не совпдают услуги в позиции и в приборе учета";
            }
            else if (
                Lines.Values.Any(_pos =>
                    _pos.Service.ID == curItem.Service.ID
                    && ((_pos.Since <= curItem.Since && curItem.Since <= _pos.Till) || (_pos.Since <= curItem.Till && curItem.Till <= _pos.Till))))
            {
                message = "Две одинаковых услуги не могут предоставлятся в один и тот же период\r\n";
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
                }
            }

            if(!string.IsNullOrEmpty(message))
            {
                RestoreSavedPositions();
            }

            return String.IsNullOrEmpty(message);
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
            return period < _firstUnchargedPeriod;
        }
    }
}