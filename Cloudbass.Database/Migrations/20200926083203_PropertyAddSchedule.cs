using Microsoft.EntityFrameworkCore.Migrations;

namespace Cloudbass.Database.Migrations
{
    public partial class PropertyAddSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobRef",
                table: "Schedules",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobRef",
                table: "Schedules");
        }
    }
}
