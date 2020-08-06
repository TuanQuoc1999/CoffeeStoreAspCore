using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Configurations
{
   public  class DrinkConfiguration : IEntityTypeConfiguration<Drink>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Drink> builder)
        {
            builder.ToTable("Drinks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.UnitPrice).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
            builder.HasOne(t => t.Menu).WithMany(t => t.Drinks).HasForeignKey(t => t.IdMenu);
            
        }
    }
}
