using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Repositories
{
	public interface IIngredientsRepository
	{
		public List<Ingredient> GetIngredients();
		public Ingredient GetIngredient(int id);
		public void PutIngredient(Ingredient ingredient);
		public void PostIngredient(Ingredient ingredient);
		public void DeleteIngredient(Ingredient ingredient);
		public List<Ingredient> GetIngredientsForRecipe(int id);
		public void DeleteIngredienstForRecipe(int recipeId);
		public bool IsIngredientExists(int id);
	}
}
