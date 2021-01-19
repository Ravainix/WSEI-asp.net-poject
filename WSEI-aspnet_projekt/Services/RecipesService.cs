using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;
using WSEI_aspnet_projekt.Services;

public class RecipesService : IRecipesService 
{
	private readonly IRecipesRepository _recipesRepository;
	private readonly IIngredientsRepository _ingredientsRepository;

	public RecipesService(IRecipesRepository recipesRepository, IIngredientsRepository ingredientsRepository)
	{
		_recipesRepository = recipesRepository;
		_ingredientsRepository = ingredientsRepository;
	}

	public List<Recipe> GetRecipes()
	{
		return _recipesRepository.GetRecipes();
	}

	public Recipe GetRecipe(int id) 
	{
		return _recipesRepository.GetRecipe(id);
	}

	public List<Recipe> GetUserRecipes(string id)
	{
		return _recipesRepository.GetUserRecipes(id);
	}

	public MyResponse UpdateRecipe(int id, Recipe recipe, string userId)
	{
		MyResponse validResult = ValidatePutRecipe(id, recipe, userId);
		if (validResult.Success)
		{
			recipe.UserId = userId;
			_recipesRepository.PutRecipe(recipe);
		}
		return validResult;
	}

	private MyResponse ValidatePutRecipe(int id, Recipe recipe, string userId)
	{
		MyResponse response = new MyResponse(false);
		if (id != recipe.Id)
		{
			response.Message = "Id in url must be the same as in the body";
			return response;
		}
		Recipe recipeFromDb = GetRecipe(id);
		if (recipeFromDb == null)
		{
			response.Message = "Recipe with id = " + id + " doesn't exist";
		} else if (!recipeFromDb.UserId.Equals(userId))
		{
			response.Message = "You are not a creator of that recipe, update rejected";
		} else
		{
			response.Success = true;
		}
		return response;
	}

	public void AddRecipe(Recipe recipe)
	{
		_recipesRepository.PostRecipe(recipe);
	}

	public void AddRecipeWithIngredients(RecipeWithIngredients recipeWithIngredients, string userId)
	{
		Recipe recipe = recipeWithIngredients.Recipe;
		recipe.UserId = userId;
		_recipesRepository.PostRecipe(recipe);
		foreach (Ingredient ingredient in recipeWithIngredients.Ingredients)
		{
			ingredient.RecipeId = recipeWithIngredients.Recipe.Id;
			_ingredientsRepository.PostIngredient(ingredient);
		}
	}

	public RecipeWithIngredients GetRecipeWithIngredients(int id)
	{
		return new RecipeWithIngredients(
			_recipesRepository.GetRecipe(id),
			_ingredientsRepository.GetIngredientsForRecipe(id).ToArray());
	}

	public MyResponse DeleteRecipe(int id)
	{
		Recipe recipe = GetRecipe(id);
		if (recipe == null)
		{
			return new MyResponse(false, "Recipe with id = " + id + " doesn't exist");
		}
		_recipesRepository.DeleteRecipe(recipe);
		return new MyResponse(true);
	}

	public List<Recipe> GetFavoriteRecipes(string userId)
	{
		return _recipesRepository.GetFavoriteRecipes(userId);
	}

	public MyResponse PostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
	{
		MyResponse validateResult = ValidatePostFavoriteRecipe(favoriteRecipe);
		if (validateResult.Success)
		{
			_recipesRepository.PostFavoriteRecipe(favoriteRecipe);
		}
		return validateResult;
	}

	private MyResponse ValidatePostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
	{
		MyResponse response = new MyResponse(false);
		if (GetRecipe(favoriteRecipe.RecipeId) == null)
		{
			response.Message = "Recipe with id = " + favoriteRecipe.RecipeId + " doesn't exist";
		}
		else if (_recipesRepository.IsFavoriteRecipeAdded(favoriteRecipe))
		{
			response.Message = "Recipe is already added to favorites";
		} else
		{
			response.Success = true;
		}
		return response;
	}

	public MyResponse DeleteFavoriteRecipe(FavoriteRecipe favoriteRecipe)
	{
		MyResponse validateResult = ValidateDeleteFavoriteRecipe(favoriteRecipe);
		if (validateResult.Success)
		{
			_recipesRepository.DeleteFavoriteRecipe(favoriteRecipe);
		}
		return validateResult;
	}

	private MyResponse ValidateDeleteFavoriteRecipe(FavoriteRecipe favoriteRecipe)
	{
		MyResponse response = new MyResponse(true);
		if (!_recipesRepository.IsFavoriteRecipeAdded(favoriteRecipe))
		{
			response.Success = false;
			response.Message = "Can't delete - recipe is not added to favorites";
		}
		return response;
	}
}
