using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class RecipeWithIngredients
	{
		public RecipeWithIngredients(Recipe recipe, Ingredient[] ingredients) 
		{
			Recipe = recipe;
			Ingredients = ingredients;
		}

		public RecipeWithIngredients() { }

		public Recipe Recipe { get; set; }
		public Ingredient[] Ingredients { get; set; }
	}
}
