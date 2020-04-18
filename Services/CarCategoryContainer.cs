using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.Domain;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace AdvertismentPlatform.Services
{
    public class CarCategoryContainer : ICartype
    {        
        private IEnumerable<CarType> carTypes { get; set; }
        private readonly IWebHostEnvironment hostingEnvironment;
        public CarCategoryContainer(IWebHostEnvironment hostEnvironment)
        {
            this.hostingEnvironment = hostEnvironment;
            var carModelsPath = Path.Combine(hostingEnvironment.WebRootPath, "Resource", "CarTypes.json");
            string jsonText = null;
            using (var reader = new StreamReader(carModelsPath))
            {
                jsonText = reader.ReadToEnd();
            };
            carTypes = JsonConvert.DeserializeObject<List<CarType>>(jsonText);            
        }

        public IEnumerable<string> GetTypeImages()
        {
            return carTypes.Select(i => i.svgPath);
        }

        public IEnumerable<string> GetTypeNames()
        {
            return carTypes.Select(i => i.type);
        }

        public IEnumerable<CarType> GetTypes()
        {
            return carTypes;
        }
    }
}
