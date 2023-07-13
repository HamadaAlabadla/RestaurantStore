using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteStore.EF.Migrations
{
    /// <inheritdoc />
    public partial class userTypeEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8453ba78-ea96-416d-9617-a743bbf1ed92", "b971db98-2bce-423b-9696-25756e9fd2c4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8453ba78-ea96-416d-9617-a743bbf1ed92");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b971db98-2bce-423b-9696-25756e9fd2c4");

            migrationBuilder.DropColumn(
                name: "AdminType",
                table: "Admins");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreate", "Logo", "UserId" },
                values: new object[] { new DateTime(2023, 7, 13, 14, 26, 32, 613, DateTimeKind.Local).AddTicks(1519), "superadmin - Logo01.jpg", "1da24ff8-f987-4fa9-a17b-49c0dcfd9390" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2013d021-4085-4827-be68-7fce5b6f969a", null, "superadmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "isDelete" },
                values: new object[] { "1da24ff8-f987-4fa9-a17b-49c0dcfd9390", 0, "6e08ff6d-965e-45bc-9f44-55abb8f50b72", "superadmin@admin.com", false, false, null, "SUPERADMIN@ADMIN.COM", "superadmin", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "80abea34-8759-4322-9b1b-70d6418ece94", false, "superadmin", 0, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2013d021-4085-4827-be68-7fce5b6f969a", "1da24ff8-f987-4fa9-a17b-49c0dcfd9390" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2013d021-4085-4827-be68-7fce5b6f969a", "1da24ff8-f987-4fa9-a17b-49c0dcfd9390" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2013d021-4085-4827-be68-7fce5b6f969a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1da24ff8-f987-4fa9-a17b-49c0dcfd9390");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "AdminType",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AdminType", "DateCreate", "Logo", "UserId" },
                values: new object[] { 0, new DateTime(2023, 7, 12, 13, 18, 50, 197, DateTimeKind.Local).AddTicks(3040), "superadmin - Logo01", "b971db98-2bce-423b-9696-25756e9fd2c4" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8453ba78-ea96-416d-9617-a743bbf1ed92", null, "superadmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "isDelete" },
                values: new object[] { "b971db98-2bce-423b-9696-25756e9fd2c4", 0, "b4ca51c5-2743-4a31-ae3c-1c5160ea3267", "superadmin@admin.com", false, false, null, "SUPERADMIN@ADMIN.COM", "superadmin", "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==", "0596549873", false, "a383a3be-5d20-44b6-a18f-d11c82727ff4", false, "superadmin", false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8453ba78-ea96-416d-9617-a743bbf1ed92", "b971db98-2bce-423b-9696-25756e9fd2c4" });
        }
    }
}
