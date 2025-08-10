
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class ChaptersToSeriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25dec0fd-8acf-40d9-befc-4e948bbe41d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c5600cd-95b5-4c04-9629-b699ef8533cc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ae7692b7-0908-4784-959c-09dd8ace3e4c", null, "Admin", "ADMIN" },
                    { "cae8d958-c84a-4215-9961-149fab8eb854", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae7692b7-0908-4784-959c-09dd8ace3e4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cae8d958-c84a-4215-9961-149fab8eb854");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25dec0fd-8acf-40d9-befc-4e948bbe41d2", null, "Admin", "ADMIN" },
                    { "9c5600cd-95b5-4c04-9629-b699ef8533cc", null, "User", "USER" }
                });
        }
    }
}
