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

namespace AdvertismentPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvertismentRepository advertismentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment iHostingEnvironment;

        public HomeController(ILogger<HomeController> logger, 
            IAdvertismentRepository advertismentRepository, 
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment IHostingEnvironment)
        {
            _logger = logger;
            this.advertismentRepository = advertismentRepository;
            this.userManager = userManager;
            iHostingEnvironment = IHostingEnvironment;
        }
              
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? page)
        {
           
            int pageSize = 8;
            var pageNumber = page ?? 1;
            var ads = await advertismentRepository.GetForPageFormat(pageSize, pageNumber);
           
            return View(ads);
        }

        [HttpGet]
        public async Task<ActionResult> ViewAdvertisment(int advId)
        {
            var advertisment = await advertismentRepository.GetById(advId);
            return View(advertisment);
        }

    }
}
