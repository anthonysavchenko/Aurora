using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;
using Microsoft.Practices.CompositeUI;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.CounterValue;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Infrastructure.Interface.Services;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Reports.PersonalData;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views;
using System.Windows.Forms;
using Taumis.Infrastructure.Interface.Constants;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    /// <summary>
    /// Презентатор Вида формы ввода ЖД накладных.
    /// </summary>
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, DomItem>
    {
        private string _physicalNameState;
        private string _errorMessage = string.Empty;

        /// <summary>
        /// Единица работы
        /// </summary>
        [ServiceDependency]
        public IUnitOfWork UOW { protected get; set; }

        [ServiceDependency]
        public ICryptoService CryptoService { get; set; }

        [ServiceDependency]
        public IEmailService EmailService { get; set; }

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_updateMode">Режим изменения элемента</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool AddOrUpdateItem(DomItem _domItem, UpdateMode _updateMode)
        {
            bool _result;

            if (WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE].ToString() == ModuleEditItemModes.Multiple)
            {
                _result = WorkItem.Services.Get<IUnitOfWork>().commit();
            }
            else
            {
                DateTime? _since = null,
                          _till = null;
                foreach (var _counter in _domItem.Counters.Values)
                {
                    if (_counter.Values.Values.Any(v => v.IsNew))
                    {
                        var _values = _counter.Values.Values.OrderByDescending(v => v.Period).ToArray();
                        var _lastValue = _values[0];
                        var _prevValue = _values[1];

                        if (!_lastValue.ByNorm && _prevValue.ByNorm)
                        {
                            DateTime _prevPeriod = _counter.Values.Values.Where(v => v.ID != _lastValue.ID && !v.ByNorm).Max(v => v.Period).AddMonths(1);
                            DateTime _lastPeriod = _lastValue.Period.AddMonths(-1);
                            _since = _since.HasValue && _since.Value < _prevPeriod ? _since : _prevPeriod;
                            _till = _till.HasValue && _till > _lastPeriod ? _till : _lastPeriod;
                        }
                    }
                }

                _result = true;

                try
                {
                    GrantWebAccess(_domItem);
                }
                catch (ApplicationException _ex)
                {
                    _errorMessage = _ex.Message;
                }

                if (_domItem.User != null && _domItem.User.IsNew)
                {
                    _result = UpdateItem(_domItem.User);
                }

                _result = 
                    _result
                    && UpdateItem(_domItem) 
                    && UOW.commit();

                if(_result)
                {
                    if (_since.HasValue && _till.HasValue)
                    {
                        DialogResult _dialogResult = MessageBox.Show(
                            $"Для некоторых услуг показания прибора учета проставлены не по норме, необходимо выполнить перерасчет с {_since:MM.yyyy} по {_till:MM.yyyy}. Продолжить?",
                            "Необходим перерасчет",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (_dialogResult == DialogResult.Yes)
                        {

                            WorkItem.Controller.RunUsecase(
                                ApplicationUsecaseNames.CHARGES,
                                new RechargeStartUpParams
                                {
                                    CustomerID = int.Parse(_domItem.ID),
                                    Since = _since.Value,
                                    Till = _till.Value
                                });
                        }
                    }
                }
            }

            return _result;
        }

        private void GrantWebAccess(DomItem domItem)
        {
            string _email = View.Email;

            if ((!View.WebAccess && domItem.User != null) || (domItem.User != null && domItem.User.Login != _email))
            {
                int _userID = int.Parse(domItem.User.ID);
                int _id = int.Parse(domItem.ID);

                bool _otherCustomersExist;

                using (Entities _entities = new Entities())
                {
                    _otherCustomersExist = _entities.Customers.Any(c => c.UserID == _userID && c.ID != _id);
                }

                if (!_otherCustomersExist)
                {
                    UOW.registerRemoved(domItem.User);
                }
            }

            if (View.WebAccess)
            {
                if (domItem.User == null || domItem.User.Login != _email)
                {
                    List<int> _users;

                    using (Entities _entities = new Entities())
                    {
                        _users = _entities.Users.Where(u => u.Login == _email).Select(u => u.ID).ToList();
                    }

                    if (_users.Any())
                    {
                        string _id = _users.First().ToString();
                        domItem.User = GetItem<User>(_id);
                    }
                    else
                    {
                        string _password = CryptoService.GenerateWebPassword();

                        domItem.User =
                            new User
                            {
                                Login = _email,
                                Password = CryptoService.HashPassword(_password),
                                Aka =
                                    domItem.OwnerType == DomItem.OwnerTypes.PhysicalPerson
                                        ? domItem.PhysicalPersonShortName
                                        : domItem.JuridicalPersonFullName,
                            };

                        //EmailService.SendCredentials(domItem.User.Login, domItem.User.Aka, _password);
                    }

                    domItem.BillSendingSubscription = true;
                }
            }
            else
            {
                domItem.User = null;
                domItem.BillSendingSubscription = false;
            }
        }

        /// <summary>
        /// Получить атрибуты домена из всех "видов (Views)" юзкейса.
        /// </summary>
        /// <param name="_domItem">Результирующий объект домена</param>
        protected override void FillDomainFromAllViews(DomItem _domItem)
        {
            _domItem.OwnerType = View.OwnerType;

            if (_domItem.OwnerType == DomItem.OwnerTypes.PhysicalPerson)
            {
                _domItem.PhysicalPersonFullName = View.PhysicalPersonFullName;
                _domItem.PhysicalPersonShortName = View.PhysicalPersonShortName;
                _domItem.JuridicalPersonFullName = String.Empty;
            }
            else if (_domItem.OwnerType == DomItem.OwnerTypes.JuridicalPerson)
            {
                _domItem.PhysicalPersonFullName = String.Empty;
                _domItem.PhysicalPersonShortName = String.Empty;
                _domItem.JuridicalPersonFullName = View.JuridicalPersonFullName;
            }
            else
            {
                _domItem.PhysicalPersonFullName = String.Empty;
                _domItem.PhysicalPersonShortName = String.Empty;
                _domItem.JuridicalPersonFullName = String.Empty;
            }

            _domItem.Account = View.Account;
            _domItem.RoomsCount = View.RoomsCount;
            _domItem.IsPrivate = View.IsPrivate;
            _domItem.Building = View.Building;
            _domItem.Floor = View.Floor;
            _domItem.Entrance = View.Entrance;
            _domItem.Apartment = View.Apartment;
            _domItem.Square = View.Square > 0 ? View.Square : 0;
            _domItem.HeatedArea = View.HeatedArea;
            _domItem.Comment = View.Comment;
            _domItem.LiftPresence = View.LiftPresence;
            _domItem.RubbishChutePresence = View.RubbishChutePresence;
            _domItem.DebtsRepayment = View.DebtsRepayment;
        }

        /// <summary>
        /// Проверить предусловия перед операцией сохранения
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        /// <returns>true, если сохранение возможно; иначе - false</returns>
        protected override bool CheckPreSaveConditions(DomItem _domItem, out string _errorMessage)
        {
            StringBuilder _errorMsg = new StringBuilder();
            int _customerId;
            int.TryParse(_domItem.ID, out _customerId);

            if (string.IsNullOrEmpty(_domItem.Account))
            {
                _errorMsg.AppendLine(" - Не заполнено поле номера лицевого счета");
            }
            else
            {
                using (Entities _entities = new Entities())
                {
                    if (_entities.Customers.Any(customer => customer.Account == _domItem.Account && customer.ID != _customerId))
                    {
                        _errorMsg.AppendLine(" - Абонент с указанным номером лицевого счета уже существует");
                    }
                }
            }

            if (_domItem.Building == null)
            {
                _errorMsg.AppendLine(" - Не заполнены поля адреса");
            }

            if (_domItem.Square <= 0)
            {
                _errorMsg.AppendLine(" - Не заполнена общая площадь");
            }

            if (_domItem.OwnerType == DomItem.OwnerTypes.PhysicalPerson && String.IsNullOrEmpty(_domItem.PhysicalPersonFullName))
            {
                _errorMsg.AppendLine(" - Не заполнено поле \"Полное имя физического лица\"");
            }

            if (_domItem.OwnerType == DomItem.OwnerTypes.PhysicalPerson && String.IsNullOrEmpty(_domItem.PhysicalPersonShortName))
            {
                _errorMsg.AppendLine(" - Не заполнено поле \"Краткое имя физического лица\"");
            }

            if (_domItem.OwnerType == DomItem.OwnerTypes.JuridicalPerson && String.IsNullOrEmpty(_domItem.JuridicalPersonFullName))
            {
                _errorMsg.AppendLine(" - Не заполнено поле \"Полное наименование юридического лица\"");
            }

            if (string.IsNullOrEmpty(View.Email) && View.WebAccess)
            {
                _errorMsg.AppendLine(" - Не заполнено поле \"Email\"");
            }

            _errorMessage = _errorMsg.Length > 0
                                ? string.Format("Ошибки заполнения формы:\n{0}", _errorMsg)
                                : string.Empty;

            return string.IsNullOrEmpty(_errorMessage);
        }

        /// <summary>
        /// Выполняет действия при неуспешности сохранения
        /// </summary>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        protected override void OnSaveFailed(out string errorMessage)
        {
            errorMessage = string.Format("Произошла ошибка при сохранении. {0}", _errorMessage);
            _errorMessage = string.Empty;
        }

        /// <summary>
        /// Отображает домен на всех видах
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void ShowDomainOnAllViews(DomItem _domItem)
        {
            UOW.Clear();
            View.OwnerType = _domItem.OwnerType;

            if (_domItem.OwnerType == DomItem.OwnerTypes.PhysicalPerson)
            {
                View.PhysicalPersonFullName = _domItem.PhysicalPersonFullName;
                View.PhysicalPersonShortName = _domItem.PhysicalPersonShortName;
                View.JuridicalPersonFullName = String.Empty;
            }
            else if (_domItem.OwnerType == DomItem.OwnerTypes.JuridicalPerson)
            {
                View.PhysicalPersonFullName = String.Empty;
                View.PhysicalPersonShortName = String.Empty;
                View.JuridicalPersonFullName = _domItem.JuridicalPersonFullName;
            }
            else
            {
                View.PhysicalPersonFullName = String.Empty;
                View.PhysicalPersonShortName = String.Empty;
                View.JuridicalPersonFullName = String.Empty;
            }

            string _account = _domItem.Account;

            View.Account = _account;
            View.RoomsCount = _domItem.RoomsCount;
            View.IsPrivate = _domItem.IsPrivate;
            View.Streets = GetList<Street>();

            if (_domItem.Building != null)
            {
                View.Street = _domItem.Building.Street;
                View.Buildings = DataMapper<Building, IBuildingDataMapper>().GetBuildingsOnStreet(_domItem.Building.Street);
                View.FloorMax = _domItem.Building.FloorCount;
                View.EntranceMax = _domItem.Building.EntranceCount;
            }
            else
            {
                View.Street = null;
                View.Buildings = null;
            }

            View.Building = _domItem.Building;
            View.Floor = _domItem.Floor;
            View.Entrance = _domItem.Entrance;
            View.Apartment = _domItem.Apartment;
            View.Square = _domItem.Square;
            View.HeatedArea = _domItem.HeatedArea;
            View.Comment = _domItem.Comment;
            View.LiftPresence = _domItem.LiftPresence;
            View.RubbishChutePresence = _domItem.RubbishChutePresence;
            View.WebAccess = _domItem.User != null;
            View.Email = _domItem.User?.Login;
            View.DebtsRepayment = _domItem.DebtsRepayment;
            
            ((IResidentsListView)WorkItem.SmartParts.Get(ModuleViewNames.RESIDENTS_LIST_VIEW)).RefreshList();
        }

        /// <summary>
        /// Включить отслеживание изменений элементов управления на дополнительных вью
        /// </summary>
        protected override void BindAdditionalViewsControls()
        {
            base.BindAdditionalViewsControls();
            ((IResidentsListView)WorkItem.SmartParts.Get(ModuleViewNames.RESIDENTS_LIST_VIEW)).BindActivate(OnAnyAttributeChangedEventHandler);
            ((ICustomerPosListView)WorkItem.SmartParts.Get(ModuleViewNames.CUSTOMER_POS_VIEW)).BindActivate(OnAnyAttributeChangedEventHandler);
            ((ICounterView)WorkItem.SmartParts.Get(ModuleViewNames.COUNTER_VIEW)).BindActivate(OnAnyAttributeChangedEventHandler);
            ((ICounterValueView)WorkItem.SmartParts.Get(ModuleViewNames.COUNTER_VALUE_VIEW)).BindActivate(OnAnyAttributeChangedEventHandler);
        }

        /// <summary>
        /// Выключить отслеживание изменений элементов управления на дополнительных вью
        /// </summary>
        protected override void UnbindAdditionalViewsControls()
        {
            base.UnbindAdditionalViewsControls();
            ((IResidentsListView)WorkItem.SmartParts.Get(ModuleViewNames.RESIDENTS_LIST_VIEW)).BindDeactivate(OnAnyAttributeChangedEventHandler);
            ((ICustomerPosListView)WorkItem.SmartParts.Get(ModuleViewNames.CUSTOMER_POS_VIEW)).BindDeactivate(OnAnyAttributeChangedEventHandler);
            ((ICounterView)WorkItem.SmartParts.Get(ModuleViewNames.COUNTER_VIEW)).BindDeactivate(OnAnyAttributeChangedEventHandler);
            ((ICounterValueView)WorkItem.SmartParts.Get(ModuleViewNames.COUNTER_VALUE_VIEW)).BindDeactivate(OnAnyAttributeChangedEventHandler);
        }

        protected override void RefreshRefBooks()
        {
            View.Streets = GetList<Street>();
        }

        internal void FillBuildingList()
        {
            View.Buildings = DataMapper<Building, IBuildingDataMapper>().GetBuildingsOnStreet(View.Street);
        }

        public void SetFloorMax()
        {
            View.FloorMax = View.Building.FloorCount;
        }

        public void SetEntranceMax()
        {
            View.EntranceMax = View.Building.EntranceCount;
        }

        public void UpdateResidentsListView(bool clearOwnerRelationship)
        {
            IResidentsListView _residentsListView =
                (IResidentsListView)WorkItem.SmartParts[ModuleViewNames.RESIDENTS_LIST_VIEW];

            if (clearOwnerRelationship)
            {
                DomItem _customer = (DomItem)WorkItem.State[CommonStateNames.CurrentItem];

                foreach (Resident _resident in _customer.Residents.Values)
                {
                    if (_resident.OwnerRelationship != Resident.Relationship.Unknown)
                    {
                        _resident.OwnerRelationship = Resident.Relationship.Unknown;
                        UOW.registerDirty(_resident);
                    }
                }
                _residentsListView.RefreshList();
            }

            _residentsListView.OwnerRelationshipEnabled = !clearOwnerRelationship;
        }

        public void OnBeginPhysicalNameEdit(string name)
        {
            _physicalNameState = name;
        }

        public void OnEndPhysicalNameEdit(string name)
        {
            if (_physicalNameState != name)
            {
                DomItem _customer = (DomItem)WorkItem.State[CommonStateNames.CurrentItem];

                Resident _resident = _customer.Residents.Values.FirstOrDefault(r => r.OwnerRelationship == Resident.Relationship.Owner);
                if (_resident != null)
                {
                    _resident.OwnerRelationship = Resident.Relationship.Unknown;
                    UOW.registerDirty(_resident);

                    IResidentsListView _residentsListView = (IResidentsListView)WorkItem.SmartParts[ModuleViewNames.RESIDENTS_LIST_VIEW];
                    _residentsListView.RefreshList();
                }
            }
        }

        public void PrintStatement()
        {
            DomItem _customer = (DomItem)WorkItem.State[Params.CurrentItemStateName];

            DataSet _ds = new DataSet();
            _ds.Customer.Rows.Add(
                string.Format("{0}, проживающий(ая) по адресу:", _customer.PhysicalPersonFullName),
                string.Format(
                    "г. Владивосток, ул. {0}, {1}, кв. {2}",
                    _customer.Building.Street.Name,
                    _customer.Building.Number,
                    _customer.Apartment),
                _customer.Account);

            PersonalDataReportObject _statement = new PersonalDataReportObject();
            _statement.DataSource = _ds;

            ReportPrintTool _reportPrintTool = new ReportPrintTool(_statement);
            _reportPrintTool.ShowPreviewDialog();
        }

        public void PrintDebtRepaymentAgreement()
        {
            using (CreateDebtPaymentAgreementBox _createDialogBox = new CreateDebtPaymentAgreementBox((DomItem)WorkItem.State[Params.CurrentItemStateName]))
            {
                _createDialogBox.ShowDialog();
            }
        }
    }
}