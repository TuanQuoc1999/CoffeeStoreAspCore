using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Configurations
{
    class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
       
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Status).HasDefaultValue(OrderStatus.InProcess);
            builder.Property(x => x.Days).HasColumnType("date");
            builder.HasOne(t => t.Voucher).WithMany(t => t.Orders).HasForeignKey(t => t.IdVoucher);
            builder.HasOne(t => t.User).WithMany(t => t.Orders).HasForeignKey(t => t.IdUser);
        }
    }
}
