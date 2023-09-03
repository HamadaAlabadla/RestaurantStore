using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestauranteStore.EF.Migrations
{
    /// <inheritdoc />
    public partial class testExcel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryExcels",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryExcels", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ProductExcels",
                columns: table => new
                {
                    SKU = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Band = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<int>(type: "int", nullable: false),
                    CategoryExcelCode = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    MinimumDiscount = table.Column<double>(type: "float", nullable: false),
                    DiscountedPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductExcels", x => x.SKU);
                    table.ForeignKey(
                        name: "FK_ProductExcels_CategoryExcels_CategoryExcelCode",
                        column: x => x.CategoryExcelCode,
                        principalTable: "CategoryExcels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryExcels",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "4M", "4MName" },
                    { "TPM", "TPMName" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductExcels_CategoryExcelCode",
                table: "ProductExcels",
                column: "CategoryExcelCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductExcels");

            migrationBuilder.DropTable(
                name: "CategoryExcels");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5bb1850f-61b2-40fc-8925-92d914a4f497", "afc26c50-f215-4617-8647-7aad26c2a1bc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bb1850f-61b2-40fc-8925-92d914a4f497");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "afc26c50-f215-4617-8647-7aad26c2a1bc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3f4b162-90fe-472c-8b29-bfe1e8651827", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "eb2914ba-6996-4ec3-a339-8a2b2db4f3d9", 0, "673a5619-1361-406b-bf23-e9d9d5cf7c53", new DateTime(2023, 8, 2, 8, 5, 47, 491, DateTimeKind.Utc).AddTicks(1137), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "4602b039-6006-4db3-a2eb-ba253cc1bd26", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c3f4b162-90fe-472c-8b29-bfe1e8651827", "eb2914ba-6996-4ec3-a339-8a2b2db4f3d9" });
        }
    }
}
