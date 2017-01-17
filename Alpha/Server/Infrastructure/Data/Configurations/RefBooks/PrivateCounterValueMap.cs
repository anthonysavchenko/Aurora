using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class PrivateCounterValueMap : EntityTypeConfiguration<PrivateCounterValue>
    {
        public PrivateCounterValueMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("PrivateCounterValues");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.PrivateCounterID).HasColumnName("PrivateCounter");

            // Relationships
            HasRequired(t => t.PrivateCounter)
                .WithMany(t => t.PrivateCounterValues)
                .HasForeignKey(d => d.PrivateCounterID);
        }
    }
}