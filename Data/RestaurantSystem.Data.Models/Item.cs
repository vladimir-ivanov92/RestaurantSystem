namespace RestaurantSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Item
    {
        public Item()
        {
            this.OrderItems = new List<OrderItem>();
        }

        public int ItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the needed time to prepare the item. It will be something between 1 and 30 minutes.
        /// </summary>
        public int PreparationTime { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool Vegeterian { get; set; }

        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
