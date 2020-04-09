﻿namespace RestaurantSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OrderItem
    {
        public int Quantity { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
