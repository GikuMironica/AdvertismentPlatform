using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public interface IAdvertismentRepository : IRepository<Advertisment>
    {
        public Task<IEnumerable<Advertisment>> GetAllByUserId(string id);

        public Task<IEnumerable<Advertisment>> GetForPageFormat(int pageSize, int pageNumber, string? search);

        public Task<IEnumerable<Advertisment>> GetForPageFormat(string itemType, int? fromPrice, int? toPrice, int? fromYear, int? toYear, int? fromMileage, int? toMileage, int pageSize, int pageNumber);
    }
}
