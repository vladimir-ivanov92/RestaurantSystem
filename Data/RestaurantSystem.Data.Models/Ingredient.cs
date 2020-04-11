namespace RestaurantSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Name { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
