using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Repositories
{
	public interface IRecipesRepository
	{
		public Task<ActionResult<IEnumerable<Recipe>>> GetRecipes();
		public Recipe GetRecipe(int id);
		public void PutRecipe(Recipe recipe);
		public void PostRecipe(Recipe recipe);
		public void DeleteRecipe(Recipe recipe);
	}
}
