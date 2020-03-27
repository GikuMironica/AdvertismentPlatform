using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdvertismentPlatform.Controllers
{
    
    public class AdvertismentController : Controller
    {
        [HttpGet]
        public IActionResult CreateAdvertise()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCarAd()
        {
            List<string> car_types = new List<string>
            {
                "Sedan",
                "Coupe",
                "Hatchback",
                "Fastback",
                "GT",
                "Convertible",
                "Minivan",
                "SUV"
            };

            var types = new SelectList(car_types);
            var model = new CreateCarViewModel();
            model.CarTypes = types;

            return View(model);
        }
    }
}