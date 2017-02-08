using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Taumis.Alpha.Server.Core.Models.Docs;
using Taumis.Alpha.Server.Core.Models.Enums;
using Taumis.Alpha.Server.Core.Services;
using Taumis.Alpha.Server.Core.Services.ServerTime;
using Taumis.Alpha.Server.Infrastructure.Data;
using Taumis.Alpha.Server.PrintForms.DataSets;

namespace Taumis.Alpha.Server.Infrastructure.Services
{
    public class MutualSettlementService : IMutualSettlementService
    {
        #region Вспомогательные классы

        private class ServiceBalances
        {
            public IDictionary<Key, Balance> Balances
            {
                set;
                get;
            }

            public ServiceBalances()
            {
                Balances = new Dictionary<Key, Balance>();
            }

            public void Add(Key key, Balance value)
            {
                if (!Balances.ContainsKey(key))
                {
                    Balances.Add(key, new Balance());
                }

                Balances[key].Add(value);
            }
        }

        private class Key
        {
            public int ID
            {
                set;
                get;
            }

            public string Name
            {
                set;
                get;
            }

            public override int GetHashCode()
            {
                return ID.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj != null && ((Key)obj).ID == ID;
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

            public decimal Recharge
            {
                set;
                get;
            }

            public decimal Payable
            {
                set;
                get;
            }

            public decimal Payment
            {
                set;
                get;
            }

            public decimal Debt
            {
                set;
                get;
            }

            public void Add(Balance value)
            {
                Charge += value.Charge;
                Benefit += value.Benefit;
                Recharge += value.Recharge;
                Payable += value.Payable;
                Payment += value.Payment;
                Debt += value.Debt;
            }
        }

        #endregion

        private readonly IAlphaDbContext _db;
        private readonly IServerTimeService _sts;

        public MutualSettlementService(IAlphaDbContext db, IServerTimeService sts)
        {
            _db = db;
            _sts = sts;
        }

        public DataSet GetDataForReport(int customerId)
        {
            MutualSettlementDataSet _data = new MutualSettlementDataSet();
            DataTable _settlementTable = _data.Tables["MutualSettlement"];
            DataTable _posesTable = _data.Tables["MutualSettlementPoses"];

            try
            {
                Customer _customer =
                    _db.Customers
                        .Include("Residents")
                        .Include("Building")
                        .Include("Building.Street")
                        .First(c => c.ID == customerId);

                #region Запросы

                var _periodBalances =
                    _db.ChargeOperPoses
                        .Select(c =>
                            new
                            {
                                c.ChargeOper.CustomerID,
                                c.ChargeOper.ChargeSet.Period,
                                ServiceTypeID = c.Service.ServiceType.ID,
                                Charge = c.Value,
                                Recharge = (decimal)0,
                                Benefit = (decimal)0,
                                Payment = (decimal)0
                            })
                        .Concat(
                            _db.RechargeOperPoses
                                .Select(c =>
                                    new
                                    {
                                        c.RechargeOper.CustomerID,
                                        c.RechargeOper.RechargeSet.Period,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = c.Value,
                                        Benefit = (decimal)0,
                                        Payment = (decimal)0
                                    }))
                        .Concat(
                            _db.ChargeOperPoses
                                .Where(c => c.ChargeOper.ChargeCorrectionOper != null)
                                .Select(c =>
                                    new
                                    {
                                        c.ChargeOper.CustomerID,
                                        c.ChargeOper.ChargeCorrectionOper.Period,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = -c.Value,
                                        Benefit = (decimal)0,
                                        Payment = (decimal)0
                                    }))
                        .Concat(
                            _db.RechargeOperPoses
                                .Where(r => r.RechargeOper.ChargeCorrectionOper != null)
                                .Select(c =>
                                    new
                                    {
                                        c.RechargeOper.CustomerID,
                                        c.RechargeOper.ChargeCorrectionOper.Period,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = -c.Value,
                                        Benefit = (decimal)0,
                                        Payment = (decimal)0
                                    }))
                        .Concat(
                            _db.BenefitOperPoses
                                .Select(c =>
                                    new
                                    {
                                        c.BenefitOper.ChargeOper.CustomerID,
                                        c.BenefitOper.ChargeOper.ChargeSet.Period,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = (decimal)0,
                                        Benefit = c.Value,
                                        Payment = (decimal)0
                                    }))
                        .Concat(
                            _db.RebenefitOperPoses
                                .Select(c =>
                                    new
                                    {
                                        c.RebenefitOper.RechargeOper.CustomerID,
                                        c.RebenefitOper.RechargeOper.RechargeSet.Period,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = (decimal)0,
                                        Benefit = c.Value,
                                        Payment = (decimal)0
                                    }))
                        .Concat(
                            _db.BenefitOperPoses
                                .Where(c => c.BenefitOper.BenefitCorrectionOper != null)
                                .Select(c =>
                                    new
                                    {
                                        c.BenefitOper.ChargeOper.CustomerID,
                                        c.BenefitOper.BenefitCorrectionOper.ChargeCorrectionOper.Period,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = (decimal)0,
                                        Benefit = -c.Value,
                                        Payment = (decimal)0
                                    }))
                        .Concat(
                            _db.RebenefitOperPoses
                                .Where(r => r.RebenefitOper.BenefitCorrectionOper != null)
                                .Select(c =>
                                    new
                                    {
                                        c.RebenefitOper.RechargeOper.CustomerID,
                                        c.RebenefitOper.BenefitCorrectionOper.ChargeCorrectionOper.Period,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = (decimal)0,
                                        Benefit = -c.Value,
                                        Payment = (decimal)0
                                    }))
                        .Concat(
                            _db.PaymentOperPoses
                                .Select(c =>
                                    new
                                    {
                                        c.PaymentOper.CustomerID,
                                        Period = DbFunctions.TruncateTime(c.PaymentOper.CreationDateTime).Value,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = (decimal)0,
                                        Benefit = (decimal)0,
                                        Payment = c.Value
                                    }))
                        .Concat(
                            _db.PaymentOperPoses
                                .Where(p => p.PaymentOper.PaymentCorrectionOper != null)
                                .Select(c =>
                                    new
                                    {
                                        c.PaymentOper.CustomerID,
                                        c.PaymentOper.PaymentCorrectionOper.Period,
                                        ServiceTypeID = c.Service.ServiceType.ID,
                                        Charge = (decimal)0,
                                        Recharge = (decimal)0,
                                        Benefit = (decimal)0,
                                        Payment = -c.Value
                                    }))
                        .Where(c => c.CustomerID == _customer.ID)
                        .GroupBy(c => 
                            new
                            {
                                c.Period,
                                c.ServiceTypeID
                            })
                        .Select(g =>
                            new
                            {
                                g.Key.Period,
                                g.Key.ServiceTypeID,
                                Charge = g.Sum(c => c.Charge),
                                Recharge = g.Sum(c => c.Recharge),
                                Benefit = g.Sum(c => c.Benefit),
                                Payment = g.Sum(c => c.Payment)
                            })
                        .ToList()
                        .Join(
                            _db.ServiceTypes.Select(s => new { s.ID, s.Name }), 
                            g => g.ServiceTypeID,
                            st => st.ID,
                            (g, st) => 
                                new
                                {
                                    g.Period,
                                    g.ServiceTypeID,
                                    ServiceTypeName = st.Name,
                                    g.Charge,
                                    g.Recharge,
                                    g.Benefit,
                                    g.Payment
                                })
                        .GroupBy(x => x.Period)
                        .Select(g =>
                            new
                            {
                                Period = g.Key,
                                ServiceTypeBalance =
                                    g.GroupBy(c => new { c.ServiceTypeID, c.ServiceTypeName })
                                        .Select(gs =>
                                            new
                                            {
                                                ServiceTypeID = gs.Key.ServiceTypeID,
                                                ServiceTypeName = gs.Key.ServiceTypeName,
                                                Charge = gs.Sum(c => c.Charge),
                                                Recharge = gs.Sum(c => c.Recharge),
                                                Benefit = gs.Sum(c => c.Benefit),
                                                Payment = gs.Sum(c => c.Payment)
                                            })
                            })
                        .OrderBy(c => c.Period)
                        .Select(b =>
                            new
                            {
                                b.Period,
                                ServiceBalances =
                                    new ServiceBalances
                                    {
                                        Balances =
                                            b.ServiceTypeBalance
                                                .OrderBy(sb => sb.ServiceTypeName)
                                                .ToDictionary(
                                                    sb =>
                                                        new Key
                                                        {
                                                            ID = sb.ServiceTypeID,
                                                            Name = sb.ServiceTypeName
                                                        },
                                                    sb =>
                                                        new Balance
                                                        {
                                                            Charge = sb.Charge,
                                                            Benefit = sb.Benefit,
                                                            Recharge = sb.Recharge,
                                                            Payable = sb.Charge + sb.Benefit + sb.Recharge,
                                                            Payment = sb.Payment,
                                                            Debt = sb.Charge + sb.Benefit + sb.Recharge + sb.Payment
                                                        })
                                    }
                            })
                        .ToArray();

                #endregion

                int _reportNumber = 0;
                int _groupNumber = 0;
                DataRow _lastReportRow = null;
                DateTime _previousPeriod = DateTime.MinValue;
                DateTime _now = _sts.GetDateTimeInfo().Now;
                ServiceBalances _yearBalances = new ServiceBalances();
                ServiceBalances _reportBalances = new ServiceBalances();

                for (int i = 0; i < _periodBalances.Length; i++)
                {
                    var _periodBalance = _periodBalances[i];

                    // Заполенение таблицы отчетов
                    if (_previousPeriod == DateTime.MinValue || _periodBalance.Period != _previousPeriod.AddMonths(1))
                    {
                        _lastReportRow = _settlementTable.Rows.Add(
                            ++_reportNumber,
                            _previousPeriod != DateTime.MinValue
                                ? string.Format("{0} - ", _periodBalance.Period.ToString("MMMM yyyy"))
                                : "по ",
                            _now,
                            _customer.OwnerType == OwnerTypes.JuridicalPerson
                                ? _customer.JuridicalPersonFullName
                                : _customer.PhysicalPersonShortName,
                            string.Format("ул. {0}, {1}, кв. {2}",
                                _customer.Building.Street.Name,
                                _customer.Building.Number,
                                _customer.Apartment),
                            string.Format("{0} кв.м.", _customer.Square),
                            _customer.Residents.Count(),
                            "");
                    }

                    // Заполнение таблицы балансов по типам услуг за месяц
                    foreach (KeyValuePair<Key, Balance> _serviceTypeBalance in _periodBalance.ServiceBalances.Balances)
                    {
                        _yearBalances.Add(_serviceTypeBalance.Key, _serviceTypeBalance.Value);
                        _reportBalances.Add(_serviceTypeBalance.Key, _serviceTypeBalance.Value);

                        _posesTable.Rows.Add(
                            _reportNumber,
                            _periodBalance.Period.ToString("MMMM yyyy"),
                            _groupNumber,
                            _serviceTypeBalance.Key.Name,
                            _serviceTypeBalance.Value.Charge,
                            Math.Abs(_serviceTypeBalance.Value.Benefit),
                            _serviceTypeBalance.Value.Recharge,
                            _serviceTypeBalance.Value.Payable,
                            Math.Abs(_serviceTypeBalance.Value.Payment),
                            _serviceTypeBalance.Value.Debt);
                    }

                    _groupNumber++;

                    // Заполенение таблицы балансов по типа услуг за год
                    if (i + 1 == _periodBalances.Length || _periodBalances[i + 1].Period.Year != _periodBalance.Period.Year)
                    {
                        foreach (KeyValuePair<Key, Balance> _serviceTypeBalance in _yearBalances.Balances)
                        {
                            _posesTable.Rows.Add(
                                _reportNumber,
                                _periodBalance.Period.ToString("yyyy"),
                                _groupNumber,
                                _serviceTypeBalance.Key.Name,
                                _serviceTypeBalance.Value.Charge,
                                Math.Abs(_serviceTypeBalance.Value.Benefit),
                                _serviceTypeBalance.Value.Recharge,
                                _serviceTypeBalance.Value.Payable,
                                Math.Abs(_serviceTypeBalance.Value.Payment),
                                _serviceTypeBalance.Value.Debt);
                        }

                        _yearBalances.Balances.Clear();
                        _groupNumber++;
                    }

                    // Заполнение таблицы балансов по типам услуг за один отчет
                    if (i + 1 == _periodBalances.Length || _periodBalances[i + 1].Period != _periodBalance.Period.AddMonths(1))
                    {
                        _lastReportRow["SinceTillPeriods"] =
                            string.Format("{0}{1:MMMM yyyy}", _lastReportRow["SinceTillPeriods"], _periodBalance.Period);

                        foreach (KeyValuePair<Key, Balance> _serviceTypeBalance in _reportBalances.Balances)
                        {
                            _posesTable.Rows.Add(
                                _reportNumber,
                                "Итого",
                                _groupNumber,
                                _serviceTypeBalance.Key.Name,
                                _serviceTypeBalance.Value.Charge,
                                Math.Abs(_serviceTypeBalance.Value.Benefit),
                                _serviceTypeBalance.Value.Recharge,
                                _serviceTypeBalance.Value.Payable,
                                Math.Abs(_serviceTypeBalance.Value.Payment),
                                _serviceTypeBalance.Value.Debt);
                        }

                        _reportBalances.Balances.Clear();
                        _groupNumber++;
                    }

                    _previousPeriod = _periodBalance.Period;
                }
            }
            catch (Exception _ex)
            {
                //Logger.SimpleWrite(string.Format("Mutual settlement error: {0}", _ex));
            }

            return _data;
        }
    }
}