using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class FixingUserRolesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "95efc24e-09de-4009-8c6c-f38687881adc", null, "Admin", "ADMIN" },
                    { "dfbee655-98bb-4972-bd6f-4e652b4288fd", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1e5b74da-b809-4107-8357-9356e430be48", 0, "baf3eaff-3d61-4a5e-ac95-be950c0b2cec", "admin@admin.com", false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEKFhp9mf+blWO/aPR/y1rIDkgxXQmQQJo4vqu/8p+bxCrwIqCKuQhHd/p6BEbNCfBw==", null, false, "2d83b299-aa6d-4a12-affe-6cde34cb44b9", false, "admin" },
                    { "e08d6124-4742-4e8c-9312-29ac17faca05", 0, "3817c446-a413-45c9-a259-cd1e275e7ac7", "user@user.com", false, false, null, null, "USER", "AQAAAAIAAYagAAAAEKnK18gAQLxVLOoHJT3415F7Gxn6Oc5jUt6frUW8CLX9neZIxmIzaOKQSjqhHKflhQ==", null, false, "e687f0b6-da78-47ee-bbb2-4d67ba10a49b", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "95efc24e-09de-4009-8c6c-f38687881adc", "1e5b74da-b809-4107-8357-9356e430be48" },
                    { "dfbee655-98bb-4972-bd6f-4e652b4288fd", "e08d6124-4742-4e8c-9312-29ac17faca05" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "95efc24e-09de-4009-8c6c-f38687881adc", "1e5b74da-b809-4107-8357-9356e430be48" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dfbee655-98bb-4972-bd6f-4e652b4288fd", "e08d6124-4742-4e8c-9312-29ac17faca05" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95efc24e-09de-4009-8c6c-f38687881adc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfbee655-98bb-4972-bd6f-4e652b4288fd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e5b74da-b809-4107-8357-9356e430be48");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e08d6124-4742-4e8c-9312-29ac17faca05");

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
    }
}
