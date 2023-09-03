using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
    /// <inheritdoc />
    public partial class test02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ddbe45f9-18c0-4cda-bdfc-7dc3701df0db", "503fbe0b-581a-4670-b621-48b00480b0ab" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddbe45f9-18c0-4cda-bdfc-7dc3701df0db");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "503fbe0b-581a-4670-b621-48b00480b0ab");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "77643318-d765-4483-9a99-0933de615e04", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "e42a09e6-a320-4dcc-85e0-5dc1b33e4a67", 0, "b77014d8-4513-4c7d-87eb-75afd1d0af8c", new DateTime(2023, 7, 22, 10, 32, 7, 261, DateTimeKind.Utc).AddTicks(21), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "98befa3f-6168-425c-a2ea-9c177139347e", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "77643318-d765-4483-9a99-0933de615e04", "e42a09e6-a320-4dcc-85e0-5dc1b33e4a67" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "77643318-d765-4483-9a99-0933de615e04", "e42a09e6-a320-4dcc-85e0-5dc1b33e4a67" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77643318-d765-4483-9a99-0933de615e04");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e42a09e6-a320-4dcc-85e0-5dc1b33e4a67");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ddbe45f9-18c0-4cda-bdfc-7dc3701df0db", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "503fbe0b-581a-4670-b621-48b00480b0ab", 0, "1242458c-eabb-4261-888a-38de94627726", new DateTime(2023, 7, 22, 6, 54, 21, 992, DateTimeKind.Utc).AddTicks(547), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "37e61003-0fcb-492a-ab16-e0158ab20812", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ddbe45f9-18c0-4cda-bdfc-7dc3701df0db", "503fbe0b-581a-4670-b621-48b00480b0ab" });
        }
    }
}
