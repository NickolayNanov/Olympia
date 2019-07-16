using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Olympia.Data.Migrations
{
    public partial class ChangedFitnessPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FitnessPlans_DietPlans_DietPlanId",
                table: "FitnessPlans");

            migrationBuilder.DropTable(
                name: "DietPlans");

            migrationBuilder.DropIndex(
                name: "IX_FitnessPlans_DietPlanId",
                table: "FitnessPlans");

            migrationBuilder.RenameColumn(
                name: "DietPlanId",
                table: "FitnessPlans",
                newName: "CaloriesGoal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaloriesGoal",
                table: "FitnessPlans",
                newName: "DietPlanId");

            migrationBuilder.CreateTable(
                name: "DietPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaloriesGoal = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FitnessPlans_DietPlanId",
                table: "FitnessPlans",
                column: "DietPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_FitnessPlans_DietPlans_DietPlanId",
                table: "FitnessPlans",
                column: "DietPlanId",
                principalTable: "DietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
