using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models.MySqlRepository
{
    public class AutoRepository : BaseRepository<AutoItem>, IAutoItemRepository
    {
        public AutoRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<AutoItem> GetById(int id)
        {
            var item = await context.Set<AutoItem>().FindAsync(id);
            return item;
        }

        public override async Task<IEnumerable<AutoItem>> GetAll()
        {
            return await context.AutoItems
                .Include(it => it.Advertisment)
                    .ThenInclude(ad => ad.ApplicationUser)
                .ToListAsync();
        }
    }
}
