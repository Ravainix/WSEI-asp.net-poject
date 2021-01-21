using Microsoft.EntityFrameworkCore.Migrations;

namespace WSEI_aspnet_projekt.Data.Migrations
{
    public partial class AddSeedDataForRecipeCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RecipeCategories",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, "Dania główne", null },
                    { 2, "Zupy", null },
                    { 3, "Napoje / koktajle", null },
                    { 4, "Śniadania i kolacje", null },
                    { 5, "Fast Food", null },
                    { 6, "Słodkości", null },
                    { 7, "Przekąski", null },
                    { 8, "Sałatki", null }
                });

            migrationBuilder.InsertData(
                table: "RecipeCategories",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { 9, "Mięsne", 1 },
                    { 23, "Z rybą", 8 },
                    { 22, "Z mięsem", 8 },
                    { 21, "Słone", 7 },
                    { 20, "Słodkie", 7 },
                    { 19, "Desery", 6 },
                    { 18, "Ciastka", 6 },
                    { 24, "Z nabiałem", 8 },
                    { 17, "Słone", 4 },
                    { 15, "Bezalkoholowe", 3 },
                    { 14, "Alkoholowe", 3 },
                    { 13, "Klasyczne", 2 },
                    { 12, "Krem", 2 },
                    { 11, "Wegetariańskie", 1 },
                    { 10, "Rybne", 1 },
                    { 16, "Słodkie", 4 },
                    { 25, "Vege", 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RecipeCategories",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
