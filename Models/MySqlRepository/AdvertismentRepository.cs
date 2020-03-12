using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public class AdvertismentRepository : BaseRepository<Advertisment>, IAdvertismentRepository
    {

        public AdvertismentRepository(AppDbContext context) : base(context)
        {
            
        }

        public override async Task<IEnumerable<Advertisment>> GetAll()
        {
            return await context.Advertisments
                 .Include(ad => ad.Item)
                 .ToListAsync();
        } 

        public override async Task<Advertisment> GetById(int id)
        {
            return await context.Advertisments
                .Include(advertisment => advertisment.Item)
                .Include(advertisment => advertisment.ApplicationUser)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
    }
}
