using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using WSEI_aspnet_projekt.Controllers;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_project_tests
{
	class RecipesControllerTests
	{
		private Mock<IRecipesService> _recipesService = new Mock<IRecipesService>();
		private RecipesController _recipesController;
		[OneTimeSetUp]
		public void Setup()
		{
			_recipesController = new RecipesController(_recipesService.Object);
			
			var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.NameIdentifier, "user1"),
			}, "mock"));
			
			_recipesController.ControllerContext = new ControllerContext()
			{
				HttpContext = new DefaultHttpContext() { User = user }
			};
		}

		[Test]
		public void GetRecipeTest()
		{
			var expectedRecipe = new Recipe()
			{
				Description = "Test description",
				Hidden = false,
				Id = 1,
				Name = "Test Recipe Name",
				UserId = "test-user-id"
			};
			_recipesService.Setup(i => i.GetRecipe(1)).Returns(expectedRecipe);

			var result = _recipesController.GetRecipe(1).Value;
			
			Assert.AreEqual(expectedRecipe, result);
		}

		[Test]
		public void GetCurrentUserRecipesTest()
		{
			List<Recipe> list = new List<Recipe>();
			var recipe1 = new Recipe()
			{
				Description = "Test description 1",
				Hidden = false,
				Id = 1,
				Name = "Test Recipe Name 1",
				UserId = "user1"
			};
			var recipe2 = new Recipe()
			{
				Description = "Test description 2",
				Hidden = false,
				Id = 2,
				Name = "Test Recipe Name 2",
				UserId = "user2"
			};
			var recipe3 = new Recipe()
			{
				Description = "Test description 3",
				Hidden = false,
				Id = 3,
				Name = "Test Recipe Name 3",
				UserId = "user1"
			};
			list.Add(recipe1);
			list.Add(recipe2);
			list.Add(recipe3);
			
			var expectedList = list.Where(o => o.UserId.Equals("user1"))
				.ToList();
			
			_recipesService.Setup(i => i.GetUserRecipes("user1"))
				.Returns(expectedList);

			var resultList = _recipesController.GetCurrentUserRecipes().Value;
			
			Assert.AreEqual(expectedList, resultList);
		}

		[Test]
		public void GetAllRecipesTest()
		{
			List<Recipe> listExpected = new List<Recipe>();
			var recipe1 = new Recipe()
			{
				Description = "Test description 1",
				Hidden = false,
				Id = 1,
				Name = "Test Recipe Name 1",
				UserId = "user1"
			};
			var recipe2 = new Recipe()
			{
				Description = "Test description 2",
				Hidden = false,
				Id = 2,
				Name = "Test Recipe Name 2",
				UserId = "user2"
			};
			var recipe3 = new Recipe()
			{
				Description = "Test description 3",
				Hidden = false,
				Id = 3,
				Name = "Test Recipe Name 3",
				UserId = "user1"
			};
			listExpected.Add(recipe1);
			listExpected.Add(recipe2);
			listExpected.Add(recipe3);
			
			_recipesService.Setup(i => i.GetRecipes())
				.Returns(listExpected);
			
			var result = _recipesController.GetRecipes();
			
			Assert.AreEqual(result, listExpected);
		}

		[Test]
		public void PutRecipeTest()
		{
			var recipe = new Recipe()
			{
				Id = 1,
				UserId = "user1"
			};

			_recipesService.Setup(i => i.GetRecipe(It.IsAny<int>())).Returns(recipe);
			_recipesService.Setup(i => i.UpdateRecipe(1, recipe)).Returns(new MyResponse(true));
			
			var result = _recipesController.PutRecipe(1, recipe);
			
			_recipesService.Verify(i => i.UpdateRecipe(1, recipe), Times.Once);
			
			Assert.IsNotNull(result);
		}

		[Test]
		public void PostRecipeTest()
		{
			Recipe recipe = new Recipe();

			_recipesService.Setup(i => i.AddRecipe(recipe));

			var result = _recipesController.PostRecipe(recipe).Result;
			
			_recipesService.Verify(i => i.AddRecipe(recipe), Times.Once);
			Assert.IsInstanceOf<CreatedAtActionResult>(result);
		}

		[Test]
		public void PostRecipeWithIngredientsTest()
		{
			RecipeWithIngredients recipe = new RecipeWithIngredients();

			_recipesService.Setup(i => i.AddRecipeWithIngredients(It.IsAny<RecipeWithIngredients>(), "user1"));

			var result = _recipesController.PostRecipeWithIngredients(It.IsAny<RecipeWithIngredients>()).Result;
			
			_recipesService.Verify(i => i.AddRecipeWithIngredients(It.IsAny<RecipeWithIngredients>(), "user1"), Times.Once);
			Assert.IsInstanceOf<ContentResult>(result);
		}

		[Test]
		public void DeleteRecipeTest()
		{
			var recipe = new Recipe()
			{
				Id = 1,
				UserId = "user1"
			};
			
			_recipesService.Setup(i => i.DeleteRecipe(1)).Returns(new MyResponse(true));
			_recipesService.Setup(i => i.GetRecipe(1)).Returns(recipe);

			var result = _recipesController.DeleteRecipe(1).Result;
			
			_recipesService.Verify(i => i.DeleteRecipe(1), Times.Once);
			
			Assert.IsInstanceOf<ContentResult>(result);
		}
	}
}
