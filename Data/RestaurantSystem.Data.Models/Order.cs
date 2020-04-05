namespace RestaurantSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RestaurantSystem.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {

        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

        public decimal NetAmount { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
