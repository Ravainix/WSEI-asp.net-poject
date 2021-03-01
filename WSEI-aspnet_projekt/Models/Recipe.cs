using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class Recipe
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(TypeName = "int")]
		public int Id { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }
		[Column(TypeName = "nvarchar(300)")]
		public string Description { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(450)")]
		public string UserId { get; set; }
		[Column(TypeName = "bit")]
		public bool Hidden { get; set; }
		[Column(TypeName = "nvarchar(200)")]
		public string Image { get; set; }
		[Column(TypeName = "text")]
		public string Instruction { get; set; }
		[Column(TypeName = "tinyint")]
		public short Portions { get; set; }
		[Column(TypeName = "int")]
		public int PrepareTime { get; set; }
		[Column(TypeName = "nvarchar(30)")]
		public RecipeDifficulty Difficulty { get; set; }
		[Column(TypeName = "int")]
		[ForeignKey("RecipeCategoryId")]
		public int? RecipeCategoryId { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(TypeName = "DateTime")]
		public DateTime CreatedOn { get; set; }
		[Column(TypeName = "decimal(3,2)")]
		public float AvgRate { get; private set; }
		[Column(TypeName = "int")]
		public int RateCount { get; private set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ICollection<Ingredient> Ingredients { get; set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ICollection<Comment> Comments { get; set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ICollection<Rating> Ratings { get; set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ApplicationUser User { get; set; }
	}
}
 