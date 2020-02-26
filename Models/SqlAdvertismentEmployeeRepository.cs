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
            context.advertisments.Add(advertisment);
            await context.SaveChangesAsync();
           
        }

        public async Task Delete(int Id)
        {
            Advertisment ad = await context.advertisments.FindAsync(Id);
            if(ad != null)
            {
                context.advertisments.Remove(ad);
                await context.SaveChangesAsync();
            }
            
        }

        public async Task<Advertisment> GetAdvertisment(int Id)
        {
            return await context.advertisments.FindAsync(Id);
        }

        public async Task<IEnumerable<Advertisment>> GetAdvertisments()
        {
            return context.advertisments;
        }

        public async Task Update(Advertisment advertisment)
        {
            var ad = context.advertisments.Attach(advertisment);
            ad.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
