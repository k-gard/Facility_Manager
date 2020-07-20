using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityManager.Migrations
{
    public partial class workOrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "WorkEnd",
                table: "WorkOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkStart",
                table: "WorkOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkEnd",
                table: "WorkOrder");

            migrationBuilder.DropColumn(
                name: "WorkStart",
                table: "WorkOrder");
        }
    }
}
