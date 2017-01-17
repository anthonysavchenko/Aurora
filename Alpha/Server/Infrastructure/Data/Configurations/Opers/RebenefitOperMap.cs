using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RebenefitOperMap : EntityTypeConfiguration<RebenefitOper>
    {
        public RebenefitOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("RebenefitOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.RechargeOperID).HasColumnName("RechargeOper");
            Property(t => t.BenefitCorrectionOperID).HasColumnName("BenefitCorrectionOper");

            // Relationships
            HasOptional(t => t.BenefitCorrectionOper)
                .WithMany(t => t.RebenefitOpers)
                .HasForeignKey(d => d.BenefitCorrectionOperID);
            HasRequired(t => t.RechargeOper)
                .WithMany(t => t.RebenefitOpers)
                .HasForeignKey(d => d.RechargeOperID);
        }
    }
}