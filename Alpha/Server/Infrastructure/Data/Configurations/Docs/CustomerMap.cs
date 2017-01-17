using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Infrastructure.Data.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Account).IsRequired().HasMaxLength(50);
            Property(t => t.Apartment).IsRequired().HasMaxLength(50);
            Property(t => t.PhysicalPersonFullName).IsRequired().HasMaxLength(50);
            Property(t => t.PhysicalPersonShortName).IsRequired().HasMaxLength(50);
            Property(t => t.JuridicalPersonFullName).IsRequired().HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Customers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Account).HasColumnName("Account");
            Property(t => t.OwnerType).HasColumnName("OwnerType");
            Property(t => t.IsPrivate).HasColumnName("IsPrivate");
            Property(t => t.RoomsCount).HasColumnName("RoomsCount");
            Property(t => t.Apartment).HasColumnName("Apartment");
            Property(t => t.Square).HasColumnName("Square");
            Property(t => t.PhysicalPersonFullName).HasColumnName("PhysicalPersonFullName");
            Property(t => t.PhysicalPersonShortName).HasColumnName("PhysicalPersonShortName");
            Property(t => t.JuridicalPersonFullName).HasColumnName("JuridicalPersonFullName");
            Property(t => t.BuildingID).HasColumnName("Building");
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.Floor).HasColumnName("Floor");
            Property(t => t.LiftPresence).HasColumnName("LiftPresence");
            Property(t => t.RubbishChutePresence).HasColumnName("RubbishChutePresence");
            Property(t => t.BillSendingSubscription).HasColumnName("BillSendingSubscription");
            Property(t => t.UserID).HasColumnName("UserID");

            // Relationships
            HasRequired(t => t.Building)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.BuildingID);

            HasOptional(t => t.User)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.UserID);
        }
    }
}