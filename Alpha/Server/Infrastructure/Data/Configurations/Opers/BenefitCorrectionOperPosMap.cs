using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class BenefitCorrectionOperPosMap : EntityTypeConfiguration<BenefitCorrectionOperPos>
    {
        public BenefitCorrectionOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("BenefitCorrectionOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.BenefitRule).HasColumnName("BenefitRule");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.BenefitCorrectionOperID).HasColumnName("BenefitCorrectionOper");
            Property(t => t.ContractorID).HasColumnName("Contractor");

            // Relationships
            HasRequired(t => t.BenefitCorrectionOper)
                .WithMany(t => t.BenefitCorrectionOperPoses)
                .HasForeignKey(d => d.BenefitCorrectionOperID);
            HasRequired(t => t.Contractor)
                .WithMany(t => t.BenefitCorrectionOperPoses)
                .HasForeignKey(d => d.ContractorID);
            HasRequired(t => t.Service)
                .WithMany(t => t.BenefitCorrectionOperPoses)
                .HasForeignKey(d => d.ServiceID);
        }
    }
}