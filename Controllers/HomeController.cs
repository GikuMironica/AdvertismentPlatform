using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdvertismentPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using X.PagedList;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using AdvertismentPlatform.Filter.Base;
using System.ComponentModel.DataAnnotations;
using AdvertismentPlatform.Attributes;
using Microsoft.AspNetCore.Http;
using AdvertismentPlatform.Services;

namespace AdvertismentPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvertismentRepository advertismentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment iHostingEnvironment;
        private readonly ICategory categoryContainer;

        public HomeController(ILogger<HomeController> logger, 
            IAdvertismentRepository advertismentRepository, 
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment IHostingEnvironment,
            ICategory categoryContainer)
        {
            _logger = logger;
            this.advertismentRepository = advertismentRepository;
            this.userManager = userManager;
            iHostingEnvironment = IHostingEnvironment;
            this.categoryContainer = categoryContainer;
        }
              
        [HttpGet]       
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery]int? page, string? search = null)
        {           
            int pageSize = 8;
            var pageNumber = page ?? 1;
                                 
            var ads = await advertismentRepository.GetForPageFormat(pageSize, pageNumber, search);                            
            return View(ads);
        }

        [HttpGet]
        [QueryString(new[] {"itemType","toPrice","fromPrice","fromYear","toYear","fromMileage","toMileage"}, true)]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery]int itemType, int? fromPrice, int? toPrice, int? fromYear, int? toYear, int? fromMileage, int? toMileage, int? page)
        {
            int pageSize = 8;
            var pageNumber = page ?? 1;
            
            var category = categoryContainer.GetCategoryById(itemType);

            if (fromPrice > toPrice) { var x = fromPrice; fromPrice = toPrice; toPrice = x; }
            if (fromYear > toYear) { var x = fromYear; fromYear = toYear; toYear = x; }
            if (fromMileage > toMileage) { var x = fromMileage; fromMileage = toMileage; toMileage = x; }


            var ads = await advertismentRepository.GetForPageFormat(category, fromPrice ?? 0, toPrice ?? Int32.MaxValue, fromYear ?? 0, toYear ?? DateTime.UtcNow.Year, fromMileage ?? 0, toMileage ?? Int32.MaxValue, pageSize, pageNumber);
            return View(ads);
        }

        [HttpGet("Home/ViewAdvertisment/{advId}")]
        [AllowAnonymous]

        public async Task<ActionResult> ViewAdvertisment(string advId)
        {
            var advertisment = await advertismentRepository.GetById(Int32.Parse(advId));
            return View(advertisment);
        }

        
    }
}
