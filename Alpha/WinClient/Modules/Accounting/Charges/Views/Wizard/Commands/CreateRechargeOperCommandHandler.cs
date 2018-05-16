using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateRechargeOperCommandHandler : ICommandHandler<CreateRechargeOperCommand>
    {
        private readonly Entities _db;

        public CreateRechargeOperCommandHandler(Entities db)
        {
            _db = db;
        }

        public void Execute(CreateRechargeOperCommand command)
        {
            RechargeOpers _rechargeOper =
                new RechargeOpers
                {
                    RechargeSets = command.RechargeSet,
                    CreationDateTime = command.Now,
                    Customers = command.DbCustomer,
                    ChargeOpers = command.ChargeOper
                };
            _db.AddToRechargeOpers(_rechargeOper);

            if (command.ChargeCorrectionOper != null)
            {
                command.ChargeCorrectionOper.ChildRechargeOpers = _rechargeOper;
            }

            RebenefitOpers _rebenefitOper = null;

            if (command.BenefitsByPos.Count > 0)
            {
                _rebenefitOper =
                    new RebenefitOpers
                    {
                        RechargeOpers = _rechargeOper
                    };
                _db.AddToRebenefitOpers(_rebenefitOper);
            }

            foreach (CustomerPosInfo _pos in command.CustomerInfo.Poses)
            {
                if(command.ChargesByPos.ContainsKey(_pos.Id))
                {
                    RechargeOperPoses _rechargeOperPos = 
                        new RechargeOperPoses
                        {
                            RechargeOpers = _rechargeOper,
                            Services = command.Services[_pos.ServiceId],
                            Contractors = command.Contractors[_pos.ContractorId],
                            Value = Math.Round(command.ChargesByPos[_pos.Id], 2, MidpointRounding.AwayFromZero)
                        };
                    _db.AddToRechargeOperPoses(_rechargeOperPos);

                    _rechargeOper.Value += _rechargeOperPos.Value;
                }

                if(command.BenefitsByPos.ContainsKey(_pos.Id))
                {
                    RebenefitOperPoses _rebenefitOperPos =
                        new RebenefitOperPoses()
                        {
                            RebenefitOpers = _rebenefitOper,
                            Services = command.Services[_pos.ServiceId],
                            BenefitRule = (byte)BenefitRuleType.FixedPercent,
                            Contractors = command.Contractors[_pos.ContractorId],
                            Value = command.BenefitsByPos[_pos.Id]
                        };
                    _db.AddToRebenefitOperPoses(_rebenefitOperPos);
                    _rebenefitOper.Value += _rebenefitOperPos.Value;
                }
            }

            command.RechargeSet.Quantity++;
            command.RechargeSet.ValueSum += _rechargeOper.Value;
        }
    }
}
