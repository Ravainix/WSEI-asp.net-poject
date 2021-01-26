using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Repositories
{
	public interface ICommentsRepository
	{
		public void PostComment(Comment comment);
		public List<Comment> GetCommentsForRecipe(int recipeId);
		public Comment GetComment(int id);
		public void DeleteComment(Comment comment);
		public string GetUsername(string userId);
	}
}
