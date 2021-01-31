using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityManager.Migrations
{
    public partial class _290720_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_WorkOrder_WorkOrderId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkOrderId",
                table: "AspNetUsers");

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
                name: "WorkOrderId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "WorkOrderEmployee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(nullable: false),
                    WorkOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderEmployee", x => new { x.WorkOrderId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_WorkOrderEmployee_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderEmployee_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEmployee_EmployeeId",
                table: "WorkOrderEmployee",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderEmployee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "037c8c2a-38ab-4ee1-96d8-d753765c0c84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f1a87cf-e679-4c00-8a51-d3c3d5295535");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1b771e3-7d16-49e1-aee4-c228ad7b5a7e");

            migrationBuilder.AddColumn<int>(
                name: "WorkOrderId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

          

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkOrderId",
                table: "AspNetUsers",
                column: "WorkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WorkOrder_WorkOrderId",
                table: "AspNetUsers",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
