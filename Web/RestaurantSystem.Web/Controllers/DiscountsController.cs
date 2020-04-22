using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Controllers
{
    public class DiscountsController : BaseController
    {
        private readonly IDiscountService discountService;

        public DiscountsController(IDiscountService discountService)
        {
            this.discountService = discountService;
        }

        [HttpGet]
        public IActionResult ApplyDiscountCode()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyDiscountCode(string discountCode)
        {
            if (discountCode == null)
            {
                return this.Redirect("/");
            }

            bool getDiscount = await this.discountService.CheckDiscountCode(discountCode);
            if (getDiscount == true)
            {
                return this.Redirect("/");
            }

            return this.View();
        }
    }
}
