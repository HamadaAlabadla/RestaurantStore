using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
    /// <inheritdoc />
    public partial class test01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "93cfeda5-f664-4e42-b908-c08d51e407ea", "284b4fd3-ffcb-4c15-acdd-06079aa4af0c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93cfeda5-f664-4e42-b908-c08d51e407ea");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "284b4fd3-ffcb-4c15-acdd-06079aa4af0c");

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93cfeda5-f664-4e42-b908-c08d51e407ea", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logo", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "284b4fd3-ffcb-4c15-acdd-06079aa4af0c", 0, "0ca6c4bf-029c-4d4e-8f56-8ff54d83e34f", new DateTime(2023, 7, 21, 14, 17, 8, 727, DateTimeKind.Utc).AddTicks(1717), "admin@admin.com", false, false, null, "admin - Logo01.jpg", "Hamada", "SUPERADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "203adae2-0b9e-4e75-a224-35e6a2df9559", false, "admin", 2, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "93cfeda5-f664-4e42-b908-c08d51e407ea", "284b4fd3-ffcb-4c15-acdd-06079aa4af0c" });
        }
    }
}
