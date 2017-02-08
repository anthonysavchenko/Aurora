using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class PaymentOperPosMap : EntityTypeConfiguration<PaymentOperPos>
    {
        public PaymentOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("PaymentOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.PaymentOperID).HasColumnName("PaymentOper");

            // Relationships
            HasRequired(t => t.PaymentOper)
                .WithMany(t => t.PaymentOperPoses)
                .HasForeignKey(d => d.PaymentOperID);
        }
    }
}