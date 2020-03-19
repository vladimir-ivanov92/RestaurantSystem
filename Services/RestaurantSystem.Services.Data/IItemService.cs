namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IItemService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
