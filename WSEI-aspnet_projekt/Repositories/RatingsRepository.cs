using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Data;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Repositories
{
	public class RatingsRepository : IRatingsRepository
	{
		private readonly ApplicationDbContext _context;

		public RatingsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void PostRating(Rating rating)
		{
			_context.Ratings.Add(rating);
			_context.SaveChanges();
			_context.Entry(rating).State = EntityState.Detached;
			CalculateRatingsForRecipe(rating.RecipeId);
		}

		public void PutRating(Rating rating)
		{
			_context.Entry(rating).State = EntityState.Modified;
			_context.SaveChanges();
			_context.Entry(rating).State = EntityState.Detached;
			CalculateRatingsForRecipe(rating.RecipeId);
		}

		public Rating GetRating(int recipeId, string userId)
		{
			return _context.Ratings.Where(r => r.RecipeId == recipeId && r.UserId.Equals(userId)).FirstOrDefault();
		}

		public void DeleteRating(Rating rating)
		{
			_context.Ratings.Remove(rating);
			_context.SaveChanges();
			CalculateRatingsForRecipe(rating.RecipeId);
		}

		public void CalculateRatingsForRecipe(int recipeId)
		{
			int count = _context.Ratings.Where(r => r.RecipeId == recipeId).Select(r => r.Rate).Count();
			float average = count == 0 ? 0 : (float) _context.Ratings.Where(r => r.RecipeId == recipeId).Select(r => r.Rate).Average();

			_context.Database.ExecuteSqlRaw("UPDATE Recipes SET AvgRate = @Average, RateCount = @Count WHERE Id = @RecipeId",
				new SqlParameter("@Average", average),
				new SqlParameter("@Count", count),
				new SqlParameter("@RecipeId", recipeId));
		}
	}
}
