using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class PaymentCorrectionOperPosMap : EntityTypeConfiguration<PaymentCorrectionOperPos>
    {
        public PaymentCorrectionOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("PaymentCorrectionOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.PaymentCorrectionOperID).HasColumnName("PaymentCorrectionOper");

            // Relationships
            HasRequired(t => t.PaymentCorrectionOper)
                .WithMany(t => t.PaymentCorrectionOperPoses)
                .HasForeignKey(d => d.PaymentCorrectionOperID);
            HasRequired(t => t.Service)
                .WithMany(t => t.PaymentCorrectionOperPoses)
                .HasForeignKey(d => d.ServiceID);
        }
    }
}