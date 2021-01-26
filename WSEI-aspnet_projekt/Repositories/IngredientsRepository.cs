using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Data;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Repositories
{
	public class IngredientsRepository : IIngredientsRepository
	{
		private readonly ApplicationDbContext _context;

		public IngredientsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public Ingredient GetIngredient(int id)
		{
			return _context.Ingredients.Find(id);
		}

		public List<Ingredient> GetIngredients()
		{
			return _context.Ingredients.ToList();
		}

		public List<Ingredient> GetIngredientsForRecipe(int recipeId)
		{
			return _context.Ingredients.Where(i => i.RecipeId == recipeId).AsNoTracking().ToList();
		}

		public void PostIngredient(Ingredient ingredient)
		{
			_context.Ingredients.Add(ingredient);
			_context.SaveChanges();
			_context.Entry(ingredient).State = EntityState.Detached;
		}

		public void PutIngredient(Ingredient ingredient)
		{
			_context.Entry(ingredient).State = EntityState.Modified;
			_context.SaveChanges();
			_context.Entry(ingredient).State = EntityState.Detached;
		}

		public void DeleteIngredient(Ingredient ingredient)
		{
			_context.Ingredients.Remove(ingredient);
			_context.SaveChanges();
		}

		public void DeleteIngredienstForRecipe(int recipeId)
		{
			_context.Ingredients.RemoveRange(
				_context.Ingredients.Where(i => i.RecipeId == recipeId));
			_context.SaveChanges();
		}

		public bool IsIngredientExists(int id)
		{
			return _context.Ingredients.Where(i => i.Id == id).Any();
		}
	}
}
