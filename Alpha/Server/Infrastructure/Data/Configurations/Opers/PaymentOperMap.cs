using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class PaymentOperMap : EntityTypeConfiguration<PaymentOper>
    {
        public PaymentOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("PaymentOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.PaymentPeriod).HasColumnName("PaymentPeriod");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.CustomerID).HasColumnName("Customer");
            Property(t => t.PaymentSetID).HasColumnName("PaymentSet");
            Property(t => t.PaymentCorrectionOperID).HasColumnName("PaymentCorrectionOper");

            // Relationships
            HasRequired(t => t.Customer)
                .WithMany(t => t.PaymentOpers)
                .HasForeignKey(d => d.CustomerID);
            HasOptional(t => t.PaymentCorrectionOper)
                .WithMany(t => t.PaymentOpers)
                .HasForeignKey(d => d.PaymentCorrectionOperID);
            HasRequired(t => t.PaymentSet)
                .WithMany(t => t.PaymentOpers)
                .HasForeignKey(d => d.PaymentSetID);
        }
    }
}