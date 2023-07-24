using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestauranteStore.EF.Migrations
{
	/// <inheritdoc />
	public partial class unitPriceMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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

			migrationBuilder.CreateTable(
				name: "QuantityUnits",
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
					table.PrimaryKey("PK_QuantityUnits", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "unitsPrice",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ShortenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					isDelete = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_unitsPrice", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "40651f05-128b-4650-942f-2896758559fc", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "05714b30-cd23-4f3c-b8ac-63d257e25b33", 0, "0b1cbe54-86e6-47dc-8137-a007b5799fc2", new DateTime(2023, 7, 16, 10, 2, 7, 495, DateTimeKind.Utc).AddTicks(7044), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "b30509e8-2a4a-48d4-9b60-2fe2f7f884b2", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "QuantityUnits",
				columns: new[] { "Id", "Name", "isDelete", "shortenQuantity" },
				values: new object[,]
				{
					{ 1, "Kilogram", false, "KM" },
					{ 2, "Gram", false, "G" },
					{ 3, "Unit", false, "U" }
				});

			migrationBuilder.InsertData(
				table: "unitsPrice",
				columns: new[] { "Id", "Name", "ShortenName", "isDelete" },
				values: new object[,]
				{
					{ 1, "Dollar", "USD", false },
					{ 2, "Euro", "EUR", false },
					{ 3, "Yen", "JPY", false },
					{ 4, "Shekel ", "ILS", false }
				});

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "40651f05-128b-4650-942f-2896758559fc", "05714b30-cd23-4f3c-b8ac-63d257e25b33" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "QuantityUnits");

			migrationBuilder.DropTable(
				name: "unitsPrice");

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

			migrationBuilder.CreateTable(
				name: "Quantities",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					isDelete = table.Column<bool>(type: "bit", nullable: false),
					shortenQuantity = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
	}
}
