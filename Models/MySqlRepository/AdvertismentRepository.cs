using AdvertismentPlatform.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
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
           
            return await context.Advertisments
                        .Include(ad => ad.ApplicationUser)
                        .Include(ad => ad.Item)
                        .Where(ad => ad.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1
                            || ad.Item.Brand.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1
                            || ad.Item.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1)
                        .OrderByDescending(x => x.PostDate)
                        .ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Advertisment>> GetForPageFormat(string itemType, int? fromPrice, int? toPrice, int? fromYear, int? toYear, int? fromMileage, int? toMileage, IEnumerable<string> carTypePreferences, int pageSize, int pageNumber)
        {
            ItemCategory itemCategory = null;

            switch (itemType)
            {
                case "Auto": itemCategory = new AutoItem(); break;
                case "Bike": itemCategory = new BikeItem(); break;
                default: itemCategory = null; break;
            }             

            var ads = await context.Advertisments
                        .Include(ad => ad.ApplicationUser)
                        .Include(ad => ad.Item)
                        .Where(ad => ad.Item.Price>=fromPrice && ad.Item.Price<=toPrice && ad.Item.ProductAge.Value.Year >= fromYear
                                && ad.Item.ProductAge.Value.Year <= toYear && ad.Item.Mileage >= fromMileage && ad.Item.Mileage <= toMileage
                        )
                        .OrderByDescending(x => x.PostDate)
                        .ToListAsync();
            if (itemCategory != null)
            {
                if (itemCategory.GetType().Name == (typeof(AutoItem).Name))
                    return await ads.Where(ad => ad.Item.GetType() == itemCategory.GetType()
                                            && (((AutoItem)(ad.Item)).Car_Type.EqualsAny(carTypePreferences))).ToPagedListAsync(pageNumber, pageSize);
                else if (itemCategory.GetType().Name == (typeof(BikeItem).Name))
                    return await ads.Where(ad => ad.Item.GetType() == itemCategory.GetType()).ToPagedListAsync(pageNumber, pageSize);
            }
            return await ads.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
