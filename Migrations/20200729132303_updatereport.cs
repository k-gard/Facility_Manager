using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityManager.Migrations
{
    public partial class updatereport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "105cdc0d-6013-4e37-a72e-5c0917ed5ac0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5688cfd0-ccf3-46e0-88dd-db79418eb780");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6522ea2f-8e6a-4485-92a4-a90090f58783");

            migrationBuilder.AlterColumn<float>(
                name: "TotalWorkHours",
                table: "Report",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<float>(
                name: "TotalWorkCost",
                table: "Report",
                nullable: false,
                defaultValue: 0f);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4aebeb53-0605-405a-bcad-79b3fc40ca55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b37cafb-ce32-472b-9782-4ce3786480f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edb1a4d3-c31f-429c-b5d0-d38931919c59");

            migrationBuilder.DropColumn(
                name: "TotalWorkCost",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "TotalWorkHours",
                table: "Report",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

          
        }
    }
}
