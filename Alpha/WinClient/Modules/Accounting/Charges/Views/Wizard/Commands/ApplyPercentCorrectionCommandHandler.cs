using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class ApplyPercentCorrectionCommandHandler : ICommandHandler<ApplyPercentCorrectionCommand>
    {
        private readonly Entities _db;

        public ApplyPercentCorrectionCommandHandler(Entities db)
        {
            _db = db;
        }

        public void Execute(ApplyPercentCorrectionCommand command)
        {
            int _daysInMonth = DateTime.DaysInMonth(command.Period.Year, command.Period.Month);

            Dictionary<int, RechargePercentCorrections> _rechargePercentCorrDict = _db.RechargePercentCorrections
                .Where(rpc => rpc.CustomerPoses.Customers.ID == command.CustomerInfo.Id && rpc.Period == command.Period)
                .ToDictionary(rpc => rpc.CustomerPosID);

            foreach (var _posCharge in command.ChargesByPos)
            {
                int _posId = _posCharge.Key;
                int _serviceId = command.CustomerInfo.Poses[_posId].ServiceId;
                if (command.ServicePercentCorrection != null)
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
                                Period = command.Period
                            };
                        _db.RechargePercentCorrections.AddObject(_rpc);
                        _rechargePercentCorrDict.Add(_posId, _rpc);
                    }

                    _rpc.Percent = command.ServicePercentCorrection.Percent;
                    _rpc.Days = command.ServicePercentCorrection.Days > _daysInMonth ? _daysInMonth : command.ServicePercentCorrection.Days;
                }

                if (_rechargePercentCorrDict.ContainsKey(_posId))
                {
                    RechargePercentCorrections _rpc = _rechargePercentCorrDict[_posId];
                    command.ChargesByPos[_posId] = (_posCharge.Value / _daysInMonth * _rpc.Days * _rpc.Percent) / 100;
                }
            }
        }
    }
}
