using System;
using System.Linq;
using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report.Models;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report.Queries
{
    public static class GetReportDataQuery
    {
        public static PeriodBalance[] GetReportData(this Entities db, DateTime since, DateTime till, int customerId)
        {
            return db.ChargeOperPoses
                .Select(
                    c =>
                    new
                    {
                        CustomerID = c.ChargeOpers.Customers.ID,
                        c.ChargeOpers.ChargeSets.Period,
                        ServiceTypeID = c.Services.ServiceTypes.ID,
                        Charge = c.Value,
                        Recharge = (decimal)0,
                        Benefit = (decimal)0,
                        Payment = (decimal)0,
                        Acts = (decimal)0,
                        PaymentOnCreateDate = (decimal)0,
                        PaymentOnEnterPeriod = (decimal)0
                    })
                .Concat(
                    db.RechargeOperPoses
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.RechargeOpers.Customers.ID,
                                c.RechargeOpers.RechargeSets.Period,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = c.Value,
                                Benefit = (decimal)0,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.ChargeOperPoses
                        .Where(c => c.ChargeOpers.ChargeCorrectionOpers != null)
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.ChargeOpers.Customers.ID,
                                c.ChargeOpers.ChargeCorrectionOpers.Period,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = -1 * c.Value,
                                Benefit = (decimal)0,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.RechargeOperPoses
                        .Where(r => r.RechargeOpers.ChildChargeCorrectionOpers != null)
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.RechargeOpers.Customers.ID,
                                c.RechargeOpers.ChildChargeCorrectionOpers.Period,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = -1 * c.Value,
                                Benefit = (decimal)0,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.BenefitOperPoses
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.BenefitOpers.ChargeOpers.Customers.ID,
                                c.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = (decimal)0,
                                Benefit = c.Value,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.RebenefitOperPoses
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.RebenefitOpers.RechargeOpers.Customers.ID,
                                c.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = (decimal)0,
                                Benefit = c.Value,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.BenefitOperPoses
                        .Where(c => c.BenefitOpers.BenefitCorrectionOpers != null)
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.BenefitOpers.ChargeOpers.Customers.ID,
                                c.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = (decimal)0,
                                Benefit = -1 * c.Value,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.RebenefitOperPoses
                        .Where(r => r.RebenefitOpers.BenefitCorrectionOpers != null)
                        .Select(c =>
                            new
                            {
                                CustomerID = c.RebenefitOpers.RechargeOpers.Customers.ID,
                                c.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = (decimal)0,
                                Benefit = -1 * c.Value,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.PaymentOperPoses
                        .Where(p => p.PaymentOpers.PaymentCorrectionOper == null)
                        .Select(c =>
                            new
                            {
                                CustomerID = c.PaymentOpers.Customers.ID,
                                Period = c.Period,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = (decimal)0,
                                Benefit = (decimal)0,
                                Payment = c.Value,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.PaymentOperPoses
                        .Where(p => p.PaymentOpers.PaymentSets.Intermediaries == null && p.PaymentOpers.PaymentCorrectionOper == null)
                        .Select(c =>
                            new
                            {
                                CustomerID = c.PaymentOpers.Customers.ID,
                                Period = c.PaymentOpers.CreationDateTime,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = (decimal)0,
                                Benefit = (decimal)0,
                                Payment = (decimal)0,
                                Acts = c.Value,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.PaymentOperPoses
                        .Where(p => p.PaymentOpers.PaymentSets.Intermediaries != null && p.PaymentOpers.PaymentCorrectionOper == null)
                        .Select(c =>
                            new
                            {
                                CustomerID = c.PaymentOpers.Customers.ID,
                                Period = c.PaymentOpers.CreationDateTime,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = (decimal)0,
                                Benefit = (decimal)0,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = c.Value,
                                PaymentOnEnterPeriod = (decimal)0
                            }))
                .Concat(
                    db.PaymentOperPoses
                        .Where(p => p.PaymentOpers.PaymentSets.Intermediaries != null && p.PaymentOpers.PaymentCorrectionOper == null)
                        .Select(c =>
                            new
                            {
                                CustomerID = c.PaymentOpers.Customers.ID,
                                Period = c.PaymentOpers.PaymentPeriod,
                                ServiceTypeID = c.Services.ServiceTypes.ID,
                                Charge = (decimal)0,
                                Recharge = (decimal)0,
                                Benefit = (decimal)0,
                                Payment = (decimal)0,
                                Acts = (decimal)0,
                                PaymentOnCreateDate = (decimal)0,
                                PaymentOnEnterPeriod = c.Value
                            }))
                .Where(c => c.CustomerID == customerId && c.Period >= since && c.Period <= till)
                .GroupBy(c =>
                    new
                    {
                        c.Period.Year,
                        c.Period.Month,
                        c.ServiceTypeID
                    })
                .Select(g =>
                    new
                    {
                        g.Key.Year,
                        g.Key.Month,
                        g.Key.ServiceTypeID,
                        Charge = g.Sum(c => c.Charge),
                        Recharge = g.Sum(c => c.Recharge),
                        Benefit = g.Sum(c => c.Benefit),
                        Payment = g.Sum(c => c.Payment),
                        Acts = g.Sum(c => c.Acts),
                        PaymentOnCreateDate = g.Sum(c => c.PaymentOnCreateDate),
                        PaymentOnEnterPeriod = g.Sum(c => c.PaymentOnEnterPeriod),
                    })
                .ToList()
                .Join(
                    db.ServiceTypes
                        .Select(s =>
                            new
                            {
                                s.ID,
                                s.Name
                            })
                        .ToList(),
                    c => c.ServiceTypeID,
                    s => s.ID,
                    (c, s) =>
                    new
                    {
                        c.Year,
                        c.Month,
                        c.ServiceTypeID,
                        ServiceTypeName = s.Name,
                        c.Charge,
                        c.Recharge,
                        c.Benefit,
                        c.Payment,
                        c.Acts,
                        c.PaymentOnCreateDate,
                        c.PaymentOnEnterPeriod
                    })
                .GroupBy(c => new { c.Year, c.Month })
                .Select(g =>
                    new PeriodBalance
                    {
                        Period = new DateTime(g.Key.Year, g.Key.Month, 1),
                        ServiceBalances =
                            new ServiceBalances
                            {
                                Balances = g
                                    .GroupBy(c =>
                                        new
                                        {
                                            c.ServiceTypeID,
                                            c.ServiceTypeName
                                        })
                                        .Select(gs =>
                                            new
                                            {
                                                gs.Key.ServiceTypeID,
                                                gs.Key.ServiceTypeName,
                                                Charge = gs.Sum(c => c.Charge),
                                                Recharge = gs.Sum(c => c.Recharge),
                                                Benefit = gs.Sum(c => c.Benefit),
                                                Payment = gs.Sum(c => c.Payment),
                                                Acts = gs.Sum(c => c.Acts),
                                                PaymentOnCreateDate = gs.Sum(c => c.PaymentOnCreateDate),
                                                PaymentOnEnterPeriod = gs.Sum(c => c.PaymentOnEnterPeriod),
                                            })
                                        .OrderBy(sb => sb.ServiceTypeName)
                                        .ToDictionary(
                                            sb =>
                                            new ServiceBalanceKey
                                            {
                                                ID = sb.ServiceTypeID,
                                                Name = sb.ServiceTypeName
                                            },
                                            sb =>
                                            new Balance
                                            {
                                                Charge = sb.Charge,
                                                Benefit = sb.Benefit,
                                                Recharge = sb.Recharge,
                                                Payable = sb.Charge + sb.Benefit + sb.Recharge,
                                                Payment = sb.Payment,
                                                PaymentOnCreateDate = sb.PaymentOnCreateDate,
                                                PaymentOnEnterPeriod = sb.PaymentOnEnterPeriod,
                                                Act = sb.Acts,
                                                Debt = sb.Charge + sb.Benefit + sb.Recharge + sb.PaymentOnCreateDate + sb.Acts
                                            })
                            }
                    })
                .OrderBy(c => c.Period)
                .ToArray();
        }
    }
}
