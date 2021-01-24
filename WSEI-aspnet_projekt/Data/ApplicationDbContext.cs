using WSEI_aspnet_projekt.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
		public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteRecipe>()
                .HasKey(f => new { f.UserId, f.RecipeId });

            modelBuilder.Entity<RecipeCategory>().HasData(
                new RecipeCategory() { Id = 1, Name = "Dania główne", ParentId = null },
                new RecipeCategory() { Id = 2, Name = "Zupy", ParentId = null },
                new RecipeCategory() { Id = 3, Name = "Napoje / koktajle", ParentId = null },
                new RecipeCategory() { Id = 4, Name = "Śniadania i kolacje", ParentId = null },
                new RecipeCategory() { Id = 5, Name = "Fast Food", ParentId = null },
                new RecipeCategory() { Id = 6, Name = "Słodkości", ParentId = null },
                new RecipeCategory() { Id = 7, Name = "Przekąski", ParentId = null },
                new RecipeCategory() { Id = 8, Name = "Sałatki", ParentId = null },

                new RecipeCategory() { Id = 9, Name = "Mięsne", ParentId = 1 },
                new RecipeCategory() { Id = 10, Name = "Rybne", ParentId = 1 },
                new RecipeCategory() { Id = 11, Name = "Wegetariańskie", ParentId = 1 },
                new RecipeCategory() { Id = 12, Name = "Krem", ParentId = 2 },
                new RecipeCategory() { Id = 13, Name = "Klasyczne", ParentId = 2 },
                new RecipeCategory() { Id = 14, Name = "Alkoholowe", ParentId = 3 },
                new RecipeCategory() { Id = 15, Name = "Bezalkoholowe", ParentId = 3 },
                new RecipeCategory() { Id = 16, Name = "Słodkie", ParentId = 4 },
                new RecipeCategory() { Id = 17, Name = "Słone", ParentId = 4 },
                new RecipeCategory() { Id = 18, Name = "Ciastka", ParentId = 6 },
                new RecipeCategory() { Id = 19, Name = "Desery", ParentId = 6 },
                new RecipeCategory() { Id = 20, Name = "Słodkie", ParentId = 7 },
                new RecipeCategory() { Id = 21, Name = "Słone", ParentId = 7 },
                new RecipeCategory() { Id = 22, Name = "Z mięsem", ParentId = 8 },
                new RecipeCategory() { Id = 23, Name = "Z rybą", ParentId = 8 },
                new RecipeCategory() { Id = 24, Name = "Z nabiałem", ParentId = 8 },
                new RecipeCategory() { Id = 25, Name = "Vege", ParentId = 8 }
            );

            modelBuilder.Entity<Recipe>()
                .Property(r => r.CreatedOn)
                .HasDefaultValueSql("getdate()");
        }
    }
}
