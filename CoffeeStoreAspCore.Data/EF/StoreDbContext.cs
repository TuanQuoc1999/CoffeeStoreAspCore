using CoffeeStoreAspCore.Data.Configurations;
using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoffeeStoreAspCore.Data.EF
{
   public  class StoreDBContext : IdentityDbContext<User, Role, Guid>
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options)
        {

        }

        public StoreDBContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var builder = new ConfigurationBuilder()
        //                        .SetBasePath(Directory.GetCurrentDirectory())
        //                        .AddJsonFile("appsettings.json");
        //    var configuration = builder.Build();
        //    optionsBuilder.UseSqlServer(configuration["ConnectionStrings:AppDbConnection"]);
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                */
                optionsBuilder.UseSqlServer("Server=.;Database=coreAppp;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DrinkConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
           
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new VoucherConfiguration());
            modelBuilder.ApplyConfiguration(new DrinkImageConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            modelBuilder.Seed();
        }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<Drink> Drinks { set; get; }
        public DbSet<DrinkImage> DrinkImages { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Order> Order { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Voucher> Vouchers { set; get; }


    }
}

