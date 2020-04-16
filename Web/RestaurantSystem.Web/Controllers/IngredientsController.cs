namespace RestaurantSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.ViewModels.Ingredients;
    using RestaurantSystem.Web.ViewModels.Items;

    [Authorize(Roles = "Administrator")]
    public class IngredientsController : BaseController
    {
        private readonly IIngredientService ingredientService;
        private readonly IItemService itemService;

        public IngredientsController(IIngredientService ingredientService,  IItemService itemService)
        {
            this.ingredientService = ingredientService;
            this.itemService = itemService;
        }

        [HttpGet]
        public IActionResult AddIngredient([FromQuery(Name = "itemName")] string itemName)
        {
            var viewIngredientViewModel = new IngredientViewModel { Name = itemName };
            return this.View(viewIngredientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(string name, int quantity, string itemName)
        {
            await this.ingredientService.AddIngredientToItem(name, quantity, itemName);
            var viewModel = this.itemService.GetByName<ItemViewModel>(itemName);
            return this.RedirectToAction("ByName", "Items", viewModel);
        }

        [HttpGet]
        public IActionResult EditIngredient([FromQuery(Name = "itemName")] string itemName, [FromQuery(Name = "id")] int id)
        {
            var viewIngredientViewModel = this.ingredientService.GetById<IngredientViewModel>(id);
            viewIngredientViewModel.ItemName = itemName;
            return this.View(viewIngredientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditIngredient(string name, int quantity, string itemName, int id)
        {
            await this.ingredientService.EditIngredientToItem(name, quantity, itemName, id);
            var viewModel = this.itemService.GetByName<ItemViewModel>(itemName);
            return this.RedirectToAction("ByName", "Items", viewModel);
        }

        [HttpGet]
        public IActionResult DeleteIngredient([FromQuery(Name = "itemName")] string itemName, [FromQuery(Name = "id")] int id)
        {
            var viewIngredientViewModel = this.ingredientService.GetByName<IngredientViewModel>(id);
            viewIngredientViewModel.ItemName = itemName;
            return this.View(viewIngredientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIngredient(string name, int quantity, string itemName, int id)
        {
            await this.ingredientService.DeleteIngredientToItem(name, quantity, itemName, id);
            var viewModel = this.itemService.GetByName<ItemViewModel>(itemName);
            return this.RedirectToAction("ByName", "Items", viewModel);
        }
    }
}
