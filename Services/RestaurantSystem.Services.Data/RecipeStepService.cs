namespace RestaurantSystem.Services.Data
{
    using RestaurantSystem.Data.Common.Repositories;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RecipeStepService : IRecipeStepService
    {
        private readonly IRepository<Item> itemsRepository;
        private readonly IRepository<RecipeStep> recipeStepRepository;

        public RecipeStepService(IRepository<Item> itemsRepository, IRepository<RecipeStep> recipeStepRepository)
        {
            this.itemsRepository = itemsRepository;
            this.recipeStepRepository = recipeStepRepository;
        }

        public async Task AddRecipeStepToItem(string description, int minutes, string itemName)
        {
            var item = this.itemsRepository.All().Where(x => x.Name == itemName).FirstOrDefault();
            var recipeStep = new RecipeStep
            {
                Description = description,
                Minutes = minutes,
                ItemId = item.ItemId,
            };
            item.RecipeSteps.Add(recipeStep);
            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task DeleteRecipeToItem(int id)
        {
            var recipeStep = this.recipeStepRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.recipeStepRepository.Delete(recipeStep);
            await this.recipeStepRepository.SaveChangesAsync();
        }

        public async Task EditRecipeToItem(string description, int minutes, string itemName, int id)
        {
            var recipeStep = this.recipeStepRepository.All().Where(x => x.Id == id).FirstOrDefault();
            recipeStep.Description = description;
            recipeStep.Minutes = minutes;
            await this.recipeStepRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            var recipeStep = this.recipeStepRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return recipeStep;
        }
    }
}
