using AdvertismentPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Filter.Base
{
    public class GreaterMileageCriteria : ICriteria
    {
        private int mileageCriteria;
        public GreaterMileageCriteria(int mileageCriteria)
        {
            this.mileageCriteria = mileageCriteria;
        }
        public IEnumerable<Advertisment> MeetCriteria(IEnumerable<Advertisment> advertisments)
        {
            List<Advertisment> ads = advertisments.Where(ad => ad.Item.Mileage >= mileageCriteria).ToList();
            return ads;
        }
    }
}
