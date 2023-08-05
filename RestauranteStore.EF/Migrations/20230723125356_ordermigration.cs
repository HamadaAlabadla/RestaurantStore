using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
    /// <inheritdoc />
    public partial class ordermigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41e49b14-be4d-4aba-b264-3752366192f4", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "855a538f-4ca1-4442-af0e-d1b2e769485f", 0, "f3eaa676-9c59-4767-a638-5220202ac0dd", new DateTime(2023, 7, 23, 12, 53, 56, 396, DateTimeKind.Utc).AddTicks(6814), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "9a1421be-8ca4-41ff-be06-ced9fea71227", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "41e49b14-be4d-4aba-b264-3752366192f4", "855a538f-4ca1-4442-af0e-d1b2e769485f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "41e49b14-be4d-4aba-b264-3752366192f4", "855a538f-4ca1-4442-af0e-d1b2e769485f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41e49b14-be4d-4aba-b264-3752366192f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "855a538f-4ca1-4442-af0e-d1b2e769485f");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "OrderItems");

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
    }
}
