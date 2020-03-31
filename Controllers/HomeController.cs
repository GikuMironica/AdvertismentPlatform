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

namespace AdvertismentPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvertismentRepository advertismentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        

        public HomeController(ILogger<HomeController> logger, 
            IAdvertismentRepository advertismentRepository, 
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.advertismentRepository = advertismentRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index2()
        {
            var advertisments = await advertismentRepository.GetAll();           
            
            return View(advertisments);
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

      
    }
}
