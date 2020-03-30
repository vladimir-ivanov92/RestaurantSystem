namespace RestaurantSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using RestaurantSystem.Data.Common.Repositories;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Mapping;

    public class ItemService : IItemService
    {
        private readonly IRepository<Item> itemsRepository;

        public ItemService(IRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Item> query = this.itemsRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            string nameDB = name.Replace('-', ' ');
            var item = this.itemsRepository.All().Where(x => x.Name == nameDB).To<T>().FirstOrDefault();
            return item;
        }
    }
}
