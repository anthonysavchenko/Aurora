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

            foreach (var _posCharge in cmd.ChargesByPos)
            {
                int _posId = _posCharge.Key;
                int _serviceId = cmd.CustomerInfo.Poses.First(x => x.Id == _posId).ServiceId;
                if (cmd.ServicePercentCorrection != null)
                {
                    RechargePercentCorrections _rpc;
                    if (_rechargePercentCorrDict.ContainsKey(_posCharge.Key))
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

                if (_rechargePercentCorrDict.ContainsKey(_posId))
                {
                    RechargePercentCorrections _rpc = _rechargePercentCorrDict[_posId];
                    decimal _value = (_posCharge.Value / _daysInMonth * _rpc.Days * _rpc.Percent) / 100;
                    cmd.ChargesByPos[_posId] = Math.Round(_value, 2, MidpointRounding.AwayFromZero);
                }
            }
        }
    }
}
