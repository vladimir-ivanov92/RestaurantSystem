namespace RestaurantSystem.Web.ViewModels.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Ingredients;
    using RestaurantSystem.Web.ViewModels.RecipeSteps;

    public class ItemViewModel : IMapFrom<Item>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int PreparationTime { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }

        public IEnumerable<RecipeStepViewModel> RecipeSteps { get; set; }
    }
}
