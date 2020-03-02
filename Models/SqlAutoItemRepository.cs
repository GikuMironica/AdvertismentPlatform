using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public class SqlAutoItemRepository : IitemRepository
    {
        private readonly AppDbContext context;

        public SqlAutoItemRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Add(ItemCategory item)
        {
            if(item is AutoItem)
            {
                AutoItem autoItem = (AutoItem)item;
                context.AutoItems.Add(autoItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(int Id)
        {
            var item = await context.AutoItems.FindAsync(Id);
            if( item != null)
            {
                context.AutoItems.Remove(item);
                await context.SaveChangesAsync();
            }
           
        }

        public async Task<ItemCategory> GetItem(int Id)
        {
            var item = await context.AutoItems.FindAsync(Id);
            if(item is AutoItem)
            {
                return (AutoItem)item;
            }

            return item;
        }

        public async Task<IEnumerable<ItemCategory>> GetItems()
        {
            return context.AutoItems.Where(it => (it is AutoItem));
        }

        public async Task Update(AutoItem itemChanged)
        {
            var item = await context.AutoItems.AddAsync(itemChanged);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
