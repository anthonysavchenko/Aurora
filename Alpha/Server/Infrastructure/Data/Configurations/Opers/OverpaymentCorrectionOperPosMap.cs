using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class OverpaymentCorrectionOperPosMap : EntityTypeConfiguration<OverpaymentCorrectionOperPos>
    {
        public OverpaymentCorrectionOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("OverpaymentCorrectionOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.OverpaymentCorrectionOperID).HasColumnName("OverpaymentCorrectionOper");

            // Relationships
            HasRequired(t => t.OverpaymentCorrectionOper)
                .WithMany(t => t.OverpaymentCorrectionOperPoses)
                .HasForeignKey(d => d.OverpaymentCorrectionOperID);
        }
    }
}