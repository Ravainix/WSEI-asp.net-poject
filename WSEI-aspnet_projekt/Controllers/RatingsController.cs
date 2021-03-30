
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
    [Route("api/ratings/")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService _ratingsService;

        public RatingsController(IRatingsService ratingsService)
        {
            _ratingsService = ratingsService;
        }

        // POST: api/ratings
        [Authorize]
        [HttpPost]
        public ActionResult<Rating> PostRating([FromBody] Rating rating)
        {
            rating.UserId = GetUserId();
            _ratingsService.AddRating(rating);
            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
        }

        // DELETE: api/ratings/{recipeId}
        [Authorize]
        [HttpDelete("{recipeId}")]
        public ActionResult<MyResponse> DeleteRating(int recipeId)
        {
            return _ratingsService.DeleteRating(recipeId, GetUserId());
        }

        // GET: api/ratings/{recipeId}
        [Authorize]
        [HttpGet("{recipeId}")]
        public ActionResult<int> GetRating(int recipeId)
        {
            return _ratingsService.GetRating(recipeId, GetUserId());
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
