using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Services
{
	public interface ICommentsService
	{
		public void AddComment(Comment comment);
		public List<Comment> GetCommentsForRecipe(int recipeId);
		public MyResponse DeleteComment(int id, string userId);
	}
}
