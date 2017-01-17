using System.Data.Entity;
using System.Threading.Tasks;
using Taumis.Alpha.Server.Core.Models.Docs;
using Taumis.Alpha.Server.Core.Models.Opers;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Infrastructure.Data
{
    public interface IAlphaDbContext
    {
        DbSet<BenefitCorrectionOperPos> BenefitCorrectionOperPoses { get; }
        DbSet<BenefitCorrectionOper> BenefitCorrectionOpers { get; }
        DbSet<BenefitOperPos> BenefitOperPoses { get; }
        DbSet<BenefitOper> BenefitOpers { get; }
        DbSet<BenefitType> BenefitTypes { get; }
        DbSet<BillSet> BillSets { get; }
        DbSet<Building> Buildings { get; }
        DbSet<ChargeCorrectionOperPos> ChargeCorrectionOperPoses { get; }
        DbSet<ChargeCorrectionOper> ChargeCorrectionOpers { get; }
        DbSet<ChargeOperPos> ChargeOperPoses { get; }
        DbSet<ChargeOper> ChargeOpers { get; }
        DbSet<ChargeSet> ChargeSets { get; }
        DbSet<CommonCounterCoefficient> CommonCounterCoefficients { get; }
        DbSet<CommonCounter> CommonCounters { get; }
        DbSet<CommonCounterValue> CommonCounterValues { get; }
        DbSet<Contractor> Contractors { get; }
        DbSet<CustomerPos> CustomerPoses { get; }
        DbSet<Customer> Customers { get; }
        DbSet<DebtBillDoc> DebtBillDocs { get; }
        DbSet<Intermediary> Intermediaries { get; }
        DbSet<OverpaymentCorrectionOperPos> OverpaymentCorrectionOperPoses { get; }
        DbSet<OverpaymentCorrectionOper> OverpaymentCorrectionOpers { get; }
        DbSet<OverpaymentOperPos> OverpaymentOperPoses { get; }
        DbSet<OverpaymentOper> OverpaymentOpers { get; }
        DbSet<PaymentCorrectionOperPos> PaymentCorrectionOperPoses { get; }
        DbSet<PaymentCorrectionOper> PaymentCorrectionOpers { get; }
        DbSet<PaymentOperPos> PaymentOperPoses { get; }
        DbSet<PaymentOper> PaymentOpers { get; }
        DbSet<PaymentSet> PaymentSets { get; }
        DbSet<PrivateCounter> PrivateCounters { get; }
        DbSet<PrivateCounterValue> PrivateCounterValues { get; }
        DbSet<RebenefitOperPos> RebenefitOperPoses { get; }
        DbSet<RebenefitOper> RebenefitOpers { get; }
        DbSet<RechargeOperPos> RechargeOperPoses { get; }
        DbSet<RechargeOper> RechargeOpers { get; }
        DbSet<RechargeSet> RechargeSets { get; }
        DbSet<RegularBillDocCounterPos> RegularBillDocCounterPoses { get; }
        DbSet<RegularBillDoc> RegularBillDocs { get; }
        DbSet<RegularBillDocSeviceTypePos> RegularBillDocSeviceTypePoses { get; }
        DbSet<RegularBillDocSharedCounterPos> RegularBillDocSharedCounterPoses { get; }
        DbSet<Resident> Residents { get; }
        DbSet<Service> Services { get; }
        DbSet<ServiceType> ServiceTypes { get; }
        DbSet<Setting> Settings { get; }
        DbSet<Street> Streets { get; }
        DbSet<TotalBillDocPos> TotalBillDocPoses { get; }
        DbSet<TotalBillDoc> TotalBillDocs { get; }
        DbSet<User> Users { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}