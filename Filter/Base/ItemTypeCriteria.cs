using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.Models;

namespace AdvertismentPlatform.Filter.Base
{
    public class ItemTypeCriteria : ICriteria
    {
        private string searchCriteria;
        public ItemTypeCriteria(string searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }
        public List<Advertisment> MeetCriteria(List<Advertisment> advertisments)
        {
            throw new NotImplementedException();
        }
    }
}
