using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Infrastructure.Data.Configurations.RefBooks
{
    public class BankDetailMap : EntityTypeConfiguration<BankDetail>
    {
        public BankDetailMap()
        {
            Property(p => p.Account)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20)
                .HasColumnType("char");

            Property(p => p.BIK)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(9)
                .HasColumnType("char");

            Property(p => p.CorrAccount)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar");

            Property(p => p.INN)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("varchar");

            Property(p => p.KPP)
                .IsRequired()
                .HasMaxLength(9)
                .HasColumnType("varchar");

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}