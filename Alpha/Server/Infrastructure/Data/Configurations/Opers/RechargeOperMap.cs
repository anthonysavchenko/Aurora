using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class RechargeOperMap : EntityTypeConfiguration<RechargeOper>
    {
        public RechargeOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("RechargeOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.CustomerID).HasColumnName("Customer");
            Property(t => t.RechargeSetID).HasColumnName("RechargeSet");
            Property(t => t.ChargeOperID).HasColumnName("ChargeOper");
            Property(t => t.ChargeCorrectionOperID).HasColumnName("ChargeCorrectionOper");

            // Relationships
            HasOptional(t => t.ChargeCorrectionOper)
                .WithMany(t => t.RechargeOpers)
                .HasForeignKey(d => d.ChargeCorrectionOperID);
            HasOptional(t => t.ChargeOper)
                .WithMany(t => t.RechargeOpers)
                .HasForeignKey(d => d.ChargeOperID);
            HasRequired(t => t.Customer)
                .WithMany(t => t.RechargeOpers)
                .HasForeignKey(d => d.CustomerID);
            HasRequired(t => t.RechargeSet)
                .WithMany(t => t.RechargeOpers)
                .HasForeignKey(d => d.RechargeSetID);
        }
    }
}