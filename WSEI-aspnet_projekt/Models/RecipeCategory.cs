using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class RecipeCategory
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(TypeName = "int")]
		public int Id { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }
		[Column(TypeName = "int")]
		public int? ParentId { get; set; }
		public virtual RecipeCategory Parent { get; set; }
		public virtual ICollection<RecipeCategory> Children { get; set; }
	}
}
