using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CalculateChargeCommandHandler : ICommandHandler<CalculateChargeCommand>
    {
        public void Execute(CalculateChargeCommand command)
        {
            command.Result = new Dictionary<int, decimal>();

            foreach (var _pos in command.CustomerInfo.Poses)
            {
                decimal _value = 0;

                switch ((ChargeRuleType)_pos.ChargeRule)
                {
                    case ChargeRuleType.SquareRate:
                        _value = _pos.Rate * command.CustomerInfo.Area;
                        break;

                    case ChargeRuleType.ResidentsRate:
                        _value = _pos.Rate * command.CustomerInfo.ResidentsCount;
                        break;

                    case ChargeRuleType.CounterRate:
                        break;

                    case ChargeRuleType.PublicPlaceAreaRate:
                        _value = CalculatePublicPlaceAreaRate(_pos, command.CustomerInfo, command.Cache);
                        break;

                    case ChargeRuleType.PublicPlaceVolumeAreaRate:
                        _value = CalculatePublicPlaceVolumeAreaRate(_pos, command.CustomerInfo, command.Cache);
                        break;

                    case ChargeRuleType.PublicPlaceBankCommission:
                        _value = CalculatePublicPlaceBankCommission(_pos, command.CustomerInfo, command.Cache);
                        break;

                    case ChargeRuleType.FixedRate:
                    default:
                        _value = _pos.Rate;
                        break;
                }

                command.Result.Add(_pos.Id, _value);
            }
        }

        private decimal CalculatePublicPlaceAreaRate(CustomerPosInfo pos, CustomerInfo customer, ICache cache)
        {
            decimal _value = 0;
            decimal _buildingArea = cache.GetBuildingArea(customer.BuildingId);

            if (_buildingArea > 0)
            {
                decimal _ppArea = cache.GetPublicPlaceArea(customer.BuildingId, pos.ServiceId);
                decimal _rate = pos.Norm * _ppArea / _buildingArea * pos.Rate;
                _value = customer.Area * _rate;
            }

            return _value;
        }

        private decimal CalculatePublicPlaceVolumeAreaRate(CustomerPosInfo pos, CustomerInfo customer, ICache cache)
        {
            decimal _value = 0;
            decimal _buildingArea = cache.GetBuildingArea(customer.BuildingId);

            if (_buildingArea > 0)
            {
                decimal _volume = cache.GetPublicPlaceServiceVolume(customer.BuildingId, pos.ServiceId);
                decimal _rate = _volume / _buildingArea * pos.Rate;
                _value = customer.Area * _rate;
            }

            return _value;
        }

        private decimal CalculatePublicPlaceBankCommission(CustomerPosInfo pos, CustomerInfo customer, ICache cache)
        {
            decimal _rateSum = customer.Poses
               .Sum(p =>
               {
                   decimal _rate = 0;
                   decimal _buildingArea = cache.GetBuildingArea(customer.BuildingId);

                   if (_buildingArea > 0)
                   {
                       decimal _ppArea = cache.GetPublicPlaceArea(customer.BuildingId, p.ServiceId);
                       _rate = p.Norm * _ppArea / _buildingArea * pos.Rate;
                   }

                   return _rate;
               });

            return _rateSum * pos.Rate / 100 * customer.Area;
        }
    }
}
