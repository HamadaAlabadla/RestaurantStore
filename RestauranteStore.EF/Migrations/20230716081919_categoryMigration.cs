using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestauranteStore.EF.Migrations
{
	/// <inheritdoc />
	public partial class categoryMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "413170d5-9d2b-4522-9b15-1b572cae04d5", "49d65159-7110-49a3-b357-e61d7e2cc9eb" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "413170d5-9d2b-4522-9b15-1b572cae04d5");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "49d65159-7110-49a3-b357-e61d7e2cc9eb");

			migrationBuilder.CreateTable(
				name: "Categories",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					isDelete = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Categories", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Quantities",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					shortenQuantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
					isDelete = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Quantities", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "52fe9dc6-314d-405b-af0b-92083f15b5e5", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "317c9121-3adb-45b2-a585-c9b1d7fec8d1", 0, "674be33c-bece-417e-bba3-6141cd10785e", new DateTime(2023, 7, 16, 8, 19, 18, 906, DateTimeKind.Utc).AddTicks(6231), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "34f32327-f0d9-41b6-9b6c-d73a7857b1cc", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "Categories",
				columns: new[] { "Id", "Name", "isDelete" },
				values: new object[,]
				{
					{ 1, "Meat", false },
					{ 2, "chickens", false },
					{ 3, "Dairy and Produce", false },
					{ 4, "Frozen Fruits and Vegetables", false }
				});

			migrationBuilder.InsertData(
				table: "Quantities",
				columns: new[] { "Id", "Name", "isDelete", "shortenQuantity" },
				values: new object[,]
				{
					{ 1, "Kilogram", false, "KM" },
					{ 2, "Gram", false, "G" },
					{ 3, "Unit", false, "U" }
				});

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "52fe9dc6-314d-405b-af0b-92083f15b5e5", "317c9121-3adb-45b2-a585-c9b1d7fec8d1" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Categories");

			migrationBuilder.DropTable(
				name: "Quantities");

			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "52fe9dc6-314d-405b-af0b-92083f15b5e5", "317c9121-3adb-45b2-a585-c9b1d7fec8d1" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "52fe9dc6-314d-405b-af0b-92083f15b5e5");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "317c9121-3adb-45b2-a585-c9b1d7fec8d1");

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "413170d5-9d2b-4522-9b15-1b572cae04d5", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "49d65159-7110-49a3-b357-e61d7e2cc9eb", 0, "f53c8cc8-2c96-437d-94fb-adb353f473ac", new DateTime(2023, 7, 16, 6, 41, 28, 557, DateTimeKind.Utc).AddTicks(3502), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "2f9a3810-f189-4049-9470-44e5cceee5a1", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "413170d5-9d2b-4522-9b15-1b572cae04d5", "49d65159-7110-49a3-b357-e61d7e2cc9eb" });
		}
	}
}
