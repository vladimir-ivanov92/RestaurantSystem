namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRecipeStepService
    {
        Task AddRecipeStepToItem(string description, int minutes, string itemName);

        T GetById<T>(int id);

        Task EditRecipeToItem(string description, int minutes, string itemName, int id);

        Task DeleteRecipeToItem(int id);
    }
}
