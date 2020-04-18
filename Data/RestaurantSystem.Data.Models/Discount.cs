namespace RestaurantSystem.Data.Models
{
    public class Discount
    {
        public string DiscountId { get; set; }

        public string UserId { get; set; }

        public bool IsApplied { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
