using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class CommonCounterValueMap : EntityTypeConfiguration<CommonCounterValue>
    {
        public CommonCounterValueMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("CommonCounterValues");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.CommonCounterID).HasColumnName("CommonCounter");

            // Relationships
            HasRequired(t => t.CommonCounter)
                .WithMany(t => t.CommonCounterValues)
                .HasForeignKey(d => d.CommonCounterID);
        }
    }
}