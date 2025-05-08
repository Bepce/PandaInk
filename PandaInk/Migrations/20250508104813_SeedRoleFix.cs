using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoleFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73edf941-13e8-4406-b528-f3cad1611a6d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec5b4464-e039-4d2f-b8ae-732fa249215e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8568f226-f86a-4ff5-a0c0-0822bb059980", null, "USER", null },
                    { "ecb353a7-c9a2-4150-940c-0f544aa28803", null, "ADMIN", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8568f226-f86a-4ff5-a0c0-0822bb059980");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecb353a7-c9a2-4150-940c-0f544aa28803");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73edf941-13e8-4406-b528-f3cad1611a6d", null, "Admin", null },
                    { "ec5b4464-e039-4d2f-b8ae-732fa249215e", null, "User", null }
                });
        }
    }
}
