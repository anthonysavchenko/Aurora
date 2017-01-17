using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Login).IsRequired().HasMaxLength(50);
            Property(t => t.Aka).IsRequired().HasMaxLength(50);
            Property(t => t.LockoutEnabled).IsRequired();
            Property(t => t.AccessFailedCount).IsRequired();

            // Table & Column Mappings
            ToTable("Users");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Login).HasColumnName("Login");
            Property(t => t.Password).HasColumnName("Password");
            Property(t => t.Aka).HasColumnName("Aka");
            Property(t => t.SecurityStamp).HasColumnName("SecurityStamp");
            Property(t => t.LockoutEndDateUtc).HasColumnName("LockoutEndDateUtc");
            Property(t => t.LockoutEnabled).HasColumnName("LockoutEnabled");
            Property(t => t.AccessFailedCount).HasColumnName("AccessFailedCount");
        }
    }
}