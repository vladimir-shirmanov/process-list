using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessWorker.Db.Migrations
{
    public partial class AppProcessChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AppProcesses",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDate",
                table: "AppProcesses",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "AppProcesses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AppProcesses");

            migrationBuilder.DropColumn(
                name: "FinishedDate",
                table: "AppProcesses");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "AppProcesses");
        }
    }
}
