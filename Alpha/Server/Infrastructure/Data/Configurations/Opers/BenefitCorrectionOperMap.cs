using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class BenefitCorrectionOperMap : EntityTypeConfiguration<BenefitCorrectionOper>
    {
        public BenefitCorrectionOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("BenefitCorrectionOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ChargeCorrectionOperID).HasColumnName("ChargeCorrectionOper");

            // Relationships
            HasRequired(t => t.ChargeCorrectionOper)
                .WithMany(t => t.BenefitCorrectionOpers)
                .HasForeignKey(d => d.ChargeCorrectionOperID);
        }
    }
}