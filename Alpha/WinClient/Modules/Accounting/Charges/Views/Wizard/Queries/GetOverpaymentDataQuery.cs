using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries
{
    public static class GetOverpaymentDataQuery
    {
        public static Dictionary<int, decimal> GetOverpaymentData(this Entities db, int customerId, DateTime period)
        {
            return db.ChargeOperPoses
                .Select(p =>
                    new
                    {
                        CustomerID = p.ChargeOpers.Customers.ID,
                        p.ChargeOpers.ChargeSets.Period,
                        ServiceID = p.Services.ID,
                        p.Value,
                    })
                .Concat(db.RechargeOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.RechargeOpers.Customers.ID,
                            p.RechargeOpers.RechargeSets.Period,
                            ServiceID = p.Services.ID,
                            p.Value,
                        }))
                .Concat(db.ChargeOperPoses
                    .Where(p => p.ChargeOpers.ChargeCorrectionOpers != null)
                    .Select(p =>
                        new
                        {
                            CustomerID = p.ChargeOpers.Customers.ID,
                            p.ChargeOpers.ChargeCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Value = -1 * p.Value,
                        }))
                .Concat(db.RechargeOperPoses
                    .Where(p => p.RechargeOpers.ChildChargeCorrectionOpers != null)
                    .Select(p =>
                        new
                        {
                            CustomerID = p.RechargeOpers.Customers.ID,
                            p.RechargeOpers.ChildChargeCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Value = -1 * p.Value,
                        }))
                .Concat(db.BenefitOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.BenefitOpers.ChargeOpers.Customers.ID,
                            p.BenefitOpers.ChargeOpers.ChargeSets.Period,
                            ServiceID = p.Services.ID,
                            p.Value,
                        }))
                .Concat(db.BenefitOperPoses
                    .Where(p => p.BenefitOpers.BenefitCorrectionOpers != null)
                    .Select(p =>
                        new
                        {
                            CustomerID = p.BenefitOpers.ChargeOpers.Customers.ID,
                            p.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Value = -1 * p.Value,
                        }))
                .Concat(db.RebenefitOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.RebenefitOpers.RechargeOpers.Customers.ID,
                            p.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                            ServiceID = p.Services.ID,
                            p.Value,
                        }))
                .Concat(db.RebenefitOperPoses
                    .Where(p => p.RebenefitOpers.BenefitCorrectionOpers != null)
                    .Select(p =>
                        new
                        {
                            CustomerID = p.RebenefitOpers.RechargeOpers.Customers.ID,
                            p.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            Value = -1 * p.Value,
                        }))
                .Concat(db.PaymentOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.PaymentOpers.Customers.ID,
                            p.Period,
                            ServiceID = p.Services.ID,
                            p.Value,
                        }))
                .Concat(db.PaymentCorrectionOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                            p.PaymentCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            p.Value,
                        }))
                .Concat(db.OverpaymentOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.OverpaymentOpers.Customers.ID,
                            p.Period,
                            ServiceID = p.Services.ID,
                            p.Value,
                        }))
                .Concat(db.OverpaymentCorrectionOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.OverpaymentCorrectionOpers.ChargeOpers.Customers.ID,
                            p.OverpaymentCorrectionOpers.Period,
                            ServiceID = p.Services.ID,
                            p.Value,
                        }))
                .Where(p => p.CustomerID == customerId && p.Period == period)
                .GroupBy(b => b.ServiceID)
                .Select(g =>
                    new
                    {
                        ServiceID = g.Key,
                        Value = g.Sum(b => b.Value),
                    })
                .Where(x => x.Value < 0)
                .ToDictionary(x => x.ServiceID, x => x.Value);
        }
    }
}
