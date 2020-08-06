using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Configurations
{
    class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CodeText).IsRequired().HasMaxLength(200);
            builder.Property(x => x.EndDay).IsRequired();
            builder.Property(x => x.StartDay).IsRequired();
            builder.Property(x => x.TimesOfUsed).IsRequired();
            builder.Property(x => x.AvailableTimes).HasDefaultValue(0);
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
            builder.Property(x => x.EndDay).HasColumnType("date");
            builder.Property(x => x.StartDay).HasColumnType("date");
        }
    }
}
