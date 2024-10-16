﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class ChargeCorrectionOperMap : EntityTypeConfiguration<ChargeCorrectionOper>
    {
        public ChargeCorrectionOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("ChargeCorrectionOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.RechargeOperID).HasColumnName("RechargeOper");

            // Relationships
            HasOptional(t => t.RechargeOper)
                .WithMany(t => t.ChargeCorrectionOpers)
                .HasForeignKey(d => d.RechargeOperID);
        }
    }
}