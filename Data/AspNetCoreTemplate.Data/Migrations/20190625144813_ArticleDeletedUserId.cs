using Microsoft.EntityFrameworkCore.Migrations;

namespace Olympia.Data.Migrations
{
    public partial class ArticleDeletedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("UserId", "Articles"); 
        }
    }
}
