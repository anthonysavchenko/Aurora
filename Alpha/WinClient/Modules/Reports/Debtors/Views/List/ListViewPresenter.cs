using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Debtors.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private static class ColumnNames
        {
            public const string KEY_COLUMN = "Key";
            public const string STREET_COLUMN = "Street";
            public const string HOUSE_COLUMN = "House";
            public const string APARTMENT_COLUMN = "Apartment";
            public const string ACCOUNT_COLUMN = "Account";
            public const string OWNER_AKA_COLUMN = "OwnerAka";
            public const string TOTAL = "Total";
            public const string CURRENT_MONTH_CHARGE = "CurrentMonthCharge";
        }

        private class StringAsNumbersComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                int _x;
                int _y;

                int.TryParse(x, out _x);
                int.TryParse(y, out _y);

                return _x - _y;
            }
        }

        private readonly Dictionary<string, string> _serviceTypesDictionary = new Dictionary<string, string>();

        public override void OnViewReady()
        {
            base.OnViewReady();

            View.Streets = GetList<Street>();
            View.DebtMinSum = 5000;
            View.TillDateTime = ServerTime.GetPeriodInfo().LastCharged.AddMonths(-1);
        }

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            View.ClearColumns();

            View.AddColumn(ColumnNames.STREET_COLUMN, "Улица");
            View.AddColumn(ColumnNames.HOUSE_COLUMN, "Дом");
            View.AddColumn(ColumnNames.APARTMENT_COLUMN, "Кв.");
            View.AddColumn(ColumnNames.ACCOUNT_COLUMN, "Номер счета");
            View.AddColumn(ColumnNames.OWNER_AKA_COLUMN, "Собственник");
            View.AddMoneyColumn(ColumnNames.TOTAL, "Итого");
            base.ProcessGridData();
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add(ColumnNames.STREET_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.HOUSE_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.APARTMENT_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.ACCOUNT_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.OWNER_AKA_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.TOTAL, typeof(decimal));

            decimal _debtMinSum = View.DebtMinSum;

            using (var _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _raw =
                    _db.ChargeOpers
                        .Select(x =>
                            new
                            {
                                CustomerID = x.Customers.ID,
                                StreetID = x.Customers.Buildings.Streets.ID,
                                BuildingID = x.Customers.Buildings.ID,
                                x.ChargeSets.Period,
                                x.Value
                            })
                        .Concat(_db.ChargeOpers
                            .Where(x => x.ChargeCorrectionOpers != null)
                            .Select(x =>
                                new
                                {
                                    CustomerID = x.Customers.ID,
                                    StreetID = x.Customers.Buildings.Streets.ID,
                                    BuildingID = x.Customers.Buildings.ID,
                                    x.ChargeCorrectionOpers.Period,
                                    Value = -1 * x.Value
                                }))
                        .Concat(
                            _db.RechargeOpers
                                .Select(x =>
                                    new
                                    {
                                        CustomerID = x.Customers.ID,
                                        StreetID = x.Customers.Buildings.Streets.ID,
                                        BuildingID = x.Customers.Buildings.ID,
                                        x.RechargeSets.Period,
                                        x.Value
                                    }))
                        .Concat(
                            _db.RechargeOpers
                                .Where(x => x.ChildChargeCorrectionOpers != null)
                                .Select(x =>
                                    new
                                    {
                                        CustomerID = x.Customers.ID,
                                        StreetID = x.Customers.Buildings.Streets.ID,
                                        BuildingID = x.Customers.Buildings.ID,
                                        x.ChildChargeCorrectionOpers.Period,
                                        Value = -1 * x.Value
                                    }))
                        .Concat(
                            _db.BenefitOpers
                                .Select(x =>
                                    new
                                    {
                                        CustomerID = x.ChargeOpers.Customers.ID,
                                        StreetID = x.ChargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = x.ChargeOpers.Customers.Buildings.ID,
                                        x.ChargeOpers.ChargeSets.Period,
                                        x.Value
                                    }))
                        .Concat(
                            _db.BenefitOpers
                                .Where(x => x.BenefitCorrectionOpers != null)
                                .Select(x =>
                                    new
                                    {
                                        CustomerID = x.ChargeOpers.Customers.ID,
                                        StreetID = x.ChargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = x.ChargeOpers.Customers.Buildings.ID,
                                        x.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        Value = -1 * x.Value
                                    }))
                        .Concat(
                            _db.RebenefitOpers
                                .Select(x =>
                                    new
                                    {
                                        CustomerID = x.RechargeOpers.Customers.ID,
                                        StreetID = x.RechargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = x.RechargeOpers.Customers.Buildings.ID,
                                        x.RechargeOpers.RechargeSets.Period,
                                        x.Value
                                    }))
                        .Concat(
                            _db.RebenefitOpers
                                .Where(x => x.BenefitCorrectionOpers != null)
                                .Select(x =>
                                    new
                                    {
                                        CustomerID = x.RechargeOpers.Customers.ID,
                                        StreetID = x.RechargeOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = x.RechargeOpers.Customers.Buildings.ID,
                                        x.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                        Value = -1 * x.Value
                                    }))
                        .Concat(
                            _db.PaymentOpers
                                .Select(x =>
                                    new
                                    {
                                        CustomerID = x.Customers.ID,
                                        StreetID = x.Customers.Buildings.Streets.ID,
                                        BuildingID = x.Customers.Buildings.ID,
                                        Period = x.CreationDateTime,
                                        x.Value
                                    }))
                        .Concat(
                            _db.PaymentCorrectionOpers
                                .Select(x =>
                                    new
                                    {
                                        CustomerID = x.PaymentOpers.Customers.ID,
                                        StreetID = x.PaymentOpers.Customers.Buildings.Streets.ID,
                                        BuildingID = x.PaymentOpers.Customers.Buildings.ID,
                                        x.Period,
                                        x.Value
                                    }));

                if (!string.IsNullOrEmpty(View.BuildingId))
                {
                    int _id = int.Parse(View.BuildingId);
                    _raw = _raw.Where(x => x.BuildingID == _id);
                }
                else if(!string.IsNullOrEmpty(View.StreetId))
                {
                    int _id = int.Parse(View.StreetId);
                    _raw = _raw.Where(x => x.StreetID == _id);
                }

                if(View.TillDateTime > DateTime.MinValue)
                {
                    DateTime _till = View.TillDateTime;
                    _till = new DateTime(_till.Year, _till.Month, DateTime.DaysInMonth(_till.Year, _till.Month), 23, 59, 59);
                    _raw = _raw.Where(x => x.Period <= _till);
                }

                var _raw2 = _raw
                    .GroupBy(c => c.CustomerID)
                    .Select(g =>
                        new
                        {
                            CustomerID = g.Key,
                            Value = g.Sum(c => (decimal?)c.Value) ?? 0
                        })
                    .Where(c => c.Value > _debtMinSum)
                    .ToList();

                if(View.DebtMonthCount > 0)
                {
                    DateTime _lastChargedPeriod = ServerTime.GetPeriodInfo().LastCharged;

                    _raw2 = _raw2
                        .Join(_db.ChargeOpers
                            .Where(c => c.ChargeSets.Period == _lastChargedPeriod)
                            .Select(c =>
                                new
                                {
                                    CustomerID = c.Customers.ID,
                                    c.Value
                                }),
                                x => x.CustomerID,
                                c => c.CustomerID,
                                (x, c) =>
                                    new
                                    {
                                        x.CustomerID,
                                        DebtValue = x.Value,
                                        ChargeValue = c.Value
                                    })
                        .Where(x => (
                            x.ChargeValue > 0 && Math.Round(x.DebtValue / x.ChargeValue, 0, MidpointRounding.AwayFromZero) >= View.DebtMonthCount) 
                            || x.DebtValue > 0)
                        .Select(x =>
                            new
                            {
                                x.CustomerID,
                                Value = x.DebtValue
                            })
                        .ToList();
                }

                int[] _customerIDs = _raw2.Select(x => x.CustomerID).ToArray();
                StringAsNumbersComparer _comparer = new StringAsNumbersComparer();

                var _result = _raw2
                    .Join(_db.Customers
                        .Where(c => _customerIDs.Contains(c.ID))
                        .Select(c =>
                            new
                            {
                                c.ID,
                                c.PhysicalPersonFullName,
                                c.JuridicalPersonFullName,
                                c.OwnerType,
                                StreetName = c.Buildings.Streets.Name,
                                BuildingNumber = c.Buildings.Number,
                                c.Apartment,
                                c.Account
                            }),
                            x => x.CustomerID,
                            y => y.ID,
                            (x, y) =>
                                new
                                {
                                    FullName = y.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson
                                        ? y.PhysicalPersonFullName
                                        : y.JuridicalPersonFullName,
                                    y.StreetName,
                                    y.BuildingNumber,
                                    y.Apartment,
                                    y.Account,
                                    x.Value
                                })
                        .OrderBy(x => x.StreetName)
                        .ThenBy(x => x.BuildingNumber, _comparer)
                        .ThenBy(x => x.Apartment, _comparer)
                        .ToList();
                
                foreach (var _customer in _result)
                {
                    DataRow _row = _table.NewRow();
                    _row[ColumnNames.STREET_COLUMN] = _customer.StreetName;
                    _row[ColumnNames.HOUSE_COLUMN] = _customer.BuildingNumber;
                    _row[ColumnNames.APARTMENT_COLUMN] = _customer.Apartment;
                    _row[ColumnNames.ACCOUNT_COLUMN] = _customer.Account;
                    _row[ColumnNames.OWNER_AKA_COLUMN] = _customer.FullName;
                    _row[ColumnNames.TOTAL] = _customer.Value;

                    _table.Rows.Add(_row);
                }
            }

            return _table;
        }

        /// <summary>
        /// Заполняет список домов
        /// </summary>
        public void FillBuildingList()
        {
            View.Buildings = DataMapper<Building, IBuildingDataMapper>().GetBuildingsOnStreet(GetItem<Street>(View.StreetId));
        }
    }
}