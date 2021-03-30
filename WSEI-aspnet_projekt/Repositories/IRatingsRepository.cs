using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Repositories
{
	public interface IRatingsRepository
	{
		public void PostRating(Rating rating);
		public void PutRating(Rating rating);
		public Rating GetRating(int recipeId, string userId);
		public void DeleteRating(Rating rating);
	}
}
