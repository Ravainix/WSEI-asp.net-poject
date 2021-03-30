
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_projekt.Controllers
{
	[Route("api/comments/")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        // POST: api/comments
        [Authorize]
        [HttpPost]
        public ActionResult<Comment> PostComment([FromBody] Comment comment)
        {
            comment.UserId = GetUserId();
            _commentsService.AddComment(comment);
            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // GET: api/comments/{recipeId}
        [HttpGet("{recipeId}")]
        public List<Comment> GetCommentsForRecipe(int recipeId)
        {
            return _commentsService.GetCommentsForRecipe(recipeId);
        }

        // DELETE: api/comments/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<MyResponse> DeleteComment(int id)
        {
            return _commentsService.DeleteComment(id, GetUserId());
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
