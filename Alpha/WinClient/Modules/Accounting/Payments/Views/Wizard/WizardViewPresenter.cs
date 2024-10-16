using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Common;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Wizard
{
    /// <summary>
    /// ���������
    /// </summary>
    public class WizardViewPresenter : BasePresenter<IWizardView>
    {
        /// <summary>
        /// ������ ��������� ������ �� ��������
        /// </summary>
        Dictionary<int, WizardPaymentElement> Payments { get; set; }

        /// <summary>
        /// ������� �������� �������
        /// </summary>
        WizardPaymentElement CurrentPayment { get; set; }

        #region Save Payments Variables

        private DateTime _creationDateTime;
        private int _paymentSetID;
        private decimal _totalSum;
        private int _processedCount;
        private int _errorsCount;
        private readonly object _statusChangeLock = new object();
        private readonly object _errorLogLock = new object();

        #endregion

        /// <summary>
        /// ������ ������ � ��������, �������� �������� � ������������
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

        [ServiceDependency]
        public IExcelService ExcelService { get; set; }

        /// <summary>
        /// ��������� ����� �� ��� ID
        /// </summary>
        internal T1 GetItem<T1>(string _id)
        {
            return DomainWithDataMapperHelperServ.GetItem<T1>(_id);
        }

        /// <summary>
        /// ������ ����������� ����
        /// </summary>
        public override void OnViewReady()
        {
            View.Intermediaries = DomainWithDataMapperHelperServ.GetList<Intermediary>();
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
        /// ������������ ��������� ���� �������
        /// </summary>
        /// <param name="prevPage">���������� ��������</param>
        /// <param name="page">����������� ��������</param>
        /// <param name="direction">����� / �����</param>
        /// <returns>��������� �������� �������</returns>
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
                                    View.ShowMessage("�������� ���� ��� ��������.", "������ ������ �����");
                                    _next = WizardSteps.Unknown;
                                }
                                else if (_intermediary == null)
                                {
                                    View.ShowMessage("�������� ���������� �������.", "������ ������ ����������");
                                    _next = WizardSteps.Unknown;
                                }
                                else if (
                                    _intermediary.ID != IntermediaryConstants.SBRF_ID &&
                                    _intermediary.ID != IntermediaryConstants.PRIMSOCBANK_ID &&
                                    _intermediary.ID != IntermediaryConstants.KEDR_ID &&
                                    _intermediary.ID != IntermediaryConstants.MOSOBLBANK_ID &&
                                    _intermediary.ID != IntermediaryConstants.PRIMORYE_ID &&
                                    _intermediary.ID != IntermediaryConstants.UFPS_ID &&
                                    _intermediary.ID != IntermediaryConstants.POCHTABANK_ID)
                                {
                                    View.ShowMessage("��������� ������ �� ����� ����� ������ ��� �����������: ��������, �����������, ����, ����������, ��������, ����, ����� ����", "������ ������ ����������");
                                    _next = WizardSteps.Unknown;
                                }
                                else if (_intermediary.ID == IntermediaryConstants.PRIMORYE_ID && Path.GetExtension(View.FileName) != ".xlsx")
                                {
                                    View.ShowMessage("��� ���������� ���������� ������ ����� ��������� ������ � ������� ����� Microsoft Excel", "������ ������ �����");
                                    _next = WizardSteps.Unknown;
                                }
                                else if (
                                    (_intermediary.ID == IntermediaryConstants.SBRF_ID ||
                                     _intermediary.ID == IntermediaryConstants.PRIMSOCBANK_ID ||
                                     _intermediary.ID == IntermediaryConstants.KEDR_ID ||
                                     _intermediary.ID == IntermediaryConstants.MOSOBLBANK_ID ||
                                     _intermediary.ID == IntermediaryConstants.UFPS_ID ||
                                     _intermediary.ID == IntermediaryConstants.POCHTABANK_ID) &&
                                    Path.GetExtension(View.FileName) != ".txt")
                                {
                                    View.ShowMessage("��� ���������� ���������� ������ ����� ��������� ������ � ������� ���������� �����", "������ ������ �����");
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
                                    View.ShowMessage("�������� ���������� �������.", "������ ������ ����������");
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
                            // ��������� ������� ���� ����� ������
                            if (Payments.Count == 1 && string.IsNullOrEmpty(Payments.First().Value.Account))
                            {
                                View.ShowMessage("������� ���� �� ���� ������.", "������ ����� ������");
                                _next = WizardSteps.Unknown;
                            }
                            // ��������� �� ������� ���� �� ����� ������
                            else if (Payments.Values.Any(o => o.HasError))
                            {
                                View.ShowMessage("��������� ������ � ������ ����� �� �����������.", "������ ����� ������");
                                _next = WizardSteps.Unknown;
                            }
                            else if (!IsDistributionAvailable(out string _accounts))
                            {
                                View.ShowMessage(
                                    $"{_accounts} ����������� ����������. ���������� ������ ������ �� ��������, ���� �� ���� �� ������� �� ������ ���������� ��� �����������.",
                                    "���������� ��������� ��������");
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

        private bool IsDistributionAvailable(out string accounts)
        {
            bool _result;

            using (Entities _db = new Entities())
            {
                var _accounts =
                    Payments.Values
                        .Select(p => p.Account)
                        .Distinct()
                        .Where(c =>
                            _db.ChargeOpers.Where(x => x.Customers.Account == c).All(x => x.Value == 0)
                            && _db.RechargeOpers.Where(x => x.Customers.Account == c).All(x => x.Value == 0))
                        .ToArray();

                _result = _accounts.Length == 0;

                accounts = string.Join(", ", _accounts);
            }

            return _result;
        }

        /// <summary>
        /// ������������ ������� �������� �� ����� ��������
        /// </summary>
        /// <param name="page">��������, �� ������� ��� ����������� �������</param>
        /// <param name="prevPage">�������� ����������� ���������</param>
        /// <param name="direction">����� / �����</param>
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
        /// ��������� ��������� ������
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
            Dictionary<DateTime, Dictionary<int, Balance>> _debtBalancesByPeriod;

            #region ������

            using (Entities _db = new Entities())
            {
                _db.CommandTimeout = 3600;
                _debtBalancesByPeriod = _db.GetCustomerBalancesGroupedByPeriod(customerPaymentsPair.Key, afterGroupFilter: x => x.Total > 0);
                Tuple<DateTime, Dictionary<int, Balance>> _lastChargedPeriodBalance =
                    _db.GetLastChargedPeriodBalance(customerPaymentsPair.Key);

                if (!_debtBalancesByPeriod.ContainsKey(_lastChargedPeriodBalance.Item1))
                {
                    _debtBalancesByPeriod.Add(_lastChargedPeriodBalance.Item1, _lastChargedPeriodBalance.Item2);
                }
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

                        Dictionary<DateTime, Dictionary<int, decimal>> _distribution =
                            PaymentDistributionSrv.DistributePayment(
                                _debtBalancesByPeriod,
                                _paymentOper.PaymentSets.PaymentDate,
                                _paymentOper.PaymentPeriod,
                                ServerTime.GetPeriodInfo().LastCharged,
                                _paymentOper.Value,
                                _customer.ID);

                        foreach (var periodBalancePair in _distribution)
                        {
                            foreach (var _serviceValuePair in periodBalancePair.Value)
                            {
                                PaymentOperPoses _pos = new PaymentOperPoses()
                                {
                                    PaymentOpers = _paymentOper,
                                    Period = periodBalancePair.Key,
                                    Services = _services[_serviceValuePair.Key],
                                    Value = _serviceValuePair.Value,
                                };

                                _entities.AddToPaymentOperPoses(_pos);
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
                                "������ ��� ���������� �������� �������, Customer: {0}, Exception: {1}\r\n",
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

        #region �������� ������

        /// <summary>
        /// ������������ ���� � ��������� � ����������� �� ���������� ����������
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
                    using (IExcelWorkbook _wb = ExcelService.OpenWorkbook(_fileName))
                    {
                        IExcelWorksheet _ws = _wb.Worksheet(1);

                        _currentRow = 6;
                        int _rowCount = _ws.GetRowCount();
                        View.ResetProgressBar(_rowCount - 10);

                        while (_currentRow < _rowCount - 4)
                        {
                            Payments.Add(++_currentRow - 7, ProcessImportPrimoryeLine(_ws.Cell(_currentRow, "G").Value, _ws.Cell(_currentRow, "H").Value, _ws.Cell(_currentRow, "F").Value));
                            View.AddProgress();
                        }
                    }
                }
                else
                {
                    Encoding _encoding =
                        (_intermediaryID == IntermediaryConstants.SBRF_ID
                        || _intermediaryID == IntermediaryConstants.POCHTABANK_ID)
                            ? Encoding.GetEncoding(1251)
                            : Encoding.UTF8;

                    string[] _lines = File.ReadAllLines(View.FileName, _encoding);

                    View.ResetProgressBar(_lines.Length);

                    List<WizardPaymentElement> _elements;

                    switch (View.Intermediary.ID)
                    {
                        case IntermediaryConstants.PRIMSOCBANK_ID:
                        case IntermediaryConstants.UFPS_ID:
                            _elements = _lines.AsParallel().AsOrdered().Select(ProcessImportPrimsocbankLine).ToList();
                            break;
                        case IntermediaryConstants.SBRF_ID:
                            _elements = _lines
                                .Where(x => !string.IsNullOrEmpty(x) && !x.StartsWith("="))
                                .AsParallel()
                                .AsOrdered()
                                .Select(ProcessImportSbrfLine)
                                .ToList();
                            break;
                        case IntermediaryConstants.MOSOBLBANK_ID:
                            _elements = _lines.AsParallel().AsOrdered().Select(ProcessImportMosoblbankLine).ToList();
                            break;
                        case IntermediaryConstants.POCHTABANK_ID:
                            _elements = _lines
                                .Where(x => !string.IsNullOrEmpty(x) && !x.StartsWith("="))
                                .AsParallel()
                                .AsOrdered()
                                .Select(ProcessImportPochtabankLine)
                                .ToList();
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
                Logger.SimpleWrite(string.Format("Import file error. Line: {0}; {1}", _currentRow, _exception));
                View.ShowMessage("���������� ���������� ������ �����", "������ �������");
                Payments = new Dictionary<int, WizardPaymentElement>();
            }

            FillDataGrid();
            View.IsMasterInProgress = false;
            View.SelectPage(WizardSteps.CheckDataPage);
        }

        /// <summary>
        /// ������������ ���� � ��������� �� ����� ��������
        /// </summary>
        /// <param name="account">����</param>
        /// <param name="period">������</param>
        /// <param name="value">�����</param>
        /// <returns>����� ������ �� �������</returns>
        private WizardPaymentElement ProcessImportPrimoryeLine(string account, string period, string value)
        {
            WizardPaymentElement _payment = new WizardPaymentElement
            {
                Account = Regex.IsMatch(account, @"\d{8}")
                    ? string.Format("{0}-{1}-{2}", account.Substring(0, 4), account.Substring(4, 3), account.Substring(7, 1))
                    : string.Empty,
                Period = Regex.IsMatch(period, @"\d{2}.\d{4}")
                    ? new DateTime(Convert.ToInt32(period.Substring(3)), Convert.ToInt32(period.Substring(0, 2)), 1)
                    : DateTime.MinValue,
                Value = decimal.TryParse(value, out decimal _tempValue) ? _tempValue : 0,
            };

            _payment.Owner = !string.IsNullOrEmpty(_payment.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_payment.Account)
                : null;
            _payment.HasError = _payment.Validate();

            return _payment;
        }

        /// <summary>
        /// ������������ ���� � ��������� �� ������������
        /// </summary>
        /// <param name="line">������ � �������</param>
        /// <returns>����� ������ �� �������</returns>
        private WizardPaymentElement ProcessImportPrimsocbankLine(string line)
        {
            string[] _poses = line.Split('|');

            WizardPaymentElement _res = new WizardPaymentElement
            {
                Account = _poses.Length > 6 ? _poses[6].ToUpper() : string.Empty,
                Period = _poses.Length > 8 && Regex.IsMatch(_poses[8], @"\d{2}.\d{4}")
                    ? new DateTime(Convert.ToInt32(_poses[8].Substring(3)), Convert.ToInt32(_poses[8].Substring(0, 2)), 1)
                    : DateTime.MinValue
            };

            if (_poses.Length > 5)
            {
                decimal.TryParse(_poses[5].Replace('.', ','), out decimal _count);
                _res.Value = _count;
            }

            _res.Owner = !string.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        /// <summary>
        /// ������������ ���� � ��������� �� ��������
        /// </summary>
        /// <param name="line">������ � �������</param>
        /// <returns>����� ������ �� �������</returns>
        private WizardPaymentElement ProcessImportSbrfLine(string line)
        {
            string[] _poses = line.Split(';');

            WizardPaymentElement _res = new WizardPaymentElement
            {
                Account = _poses.Length > 6 ? _poses[5] : string.Empty,
                Period = _poses.Length > 8 && Regex.IsMatch(_poses[8], @"\d{4}")
                    ? new DateTime(Convert.ToInt32("20" + _poses[8].Substring(2)), Convert.ToInt32(_poses[8].Substring(0, 2)), 1)
                    : DateTime.MinValue
            };
            if (_poses.Length > 10)
            {
                decimal.TryParse(_poses[9], out decimal _count);
                _res.Value = _count;
            }
            _res.Owner = !string.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        /// <summary>
        /// ������������ ���� � ��������� �� ����
        /// </summary>
        /// <param name="line">������ � �������</param>
        /// <returns>����� ������ �� �������</returns>
        private WizardPaymentElement ProcessImportKedrLine(string line)
        {
            string[] _poses = line.Split(';');

            WizardPaymentElement _res = new WizardPaymentElement();
            DateTime _lastChargedPeriod = ServerTime.GetPeriodInfo().LastCharged;

            _res.Account = _poses.Length > 1 ? _poses[1].ToUpper() : string.Empty;
            _res.Period = _lastChargedPeriod;
            if (_poses.Length > 2)
            {
                decimal.TryParse(_poses[2].Replace('.', ','), out decimal _count);
                _res.Value = _count;
            }
            _res.Owner = !string.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        /// <summary>
        /// ������������ ���� � ��������� �� ����������
        /// </summary>
        /// <param name="line">������ � �������</param>
        /// <returns>����� ������ �� �������</returns>
        private WizardPaymentElement ProcessImportMosoblbankLine(string line)
        {
            string[] _poses = line.Split('|');

            WizardPaymentElement _res = new WizardPaymentElement
            {
                Account = _poses.Length > 0 ? _poses[0].ToUpper() : string.Empty,
                Period = _poses.Length > 2 && Regex.IsMatch(_poses[2], @"\d{2}.\d{4}")
                    ? new DateTime(Convert.ToInt32(_poses[2].Substring(3)), Convert.ToInt32(_poses[2].Substring(0, 2)), 1)
                    : DateTime.MinValue
            };

            if (_poses.Length > 3)
            {
                decimal.TryParse(_poses[3].Replace('.', ','), out decimal _count);
                _res.Value = _count;
            }

            _res.Owner = !string.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        private WizardPaymentElement ProcessImportPochtabankLine(string line)
        {
            string[] _poses = line.Split(';');

            WizardPaymentElement _res = new WizardPaymentElement
            {
                Account =
                    _poses.Length >= 5
                        ? Regex.IsMatch(_poses[5], @"\d{8}")
                            ? string.Format(
                                "{0}-{1}-{2}",
                                _poses[5].Substring(0, 4),
                                _poses[5].Substring(4, 3),
                                _poses[5].Substring(7, 1))
                            : Regex.IsMatch(_poses[5], @"\d{4}-\d{3}-\d{1}")
                                ? _poses[5]
                                : string.Empty
                        : string.Empty,

                Period = _poses.Length >= 12 && Regex.IsMatch(_poses[12], @"\d{2}.\d{2}")
                    ? new DateTime(int.Parse("20" + _poses[12].Substring(3)), int.Parse(_poses[12].Substring(0, 2)), 1)
                    : DateTime.MinValue,

                Value = _poses.Length >= 9 && decimal.TryParse(_poses[9].Replace('.', ','), out decimal _value)
                    ? _value
                    : 0,
            };

            _res.Owner = !string.IsNullOrEmpty(_res.Account)
                ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(_res.Account)
                : null;

            _res.HasError = _res.Validate();

            return _res;
        }

        #endregion

        /// <summary>
        /// ��������� ������� ���������� �������
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
                    string.Format("{0:MM.yyyy}", _element.Value.Period),
                    _element.Value.Value,
                    _element.Value.Owner != null
                        ? (_element.Value.Owner.OwnerType == OwnerType.PhysicalPerson
                            ? _element.Value.Owner.PhysicalPersonShortName
                            : _element.Value.Owner.JuridicalPersonFullName)
                        : string.Empty,
                    _element.Value.HasError);
            }

            View.ProcessingData = _data;
        }

        /// <summary>
        /// ������� ������� ���� �� ������ ��� ������� �����
        /// </summary>
        /// <param name="accountNumber">����� �/�</param>
        public void SetCustomer(string accountNumber)
        {
            Customer _customer = !string.IsNullOrEmpty(accountNumber)
                            ? DomainWithDataMapperHelperServ.DataMapper<Customer, ICustomerDataMapper>().GetItem(accountNumber)
                            : null;

            FillCustomerData(_customer);

            CurrentPayment.Owner = _customer;
        }

        /// <summary>
        /// ��������� ����, ���������� ������������
        /// </summary>
        /// <param name="customer">������ ������������</param>
        private void FillCustomerData(Customer customer)
        {
            if (customer != null)
            {
                string _owner = "����������";

                if (customer.OwnerType == OwnerType.PhysicalPerson)
                {
                    _owner = customer.PhysicalPersonShortName;
                }
                else if (customer.OwnerType == OwnerType.JuridicalPerson)
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
                View.CurrentOwner = string.Empty;
                View.CurrentStreet = string.Empty;
                View.CurrentHouse = string.Empty;
                View.CurrentApartment = string.Empty;
                View.CurrentSquare = string.Empty;
            }
        }

        /// <summary>
        /// ������������ ������� ������ ������ � ������� � ������������� �������
        /// </summary>
        /// <param name="pos">����� �������</param>
        internal void OnProcesingDataRowChanged(int pos)
        {
            CurrentPayment = Payments[pos];

            View.CurrentBarcode = string.Empty;
            View.CurrentAccount = CurrentPayment.Account;
            View.CurrentPeriod = CurrentPayment.Period;
            View.CurrentValue = CurrentPayment.Value;
            View.CurrentIntermediary = View.Intermediary != null ? View.Intermediary.Name : string.Empty;

            View.CurrentItemMessage = CurrentPayment.ErrorMessage;
            View.CurrentItemHasError = CurrentPayment.HasError;

            FillCustomerData(CurrentPayment.Owner);
        }

        /// <summary>
        /// ������������� ������������ ��������� ������
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
        /// ������� ����� ������
        /// </summary>
        internal void CreateNewPayment()
        {
            int _key = Payments.Keys.Any() ? Payments.Keys.Max() + 1 : 0;
            CurrentPayment = new WizardPaymentElement();
            Payments.Add(_key, CurrentPayment);

            View.ProcessingData.Rows.Add(
                _key,
                string.Empty,
                string.Empty,
                0,
                string.Empty,
                false);
        }

        /// <summary>
        /// ������� ������� �� ��������
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
        /// ���������� ����� ���������� ��� �������� �� ������
        /// </summary>
        /// <param name="account">������� ���� ��������</param>
        /// <param name="period">������ ��������</param>
        /// <returns>����� ����������</returns>
        internal decimal GetChargeValueByAccountAndPeriod(string account, DateTime period)
        {
            decimal _res;
            using (var _entities = new Entities())
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