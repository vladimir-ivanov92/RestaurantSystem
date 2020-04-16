using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Data
{
    public interface IIngredientService
    {
        Task AddIngredientToItem(string name, int quantity, string itemName);

        Task EditIngredientToItem(string name, int quantity, string itemName, int id);

        Task DeleteIngredientToItem(string name, int quantity, string itemName, int id);

        T GetById<T>(int id);
    }
}
