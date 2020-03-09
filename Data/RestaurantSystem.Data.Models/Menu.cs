namespace RestaurantSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Menu
    {
        public Menu()
        {
            this.Items = new HashSet<Item>();
        }

        public int MenuId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
