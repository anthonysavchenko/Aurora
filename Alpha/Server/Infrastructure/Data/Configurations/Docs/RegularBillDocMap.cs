using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RegularBillDocMap : EntityTypeConfiguration<RegularBillDoc>
    {
        public RegularBillDocMap()
        {
            // Primary Key
            HasKey(t => t.ID);

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

            // Table & Column Mappings
            ToTable("RegularBillDocs");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CustomerID).HasColumnName("Customer");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.PayBeforeDateTime).HasColumnName("PayBeforeDateTime");
            Property(t => t.Account).HasColumnName("Account");
            Property(t => t.Owner).HasColumnName("Owner");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.Square).HasColumnName("Square");
            Property(t => t.ResidentsCount).HasColumnName("ResidentsCount");
            Property(t => t.OverpaymentValue).HasColumnName("OverpaymentValue");
            Property(t => t.MonthChargeValue).HasColumnName("MonthChargeValue");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.BillSetID).HasColumnName("BillSet");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.EmergencyPhoneNumber).HasColumnName("EmergencyPhoneNumber");

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