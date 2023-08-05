using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
    /// <inheritdoc />
    public partial class test05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f3aa35fd-3037-4a75-911f-93e18cb59e95", "ab51e373-31a8-43b4-b75b-fc7aebf1ce25" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3aa35fd-3037-4a75-911f-93e18cb59e95");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab51e373-31a8-43b4-b75b-fc7aebf1ce25");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3aa35fd-3037-4a75-911f-93e18cb59e95", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "ab51e373-31a8-43b4-b75b-fc7aebf1ce25", 0, "957ca1a7-f9bd-4474-95d2-a1aebd16d577", new DateTime(2023, 7, 23, 21, 6, 52, 224, DateTimeKind.Utc).AddTicks(2755), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "e40f31df-a39b-4785-b318-fda7dd12e551", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f3aa35fd-3037-4a75-911f-93e18cb59e95", "ab51e373-31a8-43b4-b75b-fc7aebf1ce25" });
        }
    }
}
