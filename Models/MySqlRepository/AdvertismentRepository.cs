using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;


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
                 .Include(ad => ad.ApplicationUser)
                 .ToListAsync();
        } 

        public override async Task<Advertisment> GetById(int id)
        {
            return await context.Advertisments
                .Include(advertisment => advertisment.Item)
                .Include(advertisment => advertisment.ApplicationUser)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Advertisment>> GetAllByUserId(string id)
        {
            return await context.Advertisments
                 .Include(advertisment => advertisment.Item)
                 .Include(advertisment => advertisment.ApplicationUser)
                 .Where(advertisment => advertisment.ApplicationUserId == id)
                 .OrderByDescending(ad => ad.PostDate)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Advertisment>> GetForPageFormat(int pageSize, int pageNumber, string? search)
        {
            int ExcludeAds = (pageSize * pageNumber) - pageSize;

            return await context.Advertisments
                        .Include(ad => ad.ApplicationUser)
                        .Include(ad => ad.Item)
                        .Where(ad => ad.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1
                            || ad.Item.Brand.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1 
                            || ad.Item.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1 )
                        .OrderByDescending(x => x.PostDate)
                        .ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
