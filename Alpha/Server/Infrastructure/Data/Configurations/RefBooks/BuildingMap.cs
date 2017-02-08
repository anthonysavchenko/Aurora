using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Infrastructure.Data.Mapping
{
    public class BuildingMap : EntityTypeConfiguration<Building>
    {
        public BuildingMap()
        {
            // Properties
            Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ZipCode)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Note)
                .IsRequired();

            Property(t => t.BankDetailID)
                .IsRequired();

            Property(t => t.NonResidentialPlaceArea)
                .IsRequired()
                .HasPrecision(9, 2);

            Property(t => t.StreetID).HasColumnName("Street");

            // Relationships
            HasRequired(t => t.Street)
                .WithMany(t => t.Buildings)
                .HasForeignKey(t => t.StreetID);

            HasRequired(p => p.BankDetail)
                .WithMany(p => p.Buildings)
                .HasForeignKey(p => p.BankDetailID);
        }
    }
}