using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityManager.Migrations
{
    public partial class _180720 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7ac5c784-d3f8-4d71-9267-efb2043de63e", "8848bd97-bff1-4fcc-bfad-3aa38b0ca436", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af2fe463-8a46-4b27-bc43-3c5050e886bf", "cdfba392-b1ad-4076-9b0e-08791b8cf942", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb1b1be9-2e20-45c1-9eb4-3733f7e7cde1", "2d4b6a7e-9b33-4c85-863f-cc9f2d066ed2", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ac5c784-d3f8-4d71-9267-efb2043de63e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af2fe463-8a46-4b27-bc43-3c5050e886bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb1b1be9-2e20-45c1-9eb4-3733f7e7cde1");
        }
    }
}
