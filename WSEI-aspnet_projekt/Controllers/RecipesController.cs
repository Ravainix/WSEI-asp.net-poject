using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSEI_aspnet_projekt.Data;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_projekt.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private IRecipesService _recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }

        // GET: api/currentUserRecipes
        [HttpGet("currentUserRecipes")]
        public ActionResult<IEnumerable<Recipe>> GetCurrentUserRecipes()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //Not tested
            if(userId == null)
            {
                Response.StatusCode = 400;
                return Content("Can't recognize current user");
            }
            return _recipesService.GetUserRecipes(userId);
        }

        // GET: api/Recipes
        [HttpGet("recipes")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _recipesService.GetRecipes();
        }

        // GET: api/Recipes/5
        [HttpGet("recipes/{id}")]
        public ActionResult<Recipe> GetRecipe(int id)
        {
            var recipe = _recipesService.GetRecipe(id);
            if (recipe == null)
            {
                Response.StatusCode = 400;
                return Content("Recipe with id = " + id + " doesn't exist"); 
            }
            return recipe;
        }

        // PUT: api/Recipes/5
        [HttpPut("recipes/{id}")]
        public ActionResult PutRecipe(int id, [FromBody] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                Response.StatusCode = 400;
                return Content("Id in url must be the same as in the body");
            }

            MyResponse response = _recipesService.UpdateRecipe(id, recipe);
            if (response.isFailed()) 
            {
                Response.StatusCode = 400;
            }
                return Content(response._message);
        }

        // POST: api/Recipes
        [HttpPost("recipes")]
        public ActionResult<Recipe> PostRecipe([FromBody] Recipe recipe)
        {
            _recipesService.AddRecipe(recipe);
            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("recipes/{id}")]
        public ActionResult<Recipe> DeleteRecipe(int id)
        {
            MyResponse response = _recipesService.DeleteRecipe(id);
            if (response.isFailed())
            {
                Response.StatusCode = 400;
            }
            return Content(response._message);
        }

    }
}
