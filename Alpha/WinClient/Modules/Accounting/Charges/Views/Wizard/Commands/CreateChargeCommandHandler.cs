using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateChargeCommandHandler : ICommandHandler<CreateChargeCommand>
    {
        private readonly Entities _db;

        public CreateChargeCommandHandler(Entities db)
        {
            _db = db;
        }

        public void Execute(CreateChargeCommand command)
        {
            ChargeSets _chargeSet =_db.ChargeSets.First(x => x.ID == command.ChargeSetId);

            var _chargeOper =
                new ChargeOpers
                {
                    CreationDateTime = command.Now,
                    Customers = command.DbCustomerStub,
                    ChargeSets = _chargeSet,
                    Value = command.ChargesByPos.Sum(x => x.Value)
                };
            _db.ChargeOpers.AddObject(_chargeOper);

            BenefitOpers _benefitOper = null;

            if (command.BenefitsByPos.Count > 0)
            {
                _benefitOper =
                    new BenefitOpers
                    {
                        ChargeOpers = _chargeOper,
                        Value = command.BenefitsByPos.Sum(x => x.Value)
                    };
                _db.BenefitOpers.AddObject(_benefitOper);
            }

            foreach (var _pos in command.CustomerInfo.Poses)
            {
                if (command.ChargesByPos.ContainsKey(_pos.Id))
                {
                    var _operPos =
                        new ChargeOperPoses
                        {
                            ChargeOpers = _chargeOper,
                            Services = command.Services[_pos.ServiceId],
                            Contractors = command.Contractors[_pos.ContractorId],
                            Value = command.ChargesByPos[_pos.Id]
                        };

                    _db.ChargeOperPoses.AddObject(_operPos);
                }

                if (command.BenefitsByPos.ContainsKey(_pos.Id))
                {
                    var _operPos =
                        new BenefitOperPoses
                        {
                            BenefitOpers = _benefitOper,
                            Contractors = command.Contractors[_pos.ContractorId],
                            Services = command.Services[_pos.ServiceId],
                            BenefitRule = (byte)BenefitRuleType.FixedPercent,
                            Value = command.BenefitsByPos[_pos.Id]
                        };

                    _db.BenefitOperPoses.AddObject(_operPos);
                }
            }

            _chargeSet.Quantity++;
            _chargeSet.ValueSum += _chargeOper.Value;

            command.Result = _chargeOper;
        }
    }
}
