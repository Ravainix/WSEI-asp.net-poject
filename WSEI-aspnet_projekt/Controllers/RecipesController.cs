using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IRecipesService _recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }

        // GET: api/currentUserRecipes
        [Authorize]
        [HttpGet("currentUserRecipes")]
        public ActionResult<IEnumerable<Recipe>> GetCurrentUserRecipes()
        {
            return _recipesService.GetUserRecipes(GetUserId());
        }

        // GET: api/recipes
        [HttpGet("recipes")]
        public List<Recipe> GetRecipes()
        {
            return _recipesService.GetRecipes();
        }

        // GET: api/recipes/{id}
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

        // PUT: api/recipes
        [Authorize]
        [HttpPut("recipes")]
        public ActionResult<MyResponse> PutRecipe([FromBody] Recipe recipe)
        {
            MyResponse response = _recipesService.UpdateRecipe(recipe, GetUserId());
            if (!response.Success)
            {
                Response.StatusCode = 400;
            }
            return response;
        }

        // POST: api/recipes
        [Authorize]
        [HttpPost("recipes")]
        public ActionResult<Recipe> PostRecipe([FromBody] Recipe recipe)
        {
            recipe.UserId = GetUserId();
            _recipesService.AddRecipe(recipe);
            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // POST: api/recipesWithIngredients
        [Authorize]
        [HttpPost("recipesWithIngredients")]
        public ActionResult<Recipe> PostRecipeWithIngredients([FromBody] RecipeWithIngredients recipeWithIngredients)
        {
            _recipesService.AddRecipeWithIngredients(recipeWithIngredients, GetUserId());
            return Content("Recipe added succesfully");
        }

        // PUT: api/recipesWithIngredients
        [Authorize]
        [HttpPut("recipesWithIngredients")]
        public ActionResult<MyResponse> PutRecipeWithIngredients([FromBody] RecipeWithIngredients recipeWithIngredients)
        {
            return _recipesService.UpdateRecipeWithIngredients(recipeWithIngredients, GetUserId());
        }

        // GET: api/recipesWithIngredients/{id}
        [HttpGet("recipesWithIngredients/{id}")]
        public RecipeWithIngredients GetRecipeWithIngredients(int id)
        {
            return _recipesService.GetRecipeWithIngredients(id);
        }

        // DELETE: api/recipes/{id}
        [Authorize]
        [HttpDelete("recipes/{id}")]
        public ActionResult<MyResponse> DeleteRecipe(int id)
        {
            return _recipesService.DeleteRecipe(id, GetUserId());
        }

        // GET: api/recipes/favorite
        [Authorize]
        [HttpGet("recipes/favorite")]
        public ActionResult<IEnumerable<Recipe>> GetFavoriteRecipes()
        {
            return _recipesService.GetFavoriteRecipes(GetUserId());
        }

        // POST: api/recipes/favorite/{recipeId}
        [Authorize]
        [HttpPost("recipes/favorite/{recipeId}")]
        public ActionResult<MyResponse> PostFavoriteRecipe(int recipeId)
        {
            return _recipesService.PostFavoriteRecipe(new FavoriteRecipe(GetUserId(), recipeId));
        }

        // DELETE: api/recipes/favorite/{recipeId}
        [Authorize]
        [HttpDelete("recipes/favorite/{recipeId}")]
        public ActionResult<MyResponse> DeleteFavoriteRecipe(int recipeId)
        {
            return _recipesService.DeleteFavoriteRecipe(new FavoriteRecipe(GetUserId(), recipeId));
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
