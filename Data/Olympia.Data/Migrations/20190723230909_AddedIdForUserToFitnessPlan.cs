using Microsoft.EntityFrameworkCore.Migrations;

namespace Olympia.Data.Migrations
{
    public partial class AddedIdForUserToFitnessPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FitnessPlans_FitnessPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FitnessPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FitnessPlanId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "FitnessPlans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FitnessPlans_OwnerId",
                table: "FitnessPlans",
                column: "OwnerId",
                unique: true,
                filter: "[OwnerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FitnessPlans_AspNetUsers_OwnerId",
                table: "FitnessPlans",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FitnessPlans_AspNetUsers_OwnerId",
                table: "FitnessPlans");

            migrationBuilder.DropIndex(
                name: "IX_FitnessPlans_OwnerId",
                table: "FitnessPlans");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "FitnessPlans");

            migrationBuilder.AddColumn<int>(
                name: "FitnessPlanId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FitnessPlanId",
                table: "AspNetUsers",
                column: "FitnessPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FitnessPlans_FitnessPlanId",
                table: "AspNetUsers",
                column: "FitnessPlanId",
                principalTable: "FitnessPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
