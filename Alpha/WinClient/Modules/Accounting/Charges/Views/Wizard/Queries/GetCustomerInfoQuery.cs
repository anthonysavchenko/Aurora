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
                        Norm = p.Services.Norm ?? 0
                    })
                .ToList();

            return _result;
        }
    }
}
