using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;

namespace WSEI_aspnet_projekt.Services
{
	public class RatingsService : IRatingsService
	{
		private readonly IRatingsRepository _ratingsRepository;

		public RatingsService(IRatingsRepository ratingsRepository)
		{
			_ratingsRepository = ratingsRepository;
		}

		public void AddRating(Rating rating)
		{
			Rating ratingFromDb = _ratingsRepository.GetRating(rating.RecipeId, rating.UserId);
			if (ratingFromDb == null)
			{
				_ratingsRepository.PostRating(rating);
			} 
			else
			{
				ratingFromDb.Rate = rating.Rate;
				_ratingsRepository.PutRating(ratingFromDb);
			}
			
		}

		public MyResponse DeleteRating(int recipeId, string userId)
		{
			Rating rating = _ratingsRepository.GetRating(recipeId, userId);
			MyResponse validateResult = ValidateDeleteRating(rating);
			if (validateResult.IsSuccess())
			{
				_ratingsRepository.DeleteRating(rating);
			}
			return validateResult;
		}

		private MyResponse ValidateDeleteRating(Rating rating)
		{
			MyResponse response = new MyResponse(false);
			if (rating == null)
			{
				response.Message = "Rating doesn't exist";
			}
			else
			{
				response.Success = true;
			}
			return response;
		}
	}
}
