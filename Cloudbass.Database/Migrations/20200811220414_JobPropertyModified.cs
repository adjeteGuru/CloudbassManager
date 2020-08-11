using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cloudbass.Database.Migrations
{
    public partial class JobPropertyModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crew_Employees_EmployeeId",
                table: "Crew");

            migrationBuilder.DropForeignKey(
                name: "FK_Crew_Jobs_JobId",
                table: "Crew");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Crew",
                table: "Crew");

            migrationBuilder.RenameTable(
                name: "Crew",
                newName: "Crews");

            migrationBuilder.RenameIndex(
                name: "IX_Crew_EmployeeId",
                table: "Crews",
                newName: "IX_Crews_EmployeeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Crews",
                table: "Crews",
                columns: new[] { "JobId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_Employees_EmployeeId",
                table: "Crews",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_Jobs_JobId",
                table: "Crews",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crews_Employees_EmployeeId",
                table: "Crews");

            migrationBuilder.DropForeignKey(
                name: "FK_Crews_Jobs_JobId",
                table: "Crews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Crews",
                table: "Crews");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Crews",
                newName: "Crew");

            migrationBuilder.RenameIndex(
                name: "IX_Crews_EmployeeId",
                table: "Crew",
                newName: "IX_Crew_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Crew",
                table: "Crew",
                columns: new[] { "JobId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Crew_Employees_EmployeeId",
                table: "Crew",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Crew_Jobs_JobId",
                table: "Crew",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
