namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RestaurantSystem.Data.Common.Repositories;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Ingredients;

    public class IngredientService : IIngredientService
    {
        private readonly IRepository<Item> itemsRepository;
        private readonly IRepository<Ingredient> ingrediantsRepository;

        public IngredientService(IRepository<Item> itemsRepository, IRepository<Ingredient> ingrediantsRepository)
        {
            this.itemsRepository = itemsRepository;
            this.ingrediantsRepository = ingrediantsRepository;
        }

        public async Task AddIngredientToItem(string name, int quantity, string itemName)
        {
            var item = this.itemsRepository.All().Where(x => x.Name == itemName).FirstOrDefault();
            var ingredient = new Ingredient
            {
                Name = name,
                Quantity = quantity,
                ItemId = item.ItemId,
            };
            item.Ingredients.Add(ingredient);
            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task DeleteIngredientToItem(string name, int quantity, string itemName, int id)
        {
            var ingredient = this.ingrediantsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.ingrediantsRepository.Delete(ingredient);
            await this.ingrediantsRepository.SaveChangesAsync();
        }

        public async Task EditIngredientToItem(string name, int quantity, string itemName, int id)
        {
            var ingredient = this.ingrediantsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            ingredient.Name = name;
            ingredient.Quantity = quantity;
            await this.ingrediantsRepository.SaveChangesAsync();
        }

        public T GetByName<T>(int id)
        {
            var ingredient = this.ingrediantsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return ingredient;
        }
    }
}
