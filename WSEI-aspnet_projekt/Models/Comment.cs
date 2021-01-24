using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class Comment
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
		[Column(TypeName = "text")]
		[Required]
		public string Content { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(TypeName = "DateTime")]
		public DateTime CreatedOn { get; }
		public virtual ApplicationUser User { get; set; }
	}
}
