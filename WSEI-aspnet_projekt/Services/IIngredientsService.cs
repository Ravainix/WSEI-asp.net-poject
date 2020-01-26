using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;

namespace WSEI_aspnet_projekt.Services
{
	public interface IIngredientsService
	{
		public List<Ingredient> GetIngredients();
		public Ingredient GetIngredient(int id);
		public MyResponse UpdateIngredient(int id, Ingredient ingredient);
		public void AddIngredient(Ingredient ingredient);
		public MyResponse DeleteIngredient(int id);
	}
}
