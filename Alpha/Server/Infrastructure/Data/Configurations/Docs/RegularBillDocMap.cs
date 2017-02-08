using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RegularBillDocMap : EntityTypeConfiguration<RegularBillDoc>
    {
        public RegularBillDocMap()
        {
            // Properties
            Property(t => t.Account)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Owner)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Square)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.EmergencyPhoneNumber)
                .IsFixedLength()
                .HasMaxLength(10);

            Property(t => t.ContractorContactInfo)
                .HasMaxLength(100);

            // Table & Column Mappings
            Property(t => t.CustomerID).HasColumnName("Customer");
            Property(t => t.BillSetID).HasColumnName("BillSet");

            // Relationships
            HasRequired(t => t.BillSet)
                .WithMany(t => t.RegularBillDocs)
                .HasForeignKey(d => d.BillSetID);
            HasRequired(t => t.Customer)
                .WithMany(t => t.RegularBillDocs)
                .HasForeignKey(d => d.CustomerID);
        }
    }
}