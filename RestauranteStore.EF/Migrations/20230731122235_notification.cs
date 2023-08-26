using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
	/// <inheritdoc />
	public partial class notification : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "228e00a6-9361-46ed-8f91-44a3f88ad6a0", "36a7efb8-3172-4fdd-8c0b-d50b0f6b6c54" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "228e00a6-9361-46ed-8f91-44a3f88ad6a0");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "36a7efb8-3172-4fdd-8c0b-d50b0f6b6c54");

			migrationBuilder.CreateTable(
				name: "Notifications",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					OrderId = table.Column<int>(type: "int", nullable: false),
					FromUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ToUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
					isReady = table.Column<bool>(type: "bit", nullable: false),
					DateReady = table.Column<DateTime>(type: "datetime2", nullable: true),
					DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Notifications", x => x.Id);
					table.ForeignKey(
						name: "FK_Notifications_AspNetUsers_FromUserId",
						column: x => x.FromUserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Notifications_Orders_OrderId",
						column: x => x.OrderId,
						principalTable: "Orders",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

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

			migrationBuilder.CreateIndex(
				name: "IX_Notifications_FromUserId",
				table: "Notifications",
				column: "FromUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Notifications_OrderId",
				table: "Notifications",
				column: "OrderId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Notifications");

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

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "228e00a6-9361-46ed-8f91-44a3f88ad6a0", null, "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
				values: new object[] { "36a7efb8-3172-4fdd-8c0b-d50b0f6b6c54", 0, "cf574ba3-0074-4e01-8e3c-e2c634a3c166", new DateTime(2023, 7, 24, 9, 20, 9, 520, DateTimeKind.Utc).AddTicks(7918), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "5b55bbb6-b00d-4ede-bb43-354d6c526036", false, "admin", 2, false });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "228e00a6-9361-46ed-8f91-44a3f88ad6a0", "36a7efb8-3172-4fdd-8c0b-d50b0f6b6c54" });
		}
	}
}
