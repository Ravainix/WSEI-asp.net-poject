using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Services
{
	public interface IRecipesService
	{
		public List<Recipe> GetRecipes();
		public Recipe GetRecipe(int id);
		public MyResponse UpdateRecipe(int id, Recipe recipe);
		public void AddRecipe(Recipe recipe);
		public MyResponse DeleteRecipe(int id);
		public List<Recipe> GetUserRecipes(string id);
		public void AddRecipeWithIngredients(RecipeWithIngredients recipeWithIngredients, string userId);
		public RecipeWithIngredients GetRecipeWithIngredients(int id);
	}
}
