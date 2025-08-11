using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class UserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96dd5dc1-a19a-4e10-aacc-a1e898fd5b07");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7e4519c-0ca3-42f9-9259-340db737431d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dd21cd8d-598e-4797-bd34-c8e9a34da20d", null, "Admin", "ADMIN" },
                    { "f8ac7626-d2ba-4d6e-b85a-3a19de1b0fb1", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "bc0d1ccb-8b1c-4512-abc8-0c47e22e8fb1", 0, "6adcef97-a045-4675-b9ae-4943fe83bbe6", "user@user.com", false, false, null, null, "USER", "AQAAAAIAAYagAAAAEP65pWO42CmKOXejioxIetXI9y8aOZcYybdvVi6ee68/LKDYFT1PK6PxeHBhXrFL8Q==", null, false, "0e2d0f2f-cf9d-47da-9eb2-eb5e5d83a5dc", false, "user" },
                    { "d57a8aeb-b852-4f8d-93b1-3075bfa9861c", 0, "808731cf-eee3-47fb-9de0-71c90563c79e", "admin@admin.com", false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEKQzr483b2mRsxMjHUmNU/ZdQQliXaFts7t44jYKa6PotVVe9ioGe7EZgG6VGIxrlg==", null, false, "1a908c6d-f51d-44a2-840e-d070f6324f9f", false, "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd21cd8d-598e-4797-bd34-c8e9a34da20d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8ac7626-d2ba-4d6e-b85a-3a19de1b0fb1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc0d1ccb-8b1c-4512-abc8-0c47e22e8fb1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d57a8aeb-b852-4f8d-93b1-3075bfa9861c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "96dd5dc1-a19a-4e10-aacc-a1e898fd5b07", null, "User", "USER" },
                    { "a7e4519c-0ca3-42f9-9259-340db737431d", null, "Admin", "ADMIN" }
                });
        }
    }
}
