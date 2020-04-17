using AdvertismentPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Filter.Base
{
    public class LessPriceCriteria : ICriteria
    {
        private double priceCriteria;
        public LessPriceCriteria(double priceCriteria)
        {
            this.priceCriteria = priceCriteria;
        }
        public IEnumerable<Advertisment> MeetCriteria(IEnumerable<Advertisment> advertisments)
        {
            List<Advertisment> ads = advertisments.Where(ad => ad.Item.Price <= priceCriteria).ToList();
            return ads;
        }
    }
        
}
