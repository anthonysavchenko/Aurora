using System.Data.Entity.Core;
using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Services;
using OperationTypes = Taumis.Alpha.Infrastructure.Interface.Enums.OperationTypes;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Wizard
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class WizardViewPresenter : BasePresenter<IWizardView>
    {
        /// <summary>
        /// Абонент и сумма долга
        /// </summary>
        private class CustomerValue
        {
            /// <summary>
            /// Сумма
            /// </summary>
            public decimal Value
            {
                set;
                get;
            }

            /// <summary>
            /// Абонент
            /// </summary>
            public int CustomerID
            {
                set;
                get;
            }
        }

        private class ServiceBalances
        {
            public IDictionary<int, Balance> Balances
            {
                set;
                get;
            }

            public ServiceBalances()
            {
                Balances = new Dictionary<int, Balance>();
            }

            public void Add(int key, Balance value)
            {
                if (!Balances.ContainsKey(key))
                {
                    Balances.Add(key, new Balance());
                }

                Balances[key].Add(value);
            }
        }

        private class Balance
        {
            public decimal Charge
            {
                set;
                get;
            }

            public decimal Benefit
            {
                set;
                get;
            }

            public decimal Correction
            {
                set;
                get;
            }

            public decimal Subtotal
            {
                set;
                get;
            }

            public decimal Payment
            {
                set;
                get;
            }

            public decimal Total
            {
                set;
                get;
            }

            public void Add(Balance value)
            {
                Charge += value.Charge;
                Benefit += value.Benefit;
                Correction += value.Correction;
                Subtotal += value.Subtotal;
                Payment += value.Payment;
                Total += value.Total;
            }
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
            View.ResetProgressBar(1);
            View.IsMasterInProgress = false;
            View.IsMasterCompleted = false;

            View.MinValue = 5000;
            View.TillDate = ServerTime.GetDateTimeInfo().Now;

            View.TotalBillAccount = String.Empty;
            View.TotalBillTillPeriod = ServerTime.GetPeriodInfo().LastCharged;

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
                            if (View.ReceiptType == ReceiptTypes.Total)
                            {
                                bool _isCustomerExists;

                                using (var _entities = new Entities())
                                {
                                    _isCustomerExists = _entities.Customers.Any(_customer => _customer.Account == View.TotalBillAccount);
                                }

                                if (!_isCustomerExists)
                                {
                                    View.ShowMessage("По введенному лицевому счету абонент не найден", "Ошибка ввода лицевого счета");
                                    _next = WizardSteps.Unknown;
                                }
                                else
                                {
                                    _next = WizardSteps.ProcessingPage;
                                }
                            }
                            else
                            {
                                _next = WizardSteps.ProcessingPage;
                            }
                        }
                        break;

                    case "ProcessingWizardPage":
                        {
                            _next = WizardSteps.FinishPage;
                        }
                        break;

                    case "FinishWizardPage":
                        {
                            FinishWizard();
                        }
                        break;
                }
            }
            else
            {
                switch (prevPage.Name)
                {
                    case "FinishWizardPage":
                        _next = WizardSteps.ChooseMethodPage;
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
        internal void OnSelectedPageChanged(BaseWizardPage page, BaseWizardPage prevPage, Direction direction)
        {
            if (direction == Direction.Forward)
            {
                switch (page.Name)
                {
                    case "ProcessingWizardPage":
                        {
                            switch (View.ReceiptType)
                            {
                                case ReceiptTypes.Debt:
                                    GenerateDebtBills();
                                    break;

                                case ReceiptTypes.Total:
                                    GenerateTotalBills();
                                    break;
                            }
                            View.IsMasterCompleted = true;
                            View.SelectPage(WizardSteps.FinishPage);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Формирует долговые квитанции
        /// </summary>
        private void GenerateDebtBills()
        {
            View.IsMasterInProgress = true;

            short _processedCount = 0;
            short _errorsCount = 0;
            decimal _totalValue = 0;
            int _billSetID;
            BillSets _billSet;
            List<CustomerValue> _customerValues;
            DateTime _now = ServerTime.GetDateTimeInfo().Now;

            using (var _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                _billSet = 
                    new BillSets
                    {
                        CreationDateTime = _now,
                        BillType = (byte)BillSet.BillTypes.Debt,
                        Number = _entities.BillSets.Any() ? _entities.BillSets.Max(c => c.Number) + 1 : 1,
                    };

                _entities.AddToBillSets(_billSet);
                _entities.SaveChanges();
                _billSetID = _billSet.ID;

                DateTime _filterPeriod = new DateTime(View.TillDate.Year, View.TillDate.Month, 1);

                #region Запрос

                _customerValues =
                    _entities.ChargeOpers
                        .Select(
                            c =>
                                new
                                {
                                    CustomerID = c.Customers.ID,
                                    c.ChargeSets.Period,
                                    c.Value
                                })
                        .Concat(
                            _entities.ChargeOpers
                                .Where(c => c.ChargeCorrectionOpers != null)
                                .Select(
                                    c =>
                                        new
                                        {
                                            CustomerID = c.Customers.ID,
                                            c.ChargeCorrectionOpers.Period,
                                            Value = -1 * c.Value
                                        }))
                        .Concat(
                            _entities.RechargeOpers
                                .Select(
                                    r =>
                                        new
                                        {
                                            CustomerID = r.Customers.ID,
                                            r.RechargeSets.Period,
                                            r.Value
                                        }))
                        .Concat(
                            _entities.RechargeOpers
                                .Where(r => r.ChildChargeCorrectionOpers != null)
                                .Select(
                                    r =>
                                        new
                                        {
                                            CustomerID = r.Customers.ID,
                                            r.ChildChargeCorrectionOpers.Period,
                                            Value = -1 * r.Value
                                        }))
                        .Concat(
                            _entities.BenefitOpers
                                .Select(
                                    b =>
                                        new
                                        {
                                            CustomerID = b.ChargeOpers.Customers.ID,
                                            b.ChargeOpers.ChargeSets.Period,
                                            b.Value
                                        }))
                        .Concat(
                            _entities.BenefitOpers
                                .Where(b => b.BenefitCorrectionOpers != null)
                                .Select(
                                    b =>
                                        new
                                        {
                                            CustomerID = b.ChargeOpers.Customers.ID,
                                            b.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                            Value = -1 * b.Value
                                        }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Select(
                                    r =>
                                        new
                                        {
                                            CustomerID = r.RechargeOpers.Customers.ID,
                                            r.RechargeOpers.RechargeSets.Period,
                                            r.Value
                                        }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Where(r => r.BenefitCorrectionOpers != null)
                                .Select(
                                    r =>
                                        new
                                        {
                                            CustomerID = r.RechargeOpers.Customers.ID,
                                            r.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                            Value = -1 * r.Value
                                        }))
                        .Concat(
                            _entities.OverpaymentOperPoses
                                .Select(
                                    o =>
                                        new
                                        {
                                            CustomerID = o.OverpaymentOpers.Customers.ID,
                                            o.Period,
                                            o.Value
                                        }))
                        .Concat(
                            _entities.OverpaymentCorrectionOpers
                                .Select(
                                    o =>
                                        new
                                        {
                                            CustomerID = o.ChargeOpers.Customers.ID,
                                            o.Period,
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
                                            p.Value
                                        }))
                        .Concat(
                            _entities.PaymentCorrectionOpers
                                .Select(
                                    p =>
                                        new
                                        {
                                            CustomerID = p.PaymentOpers.Customers.ID,
                                            p.Period,
                                            p.Value
                                        }))
                        .Where(c => c.Period < _filterPeriod)
                        .GroupBy(c => c.CustomerID)
                        .Select(
                            g =>
                                new CustomerValue
                                {
                                    CustomerID = g.Key,
                                    Value = g.Sum(c => c.Value)
                                })
                        .Where(c => c.Value >= View.MinValue)
                        .ToList();

                #endregion
            }

            View.ResetProgressBar(_customerValues.Count());
            Application.DoEvents();

            foreach (var _sum in _customerValues)
            {
                using (var _entities = new Entities())
                {
                    try
                    {
                        _billSet = (BillSets)_entities.GetObjectByKey(new EntityKey("Entities.BillSets", "ID", _billSetID));

                        Customers _customer = 
                            _entities.Customers
                                .Include("Buildings")
                                .Include("Buildings.Streets")
                                .First(_x => _x.ID == _sum.CustomerID);

                        DebtBillDocs _bill = 
                            new DebtBillDocs
                            {
                                BillSets = _billSet,
                                Value = _sum.Value,
                                Period = new DateTime(View.TillDate.Year, View.TillDate.Month, 1, 0, 0, 0),
                                CreationDateTime = _now,
                                Account = _customer.Account,
                                Address = _customer.Buildings != null 
                                    ? ($"ул. {_customer.Buildings.Streets.Name}, {_customer.Buildings.Number}, кв. {_customer.Apartment}")
                                    : string.Empty,
                                Owner = _customer.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson ?
                                        _customer.JuridicalPersonFullName :
                                        _customer.PhysicalPersonShortName,
                                Customers = _customer
                            };

                        _entities.AddToDebtBillDocs(_bill);

                        _processedCount++;
                        _totalValue += _sum.Value;
                        _billSet.Quantity = _processedCount;
                        _billSet.ValueSum = _totalValue;

                        _entities.SaveChanges();

                    }
                    catch (Exception _ex)
                    {
                        _errorsCount++;
                        Logger.SimpleWrite($"Ошибка при сохранении долговой квитанции.\r\n{_ex}");
                    }
                    finally
                    {
                        Application.DoEvents();
                        View.AddProgress(1);
                        Application.DoEvents();
                    }
                }

                View.ResultCount = _processedCount;
                View.ResultErrorCount = _errorsCount;
                View.ResultValue = _totalValue;
            }

            View.IsMasterInProgress = false;
        }

        /// <summary>
        /// Формирует квитанции о доплате за период
        /// </summary>
        private void GenerateTotalBills()
        {
            View.IsMasterInProgress = true;
            short _errorsCount = 0;
            decimal _totalValue = 0;
            DateTime _now = ServerTime.GetDateTimeInfo().Now;
            DateTime _firstUncharged = ServerTime.GetPeriodInfo().FirstUncharged;
            DateTime _period = View.TotalBillTillPeriod;

            try
            {
                using (var _entities = new Entities())
                {
                    _entities.CommandTimeout = 3600;

                    BillSets _billSet = 
                        new BillSets()
                        {
                            CreationDateTime = _now,
                            BillType = (byte)BillSet.BillTypes.Total,
                            Quantity = 0,
                            Number = _entities.BillSets.Any() ? _entities.BillSets.Max(c => c.Number) + 1 : 1,
                        };

                    _entities.AddToBillSets(_billSet);

                    View.ResetProgressBar(1);
                    Application.DoEvents();

                    #region Запросы

                    var _customer =
                        _entities.Customers
                            .Include("Residents")
                            .Include("Buildings")
                            .Include("Buildings.Streets")
                            .First(x => x.Account == View.TotalBillAccount);

                    var _customerPeriodBalances =
                        _entities.ChargeOperPoses
                            .Select(c =>
                                new
                                {
                                    CustomerID = c.ChargeOpers.Customers.ID,
                                    c.ChargeOpers.ChargeSets.Period,
                                    ServiceTypeID = c.Services.ServiceTypes.ID,
                                    Charge = c.Value,
                                    Recharge = (decimal)0,
                                    Payment = (decimal)0,
                                    Benefit = (decimal)0
                                })
                            .Concat(
                                _entities.ChargeOperPoses
                                    .Where(c => c.ChargeOpers.ChargeCorrectionOpers != null)
                                    .Select(c =>
                                        new
                                        {
                                            CustomerID = c.ChargeOpers.Customers.ID,
                                            c.ChargeOpers.ChargeCorrectionOpers.Period,
                                            ServiceTypeID = c.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = -1 * c.Value,
                                            Payment = (decimal)0,
                                            Benefit = (decimal)0
                                        }))
                            .Concat(
                                _entities.RechargeOperPoses
                                    .Select(
                                        r =>
                                        new
                                        {
                                            CustomerID = r.RechargeOpers.Customers.ID,
                                            r.RechargeOpers.RechargeSets.Period,
                                            ServiceTypeID = r.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = r.Value,
                                            Payment = (decimal)0,
                                            Benefit = (decimal)0
                                        }))
                            .Concat(
                                _entities.RechargeOperPoses
                                    .Where(r => r.RechargeOpers.ChildChargeCorrectionOpers != null)
                                    .Select(r =>
                                        new
                                        {
                                            CustomerID = r.RechargeOpers.Customers.ID,
                                            r.RechargeOpers.ChildChargeCorrectionOpers.Period,
                                            ServiceTypeID = r.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = -1 * r.Value,
                                            Payment = (decimal)0,
                                            Benefit = (decimal)0
                                        }))
                            .Concat(
                                _entities.BenefitOperPoses
                                    .Select(b =>
                                        new
                                        {
                                            CustomerID = b.BenefitOpers.ChargeOpers.Customers.ID,
                                            b.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                            ServiceTypeID = b.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Payment = (decimal)0,
                                            Benefit = b.Value
                                        }))
                            .Concat(
                                _entities.BenefitOperPoses
                                    .Where(b => b.BenefitOpers.BenefitCorrectionOpers != null)
                                    .Select(b =>
                                        new
                                        {
                                            CustomerID = b.BenefitOpers.ChargeOpers.Customers.ID,
                                            b.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                            ServiceTypeID = b.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Payment = (decimal)0,
                                            Benefit = -1 * b.Value
                                        }))
                            .Concat(
                                _entities.RebenefitOperPoses
                                    .Select(r =>
                                        new
                                        {
                                            CustomerID = r.RebenefitOpers.RechargeOpers.Customers.ID,
                                            r.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                            ServiceTypeID = r.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Payment = (decimal)0,
                                            Benefit = r.Value
                                        }))
                            .Concat(
                                _entities.RebenefitOperPoses
                                    .Where(r => r.RebenefitOpers.BenefitCorrectionOpers != null)
                                    .Select(r =>
                                        new
                                        {
                                            CustomerID = r.RebenefitOpers.RechargeOpers.Customers.ID,
                                            r.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                            ServiceTypeID = r.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Payment = (decimal)0,
                                            Benefit = -1 * r.Value
                                        }))
                            .Concat(
                                _entities.OverpaymentOperPoses
                                    .Select(o =>
                                        new
                                        {
                                            CustomerID = o.OverpaymentOpers.Customers.ID,
                                            o.Period,
                                            ServiceTypeID = o.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Payment = o.Value,
                                            Benefit = (decimal)0
                                        }))
                            .Concat(
                                _entities.OverpaymentCorrectionOperPoses
                                    .Where(o => o.OverpaymentCorrectionOpers.Period < _period)
                                    .Select(o =>
                                        new
                                        {
                                            CustomerID = o.OverpaymentCorrectionOpers.ChargeOpers.Customers.ID,
                                            o.OverpaymentCorrectionOpers.Period,
                                            ServiceTypeID = o.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Payment = o.Value,
                                            Benefit = (decimal)0
                                        }))
                            .Concat(
                                _entities.PaymentOperPoses
                                    .Select(p =>
                                        new
                                        {
                                            CustomerID = p.PaymentOpers.Customers.ID,
                                            p.Period,
                                            ServiceTypeID = p.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Payment = p.Value,
                                            Benefit = (decimal)0
                                        }))
                            .Concat(
                                _entities.PaymentCorrectionOperPoses
                                    .Select(p =>
                                        new
                                        {
                                            CustomerID = p.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                                            p.PaymentCorrectionOpers.Period,
                                            ServiceTypeID = p.Services.ServiceTypes.ID,
                                            Charge = (decimal)0,
                                            Recharge = (decimal)0,
                                            Payment = p.Value,
                                            Benefit = (decimal)0
                                        }))
                            .Where(c => c.CustomerID == _customer.ID && c.Period <= _period)
                            .GroupBy(c => new { c.Period, c.ServiceTypeID })
                            .Select(g =>
                                new
                                {
                                    g.Key.Period,
                                    g.Key.ServiceTypeID,
                                    Charge = g.Sum(c => c.Charge),
                                    Recharge = g.Sum(c => c.Recharge),
                                    Payment = g.Sum(c => c.Payment),
                                    Benefit = g.Sum(c => c.Benefit)
                                })
                            .ToList()
                            .GroupBy(c => c.Period)
                            .Select(
                                byPeriod =>
                                new
                                {
                                    Period = byPeriod.Key,
                                    IsCharged = byPeriod.Key < _firstUncharged,
                                    Balances =
                                        byPeriod.GroupBy(c => c.ServiceTypeID)
                                            .Select(
                                                byServiceType =>
                                                new
                                                {
                                                    ServiceTypeID = byServiceType.Key,
                                                    Charge = byServiceType.Sum(c => c.Charge) + byServiceType.Sum(c => c.Recharge),
                                                    Payment = byServiceType.Sum(p => p.Payment),
                                                    Benefit = byServiceType.Sum(b => b.Benefit),
                                                })
                                })
                            .Where(c => c.IsCharged)
                            .OrderBy(c => c.Period)
                            .ToDictionary(
                                c => c.Period,
                                c =>
                                new ServiceBalances
                                {
                                    Balances = c.Balances
                                        .ToDictionary(
                                            b => b.ServiceTypeID,
                                            b =>
                                            new Balance
                                            {
                                                Benefit = b.Benefit,
                                                Charge = b.Charge,
                                                Payment = b.Payment,
                                                Total = b.Benefit + b.Charge + b.Payment
                                            })
                                });

                    Dictionary<int, string> _serviceTypeNames =
                        _entities.ServiceTypes
                            .Select(s =>
                                new
                                {
                                    s.ID,
                                    s.Name
                                })
                            .ToDictionary(s => s.ID, s => s.Name);

                    #endregion

                    DateTime _previousPeriod = DateTime.MinValue;
                    ServiceBalances _billBalances = new ServiceBalances();
                    IList<TotalBillDocs> _bills = new List<TotalBillDocs>();
                    TotalBillDocs _currentBill = null;

                    for (int i = 0; i < _customerPeriodBalances.Count(); i++)
                    {
                        KeyValuePair<DateTime, ServiceBalances> _periodBalance = _customerPeriodBalances.ElementAt(i);

                        // Заполенение таблицы отчетов
                        if (_previousPeriod == DateTime.MinValue || _periodBalance.Key != _previousPeriod.AddMonths(1))
                        {
                            _currentBill = 
                                new TotalBillDocs()
                                {
                                    BillSets = _billSet,
                                    StartPeriod = _previousPeriod != DateTime.MinValue
                                        ? _previousPeriod
                                        : (DateTime?)null,
                                    CreationDateTime = _now,
                                    Account = _customer.Account,
                                    Address = $"ул. {_customer.Buildings.Streets.Name}, {_customer.Buildings.Number}, кв. {_customer.Apartment}",
                                    Owner = _customer.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson 
                                        ? _customer.JuridicalPersonFullName 
                                        : _customer.PhysicalPersonShortName,
                                    Customers = _customer,
                                    ResidentsCount = _customer.Residents.Count(),
                                    Square = $"{_customer.Square} кв.м.",
                                };

                            _entities.AddToTotalBillDocs(_currentBill);
                            _bills.Add(_currentBill);
                        }

                        foreach (KeyValuePair<int, Balance> _serviceTypeBalance in _periodBalance.Value.Balances)
                        {
                            _billBalances.Add(_serviceTypeBalance.Key, _serviceTypeBalance.Value);
                        }

                        if (i + 1 == _customerPeriodBalances.Count() || _customerPeriodBalances.ElementAt(i + 1).Key != _periodBalance.Key.AddMonths(1))
                        {
                            _currentBill.Period = _periodBalance.Key;

                            foreach (KeyValuePair<int, Balance> _serviceTypeBalance in _billBalances.Balances)
                            {
                                TotalBillDocPoses _pos = new TotalBillDocPoses()
                                {
                                    TotalBillDocs = _currentBill,
                                    ServiceTypeName = _serviceTypeNames[_serviceTypeBalance.Key],
                                    TotalCharged = _serviceTypeBalance.Value.Charge,
                                    TotalPaid = -1 * (_serviceTypeBalance.Value.Payment + _serviceTypeBalance.Value.Benefit),
                                    Value = _serviceTypeBalance.Value.Total,
                                };

                                _entities.AddToTotalBillDocPoses(_pos);
                                _currentBill.Value += _pos.Value;
                            }

                            _billBalances.Balances.Clear();
                            _billSet.ValueSum += _currentBill.Value;
                            _billSet.Quantity++;
                        }

                        _previousPeriod = _periodBalance.Key;
                    }

                    _entities.SaveChanges();
                    _totalValue = _billSet.ValueSum;
                }
            }
            catch (Exception _ex)
            {
                _errorsCount++;
                Logger.SimpleWrite(string.Format("Ошибка при создании квитанции по доплате.\r\n{0}", _ex));
            }
            finally
            {
                Application.DoEvents();
                View.AddProgress(1);
                Application.DoEvents();
            }

            View.ResultCount = 1;
            View.ResultErrorCount = _errorsCount;
            View.ResultValue = _totalValue;

            View.IsMasterInProgress = false;
        }
    }
}