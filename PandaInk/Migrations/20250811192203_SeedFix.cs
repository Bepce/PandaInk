using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1fedf582-694d-4f17-9f32-f6cf3b45f26b", null, "Admin", "ADMIN" },
                    { "3a38994a-2e7d-4332-849a-ec89cca101f6", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6", 0, "10dbf1b7-2295-44b5-9ea3-3ba11bbdb911", "user@user.com", false, false, null, null, "USER", "AQAAAAIAAYagAAAAEEkZ6UgBM/yno368DX05gm507bZAZz0DM86lDTD618N2e7gp9J0iMMmOHKVVrfBeyw==", null, false, "5de07116-7d8b-4075-9925-329eccf6972a", false, "user" },
                    { "f9f9694c-3118-481c-81b7-eba2a9123f91", 0, "08ec44f8-0039-4b89-88b6-50528dbbd5a9", "admin@admin.com", false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEOVojVkGJix0SISVngqTayDa+EaHxXH4gR0GvWMo25ZAOwfqdt0HVFyMNa0dtrVg1A==", null, false, "18d4a6e3-8f7d-4f7d-a907-475610d7cc53", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Author", "CoverImage", "Description", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("965c43c5-cc22-496b-8abb-001f9eadda7d"), "Masashi Kishimoto", "https://upload.wikimedia.org/wikipedia/en/9/94/NarutoCoverTankobon1.jpg", "Naruto is a Japanese manga series written and illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki, a young ninja who seeks recognition from his peers and dreams of becoming the Hokage, the leader of his village.", "Fantasy", new DateTime(1999, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Naruto" },
                    { new Guid("e31d4a9c-8323-4e58-9513-90af704b1ada"), "Eiichiro Oda", "https://upload.wikimedia.org/wikipedia/en/9/90/One_Piece%2C_Volume_61_Cover_%28Japanese%29.jpg", "One Piece is a Japanese manga series written and illustrated by Eiichiro Oda. It follows the adventures of Monkey D. Luffy and his pirate crew in their quest to find the One Piece, the greatest treasure in the world.", "Adventure", new DateTime(1997, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "One Piece" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3a38994a-2e7d-4332-849a-ec89cca101f6", "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6" },
                    { "1fedf582-694d-4f17-9f32-f6cf3b45f26b", "f9f9694c-3118-481c-81b7-eba2a9123f91" }
                });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "Id", "ChapterNumber", "SeriesId", "Title" },
                values: new object[,]
                {
                    { new Guid("577f2944-cec7-4b21-aaee-f794c1cf6381"), 1, new Guid("965c43c5-cc22-496b-8abb-001f9eadda7d"), "Chapter 1: Naruto Uzumaki" },
                    { new Guid("9ca453a0-ed51-4e9e-96fb-1b3de3a3a04b"), 2, new Guid("965c43c5-cc22-496b-8abb-001f9eadda7d"), "Chapter 2: The Worst Client" },
                    { new Guid("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"), 1, new Guid("e31d4a9c-8323-4e58-9513-90af704b1ada"), "Chapter 1: Romance Dawn" },
                    { new Guid("e115de06-3c25-4e3c-9a01-53ce220e8c17"), 2, new Guid("e31d4a9c-8323-4e58-9513-90af704b1ada"), "Chapter 2: They Call Him \"Straw Hat Luffy\"" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "ChapterId", "ImageUrl", "PageNumber" },
                values: new object[,]
                {
                    { new Guid("3e6e62cd-a701-467d-9b3a-43e03abac819"), new Guid("577f2944-cec7-4b21-aaee-f794c1cf6381"), "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/ubq1tyQ5SG3QNxww/1.jpg", 3 },
                    { new Guid("4fba4bd7-1b1f-402b-81c1-e44968c575d3"), new Guid("577f2944-cec7-4b21-aaee-f794c1cf6381"), "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/E7fnRJ3vSgYHriRY/2.jpg", 1 },
                    { new Guid("6fb6d27f-69bd-4bef-bcab-b4ef463093e9"), new Guid("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"), "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/01.jpg", 1 },
                    { new Guid("83ed2101-dc33-422d-8b25-98255014f1de"), new Guid("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"), "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/02.jpg", 2 },
                    { new Guid("bac2f36f-8e85-4e3b-8f0b-3f14b91a2c6c"), new Guid("577f2944-cec7-4b21-aaee-f794c1cf6381"), "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/4Oo1qlwVNnr1BffM/3.jpg", 2 },
                    { new Guid("ee5a44bb-3928-477f-bfd7-dff9e49b9250"), new Guid("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"), "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/03.jpg", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3a38994a-2e7d-4332-849a-ec89cca101f6", "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1fedf582-694d-4f17-9f32-f6cf3b45f26b", "f9f9694c-3118-481c-81b7-eba2a9123f91" });

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: new Guid("9ca453a0-ed51-4e9e-96fb-1b3de3a3a04b"));

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: new Guid("e115de06-3c25-4e3c-9a01-53ce220e8c17"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("3e6e62cd-a701-467d-9b3a-43e03abac819"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("4fba4bd7-1b1f-402b-81c1-e44968c575d3"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("6fb6d27f-69bd-4bef-bcab-b4ef463093e9"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("83ed2101-dc33-422d-8b25-98255014f1de"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("bac2f36f-8e85-4e3b-8f0b-3f14b91a2c6c"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("ee5a44bb-3928-477f-bfd7-dff9e49b9250"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fedf582-694d-4f17-9f32-f6cf3b45f26b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a38994a-2e7d-4332-849a-ec89cca101f6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9f9694c-3118-481c-81b7-eba2a9123f91");

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: new Guid("577f2944-cec7-4b21-aaee-f794c1cf6381"));

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: new Guid("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("965c43c5-cc22-496b-8abb-001f9eadda7d"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("e31d4a9c-8323-4e58-9513-90af704b1ada"));

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
    }
}
