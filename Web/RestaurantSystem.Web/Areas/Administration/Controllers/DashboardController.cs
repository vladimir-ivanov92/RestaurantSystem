namespace RestaurantSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Common;
    using RestaurantSystem.Data;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.Controllers;
    using RestaurantSystem.Web.ViewModels.Administration.Dashboard;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : BaseController
    {
        private readonly ISettingsService settingsService;
        private readonly IDashboardService dashboardService;
        ApplicationDbContext dbContext;

        public DashboardController(ISettingsService settingsService, IDashboardService dashboardService, ApplicationDbContext dbContext)
        {
            this.settingsService = settingsService;
            this.dashboardService = dashboardService;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var lstModel = new List<IndexViewModel>();

            var list = new List<string> { "Breakfast Menu", "Lunch Menuu", "Dinner Menu" };

            for (int i = 1; i <= 3; i++)
            {
                var viewModel = new IndexViewModel
                {
                    Name = list[i - 1],
                    Quantity = this.dashboardService.ItemQuantityPerMenu(i),
                    SupplyNeed = this.dashboardService.CheckForSupply(i),
                };
                lstModel.Add(viewModel);
            }

            return this.View(lstModel);
        }

        public IActionResult GetOrderNumber()
        {
            var lstModel = new List<OrdersReportViewModel>();

            DateTime startDate = new DateTime(2020, 4, 9);
            DateTime stopDate = DateTime.Now;
            int interval = 1;

            for (DateTime dateTime = startDate;
                 dateTime < stopDate;
                 dateTime += TimeSpan.FromDays(interval))
            {
                var viewModel = new OrdersReportViewModel
                {
                    CreatedOn = dateTime,
                    Quantity = this.dashboardService.GetOrdersPerDay(dateTime),
                };
                lstModel.Add(viewModel);
            }

            return this.View(lstModel);
        }

        public IActionResult GetNetAmmount()
        {
            var lstModel = new List<NetReportViewModel>();

            DateTime startDate = new DateTime(2020, 4, 9);
            DateTime stopDate = DateTime.Now;
            int interval = 1;

            for (DateTime dateTime = startDate;
                 dateTime < stopDate;
                 dateTime += TimeSpan.FromDays(interval))
            {
                var viewModel = new NetReportViewModel
                {
                    CreatedOn = dateTime,
                    NetAmmount= this.dashboardService.GetNetAmmountPerDay(dateTime),
                };
                lstModel.Add(viewModel);
            }

            return this.View(lstModel);
        }

        [HttpGet]
        public IActionResult AssignRole()
        {
            var usersViewModel = new List<UserViewModel>();
            var rolesViewModel = new List<RoleViewModel>();

            foreach (var user in this.dbContext.Users)
            {
                var userViewModel = new UserViewModel
                {
                    UserName = user.UserName,
                };
                usersViewModel.Add(userViewModel);
            }

            foreach (var role in this.dbContext.Roles)
            {
                var roleViewModel = new RoleViewModel
                {
                    Name = role.Name,
                };
                rolesViewModel.Add(roleViewModel);
            }

            var assignRoleViewModel = new AssignRoleViewModel
            {
                Users = usersViewModel,
                Roles = rolesViewModel,
            };

            return this.View(assignRoleViewModel);
        }

        [HttpPost]
        public IActionResult AssignRole(string userName, string name)
        {
            var user = this.dbContext.Users.Where(x => x.UserName == userName).FirstOrDefault();
            var role = this.dbContext.Roles.Where(x => x.Name == name).FirstOrDefault();

            if (user != null && role != null)
            {
                IdentityUserRole<string> currentUserRole = this.dbContext.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();
                if (currentUserRole != null)
                {
                    this.dbContext.UserRoles.Remove(currentUserRole);
                }

                this.dbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                });
                this.dbContext.SaveChanges();
            }

            return this.Redirect("/Administration/Dashboard");
        }
    }
}
