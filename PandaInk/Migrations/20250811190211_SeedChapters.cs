using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedChapters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "ce8af7b6-c851-4054-9de8-40d0fbbba44e", null, "Admin", "ADMIN" },
                    { "d30ba650-2937-4bb6-a380-b20bbeb68b47", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "379d6181-e14e-4e70-9510-a2e7a931dc8f", 0, "58768a7c-afe1-4c96-9ebb-ab80d233ce17", "admin@admin.com", false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEG6WoDLpjS1eVNcdzkaT3peBe/4RbHJ9b77KW6Fwx8ISQbsuxsQ0HZU+Du7sK/q17Q==", null, false, "5a3c6805-6bf4-4fa9-9f76-c025a72a1a4d", false, "admin" },
                    { "b865428c-3252-4330-8f74-4fefedc36891", 0, "2f14c9f1-547f-44f4-a412-62df22e4b854", "user@user.com", false, false, null, null, "USER", "AQAAAAIAAYagAAAAEAuEqr1qSqY1OonMgmQ1mSaBjbBQ8qIBh3hbx/Naurw4f62HTNnrQcakZ/XZ0yKJ4Q==", null, false, "1a516764-7064-4e75-b2e2-73c4ff8d4d8d", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Author", "CoverImage", "Description", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("09d4d445-86ea-45a6-b544-c40f8dbde3a7"), "Masashi Kishimoto", "https://upload.wikimedia.org/wikipedia/en/9/94/NarutoCoverTankobon1.jpg", "Naruto is a Japanese manga series written and illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki, a young ninja who seeks recognition from his peers and dreams of becoming the Hokage, the leader of his village.", "Fantasy", new DateTime(1999, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Naruto" },
                    { new Guid("d49a8e55-aa9a-4561-a43d-082f24e0181e"), "Eiichiro Oda", "https://upload.wikimedia.org/wikipedia/en/9/90/One_Piece%2C_Volume_61_Cover_%28Japanese%29.jpg", "One Piece is a Japanese manga series written and illustrated by Eiichiro Oda. It follows the adventures of Monkey D. Luffy and his pirate crew in their quest to find the One Piece, the greatest treasure in the world.", "Adventure", new DateTime(1997, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "One Piece" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ce8af7b6-c851-4054-9de8-40d0fbbba44e", "379d6181-e14e-4e70-9510-a2e7a931dc8f" },
                    { "d30ba650-2937-4bb6-a380-b20bbeb68b47", "b865428c-3252-4330-8f74-4fefedc36891" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ce8af7b6-c851-4054-9de8-40d0fbbba44e", "379d6181-e14e-4e70-9510-a2e7a931dc8f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d30ba650-2937-4bb6-a380-b20bbeb68b47", "b865428c-3252-4330-8f74-4fefedc36891" });

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("09d4d445-86ea-45a6-b544-c40f8dbde3a7"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("d49a8e55-aa9a-4561-a43d-082f24e0181e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce8af7b6-c851-4054-9de8-40d0fbbba44e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d30ba650-2937-4bb6-a380-b20bbeb68b47");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "379d6181-e14e-4e70-9510-a2e7a931dc8f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b865428c-3252-4330-8f74-4fefedc36891");

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
    }
}
