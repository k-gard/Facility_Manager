using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityManager.Migrations
{
    public partial class _290720 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(nullable: true),
                    TotalWorkOrders = table.Column<int>(nullable: false),
                    TotalWorkHours = table.Column<int>(nullable: false),
                    TotalCost = table.Column<int>(nullable: false),
                    fromDate = table.Column<DateTime>(nullable: false),
                    toDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

           
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrder_Equipment_EquipmentId",
                table: "WorkOrder");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_EquipmentId",
                table: "WorkOrder");

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

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "WorkOrder");
        }
    }
}
