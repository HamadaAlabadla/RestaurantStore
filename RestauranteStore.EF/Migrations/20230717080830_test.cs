using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_unitsPrice",
                table: "unitsPrice");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "40651f05-128b-4650-942f-2896758559fc", "05714b30-cd23-4f3c-b8ac-63d257e25b33" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40651f05-128b-4650-942f-2896758559fc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05714b30-cd23-4f3c-b8ac-63d257e25b33");

            migrationBuilder.RenameTable(
                name: "unitsPrice",
                newName: "UnitsPrice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitsPrice",
                table: "UnitsPrice",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    QuantityUnitId = table.Column<int>(type: "int", nullable: false),
                    UnitPriceId = table.Column<int>(type: "int", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductNumber);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_QuantityUnits_QuantityUnitId",
                        column: x => x.QuantityUnitId,
                        principalTable: "QuantityUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_UnitsPrice_UnitPriceId",
                        column: x => x.UnitPriceId,
                        principalTable: "UnitsPrice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "017799cc-aa57-408f-bb9c-47096c57ccb6", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "693a20c6-ca79-45d3-8295-0f0ad6db24c3", 0, "09e45e93-47fe-4944-86c7-07d679786b17", new DateTime(2023, 7, 17, 8, 8, 30, 343, DateTimeKind.Utc).AddTicks(4913), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "535224c3-39a8-4d70-8cce-5dc26c4c425a", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "017799cc-aa57-408f-bb9c-47096c57ccb6", "693a20c6-ca79-45d3-8295-0f0ad6db24c3" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_QuantityUnitId",
                table: "Products",
                column: "QuantityUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitPriceId",
                table: "Products",
                column: "UnitPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitsPrice",
                table: "UnitsPrice");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "017799cc-aa57-408f-bb9c-47096c57ccb6", "693a20c6-ca79-45d3-8295-0f0ad6db24c3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "017799cc-aa57-408f-bb9c-47096c57ccb6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "693a20c6-ca79-45d3-8295-0f0ad6db24c3");

            migrationBuilder.RenameTable(
                name: "UnitsPrice",
                newName: "unitsPrice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_unitsPrice",
                table: "unitsPrice",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40651f05-128b-4650-942f-2896758559fc", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "05714b30-cd23-4f3c-b8ac-63d257e25b33", 0, "0b1cbe54-86e6-47dc-8137-a007b5799fc2", new DateTime(2023, 7, 16, 10, 2, 7, 495, DateTimeKind.Utc).AddTicks(7044), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "b30509e8-2a4a-48d4-9b60-2fe2f7f884b2", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "40651f05-128b-4650-942f-2896758559fc", "05714b30-cd23-4f3c-b8ac-63d257e25b33" });
        }
    }
}
