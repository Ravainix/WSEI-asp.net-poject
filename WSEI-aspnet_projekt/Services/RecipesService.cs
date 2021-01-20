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

	public MyResponse UpdateRecipe(Recipe recipe, string userId)
	{
		MyResponse validResult = ValidatePutRecipe(recipe, userId);
		if (validResult.IsSuccess())
		{
			recipe.UserId = userId;
			_recipesRepository.PutRecipe(recipe);
		}
		return validResult;
	}

	private MyResponse ValidatePutRecipe(Recipe recipe, string userId)
	{
		MyResponse response = new MyResponse(false);
		Recipe recipeFromDb = GetRecipe(recipe.Id);
		if (recipeFromDb == null)
		{
			response.Message = "Recipe with id = " + recipe.Id + " doesn't exist";
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

	public MyResponse UpdateRecipeWithIngredients(RecipeWithIngredients recipeWithIngredients, string userId)
	{
		MyResponse updateResult = UpdateRecipe(recipeWithIngredients.Recipe, userId);
		if (updateResult.IsFailed())
		{
			return updateResult;
		}

		List<Ingredient> dbIngredients = _ingredientsRepository.GetIngredientsForRecipe(recipeWithIngredients.Recipe.Id);

		UpdateOrAddIngredientsToRecipe(recipeWithIngredients.Ingredients, dbIngredients, recipeWithIngredients.Recipe.Id);
		DeleteRemainingIngredients(recipeWithIngredients.Ingredients, dbIngredients);
		return updateResult;
	}

	private void UpdateOrAddIngredientsToRecipe(Ingredient[] ingredientsFromRequest, List<Ingredient> dbIngredients, int recipeId)
	{
		foreach (Ingredient i in ingredientsFromRequest)
		{
			i.RecipeId = recipeId;
			if (dbIngredients.Where(dbI => dbI.Id == i.Id).Any())
			{
				_ingredientsRepository.PutIngredient(i);
			}
			else
			{
				i.Id = 0;
				_ingredientsRepository.PostIngredient(i);
			}
		}
	}

	private void DeleteRemainingIngredients(Ingredient[] ingredientsFromRequest, List<Ingredient> dbIngredients)
	{
		dbIngredients.ForEach(dbI =>
		{
			if (!ingredientsFromRequest.Where(i => i.Id == dbI.Id).Any()) _ingredientsRepository.DeleteIngredient(dbI);
		});
	}

	public RecipeWithIngredients GetRecipeWithIngredients(int id)
	{
		return new RecipeWithIngredients(
			_recipesRepository.GetRecipe(id),
			_ingredientsRepository.GetIngredientsForRecipe(id).ToArray());
	}

	public MyResponse DeleteRecipe(int id, string userId)
	{
		Recipe recipe = GetRecipe(id);
		MyResponse validateResult = ValidateDeleteRecipe(recipe, userId);
		if (validateResult.IsFailed())
		{
			return validateResult;
		}

		_recipesRepository.DeleteRecipe(recipe);
		_recipesRepository.DeleteAllFavoriteRecipesForRecipe(recipe.Id);
		_ingredientsRepository.DeleteIngredienstForRecipe(recipe.Id);
		return validateResult;
	}

	private MyResponse ValidateDeleteRecipe(Recipe recipe, string userId)
	{
		MyResponse response = new MyResponse(false);
		if (recipe == null)
		{
			response.Message = "Recipe doesn't exist";
		}
		else if (!recipe.UserId.Equals(userId))
		{
			response.Message = "You are not a creator of that recipe, delete rejected";
		}
		else
		{
			response.Success = true;
		}
		return response;
	}

	public List<Recipe> GetFavoriteRecipes(string userId)
	{
		return _recipesRepository.GetFavoriteRecipes(userId);
	}

	public MyResponse PostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
	{
		MyResponse validateResult = ValidatePostFavoriteRecipe(favoriteRecipe);
		if (validateResult.IsSuccess())
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
		if (validateResult.IsSuccess())
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
