using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class GetRecipeFilter
	{
		public int? Page { get; set; }
		public int? Amount { get; set; }
		public string? Sort { get; set; }
		public string? SortOrder { get; set; }
		public int? CategoryId { get; set; }

	}
}
