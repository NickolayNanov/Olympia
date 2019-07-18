using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Olympia.Data.Migrations
{
    public partial class DeletedParentCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildCategories_ParentCategories_ParentCategoryId",
                table: "ChildCategories");

            migrationBuilder.DropTable(
                name: "ParentCategories");

            migrationBuilder.DropIndex(
                name: "IX_ChildCategories_ParentCategoryId",
                table: "ChildCategories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "ChildCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "ChildCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParentCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildCategories_ParentCategoryId",
                table: "ChildCategories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildCategories_ParentCategories_ParentCategoryId",
                table: "ChildCategories",
                column: "ParentCategoryId",
                principalTable: "ParentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
