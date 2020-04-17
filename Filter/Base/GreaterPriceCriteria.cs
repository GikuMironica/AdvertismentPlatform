using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.Models;

namespace AdvertismentPlatform.Filter.Base
{
    public class GreaterPriceCriteria : ICriteria
    {
        private double priceCriteria;
        public GreaterPriceCriteria(double priceCriteria)
        {
            this.priceCriteria = priceCriteria;
        }
        public IEnumerable<Advertisment> MeetCriteria(IEnumerable<Advertisment> advertisments)
        {
            List<Advertisment> ads = advertisments.Where(ad => ad.Item.Price >= priceCriteria).ToList();
            return ads;
        }
    }
}
