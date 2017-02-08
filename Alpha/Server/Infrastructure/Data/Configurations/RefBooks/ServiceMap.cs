using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class ServiceMap : EntityTypeConfiguration<Service>
    {
        public ServiceMap()
        {
            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ServiceTypeID).HasColumnName("ServiceType");
            Property(t => t.ChargeRule).IsRequired();
            Property(t => t.Norm).HasPrecision(9, 3);
            Property(t => t.Measure).HasMaxLength(10);

            // Relationships
            HasRequired(t => t.ServiceType)
                .WithMany(t => t.Services)
                .HasForeignKey(d => d.ServiceTypeID);
        }
    }
}