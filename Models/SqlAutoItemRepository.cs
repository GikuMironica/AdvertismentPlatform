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
                context.Items.Add(autoItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(int Id)
        {
            var item = await context.Items.FindAsync(Id);
            if( item != null)
            {
                context.Items.Remove(item);
                await context.SaveChangesAsync();
            }
           
        }

        public async Task<ItemCategory> GetItem(int Id)
        {
            var item = await context.Items.FindAsync(Id);
            if(item is AutoItem)
            {
                return (AutoItem)item;
            }

            return item;
        }

        public async Task<IEnumerable<ItemCategory>> GetItems()
        {
            return context.Items.Where(it => (it is AutoItem));
        }

        public async Task Update(ItemCategory itemChanged)
        {
            var item = context.Items.Add(itemChanged);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
