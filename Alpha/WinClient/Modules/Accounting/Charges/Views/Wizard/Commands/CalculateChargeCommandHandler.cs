using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;
using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CalculateChargeCommandHandler : ICommandHandler<CalculateChargeCommand>
    {
        public void Execute(CalculateChargeCommand cmd)
        {
            cmd.Result = new Dictionary<int, decimal>();

            foreach (var _pos in cmd.CustomerInfo.Poses)
            {
                decimal _value = 0;

                switch ((ChargeRuleType)_pos.ChargeRule)
                {
                    case ChargeRuleType.SquareRate:
                        _value = _pos.Rate * cmd.CustomerInfo.Area;
                        break;

                    case ChargeRuleType.ResidentsRate:
                        _value = _pos.Rate * cmd.CustomerInfo.ResidentsCount;
                        break;

                    case ChargeRuleType.CounterRate:
                        break;

                    case ChargeRuleType.PublicPlaceAreaRate:
                        _value = CalculatePublicPlaceAreaRate(_pos, cmd.CustomerInfo, cmd.Cache);
                        break;

                    case ChargeRuleType.PublicPlaceVolumeAreaRate:
                        _value = CalculatePublicPlaceVolumeAreaRate(_pos, cmd.CustomerInfo, cmd.Cache);
                        break;

                    case ChargeRuleType.PublicPlaceBankCommission:
                        _value = CalculatePublicPlaceBankCommission(_pos, cmd.CustomerInfo, cmd.Cache);
                        break;

                    case ChargeRuleType.FixedRate:
                    default:
                        _value = _pos.Rate;
                        break;
                }

                cmd.Result.Add(_pos.Id, Math.Round(_value, 2, MidpointRounding.AwayFromZero));
            }
        }

        private decimal CalculatePublicPlaceAreaRate(CustomerPosInfo pos, CustomerInfo customer, ICache cache)
        {
            decimal _value = 0;
            decimal _buildingArea = cache.GetBuildingArea(customer.BuildingId);

            if (_buildingArea > 0)
            {
                decimal _ppArea = cache.GetPublicPlaceArea(customer.BuildingId, pos.ServiceId);
                decimal _rate = Math.Round(pos.Norm * _ppArea / _buildingArea * pos.Rate, 2, MidpointRounding.AwayFromZero);
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
                decimal _ppArea = cache.GetPublicPlaceArea(customer.BuildingId, pos.ServiceId);
                decimal _normVolume = pos.Norm * _ppArea;
                decimal _counterVolume = cache.GetPublicPlaceServiceVolume(customer.BuildingId, pos.ServiceId);

                decimal _volume = _normVolume > 0 && _counterVolume > _normVolume 
                    ? _normVolume 
                    : _counterVolume;

                decimal _rate = Math.Round(_volume / _buildingArea * pos.Rate, 2, MidpointRounding.AwayFromZero);
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
