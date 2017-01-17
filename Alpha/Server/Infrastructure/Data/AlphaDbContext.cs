using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Taumis.Alpha.Server.Core.Models.Docs;
using Taumis.Alpha.Server.Core.Models.Mapping;
using Taumis.Alpha.Server.Core.Models.Opers;
using Taumis.Alpha.Server.Core.Models.RefBooks;
using Taumis.Alpha.Server.Infrastructure.Data.Mapping;

namespace Taumis.Alpha.Server.Infrastructure.Data
{
    /// <summary>
    /// Класс доступа к данным в БД
    /// </summary>
    public class AlphaDbContext : DbContext, IAlphaDbContext
    {
        // Таймаут 15 минут
        private const int QUERY_TIMEOUT = 900;

        /// <summary>
        /// Конструктор
        /// </summary>
        public AlphaDbContext()
            : base("DefaultConnection")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = QUERY_TIMEOUT;
        }

        public AlphaDbContext(string connectionString)
            : base(connectionString)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = QUERY_TIMEOUT;
        }

        #region DbSets

        public DbSet<BenefitCorrectionOperPos> BenefitCorrectionOperPoses { get; set; }
        public DbSet<BenefitCorrectionOper> BenefitCorrectionOpers { get; set; }
        public DbSet<BenefitOperPos> BenefitOperPoses { get; set; }
        public DbSet<BenefitOper> BenefitOpers { get; set; }
        public DbSet<BenefitType> BenefitTypes { get; set; }
        public DbSet<BillSet> BillSets { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<ChargeCorrectionOperPos> ChargeCorrectionOperPoses { get; set; }
        public DbSet<ChargeCorrectionOper> ChargeCorrectionOpers { get; set; }
        public DbSet<ChargeOperPos> ChargeOperPoses { get; set; }
        public DbSet<ChargeOper> ChargeOpers { get; set; }
        public DbSet<ChargeSet> ChargeSets { get; set; }
        public DbSet<CommonCounterCoefficient> CommonCounterCoefficients { get; set; }
        public DbSet<CommonCounter> CommonCounters { get; set; }
        public DbSet<CommonCounterValue> CommonCounterValues { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<CustomerPos> CustomerPoses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DebtBillDoc> DebtBillDocs { get; set; }
        public DbSet<Intermediary> Intermediaries { get; set; }
        public DbSet<OverpaymentCorrectionOperPos> OverpaymentCorrectionOperPoses { get; set; }
        public DbSet<OverpaymentCorrectionOper> OverpaymentCorrectionOpers { get; set; }
        public DbSet<OverpaymentOperPos> OverpaymentOperPoses { get; set; }
        public DbSet<OverpaymentOper> OverpaymentOpers { get; set; }
        public DbSet<PaymentCorrectionOperPos> PaymentCorrectionOperPoses { get; set; }
        public DbSet<PaymentCorrectionOper> PaymentCorrectionOpers { get; set; }
        public DbSet<PaymentOperPos> PaymentOperPoses { get; set; }
        public DbSet<PaymentOper> PaymentOpers { get; set; }
        public DbSet<PaymentSet> PaymentSets { get; set; }
        public DbSet<PrivateCounter> PrivateCounters { get; set; }
        public DbSet<PrivateCounterValue> PrivateCounterValues { get; set; }
        public DbSet<RebenefitOperPos> RebenefitOperPoses { get; set; }
        public DbSet<RebenefitOper> RebenefitOpers { get; set; }
        public DbSet<RechargeOperPos> RechargeOperPoses { get; set; }
        public DbSet<RechargeOper> RechargeOpers { get; set; }
        public DbSet<RechargeSet> RechargeSets { get; set; }
        public DbSet<RegularBillDocCounterPos> RegularBillDocCounterPoses { get; set; }
        public DbSet<RegularBillDoc> RegularBillDocs { get; set; }
        public DbSet<RegularBillDocSeviceTypePos> RegularBillDocSeviceTypePoses { get; set; }
        public DbSet<RegularBillDocSharedCounterPos> RegularBillDocSharedCounterPoses { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<TotalBillDocPos> TotalBillDocPoses { get; set; }
        public DbSet<TotalBillDoc> TotalBillDocs { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        #region Overrides of DbContext

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BenefitCorrectionOperPosMap());
            modelBuilder.Configurations.Add(new BenefitCorrectionOperMap());
            modelBuilder.Configurations.Add(new BenefitOperPosMap());
            modelBuilder.Configurations.Add(new BenefitOperMap());
            modelBuilder.Configurations.Add(new BenefitTypeMap());
            modelBuilder.Configurations.Add(new BillSetMap());
            modelBuilder.Configurations.Add(new BuildingMap());
            modelBuilder.Configurations.Add(new ChargeCorrectionOperPosMap());
            modelBuilder.Configurations.Add(new ChargeCorrectionOperMap());
            modelBuilder.Configurations.Add(new ChargeOperPosMap());
            modelBuilder.Configurations.Add(new ChargeOperMap());
            modelBuilder.Configurations.Add(new ChargeSetMap());
            modelBuilder.Configurations.Add(new CommonCounterCoefficientMap());
            modelBuilder.Configurations.Add(new CommonCounterMap());
            modelBuilder.Configurations.Add(new CommonCounterValueMap());
            modelBuilder.Configurations.Add(new ContractorMap());
            modelBuilder.Configurations.Add(new CustomerPosMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DebtBillDocMap());
            modelBuilder.Configurations.Add(new IntermediaryMap());
            modelBuilder.Configurations.Add(new OverpaymentCorrectionOperPosMap());
            modelBuilder.Configurations.Add(new OverpaymentCorrectionOperMap());
            modelBuilder.Configurations.Add(new OverpaymentOperPosMap());
            modelBuilder.Configurations.Add(new OverpaymentOperMap());
            modelBuilder.Configurations.Add(new PaymentCorrectionOperPosMap());
            modelBuilder.Configurations.Add(new PaymentCorrectionOperMap());
            modelBuilder.Configurations.Add(new PaymentOperPosMap());
            modelBuilder.Configurations.Add(new PaymentOperMap());
            modelBuilder.Configurations.Add(new PaymentSetMap());
            modelBuilder.Configurations.Add(new PrivateCounterMap());
            modelBuilder.Configurations.Add(new PrivateCounterValueMap());
            modelBuilder.Configurations.Add(new RebenefitOperPosMap());
            modelBuilder.Configurations.Add(new RebenefitOperMap());
            modelBuilder.Configurations.Add(new RechargeOperPosMap());
            modelBuilder.Configurations.Add(new RechargeOperMap());
            modelBuilder.Configurations.Add(new RechargeSetMap());
            modelBuilder.Configurations.Add(new RegularBillDocCounterPosMap());
            modelBuilder.Configurations.Add(new RegularBillDocMap());
            modelBuilder.Configurations.Add(new RegularBillDocSeviceTypePosMap());
            modelBuilder.Configurations.Add(new RegularBillDocSharedCounterPosMap());
            modelBuilder.Configurations.Add(new ResidentMap());
            modelBuilder.Configurations.Add(new ServiceMap());
            modelBuilder.Configurations.Add(new ServiceTypeMap());
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new StreetMap());
            modelBuilder.Configurations.Add(new TotalBillDocPosMap());
            modelBuilder.Configurations.Add(new TotalBillDocMap());
            modelBuilder.Configurations.Add(new UserMap());
        }

        #endregion
    }
}