using DevExpress.Data.Mask;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PaymentsAndCharges.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, ListViewPresenter.PaymentsAndChargesReportParams>
    {
        /// <summary>
        /// Параметры отчета
        /// </summary>
        public struct PaymentsAndChargesReportParams
        {
            public DateTime Since;
            public DateTime TillPeriod;
            public DateTime TillDateTime;
            public string StreetId;
            public string BuildingId;
            public string ServiceTypeId;
            public string ServiceId;
            public bool SplitByServices;
            public bool GroupByCustomer;
        }

        private class Result
        {
            public int GroupObjectID { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public string ServiceName { get; set; }
            public string CustomerFullName { get; set; }
            public string CustomerApartment { get; set; }
            public decimal Charge { get; set; }
            public decimal Recharge { get; set; }
            public decimal Benefit { get; set; }
            public decimal Payment { get; set; }
        }

        /// <summary>
        /// Итоговые значения начислений и платежей по услуге
        /// </summary>
        private class TotalValue
        {
            public decimal Charge { get; private set; }
            public decimal Payment { get; private set; }
            public decimal Benefit { get; private set; }
            public decimal Recharge { get; private set; }
            public decimal Payable { get; private set; }
            public decimal OverpaymentDebt { get; private set; }

            public void Add(
                decimal charge, 
                decimal recharge, 
                decimal payment, 
                decimal benefit, 
                decimal payable, 
                decimal overpaymentDebt)
            {
                Charge += charge;
                Recharge += recharge;
                Payment += payment;
                Benefit += benefit;
                Payable += payable;
                OverpaymentDebt += overpaymentDebt;
            }
        }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            RefreshRefBooks();

            PeriodInfo _periodInfo = ServerTime.GetPeriodInfo();

            View.SincePeriod = _periodInfo.LastCharged;
            View.TillPeriod = _periodInfo.LastCharged;
        }

        /// <summary>
        /// Подписка на глобальную команду "Обновить справочники".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.RefreshRefBooksFired, ThreadOption.UserInterface)]
        public void OnRefreshRefBooksFired(object sender, EventArgs eventArgs)
        {
            if (WorkItem.Status == WorkItemStatus.Active)
            {
                RefreshRefBooks();
            }
        }

        /// <summary>
        /// Обновляет справочники
        /// </summary>
        private void RefreshRefBooks()
        {
            View.Services = GetList<Service>();
            View.ServiceTypes = GetList<ServiceType>();
            View.Streets = GetList<Street>();
        }

        /// <summary>
        /// Заполняет список домов
        /// </summary>
        internal void FillBuildingList()
        {
            View.Buildings = DataMapper<Building, IBuildingDataMapper>().GetBuildingsOnStreet(GetItem<Street>(View.StreetId));
        }

        /// <summary>
        /// Возвращает параметры отчета
        /// </summary>
        protected override PaymentsAndChargesReportParams GetReportParams()
        {
            DateTime _tillPeriod = View.TillPeriod;
            DateTime _tillDateTime = new DateTime(_tillPeriod.Year, _tillPeriod.Month, DateTime.DaysInMonth(_tillPeriod.Year, _tillPeriod.Month), 23, 59, 59);

            return new PaymentsAndChargesReportParams
            {
                Since = View.SincePeriod,
                TillPeriod = _tillPeriod,
                TillDateTime = _tillDateTime,
                StreetId = View.StreetId,
                BuildingId = View.BuildingId,
                ServiceTypeId = View.ServiceTypeId,
                ServiceId = View.ServiceId,
                SplitByServices = View.SplitByServices,
                GroupByCustomer = View.GroupByCustomer
            };
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(PaymentsAndChargesReportParams _params)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("Street", typeof(string));
            _table.Columns.Add("Building", typeof(string));
            _table.Columns.Add("Service", typeof(string));
            _table.Columns.Add("Customer", typeof(string));
            _table.Columns.Add("Apartment", typeof(string));
            _table.Columns.Add("Charges", typeof(decimal));
            _table.Columns.Add("Acts", typeof(decimal));
            _table.Columns.Add("Recharges", typeof(decimal));
            _table.Columns.Add("Benefits", typeof(decimal));
            _table.Columns.Add("Payable", typeof(decimal));
            _table.Columns.Add("Paid", typeof(decimal));
            _table.Columns.Add("OverpaymentDebt", typeof(decimal));

            using (var _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                #region Запрос ч.1

                var _rows =
                    _entities.ChargeOperPoses
                        .Select(
                            p =>
                            new
                            {
                                CreationDateTime = p.ChargeOpers.ChargeSets.Period,
                                CustomerID = p.ChargeOpers.Customers.ID,
                                StreetID = p.ChargeOpers.Customers.Buildings.Streets.ID,
                                BuildingID = p.ChargeOpers.Customers.Buildings.ID,
                                ServiceTypeID = p.Services.ServiceTypes.ID,
                                ServiceID = p.Services.ID,
                                ChargeValue = p.Value,
                                RechargeValue = (decimal)0,
                                BenefitValue = (decimal)0,
                                PaymentValue = (decimal)0
                            })
                        .Concat(
                            _entities.RechargeOperPoses
                                .Select(
                                    p =>
                                    new
                                    {
                                        CreationDateTime = p.RechargeOpers.RechargeSets.Period,
                                        CustomerID = p.RechargeOpers.Customers.ID,
                                        StreetID = p.RechargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = p.RechargeOpers.Customers.Buildings.ID,
                                        ServiceTypeID = p.Services.ServiceTypes.ID,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = p.Value,
                                        BenefitValue = (decimal)0,
                                        PaymentValue = (decimal)0
                                    }))
                        .Concat(
                            _entities.ChargeOperPoses
                                .Where(p => p.ChargeOpers.ChargeCorrectionOpers != null)
                                .Select(
                                    p =>
                                    new
                                    {
                                        CreationDateTime = p.ChargeOpers.ChargeCorrectionOpers.Period,
                                        CustomerID = p.ChargeOpers.Customers.ID,
                                        StreetID = p.ChargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = p.ChargeOpers.Customers.Buildings.ID,
                                        ServiceTypeID = p.Services.ServiceTypes.ID,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = -1 * p.Value,
                                        BenefitValue = (decimal)0,
                                        PaymentValue = (decimal)0
                                    }))
                        .Concat(
                            _entities.RechargeOperPoses
                                .Where(p => p.RechargeOpers.ChildChargeCorrectionOpers != null)
                                .Select(
                                    p =>
                                    new
                                    {
                                        CreationDateTime = p.RechargeOpers.ChildChargeCorrectionOpers.Period,
                                        CustomerID = p.RechargeOpers.Customers.ID,
                                        StreetID = p.RechargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = p.RechargeOpers.Customers.Buildings.ID,
                                        ServiceTypeID = p.Services.ServiceTypes.ID,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = -1 * p.Value,
                                        BenefitValue = (decimal)0,
                                        PaymentValue = (decimal)0
                                    }))
                        .Concat(
                            _entities.BenefitOperPoses
                                .Select(
                                    p =>
                                    new
                                    {
                                        CreationDateTime = p.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                        CustomerID = p.BenefitOpers.ChargeOpers.Customers.ID,
                                        StreetID = p.BenefitOpers.ChargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = p.BenefitOpers.ChargeOpers.Customers.Buildings.ID,
                                        ServiceTypeID = p.Services.ServiceTypes.ID,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = (decimal)0,
                                        BenefitValue = p.Value,
                                        PaymentValue = (decimal)0
                                    }))
                        .Concat(
                            _entities.BenefitOperPoses
                                .Where(p => p.BenefitOpers.BenefitCorrectionOpers != null)
                                .Select(
                                    p =>
                                    new
                                    {
                                        CreationDateTime = p.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        CustomerID = p.BenefitOpers.ChargeOpers.Customers.ID,
                                        StreetID = p.BenefitOpers.ChargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = p.BenefitOpers.ChargeOpers.Customers.Buildings.ID,
                                        ServiceTypeID = p.Services.ServiceTypes.ID,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = (decimal)0,
                                        BenefitValue = -1 * p.Value,
                                        PaymentValue = (decimal)0
                                    }))
                        .Concat(
                            _entities.RebenefitOperPoses
                                .Select(
                                    r =>
                                        new
                                        {
                                            CreationDateTime = r.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                            CustomerID = r.RebenefitOpers.RechargeOpers.Customers.ID,
                                            StreetID = r.RebenefitOpers.RechargeOpers.Customers.Buildings.Streets.ID,
                                            BuildingID = r.RebenefitOpers.RechargeOpers.Customers.Buildings.ID,
                                            ServiceTypeID = r.Services.ServiceTypes.ID,
                                            ServiceID = r.Services.ID,
                                            ChargeValue = (decimal)0,
                                            RechargeValue = (decimal)0,
                                            BenefitValue = r.Value,
                                            PaymentValue = (decimal)0
                                        }))
                        .Concat(
                            _entities.RebenefitOperPoses
                                .Where(r => r.RebenefitOpers.BenefitCorrectionOpers != null)
                                .Select(
                                    r =>
                                        new
                                        {
                                            CreationDateTime = r.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                            CustomerID = r.RebenefitOpers.RechargeOpers.Customers.ID,
                                            StreetID = r.RebenefitOpers.RechargeOpers.Customers.Buildings.Streets.ID,
                                            BuildingID = r.RebenefitOpers.RechargeOpers.Customers.Buildings.ID,
                                            ServiceTypeID = r.Services.ServiceTypes.ID,
                                            ServiceID = r.Services.ID,
                                            ChargeValue = (decimal)0,
                                            RechargeValue = (decimal)0,
                                            BenefitValue = -1 * r.Value,
                                            PaymentValue = (decimal)0
                                        }))
                        .Concat(
                            _entities.PaymentOperPoses
                                .Select(
                                    p =>
                                    new
                                    {
                                        p.PaymentOpers.CreationDateTime,
                                        CustomerID = p.PaymentOpers.Customers.ID,
                                        StreetID = p.PaymentOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = p.PaymentOpers.Customers.Buildings.ID,
                                        ServiceTypeID = p.Services.ServiceTypes.ID,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = (decimal)0,
                                        BenefitValue = (decimal)0,
                                        PaymentValue = p.Value
                                    }))
                        .Concat(
                            _entities.PaymentCorrectionOperPoses
                                .Select(
                                    p =>
                                    new
                                    {
                                        CreationDateTime = p.PaymentCorrectionOpers.Period,
                                        CustomerID = p.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                                        StreetID = p.PaymentCorrectionOpers.PaymentOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = p.PaymentCorrectionOpers.PaymentOpers.Customers.Buildings.ID,
                                        ServiceTypeID = p.Services.ServiceTypes.ID,
                                        ServiceID = p.Services.ID,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = (decimal)0,
                                        BenefitValue = (decimal)0,
                                        PaymentValue = p.Value
                                    }))
                        .Where(c => c.CreationDateTime >= _params.Since && c.CreationDateTime <= _params.TillDateTime)
                        .GroupBy(
                            c =>
                            new
                            {
                                c.CustomerID,
                                c.StreetID,
                                c.BuildingID,
                                c.ServiceTypeID,
                                c.ServiceID
                            })
                        .Select(
                            g =>
                            new
                            {
                                g.Key.CustomerID,
                                g.Key.StreetID,
                                g.Key.BuildingID,
                                g.Key.ServiceTypeID,
                                g.Key.ServiceID,
                                ChargeValue = g.Sum(c => c.ChargeValue),
                                RechargeValue = g.Sum(c => c.RechargeValue),
                                BenefitValue = g.Sum(c => c.BenefitValue),
                                PaymentValue = g.Sum(c => c.PaymentValue)
                            })
                        .ToList();

                #endregion

                #region Фильтры

                if (!string.IsNullOrEmpty(_params.StreetId))
                {
                    int _param = int.Parse(_params.StreetId);
                    _rows = _rows.Where(r => r.StreetID == _param).ToList();
                }

                if (!string.IsNullOrEmpty(_params.BuildingId))
                {
                    int _param = int.Parse(_params.BuildingId);
                    _rows = _rows.Where(r => r.BuildingID == _param).ToList();
                }

                if (!string.IsNullOrEmpty(_params.ServiceTypeId))
                {
                    int _param = int.Parse(_params.ServiceTypeId);
                    _rows = _rows.Where(r => r.ServiceTypeID == _param).ToList();
                }

                if (!string.IsNullOrEmpty(_params.ServiceId))
                {
                    int _param = int.Parse(_params.ServiceId);
                    _rows = _rows.Where(r => r.ServiceID == _param).ToList();
                }

                #endregion

                #region Запрос ч.2

                var _rawResultData =
                    (_params.GroupByCustomer
                        ? _rows.GroupBy(
                            r =>
                            new
                            {
                                GroupObjectID = r.CustomerID,
                                r.BuildingID,
                            })
                        : _rows.GroupBy(
                            r =>
                            new
                            {
                                GroupObjectID = _params.SplitByServices ? r.ServiceID : r.ServiceTypeID,
                                r.BuildingID,
                            }))
                        .Select(g =>
                            new
                            {
                                g.Key.GroupObjectID,
                                g.Key.BuildingID,
                                Charge = g.Sum(r => r.ChargeValue),
                                Recharge = g.Sum(r => r.RechargeValue),
                                Benefit = -1 * g.Sum(r => r.BenefitValue),
                                Payment = -1 * g.Sum(r => r.PaymentValue)
                            })
                        .ToList();

                #endregion

                List<Result> _resultData;

                #region Вспомогательные данные

                var _buildingData =
                    _entities.Buildings
                        .Include("Streets")
                        .Select(
                            b =>
                            new
                            {
                                b.ID,
                                b.Number,
                                b.Streets.Name
                            })
                        .ToDictionary(
                            b => b.ID,
                            b =>
                            new
                            {
                                b.Number,
                                StreetName = b.Name
                            });

                
                Dictionary<int, string> _serviceNames = null;

                if (_params.GroupByCustomer)
                {
                    var _customerNames =
                        _entities.Customers
                            .Select(
                                c =>
                                new
                                {
                                    c.ID,
                                    FullName =
                                        c.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson
                                            ? c.PhysicalPersonFullName
                                            : c.JuridicalPersonFullName,
                                    c.Apartment
                                })
                            .ToDictionary(
                                c => c.ID,
                                c =>
                                new
                                {
                                    c.FullName,
                                    c.Apartment
                                });

                    _resultData =
                        _rawResultData
                            .Select(
                                r =>
                                new Result
                                {
                                    GroupObjectID = r.GroupObjectID,
                                    Street = _buildingData[r.BuildingID].StreetName,
                                    Building = _buildingData[r.BuildingID].Number,
                                    ServiceName = string.Empty,
                                    CustomerFullName = _customerNames[r.GroupObjectID].FullName,
                                    CustomerApartment = _customerNames[r.GroupObjectID].Apartment,
                                    Charge = r.Charge,
                                    Recharge = r.Recharge,
                                    Benefit = r.Benefit,
                                    Payment = r.Payment,
                                })
                            .OrderBy(r => r.Street)
                            .ThenBy(r => r.Building, new StringWithNumbersComparer())
                            .ThenBy(r => r.CustomerApartment, new StringWithNumbersComparer())
                            .ThenBy(r => r.CustomerFullName)
                            .ToList();
                }
                else
                {
                    _serviceNames =
                        _params.SplitByServices
                            ? _entities.Services
                                .Select(
                                    s =>
                                    new
                                    {
                                        s.ID,
                                        s.Name
                                    })
                                .ToDictionary(s => s.ID, s => s.Name)
                            : _entities.ServiceTypes
                                .Select(
                                    s =>
                                    new
                                    {
                                        s.ID,
                                        s.Name
                                    })
                                .ToDictionary(s => s.ID, s => s.Name);

                    _resultData =
                        _rawResultData
                            .Select(
                                r =>
                                new Result
                                {
                                    GroupObjectID = r.GroupObjectID,
                                    Street = _buildingData[r.BuildingID].StreetName,
                                    Building = _buildingData[r.BuildingID].Number,
                                    ServiceName = _serviceNames[r.GroupObjectID],
                                    CustomerFullName = string.Empty,
                                    CustomerApartment = string.Empty,
                                    Charge = r.Charge,
                                    Recharge = r.Recharge,
                                    Benefit = r.Benefit,
                                    Payment = r.Payment,
                                })
                            .OrderBy(r => r.Street)
                            .ThenBy(r => r.Building, new StringWithNumbersComparer())
                            .ThenBy(r => r.ServiceName)
                            .ToList();
                }

                #endregion

                Dictionary<int, TotalValue> _serviceTotal = new Dictionary<int, TotalValue>();
                TotalValue _customerTotal = new TotalValue();

                foreach (var _result in _resultData)
                {
                    decimal _payable = _result.Charge + _result.Recharge - _result.Benefit;
                    decimal _overpaymentDebt = _result.Charge + _result.Recharge - _result.Benefit - _result.Payment;

                    _table.Rows.Add(
                        _result.Street,
                        _result.Building,
                        _result.ServiceName,
                        _result.CustomerFullName,
                        _result.CustomerApartment,
                        _result.Charge,
                        0,
                        _result.Recharge,
                        _result.Benefit,
                        _payable,
                        _result.Payment,
                        _overpaymentDebt);

                    if (_params.GroupByCustomer)
                    {
                        _customerTotal.Add(_result.Charge, _result.Recharge, _result.Payment, _result.Benefit, _payable, _overpaymentDebt);
                    }
                    else
                    {
                        TotalValue _currentTotalValue;

                        if (!_serviceTotal.ContainsKey(_result.GroupObjectID))
                        {
                            _currentTotalValue = new TotalValue();
                            _serviceTotal.Add(_result.GroupObjectID, _currentTotalValue);
                        }
                        else
                        {
                            _currentTotalValue = _serviceTotal[_result.GroupObjectID];
                        }

                        _currentTotalValue.Add(_result.Charge, _result.Recharge, _result.Payment, _result.Benefit, _payable, _overpaymentDebt);
                    }
                }

                if (_params.GroupByCustomer)
                {
                    _table.Rows.Add(
                        "= Итого",
                        "= Итого",
                        string.Empty,
                        "= Итого",
                        string.Empty,
                        _customerTotal.Charge,
                        0,
                        _customerTotal.Recharge,
                        _customerTotal.Benefit,
                        _customerTotal.Payable,
                        _customerTotal.Payment,
                        _customerTotal.OverpaymentDebt);
                }
                else
                {
                    foreach (var _totalValue in _serviceTotal)
                    {
                        _table.Rows.Add(
                            "= Итого",
                            "= Итого",
                            _serviceNames[_totalValue.Key],
                            string.Empty,
                            string.Empty,
                            _totalValue.Value.Charge,
                            0,
                            _totalValue.Value.Recharge,
                            _totalValue.Value.Benefit,
                            _totalValue.Value.Payable,
                            _totalValue.Value.Payment,
                            _totalValue.Value.OverpaymentDebt);
                    }
                }
            }

            return _table;
        }

        #region Overrides of BaseReportForGridPresenter<IListView,PaymentsAndChargesReportParams>

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            View.SetGridColumns();
            base.ProcessGridData();
        }

        #endregion
    }
}