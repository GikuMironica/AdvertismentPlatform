using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public interface IAdvertismentRepository : IRepository<Advertisment>
    {
        public Task<IEnumerable<Advertisment>> GetAllByUserId(string id);
    }
}
