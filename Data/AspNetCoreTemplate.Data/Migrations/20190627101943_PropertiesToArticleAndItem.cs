using Microsoft.EntityFrameworkCore.Migrations;

namespace Olympia.Data.Migrations
{
    public partial class PropertiesToArticleAndItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesBought",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimesRead",
                table: "Articles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesBought",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TimesRead",
                table: "Articles");
        }
    }
}
