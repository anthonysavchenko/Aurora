using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class ChargeCorrectionOperPosMap : EntityTypeConfiguration<ChargeCorrectionOperPos>
    {
        public ChargeCorrectionOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("ChargeCorrectionOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.ContractorID).HasColumnName("Contractor");
            Property(t => t.ChargeCorrectionOperID).HasColumnName("ChargeCorrectionOper");

            // Relationships
            HasRequired(t => t.ChargeCorrectionOper)
                .WithMany(t => t.ChargeCorrectionOperPoses)
                .HasForeignKey(d => d.ChargeCorrectionOperID);
            HasRequired(t => t.Contractor)
                .WithMany(t => t.ChargeCorrectionOperPoses)
                .HasForeignKey(d => d.ContractorID);
            HasRequired(t => t.Service)
                .WithMany(t => t.ChargeCorrectionOperPoses)
                .HasForeignKey(d => d.ServiceID);
        }
    }
}