using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class ServiceMap : EntityTypeConfiguration<Service>
    {
        public ServiceMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Services");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.ServiceTypeID).HasColumnName("ServiceType");
            Property(t => t.ChargeRule).HasColumnName("ChargeRule");

            // Relationships
            HasRequired(t => t.ServiceType)
                .WithMany(t => t.Services)
                .HasForeignKey(d => d.ServiceTypeID);
        }
    }
}