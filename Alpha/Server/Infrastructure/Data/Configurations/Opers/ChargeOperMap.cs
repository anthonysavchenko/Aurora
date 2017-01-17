using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class ChargeOperMap : EntityTypeConfiguration<ChargeOper>
    {
        public ChargeOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("ChargeOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.CustomerID).HasColumnName("Customer");
            Property(t => t.ChargeSetID).HasColumnName("ChargeSet");
            Property(t => t.ChargeCorrectionOperID).HasColumnName("ChargeCorrectionOper");
            Property(t => t.RegularBillDocID).HasColumnName("RegularBillDoc");

            // Relationships
            HasOptional(t => t.ChargeCorrectionOper)
                .WithMany(t => t.ChargeOpers)
                .HasForeignKey(d => d.ChargeCorrectionOperID);
            HasOptional(t => t.RegularBillDoc)
                .WithMany(t => t.ChargeOpers)
                .HasForeignKey(d => d.RegularBillDocID);
            HasRequired(t => t.ChargeSet)
                .WithMany(t => t.ChargeOpers)
                .HasForeignKey(d => d.ChargeSetID);
            HasRequired(t => t.Customer)
                .WithMany(t => t.ChargeOpers)
                .HasForeignKey(d => d.CustomerID);
        }
    }
}