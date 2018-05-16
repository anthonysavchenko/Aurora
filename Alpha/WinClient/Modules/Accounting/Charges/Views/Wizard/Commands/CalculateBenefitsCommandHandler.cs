using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CalculateBenefitsCommandHandler : ICommandHandler<CalculateBenefitsCommand>
    {
        public void Execute(CalculateBenefitsCommand command)
        {
            command.Result = new Dictionary<int, decimal>();

            // decimal benefitNormalSquare = (residentCount == 0 ? 0 : residentCount == 1 ? 33 : residentCount == 2 ? 21 : 18) * federalBenefitResidentsCount;
            // decimal benefitSquare = square < benefitNormalSquare ? square : benefitNormalSquare;
            decimal extraSquare = command.CustomerInfo.Area;// < benefitNormalSquare ? 0 : square - benefitSquare;

            foreach (var _pos in command.CustomerInfo.Poses.Where(p => p.ChargeRule == (byte)ChargeRuleType.SquareRate))
            {
                decimal _federalBenefitValue = 0,
                        _localBenefitValue = 0;

                if (_pos.ServiceTypeCodeNumber == ServiceTypeConstants.MAINTENANCE)
                {
                    _federalBenefitValue = 0;//Math.Round(-1 * rate * benefitSquare / 100 * 50, 2, MidpointRounding.AwayFromZero);
                    _localBenefitValue = Math.Round(-1 * _pos.Rate * extraSquare / 100 * command.CustomerInfo.LocalBenefitCoefficient, 2, MidpointRounding.AwayFromZero);

                    if ((_federalBenefitValue != 0 || _localBenefitValue != 0) && !command.Result.ContainsKey(_pos.Id))
                    {
                        command.Result.Add(_pos.Id, 0);
                    }

                    /*if (_federalBenefitValue != 0)
                    {
                        _result[_pos.Id] += _federalBenefitValue;
                    }*/

                    if (_localBenefitValue != 0)
                    {
                        command.Result[_pos.Id] += _localBenefitValue;
                    }
                }
            }
        }
    }
}
