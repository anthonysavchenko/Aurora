using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class TotalBillDocPosMap : EntityTypeConfiguration<TotalBillDocPos>
    {
        public TotalBillDocPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ServiceTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("TotalBillDocPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TotalBillDocID).HasColumnName("TotalBillDoc");
            Property(t => t.ServiceTypeName).HasColumnName("ServiceTypeName");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.TotalCharged).HasColumnName("TotalCharged");
            Property(t => t.TotalPaid).HasColumnName("TotalPaid");

            // Relationships
            HasRequired(t => t.TotalBillDoc)
                .WithMany(t => t.TotalBillDocPoses)
                .HasForeignKey(d => d.TotalBillDocID);
        }
    }
}