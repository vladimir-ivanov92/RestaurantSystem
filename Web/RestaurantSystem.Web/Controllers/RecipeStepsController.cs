namespace RestaurantSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Data;
    using RestaurantSystem.Web.ViewModels.Items;
    using RestaurantSystem.Web.ViewModels.RecipeSteps;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = "Administrator")]
    public class RecipeStepsController : BaseController
    {
        private readonly IRecipeStepService recipeStepService;
        private readonly IItemService itemService;

        public RecipeStepsController(IRecipeStepService recipeStepService, IItemService itemService)
        {
            this.recipeStepService = recipeStepService;
            this.itemService = itemService;
        }

        [HttpGet]
        public IActionResult AddRecipeStep ([FromQuery(Name = "itemName")] string itemName)
        {
            var recipeStepViewModel = new RecipeStepViewModel { ItemName = itemName };
            return this.View(recipeStepViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipeStep(string description, int minutes, string itemName)
        {
            await this.recipeStepService.AddRecipeStepToItem(description, minutes, itemName);
            var viewModel = this.itemService.GetByName<ItemViewModel>(itemName);
            return this.RedirectToAction("ByName", "Items", viewModel);
        }

        [HttpGet]
        public IActionResult EditRecipeStep([FromQuery(Name = "itemName")] string itemName, [FromQuery(Name = "id")] int id)
        {
            var viewRecipeViewModel = this.recipeStepService.GetById<RecipeStepViewModel>(id);
            viewRecipeViewModel.ItemName = itemName;
            return this.View(viewRecipeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRecipeStep(string description, int minutes, string itemName, int id)
        {
            await this.recipeStepService.EditRecipeToItem(description, minutes, itemName, id);
            var viewModel = this.itemService.GetByName<ItemViewModel>(itemName);
            return this.RedirectToAction("ByName", "Items", viewModel);
        }

        [HttpGet]
        public IActionResult DeleteRecipeStep([FromQuery(Name = "itemName")] string itemName, [FromQuery(Name = "id")] int id)
        {
            var viewRecipeViewModel = this.recipeStepService.GetById<RecipeStepViewModel>(id);
            viewRecipeViewModel.ItemName = itemName;
            return this.View(viewRecipeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecipeStep(string itemName, int id, int minutes)
        {
            await this.recipeStepService.DeleteRecipeToItem(id);
            var viewModel = this.itemService.GetByName<ItemViewModel>(itemName);
            return this.RedirectToAction("ByName", "Items", viewModel);
        }
    }
}
