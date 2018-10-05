using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Tabbed;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Model;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard
{
    /// <summary>
    /// ���������
    /// </summary>
    public class WizardViewPresenter : BasePresenter<IWizardView>
    {
        private Dictionary<int, WizardItem> Items { get; set; }
        private WizardItem CurrentItem { get; set; }

        #region Save Payments Variables

        private int _processedCount;
        private int _errorsCount;

        #endregion

        /// <summary>
        /// ������ ������ � ��������, �������� �������� � ������������
        /// </summary>
        [ServiceDependency]
        public IDomainWithDataMapperHelperService DomainDataMapperService
        {
            set;
            private get;
        }

        /// <summary>
        /// ��������� ������ �������
        /// </summary>
        internal void FinishWizard()
        {
            IBaseListView _view = (IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.LIST_VIEW);
            _view.RefreshList();

            ITabbedView _tabbed = ((ITabbedView)WorkItem.SmartParts.Get(ModuleViewNames.TABBED_VIEW));
            _tabbed.SelectTab("tabList");
        }

        /// <summary>
        /// �������� ������ �������
        /// </summary>
        public void StartWizard()
        {
            Items = new Dictionary<int, WizardItem>();

            View.IsMasterCompleted = false;
            View.IsMasterInProgress = false;

            FillDataGrid();
            CreateItem();

            View.CurrentItemHasError = false;
            View.CurrentItemMessage = string.Empty;

            View.ResultCount = 0;
            View.ResultErrorCount = 0;
            View.ResetProgressBar(1);

            View.SelectPage(WizardSteps.CollectDataPage);
            View.SetAccountFocus();
        }

        /// <summary>
        /// ������������ ��������� ���� �������
        /// </summary>
        /// <param name="prevPage">���������� ��������</param>
        /// <param name="page">����������� ��������</param>
        /// <param name="direction">����� / �����</param>
        /// <returns>��������� �������� �������</returns>
        public WizardSteps OnSelectedPageChanging(BaseWizardPage prevPage, BaseWizardPage page, Direction direction)
        {
            WizardSteps _next = WizardSteps.Unknown;

            if (direction == Direction.Forward)
            {
                switch (prevPage.Name)
                {
                    case "collectDataWizardPage":
                        {
                            // ��������� ������� ���� ����� ������
                            if (Items.Count == 1 && string.IsNullOrEmpty(Items[0].CustomerInfo.Account))
                            {
                                View.ShowMessage("������� ���� �� ���� ������.", "������ ����� ������");
                                _next = WizardSteps.Unknown;
                            }
                            // ��������� �� ������� ���� �� ����� ������
                            else if (Items.Values.Any(o => o.HasError))
                            {
                                View.ShowMessage("��������� ������ � ������ ����� �� �����������.", "������ ����� ������");
                                _next = WizardSteps.Unknown;
                            }
                            else
                            {
                                _next = WizardSteps.ProcessingPage;
                            }
                        }
                        break;

                    case "processingWizardPage":
                        {
                            if (page.Name == "collectDataWizardPage")
                            {
                                _next = WizardSteps.CollectDataPage;
                            }
                            else
                            {
                                _next = WizardSteps.FinishPage;
                            }
                        }
                        break;
                }
            }
            else if (prevPage.Name == "finishWizardPage")
            {
                _next = WizardSteps.CollectDataPage;
            }

            return _next;
        }

        /// <summary>
        /// ������������ ������� �������� �� ����� ��������
        /// </summary>
        /// <param name="page">��������, �� ������� ��� ����������� �������</param>
        /// <param name="prevPage">�������� ����������� ���������</param>
        /// <param name="direction">����� / �����</param>
        public void OnSelectedPageChanged(BaseWizardPage page, BaseWizardPage prevPage, Direction direction)
        {
            if (direction == Direction.Forward)
            {
                switch (page.Name)
                {
                    case "processingWizardPage":
                        {
                            switch (prevPage.Name)
                            {
                                case "collectDataWizardPage":
                                    {
                                        View.ResetProgressBar(Items.Count);
                                        Thread _thread = new Thread(Save);
                                        _thread.Start();
                                    }
                                    break;
                            }
                        }
                        break;
                    case "collectDataWizardPage":
                        View.SetAccountFocus();
                        break;
                }
            }
        }

        /// <summary>
        /// ��������� ��������� ������
        /// </summary>
        private void Save()
        {
            View.IsMasterInProgress = true;
            int _customerID = int.Parse(UserHolder.User.ID);

            _processedCount = _errorsCount = 0;

            foreach (var _item in Items.Values)
            {
                using (var _db = new Entities())
                {
                    try
                    {
                        var _period = new DateTime(_item.CollectDate.Year, _item.CollectDate.Month, 1);

                        PrivateCounterValues _value = _db.PrivateCounterValues
                            .FirstOrDefault(x => x.Period == _period && x.PrivateCounters.ID == _item.CounterId);

                        if (_value == null)
                        {
                            _value =
                                new PrivateCounterValues
                                {
                                    Period = _period,
                                    PrivateCounters = _db.PrivateCounters.First(x => x.ID == _item.CounterId)
                                };
                            _db.PrivateCounterValues.AddObject(_value);
                        }

                        _value.CollectDate = _item.CollectDate;
                        _value.Value = _item.CounterValue;
                        _db.SaveChanges();
                        _processedCount++;
                    }
                    catch(Exception ex)
                    {
                        Logger.SimpleWrite(
                            $"������ ��� ���������� �������� ��(ID: {_item.CounterId}, Value{_item.CounterValue}, CollectDate: {_item.CollectDate}): {ex}");
                        _errorsCount++;
                    }
                }
            }

            View.ResultCount = _processedCount;
            View.ResultErrorCount = _errorsCount;
            View.IsMasterInProgress = false;
            View.IsMasterCompleted = true;

            View.SelectPage(WizardSteps.FinishPage);
        }

        /// <summary>
        /// ��������� ������� ���������� �������
        /// </summary>
        private void FillDataGrid()
        {
            DataTable _data = new DataTable();
            _data.Columns.Add("ID");
            _data.Columns.Add("Account");
            _data.Columns.Add("Counter");
            _data.Columns.Add("CollectDate");
            _data.Columns.Add("Period");
            _data.Columns.Add("Value", typeof(decimal));
            _data.Columns.Add("HasError", typeof(bool));
            _data.PrimaryKey = new DataColumn[] { _data.Columns["ID"] };

            foreach (KeyValuePair<int, WizardItem> _pair in Items)
            {
                _data.Rows.Add(
                    _pair.Key,
                    _pair.Value.CustomerInfo.Account,
                    _pair.Value.CounterNumber,
                    _pair.Value.CollectDate.ToString("dd.MM.yyyy"),
                    _pair.Value.CollectDate.ToString("MM.yyyy"),
                    _pair.Value.CounterValue,
                    _pair.Value.HasError);
            }

            View.Items = _data;
        }

        /// <summary>
        /// ������� ������� ���� �� ������ ��� ������� �����
        /// </summary>
        /// <param name="account">����� �/�</param>
        public void SetCustomer(string account)
        {
            if (!string.IsNullOrEmpty(account))
            {
                using (Entities _db = new Entities())
                {
                    CurrentItem.CustomerInfo = _db.GetCustomerInfo(account);
                }

                if (CurrentItem.CustomerInfo != null)
                {

                    CurrentItem.CollectDate = ServerTime.GetDateTimeInfo().Now.Date;
                    AddItem();
                }
            }
            else
            {
                CurrentItem.CustomerInfo = null;
            }

            FillView();
        }

        private void FillView()
        {
            if (CurrentItem != null && CurrentItem.CustomerInfo != null)
            {
                View.Account = CurrentItem.CustomerInfo.Account;
                View.Apartment = CurrentItem.CustomerInfo.Apartment;
                View.Area = CurrentItem.CustomerInfo.Area.ToString("0.00 ��.�.");
                View.Building = CurrentItem.CustomerInfo.Building;
                View.CustomerName = CurrentItem.CustomerInfo.CustomerName;
                View.Street = CurrentItem.CustomerInfo.Street;
                View.CollectDate = CurrentItem.CollectDate;
                View.CounterValue = CurrentItem.CounterValue;

                using (var _db = new Entities())
                {
                    View.Counters = _db.GetCustomerCounters(CurrentItem.CustomerInfo.CustomerId);
                }

                if (View.Counters.Rows.Count > 0 && CurrentItem.CounterId < 1)
                {
                    CurrentItem.CounterId = (int)View.Counters.Rows[0]["ID"];
                }

                View.CounterId = CurrentItem.CounterId;
            }
            else
            {
                View.Account = string.Empty;
                View.Apartment = string.Empty;
                View.Area = string.Empty;
                View.Building = string.Empty;
                View.CustomerName = string.Empty;
                View.Street = string.Empty;
                View.Counters = null;
                View.CounterModel = string.Empty;
                View.CollectDate = DateTime.MinValue;
                View.PrevCounterValue = 0;
                View.CounterValue = 0;
            }
        }

        /// <summary>
        /// ������������ ������� ������ ������ � ������� � ������������� �������
        /// </summary>
        /// <param name="pos">����� �������</param>
        public void OnProcesingDataRowChanged(int pos)
        {
            CurrentItem = Items[pos];
            FillView();
        }

        /// <summary>
        /// ������������� ������������ ��������� ������
        /// </summary>
        public bool ValidateCurrentItem()
        {
            string _message = string.Empty;
            bool _hasErrors = true;

            if (CurrentItem != null && CurrentItem.CustomerInfo != null)
            {
                CurrentItem.CounterId = View.CounterId;
                CurrentItem.CollectDate = View.CollectDate;
                CurrentItem.Period = new DateTime(CurrentItem.CollectDate.Year, CurrentItem.CollectDate.Month, 1);
                CurrentItem.CounterValue = View.CounterValue;

                if (!Regex.IsMatch(CurrentItem.CustomerInfo.Account, @"EG-\d{4}-\d{3}-\d{1}"))
                {
                    _message += "������������ ������� ����. \r\n";
                }

                if (CurrentItem.CounterId <= 0)
                {
                    _message += "�� ������ ������ �����. \r\n";
                }

                if (CurrentItem.CounterValue <= 0)
                {
                    _message += "������������ ���������. \r\n";
                }
                else if (CurrentItem.PrevCounterValue > 0 && CurrentItem.CounterValue < CurrentItem.PrevCounterValue)
                {
                    _message += "������� ��������� ������ �����������";
                }

                _hasErrors = !string.IsNullOrEmpty(_message);
            }

            View.CurrentItemHasError = _hasErrors;
            View.CurrentItemMessage = _hasErrors ? _message : "������ ���������";

            if (CurrentItem.Period == CurrentItem.PrevCounterValuePeriod)
            {
                View.CurrentItemMessage += "\r\n�� ��������� ������ ��� ������� ���������. ��� ����� ������������.";
            }

            return !_hasErrors;
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        public void CreateItem()
        {
            CurrentItem = new WizardItem();
            FillView();
        }

        public void DublicateItem()
        {
            CurrentItem =
                new WizardItem
                {
                    CustomerInfo = CurrentItem.CustomerInfo,
                    CollectDate = CurrentItem.CollectDate
                };

            AddItem();
            View.CounterValue = 0;
        }

        private void AddItem()
        {
            int _key = Items.Keys.Any() ? Items.Keys.Max() + 1 : 1;
            Items.Add(_key, CurrentItem);

            View.Items.Rows.Add(
                _key,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                0,
                false);
        }

        /// <summary>
        /// ������� ������� �� ��������
        /// </summary>
        public void DeleteItems(IList<int> ids)
        {
            foreach (int _id in ids)
            {
                Items.Remove(_id);
                View.Items.Rows.Remove(View.Items.Rows.Find(_id));
            }

            if (!Items.Any())
            {
                CreateItem();
            }
        }

        /// <summary>
        /// ���������� ����� ���������� ��� �������� �� ������
        /// </summary>
        /// <param name="account">������� ���� ��������</param>
        /// <param name="period">������ ��������</param>
        /// <returns>����� ����������</returns>
        public decimal GetSuggestedValue()
        {
            const decimal MONTH_NORM = 200;

            decimal _value;

            CounterLastValue _lastValue;
            using (var _db = new Entities())
            {
                _lastValue = _db.GetCounterLastValue(CurrentItem.CounterId);
            }

            if (_lastValue != null)
            {
                int _mounthCount = (int)((CurrentItem.Period - _lastValue.Period).TotalDays / 30);
                _value = _mounthCount * MONTH_NORM + _lastValue.Value;
            }
            else
            {
                _value = MONTH_NORM;
            }

            return _value;
        }

        /// <summary>
        /// ������������� �������� ����. ��������� ������� �����
        /// </summary>
        public void SetPrevCounterValue()
        {
            int _counterId = View.CounterId;

            if (_counterId > 0)
            {
                CurrentItem.CounterId = _counterId;
                CounterLastValue _lastValue;
                using (var _db = new Entities())
                {
                    _lastValue = _db.GetCounterLastValue(CurrentItem.CounterId);
                }

                if (_lastValue != null)
                {
                    CurrentItem.PrevCounterValue = _lastValue.Value;
                    CurrentItem.PrevCounterValuePeriod = _lastValue.Period;
                }
                else
                {
                    CurrentItem.PrevCounterValue = 0;
                    CurrentItem.PrevCounterValuePeriod = DateTime.MinValue;
                }

                
                View.PrevCounterValue = CurrentItem.PrevCounterValue;
            }
        }
    }
}