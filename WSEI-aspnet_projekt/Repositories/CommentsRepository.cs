using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Data;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Repositories
{
	public class CommentsRepository : ICommentsRepository
	{
		private readonly ApplicationDbContext _context;

		public CommentsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void PostComment(Comment comment)
		{
			_context.Comments.Add(comment);
			_context.SaveChanges();
			_context.Entry(comment).State = EntityState.Detached;
		}

		public List<Comment> GetCommentsForRecipe(int recipeId)
		{
			return _context.Comments.Where(c => c.RecipeId == recipeId).AsNoTracking().ToList();
		}

		public Comment GetComment(int id)
		{
			Comment comment = _context.Comments.Find(id);
			if (comment != null)
			{
				_context.Entry(comment).State = EntityState.Detached;
			}
			return comment;
		}

		public void DeleteComment(Comment comment)
		{
			_context.Comments.Remove(comment);
			_context.SaveChanges();
		}
	}
}
