using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
	/// <inheritdoc />
	public partial class readNotifi : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "c6142978-3c10-41e7-a098-00ddd8582044", "a870b841-b471-49bc-898b-8866ee71a4a4" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "c6142978-3c10-41e7-a098-00ddd8582044");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "a870b841-b471-49bc-898b-8866ee71a4a4");

			migrationBuilder.RenameColumn(
				name: "isReady",
				table: "Notifications",
				newName: "isRead");

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "5288a0d5-e7c1-4860-9c91-c06a20ebd849", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "9be52558-7b0f-47e1-8f1c-b7f0e609fa93", 0, "1540708c-f149-4b35-94a6-3732b0cdbae1", new DateTime(2023, 8, 1, 11, 30, 32, 378, DateTimeKind.Utc).AddTicks(6026), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "f607a977-8049-4c61-9afd-451de2ddd9d6", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "5288a0d5-e7c1-4860-9c91-c06a20ebd849", "9be52558-7b0f-47e1-8f1c-b7f0e609fa93" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "5288a0d5-e7c1-4860-9c91-c06a20ebd849", "9be52558-7b0f-47e1-8f1c-b7f0e609fa93" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "5288a0d5-e7c1-4860-9c91-c06a20ebd849");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "9be52558-7b0f-47e1-8f1c-b7f0e609fa93");

			migrationBuilder.RenameColumn(
				name: "isRead",
				table: "Notifications",
				newName: "isReady");

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "c6142978-3c10-41e7-a098-00ddd8582044", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "a870b841-b471-49bc-898b-8866ee71a4a4", 0, "ec67c73d-ca94-4c6a-b06b-0935d7d8c15f", new DateTime(2023, 7, 31, 12, 22, 34, 993, DateTimeKind.Utc).AddTicks(9900), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "0e298496-0f52-4064-b35c-f94e4f86b6ab", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "c6142978-3c10-41e7-a098-00ddd8582044", "a870b841-b471-49bc-898b-8866ee71a4a4" });
		}
	}
}
