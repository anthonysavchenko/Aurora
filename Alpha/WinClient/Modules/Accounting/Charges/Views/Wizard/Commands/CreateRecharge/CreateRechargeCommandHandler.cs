using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateRechargeCommandHandler : ICommandHandler<CreateRechargeCommand>
    {
        public void Execute(CreateRechargeCommand cmd)
        {
            RechargeOpers _rechargeOper =
                new RechargeOpers
                {
                    RechargeSets = cmd.RechargeSet,
                    CreationDateTime = cmd.Now,
                    Customers = cmd.DbCustomer,
                    ChargeOpers = cmd.ChargeOper
                };
            cmd.Db.AddToRechargeOpers(_rechargeOper);

            if (cmd.ChargeCorrectionOper != null)
            {
                cmd.ChargeCorrectionOper.ChildRechargeOpers = _rechargeOper;
            }

            RebenefitOpers _rebenefitOper = null;

            if (cmd.BenefitsByPos.Count > 0)
            {
                _rebenefitOper =
                    new RebenefitOpers
                    {
                        RechargeOpers = _rechargeOper
                    };
                cmd.Db.AddToRebenefitOpers(_rebenefitOper);
            }

            foreach (CustomerPosInfo _pos in cmd.CustomerInfo.Poses)
            {
                if(cmd.ChargesByPos.ContainsKey(_pos.Id))
                {
                    RechargeOperPoses _rechargeOperPos = 
                        new RechargeOperPoses
                        {
                            RechargeOpers = _rechargeOper,
                            Services = cmd.Services[_pos.ServiceId],
                            Contractors = cmd.Contractors[_pos.ContractorId],
                            Value = Math.Round(cmd.ChargesByPos[_pos.Id], 2, MidpointRounding.AwayFromZero)
                        };
                    cmd.Db.AddToRechargeOperPoses(_rechargeOperPos);

                    _rechargeOper.Value += _rechargeOperPos.Value;
                }

                if(cmd.BenefitsByPos.ContainsKey(_pos.Id))
                {
                    RebenefitOperPoses _rebenefitOperPos =
                        new RebenefitOperPoses()
                        {
                            RebenefitOpers = _rebenefitOper,
                            Services = cmd.Services[_pos.ServiceId],
                            BenefitRule = (byte)BenefitRuleType.FixedPercent,
                            Contractors = cmd.Contractors[_pos.ContractorId],
                            Value = cmd.BenefitsByPos[_pos.Id]
                        };
                    cmd.Db.AddToRebenefitOperPoses(_rebenefitOperPos);
                    _rebenefitOper.Value += _rebenefitOperPos.Value;
                }
            }

            cmd.RechargeSet.Quantity++;
            cmd.RechargeSet.ValueSum += _rechargeOper.Value;
        }
    }
}
