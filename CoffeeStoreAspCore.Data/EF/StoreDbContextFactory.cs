using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoffeeStoreAspCore.Data.EF
{
    public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDBContext>
    {
        public StoreDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDbConnection");

            var optionsBuilder = new DbContextOptionsBuilder<StoreDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new StoreDBContext(optionsBuilder.Options);
        }
    }
}
