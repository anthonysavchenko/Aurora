﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Common;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries
{
    public static class GetCustomerBalancesGroupedByPeriodQuery
    {
        public class ServiceBalance
        {
            public int ServiceId { get; set; }
            public decimal Charge { get; set; }
            public decimal Benefit { get; set; }
            public decimal Recharge { get; set; }
            public decimal Payment { get; set; }
            public decimal Overpayment { get; set; }
            public decimal OverpaymentCorrection { get; set; }
        }

        public class PeriodBalance
        {
            public DateTime Period { get; set; }
            public decimal Total { get; set; }
            public IEnumerable<ServiceBalance> ServiceBalances { get; set; }
        }

        public static Dictionary<DateTime, Dictionary<int, Balance>> GetCustomerBalancesGroupedByPeriod(
            this Entities db, 
            int customerId, 
            Expression<Func<PeriodBalance, bool>> filter)
        {
            return db.ChargeOperPoses
                .Select(p =>
                    new
                    {
                        CustomerID = p.ChargeOpers.Customers.ID,
                        p.ChargeOpers.ChargeSets.Period,
                        ServiceID = p.Services.ID,
                        Charge = p.Value,
                        Benefit = (decimal)0,
                        Recharge = (decimal)0,
                        Payment = (decimal)0,
                        Overpayment = (decimal)0,
                        OverpaymentCorrection = (decimal)0,
                        Total = p.Value,
                    })
                .Concat(db.RechargeOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.RechargeOpers.Customers.ID,
                            p.RechargeOpers.RechargeSets.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = p.Value,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = p.Value,
                        }))
                .Concat(db.ChargeOperPoses
                    .Where(p => p.ChargeOpers.ChargeCorrectionOpers != null)
                    .Select(p =>
                        new
                        {
                            CustomerID = p.ChargeOpers.Customers.ID,
                            p.ChargeOpers.ChargeCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = -1 * p.Value,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = -1 * p.Value,
                        }))
                .Concat(db.RechargeOperPoses
                    .Where(p => p.RechargeOpers.ChildChargeCorrectionOpers != null)
                    .Select(p =>
                        new
                        {
                            CustomerID = p.RechargeOpers.Customers.ID,
                            p.RechargeOpers.ChildChargeCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = -1 * p.Value,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = -1 * p.Value,
                        }))
                .Concat(db.BenefitOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.BenefitOpers.ChargeOpers.Customers.ID,
                            p.BenefitOpers.ChargeOpers.ChargeSets.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = p.Value,
                            Recharge = (decimal)0,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = p.Value,
                        }))
                .Concat(db.BenefitOperPoses
                    .Where(p => p.BenefitOpers.BenefitCorrectionOpers != null)
                    .Select(p =>
                        new
                        {
                            CustomerID = p.BenefitOpers.ChargeOpers.Customers.ID,
                            p.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = -1 * p.Value,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = -1 * p.Value,
                        }))
                .Concat(db.RebenefitOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.RebenefitOpers.RechargeOpers.Customers.ID,
                            p.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = p.Value,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = p.Value,
                        }))
                .Concat(db.RebenefitOperPoses
                    .Where(p => p.RebenefitOpers.BenefitCorrectionOpers != null)
                    .Select(p =>
                        new
                        {
                            CustomerID = p.RebenefitOpers.RechargeOpers.Customers.ID,
                            p.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = -1 * p.Value,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = -1 * p.Value,
                        }))
                .Concat(db.PaymentOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.PaymentOpers.Customers.ID,
                            p.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = (decimal)0,
                            Payment = p.Value,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = p.Value,
                        }))
                .Concat(db.PaymentCorrectionOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                            p.PaymentCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = p.Value,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = (decimal)0,
                            Total = p.Value,
                        }))
                .Concat(db.OverpaymentOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.OverpaymentOpers.Customers.ID,
                            p.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = (decimal)0,
                            Payment = (decimal)0,
                            Overpayment = p.Value,
                            OverpaymentCorrection = (decimal)0,
                            Total = p.Value,
                        }))
                .Concat(db.OverpaymentCorrectionOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.OverpaymentCorrectionOpers.ChargeOpers.Customers.ID,
                            p.OverpaymentCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Charge = (decimal)0,
                            Benefit = (decimal)0,
                            Recharge = (decimal)0,
                            Payment = (decimal)0,
                            Overpayment = (decimal)0,
                            OverpaymentCorrection = p.Value,
                            Total = p.Value,
                        }))
                .Where(p => p.CustomerID == customerId)
                .GroupBy(p => p.Period)
                .Select(groupedByPeriod =>
                    new PeriodBalance
                    {
                        Period = groupedByPeriod.Key,
                        Total = groupedByPeriod.Sum(x => x.Total),
                        ServiceBalances = groupedByPeriod
                            .GroupBy(b => b.ServiceID)
                            .Select(groupedByService =>
                                new ServiceBalance
                                {
                                    ServiceId = groupedByService.Key,
                                    Charge = groupedByService.Sum(b => b.Charge),
                                    Benefit = groupedByService.Sum(b => b.Benefit),
                                    Recharge = groupedByService.Sum(b => b.Recharge),
                                    Payment = groupedByService.Sum(b => b.Payment),
                                    Overpayment = groupedByService.Sum(b => b.Overpayment),
                                    OverpaymentCorrection = groupedByService.Sum(b => b.OverpaymentCorrection),
                                }),
                    })
                .Where(filter)
                .ToDictionary(
                    x => x.Period,
                    x => x.ServiceBalances.ToDictionary(
                        y => y.ServiceId,
                        y =>
                        new Balance
                        {
                            Charge = y.Charge,
                            Benefit = y.Benefit,
                            Recharge = y.Recharge,
                            Payment = y.Payment,
                            Overpayment = y.Overpayment
                        }));
        }
    }
}
