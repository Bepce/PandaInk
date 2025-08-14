using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteOnChapters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37946001-fbf6-4811-b0f3-4fba5626941f", null, "User", "USER" },
                    { "903a479f-e2eb-4552-9ed3-fcaef6e45b3d", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad04d441-cf81-40bf-b691-0bdbdbcd3229", "AQAAAAIAAYagAAAAEKjWmLubQSRw7dXthAXygSgqSEMo3mtqDmXN5GXUi9cD/zrLgzm2PUVWuHSwJofPWA==", "00c02f54-1845-4b67-bb89-d9d41e923dde" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9f9694c-3118-481c-81b7-eba2a9123f91",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f8bc1f7-d0d5-415c-871f-e29c93034e7d", "AQAAAAIAAYagAAAAEIjVBrTW6MP9hoAAxMdz32eVB9X4+MyDgrJuvDAubMUS3JSUKL6F6V2/OWBzCSi9gg==", "002add9d-e35b-47ad-8b3d-45a2913e6d96" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "ChapterId", "ImageUrl", "PageNumber" },
                values: new object[,]
                {
                    { new Guid("31a5f81f-f04f-41f8-8637-ac87977425f7"), new Guid("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"), "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/03.jpg", 3 },
                    { new Guid("3af9bea8-6a6a-4746-9f6b-916abe0d4fcc"), new Guid("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"), "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/02.jpg", 2 },
                    { new Guid("4391ff5a-3abd-4eb8-823a-6a9ad521ed12"), new Guid("cccd4420-5e1a-4630-ab20-6e0ef2225ee9"), "https://eu2.contabostorage.com/2352a0b47a16442aa2bd93b0a47735ea:manga/1piece/Chapter%201/01.jpg", 1 },
                    { new Guid("7ea3efe4-c87d-47f6-b91d-2490564fa396"), new Guid("577f2944-cec7-4b21-aaee-f794c1cf6381"), "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/ubq1tyQ5SG3QNxww/1.jpg", 3 },
                    { new Guid("e40025d6-b170-42ad-b752-56f773d8456d"), new Guid("577f2944-cec7-4b21-aaee-f794c1cf6381"), "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/4Oo1qlwVNnr1BffM/3.jpg", 2 },
                    { new Guid("e635a601-7591-4076-bd8c-ac8cdeb47d39"), new Guid("577f2944-cec7-4b21-aaee-f794c1cf6381"), "https://cmsapi-frontend.naruto-official.com/site/api/naruto/Image/get?path=/naruto/en/comics/2022/09/29/E7fnRJ3vSgYHriRY/2.jpg", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "37946001-fbf6-4811-b0f3-4fba5626941f", "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6" },
                    { "903a479f-e2eb-4552-9ed3-fcaef6e45b3d", "f9f9694c-3118-481c-81b7-eba2a9123f91" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "37946001-fbf6-4811-b0f3-4fba5626941f", "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "903a479f-e2eb-4552-9ed3-fcaef6e45b3d", "f9f9694c-3118-481c-81b7-eba2a9123f91" });

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("31a5f81f-f04f-41f8-8637-ac87977425f7"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("3af9bea8-6a6a-4746-9f6b-916abe0d4fcc"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("4391ff5a-3abd-4eb8-823a-6a9ad521ed12"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("7ea3efe4-c87d-47f6-b91d-2490564fa396"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("e40025d6-b170-42ad-b752-56f773d8456d"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("e635a601-7591-4076-bd8c-ac8cdeb47d39"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37946001-fbf6-4811-b0f3-4fba5626941f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "903a479f-e2eb-4552-9ed3-fcaef6e45b3d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1fedf582-694d-4f17-9f32-f6cf3b45f26b", null, "Admin", "ADMIN" },
                    { "3a38994a-2e7d-4332-849a-ec89cca101f6", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10dbf1b7-2295-44b5-9ea3-3ba11bbdb911", "AQAAAAIAAYagAAAAEEkZ6UgBM/yno368DX05gm507bZAZz0DM86lDTD618N2e7gp9J0iMMmOHKVVrfBeyw==", "5de07116-7d8b-4075-9925-329eccf6972a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9f9694c-3118-481c-81b7-eba2a9123f91",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08ec44f8-0039-4b89-88b6-50528dbbd5a9", "AQAAAAIAAYagAAAAEOVojVkGJix0SISVngqTayDa+EaHxXH4gR0GvWMo25ZAOwfqdt0HVFyMNa0dtrVg1A==", "18d4a6e3-8f7d-4f7d-a907-475610d7cc53" });

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3a38994a-2e7d-4332-849a-ec89cca101f6", "3bf4ffff-5c78-4a62-b687-609b3cc0b6a6" },
                    { "1fedf582-694d-4f17-9f32-f6cf3b45f26b", "f9f9694c-3118-481c-81b7-eba2a9123f91" }
                });
        }
    }
}
