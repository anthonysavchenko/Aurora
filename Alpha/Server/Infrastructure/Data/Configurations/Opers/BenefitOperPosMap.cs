using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class BenefitOperPosMap : EntityTypeConfiguration<BenefitOperPos>
    {
        public BenefitOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("BenefitOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.BenefitRule).HasColumnName("BenefitRule");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.BenefitOperID).HasColumnName("BenefitOper");
            Property(t => t.ContractorID).HasColumnName("Contractor");

            // Relationships
            HasRequired(t => t.BenefitOper)
                .WithMany(t => t.BenefitOperPoses)
                .HasForeignKey(d => d.BenefitOperID);
            HasRequired(t => t.Contractor)
                .WithMany(t => t.BenefitOperPoses)
                .HasForeignKey(d => d.ContractorID);
            HasRequired(t => t.Service)
                .WithMany(t => t.BenefitOperPoses)
                .HasForeignKey(d => d.ServiceID);
        }
    }
}