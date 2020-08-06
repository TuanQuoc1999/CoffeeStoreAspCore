using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<Menu>().HasData(
                new Menu()
                {
                    Id = 1,
                   Name="Thức uống trái cây",
                    Status = Status.Active,
                },
                 new Menu()
                 {
                     Id = 2,
                     Name = "Thức uống đá xay",
                     Status = Status.Active,
                 },
                    new Menu()
                    {
                        Id = 3,
                        Name = "Trà & Macchiato",
                        Status = Status.Active,
                    }, new Menu()
                    {
                        Id = 4,
                        Name = "Cà Phê",
                        Status = Status.Active,
                    } );

           
            modelBuilder.Entity<Drink>().HasData(
           new Drink()
           {
               Id = 1,
               IdMenu=1,
               Name="Sinh tố Việt Quốc",
               UnitPrice=59000,
               Status = Status.Active

           },
           new Drink()
           {
               Id = 2,
               IdMenu = 1,
               Name = "Sinh tố Cam Xoài",
               UnitPrice = 59000,
               Status = Status.Active
           },
              new Drink()
              {
                  Id = 3,
                  IdMenu = 4,
                  Name = "Americano",
                  UnitPrice = 39000,
                  Status = Status.Active
              },
                new Drink()
                {
                    Id = 4,
                    IdMenu = 4,
                    Name = "Bạc Xỉu",
                    UnitPrice = 29000,
                    Status = Status.Active
                },
                  new Drink()
                  {
                      Id = 5,
                      IdMenu = 4,
                      Name = "Cafe đen",
                      UnitPrice = 29000,
                      Status = Status.Active
                  },
                    new Drink()
                    {
                        Id = 6,
                        IdMenu = 4,
                        Name = "Cafe Sữa",
                        UnitPrice = 29000,
                        Status = Status.Active
                    },
                      new Drink()
                      {
                          Id = 7,
                          IdMenu = 4,
                          Name = "Cappuchino",
                          UnitPrice = 55000,
                          Status = Status.Active
                      },
                      new Drink()
                      {
                          Id = 8,
                          IdMenu = 4,
                          Name = "Carameo Macchiato",
                          UnitPrice = 55000,
                          Status = Status.Active
                      },
                      new Drink()
                      {
                          Id = 9,
                          IdMenu = 4,
                          Name = "Espresso",
                          UnitPrice = 35000,
                          Status = Status.Active
                      },
                      new Drink()
                      {
                          Id = 10,
                          IdMenu = 4,
                          Name = "Latte",
                          UnitPrice = 45000,
                          Status = Status.Active
                      },
                      new Drink()
                      {
                          Id = 11,
                          IdMenu = 4,
                          Name = "Mocha",
                          UnitPrice = 49000,
                          Status = Status.Active
                      },
                      new Drink()
                      {
                          Id = 12,
                          IdMenu = 4,
                          Name = "Chocolate Đá",
                          UnitPrice = 55000,
                          Status = Status.Active
                      },
                      new Drink()
                      {
                          Id = 13,
                          IdMenu = 3,
                          Name = "Trà Đào Cam Sả",
                          UnitPrice = 45000,
                          Status = Status.Active
                      },
                       new Drink()
                       {
                           Id = 14,
                           IdMenu = 3,
                           Name = "Trà Đen Macchiato",
                           UnitPrice = 42000,
                           Status = Status.Active
                       },
                        new Drink()
                        {
                            Id = 15,
                            IdMenu = 3,
                            Name = "Trà Gạo Rang Macchiato",
                            UnitPrice = 48000,
                            Status = Status.Active
                        },
                         new Drink()
                         {
                             Id = 16,
                             IdMenu = 3,
                             Name = "Trà Matcha Latte",
                             UnitPrice = 59000,
                             Status = Status.Active
                         },
                          new Drink()
                          {
                              Id = 17,
                              IdMenu = 3,
                              Name = "Trà Matcha Macchiato",
                              UnitPrice = 45000,
                              Status = Status.Active
                          },
                           new Drink()
                           {
                               Id = 18,
                               IdMenu = 3,
                               Name = "Trà Oolong Sen An Nhiên",
                               UnitPrice = 45000,
                               Status = Status.Active
                           },
                            new Drink()
                            {
                                Id = 19,
                                IdMenu = 3,
                                Name = "Trà Oolong vải Như Ý",
                                UnitPrice = 45000,
                                Status = Status.Active
                            },
                             new Drink()
                             {
                                 Id = 20,
                                 IdMenu = 2,
                                 Name = "Caramel Đá Xay",
                                 UnitPrice = 59000,
                                 Status = Status.Active
                             },
                              new Drink()
                              {
                                  Id = 21,
                                  IdMenu = 2,
                                  Name = "Chocolate Đá xay",
                                  UnitPrice = 59000,
                                  Status = Status.Active
                              }
                              ,
                              new Drink()
                              {
                                  Id = 22,
                                  IdMenu = 2,
                                  Name = "Matcha Đá xay",
                                  UnitPrice = 59000,
                                  Status = Status.Active
                              });
          
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "Admin",
                Description = "Administrator role"
            }, new Role
            {
                Id=new Guid("ffb27ec8-1d44-4dc3-9f0a-4bbeece0765d"),
                Name ="Customer",
                NormalizedName = "Customer",
                Description = "Customer role"
            },
            new Role
            {
                Id= new Guid("0ed83a75-3fc9-4705-9e7e-c8416897b855"),
                Name = "Staff",
                NormalizedName = "Staff",
                Description = "Staff role"
            }
            ); ;

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "Admin",
                Email = "admin@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Pham@123"),
                Address="Quảng Nam",
                Name="Phạm Huỳnh Mỹ Hạnh",
                PhoneNumber="0326837276",
               Status=Status.Active

            },
            new User
            {
                Id = new Guid("b2f823b5-5565-4ca6-bd95-25ec2b058f8a"),
                UserName = "Staff",
                Email = "staff@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Pham@123"),
                Address = "Đồng Nai",
                Name = "Hoàng Tùng Dương",
                PhoneNumber = "01263606007",
                Status = Status.Active

            },
              new User
              {
                  Id = new Guid("caee66a1-a754-43a6-ab8f-b18204179dd1"),
                  UserName = "MinhTu",
                  Email = "MinhTu@gmail.com",
                  PasswordHash = hasher.HashPassword(null, "Pham@123"),
                  Address = "HCM",
                  Name = "Phạm Minh Tú",
                  PhoneNumber = "01694564469",
                  Status = Status.Active

              }
            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            },
            new IdentityUserRole<Guid>
            {
                RoleId = new Guid("ffb27ec8-1d44-4dc3-9f0a-4bbeece0765d"),
                UserId = new Guid("caee66a1-a754-43a6-ab8f-b18204179dd1")
            }, new IdentityUserRole<Guid>
            {
                RoleId = new Guid("0ed83a75-3fc9-4705-9e7e-c8416897b855"),
                UserId = new Guid("b2f823b5-5565-4ca6-bd95-25ec2b058f8a")
            }
            );
        }




    }
}
