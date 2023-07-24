using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
	/// <inheritdoc />
	public partial class warehouses : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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

			migrationBuilder.CreateTable(
				name: "Warehouses",
				columns: table => new
				{
					ProductId = table.Column<int>(type: "int", nullable: false),
					Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Quantity = table.Column<double>(type: "float", nullable: false),
					Price = table.Column<double>(type: "float", nullable: false),
					isDelete = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Warehouses", x => x.ProductId);
					table.ForeignKey(
						name: "FK_Warehouses_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "ProductNumber",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "e32626c9-c0ca-45bf-aa56-f8d13b875cc3", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "2e93bcb5-1bef-4e8b-8abc-1c5975c79314", 0, "d7d2be73-9960-4cb2-8f99-ced2ba916caf", new DateTime(2023, 7, 18, 8, 30, 17, 1, DateTimeKind.Utc).AddTicks(3498), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "df5fc58f-650d-4acd-aae9-7a5979797ecd", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "e32626c9-c0ca-45bf-aa56-f8d13b875cc3", "2e93bcb5-1bef-4e8b-8abc-1c5975c79314" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Warehouses");

			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "e32626c9-c0ca-45bf-aa56-f8d13b875cc3", "2e93bcb5-1bef-4e8b-8abc-1c5975c79314" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "e32626c9-c0ca-45bf-aa56-f8d13b875cc3");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "2e93bcb5-1bef-4e8b-8abc-1c5975c79314");

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
		}
	}
}
