using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public interface IAdvertismentRepository
    {
        public Task<Advertisment> GetAdvertisment(int Id);
        public Task<IEnumerable<Advertisment>> GetAdvertisments();

        public Task Add(Advertisment advertisment);

        public Task Update(Advertisment advertisment);

        public Task Delete(int Id);
    }
}
