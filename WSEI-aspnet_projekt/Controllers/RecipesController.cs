using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSEI_aspnet_projekt.Data;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        IRecipesService _recipesService;

        public RecipesController(ApplicationDbContext context, IRecipesService recipesService)
        {
            _context = context;
            _recipesService = recipesService;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _recipesService.GetRecipes();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
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
        [HttpPost]
        public ActionResult<Recipe> PostRecipe([FromBody] Recipe recipe)
        {
            _recipesService.AddRecipe(recipe);
            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
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
