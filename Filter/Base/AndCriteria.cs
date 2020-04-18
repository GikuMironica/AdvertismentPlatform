using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.Models;

namespace AdvertismentPlatform.Filter.Base
{
    public class AndCriteria : ICriteria
    {
        IEnumerable<ICriteria> criterias;

        public AndCriteria(IEnumerable<ICriteria> criterias)
        {
            this.criterias = criterias;
        }
        public IEnumerable<Advertisment> MeetCriteria(IEnumerable<Advertisment> advertisments)
        {
            IEnumerable<Advertisment> ads = advertisments;
            foreach(var criteria in criterias)
            {
                ads = criteria.MeetCriteria(ads);
            }
            return ads;
        }
    }
}
