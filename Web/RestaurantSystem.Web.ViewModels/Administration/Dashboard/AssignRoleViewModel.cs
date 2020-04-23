namespace RestaurantSystem.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RestaurantSystem.Services.Mapping;

    public class AssignRoleViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}
