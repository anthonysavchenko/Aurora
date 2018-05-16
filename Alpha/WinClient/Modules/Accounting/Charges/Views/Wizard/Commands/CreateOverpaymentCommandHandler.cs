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
        private readonly Entities _db;

        public CreateOverpaymentCommandHandler(PaymentDistributionService pds, Entities db)
        {
            _pds = pds;
            _db = db;
        }

        public void Execute(CreateOverpaymentCommand command)
        {
            Dictionary<int, decimal> _total = _db.GetOverpaymentData(command.DbCustomerStub.ID, command.PeriodInfo.LastCharged);

            if (_total.Count > 0)
            {
                OverpaymentCorrectionOpers _overpaymentCorrectionOper =
                    new OverpaymentCorrectionOpers
                    {
                        Period = command.PeriodInfo.LastCharged,
                        Value = -1 * _total.Sum(x => x.Value),
                        ChargeOpers = command.ChargeOper
                    };
                _db.OverpaymentCorrectionOpers.AddObject(_overpaymentCorrectionOper);

                foreach (KeyValuePair<int, decimal> _serviceValue in _total)
                {
                    OverpaymentCorrectionOperPoses _pos =
                        new OverpaymentCorrectionOperPoses
                        {
                            OverpaymentCorrectionOpers = _overpaymentCorrectionOper,
                            Services = command.Services[_serviceValue.Key],
                            Value = -1 * _serviceValue.Value,
                        };
                    _db.OverpaymentCorrectionOperPoses.AddObject(_pos);
                }

                Dictionary<DateTime, Dictionary<int, decimal>> _distribution =
                    _pds.DistributeOverpayment(
                        command.ChargePeriodBalance, 
                        -_overpaymentCorrectionOper.Value, 
                        command.PeriodInfo.FirstUncharged);

                OverpaymentOpers _overpaymentOper =
                    new OverpaymentOpers
                    {
                        CreationDateTime = command.ChargeOper.CreationDateTime,
                        Customers = command.DbCustomerStub,
                        PaymentPeriod = command.PeriodInfo.FirstUncharged,
                        OverpaymentCorrectionOpers = _overpaymentCorrectionOper,
                        Value = -_overpaymentCorrectionOper.Value
                    };
                _db.OverpaymentOpers.AddObject(_overpaymentOper);

                foreach (var _pb in _distribution)
                {
                    foreach (var _sb in _pb.Value)
                    {
                        OverpaymentOperPoses _pos =
                            new OverpaymentOperPoses
                            {
                                OverpaymentOpers = _overpaymentOper,
                                Period = _pb.Key,
                                Services = command.Services[_sb.Key],
                                Value = _sb.Value,
                            };
                        _db.OverpaymentOperPoses.AddObject(_pos);
                    }
                }
            }
        }
    }
}
