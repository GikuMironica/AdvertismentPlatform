using AdvertismentPlatform.Domain;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static AdvertismentPlatform.Domain.Category;

namespace AdvertismentPlatform.Services
{
    public class CategoryContainer : ICategory
    {

        private readonly IWebHostEnvironment iHostingEnvironment;
        private readonly IEnumerable<Category> categories;

        public CategoryContainer(IWebHostEnvironment IHostingEnvironment)
        {
            iHostingEnvironment = IHostingEnvironment;

            var path = Path.Combine(iHostingEnvironment.WebRootPath, "Resource", "ItemCategories.json");
            string jsonText = null;
            using (var reader = new StreamReader(path))
            {
                jsonText = reader.ReadToEnd();
            };
            categories = JsonConvert.DeserializeObject<List<Category>>(jsonText);
        }

        public string GetCategoryById(int id)
        {
            var result = categories.FirstOrDefault(c => c.Id == id);
            if (result != null)
                return result.Type;
            else return null;
        }

        public int GetIdByCategory(string category)
        {
            var result = categories.FirstOrDefault(c => c.Type == category);
            if (result != null)
                return result.Id;
            else return 0;
        }
    }
}
