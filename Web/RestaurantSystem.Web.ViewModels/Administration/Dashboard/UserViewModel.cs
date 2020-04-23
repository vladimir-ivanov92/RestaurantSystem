namespace RestaurantSystem.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        [Required]
        public string UserName { get; set; }
    }
}
