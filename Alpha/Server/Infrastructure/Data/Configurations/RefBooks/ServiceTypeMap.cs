using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class ServiceTypeMap : EntityTypeConfiguration<ServiceType>
    {
        public ServiceTypeMap()
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
            ToTable("ServiceTypes");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Code).HasColumnName("Code");
        }
    }
}