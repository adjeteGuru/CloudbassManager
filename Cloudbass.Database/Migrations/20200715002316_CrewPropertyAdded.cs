using Microsoft.EntityFrameworkCore.Migrations;

namespace Cloudbass.Database.Migrations
{
    public partial class CrewPropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crew_HasRoles_HasRoleId",
                table: "Crew");

            migrationBuilder.DropForeignKey(
                name: "FK_Crew_Jobs_JobId",
                table: "Crew");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Crew",
                table: "Crew");

            migrationBuilder.RenameTable(
                name: "Crew",
                newName: "CrewMembers");

            migrationBuilder.RenameIndex(
                name: "IX_Crew_HasRoleId",
                table: "CrewMembers",
                newName: "IX_CrewMembers_HasRoleId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CrewMembers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrewMembers",
                table: "CrewMembers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CrewMembers_JobId",
                table: "CrewMembers",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_CrewMembers_HasRoles_HasRoleId",
                table: "CrewMembers",
                column: "HasRoleId",
                principalTable: "HasRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CrewMembers_Jobs_JobId",
                table: "CrewMembers",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrewMembers_HasRoles_HasRoleId",
                table: "CrewMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_CrewMembers_Jobs_JobId",
                table: "CrewMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrewMembers",
                table: "CrewMembers");

            migrationBuilder.DropIndex(
                name: "IX_CrewMembers_JobId",
                table: "CrewMembers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CrewMembers");

            migrationBuilder.RenameTable(
                name: "CrewMembers",
                newName: "Crew");

            migrationBuilder.RenameIndex(
                name: "IX_CrewMembers_HasRoleId",
                table: "Crew",
                newName: "IX_Crew_HasRoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Crew",
                table: "Crew",
                columns: new[] { "JobId", "HasRoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Crew_HasRoles_HasRoleId",
                table: "Crew",
                column: "HasRoleId",
                principalTable: "HasRoles",
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
