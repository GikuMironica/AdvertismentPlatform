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
        public async Task<IActionResult> Index()
        {
            var advertise = await advertismentRepository.GetById(2);
            /*    
              var user = new ApplicationUser
               {
                   UserName = "New",
                   Email = "hahaha@gmai.com",
                   City = "Chisinau"
               };
               var result = await userManager.CreateAsync(user, "Compot123!as");
               if (result.Succeeded)
               {
                   //
               }*/

            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
