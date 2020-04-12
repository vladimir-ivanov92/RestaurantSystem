namespace RestaurantSystem.Web.ViewModels.RecipeSteps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Ganss.XSS;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class RecipeStepViewModel : IMapFrom<RecipeStep>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public int Minutes { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public virtual Item Item { get; set; }
    }
}
