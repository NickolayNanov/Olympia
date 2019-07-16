using Microsoft.EntityFrameworkCore.Migrations;

namespace Olympia.Data.Migrations
{
    public partial class addedUrlToWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Workouts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Workouts");
        }
    }
}
