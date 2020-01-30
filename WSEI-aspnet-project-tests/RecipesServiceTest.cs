using Moq;
using NUnit.Framework;
using System.IO;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_project_tests
{

	public class RecipesServiceTest
	{
		private IRecipesService _recipesService;
		private Recipe recipe;
		private Mock<IRecipesRepository> _recipesRepository = new Mock<IRecipesRepository>();
		private Mock<IIngredientsRepository> _ingredientsRepository = new Mock<IIngredientsRepository>();

		[OneTimeSetUp]
		public void Setup()
		{
			_recipesService = new RecipesService(_recipesRepository.Object, _ingredientsRepository.Object);
		}

		[Test]
		public void GetRecipesTest()
		{
			_recipesService.GetRecipes();
			_recipesRepository.Verify(r => r.GetRecipes(), Times.Once);
		}

		[Test]
		public void GetRecipeTest()
		{
			_recipesService.GetRecipe(5);
			_recipesRepository.Verify(r => r.GetRecipe(5), Times.Once);
		}

		[Test]
		public void GetUserRecipesTest()
		{
			_recipesService.GetUserRecipes("userId");
			_recipesRepository.Verify(r => r.GetUserRecipes("userId"), Times.Once);
		}

		[Test]
		public void UpdateRecipeShouldSuccessAndForSecondTryFails()
		{
			recipe = new Recipe();
			MyResponse resultResponse = _recipesService.UpdateRecipe(95, recipe);
			MyResponse expectedResponse = new MyResponse(true, "Recipe updated successfully");

			Assert.AreEqual(resultResponse._message, "yhbg");
			Assert.AreEqual(resultResponse._success, expectedResponse._success);
			_recipesRepository.Verify(r => r.GetRecipe(2), Times.Never);
			
			_recipesRepository.Setup(r => r.PutRecipe(It.IsAny<Recipe>())).Throws(new IOException());
			resultResponse = _recipesService.UpdateRecipe(2, recipe);
			expectedResponse = new MyResponse(false, "Recipe with id = " + recipe.Id + " doesn't exist");

			Assert.AreEqual(resultResponse._message, expectedResponse._message);
			Assert.AreEqual(resultResponse._success, expectedResponse._success);
			_recipesRepository.Verify(r => r.GetRecipe(2), Times.Once);
		}

		[Test]
		public void AddRecipeTest()
		{
			recipe = new Recipe();
			_recipesService.AddRecipe(recipe);
			_recipesRepository.Verify(r => r.PostRecipe(recipe), Times.Once);
		}

		[Test]
		public void AddRecipeWithIngredientsTest()
		{
			recipe = new Recipe();
			recipe.Id = 0;
			Ingredient[] ingredients = new Ingredient[]
			{
				new Ingredient(),
				new Ingredient(),
				new Ingredient()
			};

			RecipeWithIngredients recipeWithIngredients = new RecipeWithIngredients();
			recipeWithIngredients.recipe = recipe;
			recipeWithIngredients.ingredients = ingredients;

			_recipesService.AddRecipeWithIngredients(recipeWithIngredients, "userId");
			_ingredientsRepository.Verify(r => r.PostIngredient(It.IsAny<Ingredient>()), Times.Exactly(3));
			_recipesRepository.Verify(r => r.PostRecipe(recipe), Times.Once);
		}

		[Test]
		public void DeleteRecipeShouldNotDelete()
		{
			recipe = null;
			_recipesRepository.Setup(r => r.GetRecipe(8)).Returns(recipe);

			MyResponse resultResponse = _recipesService.DeleteRecipe(8);
			MyResponse expectedResponse = new MyResponse(false, "Recipe with id = " + 8 + " doesn't exist");

			Assert.AreEqual(resultResponse._message, expectedResponse._message);
			Assert.AreEqual(resultResponse._success, expectedResponse._success);
			_recipesRepository.Verify(r => r.GetRecipe(8), Times.Once);
			_recipesRepository.Verify(r => r.DeleteRecipe(recipe), Times.Never);
		}

		[Test]
		public void DeleteRecipeShouldDelete()
		{
			recipe = new Recipe();
			recipe.Id = 10;
			_recipesRepository.Setup(r => r.GetRecipe(10)).Returns(recipe);

			MyResponse resultResponse = _recipesService.DeleteRecipe(10);
			MyResponse expectedResponse = new MyResponse(true, "Recipe id = " + recipe.Id + " deleted successfully");

			Assert.AreEqual(resultResponse._message, expectedResponse._message);
			Assert.AreEqual(resultResponse._success, expectedResponse._success);
			_recipesRepository.Verify(r => r.GetRecipe(10), Times.Once);
			_recipesRepository.Verify(r => r.DeleteRecipe(It.IsAny<Recipe>()), Times.Once);
		}
	}
}