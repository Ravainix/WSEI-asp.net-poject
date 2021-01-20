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
        private readonly IIngredientsService _ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            _ingredientsService = ingredientsService;
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
                return Content(new MyResponse(true, "Ingredient with id = " + id + " doesn't exist").ToString(), "application /json");
            }
            return ingredient;
        }

        // PUT: api/Ingredients
        [HttpPut]
        public ActionResult<MyResponse> PutIngredient([FromBody] Ingredient ingredient)
        {
            MyResponse response = _ingredientsService.UpdateIngredient(ingredient, GetUserId());
            if (response.IsFailed())
            {
                Response.StatusCode = 400;
            }
            return response;
        }

        // POST: api/Ingredients
        [HttpPost]
        public ActionResult<MyResponse> PostIngredient([FromBody] Ingredient ingredient)
        {
            return _ingredientsService.AddIngredient(ingredient, GetUserId());
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public ActionResult<MyResponse> DeleteIngredient(int id)
        {
            MyResponse response = _ingredientsService.DeleteIngredient(id, GetUserId());
            if (response.IsFailed())
            {
                Response.StatusCode = 400;
            }
            return response;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
