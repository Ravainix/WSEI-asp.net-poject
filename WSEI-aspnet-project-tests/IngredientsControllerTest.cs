using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WSEI_aspnet_projekt.Controllers;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_project_tests
{
	class IngredientsControllerTest
	{
		private readonly Mock<IIngredientsService> _ingredientsService = new Mock<IIngredientsService>();
		private IngredientsController _ingredientsController;
		[OneTimeSetUp]
		public void Setup()
		{
			_ingredientsController = new IngredientsController(_ingredientsService.Object);
		}

		[Test]
		public void GetIngredientsTest()
		{
			List<Ingredient> expectedList = new List<Ingredient>();
			Ingredient ingredient1 = new Ingredient
			{
				Id = 1,
				Name = "Name1"
			};
			Ingredient ingredient2 = new Ingredient
			{
				Id = 2,
				Name = "Name2"
			};
			expectedList.Add(ingredient1);
			expectedList.Add(ingredient2);
			_ingredientsService.Setup(i => i.GetIngredients()).Returns(expectedList);

			List<Ingredient> resultList = _ingredientsController.GetIngredients();
			Assert.AreEqual(expectedList, resultList);
		}

		[Test]
		public void GetIngredientTest()
		{
			Ingredient expectedIngredient = new Ingredient();
			expectedIngredient.Id = 1;
			expectedIngredient.Name = "Name1";
			_ingredientsService.Setup(i => i.GetIngredient(1)).Returns(expectedIngredient);

			ActionResult <Ingredient> resultIngredient = _ingredientsController.GetIngredient(1);
			Assert.AreEqual(expectedIngredient.Id, resultIngredient.Value.Id);
			Assert.AreEqual(expectedIngredient.Name, resultIngredient.Value.Name);
		}

		[Test]
		public void PostIngredientTest()
		{
			_ingredientsService.Setup(i => i.AddIngredient(It.IsAny<Ingredient>(), "userId"))
				.Verifiable();
			Ingredient ingredient = new Ingredient();
			ingredient.Id = 1;

			_ingredientsController.PostIngredient(ingredient);
			_ingredientsService.Verify(i => i.AddIngredient(It.IsAny<Ingredient>(), "userId"), Times.Once);
		}


	}
}
