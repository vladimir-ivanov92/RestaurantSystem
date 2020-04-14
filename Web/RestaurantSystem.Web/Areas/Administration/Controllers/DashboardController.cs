namespace RestaurantSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Common;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.Controllers;
    using RestaurantSystem.Web.ViewModels.Administration.Dashboard;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : BaseController
    {
        private readonly ISettingsService settingsService;
        private readonly IDashboardService dashboardService;

        public DashboardController(ISettingsService settingsService, IDashboardService dashboardService)
        {
            this.settingsService = settingsService;
            this.dashboardService = dashboardService;
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
    }
}
