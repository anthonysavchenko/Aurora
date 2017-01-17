using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class ResidentMap : EntityTypeConfiguration<Resident>
    {
        public ResidentMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Patronymic)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Surname)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ResidentDocument)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Residents");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.Patronymic).HasColumnName("Patronymic");
            Property(t => t.Surname).HasColumnName("Surname");
            Property(t => t.ResidentDocument).HasColumnName("ResidentDocument");
            Property(t => t.OwnerRelationship).HasColumnName("OwnerRelationship");
            Property(t => t.BenefitTypeID).HasColumnName("BenefitType");
            Property(t => t.CustomerID).HasColumnName("Customer");

            // Relationships
            HasOptional(t => t.BenefitType)
                .WithMany(t => t.Residents)
                .HasForeignKey(d => d.BenefitTypeID);
            HasRequired(t => t.Customer)
                .WithMany(t => t.Residents)
                .HasForeignKey(d => d.CustomerID);
        }
    }
}