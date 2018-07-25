using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class ApplyPercentCorrectionCommandHandler : ICommandHandler<ApplyPercentCorrectionCommand>
    {
        public void Execute(ApplyPercentCorrectionCommand cmd)
        {
            int _daysInMonth = DateTime.DaysInMonth(cmd.Period.Year, cmd.Period.Month);

            Dictionary<int, RechargePercentCorrections> _rechargePercentCorrDict = 
                cmd.Db.RechargePercentCorrections
                    .Where(rpc => rpc.CustomerPoses.Customers.ID == cmd.CustomerInfo.Id && rpc.Period == cmd.Period)
                    .ToDictionary(rpc => rpc.CustomerPosID);

            if (cmd.ServicePercentCorrection != null)
            {
                List<int> _posIds = cmd.CustomerInfo.Poses
                    .Where(x => x.ServiceId == cmd.ServicePercentCorrection.ServiceId)
                    .Select(x => x.Id)
                    .ToList();

                foreach (int _posId in _posIds)
                {
                    RechargePercentCorrections _rpc;
                    if (_rechargePercentCorrDict.ContainsKey(_posId))
                    {
                        _rpc = _rechargePercentCorrDict[_posId];
                    }
                    else
                    {
                        _rpc =
                            new RechargePercentCorrections
                            {
                                CustomerPosID = _posId,
                                Period = cmd.Period
                            };
                        cmd.Db.RechargePercentCorrections.AddObject(_rpc);
                        _rechargePercentCorrDict.Add(_posId, _rpc);
                    }

                    _rpc.Percent = cmd.ServicePercentCorrection.Percent;
                    _rpc.Days = cmd.ServicePercentCorrection.Days > _daysInMonth ? _daysInMonth : cmd.ServicePercentCorrection.Days;
                }
            }

            foreach (var _pair in _rechargePercentCorrDict)
            {
                if (cmd.ChargesByPos.ContainsKey(_pair.Key))
                {
                    decimal _chargeValue = cmd.ChargesByPos[_pair.Key];

                    RechargePercentCorrections _rpc = _rechargePercentCorrDict[_pair.Key];
                    decimal _value = (_chargeValue / _daysInMonth * _rpc.Days * _rpc.Percent) / 100;
                    cmd.ChargesByPos[_pair.Key] -= Math.Round(_value, 2, MidpointRounding.AwayFromZero);
                }
            }
        }
    }
}
