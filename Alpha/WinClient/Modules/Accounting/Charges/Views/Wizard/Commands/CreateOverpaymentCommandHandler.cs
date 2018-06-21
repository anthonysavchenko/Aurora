using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateOverpaymentCommandHandler : ICommandHandler<CreateOverpaymentCommand>
    {
        private readonly PaymentDistributionService _pds;

        public CreateOverpaymentCommandHandler(PaymentDistributionService pds)
        {
            _pds = pds;
        }

        public void Execute(CreateOverpaymentCommand cmd)
        {
            Dictionary<int, decimal> _total = cmd.Db.GetOverpaymentData(cmd.DbCustomerStub.ID, cmd.LastChargedPeriod);

            if (_total.Count > 0)
            {
                OverpaymentCorrectionOpers _overpaymentCorrectionOper =
                    new OverpaymentCorrectionOpers
                    {
                        Period = cmd.LastChargedPeriod,
                        Value = -1 * _total.Sum(x => x.Value),
                        ChargeOpers = cmd.ChargeOper
                    };
                cmd.Db.OverpaymentCorrectionOpers.AddObject(_overpaymentCorrectionOper);

                foreach (KeyValuePair<int, decimal> _serviceValue in _total)
                {
                    OverpaymentCorrectionOperPoses _pos =
                        new OverpaymentCorrectionOperPoses
                        {
                            OverpaymentCorrectionOpers = _overpaymentCorrectionOper,
                            Services = cmd.Services[_serviceValue.Key],
                            Value = -1 * _serviceValue.Value,
                        };
                    cmd.Db.OverpaymentCorrectionOperPoses.AddObject(_pos);
                }

                Dictionary<DateTime, Dictionary<int, decimal>> _distribution =
                    _pds.DistributeOverpayment(
                        cmd.ChargePeriodBalance, 
                        -_overpaymentCorrectionOper.Value, 
                        cmd.Period);

                OverpaymentOpers _overpaymentOper =
                    new OverpaymentOpers
                    {
                        CreationDateTime = cmd.ChargeOper.CreationDateTime,
                        Customers = cmd.DbCustomerStub,
                        PaymentPeriod = cmd.Period,
                        OverpaymentCorrectionOpers = _overpaymentCorrectionOper,
                        Value = -_overpaymentCorrectionOper.Value
                    };
                cmd.Db.OverpaymentOpers.AddObject(_overpaymentOper);

                foreach (var _pb in _distribution)
                {
                    foreach (var _sb in _pb.Value)
                    {
                        OverpaymentOperPoses _pos =
                            new OverpaymentOperPoses
                            {
                                OverpaymentOpers = _overpaymentOper,
                                Period = _pb.Key,
                                Services = cmd.Services[_sb.Key],
                                Value = _sb.Value,
                            };
                        cmd.Db.OverpaymentOperPoses.AddObject(_pos);
                    }
                }
            }
        }
    }
}
