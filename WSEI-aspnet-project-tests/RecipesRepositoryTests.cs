using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using WSEI_aspnet_projekt.Data;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Repositories;

namespace WSEI_aspnet_project_tests
{
    
    public class RecipesRepositoryTests
    {

        [Test]
        public void GetRecipes_Returns_UserRecipes()
        {
            var recipe1 = new Recipe()
            {
                Description = "Test description 1",
                Id = 1,
                Name = "Test Recipe Name 1",
                UserId = "user1"
            };
            var recipe2 = new Recipe()
            {
                Description = "Test description 2",
                Id = 2,
                Name = "Test Recipe Name 2",
                UserId = "user2"
            };
            var recipe3 = new Recipe()
            {
                Description = "Test description 3",
                Id = 3,
                Name = "Test Recipe Name 3",
                UserId = "user1"
            };

            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                context.Recipes.Add(recipe1);
                context.Recipes.Add(recipe2);
                context.Recipes.Add(recipe3);
                context.SaveChanges();

            }

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                
                var expectedList = new List<Recipe>()
                {
                    recipe1, recipe2, recipe3
                };
                
                var recipesRepository = new RecipesRepository(context);

                var result = recipesRepository.GetRecipes();
                
                Assert.AreEqual(3, context.Recipes.Count());
                Assert.IsTrue(result.Any(r => r.Id == recipe1.Id));
                Assert.IsTrue(result.Any(r => r.Id == recipe2.Id));
                Assert.IsTrue(result.Any(r => r.Id == recipe3.Id));
            };
        }

        [Test]
        public void GetRecipes_Returns_Empty_When_NoRecipes()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                var recipesRepository = new RecipesRepository(context);

                var result = recipesRepository.GetRecipes();
                
                Assert.AreEqual(0, context.Recipes.Count());
                Assert.IsEmpty(result);
            };
        }
        
        [Test]
        public void GetRecipe_Returns_ExistingRecipe()
        {
            var recipe1 = new Recipe()
            {
                Description = "Test description 1",
                Id = 1,
                Name = "Test Recipe Name 1",
                UserId = "user1"
            };
            var recipe2 = new Recipe()
            {
                Description = "Test description 2",
                Id = 2,
                Name = "Test Recipe Name 2",
                UserId = "user2"
            };
            var recipe3 = new Recipe()
            {
                Description = "Test description 3",
                Id = 3,
                Name = "Test Recipe Name 3",
                UserId = "user1"
            };
            
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                context.Recipes.Add(recipe1);
                context.Recipes.Add(recipe2);
                context.Recipes.Add(recipe3);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                var recipesRepository = new RecipesRepository(context);
                var result = recipesRepository.GetRecipe(2);

                Assert.AreEqual(recipe2.Id, result.Id);
                Assert.AreEqual(recipe2.Description, result.Description);
                Assert.AreEqual(recipe2.Name, result.Name);
            }
        }
        
        [Test]
        public void GetRecipe_Returns_Null_When_NoId()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {

            }

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                var recipesRepository = new RecipesRepository(context);
                var result = recipesRepository.GetRecipe(4);
                
                Assert.IsNull(result);
            }
        }

        [Test]
        public void PutRecipe_Updates_Existing_Recipe()
        {
            var recipe2 = new Recipe()
            {
                Description = "Test description 1 - update",
                Id = 1,
                Name = "Test Recipe Name 1 - update",
                UserId = "user1"
            };
            
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());
            
            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                var recipe = new Recipe()
                {
                    Description = "Test description 1",
                    Id = 1,
                    Name = "Test Recipe Name 1",
                    UserId = "user1"
                };
                
                context.Recipes.Add(recipe);
                context.SaveChangesAsync();
                context.Entry(recipe).State = EntityState.Detached;
                
                var recipesRepository = new RecipesRepository(context);
                
                recipesRepository.PutRecipe(recipe2);

            };

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                Assert.AreEqual(1, context.Recipes.Count());
            }
            
        }
        
        [Test]
        public void PutRecipe_Throws_Exception_When_NoRecipe()
        {
            var recipe = new Recipe()
            {
                Description = "Test description 1 - update",
                Id = 1,
                Name = "Test Recipe Name 1 - update",
                UserId = "user1"
            };
            
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());

            using(var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                var recipesRepository = new RecipesRepository(context);
                
                Assert.That(() => recipesRepository.PutRecipe(recipe), Throws.Exception);
            }
            
        }

        [Test]
        public void PostRecipe_Adds_NewRecipe()
        {
            var recipe = new Recipe()
            {
                Description = "Test description 1",
                Id = 1,
                Name = "Test Recipe Name 1",
                UserId = "user1"
            };
            
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());
            
            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                var recipesRepository = new RecipesRepository(context);
                recipesRepository.PostRecipe(recipe);
            };

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                Assert.AreEqual(1, context.Recipes.Count());
                Assert.IsTrue(context.Recipes.Contains(recipe));
            }
        }

        [Test]
        public void DeleteRecipe_Deletes_Existing_Recipe()
        {
            var recipe1 = new Recipe()
            {
                Description = "Test description 1",
                Id = 1,
                Name = "Test Recipe Name 1",
                UserId = "user1"
            };
            var recipe2 = new Recipe()
            {
                Description = "Test description 2",
                Id = 2,
                Name = "Test Recipe Name 2",
                UserId = "user2"
            };
            
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());
            
            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                context.Recipes.Add(recipe1);
                context.Recipes.Add(recipe2);
                context.SaveChanges();
                
                var recipesRepository = new RecipesRepository(context);
                recipesRepository.DeleteRecipe(recipe2);

            };

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                Assert.AreEqual(1, context.Recipes.Count());
                Assert.IsTrue(context.Recipes.Contains(recipe1));
            }
        }
        
        [Test]
        public void Can_Delete_Existing_Recipe()
        {
            var recipe1 = new Recipe()
            {
                Description = "Test description 1",
                Id = 1,
                Name = "Test Recipe Name 1",
                UserId = "user1"
            };
            var recipe2 = new Recipe()
            {
                Description = "Test description 2",
                Id = 2,
                Name = "Test Recipe Name 2",
                UserId = "user2"
            };
            
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            var someOptions = Options.Create(new OperationalStoreOptions());
            
            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                context.Recipes.Add(recipe1);
                context.SaveChanges();
            };

            using (var context = new ApplicationDbContext(dbOptions, someOptions))
            {
                var recipesRepository = new RecipesRepository(context);

                Assert.AreEqual(1, context.Recipes.Count());
                Assert.IsTrue(context.Recipes.Contains(recipe1));
                Assert.That(() => recipesRepository.DeleteRecipe(recipe2), Throws.Exception);
            }
        }
    }
}