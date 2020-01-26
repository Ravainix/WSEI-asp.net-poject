using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSEI_aspnet_projekt.Data;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_projekt.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private IIngredientsService _ingredientsService;
        private IRecipesService _recipesService;

        public IngredientsController(IIngredientsService ingredientsService, IRecipesService recipesService)
        {
            _ingredientsService = ingredientsService;
            _recipesService = recipesService;
        }

        // GET: api/Ingredients
        [HttpGet]
        public List<Ingredient> GetIngredients()
        {
            return _ingredientsService.GetIngredients();
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public ActionResult<Ingredient> GetIngredient(int id)
        {
            var ingredient = _ingredientsService.GetIngredient(id);
            if (ingredient == null)
            {
                Response.StatusCode = 400;
                return Content("Ingredient with id = " + id + " doesn't exist");
            }
            return ingredient;
        }

        // PUT: api/Ingredients/5
        [HttpPut("{id}")]
        public ActionResult<Ingredient> PutIngredient(int id, [FromBody] Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                Response.StatusCode = 400;
                return Content("Id in url must be the same as in the body");
            }

            if (_recipesService.GetRecipe(ingredient.RecipeId).UserId != GetUserId())
            {
                Response.StatusCode = 400;
                return Content("You are not the creator of the recipe associated with this ingredient, update rejected");
            }

            MyResponse response = _ingredientsService.UpdateIngredient(id, ingredient);
            if (response.isFailed())
            {
                Response.StatusCode = 400;
            }
            return Content(response._message);
        }

        // POST: api/Ingredients
        [HttpPost]
        public ActionResult<Ingredient> PostIngredient([FromBody] Ingredient ingredient)
        {
            _ingredientsService.AddIngredient(ingredient);
            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public ActionResult<Ingredient> DeleteIngredient(int id)
        {
            if (_recipesService.GetRecipe(id).UserId != GetUserId())
            {
                Response.StatusCode = 400;
                return Content("You are not a creator of that recipe, delete rejected");
            }

            MyResponse response = _ingredientsService.DeleteIngredient(id);
            if (response.isFailed())
            {
                Response.StatusCode = 400;
            }
            return Content(response._message);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
