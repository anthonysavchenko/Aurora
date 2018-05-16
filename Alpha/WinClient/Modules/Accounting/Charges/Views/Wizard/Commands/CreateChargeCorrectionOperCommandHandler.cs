using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateChargeCorrectionOperCommandHandler : ICommandHandler<CreateChargeCorrectionOperCommand>
    {
        private readonly Entities _db;

        public CreateChargeCorrectionOperCommandHandler(Entities db)
        {
            _db = db;
        }

        public void Execute(CreateChargeCorrectionOperCommand command)
        {
            if (command.ChargeOper == null)
            {
                throw new ArgumentNullException("chargeOper");
            }

            command.Result = command.ChargeOper.ChargeCorrectionOpers == null
                ? CorrectCharge(command.ChargeOper, command.Now, command.CurrentPeriod,command.Services, command.Contractors)
                : CorrectRecharge(command.ChargeOper, command.Now, command.CurrentPeriod, command.Services, command.Contractors);
        }

        private ChargeCorrectionOpers CorrectCharge(
            ChargeOpers _chargeOper, 
            DateTime now,
            DateTime period,
            Dictionary<int, DataBase.Services> services,
            Dictionary<int, Contractors> contractors)
        {
            var _chargeCorrectionOper = CreateChargeCorrectionOper(now, period, _chargeOper.Value * (-1));
            _chargeOper.ChargeCorrectionOpers = _chargeCorrectionOper;

            var _chargeOperPoses =
                _db.ChargeOperPoses
                    .Where(p => p.ChargeOpers.ID == _chargeOper.ID)
                    .Select(p =>
                        new
                        {
                            ServiceId = p.Services.ID,
                            ContractorId = p.Contractors.ID,
                            p.Value
                        })
                    .ToList();

            foreach (var _chargePos in _chargeOperPoses)
            {
                ChargeCorrectionOperPoses _chargeCorrectionPos = new ChargeCorrectionOperPoses()
                {
                    ChargeCorrectionOpers = _chargeCorrectionOper,
                    Services = services[_chargePos.ServiceId],
                    Contractors = contractors[_chargePos.ContractorId],
                    Value = _chargePos.Value * (-1)
                };
                _db.AddToChargeCorrectionOperPoses(_chargeCorrectionPos);
            }

            BenefitOpers _benefitOper =
                _db.BenefitOpers
                    .Include("BenefitOperPoses")
                    .FirstOrDefault(b => b.ChargeOpers.ID == _chargeOper.ID);

            if (_benefitOper != null)
            {
                BenefitCorrectionOpers _benefitCorrectionOper =
                    new BenefitCorrectionOpers
                    {
                        ChargeCorrectionOpers = _chargeCorrectionOper,
                        Value = _benefitOper.Value * (-1)
                    };
                _db.AddToBenefitCorrectionOpers(_benefitCorrectionOper);
                _benefitOper.BenefitCorrectionOpers = _benefitCorrectionOper;

                foreach (var _benefitPos in _benefitOper.BenefitOperPoses)
                {
                    BenefitCorrectionOperPoses _benefitCorrectionOperPos =
                        new BenefitCorrectionOperPoses
                        {
                            BenefitCorrectionOpers = _benefitCorrectionOper,
                            Services = _benefitPos.Services,
                            Contractors = _benefitPos.Contractors,
                            Value = _benefitPos.Value * (-1)
                        };
                    _db.AddToBenefitCorrectionOperPoses(_benefitCorrectionOperPos);
                }
            }

            return _chargeCorrectionOper;
        }

        private ChargeCorrectionOpers CorrectRecharge(
            ChargeOpers _chargeOper,
            DateTime now,
            DateTime period,
            Dictionary<int, DataBase.Services> services,
            Dictionary<int, Contractors> contractors)
        {
            RechargeOpers _currentRechargeOper =
                        _db.RechargeOpers
                            .FirstOrDefault(r =>
                                r.ChargeOpers.ID == _chargeOper.ID &&
                                r.ChildChargeCorrectionOpers == null);

            ChargeCorrectionOpers _chargeCorrectionOper;

            if (_currentRechargeOper != null)
            {
                _chargeCorrectionOper = CreateChargeCorrectionOper(now, period, _currentRechargeOper.Value * (-1));
                _currentRechargeOper.ChildChargeCorrectionOpers = _chargeCorrectionOper;

                var _poses =
                    _db.RechargeOperPoses
                        .Where(p => p.RechargeOpers.ID == _currentRechargeOper.ID)
                        .Select(p =>
                            new
                            {
                                ServiceId = p.Services.ID,
                                ContractorId = p.Contractors.ID,
                                p.Value
                            })
                        .ToList();

                foreach (var _pos in _poses)
                {
                    ChargeCorrectionOperPoses _chargeCorrectionPos = new ChargeCorrectionOperPoses()
                    {
                        ChargeCorrectionOpers = _chargeCorrectionOper,
                        Services = services[_pos.ServiceId],
                        Contractors = contractors[_pos.ContractorId],
                        Value = _pos.Value * (-1)
                    };
                    _db.AddToChargeCorrectionOperPoses(_chargeCorrectionPos);
                }

                RebenefitOpers _currentRebenefitOper =
                    _db.RebenefitOpers
                        .Include("RebenefitOperPoses")
                        .FirstOrDefault(b => b.RechargeOpers.ID == _currentRechargeOper.ID);

                if (_currentRebenefitOper != null)
                {
                    BenefitCorrectionOpers _benefitCorrectionOper =
                        new BenefitCorrectionOpers
                        {
                            ChargeCorrectionOpers = _chargeCorrectionOper,
                            Value = _currentRebenefitOper.Value * (-1)
                        };
                    _db.AddToBenefitCorrectionOpers(_benefitCorrectionOper);
                    _currentRebenefitOper.BenefitCorrectionOpers = _benefitCorrectionOper;

                    foreach (var _benefitPos in _currentRebenefitOper.RebenefitOperPoses)
                    {
                        BenefitCorrectionOperPoses _benefitCorrectionOperPos =
                            new BenefitCorrectionOperPoses
                            {
                                BenefitCorrectionOpers = _benefitCorrectionOper,
                                Services = _benefitPos.Services,
                                Contractors = _benefitPos.Contractors,
                                Value = _benefitPos.Value * (-1)
                            };
                        _db.AddToBenefitCorrectionOperPoses(_benefitCorrectionOperPos);
                    }
                }
            }
            else
            {
                RechargeOpers _lastRechargeOper =
                    _db.RechargeOpers
                        .Include("ChildChargeCorrectionOpers")
                        .Where(r => r.ChargeOpers.ID == _chargeOper.ID)
                        .OrderByDescending(r => r.CreationDateTime)
                        .FirstOrDefault();

                _chargeCorrectionOper =
                    _lastRechargeOper != null
                        ? _lastRechargeOper.ChildChargeCorrectionOpers
                        : _chargeOper.ChargeCorrectionOpers;
            }

            return _chargeCorrectionOper;
        }

        private ChargeCorrectionOpers CreateChargeCorrectionOper(
            DateTime now,
            DateTime period,
            decimal value)
        {
            var _oper =
                new ChargeCorrectionOpers
                {
                    CreationDateTime = now,
                    Period = period,
                    Value = value
                };

            _db.AddToChargeCorrectionOpers(_oper);

            return _oper;
        }
    }
}
