using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class BenefitOperMap : EntityTypeConfiguration<BenefitOper>
    {
        public BenefitOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("BenefitOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ChargeOperID).HasColumnName("ChargeOper");
            Property(t => t.BenefitCorrectionOperID).HasColumnName("BenefitCorrectionOper");

            // Relationships
            HasOptional(t => t.BenefitCorrectionOper)
                .WithMany(t => t.BenefitOpers)
                .HasForeignKey(d => d.BenefitCorrectionOperID);
            HasRequired(t => t.ChargeOper)
                .WithMany(t => t.BenefitOpers)
                .HasForeignKey(d => d.ChargeOperID);
        }
    }
}