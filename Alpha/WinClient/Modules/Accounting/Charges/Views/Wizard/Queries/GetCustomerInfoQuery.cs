using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries
{
    public static class GetCustomerInfoQuery
    {
        public static CustomerInfo GetCustomerInfo(this Entities db, int customerId, DateTime period)
        {
            CustomerInfo _result = db.Customers
                .Where(c => c.ID == customerId)
                .Select(c =>
                    new CustomerInfo
                    {
                        Id = c.ID,
                        BuildingId = c.Buildings.ID,
                        Area = c.Square,
                        Account = c.Account,
                        Street = c.Buildings.Streets.Name,
                        Building = c.Buildings.Number,
                        Apartment = c.Apartment,
                        Owner = c.OwnerType == (int)OwnerType.JuridicalPerson
                            ? c.JuridicalPersonFullName
                            : c.PhysicalPersonShortName,
                        ResidentsCount = c.Residents.Count(),
                        FederalBenefitResidentsCount = c.Residents
                            .Count(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule == 0),
                        LocalBenefitCoefficient = c.Residents
                            .Where(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule != 0)
                            .Max(resident => resident.BenefitTypes.FixedPercent) ?? 0,
                    })
                .First();

            _result.Poses = db.CustomerPoses
                .Where(p => p.Customers.ID == customerId && p.Since <= period && p.Till >= period)
                .Select(p =>
                    new CustomerPosInfo
                    {
                        Id = p.ID,
                        Rate = p.Rate,
                        ServiceId = p.Services.ID,
                        ServiceTypeId = p.Services.ServiceTypes.ID,
                        ServiceTypeCode = p.Services.ServiceTypes.Code,
                        ServiceTypeName = p.Services.ServiceTypes.Name,
                        ContractorId = p.Contractors.ID,
                        ChargeRule = p.Services.ChargeRule,
                        Norm = p.Services.Norm ?? 0,
                        CountersVolume = p.Services.ChargeRule == (byte)ChargeRuleType.CounterRate
                            ? GetPrivateCountersVolumes(db, customerId, p.Services.ID, period) : 0
                    })
                .ToList();

            return _result;
        }

        /// <summary>
        /// Вычисляет объем потребления абонента по всем счетчикам одной услуги
        /// 
        /// TODO: Для счетчиков абонента нужно сделать периоды действия (такие же, как есть у CustomerPoses).
        /// А указывание счетчика в CustomerPos нужно убрать, так как по одной услуге может быть несколько счетчиков.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerID">Абонент</param>
        /// <param name="serviceID">Услуга</param>
        /// <param name="period">Месяц начисления</param>
        /// <returns></returns>
        private static decimal GetPrivateCountersVolumes(Entities db, int customerID, int serviceID, DateTime period)
        {
            DateTime prevPeriod = period.AddMonths(-1);

            return db.PrivateCounterValues
                .Where(value =>
                    value.PrivateCounters.Customers.ID == customerID &&
                    value.PrivateCounters.Services.ID == serviceID &&
                    // Возможно, для оптимизации можно добавит загрузку данных .ToList сразу после следующей строки.
                    (value.Period == period || value.Period == prevPeriod))
                .GroupBy(byCounter => byCounter.PrivateCounters.ID)
                .Select(byCounter => new {
                    CurrentValue = byCounter.FirstOrDefault(value => value.Period == period),
                    PrevValue = byCounter.FirstOrDefault(value => value.Period == prevPeriod)
                })
                .Select(valuePair => 
                    (valuePair.CurrentValue != null || valuePair.PrevValue != null) &&
                    valuePair.CurrentValue.Value > valuePair.PrevValue.Value
                        ? valuePair.CurrentValue.Value - valuePair.PrevValue.Value : 0)
                .Sum();
        }
    }
}
