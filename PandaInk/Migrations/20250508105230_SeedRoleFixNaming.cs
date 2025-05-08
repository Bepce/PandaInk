using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoleFixNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "23781d8e-799e-4468-b679-09484220c649", null, "Admin", "ADMIN" },
                    { "79023320-5204-41cc-87f3-f6546c1164be", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23781d8e-799e-4468-b679-09484220c649");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79023320-5204-41cc-87f3-f6546c1164be");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8568f226-f86a-4ff5-a0c0-0822bb059980", null, "USER", null },
                    { "ecb353a7-c9a2-4150-940c-0f544aa28803", null, "ADMIN", null }
                });
        }
    }
}
