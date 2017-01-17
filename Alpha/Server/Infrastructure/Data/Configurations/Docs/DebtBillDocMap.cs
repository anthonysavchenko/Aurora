using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Infrastructure.Data.Mapping
{
    public class DebtBillDocMap : EntityTypeConfiguration<DebtBillDoc>
    {
        public DebtBillDocMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            Property(t => t.Account)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Owner)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("DebtBillDocs");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.Account).HasColumnName("Account");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.Owner).HasColumnName("Owner");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.BillSetID).HasColumnName("BillSet");
            Property(t => t.Period).HasColumnName("Period");

            // Relationships
            HasRequired(t => t.BillSet)
                .WithMany(t => t.DebtBillDocs)
                .HasForeignKey(d => d.BillSetID);
        }
    }
}