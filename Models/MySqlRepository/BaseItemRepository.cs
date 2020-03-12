using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public class BaseItemRepository : BaseRepository<ItemCategory>, IItemRepository<ItemCategory>
    {

        public BaseItemRepository(AppDbContext context) :base(context)
        {
          
        }
              
        public override async Task<IEnumerable<ItemCategory>> GetAll()
        {
            return await context.Items
                .Include(it => it.Advertisment)
                    .ThenInclude(ad => ad.ApplicationUser)
                .ToListAsync();                
        }

        public override async Task<ItemCategory> GetById(int id)
        {
            var item = await context.Set<ItemCategory>().FindAsync(id);
            return item;
        }
    }
}
