using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RegularBillDocSeviceTypePosMap : EntityTypeConfiguration<RegularBillDocSeviceTypePos>
    {
        public RegularBillDocSeviceTypePosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ServiceTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("RegularBillDocSeviceTypePoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.RegularBillDocID).HasColumnName("RegularBillDoc");
            Property(t => t.ServiceTypeName).HasColumnName("ServiceTypeName");
            Property(t => t.PayRate).HasColumnName("PayRate");
            Property(t => t.Charge).HasColumnName("Charge");
            Property(t => t.Benefit).HasColumnName("Benefit");
            Property(t => t.Recalculation).HasColumnName("Recalculation");
            Property(t => t.Payable).HasColumnName("Payable");

            // Relationships
            HasRequired(t => t.RegularBillDoc)
                .WithMany(t => t.RegularBillDocSeviceTypePoses)
                .HasForeignKey(d => d.RegularBillDocID);
        }
    }
}