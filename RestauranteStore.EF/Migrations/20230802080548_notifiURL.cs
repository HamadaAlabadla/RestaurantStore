using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
    /// <inheritdoc />
    public partial class notifiURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3f4b162-90fe-472c-8b29-bfe1e8651827", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "eb2914ba-6996-4ec3-a339-8a2b2db4f3d9", 0, "673a5619-1361-406b-bf23-e9d9d5cf7c53", new DateTime(2023, 8, 2, 8, 5, 47, 491, DateTimeKind.Utc).AddTicks(1137), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "4602b039-6006-4db3-a2eb-ba253cc1bd26", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c3f4b162-90fe-472c-8b29-bfe1e8651827", "eb2914ba-6996-4ec3-a339-8a2b2db4f3d9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c3f4b162-90fe-472c-8b29-bfe1e8651827", "eb2914ba-6996-4ec3-a339-8a2b2db4f3d9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3f4b162-90fe-472c-8b29-bfe1e8651827");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb2914ba-6996-4ec3-a339-8a2b2db4f3d9");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Notifications");

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
    }
}
