using AdvertismentPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Filter.Base
{
    public class GreaterYearCriteria : ICriteria
    {
        private int ageCriteria;
        public GreaterYearCriteria(int ageCriteria)
        {
            this.ageCriteria = ageCriteria;
        }
        public IEnumerable<Advertisment> MeetCriteria(IEnumerable<Advertisment> advertisments)
        {
            List<Advertisment> ads = advertisments.Where(ad => ad.Item.ProductAge.Value.Year > ageCriteria).ToList();
            return ads;
        }
    }
}
