using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Infrastructure.Data.Mapping
{
    public class BuildingMap : EntityTypeConfiguration<Building>
    {
        public BuildingMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ZipCode)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Note)
                .IsRequired();

            // Table & Column Mappings
            ToTable("Buildings");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.StreetID).HasColumnName("Street");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.ZipCode).HasColumnName("ZipCode");
            Property(t => t.FloorCount).HasColumnName("FloorCount");
            Property(t => t.EntranceCount).HasColumnName("EntranceCount");
            Property(t => t.Note).HasColumnName("Note");

            // Relationships
            HasRequired(t => t.Street)
                .WithMany(t => t.Buildings)
                .HasForeignKey(d => d.StreetID);
        }
    }
}