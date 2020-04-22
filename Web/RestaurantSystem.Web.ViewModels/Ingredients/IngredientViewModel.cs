namespace RestaurantSystem.Web.ViewModels.Ingredients
{
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        [Required]
        [Range(1,100)]
        public int? Quantity { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }
    }
}
