using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class CommonCounterMap : EntityTypeConfiguration<CommonCounter>
    {
        public CommonCounterMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("CommonCounters");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.BuildingID).HasColumnName("Building");
            Property(t => t.ServiceID).HasColumnName("Service");

            // Relationships
            HasRequired(t => t.Building)
                .WithMany(t => t.CommonCounters)
                .HasForeignKey(d => d.BuildingID);
        }
    }
}