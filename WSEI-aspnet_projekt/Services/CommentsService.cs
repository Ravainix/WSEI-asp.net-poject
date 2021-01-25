using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;

namespace WSEI_aspnet_projekt.Services
{
	public class CommentsService : ICommentsService
	{
		private readonly ICommentsRepository _commentsRepository;

		public CommentsService(ICommentsRepository commentsRepository)
		{
			_commentsRepository = commentsRepository;
		}

		public void AddComment(Comment comment)
		{
			_commentsRepository.PostComment(comment);
		}

		public List<Comment> GetCommentsForRecipe(int recipeId)
		{
			return _commentsRepository.GetCommentsForRecipe(recipeId);
		}

		public MyResponse DeleteComment(int id, string userId)
		{
			Comment comment = _commentsRepository.GetComment(id);
			MyResponse validateResult = ValidateDeleteComment(comment, userId);
			if (validateResult.IsSuccess())
			{
				_commentsRepository.DeleteComment(comment);
			}
			return validateResult;
		}

		private MyResponse ValidateDeleteComment(Comment comment, string userId)
		{
			MyResponse response = new MyResponse(false);
			if (comment == null)
			{
				response.Message = "Comment doesn't exist";
			}
			else if (!comment.UserId.Equals(userId))
			{
				response.Message = "You are not the author of this comment, delete rejected";
			}
			else
			{
				response.Success = true;
			}
			return response;
		}
	}
}
