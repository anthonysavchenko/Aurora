using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateBillCommandHandler : ICommandHandler<CreateBillCommand>
    {
        private const int CONTRACTOR_CONTACT_INFO_SERVICE_ID = 6;
        private const int MADIX_CONTRACTOR_ID = 8; /* ООО "Мадикс" */

        public void Execute(CreateBillCommand cmd)
        {
            BillSets _billSet = cmd.Db.BillSets.FirstOrDefault(x => x.ID == cmd.BillSetId);

            decimal _currentPeriodTotal =
                cmd.ChargePeriodBalance.Values.Sum(x => x.Charge)
                + cmd.ChargePeriodBalance.Values.Sum(x => x.Benefit)
                + cmd.ChargePeriodBalance.Values.Sum(x => x.Recharge);

            DateTime chargePeriod = cmd.ChargeOper.ChargeSets.Period;
            DateTime prevPeriod = chargePeriod.AddMonths(-1);
            DateTime _payBefore = new DateTime(chargePeriod.Year, chargePeriod.Month, 10).AddMonths(1);

            decimal _rest = cmd.Db.GetTotalForCustomer(cmd.CustomerInfo.Id, chargePeriod);

            RegularBillDocs billDoc =
                new RegularBillDocs
                {
                    CreationDateTime = cmd.ChargeOper.CreationDateTime,
                    Account = cmd.CustomerInfo.Account,
                    Address = $"ул. {cmd.CustomerInfo.Street}, {cmd.CustomerInfo.Building}, кв. {cmd.CustomerInfo.Apartment}",
                    Owner = cmd.CustomerInfo.Owner,
                    Square = $"{cmd.CustomerInfo.Area} кв.м.",
                    ResidentsCount = cmd.CustomerInfo.ResidentsCount,
                    Customers = cmd.DbCustomerStub,
                    BillSets = _billSet,
                    Period = chargePeriod,
                    EmergencyPhoneNumber = cmd.CustomerInfo.Poses.Any(pos => pos.ContractorId == MADIX_CONTRACTOR_ID)
                        ? "261-47-14" : "298-09-81",
                    PayBeforeDateTime = _payBefore,
                    MonthChargeValue = _currentPeriodTotal,
                    OverpaymentValue = _rest,
                    Value = _currentPeriodTotal + _rest,
                };

            var _contractorPos = cmd.CustomerInfo.Poses.FirstOrDefault(p => p.ServiceId == CONTRACTOR_CONTACT_INFO_SERVICE_ID);
            if (_contractorPos != null)
            {
                Contractors _cont = cmd.Contractors[_contractorPos.ContractorId];
                billDoc.ContractorContactInfo = $"{_cont.Name}, {_cont.ContactInfo}";
            }
            else
            {
                billDoc.ContractorContactInfo = string.Empty;
            }

            cmd.Db.RegularBillDocs.AddObject(billDoc);

            cmd.ChargeOper.RegularBillDocs = billDoc;

            if (cmd.ChargePeriodBalance.Count > 0)
            {
                // TODO подумать как оптимизировать, чтобы постоянно не запрашивать типы услуг по услугам
                var _services = cmd.Db.Services
                    .Where(x => cmd.ChargePeriodBalance.Keys.Contains(x.ID))
                    .Select(x =>
                        new
                        {
                            x.ID,
                            ServiceTypeId = x.ServiceTypes.ID,
                            ServiceTypeName = x.ServiceTypes.Name
                        })
                    .ToList();

                var _poses = cmd.ChargePeriodBalance
                    .Join(_services,
                        x => x.Key,
                        y => y.ID,
                        (x, y) =>
                            new
                            {
                                y.ServiceTypeId,
                                y.ServiceTypeName,
                                Balance = x.Value
                            })
                    .GroupBy(x =>
                        new
                        {
                            x.ServiceTypeId,
                            x.ServiceTypeName
                        })
                    .Select(g => 
                        new
                        {
                            g.Key.ServiceTypeId,
                            g.Key.ServiceTypeName,
                            Charge = g.Sum(x => x.Balance.Charge),
                            Benefit = g.Sum(x => x.Balance.Benefit),
                            Recharge = g.Sum(x => x.Balance.Recharge),
                        });

                foreach (var _pos in _poses)
                {
                    cmd.Db.RegularBillDocSeviceTypePoses.AddObject(
                        new RegularBillDocSeviceTypePoses
                        {
                            RegularBillDocs = billDoc,
                            ServiceTypeID = _pos.ServiceTypeId,
                            ServiceTypeName = _pos.ServiceTypeName,
                            PayRate = Math.Round(_pos.Charge / cmd.CustomerInfo.Area, 2, MidpointRounding.AwayFromZero),
                            Charge = _pos.Charge,
                            Benefit = _pos.Benefit,
                            Recalculation = _pos.Recharge,
                            Payable = _pos.Charge + _pos.Benefit + _pos.Recharge,
                        });
                }
            }
            else if (_rest > 0)
            {
                var prevPoses = cmd.Db.RegularBillDocSeviceTypePoses
                    .Where(r =>
                        r.RegularBillDocs.Customers.ID == cmd.DbCustomerStub.ID
                            && r.RegularBillDocs.Period == prevPeriod)
                    .Select(r => new
                    {
                        r.ServiceTypeID,
                        r.ServiceTypeName,
                    })
                    .ToList();

                foreach (var prevPos in prevPoses)
                {
                    cmd.Db.RegularBillDocSeviceTypePoses.AddObject(
                        new RegularBillDocSeviceTypePoses
                        {
                            RegularBillDocs = billDoc,
                            ServiceTypeID = prevPos.ServiceTypeID,
                            ServiceTypeName = prevPos.ServiceTypeName,
                            Charge = 0,
                            Benefit = 0,
                            Recalculation = 0,
                            Payable = 0,
                        });
                }
            }

            var counters = cmd.Db.PrivateCounters
                // TODO: Также тут нужно фильтровать по датам актуальности счетчика, которых пока нет
                .Where(counter => counter.CustomerID == cmd.CustomerInfo.Id)
                .Select(counter => new
                {
                    ServiceName = counter.Services.Name,
                    CounterNumber = counter.Number,
                    PrevValue = cmd.Db.PrivateCounterValues.FirstOrDefault(value =>
                        value.PrivateCounters.ID == counter.ID && value.Period == prevPeriod),
                    CurrentValue = cmd.Db.PrivateCounterValues.FirstOrDefault(value =>
                        value.PrivateCounters.ID != counter.ID && value.Period == chargePeriod),
                    CustomerPos = cmd.Db.CustomerPoses.FirstOrDefault(pos =>
                        pos.Customers.ID == cmd.CustomerInfo.Id &&
                        pos.Services.ID == counter.Services.ID &&
                        pos.Since <= chargePeriod &&
                        pos.Till >= chargePeriod),
                    counter.Services.Measure
                })
                .Select(counter => new
                {
                    counter.ServiceName,
                    counter.CounterNumber,
                    counter.PrevValue,
                    counter.CurrentValue,
                    Consumption =
                        counter.PrevValue != null &&
                        counter.CurrentValue != null &&
                        counter.CurrentValue.Value > counter.PrevValue.Value
                            ? counter.CurrentValue.Value - counter.PrevValue.Value : 0,
                    Rate = counter.CustomerPos != null ? counter.CustomerPos.Rate : 0,
                    counter.Measure
                })
                .ToList();

            foreach (var counter in counters)
            {
                cmd.Db.RegularBillDocCounterPoses.AddObject(new RegularBillDocCounterPoses()
                {
                    RegularBillDocs = billDoc,
                    ServiceName = counter.ServiceName,
                    Number = counter.CounterNumber,
                    PrevValue = counter.PrevValue?.Value ?? 0,
                    CurValue = counter.CurrentValue?.Value ?? 0,
                    Consumption = counter.Consumption,
                    Rate = counter.Rate,
                    Measure = counter.Measure
                });
            }

            _billSet.Quantity++;
            _billSet.ValueSum += cmd.ChargeOper.Value;
        }
    }
}
