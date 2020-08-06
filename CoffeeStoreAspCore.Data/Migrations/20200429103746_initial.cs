using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeStoreAspCore.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 200, nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CodeText = table.Column<string>(maxLength: 200, nullable: false),
                    DiscountPercent = table.Column<int>(nullable: true),
                    DiscountAmount = table.Column<decimal>(nullable: true),
                    StartDay = table.Column<DateTime>(type: "date", nullable: false),
                    EndDay = table.Column<DateTime>(type: "date", nullable: false),
                    TimesOfUsed = table.Column<int>(nullable: false),
                    AvailableTimes = table.Column<int>(nullable: false, defaultValue: 0),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    UnitPrice = table.Column<int>(nullable: false),
                    IdMenu = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drinks_Menus_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Days = table.Column<DateTime>(type: "date", nullable: false),
                    IdUser = table.Column<Guid>(nullable: false),
                    CalculatedTotalAmount = table.Column<decimal>(nullable: false),
                    ActualTotalAmount = table.Column<decimal>(nullable: false),
                    TotalQuantity = table.Column<int>(nullable: false),
                    IdVoucher = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Vouchers_IdVoucher",
                        column: x => x.IdVoucher,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrinkImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDrink = table.Column<int>(nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    FileSize = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrinkImages_Drinks_IdDrink",
                        column: x => x.IdDrink,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrder = table.Column<int>(nullable: false),
                    IdDrink = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Drinks_IdDrink",
                        column: x => x.IdDrink,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), new Guid("8d04dce2-969a-435d-bba4-df3f325983dc") },
                    { new Guid("caee66a1-a754-43a6-ab8f-b18204179dd1"), new Guid("ffb27ec8-1d44-4dc3-9f0a-4bbeece0765d") },
                    { new Guid("b2f823b5-5565-4ca6-bd95-25ec2b058f8a"), new Guid("0ed83a75-3fc9-4705-9e7e-c8416897b855") }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "Thức uống trái cây", 1 },
                    { 2, "Thức uống đá xay", 1 },
                    { 3, "Trà & Macchiato", 1 },
                    { 4, "Cà Phê", 1 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "5ed15568-2ddb-4ec7-aed0-b4da9daee862", "Administrator role", "Admin", "Admin" },
                    { new Guid("ffb27ec8-1d44-4dc3-9f0a-4bbeece0765d"), "6fe99ed5-c44e-4c52-b224-cd668e075f68", "Customer role", "Customer", "Customer" },
                    { new Guid("0ed83a75-3fc9-4705-9e7e-c8416897b855"), "577d4201-e34d-44e4-b7f8-752c6b6424a1", "Staff role", "Staff", "Staff" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "Quảng Nam", "10c0f890-eeee-4c6d-bc94-ed8c20f8abd3", "admin@gmail.com", false, false, null, "Phạm Huỳnh Mỹ Hạnh", null, null, "AQAAAAEAACcQAAAAEMaETu1RcgjL7t6SGEIug2i2c675kymm66DB/NmM1lUCtN8bCZtFSJugXeuWsZUrfQ==", "0326837276", false, null, 1, false, "Admin" },
                    { new Guid("b2f823b5-5565-4ca6-bd95-25ec2b058f8a"), 0, "Đồng Nai", "be875125-9920-4e05-a93e-fce4a740ddfa", "staff@gmail.com", false, false, null, "Hoàng Tùng Dương", null, null, "AQAAAAEAACcQAAAAEM6WSpXTDkgomPKueKm6dV7WavCDoAkkgGKAoqF0cQOqneaRQP7rQ7E0HiUVdAxqhQ==", "01263606007", false, null, 1, false, "Staff" },
                    { new Guid("caee66a1-a754-43a6-ab8f-b18204179dd1"), 0, "HCM", "0278eb53-844b-4bb6-a967-13eddeec31e4", "MinhTu@gmail.com", false, false, null, "Phạm Minh Tú", null, null, "AQAAAAEAACcQAAAAEPJzfgiYCQ5A9bFavmHTDBPZOHIkq+rE3t4CExoHLCBPW4ejsWzyOrXoRQ0nSUzdlQ==", "01694564469", false, null, 1, false, "MinhTu" }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "IdMenu", "Name", "Status", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, "Sinh tố Việt Quốc", 1, 59000 },
                    { 10, 4, "Latte", 1, 45000 },
                    { 9, 4, "Espresso", 1, 35000 },
                    { 8, 4, "Carameo Macchiato", 1, 55000 },
                    { 7, 4, "Cappuchino", 1, 55000 },
                    { 6, 4, "Cafe Sữa", 1, 29000 },
                    { 5, 4, "Cafe đen", 1, 29000 },
                    { 4, 4, "Bạc Xỉu", 1, 29000 },
                    { 3, 4, "Americano", 1, 39000 },
                    { 19, 3, "Trà Oolong vải Như Ý", 1, 45000 },
                    { 18, 3, "Trà Oolong Sen An Nhiên", 1, 45000 },
                    { 17, 3, "Trà Matcha Macchiato", 1, 45000 },
                    { 16, 3, "Trà Matcha Latte", 1, 59000 },
                    { 15, 3, "Trà Gạo Rang Macchiato", 1, 48000 },
                    { 14, 3, "Trà Đen Macchiato", 1, 42000 },
                    { 13, 3, "Trà Đào Cam Sả", 1, 45000 },
                    { 22, 2, "Matcha Đá xay", 1, 59000 },
                    { 21, 2, "Chocolate Đá xay", 1, 59000 },
                    { 20, 2, "Caramel Đá Xay", 1, 59000 },
                    { 2, 1, "Sinh tố Cam Xoài", 1, 59000 },
                    { 11, 4, "Mocha", 1, 49000 },
                    { 12, 4, "Chocolate Đá", 1, 55000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkImages_IdDrink",
                table: "DrinkImages",
                column: "IdDrink",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_IdMenu",
                table: "Drinks",
                column: "IdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_IdDrink",
                table: "OrderDetails",
                column: "IdDrink");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_IdOrder",
                table: "OrderDetails",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdUser",
                table: "Orders",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdVoucher",
                table: "Orders",
                column: "IdVoucher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "DrinkImages");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vouchers");
        }
    }
}
