using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class RecipeWithIngredients
	{
		public Recipe recipe { get; set; }
		public Ingredient[] ingredients { get; set; }
	}
}
