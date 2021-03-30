using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Services
{
	public interface IRatingsService
	{
		public int GetRating(int recipeId, string userId);
		public void AddRating(Rating comment);
		public MyResponse DeleteRating(int id, string userId);
	}
}
