using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.Models;

namespace AdvertismentPlatform.Filter.Base
{
    /// <summary>
    /// This criteria filters ads by the advertised Item Type - Car / Bike ...
    /// </summary>
    public class ItemTypeCriteria : ICriteria
    {
        private Object searchCriteria;
        public ItemTypeCriteria(Object searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }
        public IEnumerable<Advertisment> MeetCriteria(IEnumerable<Advertisment> advertisments)
        {
            List<Advertisment> ads = new List<Advertisment>();
            foreach(var ad in advertisments)
            {
                if (ad.Item.GetType() == searchCriteria.GetType())
                    ads.Add(ad);
            }
            return ads;
        }
    }
}
