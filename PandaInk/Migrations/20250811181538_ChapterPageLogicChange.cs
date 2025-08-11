using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaInk.API.Migrations
{
    /// <inheritdoc />
    public partial class ChapterPageLogicChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a70cda13-22ea-455b-adc8-7804c8af072b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c547cbb8-17ed-48fd-a4ca-5a3c39442125");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "96dd5dc1-a19a-4e10-aacc-a1e898fd5b07", null, "User", "USER" },
                    { "a7e4519c-0ca3-42f9-9259-340db737431d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "a70cda13-22ea-455b-adc8-7804c8af072b", null, "User", "USER" },
                    { "c547cbb8-17ed-48fd-a4ca-5a3c39442125", null, "Admin", "ADMIN" }
                });
        }
    }
}
