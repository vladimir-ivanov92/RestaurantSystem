using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantSystem.Web.ViewModels.Ingredients
{
    public class AllIngredientViewModel
    {
        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}
