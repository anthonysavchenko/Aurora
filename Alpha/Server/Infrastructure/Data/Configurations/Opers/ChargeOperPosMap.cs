using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class ChargeOperPosMap : EntityTypeConfiguration<ChargeOperPos>
    {
        public ChargeOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("ChargeOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.ContractorID).HasColumnName("Contractor");
            Property(t => t.ChargeOperID).HasColumnName("ChargeOper");

            // Relationships
            HasRequired(t => t.ChargeOper)
                .WithMany(t => t.ChargeOperPoses)
                .HasForeignKey(d => d.ChargeOperID);
            HasRequired(t => t.Contractor)
                .WithMany(t => t.ChargeOperPoses)
                .HasForeignKey(d => d.ContractorID);
        }
    }
}