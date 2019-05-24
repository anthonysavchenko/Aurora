using DevExpress.XtraReports.UI;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Reports.PersonalData;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.CounterValue;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Infrastructure.Interface.Services;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    /// <summary>
    /// ����������� ���� ����� ����� �� ���������.
    /// </summary>
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, DomItem>
    {
        private string _physicalNameState;
        private string _errorMessage = string.Empty;

        /// <summary>
        /// ������� ������
        /// </summary>
        [ServiceDependency]
        public IUnitOfWork UOW { protected get; set; }

        [ServiceDependency]
        public ICryptoService CryptoService { get; set; }

        [ServiceDependency]
        public IEmailService EmailService { get; set; }

        /// <summary>
        /// ���������� ���������� �������� � ��
        /// </summary>
        /// <param name="_domItem">������ ������</param>
        /// <param name="_updateMode">����� ��������� ��������</param>
        /// <returns>������� ���������� ���������</returns>
        protected override bool AddOrUpdateItem(DomItem _domItem, UpdateMode _updateMode)
        {
            bool _result;

            if (WorkItem.State[ModuleStateNames.EDIT_ITEM_MODE].ToString() == ModuleEditItemModes.Multiple)
            {
                _result = WorkItem.Services.Get<IUnitOfWork>().commit();
            }
            else
            {
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
                                    domItem.OwnerType == OwnerType.PhysicalPerson
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
        /// �������� �������� ������ �� ���� "����� (Views)" �������.
        /// </summary>
        /// <param name="_domItem">�������������� ������ ������</param>
        protected override void FillDomainFromAllViews(DomItem _domItem)
        {
            _domItem.OwnerType = View.OwnerType;

            if (_domItem.OwnerType == OwnerType.PhysicalPerson)
            {
                _domItem.PhysicalPersonFullName = View.PhysicalPersonFullName;
                _domItem.PhysicalPersonShortName = View.PhysicalPersonShortName;
                _domItem.JuridicalPersonFullName = String.Empty;
            }
            else if (_domItem.OwnerType == OwnerType.JuridicalPerson)
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
            _domItem.Comment = View.Comment;
            _domItem.LiftPresence = View.LiftPresence;
            _domItem.RubbishChutePresence = View.RubbishChutePresence;
            _domItem.DebtsRepayment = View.DebtsRepayment;
        }

        /// <summary>
        /// ��������� ����������� ����� ��������� ����������
        /// </summary>
        /// <param name="_domItem">������ ������</param>
        /// <param name="_errorMessage">��������� �� ������</param>
        /// <returns>true, ���� ���������� ��������; ����� - false</returns>
        protected override bool CheckPreSaveConditions(DomItem _domItem, out string _errorMessage)
        {
            StringBuilder _errorMsg = new StringBuilder();
            int _customerId;
            int.TryParse(_domItem.ID, out _customerId);

            if (string.IsNullOrEmpty(_domItem.Account))
            {
                _errorMsg.AppendLine(" - �� ��������� ���� ������ �������� �����");
            }
            else
            {
                using (Entities _entities = new Entities())
                {
                    if (_entities.Customers.Any(customer => customer.Account == _domItem.Account && customer.ID != _customerId))
                    {
                        _errorMsg.AppendLine(" - ������� � ��������� ������� �������� ����� ��� ����������");
                    }
                }
            }

            if (_domItem.Building == null || _domItem.Square <= 0)
            {
                _errorMsg.AppendLine(" - �� ��������� ���� ������");
            }

            if (_domItem.OwnerType == OwnerType.PhysicalPerson && String.IsNullOrEmpty(_domItem.PhysicalPersonFullName))
            {
                _errorMsg.AppendLine(" - �� ��������� ���� \"������ ��� ����������� ����\"");
            }

            if (_domItem.OwnerType == OwnerType.PhysicalPerson && String.IsNullOrEmpty(_domItem.PhysicalPersonShortName))
            {
                _errorMsg.AppendLine(" - �� ��������� ���� \"������� ��� ����������� ����\"");
            }

            if (_domItem.OwnerType == OwnerType.JuridicalPerson && String.IsNullOrEmpty(_domItem.JuridicalPersonFullName))
            {
                _errorMsg.AppendLine(" - �� ��������� ���� \"������ ������������ ������������ ����\"");
            }

            if (string.IsNullOrEmpty(View.Email) && View.WebAccess)
            {
                _errorMsg.AppendLine(" - �� ��������� ���� \"Email\"");
            }

            _errorMessage = _errorMsg.Length > 0
                                ? string.Format("������ ���������� �����:\n{0}", _errorMsg)
                                : string.Empty;

            return string.IsNullOrEmpty(_errorMessage);
        }

        /// <summary>
        /// ��������� �������� ��� ������������ ����������
        /// </summary>
        /// <param name="_errorMessage">��������� �� ������</param>
        protected override void OnSaveFailed(out string errorMessage)
        {
            errorMessage = string.Format("��������� ������ ��� ����������. {0}", _errorMessage);
            _errorMessage = string.Empty;
        }

        /// <summary>
        /// ���������� ����� �� ���� �����
        /// </summary>
        /// <param name="_domItem">������ ������</param>
        protected override void ShowDomainOnAllViews(DomItem _domItem)
        {
            View.OwnerType = _domItem.OwnerType;

            if (_domItem.OwnerType == OwnerType.PhysicalPerson)
            {
                View.PhysicalPersonFullName = _domItem.PhysicalPersonFullName;
                View.PhysicalPersonShortName = _domItem.PhysicalPersonShortName;
                View.JuridicalPersonFullName = String.Empty;
            }
            else if (_domItem.OwnerType == OwnerType.JuridicalPerson)
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

            if (string.IsNullOrEmpty(_account))
            {
                using (var _db = new Entities())
                {
                    _account = _db.GetNewAccountNum();
                }
            }

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
            View.Comment = _domItem.Comment;
            View.LiftPresence = _domItem.LiftPresence;
            View.RubbishChutePresence = _domItem.RubbishChutePresence;
            View.WebAccess = _domItem.User != null;
            View.Email = _domItem.User?.Login;
            View.DebtsRepayment = _domItem.DebtsRepayment;
            
            ((IResidentsListView)WorkItem.SmartParts.Get(ModuleViewNames.RESIDENTS_LIST_VIEW)).RefreshList();
        }

        /// <summary>
        /// �������� ������������ ��������� ��������� ���������� �� �������������� ���
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
        /// ��������� ������������ ��������� ��������� ���������� �� �������������� ���
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
                string.Format("{0}, �����������(��) �� ������:", _customer.PhysicalPersonFullName),
                string.Format(
                    "�. �����������, ��. {0}, {1}, ��. {2}",
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