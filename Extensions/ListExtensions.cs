using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Extensions
{
    public static class ListExtensions
    {
        public static void AddAllIfNotNull<T>(this List<T> list, IEnumerable<T> values)
        {
            foreach(var value in values)
            {
                if(value != null)
                {
                    list.Add(value);
                }
            }
        }
    }
}
