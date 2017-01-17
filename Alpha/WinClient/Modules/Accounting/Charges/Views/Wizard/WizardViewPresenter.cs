using System.Data.Entity.Core;
using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects.DataClasses;
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

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class WizardViewPresenter : BasePresenter<IWizardView>
    {
        /// <summary>
        /// Информация об индексе
        /// </summary>
        private class ZipCodeInfo
        {
            /// <summary>
            /// Почтовый индекс
            /// </summary>
            public string ZipCode { get; set; }

            /// <summary>
            /// Дома
            /// </summary>
            public IEnumerable<BuildingInfo> Buildings { get; set; }
        }

        /// <summary>
        /// Информация о доме
        /// </summary>
        private class BuildingInfo
        {
            /// <summary>
            /// ID здания
            /// </summary>
            public int BuildingID { get; set; }

            /// <summary>
            /// Номер здания
            /// </summary>
            public string BuildingNumber { get; set; }

            /// <summary>
            /// Улица
            /// </summary>
            public string StreetName { get; set; }

            /// <summary>
            /// Количество абонентов
            /// </summary>
            public int CustomersCount { get; set; }

            /// <summary>
            /// Общая площадь здания (жилая + нежилая)
            /// </summary>
            public decimal Area { get; set; }

            /// <summary>
            /// Площадь мест общего пользования по услугам
            /// </summary>
            public List<PublicPlaces> PublicPlaces { get; set; }
        }

        /// <summary>
        /// Сумма начисления по позиции абонента
        /// </summary>
        private class CustomerPosValue
        {
            /// <summary>
            /// Позиция абонента
            /// </summary>
            public CustomerPoses CustomerPos
            {
                set;
                get;
            }

            /// <summary>
            /// Сумма начисления
            /// </summary>
            public decimal Value
            {
                set;
                get;
            }
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

        /// <summary>
        /// Признак сбоя резервного копирования
        /// </summary>
        private bool _isBackupFailed;

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
                                if (IsAllCounterValuesPresent(_periodInfo.FirstUncharged, _periodInfo.LastCharged))
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
                        else if (View.Service == null)
                        {
                            View.ShowMessage("Должна быть выбрана услуга для снятия начислений", "Ошибка выбора услуги");
                        }
                        else if (View.Service.ChargeRule == Service.ChargeRuleType.CounterRate)
                        {
                            View.ShowMessage("Начисления не могут быть сняты по услугам, начисляемым по приборам учета", "Ошибка выбора услуги");
                        }
                        else
                        {
                            bool _moreThanDaysInMonth = false;
                            bool _noCharges = false;
                            int _serviceID = Int32.Parse(View.Service.ID);

                            using (Entities _entities = new Entities())
                            {
                                foreach (DataRow _row in View.CustomersWithPercents.Rows)
                                {
                                    int _customerID = Convert.ToInt32(_row["ID"]);

                                    if (!_entities.ChargeOperPoses.Any(pos =>
                                        pos.ChargeOpers.ChargeSets.Period == _correctionPeriod &&
                                        pos.ChargeOpers.Customers.ID == _customerID &&
                                        pos.Services.ID == _serviceID))
                                    {
                                        _noCharges = true;
                                    }

                                    if (Convert.ToInt32(_row["Days"]) > DateTime.DaysInMonth(_correctionPeriod.Year, _correctionPeriod.Month))
                                    {
                                        _moreThanDaysInMonth = true;
                                    }
                                }
                            }

                            if (_noCharges)
                            {
                                View.ShowMessage("Не у всех выбранных абонентов были сделаны начисления в выбранном периоде", "Ошибка выбора абонентов");
                            }
                            else if (_moreThanDaysInMonth)
                            {
                                View.ShowMessage("Количество дней снятия начислений не должно превышать количество дней в выбранном периоде", "Ошибка выбора дней");
                            }
                            else
                            {
                                _next = WizardPages.ProcessingPage;
                            }
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
                        _next = WizardPages.ChooseMethodPage;
                        break;
                    case WizardPages.ChoosePeriodPage:
                        _next = WizardPages.CustomersPage;
                        break;
                    case WizardPages.PercentPage:
                        _next = WizardPages.CustomersPage;
                        break;
                    case WizardPages.FinishPage:
                        _next = WizardPages.ChooseMethodPage;
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
                            case ChargeType.PercentCorrection:
                                RegisterRecharges();
                                break;
                            case ChargeType.Regular:
                                RegisterCharges();
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

        public void ClearSelectedCustomers()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Account", typeof(string));
            _table.Columns.Add("Owner", typeof(string));
            _table.Columns.Add("ResidentsNumber", typeof(int));
            _table.Columns.Add("Street", typeof(string));
            _table.Columns.Add("House", typeof(string));
            _table.Columns.Add("Apartment", typeof(string));
            _table.Columns.Add("Square", typeof(string));
            _table.Columns.Add("Selected", typeof(bool));
            _table.Columns.Add("Days", typeof(int));
            _table.Columns.Add("Percent", typeof(int));
            _table.PrimaryKey = new[] { _table.Columns["ID"] };

            View.SelectedCustomers = _table;
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
                foreach (Services service in _entities.Services)
                {
                    _table.Rows.Add(service.ID, service.Name);
                }
            }

            View.Services = _table;
        }

        /// <summary>
        /// Выполняет начисления
        /// </summary>
        public void RegisterCharges()
        {
            const int CONTRACTOR_CONTACT_INFO_SERVICE_ID = 6;
            const int MADIX_CONTRACTOR_ID = 8; /* ООО "Мадикс" */

            Stopwatch _stopwatch = new Stopwatch();
            _stopwatch.Start();

            try
            {
                DateTimeInfo _dateTimeInfo = ServerTime.GetDateTimeInfo();
                PeriodInfo _periodInfo = ServerTime.GetPeriodInfo();

                DateTime _now = _dateTimeInfo.Now;
                DateTime _currentPeriod = _periodInfo.FirstUncharged;
                DateTime _lastChargedPeriod = _periodInfo.LastCharged;
                DateTime _payBefore = new DateTime(_currentPeriod.Year, _currentPeriod.Month, 10).AddMonths(1);

                ZipCodeInfo[] _zipCodes;
                int _chargeSetId, _billSetId;
                int _resultCount = 0;
                decimal _totalSum = 0;

                using (Entities _entities = new Entities())
                {
                    _zipCodes = _entities.Buildings
                        .Select(b => new
                        {
                            b.ZipCode,
                            BuildingID = b.ID,
                            BuildingNumber = b.Number,
                            StreetName = b.Streets.Name,
                            CustomersCount = b.Customers.Count,
                            Area = 
                                b.Customers.Where(c => c.CustomerPoses.Any(p => p.Till >= _currentPeriod)).Sum(c => (decimal?)c.Square) ?? 0 + b.NonResidentialPlaceArea,
                            b.PublicPlaces
                        })
                        .GroupBy(building => building.ZipCode)
                        .Select(groupedByZipCode => new ZipCodeInfo
                        {
                            ZipCode = groupedByZipCode.Key,
                            Buildings = groupedByZipCode
                                .Select(b => new BuildingInfo
                                {
                                    BuildingID = b.BuildingID,
                                    BuildingNumber = b.BuildingNumber,
                                    StreetName = b.StreetName,
                                    CustomersCount = b.CustomersCount,
                                    Area = b.Area,
                                    PublicPlaces = b.PublicPlaces.ToList()
                                })
                        })
                        .ToArray();

                    View.ResetProgressBar(_zipCodes.Sum(zipCode => zipCode.Buildings.Sum(building => building.CustomersCount)));

                    ChargeSets _chargeSet = new ChargeSets
                    {
                        CreationDateTime = _now,
                        Period = _currentPeriod,
                        Number = _entities.ChargeSets.Any() ? _entities.ChargeSets.Max(c => c.Number) + 1 : 1,
                        Author = (Users)_entities.GetObjectByKey(new EntityKey("Entities.Users", "ID", int.Parse(UserHolder.User.ID)))
                    };
                    _entities.AddToChargeSets(_chargeSet);
                    _entities.SaveChanges();
                    _chargeSetId = _chargeSet.ID;
                }

                foreach (ZipCodeInfo _zipCode in _zipCodes)
                {
                    using (Entities _entities = new Entities())
                    {
                        BillSets _billSet = new BillSets()
                        {
                            CreationDateTime = _now,
                            Number = _entities.BillSets.Any() ? _entities.BillSets.Max(c => c.Number) + 1 : 1,
                            BillType = (byte)BillSet.BillTypes.Regular,
                        };
                        _entities.AddToBillSets(_billSet);
                        _entities.SaveChanges();
                        _billSetId = _billSet.ID;
                    }

                    foreach (BuildingInfo _building in _zipCode.Buildings)
                    {
                        using (Entities _entities = new Entities())
                        {
                            _entities.CommandTimeout = 3600;

                            try
                            {
                                ChargeSets _chargeSet = _entities.ChargeSets.First(c => c.ID == _chargeSetId);
                                BillSets _billSet = _entities.BillSets.First(b => b.ID == _billSetId);

                                Dictionary<int, Services> _services = _entities.Services
                                    .Include("ServiceTypes")
                                    .ToDictionary(
                                        service => service.ID,
                                        service => service);

                                Dictionary<int, Contractors> _contractors = _entities.Contractors
                                    .ToDictionary(
                                        contractor => contractor.ID,
                                        contractor => contractor);

                                var _customers = _entities.Customers
                                    .Where(customer => customer.Buildings.ID == _building.BuildingID)
                                    .Select(customer => new
                                    {
                                        Customer = customer,
                                        ResidentsCount = customer.Residents.Count(),
                                        FederalBenefitResidentsCount = customer.Residents
                                            //Отдельная задача - Убрать лишние структуры данных из льгот
                                            .Count(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule == 0),
                                        LocalBenefitCoefficient = customer.Residents
                                            .Where(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule != 0)
                                            .Max(resident => resident.BenefitTypes.FixedPercent) ?? 0,
                                        //Отдельная задача - Новая структура данных для позиций абонента в виде регистра; выбирать данные по услугам один раз по всем абонентам вместе с данными по домам и улицам
                                        CustomerPoses = customer.CustomerPoses
                                            .Where(p =>
                                                p.Since <= _currentPeriod &&
                                                p.Till >= _currentPeriod)
                                            .Select(customerPos => new
                                            {
                                                ServiceID = customerPos.Services.ID,
                                                ServiceTypeID = customerPos.Services.ServiceTypes.ID,
                                                ContractorID = customerPos.Contractors.ID,
                                                //Отдельная задача - Перенести правило начисления по услуге в тип услуги
                                                customerPos.Rate
                                            }),
                                    })
                                    .ToList();

                                //Отдельная задача - соединить с предыдущим запросом по данным абонентов в доме, чтобы сократить обращения в БД
                                Dictionary<int, CommonCounterInfo> _counterInfoByService =
                                    GetBuildingCountersInfo(
                                        _building.BuildingID,
                                        _customers.Select(x => x.Customer.ID),
                                        _currentPeriod,
                                        _lastChargedPeriod);

                                foreach (var _customer in _customers)
                                {
                                    List<RegularBillDocCounterPoses> _counterBillPoses = new List<RegularBillDocCounterPoses>();
                                    List<RegularBillDocSharedCounterPoses> _sharedCounterPoses = new List<RegularBillDocSharedCounterPoses>();
                                    List<RegularBillDocPublicPlacePoses> _publicPlacePoses = new List<RegularBillDocPublicPlacePoses>();

                                    //Отдельная задача - хранить все готовые вычисления в данных по абонентам, и при начислении их просто копировать
                                    decimal _benefitNormalSquare,
                                            _benefitSquare,
                                            _extraSquare;

                                    CalculateBenefitSquare(
                                        _customer.ResidentsCount,
                                        _customer.FederalBenefitResidentsCount,
                                        _customer.Customer.Square,
                                        out _benefitNormalSquare,
                                        out _benefitSquare,
                                        out _extraSquare);

                                    #region Запрос баланса

                                    //Отдельная задача - Отдельная структура данных для балансов абонентов, чтобы не выбирать ее из всех операций отдельно
                                    PeriodBalances _customerPeriodBalances = new PeriodBalances(_entities.ChargeOperPoses
                                        .Where(p => p.ChargeOpers.Customers.ID == _customer.Customer.ID)
                                        .Select(p => new
                                        {
                                            Period = p.ChargeOpers.ChargeSets.Period,
                                            ServiceID = p.Services.ID,
                                            Charge = p.Value,
                                            Benefit = (decimal)0,
                                            Correction = (decimal)0,
                                            Payment = (decimal)0,
                                            Overpayment = (decimal)0,
                                            OverpaymentCorrection = (decimal)0,
                                            Total = p.Value,
                                        })
                                        .Concat(_entities.RechargeOperPoses
                                            .Where(p => p.RechargeOpers.Customers.ID == _customer.Customer.ID)
                                            .Select(p => new
                                            {
                                                Period = p.RechargeOpers.RechargeSets.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = p.Value,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = p.Value,
                                            }))
                                        .Concat(_entities.ChargeOperPoses
                                            .Where(p =>
                                                p.ChargeOpers.Customers.ID == _customer.Customer.ID &&
                                                p.ChargeOpers.ChargeCorrectionOpers != null)
                                            .Select(p => new
                                            {
                                                Period = p.ChargeOpers.ChargeCorrectionOpers.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = -1 * p.Value,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = -1 * p.Value,
                                            }))
                                        .Concat(_entities.RechargeOperPoses
                                            .Where(p =>
                                                p.RechargeOpers.Customers.ID == _customer.Customer.ID &&
                                                p.RechargeOpers.ChildChargeCorrectionOpers != null)
                                            .Select(p => new
                                            {
                                                Period = p.RechargeOpers.ChildChargeCorrectionOpers.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = -1 * p.Value,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = -1 * p.Value,
                                            }))
                                        .Concat(_entities.BenefitOperPoses
                                            .Where(p => p.BenefitOpers.ChargeOpers.Customers.ID == _customer.Customer.ID)
                                            .Select(p => new
                                            {
                                                Period = p.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = p.Value,
                                                Correction = (decimal)0,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = p.Value,
                                            }))
                                        .Concat(_entities.BenefitOperPoses
                                            .Where(p =>
                                                p.BenefitOpers.ChargeOpers.Customers.ID == _customer.Customer.ID &&
                                                p.BenefitOpers.BenefitCorrectionOpers != null)
                                            .Select(p => new
                                            {
                                                Period = p.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = -1 * p.Value,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = -1 * p.Value,
                                            }))
                                        .Concat(_entities.RebenefitOperPoses
                                            .Where(p => p.RebenefitOpers.RechargeOpers.Customers.ID == _customer.Customer.ID)
                                            .Select(p => new
                                            {
                                                Period = p.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = p.Value,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = p.Value,
                                            }))
                                        .Concat(_entities.RebenefitOperPoses
                                            .Where(p =>
                                                p.RebenefitOpers.RechargeOpers.Customers.ID == _customer.Customer.ID &&
                                                p.RebenefitOpers.BenefitCorrectionOpers != null)
                                            .Select(p => new
                                            {
                                                Period = p.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = -1 * p.Value,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = -1 * p.Value,
                                            }))
                                        .Concat(_entities.PaymentOperPoses
                                            .Where(p => p.PaymentOpers.Customers.ID == _customer.Customer.ID)
                                            .Select(p => new
                                            {
                                                Period = p.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = (decimal)0,
                                                Payment = p.Value,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = p.Value,
                                            }))
                                        .Concat(_entities.PaymentCorrectionOperPoses
                                            .Where(p => p.PaymentCorrectionOpers.PaymentOpers.Customers.ID == _customer.Customer.ID)
                                            .Select(p => new
                                            {
                                                Period = p.PaymentCorrectionOpers.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = p.Value,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = p.Value,
                                            }))
                                        .Concat(_entities.OverpaymentOperPoses
                                            .Where(p => p.OverpaymentOpers.Customers.ID == _customer.Customer.ID)
                                            .Select(p => new
                                            {
                                                Period = p.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = (decimal)0,
                                                Payment = (decimal)0,
                                                Overpayment = p.Value,
                                                OverpaymentCorrection = (decimal)0,
                                                Total = p.Value,
                                            }))
                                        .Concat(_entities.OverpaymentCorrectionOperPoses
                                            .Where(p => p.OverpaymentCorrectionOpers.ChargeOpers.Customers.ID == _customer.Customer.ID)
                                            .Select(p => new
                                            {
                                                Period = p.OverpaymentCorrectionOpers.Period,
                                                ServiceID = p.Services.ID,
                                                Charge = (decimal)0,
                                                Benefit = (decimal)0,
                                                Correction = (decimal)0,
                                                Payment = (decimal)0,
                                                Overpayment = (decimal)0,
                                                OverpaymentCorrection = p.Value,
                                                Total = p.Value,
                                            }))
                                        .GroupBy(p => p.Period)
                                        .Select(groupedByPeriod => new
                                        {
                                            Period = groupedByPeriod.Key,
                                            Balance = groupedByPeriod
                                                .GroupBy(b => b.ServiceID)
                                                .Select(groupedByService => new
                                                {
                                                    ServiceID = groupedByService.Key,
                                                    Charge = groupedByService.Sum(b => b.Charge),
                                                    Benefit = groupedByService.Sum(b => b.Benefit),
                                                    Correction = groupedByService.Sum(b => b.Correction),
                                                    Payment = groupedByService.Sum(b => b.Payment),
                                                    Overpayment = groupedByService.Sum(b => b.Overpayment),
                                                    OverpaymentCorrection = groupedByService.Sum(b => b.OverpaymentCorrection),
                                                    Total = groupedByService.Sum(b => b.Total),
                                                }),
                                        })
                                        .ToDictionary(
                                            periodBalance => periodBalance.Period,
                                            periodBalance => new ServiceBalances(periodBalance.Balance
                                                .ToDictionary(
                                                    serviceBalance => serviceBalance.ServiceID,
                                                    serviceBalance => new Balance(
                                                        serviceBalance.Charge,
                                                        serviceBalance.Benefit,
                                                        serviceBalance.Correction,
                                                        serviceBalance.Payment,
                                                        serviceBalance.Overpayment,
                                                        serviceBalance.OverpaymentCorrection,
                                                        serviceBalance.Total
                                                    )))));

                                    #endregion

                                    #region Начисление и предоставление льгот

                                    ChargeOpers _chargeOper = new ChargeOpers
                                    {
                                        ChargeSets = _chargeSet,
                                        CreationDateTime = _now,
                                        Customers = _customer.Customer
                                    };
                                    _entities.AddToChargeOpers(_chargeOper);

                                    BenefitOpers _benefitOper = null;

                                    foreach (var _customerPos in _customer.CustomerPoses)
                                    {
                                        decimal _chargeValue = 0,
                                                //не разделять позиции на локальные и федеральные
                                                _localBenefitValue = 0,
                                                _federalBenefitValue = 0;

                                        Services _service = _services[_customerPos.ServiceID];

                                        switch ((Service.ChargeRuleType)_service.ChargeRule)
                                        {
                                            case Service.ChargeRuleType.FixedRate:
                                            default:
                                                _chargeValue = _customerPos.Rate;
                                                break;
                                            case Service.ChargeRuleType.SquareRate:
                                                _chargeValue = Math.Round(_customerPos.Rate * _customer.Customer.Square, 2, MidpointRounding.AwayFromZero);

                                                //Отдельная задача - Добавить соответствие льгот типам услугам
                                                CountBenefits(
                                                    _customerPos.ServiceTypeID,
                                                    _customerPos.Rate,
                                                    _benefitSquare,
                                                    _extraSquare,
                                                    _customer.LocalBenefitCoefficient,
                                                    out _federalBenefitValue,
                                                    out _localBenefitValue);
                                                break;
                                            case Service.ChargeRuleType.ResidentsRate:
                                                _chargeValue = Math.Round(_customerPos.Rate * _customer.ResidentsCount, 2, MidpointRounding.AwayFromZero);
                                                break;
                                            case Service.ChargeRuleType.CounterRate:
                                                //вычислять в отдельной функции
                                                if (_counterInfoByService.ContainsKey(_customerPos.ServiceID))
                                                {
                                                    CommonCounterInfo _commonCounterInfo = _counterInfoByService[_customerPos.ServiceID];

                                                    if (_commonCounterInfo.PrivateCounterInfoByCustomer.ContainsKey(_customer.Customer.ID))
                                                    {
                                                        decimal _sharedCharge = 0;

                                                        Dictionary<string, PrivateCounterInfo> _infoByCounterNumber =
                                                            _commonCounterInfo.PrivateCounterInfoByCustomer[_customer.Customer.ID];

                                                        foreach (KeyValuePair<string, PrivateCounterInfo> _pair in _infoByCounterNumber)
                                                        {
                                                            _chargeValue += Math.Round(_pair.Value.Consumption * _pair.Value.Rate * _commonCounterInfo.Coefficient, 2, MidpointRounding.AwayFromZero);
                                                            _sharedCharge += _pair.Value.CommonConsumptionPart * _pair.Value.Rate;

                                                            _counterBillPoses.Add(
                                                                new RegularBillDocCounterPoses
                                                                {
                                                                    Consumption = _pair.Value.Consumption,
                                                                    CurValue = _pair.Value.CurrentValue,
                                                                    PrevValue = _pair.Value.PreviousValue,
                                                                    Rate = _pair.Value.Rate,
                                                                    Number = _pair.Key
                                                                });
                                                        }

                                                        _sharedCounterPoses.Add(
                                                            new RegularBillDocSharedCounterPoses
                                                            {
                                                                SharedCounterValue = Math.Round(_commonCounterInfo.ConsumptionCustomerSum, 2, MidpointRounding.AwayFromZero),
                                                                SharedCharge = Math.Round(_sharedCharge, 2, MidpointRounding.AwayFromZero),
                                                            });
                                                    }
                                                }
                                                break;

                                            case Service.ChargeRuleType.PublicPlaceAreaRate:
                                                PublicPlaces _pp = _building.PublicPlaces.FirstOrDefault(pp => pp.ServiceID == _service.ID);
                                                RegularBillDocPublicPlacePoses _pos;

                                                if (_pp != null && _service.Norm.HasValue && _building.Area > 0)
                                                {
                                                    decimal _serviceVolume = _service.Norm.Value * _pp.Area * _customer.Customer.Square / _building.Area;
                                                    _chargeValue = _customerPos.Rate*_serviceVolume;

                                                    _pos =
                                                        new RegularBillDocPublicPlacePoses
                                                        {
                                                            Area = _pp.Area,
                                                            Norm = _service.Norm.Value,
                                                            NormMeasure = _service.NormMeasure,
                                                            Rate = _customerPos.Rate,
                                                            Service = _service.Name,
                                                            ServiceVolume = _serviceVolume,
                                                            Total = _chargeValue
                                                        };
                                                }
                                                else
                                                {
                                                    _pos =
                                                        new RegularBillDocPublicPlacePoses
                                                        {
                                                            Area = 0,
                                                            Norm = 0,
                                                            NormMeasure = _service.NormMeasure,
                                                            Rate = _customerPos.Rate,
                                                            Service = _service.Name,
                                                            ServiceVolume = 0,
                                                            Total = _chargeValue
                                                        };
                                                }

                                                _publicPlacePoses.Add(_pos);
                                                break;
                                        }

                                        if (_chargeValue > 0)
                                        {
                                            ChargeOperPoses _chargeOperPos = new ChargeOperPoses()
                                            {
                                                ChargeOpers = _chargeOper,
                                                Services = _services[_customerPos.ServiceID],
                                                Contractors = _contractors[_customerPos.ContractorID],
                                                Value = _chargeValue
                                            };
                                            _entities.ChargeOperPoses.AddObject(_chargeOperPos);
                                            _customerPeriodBalances.AddCharge(_chargeOperPos.ChargeOpers.ChargeSets.Period, _chargeOperPos.Services.ID, _chargeOperPos.Value);

                                            if (_federalBenefitValue < 0 || _localBenefitValue < 0)
                                            {
                                                if (_benefitOper == null)
                                                {
                                                    _benefitOper =
                                                        new BenefitOpers
                                                        {
                                                            ChargeOpers = _chargeOper
                                                        };
                                                    _entities.BenefitOpers.AddObject(_benefitOper);
                                                }

                                                if (_federalBenefitValue < 0)
                                                {
                                                    BenefitOperPoses _federalBenefitOperPos = new BenefitOperPoses
                                                    {
                                                        BenefitOpers = _benefitOper,
                                                        Services = _services[_customerPos.ServiceID],
                                                        BenefitRule = (byte)BenefitType.BenefitRuleType.FiftyPercentBySquare,
                                                        Contractors = _contractors[_customerPos.ContractorID],
                                                        Value = _federalBenefitValue
                                                    };
                                                    _entities.BenefitOperPoses.AddObject(_federalBenefitOperPos);
                                                    _customerPeriodBalances.AddBenefit(_federalBenefitOperPos.BenefitOpers.ChargeOpers.ChargeSets.Period, _federalBenefitOperPos.Services.ID, _federalBenefitOperPos.Value);
                                                }

                                                if (_localBenefitValue < 0)
                                                {
                                                    BenefitOperPoses _localBenefitOperPos = new BenefitOperPoses()
                                                    {
                                                        BenefitOpers = _benefitOper,
                                                        Services = _services[_customerPos.ServiceID],
                                                        BenefitRule = (byte)BenefitType.BenefitRuleType.FixedPercent,
                                                        Contractors = _contractors[_customerPos.ContractorID],
                                                        Value = _localBenefitValue
                                                    };
                                                    _entities.BenefitOperPoses.AddObject(_localBenefitOperPos);
                                                    _customerPeriodBalances.AddBenefit(_localBenefitOperPos.BenefitOpers.ChargeOpers.ChargeSets.Period, _localBenefitOperPos.Services.ID, _localBenefitOperPos.Value);
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                    decimal _currentPeriodTotal = 0,
                                            _rest = 0;

                                    if (_customerPeriodBalances.Balances.ContainsKey(_currentPeriod))
                                    {
                                        _chargeOper.Value = _customerPeriodBalances.Balances[_currentPeriod].TotalBalance.Charge;

                                        if (_benefitOper != null)
                                        {
                                            _benefitOper.Value = _customerPeriodBalances.Balances[_currentPeriod].TotalBalance.Benefit;
                                        }

                                        Balance _totalBalance = _customerPeriodBalances.Balances[_currentPeriod].TotalBalance;
                                        _currentPeriodTotal = _totalBalance.Charge + _totalBalance.Benefit + _totalBalance.Correction;

                                        _rest =
                                            _customerPeriodBalances.Balances
                                                .Where(periodBalance => periodBalance.Key < _currentPeriod)
                                                .Sum(periodBalance => periodBalance.Value.TotalBalance.Total);
                                    }

                                    #region Переплата

                                    if (_customerPeriodBalances.Balances.ContainsKey(_lastChargedPeriod))
                                    {
                                        ServiceBalances _overpaymentBalances = 
                                            new ServiceBalances(
                                                _customerPeriodBalances.Balances[_lastChargedPeriod].Balances
                                                    .Where(serviceBalance => serviceBalance.Value.Total < 0)
                                                    .ToDictionary(
                                                        serviceBalance => serviceBalance.Key,
                                                        serviceBalance => serviceBalance.Value));

                                        if (_overpaymentBalances.Balances.Count > 0)
                                        {
                                            OverpaymentCorrectionOpers _overpaymentCorrectionOper = new OverpaymentCorrectionOpers
                                            {
                                                Period = _lastChargedPeriod,
                                                Value = -1 * _overpaymentBalances.TotalBalance.Total,
                                                ChargeOpers = _chargeOper,
                                            };
                                            _entities.OverpaymentCorrectionOpers.AddObject(_overpaymentCorrectionOper);

                                            foreach (KeyValuePair<int, Balance> _balance in _overpaymentBalances.Balances)
                                            {
                                                OverpaymentCorrectionOperPoses _pos = new OverpaymentCorrectionOperPoses
                                                {
                                                    OverpaymentCorrectionOpers = _overpaymentCorrectionOper,
                                                    Services = _services[_balance.Key],
                                                    Value = -1 * _balance.Value.Total,
                                                };
                                                _entities.OverpaymentCorrectionOperPoses.AddObject(_pos);
                                                _customerPeriodBalances.AddOverpaymentCorrection(_pos.OverpaymentCorrectionOpers.Period, _pos.Services.ID, _pos.Value);
                                            }

                                            PeriodBalances _distribution = PaymentDistributionSrv.DistributeOverpayment(_customerPeriodBalances, _overpaymentBalances.TotalBalance.Total, _currentPeriod);

                                            OverpaymentOpers _overpaymentOper = new OverpaymentOpers
                                            {
                                                CreationDateTime = _now,
                                                Customers = _customer.Customer,
                                                PaymentPeriod = _currentPeriod,
                                                OverpaymentCorrectionOpers = _overpaymentCorrectionOper,
                                                Value = _overpaymentBalances.TotalBalance.Total,
                                            };
                                            _entities.OverpaymentOpers.AddObject(_overpaymentOper);

                                            foreach (KeyValuePair<DateTime, ServiceBalances> _periodBalance in _distribution.Balances)
                                            {
                                                foreach (KeyValuePair<int, Balance> _serviceBalance in _periodBalance.Value.Balances)
                                                {
                                                    OverpaymentOperPoses _pos = new OverpaymentOperPoses
                                                    {
                                                        OverpaymentOpers = _overpaymentOper,
                                                        Period = _periodBalance.Key,
                                                        Services = _services[_serviceBalance.Key],
                                                        Value = _serviceBalance.Value.Total,
                                                    };
                                                    _entities.OverpaymentOperPoses.AddObject(_pos);
                                                    _customerPeriodBalances.AddOverpayment(_pos.Period, _pos.Services.ID, _pos.Value);
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                    #region Создание квитанций

                                    RegularBillDocs _billDoc = new RegularBillDocs
                                    {
                                        CreationDateTime = _now,
                                        Account = _customer.Customer.Account,
                                        Address = $"ул. {_building.StreetName}, {_building.BuildingNumber}, кв. {_customer.Customer.Apartment}",
                                        Owner =
                                            _customer.Customer.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson
                                                ? _customer.Customer.JuridicalPersonFullName
                                                : _customer.Customer.PhysicalPersonShortName,
                                        Square = $"{_customer.Customer.Square} кв.м.",
                                        ResidentsCount = _customer.ResidentsCount,
                                        Customers = _customer.Customer,
                                        BillSets = _billSet,
                                        Period = _currentPeriod,
                                        EmergencyPhoneNumber =
                                            _customer.CustomerPoses.Any(pos => pos.ContractorID == MADIX_CONTRACTOR_ID)
                                                ? "261-47-14"
                                                : "298-09-81",
                                        BuildingArea = _building.Area,
                                        PayBeforeDateTime = _payBefore,
                                        MonthChargeValue = _currentPeriodTotal,
                                        OverpaymentValue = _rest,
                                        Value = _currentPeriodTotal + _rest,
                                    };

                                    var _contractorPos = _customer.CustomerPoses.FirstOrDefault(p => p.ServiceID == CONTRACTOR_CONTACT_INFO_SERVICE_ID);
                                    if (_contractorPos != null)
                                    {
                                        Contractors _cont = _contractors[_contractorPos.ContractorID];
                                        _billDoc.ContractorContactInfo = $"{_cont.Name}, {_cont.ContactInfo}";
                                    }
                                    else
                                    {
                                        _billDoc.ContractorContactInfo = string.Empty;
                                    }

                                    _entities.RegularBillDocs.AddObject(_billDoc);

                                    _chargeOper.RegularBillDocs = _billDoc;

                                    if (_customerPeriodBalances.Balances.ContainsKey(_currentPeriod))
                                    {
                                        var _poses = _customerPeriodBalances.Balances[_currentPeriod].Balances
                                            .GroupBy(serviceBalance => new
                                            {
                                                ServiceTypeID = _services[serviceBalance.Key].ServiceTypes.ID,
                                                ServiceTypeName = _services[serviceBalance.Key].ServiceTypes.Name,
                                            })
                                            .Select(groupedByServiceType => new
                                            {
                                                groupedByServiceType.Key.ServiceTypeName,
                                                Rate = groupedByServiceType.Sum(x => x.Value.Charge != 0 ? _customer.CustomerPoses.Single(y => y.ServiceID == x.Key).Rate : 0),
                                                Charge = groupedByServiceType.Sum(x => x.Value.Charge),
                                                Benefit = groupedByServiceType.Sum(x => x.Value.Benefit),
                                                Correction = groupedByServiceType.Sum(x => x.Value.Correction),
                                            });

                                        foreach (var _pos in _poses)
                                        {
                                            _entities.RegularBillDocSeviceTypePoses.AddObject(
                                                new RegularBillDocSeviceTypePoses()
                                                {
                                                    RegularBillDocs = _billDoc,
                                                    ServiceTypeName = _pos.ServiceTypeName,
                                                    PayRate = _pos.Rate,
                                                    Charge = _pos.Charge,
                                                    Benefit = _pos.Benefit,
                                                    Recalculation = _pos.Correction,
                                                    Payable = _pos.Charge + _pos.Benefit + _pos.Correction,
                                                });
                                        }
                                    }

                                    foreach (RegularBillDocCounterPoses _counterBillPos in _counterBillPoses)
                                    {
                                        _counterBillPos.RegularBillDocs = _billDoc;
                                        _entities.RegularBillDocCounterPoses.AddObject(_counterBillPos);
                                    }

                                    foreach (RegularBillDocSharedCounterPoses _sharedCounterBillPos in _sharedCounterPoses)
                                    {
                                        _sharedCounterBillPos.RegularBillDocs = _billDoc;
                                        _entities.RegularBillDocSharedCounterPoses.AddObject(_sharedCounterBillPos);
                                    }

                                    foreach (RegularBillDocPublicPlacePoses _publicPlacePos in _publicPlacePoses)
                                    {
                                        _publicPlacePos.RegularBillDocs = _billDoc;
                                        _entities.RegularBillDocPublicPlacePoses.AddObject(_publicPlacePos);
                                    }

                                    #endregion

                                    //Одельная задача - использовать int в БД, чтобы избежать потенциально исключения
                                    _billSet.Quantity++;
                                    _billSet.ValueSum += _chargeOper.Value;
                                    _chargeSet.Quantity++;
                                    _chargeSet.ValueSum += _chargeOper.Value;
                                }

                                View.AddProgress(_building.CustomersCount);
                                _entities.SaveChanges();

                                _totalSum = _chargeSet.ValueSum;
                                _resultCount = _chargeSet.Quantity;

                                // Отдельная задача - разделить обработку начислений на потоки и не блокировать интерфейс
                                Application.DoEvents();
                            }
                            catch (Exception _ex)
                            {
                                Logger.SimpleWrite($"Foreach building exception, Building: {_building.BuildingID}, Exception: {_ex}\r\n");
                            }
                        }
                    }
                }

                View.ResultCount = _resultCount;
                View.ResultValue = _totalSum;
                View.ResultErrorCount = _zipCodes.Sum(zipCode => zipCode.Buildings.Sum(building => building.CustomersCount)) - _resultCount;
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Register charges exception: {_ex}");
                View.ShowMessage("Начисления не выполнены", "Ошибка операции");
            }

            _stopwatch.Stop();
            Logger.SimpleWrite($"Потрачено: {_stopwatch.Elapsed}");
        }

        /// <summary>
        /// Выполняет дополнительные начисления
        /// </summary>
        public void RegisterRecharges()
        {
            DateTime _currentDate = ServerTime.GetDateTimeInfo().Now;
            DateTime _currentPeriod = ServerTime.GetPeriodInfo().FirstUncharged;
            DateTime _period;
            DateTime _tillCorrectionPeriod;

            decimal _resultValue = 0;
            int _resultCount = 0;
            int _resultErrorCount = 0;

            int _monthCount;

            DataTable _selectedCustomers;

            if (View.ChargeType == ChargeType.Correction)
            {
                _period = View.SinceCorrectionPeriod;
                _tillCorrectionPeriod = View.TillCorrectionPeriod;
                _monthCount = (_period.Year == _tillCorrectionPeriod.Year
                    ? _tillCorrectionPeriod.Month - _period.Month
                    : 12 - _period.Month + 12 * (_tillCorrectionPeriod.Year - _period.Year - 1) + _tillCorrectionPeriod.Month) + 1;
                _selectedCustomers = View.SelectedCustomers;
            }
            else // if (View.ChargeType == ChargeType.PercentCorrection)
            {
                _period = View.PercentCorrectionPeriod;
                _tillCorrectionPeriod = View.PercentCorrectionPeriod;
                _monthCount = 1;
                _selectedCustomers = View.CustomersWithPercents;
            }

            View.ResetProgressBar(_selectedCustomers.Rows.Count * _monthCount);
            Application.DoEvents();

            int _rechargeSetId;

            using (Entities _entities = new Entities())
            {
                RechargeSets _rechargeSet =
                    new RechargeSets
                    {
                        CreationDateTime = _currentDate,
                        Period = _currentPeriod,
                        Number = _entities.RechargeSets.Any() ? _entities.RechargeSets.Max(c => c.Number) + 1 : 1,
                        Author = (Users)_entities.GetObjectByKey(new EntityKey("Entities.Users", "ID", int.Parse(UserHolder.User.ID)))
                    };
                _entities.AddToRechargeSets(_rechargeSet);
                _entities.SaveChanges();
                _rechargeSetId = _rechargeSet.ID;
            }

            while (_period <= _tillCorrectionPeriod)
            {
                for (int i = 0; i < _selectedCustomers.Rows.Count; i++)
                {
                    using (Entities _entities = new Entities())
                    {
                        _entities.CommandTimeout = 3600;
                        try
                        {
                            int _id = (int)_selectedCustomers.Rows[i]["ID"];

                            Customers _customer =
                                _entities.Customers
                                    .Include("Residents")
                                    .Include("CustomerPoses")
                                    .Include("Buildings")
                                    .Include("Buildings.Streets")
                                    .First(c => c.ID == _id);

                            #region Корректировка

                            ChargeOpers _chargeOper =
                                _entities.ChargeOpers
                                    .Include("ChargeCorrectionOpers")
                                    .FirstOrDefault(
                                        c =>
                                        c.Customers.ID == _customer.ID &&
                                        c.ChargeSets.Period == _period);

                            ChargeCorrectionOpers _chargeCorrectionOper = null;

                            if (_chargeOper != null)
                            {
                                _chargeCorrectionOper =
                                    new ChargeCorrectionOpers
                                    {
                                        CreationDateTime = _currentDate,
                                        Period = _currentPeriod
                                    };

                                if (_chargeOper.ChargeCorrectionOpers == null)
                                {
                                    _entities.AddToChargeCorrectionOpers(_chargeCorrectionOper);
                                    _chargeCorrectionOper.Value = _chargeOper.Value * (-1);
                                    _chargeOper.ChargeCorrectionOpers = _chargeCorrectionOper;

                                    List<ChargeOperPoses> _chargeOperPoses =
                                        _entities.ChargeOperPoses
                                            .Include("Contractors")
                                            .Include("Services")
                                            .Where(p => p.ChargeOpers.ID == _chargeOper.ID)
                                            .ToList();

                                    foreach (ChargeOperPoses _chargePos in _chargeOperPoses)
                                    {
                                        ChargeCorrectionOperPoses _chargeCorrectionPos = new ChargeCorrectionOperPoses()
                                        {
                                            ChargeCorrectionOpers = _chargeCorrectionOper,
                                            Services = _chargePos.Services,
                                            Contractors = _chargePos.Contractors,
                                            Value = _chargePos.Value * (-1)
                                        };
                                        _entities.AddToChargeCorrectionOperPoses(_chargeCorrectionPos);
                                    }

                                    BenefitOpers _benefitOper =
                                        _entities.BenefitOpers
                                            .Include("BenefitOperPoses")
                                            .FirstOrDefault(b => b.ChargeOpers.ID == _chargeOper.ID);

                                    if (_benefitOper != null)
                                    {
                                        BenefitCorrectionOpers _benefitCorrectionOper =
                                            new BenefitCorrectionOpers
                                            {
                                                ChargeCorrectionOpers = _chargeCorrectionOper,
                                                Value = _benefitOper.Value * (-1)
                                            };
                                        _entities.AddToBenefitCorrectionOpers(_benefitCorrectionOper);
                                        _benefitOper.BenefitCorrectionOpers = _benefitCorrectionOper;

                                        foreach (var _benefitPos in _benefitOper.BenefitOperPoses)
                                        {
                                            BenefitCorrectionOperPoses _benefitCorrectionOperPos =
                                                new BenefitCorrectionOperPoses
                                                {
                                                    BenefitCorrectionOpers = _benefitCorrectionOper,
                                                    Services = _benefitPos.Services,
                                                    Contractors = _benefitPos.Contractors,
                                                    Value = _benefitPos.Value * (-1)
                                                };
                                            _entities.AddToBenefitCorrectionOperPoses(_benefitCorrectionOperPos);
                                        }
                                    }
                                }
                                else
                                {
                                    RechargeOpers _currentRechargeOper =
                                        _entities.RechargeOpers
                                            .FirstOrDefault(r =>
                                                r.ChargeOpers.ID == _chargeOper.ID &&
                                                r.ChildChargeCorrectionOpers == null);

                                    if (_currentRechargeOper != null)
                                    {
                                        _entities.AddToChargeCorrectionOpers(_chargeCorrectionOper);
                                        _chargeCorrectionOper.Value = _currentRechargeOper.Value * (-1);
                                        _currentRechargeOper.ChildChargeCorrectionOpers = _chargeCorrectionOper;

                                        List<RechargeOperPoses> _poses =
                                            _entities.RechargeOperPoses
                                                .Include("Contractors")
                                                .Include("Services")
                                                .Where(p => p.RechargeOpers.ID == _currentRechargeOper.ID)
                                                .ToList();

                                        foreach (RechargeOperPoses _rechargeOperPos in _poses)
                                        {
                                            ChargeCorrectionOperPoses _chargeCorrectionPos = new ChargeCorrectionOperPoses()
                                            {
                                                ChargeCorrectionOpers = _chargeCorrectionOper,
                                                Services = _rechargeOperPos.Services,
                                                Contractors = _rechargeOperPos.Contractors,
                                                Value = _rechargeOperPos.Value * (-1)
                                            };
                                            _entities.AddToChargeCorrectionOperPoses(_chargeCorrectionPos);
                                        }

                                        RebenefitOpers _currentRebenefitOper =
                                            _entities.RebenefitOpers
                                                .Include("RebenefitOperPoses")
                                                .FirstOrDefault(b => b.RechargeOpers.ID == _currentRechargeOper.ID);

                                        if (_currentRebenefitOper != null)
                                        {
                                            BenefitCorrectionOpers _benefitCorrectionOper =
                                                new BenefitCorrectionOpers
                                                {
                                                    ChargeCorrectionOpers = _chargeCorrectionOper,
                                                    Value = _currentRebenefitOper.Value * (-1)
                                                };
                                            _entities.AddToBenefitCorrectionOpers(_benefitCorrectionOper);
                                            _currentRebenefitOper.BenefitCorrectionOpers = _benefitCorrectionOper;

                                            foreach (var _benefitPos in _currentRebenefitOper.RebenefitOperPoses)
                                            {
                                                BenefitCorrectionOperPoses _benefitCorrectionOperPos =
                                                    new BenefitCorrectionOperPoses
                                                    {
                                                        BenefitCorrectionOpers = _benefitCorrectionOper,
                                                        Services = _benefitPos.Services,
                                                        Contractors = _benefitPos.Contractors,
                                                        Value = _benefitPos.Value * (-1)
                                                    };
                                                _entities.AddToBenefitCorrectionOperPoses(_benefitCorrectionOperPos);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        RechargeOpers _lastRechargeOper =
                                            _entities.RechargeOpers
                                                .Include("ChildChargeCorrectionOpers")
                                                .Where(r => r.ChargeOpers.ID == _chargeOper.ID)
                                                .OrderByDescending(r => r.CreationDateTime)
                                                .FirstOrDefault();

                                        _chargeCorrectionOper =
                                            _lastRechargeOper != null
                                                ? _lastRechargeOper.ChildChargeCorrectionOpers
                                                : _chargeOper.ChargeCorrectionOpers;
                                    }
                                }
                            }

                            #endregion

                            #region Дополнительные начисления

                            RechargeSets _rechargeSet =
                                (RechargeSets)_entities.GetObjectByKey(new EntityKey("Entities.RechargeSets", "ID", _rechargeSetId));

                            var _customerPoses =
                                _entities.CustomerPoses
                                    .Include("Customers")
                                    .Include("Contractors")
                                    .Include("Services")
                                    .Include("Services.ServiceTypes")
                                    .Include("Customers.Residents")
                                    .Include("Customers.Residents.BenefitTypes")
                                    .Where(
                                        c =>
                                        c.Customers.ID == _customer.ID &&
                                        _period >= c.Since &&
                                        _period <= c.Till)
                                    .ToList();

                            RechargeOpers _rechargeOper =
                                new RechargeOpers
                                {
                                    RechargeSets = _rechargeSet,
                                    CreationDateTime = _currentDate,
                                    Customers = _customer,
                                    ChargeOpers = _chargeOper
                                };
                            _entities.AddToRechargeOpers(_rechargeOper);

                            if (_chargeCorrectionOper != null)
                            {
                                _chargeCorrectionOper.ChildRechargeOpers = _rechargeOper;
                            }

                            RebenefitOpers _rebenefitOper = null;

                            decimal _benefitNormalSquare,
                                    _benefitSquare,
                                    _extraSquare;

                            CalculateBenefitSquare(
                                _customer.Residents.Count(),
                                _customer.Residents.Count(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule == 0),
                                _customer.Square,
                                out _benefitNormalSquare,
                                out _benefitSquare,
                                out _extraSquare);

                            byte _localBenefitCoefficient =
                                _customer.Residents
                                    .Where(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule != 0)
                                    .Max(resident => resident.BenefitTypes.FixedPercent) ?? 0;

                            foreach (CustomerPoses _customerPos in _customerPoses)
                            {
                                decimal _federalBenefit = 0;
                                decimal _localBenefit = 0;

                                //Перенести правило начисления по услуге в тип услуги
                                decimal _value = 0;

                                switch ((Service.ChargeRuleType)_customerPos.Services.ChargeRule)
                                {
                                    case Service.ChargeRuleType.SquareRate:
                                        _value = Math.Round(_customerPos.Rate * _customerPos.Customers.Square, 2, MidpointRounding.AwayFromZero);
                                        CountBenefits(
                                            _customerPos.Services.ServiceTypes.ID,
                                            _customerPos.Rate,
                                            _benefitSquare,
                                            _extraSquare,
                                            _localBenefitCoefficient,
                                            out _federalBenefit,
                                            out _localBenefit);
                                        break;
                                    case Service.ChargeRuleType.ResidentsRate:
                                        _value = Math.Round(_customerPos.Rate * (_customerPos.Customers.Residents.Count), 2, MidpointRounding.AwayFromZero);
                                        break;
                                    case Service.ChargeRuleType.CounterRate:
                                        var _privateCounters =
                                            _entities.PrivateCounters
                                                .Where(v => v.CustomerPoses.ID == _customerPos.ID)
                                                .ToList();

                                        if (_privateCounters.Any())
                                        {
                                            var _commonCounters =
                                                _entities.CommonCounters
                                                    .Where(
                                                        c =>
                                                        c.Services.ID == _customerPos.Services.ID &&
                                                        c.Buildings.ID == _customer.Buildings.ID)
                                                    .Select(c => c.ID)
                                                    .ToList();

                                            if (_commonCounters.Any())
                                            {
                                                int _commonCounterId = _commonCounters.First();

                                                decimal _coefficient =
                                                    _entities.CommonCounterCoefficients
                                                        .Where(
                                                            p =>
                                                            p.Period == _period &&
                                                            p.CommonCounters.ID == _commonCounterId)
                                                        .Select(p => p.Coefficient)
                                                        .FirstOrDefault();

                                                if (_coefficient > 0)
                                                {
                                                    DateTime _previousPeriod = _period.AddMonths(-1);

                                                    foreach (PrivateCounters _counter in _privateCounters)
                                                    {
                                                        decimal _previousValue =
                                                            _entities.PrivateCounterValues
                                                                .Where(
                                                                    v =>
                                                                    v.PrivateCounters.ID == _counter.ID &&
                                                                    v.Period == _previousPeriod)
                                                                .Select(v => v.Value)
                                                                .FirstOrDefault();

                                                        decimal _currentValue =
                                                            _entities.PrivateCounterValues
                                                                .Where(
                                                                    v =>
                                                                    v.PrivateCounters.ID == _counter.ID &&
                                                                    v.Period == _period)
                                                                .Select(v => v.Value)
                                                                .FirstOrDefault();

                                                        if (_currentValue == 0)
                                                        {
                                                            continue;
                                                        }

                                                        _value += Math.Round((_currentValue - _previousValue) * _customerPos.Rate * _coefficient, 2, MidpointRounding.AwayFromZero);
                                                    }
                                                }
                                            }
                                        }
                                        continue;
                                    case Service.ChargeRuleType.FixedRate:
                                    default:
                                        _value = _customerPos.Rate;
                                        break;
                                }

                                if (_value > 0)
                                {
                                    if (View.ChargeType == ChargeType.PercentCorrection && _customerPos.Services.ID == Int32.Parse(View.Service.ID))
                                    {
                                        _value -= Math.Round((_value / DateTime.DaysInMonth(View.PercentCorrectionPeriod.Year, View.PercentCorrectionPeriod.Month) *
                                            (int)_selectedCustomers.Rows[i]["Days"] * (int)_selectedCustomers.Rows[i]["Percent"]) / 100, 2, MidpointRounding.AwayFromZero);
                                    }

                                    RechargeOperPoses _rechargeOperPos = new RechargeOperPoses()
                                    {
                                        RechargeOpers = _rechargeOper,
                                        Services = _customerPos.Services,
                                        Contractors = _customerPos.Contractors,
                                        Value = _value
                                    };
                                    _entities.AddToRechargeOperPoses(_rechargeOperPos);
                                    _rechargeOper.Value += _rechargeOperPos.Value;

                                    if ((_federalBenefit < 0 || _localBenefit < 0) && _rebenefitOper == null)
                                    {
                                        _rebenefitOper =
                                            new RebenefitOpers
                                            {
                                                RechargeOpers = _rechargeOper
                                            };
                                        _entities.AddToRebenefitOpers(_rebenefitOper);
                                    }

                                    if (_federalBenefit < 0)
                                    {
                                        RebenefitOperPoses _rebenefitOperPos =
                                            new RebenefitOperPoses
                                            {
                                                RebenefitOpers = _rebenefitOper,
                                                Services = _customerPos.Services,
                                                BenefitRule = (byte)BenefitType.BenefitRuleType.FiftyPercentBySquare,
                                                Contractors = _customerPos.Contractors,
                                                Value = _federalBenefit
                                            };
                                        _entities.AddToRebenefitOperPoses(_rebenefitOperPos);
                                        _rebenefitOper.Value += _rebenefitOperPos.Value;
                                    }

                                    if (_localBenefit < 0)
                                    {
                                        RebenefitOperPoses _rebenefitOperPos =
                                            new RebenefitOperPoses()
                                            {
                                                RebenefitOpers = _rebenefitOper,
                                                Services = _customerPos.Services,
                                                BenefitRule = (byte)BenefitType.BenefitRuleType.FixedPercent,
                                                Contractors = _customerPos.Contractors,
                                                Value = _localBenefit
                                            };
                                        _entities.AddToRebenefitOperPoses(_rebenefitOperPos);
                                        _rebenefitOper.Value += _rebenefitOperPos.Value;
                                    }
                                }
                            }

                            _rechargeSet.Quantity++;
                            _rechargeSet.ValueSum += _rechargeOper.Value;

                            #endregion

                            _entities.SaveChanges();

                            _resultValue = _rechargeSet.ValueSum;
                            _resultCount = _rechargeSet.Quantity;
                        }
                        catch (Exception _ex)
                        {
                            View.ShowMessage("Начисления не выполнены", "Ошибка операции");
                            Logger.SimpleWrite(String.Format("Charging error: {0}", _ex));
                            _resultErrorCount++;
                        }
                    }

                    View.AddProgress(1);
                    Application.DoEvents();
                }

                _period = _period.AddMonths(1);
            }

            View.ResultCount = _resultCount;
            View.ResultValue = _resultValue;
            View.ResultErrorCount = _resultErrorCount;
        }

        private void CalculateBenefitSquare(
            int residentCount,
            int federalBenefitResidentsCount,
            decimal square,
            out decimal benefitNormalSquare,
            out decimal benefitSquare,
            out decimal extraSquare)
        {
            benefitNormalSquare = 0;//(residentCount == 0 ? 0 : residentCount == 1 ? 33 : residentCount == 2 ? 21 : 18) * federalBenefitResidentsCount;
            benefitSquare = 0;//square < benefitNormalSquare ? square : benefitNormalSquare;
            extraSquare = square;// < benefitNormalSquare ? 0 : square - benefitSquare;
        }

        private void CountBenefits(
            int serviceTypeID,
            decimal rate,
            decimal benefitSquare,
            decimal extraSquare,
            byte localBenefitCoefficient,
            out decimal federalBenefitValue,
            out decimal localBenefitValue)
        {

            if (serviceTypeID == 2 || serviceTypeID == 3 || serviceTypeID == 4)
            {
                federalBenefitValue = 0;//Math.Round(-1 * rate * benefitSquare / 100 * 50, 2, MidpointRounding.AwayFromZero);
                localBenefitValue = Math.Round(-1 * rate * extraSquare / 100 * localBenefitCoefficient, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                federalBenefitValue = localBenefitValue = 0;
            }
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
                using (Entities _entities = new Entities())
                {
                    using (ExcelSheet _sheet = new ExcelSheet(View.DebtFileName, "внести долг"))
                    {
                        for (int _row = 1; _row < _sheet.RowsCount; _row++)
                        {
                            try
                            {
                                string _account = _sheet.GetCell("A", _row).Trim();

                                if (!string.IsNullOrEmpty(_account))
                                {
                                    decimal _debt = decimal.Parse(_sheet.GetCell("B", _row).Trim());

                                    var _customer =
                                        _entities.Customers
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
                                        Logger.SimpleWrite(string.Format("Can't find customer in row {0}", _row));
                                        _failCount++;
                                    }
                                }
                            }
                            catch (Exception _ex)
                            {
                                Logger.SimpleWrite(string.Format("Исключение при разборе строки {0} : {1}", _row, _ex));
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
                                Number = _entities.RechargeSets.Any() ? _entities.RechargeSets.Max(c => c.Number) + 1 : 1,
                                Author = (Users)_entities.GetObjectByKey(new EntityKey("Entities.Users", "ID", int.Parse(UserHolder.User.ID)))
                            };
                        _entities.AddToRechargeSets(_rechargeSet);
                        _entities.SaveChanges();
                    }
                }

                View.ResetProgressBar(_customerDebt.Keys.Count);
                Application.DoEvents();

                foreach (KeyValuePair<int, decimal> _pair in _customerDebt)
                {
                    int _customerID = _pair.Key;
                    decimal _debt = _pair.Value;

                    using (Entities _entities = new Entities())
                    {
                        try
                        {
                            Customers _customer =
                                _entities.Customers
                                    .Include("CustomerPoses")
                                    .Include("CustomerPoses.Contractors")
                                    .Include("CustomerPoses.Services")
                                    .Include("CustomerPoses.Services.ServiceTypes")
                                    .FirstOrDefault(customer => customer.ID == _customerID);

                            IList<CustomerPosValue> _distribution = new List<CustomerPosValue>();

                            var _customerPoses =
                                _customer.CustomerPoses.Where(
                                    c => _rechargeSet.Period >= c.Since && _rechargeSet.Period <= c.Till);

                            if (_customerPoses.Any())
                            {
                                foreach (CustomerPoses _pos in _customerPoses)
                                {
                                    decimal _value = 0;

                                    //Перенести правило начисления по услуге в тип услуги
                                    switch ((Service.ChargeRuleType)_pos.Services.ChargeRule)
                                    {
                                        case Service.ChargeRuleType.SquareRate:
                                            _value =
                                                Math.Round(
                                                    _pos.Rate * _pos.Customers.Square,
                                                    2,
                                                    MidpointRounding.AwayFromZero);
                                            break;
                                        case Service.ChargeRuleType.ResidentsRate:
                                            _value = Math.Round(_pos.Rate * _pos.Customers.Residents.Count, 2, MidpointRounding.AwayFromZero);
                                            break;
                                        case Service.ChargeRuleType.CounterRate:
                                            var _privateCounters =
                                                _entities.PrivateCounters
                                                    .Where(v => v.CustomerPoses.ID == _pos.ID)
                                                    .ToList();

                                            if (_privateCounters.Any())
                                            {
                                                foreach (PrivateCounters _counter in _privateCounters)
                                                {
                                                    decimal _previousValue =
                                                        _entities.PrivateCounterValues
                                                            .Where(
                                                                v =>
                                                                v.PrivateCounters.ID == _counter.ID &&
                                                                v.Period == _previousPeriod)
                                                            .Select(v => v.Value)
                                                            .FirstOrDefault();

                                                    decimal _currentValue =
                                                        _entities.PrivateCounterValues
                                                            .Where(
                                                                v =>
                                                                v.PrivateCounters.ID == _counter.ID &&
                                                                v.Period == _period)
                                                            .Select(v => v.Value)
                                                            .FirstOrDefault();

                                                    if (_currentValue == 0)
                                                    {
                                                        continue;
                                                    }

                                                    _value += Math.Round(_currentValue - _previousValue * _pos.Rate, 2, MidpointRounding.AwayFromZero);
                                                }
                                            }
                                            break;
                                        case Service.ChargeRuleType.FixedRate:
                                        default:
                                            _value = _pos.Rate;
                                            break;
                                    }

                                    if (_value > 0)
                                    {
                                        _distribution.Add(
                                            new CustomerPosValue
                                            {
                                                CustomerPos = _pos,
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
                                        _customerPosValue.Value =
                                            Math.Round(
                                                _coefficient * _customerPosValue.Value,
                                                2,
                                                MidpointRounding.AwayFromZero);

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

                                    _rechargeSet =
                                        (RechargeSets)_entities.GetObjectByKey(
                                            new EntityKey("Entities.RechargeSets", "ID", _rechargeSet.ID));

                                    RechargeOpers _rechargeOper =
                                        new RechargeOpers
                                        {
                                            RechargeSets = _rechargeSet,
                                            CreationDateTime = _now,
                                            Customers = _customer,
                                            Value = _distributionSum
                                        };
                                    _entities.AddToRechargeOpers(_rechargeOper);

                                    foreach (CustomerPosValue _customerPosValue in _distribution)
                                    {
                                        RechargeOperPoses _pos = new RechargeOperPoses
                                        {
                                            Services = _customerPosValue.CustomerPos.Services,
                                            Contractors = _customerPosValue.CustomerPos.Contractors,
                                            RechargeOpers = _rechargeOper,
                                            Value = _customerPosValue.Value
                                        };
                                        _entities.AddToRechargeOperPoses(_pos);
                                        _posesToRegister.Add(_pos);
                                    }

                                    _entities.SaveChanges();

                                    _rechargeSet.Quantity++;
                                    _rechargeSet.ValueSum += _rechargeOper.Value;

                                    _entities.SaveChanges();

                                    _totalValue = _rechargeSet.ValueSum;

                                    _posesToRegister.Clear();

                                    #endregion
                                }

                                _processed++;
                            }
                            else
                            {
                                Logger.SimpleWrite(string.Format("Отсутствуют услуги у абонента с л/с {0}", _customer.Account));
                                _failCount++;
                            }
                        }
                        catch (Exception _ex)
                        {
                            Logger.SimpleWrite(string.Format("Абонент {0}. Exception: {1}", _customerID, _ex));
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
                Logger.SimpleWrite(string.Format("Debts import error: {0}", _ex));
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

        /// <summary>
        /// Вычисляет необходимые данные для учета по счетчикам
        /// </summary>
        /// <param name="customersInBuilding">Жильцы</param>
        /// <param name="currentPeriod">Предыдущий период</param>
        /// <param name="previousPeriod">Текущий период</param>
        /// <returns>Данные по счетчикам, сгруппированные по услуге</returns>
        private Dictionary<int, CommonCounterInfo> GetBuildingCountersInfo(
            int buildingID,
            IEnumerable<int> customersInBuilding,
            DateTime currentPeriod,
            DateTime previousPeriod)
        {
            Dictionary<int, CommonCounterInfo> _result = new Dictionary<int, CommonCounterInfo>();

            using (Entities _entities = new Entities())
            {
                var _commonCounter =
                    _entities.CommonCounters
                        .Include("Services")
                        .Where(c => c.Buildings.ID == buildingID);

                foreach (var _counter in _commonCounter)
                {
                    decimal _totalConsumption = 0;
                    CommonCounterInfo _commonCounterInfo = new CommonCounterInfo();

                    foreach (int _customerID in customersInBuilding)
                    {
                        var _privateCounters =
                            _entities.PrivateCounters
                                .Where(
                                    p =>
                                    p.CustomerPoses.Customers.ID == _customerID &&
                                    p.CustomerPoses.Services.ID == _counter.Services.ID)
                                .Select(
                                    p =>
                                    new
                                    {
                                        p.ID,
                                        p.Rate,
                                        p.Number
                                    })
                                .ToList();

                        Dictionary<string, PrivateCounterInfo> _privateCounterInfoByNumber =
                            new Dictionary<string, PrivateCounterInfo>(_privateCounters.Count);

                        foreach (var _privateCounter in _privateCounters)
                        {
                            decimal _previousValue =
                                _entities.PrivateCounterValues
                                    .Where(
                                        v =>
                                        v.PrivateCounters.ID == _privateCounter.ID &&
                                        v.Period == previousPeriod)
                                    .Sum(v => (decimal?)v.Value) ?? 0;

                            decimal _currentValue =
                                _entities.PrivateCounterValues
                                    .Where(
                                        v =>
                                        v.PrivateCounters.ID == _privateCounter.ID &&
                                        v.Period == currentPeriod)
                                    .Sum(v => (decimal?)v.Value) ?? 0;

                            PrivateCounterInfo _privateCounterInfo =
                                new PrivateCounterInfo(_previousValue, _currentValue, _privateCounter.Rate);

                            _totalConsumption += _privateCounterInfo.Consumption;

                            _privateCounterInfoByNumber.Add(_privateCounter.Number, _privateCounterInfo);
                        }

                        _commonCounterInfo.PrivateCounterInfoByCustomer.Add(_customerID, _privateCounterInfoByNumber);
                    }

                    decimal _pValue =
                        _entities.CommonCounterValues
                            .Where(
                                v =>
                                v.CommonCounters.ID == _counter.ID &&
                                v.Period == previousPeriod)
                            .Sum(v => (decimal?)v.Value) ?? 0;

                    decimal _cValue =
                        _entities.CommonCounterValues
                            .Where(
                                v =>
                                v.CommonCounters.ID == _counter.ID &&
                                v.Period == currentPeriod)
                            .Sum(v => (decimal?)v.Value) ?? 0;

                    _commonCounterInfo.CurrentValue = _cValue;
                    _commonCounterInfo.Consumption = _cValue - _pValue;
                    _commonCounterInfo.Coefficient = Math.Round(_commonCounterInfo.Consumption / _totalConsumption, 9, MidpointRounding.AwayFromZero);
                    decimal _commonCoefficient = _commonCounterInfo.Coefficient - 1;

                    _entities.AddToCommonCounterCoefficients(
                        new CommonCounterCoefficients
                        {
                            Period = currentPeriod,
                            Coefficient = _commonCounterInfo.Coefficient,
                            CommonCounters = _counter
                        });

                    foreach (Dictionary<string, PrivateCounterInfo> _value in _commonCounterInfo.PrivateCounterInfoByCustomer.Values)
                    {
                        foreach (PrivateCounterInfo _info in _value.Values)
                        {
                            _info.CommonConsumptionPart = _info.Consumption * _commonCoefficient;
                            _commonCounterInfo.ConsumptionCustomerSum += _info.CommonConsumptionPart;
                        }
                    }

                    _result.Add(_counter.Services.ID, _commonCounterInfo);
                }

                _entities.SaveChanges();
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
                        _row["ID"],
                        _row["Account"],
                        _row["Owner"],
                        _row["ResidentsNumber"],
                        _row["Street"],
                        _row["House"],
                        _row["Apartment"],
                        _row["Square"],
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
    }
}