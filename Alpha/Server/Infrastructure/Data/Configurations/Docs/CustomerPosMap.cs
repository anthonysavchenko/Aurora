using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class CustomerPosMap : EntityTypeConfiguration<CustomerPos>
    {
        public CustomerPosMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("CustomerPoses");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.ServiceID).HasColumnName("Service");
            Property(t => t.ContractorID).HasColumnName("Contractor");
            Property(t => t.Rate).HasColumnName("Rate");
            Property(t => t.CustomerID).HasColumnName("Customer");
            Property(t => t.Since).HasColumnName("Since");
            Property(t => t.Till).HasColumnName("Till");

            // Relationships
            HasRequired(t => t.Contractor).WithMany(t => t.CustomerPoses).HasForeignKey(d => d.ContractorID);
            HasRequired(t => t.Customer).WithMany(t => t.CustomerPoses).HasForeignKey(d => d.CustomerID);
            HasRequired(t => t.Service).WithMany(t => t.CustomerPoses).HasForeignKey(d => d.ServiceID);
        }
    }
}