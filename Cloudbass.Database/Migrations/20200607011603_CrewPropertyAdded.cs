using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cloudbass.Database.Migrations
{
    public partial class CrewPropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HasRole_Jobs_JobId",
                table: "HasRole");

            migrationBuilder.DropForeignKey(
                name: "FK_HasRole_Roles_RoleId",
                table: "HasRole");

            migrationBuilder.DropForeignKey(
                name: "FK_HasRole_Users_UserId",
                table: "HasRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HasRole",
                table: "HasRole");

            migrationBuilder.DropIndex(
                name: "IX_HasRole_JobId",
                table: "HasRole");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "HasRole");

            migrationBuilder.RenameTable(
                name: "HasRole",
                newName: "HasRoles");

            migrationBuilder.RenameIndex(
                name: "IX_HasRole_RoleId",
                table: "HasRoles",
                newName: "IX_HasRoles_RoleId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HasRoles",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HasRoles",
                table: "HasRoles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Crew",
                columns: table => new
                {
                    HasRoleId = table.Column<int>(nullable: false),
                    JobId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crew", x => new { x.JobId, x.HasRoleId });
                    table.ForeignKey(
                        name: "FK_Crew_HasRoles_HasRoleId",
                        column: x => x.HasRoleId,
                        principalTable: "HasRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Crew_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HasRoles_UserId",
                table: "HasRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Crew_HasRoleId",
                table: "Crew",
                column: "HasRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HasRoles_Roles_RoleId",
                table: "HasRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HasRoles_Users_UserId",
                table: "HasRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HasRoles_Roles_RoleId",
                table: "HasRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_HasRoles_Users_UserId",
                table: "HasRoles");

            migrationBuilder.DropTable(
                name: "Crew");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HasRoles",
                table: "HasRoles");

            migrationBuilder.DropIndex(
                name: "IX_HasRoles_UserId",
                table: "HasRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HasRoles");

            migrationBuilder.RenameTable(
                name: "HasRoles",
                newName: "HasRole");

            migrationBuilder.RenameIndex(
                name: "IX_HasRoles_RoleId",
                table: "HasRole",
                newName: "IX_HasRole_RoleId");

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "HasRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HasRole",
                table: "HasRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_HasRole_JobId",
                table: "HasRole",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_HasRole_Jobs_JobId",
                table: "HasRole",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HasRole_Roles_RoleId",
                table: "HasRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HasRole_Users_UserId",
                table: "HasRole",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
