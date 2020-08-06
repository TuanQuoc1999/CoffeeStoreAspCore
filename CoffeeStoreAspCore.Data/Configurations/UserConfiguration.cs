using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}
