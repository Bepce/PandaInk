using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "1c9fd707-775a-4b00-97b8-1dcee91ef74f", null, "User", "USER" },
                    { "8eed670b-1df7-4c6a-b753-7e9fcc01d038", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ca780a46-305d-412c-98f4-437d39d3b731", 0, "78343d94-6934-4c11-b8f0-dfbc5cf476f3", "admin@admin.com", false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEA9BJgF81C3WrHFEQcbMrkxHrnmzP1+xuEbuPO/pAsei9PAMh8YSvXQf4/QB4jDadg==", null, false, "8a77715e-b56e-45ea-8d8b-1652952f8a00", false, "admin" },
                    { "ea3878e3-8a3c-4c56-8042-5fde4ffa1e59", 0, "fd84019d-f932-4afa-be55-d4526955fc8f", "user@user.com", false, false, null, null, "USER", "AQAAAAIAAYagAAAAEJnmkyXc5zo1Lqnb4ZDhGfbTytacqlykX9qaYAK8bf/Cy9+o+ccwPWJ+0i1nv5uzuw==", null, false, "f099a964-ae22-465c-ad89-86c4613a6e34", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Author", "CoverImage", "Description", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("c906d390-baf4-4fae-b80e-7c3b027014ba"), "Eiichiro Oda", "https://upload.wikimedia.org/wikipedia/en/9/90/One_Piece%2C_Volume_61_Cover_%28Japanese%29.jpg", "One Piece is a Japanese manga series written and illustrated by Eiichiro Oda. It follows the adventures of Monkey D. Luffy and his pirate crew in their quest to find the One Piece, the greatest treasure in the world.", "Adventure", new DateTime(1997, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "One Piece" },
                    { new Guid("e8f774b6-f1bf-40bd-9af2-493cf451c5e3"), "Masashi Kishimoto", "https://upload.wikimedia.org/wikipedia/en/9/94/NarutoCoverTankobon1.jpg", "Naruto is a Japanese manga series written and illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki, a young ninja who seeks recognition from his peers and dreams of becoming the Hokage, the leader of his village.", "Fantasy", new DateTime(1999, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Naruto" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8eed670b-1df7-4c6a-b753-7e9fcc01d038", "ca780a46-305d-412c-98f4-437d39d3b731" },
                    { "1c9fd707-775a-4b00-97b8-1dcee91ef74f", "ea3878e3-8a3c-4c56-8042-5fde4ffa1e59" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8eed670b-1df7-4c6a-b753-7e9fcc01d038", "ca780a46-305d-412c-98f4-437d39d3b731" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1c9fd707-775a-4b00-97b8-1dcee91ef74f", "ea3878e3-8a3c-4c56-8042-5fde4ffa1e59" });

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("c906d390-baf4-4fae-b80e-7c3b027014ba"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("e8f774b6-f1bf-40bd-9af2-493cf451c5e3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c9fd707-775a-4b00-97b8-1dcee91ef74f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8eed670b-1df7-4c6a-b753-7e9fcc01d038");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca780a46-305d-412c-98f4-437d39d3b731");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ea3878e3-8a3c-4c56-8042-5fde4ffa1e59");

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
    }
}
