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
	private readonly ApplicationDbContext _context;

	public RecipesRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public List<Recipe> GetRecipes()
	{
		return  _context.Recipes.ToList();
	}

	public List<Recipe> GetSortedRecipes(GetRecipeFilter filter)
	{
		var query = _context.Recipes.AsNoTracking();
		if (filter.CategoryId.HasValue)
		{
			query = query.Where(r => r.RecipeCategoryId == filter.CategoryId);
		}

		if ("rate".Equals(filter.Sort))
		{
			if ("asc".Equals(filter.SortOrder))
			{
				query = query.OrderBy(r => r.AvgRate);
			} 
			else if ("desc".Equals(filter.SortOrder))
			{
				query = query.OrderByDescending(r => r.AvgRate);
			}
		} 
		else if ("date".Equals(filter.Sort))
		{
			if ("asc".Equals(filter.SortOrder))
			{
				query = query.OrderBy(r => r.CreatedOn);
			}
			else if ("desc".Equals(filter.SortOrder))
			{
				query = query.OrderByDescending(r => r.CreatedOn);
			}
		}

		if (filter.Amount.HasValue && filter.Page.HasValue)
		{
			query = query.Skip(filter.Amount.Value * (filter.Page.Value - 1));
		}
		if (filter.Amount.HasValue)
		{
			query = query.Take(filter.Amount.Value);
		}
		
		return query.ToList();
	}

	public List<Recipe> GetUserRecipes(string id)
	{
		return _context.Recipes.Where(r => r.UserId == id).ToList();
	}

	public Recipe GetRecipe(int id)
	{
		Recipe recipe = _context.Recipes.Find(id);
		if (recipe != null)
		{
			_context.Entry(recipe).State = EntityState.Detached;
		}
		return recipe;
	}

	public void PutRecipe(Recipe recipe)
	{
		_context.Entry(recipe).State = EntityState.Modified;
		_context.SaveChanges();
		_context.Entry(recipe).State = EntityState.Detached;
	}

	public bool IsRecipeExists(int id)
	{
		return _context.Recipes
			.Where(r => r.Id == id)
			.Any();
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

	public List<Recipe> GetFavoriteRecipes(string userId)
	{
		return _context.Recipes
			.Join(_context.FavoriteRecipes,
			r => r.Id,
			f => f.RecipeId,
			(r, f) => new { r, f })
			.Where(o => o.f.UserId.Equals(userId))
			.Select(o => o.r).ToList();
	}

	public bool IsFavoriteRecipeAdded(FavoriteRecipe favoriteRecipe)
	{
		return _context.FavoriteRecipes
			.Where(f => f.RecipeId == favoriteRecipe.RecipeId && f.UserId.Equals(favoriteRecipe.UserId))
			.Any();
	}

	public void PostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
	{
		_context.FavoriteRecipes.Add(favoriteRecipe);
		_context.SaveChanges();
		_context.Entry(favoriteRecipe).State = EntityState.Detached;
	}

	public void DeleteFavoriteRecipe(FavoriteRecipe favoriteRecipe)
	{
		_context.FavoriteRecipes.Remove(favoriteRecipe);
		_context.SaveChanges();
	}

	public void DeleteAllFavoriteRecipesForRecipe(int recipeId)
	{
		_context.FavoriteRecipes.RemoveRange(
			_context.FavoriteRecipes.Where(f => f.RecipeId == recipeId));
		_context.SaveChanges();
	}
}
