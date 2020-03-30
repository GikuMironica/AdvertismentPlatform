using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public abstract class BaseRepository<Template> : IRepository<Template> where Template : class
    {
        protected readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task Add(Template entity)
        {
            context.Set<Template>().Add(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<Template>> GetAll()
        {
            return await Task.Run(() => 
                context.Set<Template>());
        }

        public abstract Task<Template> GetById(int id);
      
        public async Task Delete(int Id)
        {
            var Entity = await context.Set<Template>().FindAsync(Id);
            if (Entity != null)
            {
                context.Set<Template>().Remove(Entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Template entity)
        {
            var Entity = context.Set<Template>().Attach(entity);
            Entity.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
