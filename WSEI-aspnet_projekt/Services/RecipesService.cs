using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;
using WSEI_aspnet_projekt.Services;

public class RecipesService : IRecipesService 
{
	IRecipesRepository _recipesRepository;
	IIngredientsRepository _ingredientsRepository;

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

	public MyResponse UpdateRecipe(int id, Recipe recipe)
	{
		try
		{
			_recipesRepository.PutRecipe(recipe);
			return new MyResponse(true, "Recipe updated successfully");
		}
		catch
		{
			if (GetRecipe(id) == null)
			{
				return new MyResponse(false, "Recipe with id = " + recipe.Id + " doesn't exist");
			}
			return new MyResponse(false, "Unexpected error");
		}
		
	}

	public void AddRecipe(Recipe recipe)
	{
		_recipesRepository.PostRecipe(recipe);
	}

	public void AddRecipeWithIngredients(RecipeWithIngredients recipeWithIngredients, string userId)
	{
		Recipe recipe = recipeWithIngredients.recipe;
		recipe.UserId = userId;
		_recipesRepository.PostRecipe(recipe);
		foreach (Ingredient ingredient in recipeWithIngredients.ingredients)
		{
			ingredient.RecipeId = recipeWithIngredients.recipe.Id;
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
		return new MyResponse(true, "Recipe id = " + recipe.Id + " deleted successfully");
	}
}
