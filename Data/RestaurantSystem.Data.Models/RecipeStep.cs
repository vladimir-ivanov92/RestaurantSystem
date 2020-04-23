namespace RestaurantSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class RecipeStep
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        [Range(0, 100)]
        public int Minutes { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
