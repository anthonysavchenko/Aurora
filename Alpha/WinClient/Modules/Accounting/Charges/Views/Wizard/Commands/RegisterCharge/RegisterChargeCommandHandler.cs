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

            int _chargeSetId = 2103;//CreateChargeSet(cmd.Now, cmd.Period, cmd.AuthorId);
            //Dictionary<int, int> _billSetByBuilding = CreateBillSets(cmd.Now);

            int[] _customers = new int[] { 2130, 2131, 2132, 2133, 2134, 2135, 2136, 2137, 2138, 2139, 2140, 2141, 2142, 2143, 2144, 2145, 2146, 2147, 2148, 2149, 2150, 2151, 2152, 2153, 2154, 2155, 2156, 2157, 2158, 2159, 2160, 2161, 2162, 2163, 2164, 2165, 2166, 2167, 2168, 2169, 2170, 2171, 2172, 2173, 2174, 2175, 2176, 2177, 2178, 2179, 2180, 2181, 2182, 2183, 2184, 2185, 2186, 2187, 2188, 2189, 2190, 2191, 2192, 2193, 2194, 2195, 2196, 2197, 2198, 2199, 2200, 2201, 2202, 2203, 2204, 2205, 2206, 2207, 2208, 2209, 2210, 2211, 2212, 2213, 2214, 2215, 2216, 2217, 2218, 2219, 2220, 2221, 2222, 2223, 2224, 2225, 2226, 2227, 2228, 2229, 2230, 2231, 2232, 2233, 2234, 2235, 2236, 2237, 2238, 2239, 2240, 2241, 2242, 2243, 2244, 2245, 2246, 2247, 2248, 2249, 2250, 2251, 2252, 2253, 2254, 2255 };
            //using (Entities _db = new Entities())
            //{
            //    _customers = _db.Customers.Select(c => c.ID).ToArray();
            //}

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
                                BillSetId = 3927,//_billSetByBuilding[_customerInfo.BuildingId],
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
