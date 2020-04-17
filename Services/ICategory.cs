using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Services
{
    public interface ICategory
    {
        public string GetCategoryById(int id);

        public int GetIdByCategory(string category);
    }
}
