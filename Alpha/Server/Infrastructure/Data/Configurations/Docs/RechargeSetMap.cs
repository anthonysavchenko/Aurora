using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RechargeSetMap : EntityTypeConfiguration<RechargeSet>
    {
        public RechargeSetMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Comment)
                .HasMaxLength(256);

            // Table & Column Mappings
            ToTable("RechargeSets");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.Quantity).HasColumnName("Quantity");
            Property(t => t.ValueSum).HasColumnName("ValueSum");
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.Author).HasColumnName("Author");

            // Relationships
            HasRequired(t => t.User)
                .WithMany(t => t.RechargeSets)
                .HasForeignKey(d => d.Author);
        }
    }
}