using AdvertismentPlatform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Services
{
    public interface ICartype
    {
        public IEnumerable<string> GetTypeNames();

        public IEnumerable<string> GetTypeImages();

        public IEnumerable<CarType> GetTypes();
             
    }
}
