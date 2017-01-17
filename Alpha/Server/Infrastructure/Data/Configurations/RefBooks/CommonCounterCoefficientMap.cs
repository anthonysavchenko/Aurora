using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class CommonCounterCoefficientMap : EntityTypeConfiguration<CommonCounterCoefficient>
    {
        public CommonCounterCoefficientMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("CommonCounterCoefficients");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.Coefficient).HasColumnName("Coefficient");
            Property(t => t.CommonCounterID).HasColumnName("CommonCounter");

            // Relationships
            HasRequired(t => t.CommonCounter)
                .WithMany(t => t.CommonCounterCoefficients)
                .HasForeignKey(d => d.CommonCounterID);
        }
    }
}