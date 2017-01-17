using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class BillSetMap : EntityTypeConfiguration<BillSet>
    {
        public BillSetMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("BillSets");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.BillType).HasColumnName("BillType");
            Property(t => t.Quantity).HasColumnName("Quantity");
            Property(t => t.ValueSum).HasColumnName("ValueSum");
        }
    }
}