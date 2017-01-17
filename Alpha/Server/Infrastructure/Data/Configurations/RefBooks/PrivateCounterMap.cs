using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class PrivateCounterMap : EntityTypeConfiguration<PrivateCounter>
    {
        public PrivateCounterMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("PrivateCounters");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.Rate).HasColumnName("Rate");
            Property(t => t.CustomerPosID).HasColumnName("CustomerPos");

            // Relationships
            HasRequired(t => t.CustomerPos)
                .WithMany(t => t.PrivateCounters)
                .HasForeignKey(d => d.CustomerPosID);
        }
    }
}