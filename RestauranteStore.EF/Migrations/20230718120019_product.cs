using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
	/// <inheritdoc />
	public partial class product : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Warehouses");

			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "f7541243-4a5e-48cf-be81-9bb1798c81a2", "0fe00553-4395-47fc-bf02-c49ed531e024" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "f7541243-4a5e-48cf-be81-9bb1798c81a2");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "0fe00553-4395-47fc-bf02-c49ed531e024");

			migrationBuilder.AddColumn<double>(
				name: "Price",
				table: "Products",
				type: "float",
				nullable: false,
				defaultValue: 0.0);

			migrationBuilder.AddColumn<double>(
				name: "QTY",
				table: "Products",
				type: "float",
				nullable: false,
				defaultValue: 0.0);

			migrationBuilder.AddColumn<float>(
				name: "Rating",
				table: "Products",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<int>(
				name: "Status",
				table: "Products",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "696cbcc4-1f5f-4706-8c87-a221d61c35be", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "3fb5a494-7252-49f8-8992-2aa300f12a50", 0, "17eeadce-476d-4c92-8019-a8c3837b4d86", new DateTime(2023, 7, 18, 12, 0, 19, 328, DateTimeKind.Utc).AddTicks(6346), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "b98ffad1-dc08-453a-bae7-911cb9aed9d0", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "696cbcc4-1f5f-4706-8c87-a221d61c35be", "3fb5a494-7252-49f8-8992-2aa300f12a50" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "696cbcc4-1f5f-4706-8c87-a221d61c35be", "3fb5a494-7252-49f8-8992-2aa300f12a50" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "696cbcc4-1f5f-4706-8c87-a221d61c35be");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "3fb5a494-7252-49f8-8992-2aa300f12a50");

			migrationBuilder.DropColumn(
				name: "Price",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "QTY",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "Rating",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "Status",
				table: "Products");

			migrationBuilder.CreateTable(
				name: "Warehouses",
				columns: table => new
				{
					ProductId = table.Column<int>(type: "int", nullable: false),
					Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Price = table.Column<double>(type: "float", nullable: false),
					Quantity = table.Column<double>(type: "float", nullable: false),
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
				values: new object[] { "f7541243-4a5e-48cf-be81-9bb1798c81a2", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "0fe00553-4395-47fc-bf02-c49ed531e024", 0, "b34df0bf-34b6-45dd-8c83-78bae02fad33", new DateTime(2023, 7, 18, 8, 54, 52, 575, DateTimeKind.Utc).AddTicks(7748), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "da8ed420-907f-4a9a-9df5-eed2e55d4395", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "f7541243-4a5e-48cf-be81-9bb1798c81a2", "0fe00553-4395-47fc-bf02-c49ed531e024" });
		}
	}
}
