using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RebenefitOperPosMap : EntityTypeConfiguration<RebenefitOperPos>
    {
        public RebenefitOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("RebenefitOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.BenefitRule).HasColumnName("BenefitRule");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.RebenefitOperID).HasColumnName("RebenefitOper");
            Property(t => t.ContractorID).HasColumnName("Contractor");

            // Relationships
            HasRequired(t => t.Contractor)
                .WithMany(t => t.RebenefitOperPoses)
                .HasForeignKey(d => d.ContractorID);
            HasRequired(t => t.RebenefitOper)
                .WithMany(t => t.RebenefitOperPoses)
                .HasForeignKey(d => d.RebenefitOperID);
            HasRequired(t => t.Service)
                .WithMany(t => t.RebenefitOperPoses)
                .HasForeignKey(d => d.ServiceID);
        }
    }
}