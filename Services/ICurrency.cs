using AdvertismentPlatform.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Services
{
    public interface ICurrency
    {
        string GetSignById(int id);
        
        public List<Currency> GetCurrencyList();

        public SelectList GetCurrencySelectList();

        public string GetNameById(int id);

        public Currency GetCurrencyById(int id);

        public IEnumerable<string> GetCurrencyNameList();

        public int GetIdByName(string name);

    }
}
