using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Wizard
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class WizardViewPresenter : BasePresenter<IWizardView>
    {
        /// <summary>
        /// Список введенных данных по платежам
        /// </summary>
        Dictionary<int, WizardPaymentElement> Payments { get; set; }

        /// <summary>
        /// Текущая операция платежа
        /// </summary>
        WizardPaymentElement CurrentPayment { get; set; }

        #region Save Payments Variables

        private DateTime _creationDateTime;
        private int _paymentSetID;
        private decimal _totalSum;
        private int _processedCount;
        private  int _errorsCount;
        private readonly object _statusChangeLock = new object();
        private readonly object _errorLogLock = new object();

        #endregion

        /// <summary>
        /// Сервис работы с доменами, умеющими работать с датамаппером
        /// </summary>
        [ServiceDependency]
        public IDomainWithDataMapperHelperService DomainWithDataMapperHelperServ
        {
            set;
            private get;
        }


        [ServiceDependency]
        public PaymentDistributionService PaymentDistributionSrv
        {
            set;
            private get;
        }

        /// <summary>
        /// Поднимает домен по его ID
        /// </summary>
        internal T1 GetItem<T1>(string _id)
        {
            return DomainWithDataMapperHelperServ.GetItem<T1>(_id);
        }

        /// <summary>
        /// Первое отображение вида
        /// </summary>
        public override void OnViewReady()
        {
            View.Intermediaries = DomainWithDataMapperHelperServ.GetList<Intermediary>();
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
            Payments = new Dictionary<int, WizardPaymentElement>();

            View.IsMasterCompleted = false;
            View.IsMasterInProgress = false;

            View.ImportType = ImportTypes.File;
            View.ManualTypeSource = ManualTypeSources.Bill;

            View.FileName = string.Empty;
            View.Intermediary = null;
            View.Comment = string.Empty;
            View.PaymentDate = ServerTime.GetDateTimeInfo().Now;

            View.ProcessingData = null;

            View.CurrentAccount = string.Empty;
            View.CurrentOwner = string.Empty;
            View.CurrentIntermediary = string.Empty;
            View.CurrentStreet = string.Empty;
            View.CurrentHouse = string.Empty;
            View.CurrentSquare = string.Empty;
            View.CurrentApartment = string.Empty;
            View.CurrentItemHasError = false;
            View.CurrentItemMessage = string.Empty;
            View.AddButtonVisible = false;

            View.ResultCount = 0;
            View.ResultValue = 0;
            View.ResultErrorCount = 0;

            View.ProcessingData = null;
            View.ResetProgressBar(1);

            View.SelectPage(WizardSteps.ChooseMethodPage);
        }

        /// <summary>
        /// Обрабатывает изменение шага мастера
        /// </summary>
        /// <param name="prevPage">Предыдущая страница</param>
        /// <param name="page">Открываемая страница</param>
        /// <param name="direction">Назад / Далее</param>
        /// <returns>Следующая страница мастера</returns>
        internal WizardSteps OnSelectedPageChanging(BaseWizardPage prevPage, BaseWizardPage page, Direction direction)
        {
            WizardSteps _next = WizardSteps.Unknown;

            if (direction == Direction.Forward)
            {
                switch (prevPage.Name)
                {
                    case "ChooseMethodWizardPage":
                        {
                            if (View.ImportType == ImportTypes.File)
                            {
                                Intermediary _intermediary = View.Intermediary;

                                if (string.IsNullOrEmpty(View.FileName))
                                {
                                    View.ShowMessage("Выберите файл для загрузки.", "Ошибка выбора файла");
                                    _next = WizardSteps.Unknown;
                                }
                                else if (_intermediary == null)
                                {
                                    View.ShowMessage("Выберите посредника платежа.", "Ошибка выбора посредника");
                                    _next = WizardSteps.Unknown;
                                }
                                else if (
                                    _intermediary.ID != IntermediaryConstants.SBRF_ID &&
                                    _intermediary.ID != IntermediaryConstants.PRIMSOCBANK_ID &&
                                    _intermediary.ID != IntermediaryConstants.KEDR_ID &&
                                    _intermediary.ID != IntermediaryConstants.MOSOBLBANK_ID &&
                                    _intermediary.ID != IntermediaryConstants.PRIMORYE_ID &&
                                    _intermediary.ID != IntermediaryConstants.UFPS_ID)
                                {
                                    View.ShowMessage("Загрузить данные из файла можно только для посредников: Сбербанк, Примсоцбанк, Кедр, Мособлбанк, Приморье, УФПС", "Ошибка выбора посредника");
                                    _next = WizardSteps.Unknown;
                                }
                                else if (_intermediary.ID == IntermediaryConstants.PRIMORYE_ID && Path.GetExtension(View.FileName) != ".xls" && Path.GetExtension(View.FileName) != ".xlsx")
                                {
                                    View.ShowMessage("Для выбранного посредника данные можно загрузить только в формате книги Microsoft Excel", "Ошибка выбора файла");
                                    _next = WizardSteps.Unknown;
                                }
                                else if (
                                    (_intermediary.ID == IntermediaryConstants.SBRF_ID ||
                                     _intermediary.ID == IntermediaryConstants.PRIMSOCBANK_ID ||
                                     _intermediary.ID == IntermediaryConstants.KEDR_ID ||
                                     _intermediary.ID == IntermediaryConstants.MOSOBLBANK_ID ||
                                     _intermediary.ID == IntermediaryConstants.UFPS_ID) &&
                                    Path.GetExtension(View.FileName) != ".txt")
                                {
                                    View.ShowMessage("Для выбранного посредника данные можно загрузить только в формате текстового файла", "Ошибка выбора файла");
                                    _next = WizardSteps.Unknown;
                                }
                                else
                                {
                                    _next = WizardSteps.ProcessingPage;
                                    View.AddButtonVisible = false;
                                }
                            }
                            else if (View.ImportType == ImportTypes.Manual)
                            {
                                if (View.ManualTypeSource == ManualTypeSources.Bill && View.Intermediary == null)
                                {
                                    View.ShowMessage("Выберите посредника платежа.", "Ошибка выбора посредника");
                                    _next = WizardSteps.Unknown;
                                }
                                else
                                {
                                    _next = WizardSteps.CheckDataPage;
                                    View.AddButtonVisible = true;

                                    FillDataGrid();

                                    if (!Payments.Any())
                                    {
                                        CreateNewPayment();
                                    }
                                }
                            }
                        }
                        break;

                    case "CheckDataWizardPage":
                        {
                            // Проверяем наличие хоть одной записи
                            if (Payments.Count == 1 && String.IsNullOrEmpty(Payments[0].Account))
                            {
                                View.ShowMessage("Введите хотя бы один платеж.", "Ошибка ввода данных");
                                _next = WizardSteps.Unknown;
                            }
                            // Проверяем на наличие хотя бы одной ошибки
                            else if (Payments.Values.Any(o => o.HasError))
                            {
                                View.ShowMessage("Исправьте ошибки в данных перед их сохранением.", "Ошибка ввода данных");
                                _next = WizardSteps.Unknown;
                            }
                            else if (!IsDistributionAvailable())
                            {
                                View.ShowMessage(
                                    "Не по всем абонентам были сделаны начисления. Невозможно внести платеж по абоненту, если по нему не сделано ни одного начисления или перерасчета.",
                                    "Невозможно выполнить операцию");
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
                            if (page.Name == "CheckDataWizardPage")
                            {
                                _next = WizardSteps.CheckDataPage;
                            }
                            else
                            {
                                _next = WizardSteps.FinishPage;
                            }
                        }
                        break;
                }
            }
            else
            {
                switch (prevPage.Name)
                {
                    case "CheckDataWizardPage":
                        _next = WizardSteps.ChooseMethodPage;

                        if (View.ImportType == ImportTypes.File)
                        {
                            Payments.Clear();
                        }

                        break;
                    case "FinishWizardPage":
                        _next = WizardSteps.ChooseMethodPage;
                        break;
                }
            }

            return _next;
        }

        bool IsDistributionAvailable()
        {
            bool _result;

            using (Entities _entities = new Entities())
            {
                _result =
                    Payments.Values
                        .Select(p => p.Account)
                        .Distinct()
                        .All(c => 
                            _entities.ChargeOpers.Any(co => co.Customers.Account == c) || 
                            _entities.RechargeOpers.Any(co => co.Customers.Account == c));
            }

            return _result;
        }

        /// <summary>
        /// Обрабатывает событие перехода на новую страницу
        /// </summary>
        /// <param name="page">Страница, на которую был осуществлен переход</param>
        /// <param name="prevPage">Страница предыдущего состояния</param>
        /// <param name="direction">Назад / Далее</param>
        internal void OnSelectedPageChanged(BaseWizardPage page, BaseWizardPage prevPage, Direction direction)
        {
            if (direction == Direction.Forward)
            {
                switch (page.Name)
                {
                    case "ProcessingWizardPage":
                        {
                            switch (prevPage.Name)
                            {
                                case "ChooseMethodWizardPage":
                                    {
                                        Thread _thread = new Thread(ProcessImportFile);
                                        _thread.Start();
                                    }
                                    break;
                                case "CheckDataWizardPage":
                                    {
                                        FillDataGrid();
                                        View.ResetProgressBar(View.ProcessingData.Rows.Count);
                                        Thread _thread = new Thread(SaveProcessingData);
                                        _thread.Start();
                                    }
                                    break;
                            }
                        }
                        break;
                    case "CheckDataWizardPage":
                        if (View.ImportType == ImportTypes.Manual && View.ManualTypeSource == ManualTypeSources.Bill)
                        {
                            View.IsBarcodeEnabled = true;
                            View.IsUseScanner = true;
                            View.SetBarcodeFocus();
                        }
                        else
                        {
                            View.IsBarcodeEnabled = false;
                            View.IsUseScanner = false;
                            View.SetAccountFocus();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Сохраняет введенные данные
        /// </summary>
        private void SaveProcessingData()
        {
            View.IsMasterInProgress = true;

            _creationDateTime = ServerTime.GetDateTimeInfo().Now;
            
            int _customerID = int.Parse(UserHolder.User.ID);

            using (Entities _entities = new Entities())
            {
                PaymentSets _paymentSet = new PaymentSets
                {
                    CreationDateTime = _creationDateTime,
                    IsFile = View.ImportType == ImportTypes.File,
                    Number = _entities.PaymentSets.Any() ? _entities.PaymentSets.Max(set => set.Number) + 1 : 1,
                    Author = _entities.Users.First(u => u.ID == _customerID),
                    PaymentDate = View.PaymentDate,
                    Comment = View.Comment,
                };

                if (View.Intermediary != null)
                {
                    int _intermediaryID = int.Parse(View.Intermediary.ID);
                    _paymentSet.Intermediaries = _entities.Intermediaries.First(i => i.ID == _intermediaryID);
                }

                _entities.AddToPaymentSets(_paymentSet);
                _entities.SaveChanges();
                _paymentSetID = _paymentSet.ID;
            }

            _totalSum = 0;
            _processedCount = _errorsCount = 0;

            var _groupedByCustomer = Payments.Values.GroupBy(p => p.Owner.ID).ToDictionary(g => int.Parse(g.Key), g => g.OrderBy(p => p.Period).ToArray());

            Parallel.ForEach(_groupedByCustomer, new ParallelOptions { MaxDegreeOfParallelism = 50 }, SaveCustomerPayments);

            using (Entities _entities = new Entities())
            {
                PaymentSets _paymentSet = _entities.PaymentSets.First(p => p.ID == _paymentSetID);
                _paymentSet.ValueSum = _totalSum;
                _paymentSet.Quantity = Convert.ToInt16(_processedCount);
                _entities.SaveChanges();
            }

            View.ResultCount = _processedCount;
            View.ResultErrorCount = _errorsCount;
            View.ResultValue = _totalSum;
            View.IsMasterInProgress = false;
            View.IsMasterCompleted = true;
            View.SelectPage(WizardSteps.FinishPage);
        }

        public void SaveCustomerPayments(KeyValuePair<int, WizardPaymentElement[]> customerPaymentsPair)
        {
            PeriodBalances _periodBalances;

            #region Запрос

            using (Entities _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                _periodBalances = new PeriodBalances(
                    _entities.ChargeOperPoses
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.ChargeOpers.Customers.ID,
                                c.ChargeOpers.ChargeSets.Period,
                                ServiceID = c.Services.ID,
                                ChargeValue = c.Value,
                                c.Value
                            })
                        .Concat(
                            _entities.ChargeOperPoses
                                .Where(c => c.ChargeOpers.ChargeCorrectionOpers != null)
                                .Select(
                                    c =>
                                    new
                                    {
                                        CustomerID = c.ChargeOpers.Customers.ID,
                                        c.ChargeOpers.ChargeCorrectionOpers.Period,
                                        ServiceID = c.Services.ID,
                                        ChargeValue = (decimal)0,
                                        Value = -1 * c.Value
                                    }))
                        .Concat(
                            _entities.RechargeOperPoses
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.RechargeOpers.Customers.ID,
                                        r.RechargeOpers.RechargeSets.Period,
                                        ServiceID = r.Services.ID,
                                        ChargeValue = (decimal)0,
                                        r.Value
                                    }))
                        .Concat(
                            _entities.RechargeOperPoses
                                .Where(r => r.RechargeOpers.ChildChargeCorrectionOpers != null)
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.RechargeOpers.Customers.ID,
                                        r.RechargeOpers.ChildChargeCorrectionOpers.Period,
                                        ServiceID = r.Services.ID,
                                        ChargeValue = (decimal)0,
                                        Value = -1 * r.Value
                                    }))
                        .Concat(
                            _entities.BenefitOperPoses
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.BenefitOpers.ChargeOpers.Customers.ID,
                                        b.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                        ServiceID = b.Services.ID,
                                        ChargeValue = (decimal)0,
                                        b.Value
                                    }))
                        .Concat(
                            _entities.BenefitOperPoses
                                .Where(b => b.BenefitOpers.BenefitCorrectionOpers != null)
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.BenefitOpers.ChargeOpers.Customers.ID,
                                        b.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        ServiceID = b.Services.ID,
                                        ChargeValue = (decimal)0,
                                        b.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOperPoses
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.RebenefitOpers.RechargeOpers.Customers.ID,
                                        b.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                        ServiceID = b.Services.ID,
                                        ChargeValue = (decimal)0,
                                        b.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOperPoses
                                .Where(b => b.RebenefitOpers.BenefitCorrectionOpers != null)
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.RebenefitOpers.RechargeOpers.Customers.ID,
                                        b.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        ServiceID = b.Services.ID,
                                        ChargeValue = (decimal)0,
                                        b.Value
                                    }))
                        .Concat(
                            _entities.OverpaymentOperPoses
                                .Select(
                                    o =>
                                    new
                                    {
                                        CustomerID = o.OverpaymentOpers.Customers.ID,
                                        o.Period,
                                        ServiceID = o.Services.ID,
                                        ChargeValue = (decimal)0,
                                        o.Value
                                    }))
                        .Concat(
                            _entities.OverpaymentCorrectionOperPoses
                                .Select(
                                    o =>
                                    new
                                    {
                                        CustomerID = o.OverpaymentCorrectionOpers.ChargeOpers.Customers.ID,
                                        o.OverpaymentCorrectionOpers.Period,
                                        ServiceID = o.Services.ID,
                                        ChargeValue = (decimal)0,
                                        o.Value
                                    }))
                        .Concat(
                            _entities.PaymentOperPoses
                                .Select(
                                    p =>
                                    new
                                    {
                                        CustomerID = p.PaymentOpers.Customers.ID,
                                        p.Period,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        p.Value
                                    }))
                        .Concat(
                            _entities.PaymentCorrectionOperPoses
                                .Select(
                                    p =>
                                    new
                                    {
                                        CustomerID = p.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                                        p.PaymentCorrectionOpers.Period,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        p.Value
                                    }))
                        .Where(c => c.CustomerID == customerPaymentsPair.Key)
                        .GroupBy(r => r.Period)
                        .Select(
                            groupedByPeriod =>
                            new
                            {
                                Period = groupedByPeriod.Key,
                                Charge = groupedByPeriod.Sum(c => c.ChargeValue),
                                Total = groupedByPeriod.Sum(c => c.Value),
                                Balances =
                                    groupedByPeriod
                                        .GroupBy(c => c.ServiceID)
                                        .Select(
                                            groupedByService =>
                                            new
                                            {
                                                ServiceID = groupedByService.Key,
                                                ValueSum = groupedByService.Sum(c => c.Value),
                                                Charged = groupedByService.Sum(c => c.ChargeValue)
                                            })
                            })
                        .OrderBy(r => r.Period)
                        .ToDictionary(
                            periodBalance => periodBalance.Period,
                            periodBalance =>
                                new ServiceBalances(
                                    periodBalance.Balances
                                        .ToDictionary(
                                            serviceBalance => serviceBalance.ServiceID,
                                            serviceBalance =>
                                            new Balance(
                                                serviceBalance.Charged,
                                                0, 0, 0, 0, 0, 
                                                serviceBalance.ValueSum)),
                                    new Balance(periodBalance.Charge, 0, 0, 0, 0, 0, periodBalance.Total))));
            }

            #endregion

            for (int i = 0; i < customerPaymentsPair.Value.Length; i++)
            {
                using (Entities _entities = new Entities())
                {
                    _entities.CommandTimeout = 3600;

                    try
                    {
                        Customers _customer = _entities.Customers.First(c => c.ID == customerPaymentsPair.Key);
                        PaymentSets _paymentSet = _entities.PaymentSets.First(p => p.ID == _paymentSetID);

                        WizardPaymentElement _payment = customerPaymentsPair.Value[i];
                        PaymentOpers _paymentOper = new PaymentOpers()
                        {
                            CreationDateTime = _creationDateTime,
                            PaymentSets = _paymentSet,
                            Customers = _customer,
                            PaymentPeriod = _payment.Period,
                            Value = -1 * _payment.Value,
                        };

                        _entities.AddToPaymentOpers(_paymentOper);

                        Dictionary<int, Services> _services = _entities.Services
                            .ToDictionary(
                                service => service.ID,
                                service => service);

                        PeriodBalances _distribution =
                            PaymentDistributionSrv.DistributePayment(
                                _periodBalances,
                                _paymentOper.PaymentSets.PaymentDate,
                                _paymentOper.PaymentPeriod,
                                ServerTime.GetPeriodInfo().LastCharged,
                                _paymentOper.Value,
                                _customer.ID);

                        foreach (KeyValuePair<DateTime, ServiceBalances> _periodBalance in _distribution.Balances)
                        {
                            foreach (KeyValuePair<int, Balance> _serviceBalance in _periodBalance.Value.Balances)
                            {
                                PaymentOperPoses _pos = new PaymentOperPoses()
                                {
                                    PaymentOpers = _paymentOper,
                                    Period = _periodBalance.Key,
                                    Services = _services[_serviceBalance.Key],
                                    Value = _serviceBalance.Value.Payment,
                                };

                                _entities.AddToPaymentOperPoses(_pos);
                                _periodBalances.AddPayment(_pos.Period, _pos.Services.ID, _pos.Value);
                            }
                        }

                        _entities.SaveChanges();

                        lock (_statusChangeLock)
                        {
                            _totalSum += -1 * _paymentOper.Value;
                            _processedCount++;
                        }
                    }
                    catch (Exception _ex)
                    {
                        lock (_errorLogLock)
                        {
                            _errorsCount++;

                            Logger.SimpleWrite(
                            string.Format(
                                "Ошибка при сохранении операции платежа, Customer: {0}, Exception: {1}\r\n",
                                customerPaymentsPair.Key,
                                _ex));
                        }
                    }
                    finally
                    {
                        View.AddProgress();
                    }
                }
            }
        }

        #region Загрузка файлов

        /// <summary>
        /// Обрабатывает файл с платежами в зависимости от выбранного посредника
        /// </summary>
        private void ProcessImportFile()
        {
            View.IsMasterInProgress = true;
            string _intermediaryID = View.Intermediary.ID;
            string _fileName = View.FileName;

            int _currentRow = 0;

            try
            {
                if (_intermediaryID == IntermediaryConstants.PRIMORYE_ID)
                {
                    using (ExcelSheet _sheet = new ExcelSheet(_fileName))
                    {
                        _currentRow = 6;
                        View.ResetProgressBar(_sheet.RowsCount - 10);

                        while (_currentRow < _sheet.RowsCount - 4)
                        {
                            Payments.Add(++_currentRow - 7, ProcessImportPrimoryeLine(_sheet.GetCell("G", _currentRow), _sheet.GetCell("H", _currentRow), _sheet.GetCell("F", _currentRow)));
                            View.AddProgress();
                        }
                    }
                }
                else
                {
                    List<string> _lines = new List<string>();

                    using (StreamReader _file = File.OpenText(View.FileName))
                    {
                        while (!_file.EndOfStream)
                        {
                            string _line = _file.ReadLine();

                            if (!string.IsNullOrEmpty(_line))
                            {
                                _lines.Add(_line);
                            }
                        }
                    }

                    View.ResetProgressBar(_lines.Count);

                    List<WizardPaymentElement> _elements;

                    switch (View.Intermediary.ID)
                    {
                        case IntermediaryConstants.PRIMSOCBANK_ID:
                        case IntermediaryConstants.UFPS_ID:
                            _elements = _lines.AsParallel().AsOrdered().Select(ProcessImportPrimsocbankLine).ToList();
                            break;
                        case IntermediaryConstants.SBRF_ID:
                            _elements = _lines.AsParallel().AsOrdered().Select(ProcessImportSbrfLine).ToList();
                            break;
                        case IntermediaryConstants.MOSOBLBANK_ID:
                            _elements = _lines.AsParallel().AsOrdered().Select(ProcessImportMosoblbankLine).ToList();
                            break;
                        /*case IntermediaryConstants.KEDR_ID:
                            _elements = _lines.AsParallel().AsOrdered().Select(ProcessImportKedrLine).ToList();
                            break;*/
                        default:
                            _elements = new List<WizardPaymentElement>();
                            break;
                    }

                    _currentRow = 0;

                    foreach (WizardPaymentElement _element in _elements)
                    {
                        Payments.Add(_currentRow++, _element);
                        View.AddProgress();
                    }
                }
            }
            catch (Exception _exception)
            {
                Logger.SimpleWrite(String.Format("Import file error. Line: {0}; {1}", _currentRow, _exception));
                View.ShowMessage("Невозможно обработать данные файла", "Ошибка импорта");
                Payments = new Dictionary<int, WizardPaymentElement>();
            }

            FillDataGrid();
            View.IsMasterInProgress = false;
            View.SelectPage(WizardSteps.CheckDataPage);
        }

        /// <summary>
        /// Обрабатывает файл с платежами от банка Приморье
        /// </summary>
        /// <param name="account">Счет</param>
        /// <param name="period">Период</param>
        /// <param name="value">Сумма</param>
        /// <returns>Набор данных по платежу</returns>
        private WizardPaymentElement ProcessImportPrimoryeLine(string account, string period, string value)
        {
            decimal _tempValue;
            WizardPaymentElement _payment = new WizardPaymentElement();

            _payment.Account = Regex.IsMatch(account, @"\d{8}")
                ? String.Format("EG-{0}-{1}-{2}", account.Substring(0, 4), account.Substring(4, 3), account.Substring(7, 1))
                : String.Empty;
            _payment.Period = Regex.IsMatch(period, @"\d{2}.\d{4}")
                ? new DateTime(Convert.ToInt32(period.Substring(3)), Convert.ToInt32(period.Substring(0, 2)), 1)
                : DateTime.MinValue;
            _payment.Value = Decimal.TryParse(value, out _tempValue) ? _tempValue : 0;
            _payment.Owner = !String.IsNullOrEmpty(_payment.Account) ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_payment.Account) : null;
            _payment.HasError = _payment.Validate();

            return _payment;
        }

        /// <summary>
        /// Обрабатывает файл с платежами от Примсоцбанка
        /// </summary>
        /// <param name="line">Строка с данными</param>
        /// <returns>Набор данных по платежу</returns>
        private WizardPaymentElement ProcessImportPrimsocbankLine(string line)
        {
            string[] _poses = line.Split('|');

            WizardPaymentElement _res = new WizardPaymentElement();

            _res.Account = _poses.Length > 6 ? _poses[6].ToUpper() : String.Empty;
            _res.Period = _poses.Length > 8 && Regex.IsMatch(_poses[8], @"\d{2}.\d{4}") ?
                new DateTime(Convert.ToInt32(_poses[8].Substring(3)), Convert.ToInt32(_poses[8].Substring(0, 2)), 1) :
                DateTime.MinValue;
            if (_poses.Length > 5)
            {
                decimal _count;
                Decimal.TryParse(_poses[5].Replace('.', ','), out _count);
                _res.Value = _count;
            }
            _res.Owner = !string.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        /// <summary>
        /// Обрабатывает файл с платежами от Сбербанк
        /// </summary>
        /// <param name="line">Строка с данными</param>
        /// <returns>Набор данных по платежу</returns>
        private WizardPaymentElement ProcessImportSbrfLine(string line)
        {
            string[] _poses = line.Split('|');

            WizardPaymentElement _res = new WizardPaymentElement();

            _res.Account = _poses.Length > 0 ? $"EG-{_poses[0]}" : string.Empty;
            _res.Period = 
                _poses.Length > 2 && Regex.IsMatch(_poses[2], @"\d{2}.\d{4}") 
                    ? new DateTime(Convert.ToInt32(_poses[2].Substring(3)), Convert.ToInt32(_poses[2].Substring(0, 2)), 1) 
                    : DateTime.MinValue;

            if (_poses.Length > 3)
            {
                string _sum =
                    _poses.Length > 6
                        ? _poses[5]
                        : _poses[3];

                decimal _value;
                decimal.TryParse(_sum.Replace('.', ','), out _value);
                _res.Value = _value;
            }

            _res.Owner = !string.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        /// <summary>
        /// Обрабатывает файл с платежами от Кедр
        /// </summary>
        /// <param name="line">Строка с данными</param>
        /// <returns>Набор данных по платежу</returns>
        private WizardPaymentElement ProcessImportKedrLine(string line)
        {
            string[] _poses = line.Split(';');

            WizardPaymentElement _res = new WizardPaymentElement();
            DateTime _lastChargedPeriod = ServerTime.GetPeriodInfo().LastCharged;

            _res.Account = _poses.Length > 1 ? _poses[1].ToUpper() : String.Empty;
            _res.Period = _lastChargedPeriod;
            if (_poses.Length > 2)
            {
                decimal _count = 0;
                Decimal.TryParse(_poses[2].Replace('.', ','), out _count);
                _res.Value = _count;
            }
            _res.Owner = !String.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        /// <summary>
        /// Обрабатывает файл с платежами от Мособлбанк
        /// </summary>
        /// <param name="line">Строка с данными</param>
        /// <returns>Набор данных по платежу</returns>
        private WizardPaymentElement ProcessImportMosoblbankLine(string line)
        {
            string[] _poses = line.Split('|');

            WizardPaymentElement _res = new WizardPaymentElement();

            _res.Account = _poses.Length > 0 ? _poses[0].ToUpper() : String.Empty;
            _res.Period = _poses.Length > 2 && Regex.IsMatch(_poses[2], @"\d{2}.\d{4}") ?
                new DateTime(Convert.ToInt32(_poses[2].Substring(3)), Convert.ToInt32(_poses[2].Substring(0, 2)), 1) :
                DateTime.MinValue;
            if (_poses.Length > 3)
            {
                decimal _count = 0;
                Decimal.TryParse(_poses[3].Replace('.', ','), out _count);
                _res.Value = _count;
            }
            _res.Owner = !string.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        #endregion

        /// <summary>
        /// Заполняет таблицу введенными данными
        /// </summary>
        private void FillDataGrid()
        {
            DataTable _data = new DataTable();
            _data.Columns.Add("ID");
            _data.Columns.Add("Account");
            _data.Columns.Add("Period");
            _data.Columns.Add("Value", typeof(decimal));
            _data.Columns.Add("Owner");
            _data.Columns.Add("HasError", typeof(bool));
            _data.PrimaryKey = new DataColumn[] { _data.Columns["ID"] };

            foreach (KeyValuePair<int, WizardPaymentElement> _element in Payments)
            {
                _data.Rows.Add(
                    _element.Key,
                    _element.Value.Account,
                    String.Format("{0:MM.yyyy}", _element.Value.Period),
                    _element.Value.Value,
                    _element.Value.Owner != null
                        ? (_element.Value.Owner.OwnerType == Customer.OwnerTypes.PhysicalPerson
                            ? _element.Value.Owner.PhysicalPersonShortName
                            : _element.Value.Owner.JuridicalPersonFullName)
                        : String.Empty,
                    _element.Value.HasError);
            }

            View.ProcessingData = _data;
        }

        /// <summary>
        /// Находит личевой счет по номеру или создает новый
        /// </summary>
        /// <param name="accountNumber">Номер л/с</param>
        public void SetCustomer(string accountNumber)
        {
            Customer _customer = !string.IsNullOrEmpty(accountNumber)
                            ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(accountNumber)
                            : null;

            FillCustomerData(_customer);

            CurrentPayment.Owner = _customer;
        }

        /// <summary>
        /// Заполняет поля, касающиеся собственника
        /// </summary>
        /// <param name="customer">Объект собственника</param>
        private void FillCustomerData(Customer customer)
        {
            if (customer != null)
            {
                string _owner = "Неизвестен";

                if (customer.OwnerType == Customer.OwnerTypes.PhysicalPerson)
                {
                    _owner = customer.PhysicalPersonShortName;
                }
                else if (customer.OwnerType == Customer.OwnerTypes.JuridicalPerson)
                {
                    _owner = customer.JuridicalPersonFullName;
                }

                View.CurrentOwner = _owner;
                View.CurrentStreet = customer.Building.Street.Name;
                View.CurrentHouse = customer.Building.Number;
                View.CurrentApartment = customer.Apartment;
                View.CurrentSquare = customer.Square.ToString();
            }
            else
            {
                View.CurrentOwner = String.Empty;
                View.CurrentStreet = String.Empty;
                View.CurrentHouse = String.Empty;
                View.CurrentApartment = String.Empty;
                View.CurrentSquare = String.Empty;
            }
        }

        /// <summary>
        /// Обрабатывает событие выбора строки в таблице с обработанными данными
        /// </summary>
        /// <param name="pos">Номер позиции</param>
        internal void OnProcesingDataRowChanged(int pos)
        {
            CurrentPayment = Payments[pos];

            View.CurrentBarcode = String.Empty;
            View.CurrentAccount = CurrentPayment.Account;
            View.CurrentPeriod = CurrentPayment.Period;
            View.CurrentValue = CurrentPayment.Value;
            View.CurrentIntermediary = View.Intermediary != null ? View.Intermediary.Name : String.Empty;

            View.CurrentItemMessage = CurrentPayment.ErrorMessage;
            View.CurrentItemHasError = CurrentPayment.HasError;

            FillCustomerData(CurrentPayment.Owner);
        }

        /// <summary>
        /// Перепроверяет корректность введенных данных
        /// </summary>
        internal bool ReValidateCurrentItem()
        {
            CurrentPayment.Account = View.CurrentAccount;
            CurrentPayment.Period = View.CurrentPeriod;
            CurrentPayment.Value = View.CurrentValue;
            CurrentPayment.Validate();

            View.CurrentItemHasError = CurrentPayment.HasError;
            View.CurrentItemMessage = CurrentPayment.ErrorMessage;

            return !CurrentPayment.HasError;
        }

        /// <summary>
        /// Создает новый платеж
        /// </summary>
        internal void CreateNewPayment()
        {
            int _key = Payments.Keys.Any() ? Payments.Keys.Max() + 1 : 0;
            CurrentPayment = new WizardPaymentElement();
            Payments.Add(_key, CurrentPayment);

            View.ProcessingData.Rows.Add(
                _key,
                String.Empty,
                String.Empty,
                0,
                String.Empty,
                false);
        }

        /// <summary>
        /// Удаляет платежи по индексам
        /// </summary>
        internal void DeletePayments(IList<int> IDs)
        {
            foreach (int _ID in IDs)
            {
                Payments.Remove(_ID);
                View.ProcessingData.Rows.Remove(View.ProcessingData.Rows.Find(_ID));
            }

            if (!Payments.Any())
            {
                CreateNewPayment();
            }
        }

        /// <summary>
        /// Возвращает сумму начислений для абонента за период
        /// </summary>
        /// <param name="account">Лицевой счет абонента</param>
        /// <param name="period">Период абонента</param>
        /// <returns>Сумма начислений</returns>
        internal decimal GetChargeValueByAccountAndPeriod(string account, DateTime period)
        {
            decimal _res;
            using (var _entities = new Taumis.Alpha.DataBase.Entities())
            {
                _res =
                    _entities.ChargeOpers
                        .Where(c => c.Customers.Account == account && c.ChargeSets.Period == period)
                        .Select(c => c.Value)
                        .FirstOrDefault();
            }

            return _res;
        }
    }
}