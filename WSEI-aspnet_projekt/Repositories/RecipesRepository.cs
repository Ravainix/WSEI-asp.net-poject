using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Data;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;

public class RecipesRepository : IRecipesRepository
{
	ApplicationDbContext _context;

	public RecipesRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public List<Recipe> GetRecipes()
	{
		return  _context.Recipes.ToList();
	}

	public List<Recipe> GetUserRecipes(string id)
	{
		return _context.Recipes.Where(r => r.UserId == id).ToList();
	}

	public Recipe GetRecipe(int id)
	{
		return _context.Recipes.Find(id);
	}

	public void PutRecipe(Recipe recipe)
	{
		_context.Entry(recipe).State = EntityState.Modified;
		try
		{
			 _context.SaveChanges();
		}
		catch (DbUpdateConcurrencyException)
		{
			_context.Entry(recipe).State = EntityState.Detached;
			throw;
		}
	}

	public void PostRecipe(Recipe recipe)
	{
		 _context.Recipes.Add(recipe);
		 _context.SaveChanges();
		_context.Entry(recipe).State = EntityState.Detached;
	}

	public void DeleteRecipe(Recipe recipe)
	{
		_context.Recipes.Remove(recipe);
		_context.SaveChanges();
	}
}
