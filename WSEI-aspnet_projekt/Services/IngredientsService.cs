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
		IIngredientsRepository _ingredientsRepository;

		public IngredientsService(IIngredientsRepository ingredientsRepository)
		{
			_ingredientsRepository = ingredientsRepository;
		}
		public void AddIngredient(Ingredient ingredient)
		{
			_ingredientsRepository.PostIngredient(ingredient);
		}

		public MyResponse DeleteIngredient(int id)
		{
			Ingredient ingredient = GetIngredient(id);
			if (ingredient == null)
			{
				return new MyResponse(false, "Ingredient with id = " + id + " doesn't exist");
			}
			_ingredientsRepository.DeleteIngredient(ingredient);
			return new MyResponse(true, "Ingredient id = " + ingredient.Id + " deleted successfully");
		}

		public Ingredient GetIngredient(int id)
		{
			return _ingredientsRepository.GetIngredient(id);
		}

		public List<Ingredient> GetIngredients()
		{
			return _ingredientsRepository.GetIngredients();
		}

		public MyResponse UpdateIngredient(int id, Ingredient ingredient)
		{
			try
			{
				_ingredientsRepository.PutIngredient(ingredient);
				return new MyResponse(true, "Ingredient updated successfully");
			}
			catch
			{
				if (GetIngredient(id) == null)
				{
					return new MyResponse(false, "Ingredient with id = " + ingredient.Id + " doesn't exist");
				}
				return new MyResponse(false, "Unexpected error");
			}
		}
	}
}
