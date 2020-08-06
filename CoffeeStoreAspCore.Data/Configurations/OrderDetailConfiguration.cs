using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Configurations
{
    class OrderDetailConfiguration: IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Quantity).IsRequired();
            builder.HasOne(x => x.Drink).WithMany(x => x.OrderDetails).HasForeignKey(x => x.IdDrink);
            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.IdOrder);
            
        }
    }
}
