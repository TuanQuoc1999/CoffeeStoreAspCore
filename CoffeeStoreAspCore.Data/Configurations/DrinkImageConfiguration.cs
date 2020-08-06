using CoffeeStoreAspCore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Configurations
{
    public class DrinkImageConfiguration : IEntityTypeConfiguration<DrinkImage>
    {
    
           public void Configure(EntityTypeBuilder<DrinkImage> builder)
        {
            builder.ToTable("DrinkImages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.Caption).HasMaxLength(200);
            builder.HasOne(x => x.Drink).WithOne(x => x.DrinkImage).HasForeignKey<DrinkImage>(x => x.IdDrink);


        }

        
    }
}
