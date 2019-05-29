using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CalculateBenefitsCommandHandler : ICommandHandler<CalculateBenefitsCommand>
    {
        public void Execute(CalculateBenefitsCommand cmd)
        {
            cmd.Result = new Dictionary<int, decimal>();

            if (cmd.CustomerInfo.LocalBenefitCoefficient > 0)
            {
                // decimal benefitNormalSquare = (residentCount == 0 ? 0 : residentCount == 1 ? 33 : residentCount == 2 ? 21 : 18) * federalBenefitResidentsCount;
                // decimal benefitSquare = square < benefitNormalSquare ? square : benefitNormalSquare;
                decimal extraSquare = cmd.CustomerInfo.Area;// < benefitNormalSquare ? 0 : square - benefitSquare;

                foreach (var _pos in cmd.CustomerInfo.Poses.Where(p => p.ChargeRule == (byte)ChargeRuleType.SquareRate))
                {
                    decimal _federalBenefitValue = 0;
                    decimal _localBenefitValue = -1 * _pos.Rate * extraSquare / 100 * cmd.CustomerInfo.LocalBenefitCoefficient;

                    if ((_federalBenefitValue != 0 || _localBenefitValue != 0) && !cmd.Result.ContainsKey(_pos.Id))
                    {
                        cmd.Result.Add(_pos.Id, 0);
                    }

                    /*if (_federalBenefitValue != 0)
                    {
                        _result[_pos.Id] += _federalBenefitValue;
                    }*/

                    if (_localBenefitValue != 0)
                    {
                        cmd.Result[_pos.Id] += Math.Round(_localBenefitValue, 2, MidpointRounding.AwayFromZero);
                    }
                }
            }
        }
    }
}
