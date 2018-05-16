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
        private readonly Entities _db;

        public CreateBillCommandHandler(Entities db)
        {
            _db = db;
        }

        public void Execute(CreateBillCommand command)
        {
            BillSets _billSet = _db.BillSets.FirstOrDefault(x => x.ID == command.BillSetId);

            decimal _currentPeriodTotal =
                command.ChargePeriodBalance.Values.Sum(x => x.Charge)
                + command.ChargePeriodBalance.Values.Sum(x => x.Benefit)
                + command.ChargePeriodBalance.Values.Sum(x => x.Recharge);

            DateTime _chargePeriod = command.ChargeOper.ChargeSets.Period;

            decimal _rest = _db.GetTotalForCustomer(command.CustomerInfo.Id, _chargePeriod);

            DateTime _payBefore = new DateTime(_chargePeriod.Year, _chargePeriod.Month, 10).AddMonths(1);

            RegularBillDocs _billDoc =
                new RegularBillDocs
                {
                    CreationDateTime = command.ChargeOper.CreationDateTime,
                    Account = command.CustomerInfo.Account,
                    Address = $"ул. {command.CustomerInfo.Street}, {command.CustomerInfo.Building}, кв. {command.CustomerInfo.Apartment}",
                    Owner = command.CustomerInfo.Owner,
                    Square = $"{command.CustomerInfo.Area} кв.м.",
                    ResidentsCount = command.CustomerInfo.ResidentsCount,
                    Customers = command.DbCustomerStub,
                    BillSets = _billSet,
                    Period = _chargePeriod,
                    EmergencyPhoneNumber = command.CustomerInfo.Poses.Any(pos => pos.ContractorId == MADIX_CONTRACTOR_ID)
                        ? "261-47-14" : "298-09-81",
                    PayBeforeDateTime = _payBefore,
                    MonthChargeValue = _currentPeriodTotal,
                    OverpaymentValue = _rest,
                    Value = _currentPeriodTotal + _rest,
                };

            var _contractorPos = command.CustomerInfo.Poses.FirstOrDefault(p => p.ServiceId == CONTRACTOR_CONTACT_INFO_SERVICE_ID);
            if (_contractorPos != null)
            {
                Contractors _cont = command.Contractors[_contractorPos.ContractorId];
                _billDoc.ContractorContactInfo = $"{_cont.Name}, {_cont.ContactInfo}";
            }
            else
            {
                _billDoc.ContractorContactInfo = string.Empty;
            }

            _db.RegularBillDocs.AddObject(_billDoc);

            command.ChargeOper.RegularBillDocs = _billDoc;

            if (command.ChargePeriodBalance.Count > 0)
            {
                var _poses = command.ChargePeriodBalance
                    .GroupBy(serviceBalance =>
                        new
                        {
                            ServiceTypeID = command.Services[serviceBalance.Key].ServiceTypes.ID,
                            ServiceTypeName = command.Services[serviceBalance.Key].ServiceTypes.Name,
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
                    _db.RegularBillDocSeviceTypePoses.AddObject(
                        new RegularBillDocSeviceTypePoses
                        {
                            RegularBillDocs = _billDoc,
                            ServiceTypeID = _pos.ServiceTypeID,
                            ServiceTypeName = _pos.ServiceTypeName,
                            PayRate = Math.Round(_pos.Charge / command.CustomerInfo.Area, 2, MidpointRounding.AwayFromZero),
                            Charge = _pos.Charge,
                            Benefit = _pos.Benefit,
                            Recalculation = _pos.Recharge,
                            Payable = _pos.Charge + _pos.Benefit + _pos.Recharge,
                        });
                }
            }

            _billSet.Quantity++;
            _billSet.ValueSum += command.ChargeOper.Value;
        }
    }
}
