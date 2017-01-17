using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class BenefitTypeMap : EntityTypeConfiguration<BenefitType>
    {
        public BenefitTypeMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("BenefitTypes");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.BenefitRule).HasColumnName("BenefitRule");
            Property(t => t.FixedPercent).HasColumnName("FixedPercent");
        }
    }
}