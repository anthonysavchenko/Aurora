﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.Mapping
{
    public class PaymentCorrectionOperMap : EntityTypeConfiguration<PaymentCorrectionOper>
    {
        public PaymentCorrectionOperMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("PaymentCorrectionOpers");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            Property(t => t.Period).HasColumnName("Period");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.PaymentOperID).HasColumnName("PaymentOper");

            // Relationships
            HasRequired(t => t.PaymentOper)
                .WithMany(t => t.PaymentCorrectionOpers)
                .HasForeignKey(d => d.PaymentOperID);
        }
    }
}