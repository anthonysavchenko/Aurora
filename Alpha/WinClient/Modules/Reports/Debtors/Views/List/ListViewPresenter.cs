using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
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

        private readonly Dictionary<string, string> _serviceTypesDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        override protected void ProcessGridData()
        {
            View.ClearColumns();

            //View.AddColumn(ColumnNames.KEY_COLUMN, "№");
            View.AddColumn(ColumnNames.STREET_COLUMN, "Улица");
            View.AddColumn(ColumnNames.HOUSE_COLUMN, "Дом");
            View.AddColumn(ColumnNames.APARTMENT_COLUMN, "Кв-ра");
            View.AddColumn(ColumnNames.ACCOUNT_COLUMN, "Номер счета");
            View.AddColumn(ColumnNames.OWNER_AKA_COLUMN, "Собственник");
            /*
            foreach (KeyValuePair<string, string> _pair in _serviceTypesDictionary)
            {
                View.AddMoneyColumn(_pair.Key, _pair.Value);
            }
            */
            View.AddMoneyColumn(ColumnNames.TOTAL, "Итого");
            /*
            View.AddMoneyColumn(
                ColumnNames.CURRENT_MONTH_CHARGE,
                string.Format("Начисления за {0:MMMM yyyy}", ServerTime.GetServerDate()));
            */
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
            //_table.Columns.Add(ColumnNames.KEY_COLUMN, typeof(int));
            _table.Columns.Add(ColumnNames.STREET_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.HOUSE_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.APARTMENT_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.ACCOUNT_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.OWNER_AKA_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.TOTAL, typeof(decimal));
            /*
            DateTime _temp = ServerTime.GetServerDateTimeTillToday();

            int _year = _temp.Year;
            int _month = _temp.Month;

            if (_month == 1)
            {
                _month = 12;
                --_year;
            }
            else
            {
                --_month;
            }

            DateTime _till = new DateTime(_year,
                                          _month,
                                          _temp.Day,
                                          _temp.Hour,
                                          _temp.Minute,
                                          _temp.Second,
                                          _temp.Millisecond);

            */
            using (var _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                var _result =
                    _entities.ChargeOpers
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.Customers.ID,
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
                                        Value = -1 * c.Value
                                    }))
                        .Concat(
                            _entities.RechargeOpers
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.Customers.ID,
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
                                        Value = -1 * r.Value
                                    }))
                        .Concat(
                            _entities.BenefitOpers
                                .Select(
                                    b =>
                                    new
                                    {
                                        CustomerID = b.ChargeOpers.Customers.ID,
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
                                        Value = -1 * b.Value
                                    }))
                        .Concat(
                            _entities.RebenefitOpers
                                .Select(
                                    r =>
                                    new
                                    {
                                        CustomerID = r.RechargeOpers.Customers.ID,
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
                                        Value = -1 * r.Value
                                    }))
                        .Concat(
                            _entities.OverpaymentOperPoses
                                .Select(
                                    o =>
                                    new
                                    {
                                        CustomerID = o.OverpaymentOpers.Customers.ID,
                                        o.Value
                                    }))
                        .Concat(
                            _entities.OverpaymentCorrectionOpers
                                .Select(
                                    o =>
                                    new
                                    {
                                        CustomerID = o.ChargeOpers.Customers.ID,
                                        o.Value
                                    }))
                        .Concat(
                            _entities.PaymentOperPoses
                                .Select(
                                    p =>
                                    new
                                    {
                                        CustomerID = p.PaymentOpers.Customers.ID,
                                        p.Value
                                    }))
                        .Concat(
                            _entities.PaymentCorrectionOpers
                                .Select(
                                    p =>
                                    new
                                    {
                                        CustomerID = p.PaymentOpers.Customers.ID,
                                        p.Value
                                    }))
                        .GroupBy(c => c.CustomerID)
                        .Select(
                            g =>
                            new
                            {
                                CustomerID = g.Key,
                                Value = g.Sum(c => (decimal?)c.Value) ?? 0
                            })
                        .Where(c => c.Value > 0);

                var _customers =
                    _entities.Customers
                        .Select(
                            c =>
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
                            })
                        .ToDictionary(c => c.ID);

                foreach (var _element in _result)
                {
                    var _customer = _customers[_element.CustomerID];

                    string _owner = "Неизвестен";

                    if (_customer.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson)
                    {
                        _owner = _customer.PhysicalPersonFullName;
                    }
                    else if (_customer.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson)
                    {
                        _owner = _customer.JuridicalPersonFullName;
                    }

                    DataRow _row = _table.NewRow();
                    _row[ColumnNames.STREET_COLUMN] = _customer.StreetName;
                    _row[ColumnNames.HOUSE_COLUMN] = _customer.BuildingNumber;
                    _row[ColumnNames.APARTMENT_COLUMN] = _customer.Apartment;
                    _row[ColumnNames.ACCOUNT_COLUMN] = _customer.Account;
                    _row[ColumnNames.OWNER_AKA_COLUMN] = _owner;
                    _row[ColumnNames.TOTAL] = _element.Value;

                    _table.Rows.Add(_row);
                }
                /*
                var _data =
                    from _customer in _entities.Customers
                    where
                        ((_customer.PaymentOpers.Sum(p => (decimal?)p.Value) ?? 0) -
                         (_customer.ChargeOpers.Where(c => c.CreationDateTime <= _till).Sum(c => (decimal?)c.Value) ?? 0)) < 0
                    select new
                    {
                        Street = _customer.Buildings.Streets.Name,
                        Building = _customer.Buildings.Number,
                        _customer.Apartment,
                        _customer.Account,
                        _customer.OwnerType,
                        _customer.PhysicalPersonFullName,
                        _customer.JuridicalPersonFullName,
                        Services =
                            from _pos in _entities.ChargeOperPoses.Include("ChargeOpers")
                            where _pos.ChargeOpers.Customers == _customer && _pos.ChargeOpers.CreationDateTime <= _till
                            group _pos by _pos.Services.ServiceTypes into _grouped
                            select new
                            {
                                ServiceTypeCode = _grouped.Key.Code,
                                Debt =
                                    (_entities.PaymentOperPoses
                                        .Where(
                                            pos => 
                                            pos.PaymentOpers.Customers.ID == _customer.ID &&
                                            pos.Services.ServiceTypes.ID == _grouped.Key.ID)
                                        .Sum(p => (decimal?)p.Value) ?? 0) 
                                    -
                                    (_grouped.Sum(c => (decimal?)c.Value) ?? 0)

                            },
                        Total =
                            (_customer.PaymentOpers.Sum(p => (decimal?)p.Value) ?? 0) -
                            (_customer.ChargeOpers.Where(c => c.CreationDateTime <= _till).Sum(c => (decimal?)c.Value) ?? 0),
                        CurrentMonthChargeValue = _customer.ChargeOpers.Where(c => c.CreationDateTime > _till).Sum(c => (decimal?)c.Value) ?? 0
                    };

                _serviceTypesDictionary.Clear();

                foreach (var _serviceType in _entities.ServiceTypes)
                {
                    _table.Columns.Add(_serviceType.Code, typeof(decimal));
                    _serviceTypesDictionary.Add(_serviceType.Code, _serviceType.Name);
                }

                _table.Columns.Add(ColumnNames.TOTAL, typeof(decimal));
                _table.Columns.Add(ColumnNames.CURRENT_MONTH_CHARGE, typeof(decimal));

                int _k = 1;

                foreach (var _element in _data)
                {
                    string _owner = "Неизвестен";

                    if (_element.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson)
                    {
                        _owner = _element.PhysicalPersonFullName;
                    }
                    else if (_element.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson)
                    {
                        _owner = _element.JuridicalPersonFullName;
                    }

                    DataRow _row = _table.NewRow();
                    _row[ColumnNames.KEY_COLUMN] = _k++;
                    _row[ColumnNames.STREET_COLUMN] = _element.Street;
                    _row[ColumnNames.HOUSE_COLUMN] = _element.Building;
                    _row[ColumnNames.APARTMENT_COLUMN] = _element.Apartment;
                    _row[ColumnNames.ACCOUNT_COLUMN] = _element.Account;
                    _row[ColumnNames.OWNER_AKA_COLUMN] = _owner;
                    _row[ColumnNames.TOTAL] = _element.Total;
                    _row[ColumnNames.CURRENT_MONTH_CHARGE] = _element.CurrentMonthChargeValue;

                    Dictionary<string, decimal> _selectedServiceTypes =
                        _element.Services.ToDictionary(e => e.ServiceTypeCode, e => e.Debt);

                    foreach (string _serviceTypeCode in _serviceTypesDictionary.Keys)
                    {
                        _row[_serviceTypeCode] = _selectedServiceTypes.ContainsKey(_serviceTypeCode)
                                                     ? _selectedServiceTypes[_serviceTypeCode]
                                                     : 0;
                    }

                    _table.Rows.Add(_row);
                }
                */
            }

            return _table;
        }
    }
}