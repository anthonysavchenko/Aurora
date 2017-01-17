using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RegularBillDocCounterPosMap : EntityTypeConfiguration<RegularBillDocCounterPos>
    {
        public RegularBillDocCounterPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("RegularBillDocCounterPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.PrevValue).HasColumnName("PrevValue");
            Property(t => t.CurValue).HasColumnName("CurValue");
            Property(t => t.Consumption).HasColumnName("Consumption");
            Property(t => t.Rate).HasColumnName("Rate");
            Property(t => t.RegularBillDocID).HasColumnName("RegularBillDoc");

            // Relationships
            HasRequired(t => t.RegularBillDoc)
                .WithMany(t => t.RegularBillDocCounterPoses)
                .HasForeignKey(d => d.RegularBillDocID);
        }
    }
}