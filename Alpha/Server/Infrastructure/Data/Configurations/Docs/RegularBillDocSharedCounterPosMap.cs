using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RegularBillDocSharedCounterPosMap : EntityTypeConfiguration<RegularBillDocSharedCounterPos>
    {
        public RegularBillDocSharedCounterPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("RegularBillDocSharedCounterPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.RegularBillDocID).HasColumnName("RegularBillDoc");
            Property(t => t.SharedCounterValue).HasColumnName("SharedCounterValue");
            Property(t => t.SharedCharge).HasColumnName("SharedCharge");

            // Relationships
            HasRequired(t => t.RegularBillDoc)
                .WithMany(t => t.RegularBillDocSharedCounterPoses)
                .HasForeignKey(d => d.RegularBillDocID);
        }
    }
}