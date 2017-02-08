using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Infrastructure.Data.Configurations.RefBooks
{
    public class PublicPlaceMap : EntityTypeConfiguration<PublicPlace>
    {
        public PublicPlaceMap()
        {
            Property(p => p.Area)
                .IsRequired()
                .HasPrecision(9, 2);
        }
    }
}