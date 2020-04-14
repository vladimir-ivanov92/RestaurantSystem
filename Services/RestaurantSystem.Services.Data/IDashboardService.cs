namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDashboardService
    {
        int ItemQuantityPerMenu(int number);

        int GetOrdersPerDay(DateTime date);

        decimal GetNetAmmountPerDay(DateTime date);

        bool CheckForSupply(int number);
    }
}
