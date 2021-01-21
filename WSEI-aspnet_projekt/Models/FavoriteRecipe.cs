using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class FavoriteRecipe
	{
		[Column(TypeName = "nvarchar(450)")]
		[ForeignKey("UserId")]
		public string UserId { get; set; }

		[Column(TypeName = "int")]
		[ForeignKey("RecipeId")]
		public int RecipeId { get; set; }
		public virtual ApplicationUser User { get; set; }

		public FavoriteRecipe(string userId, int recipeId)
		{
			UserId = userId;
			RecipeId = recipeId;
		}
	}
}
