using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Charges.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, ListViewPresenter.PaymentsAndChargesReportParams>
    {
        private int _apartmentTotalCount;
        private int _apartmentMunicipalCount;
        private int _apartmentPrivatizedCount;
        private decimal _square;
        private int _buildingCount;

        /// <summary>
        /// Параметры отчета
        /// </summary>
        public struct PaymentsAndChargesReportParams
        {
            public DateTime Period;
        }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            View.Period = ServerTime.GetPeriodInfo().LastCharged;
        }

        /// <summary>
        /// Возвращает параметры отчета
        /// </summary>
        protected override PaymentsAndChargesReportParams GetReportParams()
        {
            return new PaymentsAndChargesReportParams
            {
                Period = View.Period,
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
            _table.Columns.Add("Number", typeof(int));
            _table.Columns.Add("Service", typeof(string));
            _table.Columns.Add("AccountCount", typeof(int));
            _table.Columns.Add("Square", typeof(decimal));
            _table.Columns.Add("ChargeSum", typeof(decimal));
            _table.Columns.Add("Acts", typeof(decimal));
            _table.Columns.Add("RechargeSum", typeof(decimal));
            _table.Columns.Add("BenefitSum", typeof(decimal));
            _table.Columns.Add("TotalSum", typeof(decimal));
            _table.Columns.Add("Contractor", typeof(string));

            using (var _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                #region Запросы

                var _contractorData =
                    _entities.ChargeOperPoses
                        .Where(c => c.ChargeOpers.ChargeSets.Period == _params.Period)
                        .Select(
                            c =>
                            new
                            {
                                ServiceID = c.Services.ID,
                                ContractorID = c.Contractors.ID,
                                CustomerID = c.ChargeOpers.Customers.ID,
                                c.ChargeOpers.Customers.Square,
                                ChargeValue = c.Value,
                                RechargeValue = (decimal)0,
                                BenefitValue = (decimal)0
                            })
                        .Concat(
                            _entities.ChargeOperPoses
                                .Where(c => c.ChargeOpers.ChargeCorrectionOpers != null && c.ChargeOpers.ChargeCorrectionOpers.Period == _params.Period)
                                .Select(
                                    c =>
                                    new
                                    {
                                        ServiceID = c.Services.ID,
                                        ContractorID = c.Contractors.ID,
                                        CustomerID = c.ChargeOpers.Customers.ID,
                                        c.ChargeOpers.Customers.Square,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = -1 * c.Value,
                                        BenefitValue = (decimal)0
                                    }))
                        .Concat(
                            _entities.RechargeOperPoses
                                .Where(r => r.RechargeOpers.RechargeSets.Period == _params.Period)
                                .Select(
                                    r =>
                                    new
                                    {
                                        ServiceID = r.Services.ID,
                                        ContractorID = r.Contractors.ID,
                                        CustomerID = r.RechargeOpers.Customers.ID,
                                        r.RechargeOpers.Customers.Square,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = r.Value,
                                        BenefitValue = (decimal)0
                                    }))
                        .Concat(
                            _entities.RechargeOperPoses
                                .Where(r => r.RechargeOpers.ChildChargeCorrectionOpers != null && r.RechargeOpers.ChildChargeCorrectionOpers.Period == _params.Period)
                                .Select(
                                    r =>
                                    new
                                    {
                                        ServiceID = r.Services.ID,
                                        ContractorID = r.Contractors.ID,
                                        CustomerID = r.RechargeOpers.Customers.ID,
                                        r.RechargeOpers.Customers.Square,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = -1 * r.Value,
                                        BenefitValue = (decimal)0
                                    }))
                         .Concat(
                            _entities.BenefitOperPoses
                                .Where(
                                    b => 
                                    b.BenefitOpers.ChargeOpers.ChargeSets.Period == _params.Period)
                                .Select(
                                    b =>
                                    new
                                    {
                                        ServiceID = b.Services.ID,
                                        ContractorID = b.Contractors.ID,
                                        CustomerID = b.BenefitOpers.ChargeOpers.Customers.ID,
                                        b.BenefitOpers.ChargeOpers.Customers.Square,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = (decimal)0,
                                        BenefitValue = b.Value
                                    }))
                        .Concat(
                            _entities.BenefitOperPoses
                                .Where(
                                    b =>
                                    b.BenefitOpers.BenefitCorrectionOpers != null && b.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period == _params.Period)
                                .Select(
                                    b =>
                                    new
                                    {
                                        ServiceID = b.Services.ID,
                                        ContractorID = b.Contractors.ID,
                                        CustomerID = b.BenefitOpers.ChargeOpers.Customers.ID,
                                        b.BenefitOpers.ChargeOpers.Customers.Square,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = (decimal)0,
                                        BenefitValue = -1 * b.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOperPoses
                                .Where(r => r.RebenefitOpers.RechargeOpers.RechargeSets.Period == _params.Period)
                                .Select(
                                    r =>
                                    new
                                    {
                                        ServiceID = r.Services.ID,
                                        ContractorID = r.Contractors.ID,
                                        CustomerID = r.RebenefitOpers.RechargeOpers.Customers.ID,
                                        r.RebenefitOpers.RechargeOpers.Customers.Square,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = (decimal)0,
                                        BenefitValue = r.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOperPoses
                                .Where(
                                    r => 
                                    r.RebenefitOpers.BenefitCorrectionOpers != null &&
                                    r.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period == _params.Period)
                                .Select(
                                    r =>
                                    new
                                    {
                                        ServiceID = r.Services.ID,
                                        ContractorID = r.Contractors.ID,
                                        CustomerID = r.RebenefitOpers.RechargeOpers.Customers.ID,
                                        r.RebenefitOpers.RechargeOpers.Customers.Square,
                                        ChargeValue = (decimal)0,
                                        RechargeValue = (decimal)0,
                                        BenefitValue =-1 * r.Value
                                    }))
                        .GroupBy(
                            c =>
                            new
                            {
                                c.ServiceID,
                                c.ContractorID
                            })
                        .Select(
                            g =>
                            new
                            {
                                g.Key.ServiceID,
                                g.Key.ContractorID,
                                Charge = g.Sum(c => c.ChargeValue),
                                Recharge = g.Sum(c => c.RechargeValue),
                                TotalSum = g.Sum(c => c.ChargeValue + c.RechargeValue),
                                Benefit = g.Sum(c => c.BenefitValue),
                                AccountCount = g.GroupBy(r => r.CustomerID).Count(),
                                Square = g.Select(r => new { r.CustomerID, r.Square }).Distinct().Sum(c => c.Square),
                            })
                        .ToList();

                var _commonData = _contractorData
                    .GroupBy(c => c.ServiceID)
                    .Select(g => new
                        {
                            ServiceID = g.Key,
                            Charge = g.Sum(c => c.Charge),
                            Recharge = g.Sum(c => c.Recharge),
                            TotalSum = g.Sum(c => c.TotalSum),
                            Benefit = g.Sum(c => c.Benefit),
                            AccountCount = g.Sum(c => c.AccountCount),
                            Square = g.Sum(c => c.Square),
                        })
                    .ToList();

                var _serviceNames =
                    _entities.Services
                        .Select(
                            s =>
                            new
                            {
                                s.ID,
                                s.Name
                            })
                        .ToDictionary(s => s.ID, s => s.Name);

                var _contractorNames =
                    _entities.Contractors
                        .Select(
                            c =>
                            new
                            {
                                c.ID,
                                c.Name
                            })
                        .ToDictionary(c => c.ID, c => c.Name);

                var _bottomData =
                    _entities.ChargeOpers
                        .Where(c => c.ChargeSets.Period == _params.Period)
                        .GroupBy(
                            c =>
                            new
                            {
                                CustomerID = c.Customers.ID,
                                BuildingID = c.Customers.Buildings.ID,
                            })
                        .Select(
                            g =>
                            new
                            {
                                g.Key.CustomerID,
                                g.Key.BuildingID,
                                g.FirstOrDefault().Customers.IsPrivate,
                                g.FirstOrDefault().Customers.Square,
                                Value = g.Sum(c => c.Value)
                            })
                        .Where(g => g.Value > 0)
                        .ToList();

                int _commonGroupNumber = _contractorNames.Keys.Max() + 1;

                #endregion

                #region Заполнение таблицы

                foreach (var _data in _contractorData)
                {
                    _table.Rows.Add(
                        _data.ContractorID,
                        _serviceNames[_data.ServiceID],
                        _data.AccountCount,
                        _data.Square,
                        _data.Charge,
                        0,
                        _data.Recharge,
                        -1 * _data.Benefit,
                        _data.TotalSum + _data.Benefit,
                        _contractorNames[_data.ContractorID]);
                }

                foreach (var _data in _commonData)
                {
                    _table.Rows.Add(
                        _commonGroupNumber,
                        _serviceNames[_data.ServiceID],
                        _data.AccountCount,
                        _data.Square,
                        _data.Charge,
                        0,
                        _data.Recharge,
                        -1 * _data.Benefit,
                        _data.TotalSum + _data.Benefit,
                        "Итого");
                }

                #endregion

                _apartmentTotalCount = _bottomData.Count();
                _apartmentMunicipalCount = _bottomData.Count(c => !c.IsPrivate);
                _apartmentPrivatizedCount = _bottomData.Count(c => c.IsPrivate);
                _square = _bottomData.Sum(c => c.Square);
                _buildingCount =
                    _bottomData
                        .GroupBy(c => c.BuildingID)
                        .Select(g => g.Key)
                        .Count();
            }

            return _table;
        }

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            base.ProcessGridData();

            View.ApartmentTotalCount = _apartmentTotalCount;
            View.ApartmentMunicipalCount = _apartmentMunicipalCount;
            View.ApartmentPrivatizedCount = _apartmentPrivatizedCount;
            View.Square = _square;
            View.BuildingCount = _buildingCount;
        }
    }
}