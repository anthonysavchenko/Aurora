using System;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report.Queries
{
    public static class GetDebtQuery
    {
        public static decimal GetDebt(this Entities db, int customerId, DateTime till)
        {
            decimal _debt = db.ChargeOperPoses
                .Select(
                    c =>
                    new
                    {
                        CustomerID = c.ChargeOpers.Customers.ID,
                        c.ChargeOpers.ChargeSets.Period,
                        c.Value
                    })
                .Concat(
                    db.RechargeOperPoses
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.RechargeOpers.Customers.ID,
                                c.RechargeOpers.RechargeSets.Period,
                                c.Value
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
                                Value = -1 * c.Value,
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
                                Value = -1 * c.Value,
                            }))
                .Concat(
                    db.BenefitOperPoses
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.BenefitOpers.ChargeOpers.Customers.ID,
                                c.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                c.Value,
                            }))
                .Concat(
                    db.RebenefitOperPoses
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.RebenefitOpers.RechargeOpers.Customers.ID,
                                c.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                c.Value,
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
                                Value = -1 * c.Value,
                            }))
                .Concat(
                    db.RebenefitOperPoses
                        .Where(r => r.RebenefitOpers.BenefitCorrectionOpers != null)
                        .Select(c =>
                            new
                            {
                                CustomerID = c.RebenefitOpers.RechargeOpers.Customers.ID,
                                c.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                Value = -1 * c.Value,
                            }))
                .Concat(
                    db.PaymentOperPoses
                        .Select(c =>
                            new
                            {
                                CustomerID = c.PaymentOpers.Customers.ID,
                                c.Period,
                                c.Value,
                            }))
                .Concat(
                    db.PaymentCorrectionOperPoses
                        .Select(c =>
                            new
                            {
                                CustomerID = c.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                                c.PaymentCorrectionOpers.Period,
                                c.Value,
                            }))
                .Where(x => x.CustomerID == customerId && x.Period < till)
                .Sum(x => x.Value);

            return Math.Round(_debt, 2, MidpointRounding.AwayFromZero);
        }
    }
}
