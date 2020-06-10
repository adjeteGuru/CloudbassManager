using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cloudbass.Database.Migrations
{
    public partial class PropJobModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crew_Jobs_JobId1",
                table: "Crew");

            migrationBuilder.DropIndex(
                name: "IX_Crew_JobId1",
                table: "Crew");

            migrationBuilder.DropColumn(
                name: "JobId1",
                table: "Crew");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JobId1",
                table: "Crew",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Crew_JobId1",
                table: "Crew",
                column: "JobId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Crew_Jobs_JobId1",
                table: "Crew",
                column: "JobId1",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
