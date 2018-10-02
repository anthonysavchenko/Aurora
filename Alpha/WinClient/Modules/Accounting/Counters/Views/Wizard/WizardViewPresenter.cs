using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    /// Презентер
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
        /// Сервис работы с доменами, умеющими работать с датамаппером
        /// </summary>
        [ServiceDependency]
        public IDomainWithDataMapperHelperService DomainDataMapperService
        {
            set;
            private get;
        }

        /// <summary>
        /// Первое отображение вида
        /// </summary>
        public override void OnViewReady()
        {
        }

        /// <summary>
        /// Завершает работу мастера
        /// </summary>
        internal void FinishWizard()
        {
            IBaseListView _view = (IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.LIST_VIEW);
            _view.RefreshList();

            ITabbedView _tabbed = ((ITabbedView)WorkItem.SmartParts.Get(ModuleViewNames.TABBED_VIEW));
            _tabbed.SelectTab("tabList");
        }

        /// <summary>
        /// Начинает работу мастера
        /// </summary>
        public void StartWizard()
        {
            Items = new Dictionary<int, WizardItem>();
            CurrentItem = null;

            View.IsMasterCompleted = false;
            View.IsMasterInProgress = false;

            FillDataGrid();
            FillView();

            View.CurrentItemHasError = false;
            View.CurrentItemMessage = string.Empty;

            View.ResultCount = 0;
            View.ResultValue = 0;
            View.ResultErrorCount = 0;
            View.ResetProgressBar(1);

            View.SelectPage(WizardSteps.CollectDataPage);
            View.SetAccountFocus();
        }

        /// <summary>
        /// Обрабатывает изменение шага мастера
        /// </summary>
        /// <param name="prevPage">Предыдущая страница</param>
        /// <param name="page">Открываемая страница</param>
        /// <param name="direction">Назад / Далее</param>
        /// <returns>Следующая страница мастера</returns>
        public WizardSteps OnSelectedPageChanging(BaseWizardPage prevPage, BaseWizardPage page, Direction direction)
        {
            WizardSteps _next = WizardSteps.Unknown;

            if (direction == Direction.Forward)
            {
                switch (prevPage.Name)
                {
                    case "CollectDataWizardPage":
                        {
                            // Проверяем наличие хоть одной записи
                            if (Items.Count == 1 && string.IsNullOrEmpty(Items[0].Account))
                            {
                                View.ShowMessage("Введите хотя бы один платеж.", "Ошибка ввода данных");
                                _next = WizardSteps.Unknown;
                            }
                            // Проверяем на наличие хотя бы одной ошибки
                            else if (Items.Values.Any(o => o.HasError))
                            {
                                View.ShowMessage("Исправьте ошибки в данных перед их сохранением.", "Ошибка ввода данных");
                                _next = WizardSteps.Unknown;
                            }
                            else
                            {
                                _next = WizardSteps.ProcessingPage;
                            }
                        }
                        break;

                    case "ProcessingWizardPage":
                        {
                            if (page.Name == "CollectDataWizardPage")
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

            return _next;
        }

        /// <summary>
        /// Обрабатывает событие перехода на новую страницу
        /// </summary>
        /// <param name="page">Страница, на которую был осуществлен переход</param>
        /// <param name="prevPage">Страница предыдущего состояния</param>
        /// <param name="direction">Назад / Далее</param>
        public void OnSelectedPageChanged(BaseWizardPage page, BaseWizardPage prevPage, Direction direction)
        {
            if (direction == Direction.Forward)
            {
                switch (page.Name)
                {
                    case "ProcessingWizardPage":
                        {
                            switch (prevPage.Name)
                            {
                                case "CollectDataWizardPage":
                                    {
                                        FillDataGrid();
                                        View.ResetProgressBar(Items.Count);
                                        Thread _thread = new Thread(Save);
                                        _thread.Start();
                                    }
                                    break;
                            }
                        }
                        break;
                    case "CollectDataWizardPage":
                        View.SetAccountFocus();
                        break;
                }
            }
        }

        /// <summary>
        /// Сохраняет введенные данные
        /// </summary>
        private void Save()
        {
            View.IsMasterInProgress = true;
            int _customerID = int.Parse(UserHolder.User.ID);

            _processedCount = _errorsCount = 0;

            using (var _db = new Entities())
            {
                foreach (var _item in Items.Values)
                {
                    _db.PrivateCounterValues.AddObject(
                        new PrivateCounterValues
                        {
                            CollectDate = _item.CollectDate,
                            Period = new DateTime(_item.CollectDate.Year, _item.CollectDate.Month, 1),
                            Value = _item.Value,
                            PrivateCounters = _db.PrivateCounters.First(x => x.ID == _item.CounterId)
                        });
                }

                _db.SaveChanges();
            }

            View.ResultCount = _processedCount;
            View.ResultErrorCount = _errorsCount;
            View.IsMasterInProgress = false;
            View.IsMasterCompleted = true;

            View.SelectPage(WizardSteps.FinishPage);
        }

        /// <summary>
        /// Заполняет таблицу введенными данными
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
                    _pair.Value.Account,
                    _pair.Value.CounterNumber,
                    _pair.Value.CollectDate.ToString("dd.MM.yyyy"),
                    _pair.Value.CollectDate.ToString("MM.yyyy"),
                    _pair.Value.Value,
                    _pair.Value.HasError);
            }

            View.Items = _data;
        }

        /// <summary>
        /// Находит личевой счет по номеру или создает новый
        /// </summary>
        /// <param name="account">Номер л/с</param>
        public void SetCustomer(string account)
        {
            if (!string.IsNullOrEmpty(account))
            {
                WizardItem _item;
                using (Entities _db = new Entities())
                {
                    _item = _db.GetCustomerInfo(account);
                }
                _item.CollectDate = ServerTime.GetDateTimeInfo().Now.Date;
                AddItem(_item);
            }
            else
            {
                CurrentItem = null;
            }

            FillView();
        }

        private void FillView()
        {
            if (CurrentItem != null)
            {
                View.Apartment = CurrentItem.Apartment;
                View.Area = CurrentItem.Area.ToString("0.00 кв.м.");
                View.Building = CurrentItem.Building;
                View.CustomerName = CurrentItem.CustomerName;
                View.Street = CurrentItem.Street;
                View.CollectDate = CurrentItem.CollectDate;
                View.CounterValue = CurrentItem.Value;
                using (var _db = new Entities())
                {
                    View.Counters = _db.GetCustomerCounters(CurrentItem.CustomerId);
                }
            }
            else
            {
                View.Apartment = string.Empty;
                View.Area = string.Empty;
                View.Building = string.Empty;
                View.CustomerName = string.Empty;
                View.Street = string.Empty;
                View.Counters = null;
                View.CollectDate = DateTime.MinValue;
                View.CounterValue = 0;
            }
        }

        /// <summary>
        /// Обрабатывает событие выбора строки в таблице с обработанными данными
        /// </summary>
        /// <param name="pos">Номер позиции</param>
        public void OnProcesingDataRowChanged(int pos)
        {
            CurrentItem = Items[pos];
            FillView();
        }

        /// <summary>
        /// Перепроверяет корректность введенных данных
        /// </summary>
        public bool ValidateCurrentItem()
        {
            return true;
        }

        /// <summary>
        /// Создает новый платеж
        /// </summary>
        public void CreateItem()
        {
            AddItem(new WizardItem());
        }

        private void AddItem(WizardItem item)
        {
            int _key = Items.Keys.Any() ? Items.Keys.Max() + 1 : 1;
            CurrentItem = item;
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
        /// Удаляет платежи по индексам
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
        /// Возвращает сумму начислений для абонента за период
        /// </summary>
        /// <param name="account">Лицевой счет абонента</param>
        /// <param name="period">Период абонента</param>
        /// <returns>Сумма начислений</returns>
        public decimal GetSuggestedValue()
        {
            const decimal MONTH_NORM = 200;

            decimal _value;
            DateTime _period = ServerTime.GetPeriodInfo().FirstUncharged;

            CounterLastValue _lastValue;
            using (var _db = new Entities())
            {
                _lastValue = _db.GetCounterLastValue(CurrentItem.CounterId);
            }

            if (_lastValue != null)
            {
                int _mounthCount = (int)((_period - _lastValue.Period).TotalDays / 30);
                _value = _mounthCount * MONTH_NORM;
            }
            else
            {
                _value = MONTH_NORM;
            }

            return _value;
        }
    }
}