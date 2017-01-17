using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RechargeOperPosMap : EntityTypeConfiguration<RechargeOperPos>
    {
        public RechargeOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("RechargeOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.ContractorID).HasColumnName("Contractor");
            Property(t => t.RechargeOperID).HasColumnName("RechargeOper");

            // Relationships
            HasRequired(t => t.Contractor)
                .WithMany(t => t.RechargeOperPoses)
                .HasForeignKey(d => d.ContractorID);
            HasRequired(t => t.RechargeOper)
                .WithMany(t => t.RechargeOperPoses)
                .HasForeignKey(d => d.RechargeOperID);
            HasRequired(t => t.Service)
                .WithMany(t => t.RechargeOperPoses)
                .HasForeignKey(d => d.ServiceID);
        }
    }
}