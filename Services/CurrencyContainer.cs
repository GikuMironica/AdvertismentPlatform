using AdvertismentPlatform.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Services
{
    public class CurrencyContainer : ICurrency
    {
        private readonly IWebHostEnvironment iHostingEnvironment;
        private readonly SelectList currencySelectList;
        private readonly List<Currency> currencyList;

        public CurrencyContainer(IWebHostEnvironment IHostingEnvironment)
        {
            iHostingEnvironment = IHostingEnvironment;

            var path = Path.Combine(iHostingEnvironment.WebRootPath, "Resource", "Currencies.json");
            string jsonText = null;
            using (var reader = new StreamReader(path))
            {
                jsonText = reader.ReadToEnd();
            };
            currencyList = JsonConvert.DeserializeObject<List<Currency>>(jsonText);
            currencySelectList = new SelectList(currencyList);
        }

        public List<Currency> GetCurrencyList()
        {
            return currencyList;
        }

        public IEnumerable<string> GetCurrencyNameList()
        {
            return currencyList.Select(c => c.Name).ToList();
        }

        public SelectList GetCurrencySelectList()
        {
            return currencySelectList;
        }

        public string GetSignById(int id)
        {
            return currencyList.SingleOrDefault(x => x.Id == id).Sign;
        }

        public string GetNameById(int id)
        {
            return currencyList.SingleOrDefault(x => x.Id == id).Name;
        }

        public Currency GetCurrencyById(int id)
        {
            return currencyList.SingleOrDefault(x => x.Id == id);
        }

        public int GetIdByName(string name)
        {
            return currencyList.SingleOrDefault(x => x.Name == name).Id;
        }
    }
}
