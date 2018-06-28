using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateChargeCorrectionOperCommandHandler : ICommandHandler<CreateChargeCorrectionOperCommand>
    {
        public void Execute(CreateChargeCorrectionOperCommand cmd)
        {
            if (cmd.ChargeOper == null)
            {
                throw new ArgumentNullException("chargeOper");
            }

            cmd.Result = cmd.ChargeOper.ChargeCorrectionOpers == null
                ? CorrectCharge(cmd.ChargeOper, cmd.Now, cmd.Period, cmd.Services, cmd.Contractors, cmd.Db)
                : CorrectRecharge(cmd.ChargeOper, cmd.Now, cmd.Period, cmd.Services, cmd.Contractors, cmd.Db);
        }

        private ChargeCorrectionOpers CorrectCharge(
            ChargeOpers _chargeOper, 
            DateTime now,
            DateTime period,
            Dictionary<int, DataBase.Services> services,
            Dictionary<int, Contractors> contractors,
            Entities db)
        {
            var _chargeCorrectionOper = 
                new ChargeCorrectionOpers
                {
                    CreationDateTime = now,
                    Period = period,
                    Value = _chargeOper.Value * (-1)
                };
            db.AddToChargeCorrectionOpers(_chargeCorrectionOper);
            _chargeOper.ChargeCorrectionOpers = _chargeCorrectionOper;

            var _chargeOperPoses =
                db.ChargeOperPoses
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
                db.AddToChargeCorrectionOperPoses(_chargeCorrectionPos);
            }

            BenefitOpers _benefitOper =
                db.BenefitOpers
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
                db.AddToBenefitCorrectionOpers(_benefitCorrectionOper);
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
                    db.AddToBenefitCorrectionOperPoses(_benefitCorrectionOperPos);
                }
            }

            return _chargeCorrectionOper;
        }

        private ChargeCorrectionOpers CorrectRecharge(
            ChargeOpers _chargeOper,
            DateTime now,
            DateTime period,
            Dictionary<int, DataBase.Services> services,
            Dictionary<int, Contractors> contractors,
            Entities db)
        {
            RechargeOpers _currentRechargeOper =
                        db.RechargeOpers
                            .FirstOrDefault(r =>
                                r.ChargeOpers.ID == _chargeOper.ID &&
                                r.ChildChargeCorrectionOpers == null);

            ChargeCorrectionOpers _chargeCorrectionOper;

            if (_currentRechargeOper != null)
            {
                _chargeCorrectionOper =
                    new ChargeCorrectionOpers
                    {
                        CreationDateTime = now,
                        Period = period,
                        Value = _currentRechargeOper.Value * (-1)
                    };
                db.AddToChargeCorrectionOpers(_chargeCorrectionOper);
                _currentRechargeOper.ChildChargeCorrectionOpers = _chargeCorrectionOper;

                var _poses =
                    db.RechargeOperPoses
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
                    db.AddToChargeCorrectionOperPoses(_chargeCorrectionPos);
                }

                RebenefitOpers _currentRebenefitOper =
                    db.RebenefitOpers
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
                    db.AddToBenefitCorrectionOpers(_benefitCorrectionOper);
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
                        db.AddToBenefitCorrectionOperPoses(_benefitCorrectionOperPos);
                    }
                }
            }
            else
            {
                RechargeOpers _lastRechargeOper =
                    db.RechargeOpers
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
    }
}
