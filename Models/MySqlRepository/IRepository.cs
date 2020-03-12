using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public interface IRepository<Template>
    {
        public Task Add(Template entity);
        public Task TaskDelete(int Id);
        public Task<IEnumerable<Template>> GetAll();

        public Task Update(Template entity);

        public abstract Task<Template> GetById(int id);
    }
}
