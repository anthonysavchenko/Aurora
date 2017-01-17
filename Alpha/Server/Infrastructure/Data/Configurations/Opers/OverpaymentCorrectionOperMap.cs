using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class OverpaymentCorrectionOperMap : EntityTypeConfiguration<OverpaymentCorrectionOper>
    {
        public OverpaymentCorrectionOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("OverpaymentCorrectionOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.ChargeOperID).HasColumnName("ChargeOper");
            Property(t => t.Period).HasColumnName("Period");

            // Relationships
            HasRequired(t => t.ChargeOper)
                .WithMany(t => t.OverpaymentCorrectionOpers)
                .HasForeignKey(d => d.ChargeOperID);
        }
    }
}