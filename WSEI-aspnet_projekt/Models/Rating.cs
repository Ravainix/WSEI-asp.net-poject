using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class Rating
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(TypeName = "int")]
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(450)")]
		[Required]
		[ForeignKey("UserId")]
		public string UserId { get; set; }
		[Column(TypeName = "int")]
		[Required]
		[ForeignKey("RecipeId")]
		public int RecipeId { get; set; }
		[Column(TypeName = "tinyint")]
		[Required]
		[Range(1, 5)]
		public int Rate { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}
