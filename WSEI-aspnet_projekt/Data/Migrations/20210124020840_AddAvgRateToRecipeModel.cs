using Microsoft.EntityFrameworkCore.Migrations;

namespace WSEI_aspnet_projekt.Data.Migrations
{
    public partial class AddAvgRateToRecipeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AvgRate",
                table: "Recipes",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgRate",
                table: "Recipes");
        }
    }
}
