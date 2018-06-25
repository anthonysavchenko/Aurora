using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateChargeCommandHandler : ICommandHandler<CreateChargeCommand>
    {
        public void Execute(CreateChargeCommand cmd)
        {
            var _chargeOper =
                new ChargeOpers
                {
                    CreationDateTime = cmd.Now,
                    Customers = cmd.DbCustomerStub,
                    ChargeSets = cmd.ChargeSet,
                    Value = cmd.ChargesByPos.Sum(x => x.Value)
                };
            cmd.Db.ChargeOpers.AddObject(_chargeOper);

            BenefitOpers _benefitOper = null;

            if (cmd.BenefitsByPos.Count > 0)
            {
                _benefitOper =
                    new BenefitOpers
                    {
                        ChargeOpers = _chargeOper,
                        Value = cmd.BenefitsByPos.Sum(x => x.Value)
                    };
                cmd.Db.BenefitOpers.AddObject(_benefitOper);
            }

            foreach (var _pos in cmd.CustomerInfo.Poses)
            {
                if (cmd.ChargesByPos.ContainsKey(_pos.Id))
                {
                    var _operPos =
                        new ChargeOperPoses
                        {
                            ChargeOpers = _chargeOper,
                            Services = cmd.Services[_pos.ServiceId],
                            Contractors = cmd.Contractors[_pos.ContractorId],
                            Value = cmd.ChargesByPos[_pos.Id]
                        };

                    cmd.Db.ChargeOperPoses.AddObject(_operPos);
                }

                if (cmd.BenefitsByPos.ContainsKey(_pos.Id))
                {
                    var _operPos =
                        new BenefitOperPoses
                        {
                            BenefitOpers = _benefitOper,
                            Contractors = cmd.Contractors[_pos.ContractorId],
                            Services = cmd.Services[_pos.ServiceId],
                            BenefitRule = (byte)BenefitRuleType.FixedPercent,
                            Value = cmd.BenefitsByPos[_pos.Id]
                        };

                    cmd.Db.BenefitOperPoses.AddObject(_operPos);
                }
            }

            cmd.ChargeSet.Quantity++;
            cmd.ChargeSet.ValueSum += _chargeOper.Value;

            cmd.Result = _chargeOper;
        }
    }
}
