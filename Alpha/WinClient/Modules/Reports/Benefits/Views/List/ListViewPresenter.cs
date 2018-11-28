using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Benefits.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private class PeriodValue
        {
            public decimal Charges { get; set; }
            public decimal Recharges { get; set; }
            public decimal Benefits { get; set; }
        }

        #region Overrides of BasePresenter<IListView>

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            View.Period = ServerTime.GetPeriodInfo().LastCharged;
        }

        #endregion

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("Date", typeof(DateTime));
            _table.Columns.Add("Number", typeof(int));
            _table.Columns.Add("Surname", typeof(string));
            _table.Columns.Add("FirstName", typeof(string));
            _table.Columns.Add("Patronymic", typeof(string));
            _table.Columns.Add("Street", typeof(string));
            _table.Columns.Add("BuildingNumber", typeof(string));
            _table.Columns.Add("Apartment", typeof(string));
            _table.Columns.Add("Floor", typeof(string));
            _table.Columns.Add("LiftPresence", typeof(string));
            _table.Columns.Add("RubbishChutePresence", typeof(string));
            _table.Columns.Add("Square", typeof(decimal));
            _table.Columns.Add("Category", typeof(string));
            _table.Columns.Add("BenefitType", typeof(string));
            _table.Columns.Add("ResidentDocument", typeof(string));
            _table.Columns.Add("DocumentNumber", typeof(string));
            _table.Columns.Add("DocumentIssueDate", typeof(string));
            _table.Columns.Add("DocumentValidityPeriod", typeof(string));
            _table.Columns.Add("SocialNorms", typeof(decimal));
            _table.Columns.Add("ActualIntake", typeof(decimal));
            _table.Columns.Add("Rate", typeof(decimal));
            _table.Columns.Add("Charges", typeof(string));
            _table.Columns.Add("Acts", typeof(decimal));
            _table.Columns.Add("Recalculations", typeof(decimal));
            _table.Columns.Add("Payable", typeof(decimal));
            _table.Columns.Add("BenefitSum", typeof(decimal));

            DateTime _period = View.Period;
            DateTime _dateTime = ServerTime.GetDateTimeInfo().Now;
            _dateTime = new DateTime(_dateTime.Year, _dateTime.Month, 1);

            using (var _entities = new Entities())
            {
                Dictionary<int, PeriodValue> _valueData =
                    _entities.ChargeOpers
                        .Where(c => c.ChargeSets.Period == _period)
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.Customers.ID,
                                Charges = c.Value,
                                Recharges = (decimal)0,
                                Benefits = (decimal)0
                            })
                        .Concat(
                            _entities.ChargeOpers
                                .Where(c => c.ChargeCorrectionOpers != null && c.ChargeCorrectionOpers.Period == _period)
                                .Select(
                                    c =>
                                    new
                                    {
                                        CustomerID = c.Customers.ID,
                                        Charges = (decimal)0,
                                        Recharges = -1 * c.Value,
                                        Benefits = (decimal)0
                                    }))
                        .Concat(
                            _entities.RechargeOpers
                                .Where(r => r.RechargeSets.Period == _period)
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.Customers.ID,
                                        Charges = (decimal)0,
                                        Recharges = r.Value,
                                        Benefits = (decimal)0
                                    }))
                        .Concat(
                            _entities.RechargeOpers
                                .Where(r => r.ChildChargeCorrectionOpers != null && r.ChildChargeCorrectionOpers.Period == _period)
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.Customers.ID,
                                        Charges = (decimal)0,
                                        Recharges = -1 * r.Value,
                                        Benefits = (decimal)0
                                    }))
                        .Concat(
                            _entities.BenefitOpers
                                .Where(b => b.ChargeOpers.ChargeSets.Period == _period)
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.ChargeOpers.Customers.ID,
                                        Charges = (decimal)0,
                                        Recharges = (decimal)0,
                                        Benefits = b.Value
                                    }))
                        .Concat(
                            _entities.BenefitOpers
                                .Where(b => b.BenefitCorrectionOpers != null && b.BenefitCorrectionOpers.ChargeCorrectionOpers.Period == _period)
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.ChargeOpers.Customers.ID,
                                        Charges = (decimal)0,
                                        Recharges = (decimal)0,
                                        Benefits = -1 * b.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Where(r => r.RechargeOpers.RechargeSets.Period == _period)
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.RechargeOpers.Customers.ID,
                                        Charges = (decimal)0,
                                        Recharges = (decimal)0,
                                        Benefits = r.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Where(r => r.BenefitCorrectionOpers != null && r.BenefitCorrectionOpers.ChargeCorrectionOpers.Period == _period)
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.RechargeOpers.Customers.ID,
                                        Charges = (decimal)0,
                                        Recharges = (decimal)0,
                                        Benefits = -1 * r.Value
                                    }))
                        .GroupBy(c => c.CustomerID)
                        .Select(
                            g =>
                            new
                            {
                                CustomerID = g.Key,
                                Charges = g.Sum(c => c.Charges),
                                Recharges = g.Sum(c => c.Recharges),
                                Benefits = -1 * g.Sum(c => c.Benefits)
                            })
                        .ToDictionary(
                            c => c.CustomerID,
                            c =>
                            new PeriodValue
                            {
                                Benefits = c.Benefits,
                                Recharges = c.Recharges,
                                Charges = c.Charges
                            });

                var _query =
                    _entities.Residents
                        .Include("Customers")
                        .Include("Customers.Buildings")
                        .Include("Customers.Buildings.Streets")
                        .Where(r => r.BenefitTypes != null);

                if (View.ShowOnlyFederalBenefits)
                {
                    _query = _query.Where(r => r.BenefitTypes.BenefitRule == (int)BenefitRuleType.FiftyPercentBySquare);
                }

                var _residents =
                        _query
                        .Select(
                            r =>
                            new
                            {
                                r.Surname,
                                r.FirstName,
                                r.Patronymic,
                                Street = r.Customers.Buildings.Streets.Name,
                                BuildingNumber = r.Customers.Buildings.Number,
                                r.Customers.Apartment,
                                r.Customers.Floor,
                                r.Customers.LiftPresence,
                                r.Customers.RubbishChutePresence,
                                r.Customers.Square,
                                ResidentsCount = r.Customers.Residents.Count,
                                FederalBenefitResidentsCount = r.Customers.Residents.Count(res => res.BenefitTypes != null && r.BenefitTypes.BenefitRule == 0),
                                r.BenefitTypes.BenefitRule,
                                BenefitType = r.BenefitTypes.Name,
                                r.ResidentDocument,
                                Rate = (decimal?)r.Customers.CustomerPoses.Sum(c => c.Services.ChargeRule == (int)ChargeRuleType.SquareRate ? c.Rate : 0),
                                CustomerID = r.Customers.ID
                            })
                        .OrderBy(
                            r =>
                            new
                            {
                                r.Surname,
                                r.FirstName,
                                r.Patronymic
                            })
                        .ToList();

                int _k = 1;

                foreach (var _resident in _residents)
                {
                    PeriodValue _periodValue =
                        _valueData.ContainsKey(_resident.CustomerID)
                            ? _valueData[_resident.CustomerID]
                            : new PeriodValue();

                    decimal _coefficient = SocialNorm(_resident.ResidentsCount);
                    decimal _benefitNormalSquare = _coefficient * _resident.FederalBenefitResidentsCount;

                    _table.Rows.Add(
                        _dateTime,
                        _k++,
                        _resident.Surname,
                        _resident.FirstName,
                        _resident.Patronymic,
                        _resident.Street,
                        _resident.BuildingNumber,
                        _resident.Apartment,
                        _resident.Floor,
                        _resident.LiftPresence ? "Да" : "Нет",
                        _resident.RubbishChutePresence ? "Да" : "Нет",
                        _resident.Square,
                        _resident.BenefitRule == (byte)BenefitRuleType.FiftyPercentBySquare
                            ? "Местные льготы"
                            : "Скидки",
                        _resident.BenefitType,
                        _resident.ResidentDocument,
                        string.Empty,
                        string.Empty,
                        "бессрочно",
                        _coefficient,
                        _resident.Square < _benefitNormalSquare ? _resident.Square : _benefitNormalSquare,
                        _resident.Rate.HasValue ? _resident.Rate.Value : 0,
                        _periodValue.Charges,
                        0,
                        _periodValue.Recharges,
                        _periodValue.Charges + _periodValue.Recharges - _periodValue.Benefits,
                        _periodValue.Benefits);
                }
            }

            return _table;
        }

        private decimal SocialNorm(int residentsCount)
        {
            switch (residentsCount)
            {
                case 0:
                    return 0;
                case 1:
                    return 33;
                case 2:
                    return 21;
                default:
                    return 18;
            }
        }
    }
}