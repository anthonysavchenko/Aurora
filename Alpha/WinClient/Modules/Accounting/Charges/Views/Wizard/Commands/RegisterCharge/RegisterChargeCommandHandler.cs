using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Common;
using Taumis.Alpha.Infrastructure.Interface.ExtensionMethods;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterChargeCommandHandler : ICommandHandler<RegisterChargeCommand>
    {
        private readonly ICache _cache = new Cache();
        private readonly PaymentDistributionService _pds;
        private ICommandDispatcher _dispatcher;

        public RegisterChargeCommandHandler(PaymentDistributionService pds, ICommandDispatcher dispatcher)
        {
            _pds = pds;
            _dispatcher = dispatcher;
        }

        public void Execute(RegisterChargeCommand cmd)
        {
            cmd.Result = new RegisterCommandResult();

            _cache.Init(cmd.Period);

            int _chargeSetId = 38; //35, 36, 37, 38

            int[] _customers;
            int[] _currentCustomers = { 1965, 1966, 1967, 1968, 1969, 1970, 1971, 1972, 1973, 1974, 1975, 1976, 1977, 1978, 1979, 1980, 1981, 1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032, 2033, 2034, 2035, 2036, 2037, 2038, 2039, 2040, 2041, 2042, 2043, 2044, 2045, 2046, 2047, 2048, 2049, 2050, 2051, 2052, 2053, 2054, 2055, 2056, 2057, 2058, 2059, 2060, 2061, 2062, 2063, 2064, 2065, 2066, 2067, 2068, 2069, 2070, 2071, 2072, 2073, 2074, 2075, 2076, 2077, 2078, 2079, 2080, 2081, 2082, 2083, 2084, 2085, 2086, 2087, 2088, 2089, 2090, 2091, 2092, 2093 };
            using (Entities _db = new Entities())
            {
                _customers = _db.Customers.Where(c => _currentCustomers.Contains(c.ID)).Select(c => c.ID).ToArray();
            }

            cmd.ResetProgressBar(_customers.Length);

            for (int i = 0; i < _customers.Length; i++)
            {
                int _customerId = _customers[i];

                using (var _db = new Entities())
                {
                    _db.CommandTimeout = 3600;
                    try
                    {
                        var _chargeSet = _db.ChargeSets.First(x => x.ID == _chargeSetId);

                        var _periodBalance = _db
                            .GetCustomerBalancesGroupedByPeriod(_customerId, beforeGroupFilter: x => x.Period == cmd.Period)
                            .Values
                            .FirstOrDefault() ?? new Dictionary<int, Balance>();
                        var _services = _db.Services.ToDictionary(x => x.ID);
                        var _contractors = _db.Contractors.ToDictionary(x => x.ID);
                        var _dbCustomerStub = new Customers { ID = _customerId };
                        _db.Customers.Attach(_dbCustomerStub);
                        CustomerInfo _customerInfo = _db.GetCustomerInfo(_customerId, cmd.Period);

                        var _calculateChargesCommand =
                            new CalculateChargeCommand
                            {
                                Cache = _cache,
                                CustomerInfo = _customerInfo
                            };
                        _dispatcher.Execute(_calculateChargesCommand);
                        _periodBalance.AddCharges(
                            _calculateChargesCommand.Result
                                .Join(_customerInfo.Poses,
                                    x => x.Key,
                                    y => y.Id,
                                    (x, y) =>
                                    new
                                    {
                                        y.ServiceId,
                                        x.Value
                                    })
                                .ToDictionary(x => x.ServiceId, x => x.Value));

                        var _calculateBenefitsCommand =
                            new CalculateBenefitsCommand
                            {
                                CustomerInfo = _customerInfo
                            };
                        _dispatcher.Execute(_calculateBenefitsCommand);
                        _periodBalance.AddBenefits(
                            _calculateBenefitsCommand.Result
                                .Join(_customerInfo.Poses,
                                    x => x.Key,
                                    y => y.Id,
                                    (x, y) =>
                                    new
                                    {
                                        y.ServiceId,
                                        x.Value
                                    })
                                .ToDictionary(x => x.ServiceId, x => x.Value));

                        var _createChargeOperCommand =
                            new CreateChargeCommand
                            {
                                BenefitsByPos = _calculateBenefitsCommand.Result,
                                ChargesByPos = _calculateChargesCommand.Result,
                                ChargeSet = _chargeSet,
                                CustomerInfo = _customerInfo,
                                DbCustomerStub = _dbCustomerStub,
                                Now = cmd.Now,
                                Period = cmd.Period,
                                Services = _services,
                                Contractors = _contractors,
                                Db = _db
                            };
                        _dispatcher.Execute(_createChargeOperCommand);

                        _dispatcher.Execute(
                            new CreateOverpaymentCommand
                            {
                                ChargeOper = _createChargeOperCommand.Result,
                                ChargePeriodBalance = _periodBalance,
                                DbCustomerStub = _dbCustomerStub,
                                Period = cmd.Period,
                                LastChargedPeriod = cmd.LastChargedPeriod,
                                Services = _services,
                                Db = _db
                            });

                        _dispatcher.Execute(
                            new CreateBillCommand
                            {
                                BillSetId = 95, //82, 86, 91, 95
                                ChargeOper = _createChargeOperCommand.Result,
                                ChargePeriodBalance = _periodBalance,
                                Contractors = _contractors,
                                CustomerInfo = _customerInfo,
                                DbCustomerStub = _dbCustomerStub,
                                Db = _db
                            });

                        _db.SaveChanges();
                        cmd.Result.Processed++;
                        cmd.Result.Total += _createChargeOperCommand.Result.Value;

                        cmd.ProgressAction(1);
                    }
                    catch (Exception ex)
                    {
                        Logger.SimpleWrite($"RegisterChargeCommand. CustomerId: {_customerId}\r\nException: {ex}");
                    }
                }
            }
        }

        private int CreateChargeSet(DateTime now, DateTime period, int authorId)
        {
            int _chargeSetId;

            using (Entities _db = new Entities())
            {
                Users _user = new Users { ID = authorId };
                _db.Users.Attach(_user);

                var _chargeSet =
                    new ChargeSets
                    {
                        CreationDateTime = now,
                        Period = period,
                        Number = _db.ChargeSets.Any() ? _db.ChargeSets.Max(c => c.Number) + 1 : 1,
                        Author = _user
                    };

                _db.ChargeSets.AddObject(_chargeSet);
                _db.SaveChanges();
                _chargeSetId = _chargeSet.ID;
            }

            return _chargeSetId;
        }

        private Dictionary<int, int> CreateBillSets(DateTime now)
        {
            var _result = new Dictionary<int, int>();

            using (Entities _db = new Entities())
            {
                var _zipCodes = _db.Buildings
                    .GroupBy(x => x.ZipCode)
                    .Select(g =>
                        new
                        {
                            ZipCode = g.Key,
                            BuildingIds = g.Select(x => x.ID)
                        })
                    .ToList();

                foreach (var _zc in _zipCodes)
                {
                    BillSets _billSet =
                        new BillSets()
                        {
                            CreationDateTime = now,
                            Number = _db.BillSets.Any() ? _db.BillSets.Max(c => c.Number) + 1 : 1,
                            BillType = (byte)BillType.Regular,
                        };
                    _db.AddToBillSets(_billSet);
                    _db.SaveChanges();

                    foreach (int _id in _zc.BuildingIds)
                    {
                        _result.Add(_id, _billSet.ID);
                    }
                }
            }

            return _result;
        }
    }
}
