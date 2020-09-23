using Microsoft.EntityFrameworkCore.Migrations;

namespace Cloudbass.Database.Migrations
{
    public partial class PropertiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "HasRoles");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDays",
                table: "Crews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "Crews");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDays",
                table: "HasRoles",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
