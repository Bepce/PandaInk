using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class FixNamingAndSeedChapters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ChapterNumbr",
                table: "Chapters",
                newName: "ChapterNumber");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4dec5005-0ebf-4a83-87b4-b81a471c40aa", null, "Admin", "ADMIN" },
                    { "dc60386d-8a1b-4599-b3f2-82ee79b007ef", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "777f0d0e-c450-4160-8681-f40c11243f56", 0, "259de032-a051-4748-8f9b-7bd05d765204", "user@user.com", false, false, null, null, "USER", "AQAAAAIAAYagAAAAEMuOmFVxhcMFovr+ghL1Qzj0UekyaGNlgKoxoUab2/ibxg6WXUgoTXnxpaRPdDuFvw==", null, false, "f2fc9d56-e951-450b-b609-841e122e5261", false, "user" },
                    { "d25c3aa2-014e-445d-a8a8-9bf142bb856f", 0, "4a41b19e-4dff-41ee-93ba-207dd21b5957", "admin@admin.com", false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEIXlWwxBTzZ2p/EfBVZ5iT5Sz6lkmcw76CG8Q7Qxu1BlBR1sLy0+z8JZDo/w776g5Q==", null, false, "f253d2f0-bbee-4e13-8a33-91eca822aa14", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Author", "CoverImage", "Description", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("32fefa45-2189-4948-af17-2e0c67178668"), "Eiichiro Oda", "https://upload.wikimedia.org/wikipedia/en/9/90/One_Piece%2C_Volume_61_Cover_%28Japanese%29.jpg", "One Piece is a Japanese manga series written and illustrated by Eiichiro Oda. It follows the adventures of Monkey D. Luffy and his pirate crew in their quest to find the One Piece, the greatest treasure in the world.", "Adventure", new DateTime(1997, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "One Piece" },
                    { new Guid("b01f4b2e-5c14-49fa-8eeb-186608e4f15a"), "Masashi Kishimoto", "https://upload.wikimedia.org/wikipedia/en/9/94/NarutoCoverTankobon1.jpg", "Naruto is a Japanese manga series written and illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki, a young ninja who seeks recognition from his peers and dreams of becoming the Hokage, the leader of his village.", "Fantasy", new DateTime(1999, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Naruto" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "dc60386d-8a1b-4599-b3f2-82ee79b007ef", "777f0d0e-c450-4160-8681-f40c11243f56" },
                    { "4dec5005-0ebf-4a83-87b4-b81a471c40aa", "d25c3aa2-014e-445d-a8a8-9bf142bb856f" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc60386d-8a1b-4599-b3f2-82ee79b007ef", "777f0d0e-c450-4160-8681-f40c11243f56" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4dec5005-0ebf-4a83-87b4-b81a471c40aa", "d25c3aa2-014e-445d-a8a8-9bf142bb856f" });

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("32fefa45-2189-4948-af17-2e0c67178668"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("b01f4b2e-5c14-49fa-8eeb-186608e4f15a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4dec5005-0ebf-4a83-87b4-b81a471c40aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc60386d-8a1b-4599-b3f2-82ee79b007ef");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "777f0d0e-c450-4160-8681-f40c11243f56");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d25c3aa2-014e-445d-a8a8-9bf142bb856f");

            migrationBuilder.RenameColumn(
                name: "ChapterNumber",
                table: "Chapters",
                newName: "ChapterNumbr");

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
    }
}
