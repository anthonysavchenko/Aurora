﻿using System;
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

            DateTime _chargePeriod = cmd.ChargeOper.ChargeSets.Period;

            decimal _rest = cmd.Db.GetTotalForCustomer(cmd.CustomerInfo.Id, _chargePeriod);

            DateTime _payBefore = new DateTime(_chargePeriod.Year, _chargePeriod.Month, 10).AddMonths(1);

            RegularBillDocs _billDoc =
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
                    Period = _chargePeriod,
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
                _billDoc.ContractorContactInfo = $"{_cont.Name}, {_cont.ContactInfo}";
            }
            else
            {
                _billDoc.ContractorContactInfo = string.Empty;
            }

            cmd.Db.RegularBillDocs.AddObject(_billDoc);

            cmd.ChargeOper.RegularBillDocs = _billDoc;

            if (cmd.ChargePeriodBalance.Count > 0)
            {
                var _poses = cmd.ChargePeriodBalance
                    .GroupBy(serviceBalance =>
                        new
                        {
                            ServiceTypeID = cmd.Services[serviceBalance.Key].ServiceTypes.ID,
                            ServiceTypeName = cmd.Services[serviceBalance.Key].ServiceTypes.Name,
                        })
                    .Select(groupedByServiceType => new
                    {
                        groupedByServiceType.Key.ServiceTypeID,
                        groupedByServiceType.Key.ServiceTypeName,
                        Charge = groupedByServiceType.Sum(x => x.Value.Charge),
                        Benefit = groupedByServiceType.Sum(x => x.Value.Benefit),
                        Recharge = groupedByServiceType.Sum(x => x.Value.Recharge),
                    });

                foreach (var _pos in _poses)
                {
                    cmd.Db.RegularBillDocSeviceTypePoses.AddObject(
                        new RegularBillDocSeviceTypePoses
                        {
                            RegularBillDocs = _billDoc,
                            ServiceTypeID = _pos.ServiceTypeID,
                            ServiceTypeName = _pos.ServiceTypeName,
                            PayRate = Math.Round(_pos.Charge / cmd.CustomerInfo.Area, 2, MidpointRounding.AwayFromZero),
                            Charge = _pos.Charge,
                            Benefit = _pos.Benefit,
                            Recalculation = _pos.Recharge,
                            Payable = _pos.Charge + _pos.Benefit + _pos.Recharge,
                        });
                }
            }

            _billSet.Quantity++;
            _billSet.ValueSum += cmd.ChargeOper.Value;
        }
    }
}
