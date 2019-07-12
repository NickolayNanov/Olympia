namespace Olympia.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedProfilePicToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturImgUrl",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturImgUrl",
                table: "AspNetUsers");
        }
    }
}
