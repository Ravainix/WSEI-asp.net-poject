using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;

namespace WSEI_aspnet_projekt.Services
{
	public class IngredientsService : IIngredientsService
	{
		private readonly IIngredientsRepository _ingredientsRepository;
		private readonly IRecipesService _recipesService;

		public IngredientsService(IIngredientsRepository ingredientsRepository, IRecipesService recipesService)
		{
			_ingredientsRepository = ingredientsRepository;
			_recipesService = recipesService;
		}
		public MyResponse AddIngredient(Ingredient ingredient, string userId)
		{
			MyResponse validateResult = ValidateAddIngredient(ingredient, userId);
			if (validateResult.IsSuccess())
			{
				_ingredientsRepository.PostIngredient(ingredient);
			}
			return validateResult;
		}

		private MyResponse ValidateAddIngredient(Ingredient ingredient, string userId)
		{
			MyResponse response = new MyResponse(false);
			Recipe recipe = _recipesService.GetRecipe(ingredient.RecipeId);
			if (recipe == null)
			{
				response.Message = "Recipe with id = " + ingredient.RecipeId + " doesn't exist";
			}
			else if (!recipe.UserId.Equals(userId))
			{
				response.Message = "You are not the creator of the recipe associated with this ingredient, add rejected";
			}
			else
			{
				response.Success = true;
			}
			return response;
		}

		public MyResponse DeleteIngredient(int id, string userId)
		{
			Ingredient ingredient = GetIngredient(id);
			MyResponse validateResult = ValidateDeleteIngredient(ingredient, id, userId);
			if (validateResult.IsSuccess())
			{
				_ingredientsRepository.DeleteIngredient(ingredient);
			}
			return validateResult;
		}

		private MyResponse ValidateDeleteIngredient(Ingredient ingredient, int id, string userId)
		{
			MyResponse response = new MyResponse(false);
			if (ingredient == null)
			{
				response.Message = "Ingredient with id = " + id + " doesn't exist";
			} else if (_recipesService.GetRecipe(ingredient.RecipeId).UserId != userId)
			{
				response.Message = "You are not the creator of the recipe associated with this ingredient, delete rejected";
			} else
			{
				response.Success = true;
			}
			return response;
		}

		public Ingredient GetIngredient(int id)
		{
			return _ingredientsRepository.GetIngredient(id);
		}

		public List<Ingredient> GetIngredients()
		{
			return _ingredientsRepository.GetIngredients();
		}

		public MyResponse UpdateIngredient(Ingredient ingredient, string userId)
		{
			MyResponse validateResult = ValidateUpdateIngredient(ingredient, userId);
			if (validateResult.IsSuccess())
			{
				_ingredientsRepository.PutIngredient(ingredient);
			}
			return validateResult;
		}

		private MyResponse ValidateUpdateIngredient(Ingredient ingredient, string userId)
		{
			MyResponse response = new MyResponse(false);
			if (!_ingredientsRepository.IsIngredientExists(ingredient.Id))
			{
				response.Message = "Ingredient with id = " + ingredient.Id + " doesn't exist";
			}
			else if (_recipesService.GetRecipe(ingredient.RecipeId).UserId != userId)
			{
				response.Message = "You are not the creator of the recipe associated with this ingredient, update rejected";
			}
			else
			{
				response.Success = true;
			}
			return response;
		}
	}
}
