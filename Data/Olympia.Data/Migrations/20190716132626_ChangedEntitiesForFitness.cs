using Microsoft.EntityFrameworkCore.Migrations;

namespace Olympia.Data.Migrations
{
    public partial class ChangedEntitiesForFitness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Workouts",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
