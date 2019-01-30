using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Tabbed;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.DbBackup;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Factories;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Services;

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
        /// Признак сбоя резервного копирования
        /// </summary>
        private bool _isBackupFailed;

        /// <summary>
        /// Сервис распределения платежа
        /// </summary>
        private readonly PaymentDistributionService _pds;

        /// <summary>
        /// Сервис доступа к данным
        /// </summary>
        private readonly IDomainWithDataMapperHelperService _dmService;

        /// <summary>
        /// Сервис настроек
        /// </summary>
        private readonly ISettingsService _settingsService;

        /// <summary>
        /// Сервис для работы с Excel таблицами
        /// </summary>
        private readonly IExcelService _excelService;

        /// <summary>
        /// Сервис для создания диспетчера команд
        /// </summary>
        private readonly IWizardCommandDispatcherFactory _commandDispatcherFactory;
        private readonly IWizardService _wizardService;

        [InjectionConstructor]
        public WizardViewPresenter(
            [ServiceDependency]PaymentDistributionService pds,
            [ServiceDependency]IDomainWithDataMapperHelperService dmService,
            [ServiceDependency]ISettingsService settingsService,
            [ServiceDependency]IExcelService excelService,
            [ServiceDependency]IWizardCommandDispatcherFactory commandDispatcherFactory,
            [ServiceDependency]IWizardService wizardService)
        {
            _pds = pds;
            _dmService = dmService;
            _settingsService = settingsService;
            _excelService = excelService;
            _commandDispatcherFactory = commandDispatcherFactory;
            _wizardService = wizardService;
        }

        /// <summary>
        /// Поднимает домен по его ID
        /// </summary>
        public T1 GetItem<T1>(string _id)
        {
            return _dmService.GetItem<T1>(_id);
        }

        /// <summary>
        /// Завершает работу мастера
        /// </summary>
        public void FinishWizard()
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
            View.IsMasterCompleted = false;
            View.IsMasterInProgress = false;

            PeriodInfo _periodInfo = ServerTime.GetPeriodInfo();

            View.ChargingPeriod = _periodInfo.FirstUncharged;
            View.PercentCorrectionPeriod = _periodInfo.LastCharged;

            View.Filter = FilterType.All;
            View.FoundCustomers = null;
            ClearSelectedCustomers();

            View.Street = string.Empty;
            View.House = string.Empty;
            View.Apartment = string.Empty;
            View.Account = string.Empty;
            View.ZipCode = string.Empty;
            View.WholeWord = false;

            View.ResultCount = 0;
            View.ResultValue = 0;
            View.ResultErrorCount = 0;

            View.ResetProgressBar(1);

            View.SelectPage(WizardPages.ChooseMethodPage);
        }

        private WizardPages OnMoveFromChooseMethodPage()
        {
            WizardPages _next = WizardPages.Unknown;
            switch (View.ChargeType)
            {
                case ChargeType.Regular:
                    PeriodInfo _periodInfo = ServerTime.GetPeriodInfo();
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

            return _next;
        }

        private WizardPages OnMoveFromCustomersPage()
        {
            WizardPages _next = WizardPages.Unknown;
            if (View.SelectedCustomers.Rows.Count == 0)
            {
                View.ShowMessage("Не выбран ни один абонент", "Ошибка выбора абонентов");
            }
            else if (View.ChargeType == ChargeType.Correction)
            {
                DateTime _lastChargedPeriod = ServerTime.GetPeriodInfo().LastCharged;
                View.SinceCorrectionPeriod = _lastChargedPeriod;
                View.TillCorrectionPeriod = _lastChargedPeriod;
                _next = WizardPages.ChoosePeriodPage;
            }
            else //View.ChargeType == ChargeType.PercentCorrection
            {
                View.Services = _wizardService.GetServiceTable();
                View.CustomersWithPercents = View.SelectedCustomers;
                View.CorrectingServiceID = null;
                _next = WizardPages.PercentPage;
            }

            return _next;
        }

        private WizardPages OnMoveFromChoosePeriodPage()
        {
            WizardPages _next = WizardPages.Unknown;

            DateTime _lastCharged = ServerTime.GetPeriodInfo().LastCharged;

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

            return _next;
        }

        private WizardPages OnMoveFromPercentPage()
        {
            WizardPages _next = WizardPages.Unknown;

            DateTime _correctionPeriod = View.PercentCorrectionPeriod;
            DateTime _lastChargedPeriod = ServerTime.GetPeriodInfo().LastCharged;

            if (_correctionPeriod > _lastChargedPeriod)
            {
                View.ShowMessage(
                    $"Период для снятия начислений не должны быть больше последнего периода, за который выполнены начисления ({_lastChargedPeriod:MM.yyyy})",
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

            return _next;
        }

        /// <summary>
        /// Обрабатывает изменение шага мастера
        /// </summary>
        /// <param name="prevPage">Предыдущая страница</param>
        /// <param name="direction">Назад / Далее</param>
        /// <returns>Следующая страница мастера</returns>
        public WizardPages OnSelectedPageChanging(WizardPages prevPage, Direction direction)
        {
            WizardPages _next = WizardPages.Unknown;

            if (direction == Direction.Forward)
            {
                switch (prevPage)
                {
                    case WizardPages.ChooseMethodPage:
                        _next = OnMoveFromChooseMethodPage();
                        break;

                    case WizardPages.CustomersPage:
                        _next = OnMoveFromCustomersPage();
                        break;

                    case WizardPages.ChoosePeriodPage:
                        _next = OnMoveFromChoosePeriodPage();
                        break;

                    case WizardPages.PercentPage:
                        _next = OnMoveFromPercentPage();
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

        private void RunCharges()
        {
            switch (View.ChargeType)
            {
                case ChargeType.Correction:
                    Execute(
                        new RegisterRechargeCommand
                        {
                            ProgressAction = View.AddProgress,
                            ResetProgressBar = View.ResetProgressBar,
                            CustomerIds = View.SelectedCustomers.AsEnumerable().Select(r => r.Field<int>("ID")).ToArray(),
                            Since = View.SinceCorrectionPeriod,
                            Till = View.TillCorrectionPeriod,
                            Now = ServerTime.GetDateTimeInfo().Now,
                            Period = ServerTime.GetPeriodInfo().FirstUncharged,
                            ServicePercentCorrectionByCustomer = null,
                            AuthorId = int.Parse(UserHolder.User.ID)
                        });
                    break;
                case ChargeType.PercentCorrection:
                    var _rows = View.CustomersWithPercents.AsEnumerable();
                    Execute(
                        new RegisterRechargeCommand
                        {
                            ProgressAction = View.AddProgress,
                            ResetProgressBar = View.ResetProgressBar,
                            CustomerIds = _rows.Select(r => r.Field<int>("ID")).ToArray(),
                            Since = View.PercentCorrectionPeriod,
                            Till = View.PercentCorrectionPeriod,
                            Now = ServerTime.GetDateTimeInfo().Now,
                            Period = ServerTime.GetPeriodInfo().FirstUncharged,
                            ServicePercentCorrectionByCustomer = _rows
                                .ToDictionary(
                                    r => r.Field<int>("ID"),
                                    r =>
                                    new ServicePercentCorrection
                                    {
                                        Days = r.Field<int>("Days"),
                                        Percent = r.Field<int>("Percent"),
                                        ServiceId = View.CorrectingServiceID.Value
                                    }),
                            AuthorId = int.Parse(UserHolder.User.ID)
                        });
                    break;
                case ChargeType.Regular:
                    Execute(
                        new RegisterChargeCommand
                        {
                            ProgressAction = View.AddProgress,
                            ResetProgressBar = View.ResetProgressBar,
                            Now = ServerTime.GetDateTimeInfo().Now,
                            Period = ServerTime.GetPeriodInfo().FirstUncharged,
                            LastChargedPeriod = ServerTime.GetPeriodInfo().LastCharged,
                            AuthorId = int.Parse(UserHolder.User.ID)
                        });
                    break;
                case ChargeType.Debt:
                    Execute(
                        new RegisterDebtCommand
                        {
                            File = View.DebtFileName,
                            Now = ServerTime.GetDateTimeInfo().Now,
                            Period = ServerTime.GetPeriodInfo().FirstUncharged,
                            ProgressAction = View.AddProgress,
                            ResetProgressBar = View.ResetProgressBar,
                            AuthorId = int.Parse(UserHolder.User.ID)
                        });
                    View.DebtFileName = string.Empty;
                    break;
            }
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
                        View.IsMasterInProgress = true;
                        _commandDispatcherFactory.Create(ServerTime, _pds, _excelService).Execute(
                            new BackupDbCommand
                            {
                                BackupPath = _settingsService.GetBackupPath(),
                                OnFailedAction = OnBackupFailed,
                                OnProgressChanged = View.SetBackupProgress,
                                OnSuccess = () => View.SelectPage(WizardPages.ProcessingPage)
                            });
                        break;

                    case WizardPages.ProcessingPage:
                        RunCharges();
                        break;
                }
            }
        }

        /// <summary>
        /// Возвращает список абонентов по фильтру
        /// </summary>
        public void SetCustomers()
        {
            ICustomerDataMapper _dm = _dmService.DataMapper<Customer, ICustomerDataMapper>();
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

        private void Execute(ResultCommand<RegisterCommandResult> cmd)
        {
            var _thread = new Thread(() =>
            {
                Stopwatch _stopwatch = new Stopwatch();
                _stopwatch.Start();

                try
                {
                    View.IsMasterInProgress = true;

                    var _commandDispatcher = _commandDispatcherFactory.Create(ServerTime, _pds, _excelService);
                    _commandDispatcher.Execute(cmd);
                }
                catch (Exception ex)
                {
                    Logger.SimpleWrite($"Charges. Wizard. Exception: {ex}");
                    View.ShowMessage("Операция не выполнена", "Ошибка операции");
                }

                ShowResult(cmd.Result.Processed, cmd.Result.Errors, cmd.Result.Total);

                _stopwatch.Stop();
                Logger.SimpleWrite($"Потрачено: {_stopwatch.Elapsed}");
            });
            _thread.Start();
        }

        private void ShowResult(int resultCount, int errorCount, decimal total)
        {
            View.ResultCount = resultCount;
            View.ResultValue = total;
            View.ResultErrorCount = errorCount;
            View.IsMasterInProgress = false;
            View.IsMasterCompleted = true;
            View.SelectPage(WizardPages.FinishPage);
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

                        DateTime _now = ServerTime.GetDateTimeInfo().Now;

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
                                    CollectDate = _now,
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
                _needValidation = _db.CustomerPoses.Any(x => x.Till >= now && x.Services.ChargeRule == (byte)ChargeRuleType.PublicPlaceVolumeAreaRate);
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

                var _customers = _db.GetPercentCorrectionCustomerData(_ids, serviceID, period);

                foreach (var _customer in _customers)
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