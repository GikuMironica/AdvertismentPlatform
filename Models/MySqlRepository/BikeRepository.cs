using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models.MySqlRepository
{
    public class BikeRepository : BaseRepository<BikeItem>, IBikeItemRepository
    {
        public BikeRepository(AppDbContext context) : base(context)
        {
            
        }

        public override async Task<BikeItem> GetById(int id)
        {
            var item = await context.Set<BikeItem>().FindAsync(id);
            return item;
        }

        public override async Task<IEnumerable<BikeItem>> GetAll()
        {
            return await context.BikeItems
                .Include(it => it.Advertisment)
                    .ThenInclude(ad => ad.ApplicationUser)
                .ToListAsync();
        }
    }
}
