using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdvertismentPlatform.Models;
using AdvertismentPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdvertismentPlatform.Controllers
{
    
    public class AdvertismentController : Controller
    {
        private string carModelsJsonPath = null; 
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly IAdvertismentRepository advertismentRepository;

        public AdvertismentController(IWebHostEnvironment IHostingEnvironment,
                                      UserManager<ApplicationUser> userManager,
                                      IAdvertismentRepository repository)
        {
            hostingEnvironment = IHostingEnvironment;
            this.userManager = userManager;
            this.advertismentRepository = repository;
            carModelsJsonPath = Path.Combine(hostingEnvironment.WebRootPath, "Resource", "CarTypes.json");
        }

        [HttpGet]
        public IActionResult CreateAdvertise()
        {
            var model = new CreateBikeViewModel();
            model.ProductAge = DateTime.Today;
            BikeAndCarViewModel combinedModel = new BikeAndCarViewModel
            {
                car = initializeCarModel(),
                bike = model
            };
            return View(combinedModel);
        }
                
        [HttpGet]
        public IActionResult CreateCarAd()
        {            
            return View(initializeCarModel());                        
        }


        [HttpPost]
        public async  Task<IActionResult> CreateCarAd(BikeAndCarViewModel compositeModel)
        {
            if (ModelState.IsValid)
            {
                var model = compositeModel.car;
                string uniqueFileName = null;
                if (model.Picture != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "AdvertismentPictures");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var user = await userManager.GetUserAsync(User);

                if (user == null)
                {
                    ViewBag.ErrorMessage = "User cannot be found";
                    return View("NotFound");
                }
                else
                {
                    Advertisment advertisment = new Advertisment
                    {
                        Title = model.Title,
                        PostDate = DateTime.UtcNow,
                        ApplicationUser = user,
                        Item = new AutoItem
                        {
                            Price = Double.Parse(model.Price),
                            Brand = model.Brand,
                            Car_Type = model.Car_Type,
                            ProductAge = model.ProductAge,
                            Doors = Int32.Parse(model.Doors),
                            Description = model.Description,
                            Seats = Int32.Parse(model.Seats),
                            Mileage = Int32.Parse(model.Mileage)
                        },
                                               
                    };

                    if ( uniqueFileName != null)
                    {
                        advertisment.Picture = uniqueFileName;
                    }

                    try
                    {
                        await advertismentRepository.Add(advertisment);

                        ViewBag.Title = "Success";
                        ViewBag.OperationResult = "Success";
                        ViewBag.Message = "You have successfuly create a new article.\n";
                        ViewBag.Action = "ListMyArticles";
                        ViewBag.Controller = "Advertisment";
                        ViewBag.NextAction = "my articles";
                    }
                    catch(Exception e)
                    {
                        ViewBag.Title = "Fail";
                        ViewBag.OperationResult = "Failed to create new article";
                        ViewBag.Message = "Try again \n";
                        ViewBag.Action = "CreateCarAD";
                        ViewBag.Controller = "Advertisment";
                    }
                    
                    return View("ResultView");                    
                }                 
            }        
            return View(initializeCarModel());
        }


        [HttpGet]
        public IActionResult CreateBikeAd()
        {
            var model = new CreateBikeViewModel();
            model.ProductAge = DateTime.Today;
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> CreateBikeAd(BikeAndCarViewModel compositeModel)
        {
            if (ModelState.IsValid)
            {
                var model = compositeModel.bike;
                string uniqueFileName = null;
                if (model.Picture != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "AdvertismentPictures");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var user = await userManager.GetUserAsync(User);

                if (user == null)
                {
                    ViewBag.ErrorMessage = "User cannot be found";
                    return View("NotFound");
                }
                else
                {
                    Advertisment advertisment = new Advertisment
                    {
                        Title = model.Title,
                        PostDate = DateTime.UtcNow,
                        ApplicationUser = user,
                        Item = new BikeItem
                        {
                            Price = Double.Parse(model.Price),
                            Brand = model.Brand,
                            ProductAge = model.ProductAge,
                            Description = model.Description                           
                        }
                                                
                    };

                    if (model.Mileage != null) advertisment.Item.Mileage = Int32.Parse(model.Mileage);
                    if (model.TopSpeed != null) ((BikeItem)(advertisment.Item)).TopSpeed = Int32.Parse(model.TopSpeed);
                    if (model.Picture != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "AdvertismentPictures");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    advertisment.Picture = uniqueFileName;
/*
                    if (uniqueFileName != null)
                    {
                        advertisment.Picture = uniqueFileName;
                    }
*/
                    try
                    {
                        await advertismentRepository.Add(advertisment);

                        ViewBag.Title = "Success";
                        ViewBag.OperationResult = "Success";
                        ViewBag.Message = "You have successfuly created a new article.\n";
                        ViewBag.Action = "ListMyArticles";
                        ViewBag.Controller = "Advertisment";
                        ViewBag.NextAction = "my articles";
                    }
                    catch (Exception e)
                    {
                        ViewBag.Title = "Fail";
                        ViewBag.OperationResult = "Failed to create new article";
                        ViewBag.Message = "Try again \n";
                        ViewBag.Action = "CreateCarAD";
                        ViewBag.Controller = "Advertisment";
                    }

                    return View("ResultView");
                }
            }
            var renderModel = new CreateBikeViewModel();
            renderModel.ProductAge = DateTime.Today;
            return View(renderModel);           
        }

        /**
         * HttpGet variation of MyAdvertisments action method
         * asynchroniously fetches all advertisments of the current logged in user 
         * via IAdvertismentRepository interface
         * 
         * Returns a MyAdvertisments View with a list of Advertisments as Model
         */
        [HttpGet]
        public async Task<IActionResult> MyAdvertisments()
        {
            var user = await userManager.GetUserAsync(User);
            string id = user.Id;

            var advertisments = await advertismentRepository.GetAllByUserId(id);
            return View(advertisments);
        }


        public CreateCarViewModel initializeCarModel()
        {            
            string jsonText = null;
            // read Json File, deserialize it to List<string>
            using (var reader = new StreamReader(carModelsJsonPath))
            {
                jsonText = reader.ReadToEnd();
            };            
            var carModels = JsonConvert.DeserializeObject<List<string>>(jsonText);

            var types = new SelectList(carModels);
            var renderModel = new CreateCarViewModel();
            renderModel.CarTypes = types;
            renderModel.ProductAge = DateTime.Today;
            return renderModel;
        }
    }
}