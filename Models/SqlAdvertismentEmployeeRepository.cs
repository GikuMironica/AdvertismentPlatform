using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public class SqlAdvertismentEmployeeRepository : IAdvertismentRepository
    {

        private readonly AppDbContext context;

        public SqlAdvertismentEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task Add(Advertisment advertisment)
        {
            context.Advertisments.Add(advertisment);

            await context.SaveChangesAsync();
           
        }

        public async Task Delete(int Id)
        {
            Advertisment ad = await context.Advertisments.FindAsync(Id);
            if(ad != null)
            {
                context.Advertisments.Remove(ad);
                await context.SaveChangesAsync();
            }
            
        }
        
        public async Task<Advertisment> GetAdvertisment(int Id)
        {
            return await context.Advertisments
                .Include(a => a.Item)
                .SingleOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<IEnumerable<Advertisment>> GetAdvertisments()
        {
            return context.Advertisments
                .Include(ad => ad.Item);
        }

        public async Task Update(Advertisment advertisment)
        {
            var ad = context.Advertisments.Attach(advertisment);
            ad.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
