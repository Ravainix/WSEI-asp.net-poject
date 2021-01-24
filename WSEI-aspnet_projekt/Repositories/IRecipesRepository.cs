using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Repositories
{
	public interface IRecipesRepository
	{
		public List<Recipe> GetRecipes();
		public Recipe GetRecipe(int id);
		public void PutRecipe(Recipe recipe);
		public bool IsRecipeExists(int id);
		public void PostRecipe(Recipe recipe);
		public void DeleteRecipe(Recipe recipe);
		public List<Recipe> GetUserRecipes(string id);
		public List<Recipe> GetFavoriteRecipes(string userId);
		public bool IsFavoriteRecipeAdded(FavoriteRecipe favoriteRecipe);
		public void PostFavoriteRecipe(FavoriteRecipe favoriteRecipe);
		public void DeleteFavoriteRecipe(FavoriteRecipe favoriteRecipe);
		public void DeleteAllFavoriteRecipesForRecipe(int recipeId);
		public List<Recipe> GetSortedRecipes(GetRecipeFilter filter);
	}
}
