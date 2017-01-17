using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class PaymentSetMap : EntityTypeConfiguration<PaymentSet>
    {
        public PaymentSetMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Comment)
                .HasMaxLength(256);

            // Table & Column Mappings
            ToTable("PaymentSets");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.IsFile).HasColumnName("IsFile");
            Property(t => t.Quantity).HasColumnName("Quantity");
            Property(t => t.ValueSum).HasColumnName("ValueSum");
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.IntermediaryID).HasColumnName("Intermediary");
            Property(t => t.Author).HasColumnName("Author");
            Property(t => t.PaymentDate).HasColumnName("PaymentDate");

            // Relationships
            HasOptional(t => t.Intermediary)
                .WithMany(t => t.PaymentSets)
                .HasForeignKey(d => d.IntermediaryID);
            HasRequired(t => t.User)
                .WithMany(t => t.PaymentSets)
                .HasForeignKey(d => d.Author);
        }
    }
}