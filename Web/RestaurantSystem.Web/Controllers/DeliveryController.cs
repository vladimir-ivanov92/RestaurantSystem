using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Controllers
{
    public class DeliveryController : BaseController
    {
        public IActionResult Map()
        {
            return this.View();
        }
    }
}
