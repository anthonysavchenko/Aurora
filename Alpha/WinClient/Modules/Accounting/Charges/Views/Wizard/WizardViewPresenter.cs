using System.Data.Entity.Core;
using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Tabbed;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.ChargesViews.Wizard.Commands.Dispatchers;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands;
using System.Threading;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class WizardViewPresenter : BasePresenter<IWizardView>
    {
        private class CorrectionTableColumns
        {
            public static string ID = "ID";
            public static string Account = "Account";
            public static string Owner = "Owner";
            public static string ResidentsNumber = "ResidentsNumber";
            public static string Street = "Street";
            public static string House = "House";
            public static string Apartment = "Apartment";
            public static string Square = "Square";
            public static string Selected = "Selected";
            public static string Days = "Days";
            public static string Percent = "Percent";
        }

        /// <summary>
        /// Данные счетчика по дому 
        /// </summary>
        private class CommonCounterInfo
        {
            public CommonCounterInfo()
            {
                PrivateCounterInfoByCustomer = new Dictionary<int, Dictionary<string, PrivateCounterInfo>>();
            }

            /// <summary>
            /// ID общедомового счетчика
            /// </summary>
            public int CommonCounterId { get; set; }

            /// <summary>
            /// Коэффициент
            /// </summary>
            public decimal Coefficient { get; set; }

            /// <summary>
            /// Сумма частей общедомового потребления абонентов
            /// </summary>
            public decimal ConsumptionCustomerSum { get; set; }

            /// <summary>
            /// Расход
            /// </summary>
            public decimal Consumption { get; set; }

            /// <summary>
            /// Текущее показание
            /// </summary>
            public decimal CurrentValue { get; set; }

            /// <summary>
            /// Показания и расход, сгруппированные по абоненту и счетчику
            /// </summary>
            public Dictionary<int, Dictionary<string, PrivateCounterInfo>> PrivateCounterInfoByCustomer { get; private set; }
        }

        /// <summary>
        /// Данные частного счетчика
        /// </summary>
        private class PrivateCounterInfo
        {
            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="previousValue">Показание за предыдущий период</param>
            /// <param name="currentValue">Показание за текущий период</param>
            public PrivateCounterInfo(decimal previousValue, decimal currentValue, decimal rate)
            {
                PreviousValue = previousValue;
                CurrentValue = currentValue;
                Consumption = currentValue - previousValue;
                Rate = rate;
            }

            /// <summary>
            /// Показание за предыдущий период
            /// </summary>
            public decimal PreviousValue { get; private set; }

            /// <summary>
            /// Показание за текущий период
            /// </summary>
            public decimal CurrentValue { get; private set; }

            /// <summary>
            /// Расход
            /// </summary>
            public decimal Consumption { get; private set; }

            /// <summary>
            /// Доля в общедомовом потреблении
            /// </summary>
            public decimal CommonConsumptionPart { get; set; }

            /// <summary>
            /// Тариф
            /// </summary>
            public decimal Rate { get; private set; }
        }

        private class CustomerPosInfo
        {
            public int ID { get; set; }
            public int ServiceID { get; set; }
            public int ContractorID { get; set; }
            public byte ChargeRule { get; set; }
            public decimal Rate { get; set; }
        }

        private ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// Stores payment distibution service reference
        /// </summary>
        [ServiceDependency]
        public PaymentDistributionService PaymentDistributionSrv
        {
            set;
            private get;
        }

        /// <summary>
        /// Сервис работы с доменами, умеющими работать с датамаппером
        /// </summary>
        [ServiceDependency]
        public IDomainWithDataMapperHelperService DomainWithDataMapperHelperServ
        {
            set;
            private get;
        }

        /// <summary>
        /// Сервис настроек
        /// </summary>
        [ServiceDependency]
        public ISettingsService SettingsService { get; private set; }

        [ServiceDependency]
        public IExcelService ExcelService { get; set; }

        /// <summary>
        /// Признак сбоя резервного копирования
        /// </summary>
        private bool _isBackupFailed;

        public WizardViewPresenter()
        {
            _commandDispatcher = new WizardViewPresenterCommandDispatcher(ServerTime, PaymentDistributionSrv);
        }

        /// <summary>
        /// Поднимает домен по его ID
        /// </summary>
        internal T1 GetItem<T1>(string _id)
        {
            return DomainWithDataMapperHelperServ.GetItem<T1>(_id);
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
        internal void StartWizard()
        {
            View.IsMasterCompleted = false;
            View.IsMasterInProgress = false;

            PeriodInfo _periodInfo = ServerTime.GetPeriodInfo();

            View.ChargingPeriod = _periodInfo.FirstUncharged;
            View.PercentCorrectionPeriod = _periodInfo.LastCharged;

            View.Filter = FilterType.All;
            View.FoundCustomers = null;
            ClearSelectedCustomers();

            View.Street = String.Empty;
            View.House = String.Empty;
            View.Apartment = String.Empty;
            View.Account = String.Empty;
            View.ZipCode = string.Empty;
            View.WholeWord = false;

            View.ResultCount = 0;
            View.ResultValue = 0;
            View.ResultErrorCount = 0;

            View.ResetProgressBar(1);

            View.SelectPage(WizardPages.ChooseMethodPage);
        }

        /// <summary>
        /// Обрабатывает изменение шага мастера
        /// </summary>
        /// <param name="prevPage">Предыдущая страница</param>
        /// <param name="direction">Назад / Далее</param>
        /// <returns>Следующая страница мастера</returns>
        internal WizardPages OnSelectedPageChanging(WizardPages prevPage, Direction direction)
        {
            WizardPages _next = WizardPages.Unknown;

            if (direction == Direction.Forward)
            {
                PeriodInfo _periodInfo = ServerTime.GetPeriodInfo();

                switch (prevPage)
                {
                    case WizardPages.ChooseMethodPage:
                        switch (View.ChargeType)
                        {
                            case ChargeType.Regular:
                                if (IsAllCounterValuesPresent(_periodInfo.FirstUncharged, _periodInfo.LastCharged) 
                                    && IsPublicPlaceServiceVolumesFilledUp(ServerTime.GetDateTimeInfo().Now, _periodInfo.FirstUncharged))
                                {
                                    _next = WizardPages.BackupPage;
                                }
                                break;

                            case ChargeType.Correction:
                            case ChargeType.PercentCorrection:
                                _next = WizardPages.CustomersPage;
                                break;

                            case ChargeType.Debt:
                                if (string.IsNullOrEmpty(View.DebtFileName))
                                {
                                    View.ShowMessage("Не выбран файл", "Ошибка");
                                }
                                else
                                {
                                    _next = WizardPages.ProcessingPage;
                                }
                                break;
                        }
                        break;

                    case WizardPages.CustomersPage:
                        if (View.SelectedCustomers.Rows.Count == 0)
                        {
                            View.ShowMessage("Не выбран ни один абонент", "Ошибка выбора абонентов");
                        }
                        else if (View.ChargeType == ChargeType.Correction)
                        {
                            DateTime _lastChargedPeriod = _periodInfo.LastCharged;
                            View.SinceCorrectionPeriod = _lastChargedPeriod;
                            View.TillCorrectionPeriod = _lastChargedPeriod;
                            _next = WizardPages.ChoosePeriodPage;
                        }
                        else //View.ChargeType == ChargeType.PercentCorrection
                        {
                            RefreshServices();
                            View.CustomersWithPercents = View.SelectedCustomers;
                            View.CorrectingServiceID =(int?)null;
                            _next = WizardPages.PercentPage;
                        }
                        break;

                    case WizardPages.ChoosePeriodPage:
                        DateTime _lastCharged = _periodInfo.LastCharged;

                        if (View.SinceCorrectionPeriod > View.TillCorrectionPeriod)
                        {
                            View.ShowMessage("Начальный период больше конечного", "Ошибка выбора периода");
                        }
                        else if (View.SinceCorrectionPeriod > _lastCharged || View.TillCorrectionPeriod > _lastCharged)
                        {
                            View.ShowMessage(
                                string.Format("Начальный и конечный период не должны быть больше последнего периода, за который выполнены начисления ({0:MM.yyyy})", _lastCharged),
                                "Ошибка выбора периода");
                        }
                        else
                        {
                            _next = WizardPages.ProcessingPage;
                        }
                        break;

                    case WizardPages.PercentPage:
                        DateTime _correctionPeriod = View.PercentCorrectionPeriod;

                        if (_correctionPeriod > _periodInfo.LastCharged)
                        {
                            View.ShowMessage(
                                String.Format("Период для снятия начислений не должны быть больше последнего периода, за который выполнены начисления ({0:MM.yyyy})", _periodInfo.LastCharged),
                                "Ошибка выбора периода");
                        }
                        else if (!View.CorrectingServiceID.HasValue)
                        {
                            View.ShowMessage("Должна быть выбрана услуга для снятия начислений", "Ошибка выбора услуги");
                        }
                        else if (View.Service.ChargeRule == ChargeRuleType.CounterRate)
                        {
                            View.ShowMessage("Начисления не могут быть сняты по услугам, начисляемым по приборам учета", "Ошибка выбора услуги");
                        }
                        else
                        {
                            _next = WizardPages.ProcessingPage;
                        }
                        break;

                    case WizardPages.BackupPage:
                        _next = _isBackupFailed ? WizardPages.FinishPage : WizardPages.ProcessingPage;
                        break;

                    case WizardPages.ProcessingPage:
                        _next = WizardPages.FinishPage;
                        break;
                }
            }
            else
            {
                switch (prevPage)
                {
                    case WizardPages.CustomersPage:
                    case WizardPages.FinishPage:
                        _next = WizardPages.ChooseMethodPage;
                        break;
                    case WizardPages.ChoosePeriodPage:
                    case WizardPages.PercentPage:
                        _next = WizardPages.CustomersPage;
                        break;
                }
            }

            return _next;
        }

        /// <summary>
        /// Обрабатывает событие перехода на новую страницу
        /// </summary>
        /// <param name="page">Страница, на которую был осуществлен переход</param>
        /// <param name="direction">Назад / Далее</param>
        public void OnSelectedPageChanged(WizardPages page, Direction direction)
        {
            if (direction == Direction.Forward)
            {
                switch (page)
                {
                    case WizardPages.BackupPage:
                        string _backupPath = SettingsService.GetBackupPath();

                        _isBackupFailed = false;

                        if (string.IsNullOrEmpty(_backupPath))
                        {
                            OnBackupFailed("Укажите путь резервного копирования (меню \"Сервис\" > \"Настройки\")");
                        }
                        else
                        {
                            View.IsMasterInProgress = true;

                            BackgroundWorker _backupWorker = new BackgroundWorker();
                            _backupWorker.DoWork += (sender, args) =>
                            {
                                string _path = (string)args.Argument;
                                BackgroundWorker _worker = (BackgroundWorker)sender;
                                args.Result = StartBackup(_path, _worker.ReportProgress);
                            };

                            _backupWorker.WorkerReportsProgress = true;
                            _backupWorker.ProgressChanged += (sender, args) =>
                            {
                                View.SetBackupProgress(args.ProgressPercentage);
                            };

                            _backupWorker.RunWorkerCompleted += (sender, args) =>
                            {
                                bool _success = (bool)args.Result;

                                if (_success)
                                {
                                    View.SelectPage(WizardPages.ProcessingPage);
                                }
                                else
                                {
                                    OnBackupFailed("При создании резервной копии возникла ошибка");
                                }
                            };

                            _backupWorker.RunWorkerAsync(_backupPath);
                        }
                        break;

                    case WizardPages.ProcessingPage:
                        switch (View.ChargeType)
                        {
                            case ChargeType.Correction:
                                Execute(
                                    new RegisterRechargeCommand
                                    {
                                        ProgressAction = x => View.AddProgress(x),
                                        CustomerIds = View.SelectedCustomers.AsEnumerable().Select(r => r.Field<int>("ID")).ToArray(),
                                        Since = View.SinceCorrectionPeriod,
                                        Till = View.TillCorrectionPeriod,
                                        ServicePercentCorrectionByCustomer = null
                                    });
                                break;
                            case ChargeType.PercentCorrection:
                                var _rows = View.CustomersWithPercents.AsEnumerable();
                                Execute(
                                    new RegisterRechargeCommand
                                    {
                                        ProgressAction = x => View.AddProgress(x),
                                        ResetProgressBar = x => View.ResetProgressBar(x),
                                        CustomerIds = _rows.Select(r => r.Field<int>("ID")).ToArray(),
                                        Since = View.PercentCorrectionPeriod,
                                        Till = View.PercentCorrectionPeriod,
                                        ServicePercentCorrectionByCustomer = _rows
                                            .ToDictionary(
                                                r => r.Field<int>("ID"),
                                                r =>
                                                new ServicePercentCorrection
                                                {
                                                    Days = r.Field<int>("Days"),
                                                    Percent = r.Field<int>("Percent")
                                                })
                                    });
                                break;
                            case ChargeType.Regular:
                                Execute(
                                    new RegisterChargeCommand
                                    {
                                        ProgressAction = x => View.AddProgress(x)
                                    });
                                break;
                            case ChargeType.Debt:
                                RegisterDebts();
                                View.DebtFileName = string.Empty;
                                break;
                        }

                        View.IsMasterInProgress = false;
                        View.IsMasterCompleted = true;
                        View.SelectPage(WizardPages.FinishPage);
                        break;
                }
            }
        }

        /// <summary>
        /// Возвращает список абонентов по фильтру
        /// </summary>
        public void SetCustomers()
        {
            ICustomerDataMapper _dm = DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>();
            DataTable _result;

            try
            {
                switch (View.Filter)
                {
                    case FilterType.Address:
                        _result = _dm.GetList(View.Street, View.House, View.Apartment, View.WholeWord);
                        break;
                    case FilterType.Account:
                        _result = _dm.GetListByAccount(View.Account);
                        break;
                    case FilterType.ZipCode:
                        _result = _dm.GetListByZipCode(View.ZipCode);
                        break;
                    case FilterType.All:
                        _result = _dm.GetList();
                        break;
                    default:
                        _result = new DataTable();
                        break;
                }
            }
            catch (Exception _ex)
            {
                _result = new DataTable();
                View.ShowMessage(_ex.ToString(), "Ошибка загрузки абонентов");
            }

            View.FoundCustomers = _result;
        }

        private DataTable CreateCustomersTable()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add(CorrectionTableColumns.ID, typeof(int));
            _table.Columns.Add(CorrectionTableColumns.Account, typeof(string));
            _table.Columns.Add(CorrectionTableColumns.Owner, typeof(string));
            _table.Columns.Add(CorrectionTableColumns.ResidentsNumber, typeof(int));
            _table.Columns.Add(CorrectionTableColumns.Street, typeof(string));
            _table.Columns.Add(CorrectionTableColumns.House, typeof(string));
            _table.Columns.Add(CorrectionTableColumns.Apartment, typeof(string));
            _table.Columns.Add(CorrectionTableColumns.Square, typeof(string));
            _table.Columns.Add(CorrectionTableColumns.Selected, typeof(bool));
            _table.Columns.Add(CorrectionTableColumns.Days, typeof(int));
            _table.Columns.Add(CorrectionTableColumns.Percent, typeof(int));
            _table.PrimaryKey = new[] { _table.Columns[CorrectionTableColumns.ID] };

            return _table;
        }

        public void ClearSelectedCustomers()
        {
            View.SelectedCustomers = CreateCustomersTable();
        }

        /// <summary>
        /// Обновить услуги
        /// </summary>
        internal void RefreshServices()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (Entities _entities = new Entities())
            {
                foreach (DataBase.Services service in _entities.Services.OrderBy(s => s.Name))
                {
                    _table.Rows.Add(service.ID, service.Name);
                }
            }

            View.Services = _table;
        }

        private void Execute(ResultCommand<RegisterCommandResult> command)
        {
            Stopwatch _stopwatch = new Stopwatch();
            _stopwatch.Start();

            try
            {
                var _thread = new Thread(() =>
                {
                    _commandDispatcher.Execute(command);
                    ShowResult(command.Result.Processed, command.Result.Total);
                });
                _thread.Start();
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Charges. Wizard. Exception: {_ex}");
                View.ShowMessage("Операция не выполнена", "Ошибка операции");
            }

            _stopwatch.Stop();
            Logger.SimpleWrite($"Потрачено: {_stopwatch.Elapsed}");
        }

        private void ShowResult(int resultCount, decimal total)
        {
            View.ResultCount = resultCount;
            View.ResultValue = total;
        }

        /// <summary>
        /// Выполняет импорт задолжностей
        /// </summary>
        private void RegisterDebts()
        {
            int _failCount = 0;
            int _processed = 0;
            decimal _totalValue = 0;

            DateTime _now = ServerTime.GetDateTimeInfo().Now;
            PeriodInfo _periodInfo = ServerTime.GetPeriodInfo();
            DateTime _period = _periodInfo.FirstUncharged;
            DateTime _previousPeriod = _periodInfo.LastCharged;
            List<RechargeOperPoses> _posesToRegister = new List<RechargeOperPoses>();
            RechargeSets _rechargeSet = null;
            Dictionary<int, decimal> _customerDebt = new Dictionary<int, decimal>();

            try
            {
                using (Entities _db = new Entities())
                {
                    using (IExcelWorkbook _wb = ExcelService.OpenWorkbook(View.DebtFileName))
                    {
                        IExcelWorksheet _ws = _wb.Worksheet(1);
                        int _rowCount = _ws.GetRowCount();

                        for (int _row = 1; _row <= _rowCount; _row++)
                        {
                            try
                            {
                                string _account = _ws.Cell(_row, 1).Value.Trim();

                                if (!string.IsNullOrEmpty(_account))
                                {
                                    decimal _debt;
                                    if(!_ws.Cell(_row, 2).TryGetValue(out _debt))
                                    {
                                        throw new ApplicationException($"Не удалось преобразовать значение {_ws.Cell(_row, 2).Value} к типу decimal");
                                    }

                                    var _customer =
                                        _db.Customers
                                            .Where(c => c.Account == _account)
                                            .Select(
                                                c =>
                                                new
                                                {
                                                    c.ID,
                                                    BuildingID = c.Buildings.ID
                                                })
                                            .FirstOrDefault();

                                    if (_customer != null)
                                    {
                                        if (_customerDebt.ContainsKey(_customer.ID))
                                        {
                                            _customerDebt[_customer.ID] += _debt;
                                        }
                                        else
                                        {
                                            _customerDebt.Add(_customer.ID, _debt);
                                        }
                                    }
                                    else
                                    {
                                        Logger.SimpleWrite($"Can't find customer in row {_row}");
                                        _failCount++;
                                    }
                                }
                            }
                            catch (Exception _ex)
                            {
                                Logger.SimpleWrite($"Исключение при разборе строки {_row} : {_ex}");
                                _failCount++;
                            }
                        }
                    }

                    if (_customerDebt.Count > 0)
                    {
                        _rechargeSet =
                            new RechargeSets
                            {
                                CreationDateTime = _now,
                                Period = _period,
                                Number = _db.RechargeSets.Any() ? _db.RechargeSets.Max(c => c.Number) + 1 : 1,
                                Author = (Users)_db.GetObjectByKey(new EntityKey("Entities.Users", "ID", int.Parse(UserHolder.User.ID)))
                            };
                        _db.AddToRechargeSets(_rechargeSet);
                        _db.SaveChanges();
                    }
                }

                View.ResetProgressBar(_customerDebt.Keys.Count);
                Application.DoEvents();

                foreach (KeyValuePair<int, decimal> _pair in _customerDebt)
                {
                    int _customerID = _pair.Key;
                    decimal _debt = _pair.Value;

                    using (Entities _db = new Entities())
                    {
                        try
                        {
                            Customers _dbCustomer = new Customers { ID = _customerID };
                            _db.Customers.Attach(_dbCustomer);

                            var _customer =
                                _db.Customers
                                    .Where(c => c.ID == _customerID)
                                    .Select(c =>
                                        new
                                        {
                                            c.ID,
                                            c.Square,
                                            c.Account,
                                            BuildingID = c.Buildings.ID,
                                            BuildingNonResidentialPlaceArea = c.Buildings.NonResidentialPlaceArea,
                                            ResidentsCount = c.Residents.Count(),
                                            FederalBenefitResidentsCount = c.Residents
                                                    .Count(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule == 0),
                                            LocalBenefitCoefficient = c.Residents
                                                    .Where(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule != 0)
                                                    .Max(resident => resident.BenefitTypes.FixedPercent) ?? 0,
                                        })
                                    .First();

                            IList<CustomerPosValue> _distribution = new List<CustomerPosValue>();

                            var _customerPoses =
                                 _db.CustomerPoses
                                     .Where(p =>
                                         p.Customers.ID == _customer.ID &&
                                         p.Since <= _rechargeSet.Period &&
                                         p.Till >= _rechargeSet.Period)
                                     .Select(p =>
                                         new CustomerPosInfo
                                         {
                                             ID = p.ID,
                                             ServiceID = p.Services.ID,
                                             ContractorID = p.Contractors.ID,
                                             ChargeRule = p.Services.ChargeRule,
                                             Rate = p.Rate
                                         })
                                     // Необходимо для вычисления банковской комиссии расходов по сод. общ. им. после вычисления суммы начисления самих расходов
                                     .OrderBy(p => p.ChargeRule)
                                     .ToList();

                            Dictionary<int, Services> _services =
                                _db.Services
                                    .Include(s => s.ServiceTypes)
                                    .ToDictionary(s => s.ID, s => s);

                            Dictionary<int, Contractors> _contractors = _db.Contractors
                                    .ToDictionary(
                                        contractor => contractor.ID,
                                        contractor => contractor);

                            if (_customerPoses.Any())
                            {
                                foreach (var _customerPos in _customerPoses)
                                {
                                    decimal _value = 0;

                                    //Перенести правило начисления по услуге в тип услуги
                                    switch ((Service.ChargeRuleType)_customerPos.ChargeRule)
                                    {
                                        case Service.ChargeRuleType.SquareRate:
                                            _value = _customerPos.Rate * _customer.Square;
                                            break;
                                        case Service.ChargeRuleType.ResidentsRate:
                                            _value = _customerPos.Rate * _customer.ResidentsCount;
                                            break;
                                        case Service.ChargeRuleType.CounterRate:
                                            break;

                                        case Service.ChargeRuleType.PublicPlaceAreaRate:
                                            {
                                                decimal _livingArea =
                                                    _db.Customers
                                                        .Where(c =>
                                                            c.Buildings.ID == _customer.BuildingID &&
                                                            c.CustomerPoses.Any(p => p.Till >= _period))
                                                        .Sum(c => (decimal?)c.Square) ?? 0;

                                                decimal _area = _livingArea + _customer.BuildingNonResidentialPlaceArea;

                                                PublicPlaces _pp = _db.PublicPlaces
                                                    .FirstOrDefault(pp =>
                                                        pp.ServiceID == _customerPos.ServiceID && pp.BuildingID == _customer.BuildingID);

                                                decimal? _norm = _services[_customerPos.ServiceID].Norm;

                                                if (_pp != null && _norm.HasValue && _area > 0)
                                                {
                                                    decimal _rate = Math.Round(_norm.Value * _pp.Area / _area * _customerPos.Rate, 2, MidpointRounding.AwayFromZero);
                                                    _value = _customer.Square * _rate;
                                                }
                                            }
                                            break;

                                        case Service.ChargeRuleType.PublicPlaceVolumeAreaRate:
                                            {
                                                decimal _livingArea =
                                                    _db.Customers
                                                        .Where(c =>
                                                            c.Buildings.ID == _customer.BuildingID &&
                                                            c.CustomerPoses.Any(p => p.Till >= _period))
                                                        .Sum(c => (decimal?)c.Square) ?? 0;
                                                decimal _area = _livingArea + _customer.BuildingNonResidentialPlaceArea;

                                                decimal _volume = _db.PublicPlaceServiceVolumes
                                                    .Where(x => x.BuildingID == _customer.BuildingID && x.ServiceID == _customerPos.ServiceID && x.Period == _period)
                                                    .Select(x => x.Volume)
                                                    .FirstOrDefault();

                                                decimal _rate =
                                                    Math.Round(_volume / _area * _customerPos.Rate, 2, MidpointRounding.AwayFromZero);
                                                _value = _customer.Square * _rate;
                                            }
                                            break;

                                        case Service.ChargeRuleType.PublicPlaceBankCommission:
                                            {
                                                decimal _publicPlaceAreaRateSum = _customerPoses
                                                    .Where(p => p.ChargeRule == (byte)Service.ChargeRuleType.PublicPlaceAreaRate)
                                                    .Sum(p => p.Rate);
                                                decimal _rate = Math.Round(_publicPlaceAreaRateSum * _customerPos.Rate / 100, 2, MidpointRounding.AwayFromZero);
                                                _value = _rate * _customer.Square;
                                                // Заменяем тариф для внесения в квитанцию 
                                                _customerPos.Rate = _rate;
                                            }
                                            break;
                                        case Service.ChargeRuleType.FixedRate:
                                        default:
                                            _value = _customerPos.Rate;
                                            break;
                                    }

                                    if (_value > 0)
                                    {
                                        _distribution.Add(
                                            new CustomerPosValue
                                            {
                                                CustomerPos = _customerPos,
                                                Value = _value
                                            });
                                    }
                                }

                                decimal _distributionSum = _distribution.Sum(p => p.Value);

                                if (_distributionSum > 0)
                                {
                                    decimal _coefficient = _debt / _distributionSum;

                                    _distributionSum = 0;

                                    foreach (CustomerPosValue _customerPosValue in _distribution)
                                    {
                                        _customerPosValue.Value = _coefficient * _customerPosValue.Value;

                                        _distributionSum += _customerPosValue.Value;

                                        if (_distributionSum > _debt)
                                        {
                                            _customerPosValue.Value -= _distributionSum - _debt;
                                            _distributionSum = _debt;
                                        }
                                    }

                                    if (_distributionSum < _debt)
                                    {
                                        _distribution.First().Value += _debt - _distributionSum;
                                        _distributionSum = _debt;
                                    }

                                    #region Дополнительные начисления

                                    _rechargeSet = (RechargeSets)_db.GetObjectByKey(new EntityKey("Entities.RechargeSets", "ID", _rechargeSet.ID));

                                    RechargeOpers _rechargeOper =
                                        new RechargeOpers
                                        {
                                            RechargeSets = _rechargeSet,
                                            CreationDateTime = _now,
                                            Customers = _dbCustomer,
                                            Value = Math.Round(_distributionSum, 2, MidpointRounding.AwayFromZero)
                                        };
                                    _db.AddToRechargeOpers(_rechargeOper);

                                    foreach (CustomerPosValue _customerPosValue in _distribution)
                                    {
                                        RechargeOperPoses _pos = new RechargeOperPoses
                                        {
                                            Services = _services[_customerPosValue.CustomerPos.ServiceID],
                                            Contractors = _contractors[_customerPosValue.CustomerPos.ContractorID],
                                            RechargeOpers = _rechargeOper,
                                            Value = Math.Round(_customerPosValue.Value, 2, MidpointRounding.AwayFromZero)
                                        };
                                        _db.AddToRechargeOperPoses(_pos);
                                        _posesToRegister.Add(_pos);
                                    }

                                    _db.SaveChanges();

                                    _rechargeSet.Quantity++;
                                    _rechargeSet.ValueSum += _rechargeOper.Value;

                                    _db.SaveChanges();

                                    _totalValue = _rechargeSet.ValueSum;

                                    _posesToRegister.Clear();

                                    #endregion
                                }

                                _processed++;
                            }
                            else
                            {
                                Logger.SimpleWrite($"Отсутствуют услуги у абонента с л/с {_customer.Account}");
                                _failCount++;
                            }
                        }
                        catch (Exception _ex)
                        {
                            Logger.SimpleWrite($"Абонент {_customerID}. Exception: {_ex}");
                            _failCount++;
                        }
                    }

                    View.AddProgress(1);
                    Application.DoEvents();
                }

                View.ResultCount = _processed;
                View.ResultValue = _totalValue;
                View.ResultErrorCount = _failCount;
            }
            catch (Exception _ex)
            {
                View.ShowMessage("Импорт долгов не выполнен", "Ошибка операции");
                Logger.SimpleWrite($"Debts import error: {_ex}");
            }
        }

        /// <summary>
        /// Проверяет наличие показаний по всем счетчикам за текущий период.
        /// Предлагает скопировать показания за предыдущий период, при отсутствии оных
        /// </summary>
        /// <param name="currentPeriod">Текущий период</param>
        /// <param name="previousPeriod">Предыдущий период</param>
        /// <returns>
        /// true - все показания введены или скопированы, 
        /// false - введены не все показания, пользователь отказался скоприровать предыдущие показания
        /// </returns>
        private bool IsAllCounterValuesPresent(DateTime currentPeriod, DateTime previousPeriod)
        {
            bool _result = true;

            using (Entities _entities = new Entities())
            {
                var _commonCounters =
                    _entities.CommonCounters
                        .Where(
                            c =>
                            !_entities.CommonCounterValues
                                .Any(
                                    v =>
                                    v.Period == currentPeriod &&
                                    v.CommonCounters.ID == c.ID))
                        .ToList();

                var _privateCounters =
                    _entities.PrivateCounters
                        .Where(
                            c =>
                            !_entities.PrivateCounterValues
                                .Any(
                                    v =>
                                    v.Period == currentPeriod &&
                                    v.PrivateCounters.ID == c.ID))
                        .ToList();

                if (_commonCounters.Any() || _privateCounters.Any())
                {
                    DialogResult _dialogResult = MessageBox.Show(
                        "Введены показания не всех приборов учета за текущий учетный период. Использовать показания предыдущего учетного периода?",
                        "Отсутствуют показания по некоторым приборам учета",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (_dialogResult == DialogResult.Yes)
                    {
                        foreach (CommonCounters _commonCounter in _commonCounters)
                        {
                            decimal _value =
                                _entities.CommonCounterValues
                                    .Where(
                                        v =>
                                        v.CommonCounters.ID == _commonCounter.ID &&
                                        v.Period == previousPeriod)
                                    .Select(v => v.Value)
                                    .FirstOrDefault();

                            _entities.AddToCommonCounterValues(
                                new CommonCounterValues
                                {
                                    Period = currentPeriod,
                                    Value = _value,
                                    CommonCounters = _commonCounter
                                });
                        }

                        foreach (PrivateCounters _privateCounter in _privateCounters)
                        {
                            decimal _value =
                                _entities.PrivateCounterValues
                                    .Where(
                                        v =>
                                        v.PrivateCounters.ID == _privateCounter.ID &&
                                        v.Period == previousPeriod)
                                    .Select(v => v.Value)
                                    .FirstOrDefault();

                            _entities.AddToPrivateCounterValues(
                                new PrivateCounterValues
                                {
                                    Period = currentPeriod,
                                    Value = _value,
                                    PrivateCounters = _privateCounter
                                });
                        }

                        _entities.SaveChanges();
                    }
                    else
                    {
                        _result = false;
                        View.ShowMessage("Выполнение начислений невозможно, введите показания всех приборов учета за текущий период", "Ошибка");
                    }
                }
            }

            return _result;
        }

        private bool IsPublicPlaceServiceVolumesFilledUp(DateTime now, DateTime period)
        {
            bool _result;
            bool _needValidation;
            
            using (Entities _db = new Entities())
            {
                _needValidation = _db.CustomerPoses.Any(x => x.Till >= now && x.Services.ChargeRule == (byte)Service.ChargeRuleType.PublicPlaceVolumeAreaRate);
                _result = _db.PublicPlaceServiceVolumes.Any(x => x.Period == period);
            }

            if(_needValidation && !_result)
            {
                DialogResult _dialogResult = MessageBox.Show(
                        "Отсутсвуют данные по потребленным объемам коммунальных услуг при содержании общедомового имущества за начисляемый период. Продолжить?",
                        "Отсутствуют данные объемов комм. услуг при СОД",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                _result = _dialogResult == DialogResult.Yes;
            }
            else
            {
                _result = true;
            }

            return _result;
        }

        public void AddToSelectedCustomers()
        {
            DataTable _foundCustomers = View.FoundCustomers;
            DataTable _selectedCustomers = View.SelectedCustomers;

            DataRow[] _checkedRows = _foundCustomers.Select("Selected = True");

            foreach (DataRow _row in _checkedRows)
            {
                if (!_selectedCustomers.Rows.Contains(_row["ID"]))
                {
                    _selectedCustomers.Rows.Add(
                        _row[CorrectionTableColumns.ID],
                        _row[CorrectionTableColumns.Account],
                        _row[CorrectionTableColumns.Owner],
                        _row[CorrectionTableColumns.ResidentsNumber],
                        _row[CorrectionTableColumns.Street],
                        _row[CorrectionTableColumns.House],
                        _row[CorrectionTableColumns.Apartment],
                        _row[CorrectionTableColumns.Square],
                        false,
                        1,
                        100);
                }
            }

            View.SelectedCustomers = _selectedCustomers;
        }

        public void DeleteFromSelectedCustomers()
        {
            DataTable _selectedCustomers = View.SelectedCustomers;
            DataRow[] _rows = _selectedCustomers.Select("Selected = True");

            foreach (DataRow _row in _rows)
            {
                _selectedCustomers.Rows.Remove(_row);
            }

            View.SelectedCustomers = _selectedCustomers;
        }

        public void ChangeAllFoundCustomersSelect(bool selected)
        {
            foreach (DataRow _row in View.FoundCustomers.Rows)
            {
                _row["Selected"] = selected;
            }
        }

        /// <summary>
        /// Запускает процесс резервного копирования базы данных
        /// </summary>
        /// <param name="backupPath">Путь резервного копирования</param>
        /// <param name="progressAction">Метод, вызываемый при изменении прогресса резервного копирования</param>
        /// <returns>Признак успешного завершения процесса</returns>
        private bool StartBackup(string backupPath, Action<int> progressAction)
        {
            bool _result;

            try
            {
                DateTime _now = ServerTime.GetDateTimeInfo().Now;

                using (var _db = new Entities())
                {
                    int _sqlErrors = 0;

                    var _entityConnection = (EntityConnection)_db.Connection;
                    var _sqlConnection = (SqlConnection)_entityConnection.StoreConnection;
                    _sqlConnection.FireInfoMessageEventOnUserErrors = true;
                    _sqlConnection.InfoMessage += (sender, args) =>
                    {
                        if (!ProcessSqlConnectionInfoMessage(args.Errors, progressAction))
                        {
                            _sqlErrors++;
                        }
                    };

                    string _fileName = $"{_sqlConnection.Database}_{_now:yyyy-MM-dd_HH-mm-ss}.bak";

                    _db.CommandTimeout = 3600;
                    _db.ContextOptions.EnsureTransactionsForFunctionsAndCommands = false;
                    _db.BackupDatabase(_sqlConnection.Database, Path.Combine(backupPath, _fileName), 5);

                    _result = _sqlErrors == 0;
                }
            }
            catch (Exception _ex)
            {
                Logger.Write($"WizardViewPresenter.StartBackup(): {_ex}");
                _result = false;
            }

            return _result;
        }

        /// <summary>
        /// Обрабатывет информационные сообщения, поступающие от SQL-сервера 
        /// </summary>
        /// <param name="errors">Коллекция сообщений (ошибок)</param>
        /// <param name="progressAction">Метод, вызываемый при изменении прогресса резервного копирования</param>
        /// <returns>true - информационное сообщение, false - сообщение об ошибке</returns>
        private bool ProcessSqlConnectionInfoMessage(SqlErrorCollection errors, Action<int> progressAction)
        {
            if (errors.Count == 0)
            {
                return true;
            }

            bool _result = true;
            var _error = errors[0];
            Regex _regex = new Regex("^\\d{1,3}", RegexOptions.Compiled);

            if (_error.Class == 0)
            {
                if (_regex.IsMatch(_error.Message))
                {
                    int _progress = int.Parse(_regex.Match(_error.Message).Value);
                    progressAction(_progress);
                }
            }
            else
            {
                string _msg = "WizardViewPresenter.ProcessSqlConnectionInfoMessage(): {0}, line {1}: {2}";
                Logger.Write(string.Format(_msg, _error.Procedure, _error.LineNumber, _error.Message));
                _result = false;
            }

            return _result;
        }

        /// <summary>
        /// При ошибке резервного копирования показывает сообщение и завершает работу мастера 
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        private void OnBackupFailed(string message)
        {
            View.ShowMessage(message, "Ошибка");
            _isBackupFailed = true;
            View.IsMasterInProgress = false;
            View.IsMasterCompleted = true;
            View.ResultErrorCount = 1;
            View.SelectPage(WizardPages.FinishPage);
        }

        public void SetCorrectionDaysCount(int daysCount)
        {
            DataTable _tbl = View.CustomersWithPercents;
            for(int i = 0; i < _tbl.Rows.Count; i++)
            {
                _tbl.Rows[i][CorrectionTableColumns.Days] = daysCount;
            }
        }

        public void SetCorrectionPercent(int percent)
        {
            DataTable _tbl = View.CustomersWithPercents;
            for (int i = 0; i < _tbl.Rows.Count; i++)
            {
                _tbl.Rows[i][CorrectionTableColumns.Percent] = percent;
            }
        }

        public void UpdateCorrectionTable(int serviceID, DateTime period)
        {
            int[] _ids = View.SelectedCustomers.AsEnumerable().Select(r => (int)r[CorrectionTableColumns.ID]).ToArray();
            DataTable _table = CreateCustomersTable();

            using (Entities _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _customers= _db.ChargeOperPoses
                    .Where(p => _ids.Contains(p.ChargeOpers.Customers.ID) && p.ChargeOpers.ChargeSets.Period == period && p.Services.ID == serviceID)
                    .Select(p => p.ChargeOpers.Customers.ID)
                    .Distinct()
                    .Join(
                        _db.Customers,
                        x => x,
                        c => c.ID,
                        (x, c) =>
                        new
                        {
                            c.ID,
                            c.Account,
                            Owner = c.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson
                                ? c.PhysicalPersonFullName
                                : c.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson
                                    ? c.JuridicalPersonFullName
                                    : "Неизвестен",
                            Street = c.Buildings.Streets.Name,
                            Building = c.Buildings.Number,
                            c.Apartment
                        })
                    .OrderBy(c => c.Street)
                    .ThenBy(c => c.Building)
                    .ThenBy(c => c.Apartment)
                    .ToList();

                foreach(var _customer in _customers)
                {
                    _table.Rows.Add(
                        _customer.ID,
                        _customer.Account,
                        _customer.Owner,
                        0,
                        _customer.Street,
                        _customer.Building,
                        _customer.Apartment,
                        0,
                        false,
                        1,
                        100);
                }
            }

            View.CustomersWithPercents = _table;
        }
    }
}