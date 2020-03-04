using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public interface IitemRepository
    {
        public Task<ItemCategory> GetItem(int Id);
        public  Task<IEnumerable<ItemCategory>> GetItems();

        public Task Add(ItemCategory item);

        public Task Update(AutoItem itemChanged);

        public Task Delete(int Id);
    }
}
