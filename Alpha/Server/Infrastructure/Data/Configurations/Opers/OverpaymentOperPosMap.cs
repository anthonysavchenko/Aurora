using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class OverpaymentOperPosMap : EntityTypeConfiguration<OverpaymentOperPos>
    {
        public OverpaymentOperPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("OverpaymentOperPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.OverpaymentOperID).HasColumnName("OverpaymentOper");

            // Relationships
            HasRequired(t => t.OverpaymentOper)
                .WithMany(t => t.OverpaymentOperPoses)
                .HasForeignKey(d => d.OverpaymentOperID);
            HasRequired(t => t.Service)
                .WithMany(t => t.OverpaymentOperPoses)
                .HasForeignKey(d => d.ServiceID);
        }
    }
}