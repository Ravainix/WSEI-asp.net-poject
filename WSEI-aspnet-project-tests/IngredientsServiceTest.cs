using Moq;
using NUnit.Framework;
using System.IO;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_project_tests
{
	class IngredientsServiceTest
	{
		private IIngredientsService _ingredientsService;
		private Mock<IIngredientsRepository> _ingredientsRepository = new Mock<IIngredientsRepository>();

		[OneTimeSetUp]
		public void Setup()
		{
			_ingredientsService = new IngredientsService(_ingredientsRepository.Object);
		}

		[Test]
		public void AddIngredientTest()
		{
			_ingredientsService.AddIngredient(It.IsAny<Ingredient>());
			_ingredientsRepository.Verify(r => r.PostIngredient(It.IsAny<Ingredient>()), Times.Once);
		}

		[Test]
		public void DeleteIngredientShouldNotDelete()
		{
			Ingredient nullIngredient = null;
			_ingredientsRepository.Setup(i => i.GetIngredient(1)).Returns(nullIngredient);
			MyResponse resultResponse = _ingredientsService.DeleteIngredient(1);
			MyResponse expectedResponse = new MyResponse(false, "Ingredient with id = " + 1 + " doesn't exist");

			Assert.AreEqual(resultResponse._message, expectedResponse._message);
			Assert.AreEqual(resultResponse._success, expectedResponse._success);
			_ingredientsRepository.Verify(r => r.GetIngredient(1), Times.Once);
			_ingredientsRepository.Verify(r => r.DeleteIngredient(nullIngredient), Times.Never);
		}

		[Test]
		public void DeleteIngredientShouldDelete()
		{
			Ingredient ingredient = new Ingredient();
			ingredient.Id = 2;
			_ingredientsRepository.Setup(i => i.GetIngredient(2)).Returns(ingredient);
			MyResponse resultResponse = _ingredientsService.DeleteIngredient(2);
			MyResponse expectedResponse = new MyResponse(true, "Ingredient id = " + ingredient.Id + " deleted successfully");

			Assert.AreEqual(resultResponse._message, expectedResponse._message);
			Assert.AreEqual(resultResponse._success, expectedResponse._success);
			_ingredientsRepository.Verify(r => r.GetIngredient(2), Times.Once);
			_ingredientsRepository.Verify(r => r.DeleteIngredient(ingredient), Times.Once);
		}

		[Test]
		public void GetIngredientTest()
		{
			_ingredientsService.GetIngredient(3);
			_ingredientsRepository.Verify(r => r.GetIngredient(3), Times.Once);
		}

		[Test]
		public void GetIngredientsTest()
		{
			_ingredientsService.GetIngredients();
			_ingredientsRepository.Verify(r => r.GetIngredients(), Times.Once);
		}

		[Test]
		public void UpdateIngredientShouldSuccessAndForSecondTryFails()
		{
			Ingredient ingredient = new Ingredient();
			MyResponse resultResponse = _ingredientsService.UpdateIngredient(5, ingredient);
			MyResponse expectedResponse = new MyResponse(true, "Ingredient updated successfully");

			Assert.AreEqual(resultResponse._message, expectedResponse._message);
			Assert.AreEqual(resultResponse._success, expectedResponse._success);
			_ingredientsRepository.Verify(r => r.GetIngredient(5), Times.Never);

			_ingredientsRepository.Setup(r => r.PutIngredient(It.IsAny<Ingredient>())).Throws(new IOException());
			resultResponse = _ingredientsService.UpdateIngredient(5, ingredient);
			expectedResponse = new MyResponse(false, "Ingredient with id = " + ingredient.Id + " doesn't exist");

			Assert.AreEqual(resultResponse._message, expectedResponse._message);
			Assert.AreEqual(resultResponse._success, expectedResponse._success);
			_ingredientsRepository.Verify(r => r.GetIngredient(5), Times.Once);
		}
	}
}
