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
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> DeleteAdvertisment(string id)
        {
            int adId = Int32.Parse(id);
            try
            {
                await advertismentRepository.Delete(adId);
                return RedirectToAction("MyAdvertisments");
            }
            catch (Exception e)
            {
                return RenderErrorView();
            }
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
                    uniqueFileName = ProcessUploadedPhoto(model.Picture);
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
                        InitializeResultView(true, "You have successfuly created a new article", "MyAdvertisments", "Advertisment", "my articles");                      
                    }
                    catch(Exception e)
                    {
                        InitializeResultView(false, "Failed to create new article", "CreateCarAD", "Advertisment", "");                        
                    }
                    
                    return View("ResultView");                    
                }                 
            }
            compositeModel.car.CarTypes = initializeCarModel().CarTypes;
            return View(compositeModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditAutoItem(string id)
        {
            var incomingModel = await advertismentRepository.GetById(Int32.Parse(id));
            if (incomingModel == null)
            {
                ViewBag.ErrorMessage = $"Advertisment with Id = {id} cannot be found";
                return View("NotFound");
            }
            EditCarViewModel model = new EditCarViewModel
            {
                
                Title = incomingModel.Title,
                Description = incomingModel.Item.Description,
                ProductAge = incomingModel.Item.ProductAge,
                Price = (incomingModel.Item.Price).ToString(),
                Mileage = incomingModel.Item.Mileage.ToString(),
                PicturePath = incomingModel.Picture,
                Brand = incomingModel.Item.Brand,
                Doors = ((AutoItem)(incomingModel.Item)).Doors.ToString(),
                Seats = ((AutoItem)(incomingModel.Item)).Seats.ToString()
                
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditAutoItem(EditCarViewModel model)
        {

            if (ModelState.IsValid)
            {
                var editedCarAd = await advertismentRepository.GetById(model.Id);
                editedCarAd.Title = model.Title;
                editedCarAd.Item.Brand = model.Brand;
                editedCarAd.Item.Description = model.Description;
                editedCarAd.Item.Mileage = Int32.Parse(model.Mileage);
                ((AutoItem)(editedCarAd.Item)).Seats = Int32.Parse(model.Seats);
                ((AutoItem)(editedCarAd.Item)).Doors = Int32.Parse(model.Doors);
                ((AutoItem)(editedCarAd.Item)).Price = Double.Parse(model.Price);
                editedCarAd.Item.ProductAge = model.ProductAge;

                if (model.Picture != null)
                {
                    editedCarAd.Picture = ProcessUploadedPhoto(model.Picture);             
                }
                      
                    try
                    {
                        await advertismentRepository.Update(editedCarAd);
                        InitializeResultView(true, "You have successfuly updated this article", "Index", "Home", "Home");
                    }
                    catch (Exception e)
                    {
                        InitializeResultView(false, "Failed to update this article", "MyAdvertisments", "Advertisment", "");                       
                    }

                    return View("ResultView");                
            }      
            return View();
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
                    uniqueFileName = ProcessUploadedPhoto(model.Picture);
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
                        advertisment.Picture = uniqueFileName;
                    }

                    try
                    {
                        await advertismentRepository.Add(advertisment);
                        InitializeResultView(true, "You have successfuly created a new article", "MyAdvertisments", "Advertisment", "my articles");
                       
                    }
                    catch (Exception e)
                    {
                        InitializeResultView(true, "Failed to create new article", "CreateCarAD", "Advertisment", "");                        
                    }

                    return View("ResultView");
                }
            }
         return View(compositeModel);           
        }


        [HttpGet]
        public async Task<IActionResult> EditBikeItem(string id)
        {
            var incomingModel = await advertismentRepository.GetById(Int32.Parse(id));
                    
            if (incomingModel == null)
            {
                ViewBag.ErrorMessage = $"Advertisment with Id = {id} cannot be found";
                return View("NotFound");
            }
            EditBikeViewModel model = new EditBikeViewModel
            {
                Title = incomingModel.Title,
                Description = incomingModel.Item.Description,
                ProductAge = incomingModel.Item.ProductAge,
                Price = (incomingModel.Item.Price).ToString(),
                Mileage = incomingModel.Item.Mileage.ToString(),
                PicturePath = "~/AdvertismentPictures/" + ( incomingModel.Picture ?? "qm.jpg"),
                Brand = incomingModel.Item.Brand,
                TopSpeed = ((BikeItem)(incomingModel.Item)).TopSpeed.ToString()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditBikeItem(EditBikeViewModel model)
        {

            if (ModelState.IsValid)
            {
                var editedBikeAdvertise = await advertismentRepository.GetById(model.Id);
                editedBikeAdvertise.Title = model.Title;
                editedBikeAdvertise.Item.Brand = model.Brand;
                editedBikeAdvertise.Item.Description = model.Description;
                editedBikeAdvertise.Item.Mileage = Int32.Parse(model.Mileage);
                ((BikeItem)(editedBikeAdvertise.Item)).TopSpeed = Int32.Parse(model.TopSpeed);               
                editedBikeAdvertise.Item.ProductAge = model.ProductAge;
                editedBikeAdvertise.Item.Price = Double.Parse(model.Price);

                if (model.Picture != null)
                {
                    editedBikeAdvertise.Picture = ProcessUploadedPhoto(model.Picture);
                }

                try
                {
                    await advertismentRepository.Update(editedBikeAdvertise);
                    InitializeResultView(true, "You have successfuly updated this article", "Index", "Home", "Home");
                }
                catch (Exception e)
                {
                    InitializeResultView(false, "Failed to update this article", "MyAdvertisments", "Advertisment", "");
                }

                return View("ResultView");
            }
            return View();
        }


        private void InitializeResultView(bool result, string message, string action, string controller, string? nextAction)
        {
            ViewBag.Message = message + "\n";
            ViewBag.Action = action;
            ViewBag.Controller = controller;

            if (result)
            {
                ViewBag.Title = "Success";
                ViewBag.OperationResult = "Success";
                ViewBag.NextAction = nextAction;
            }
            else
            {
                ViewBag.Title = "Failed";
                ViewBag.OperationResult = "Failed";
            }

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

        private string ProcessUploadedPhoto(IFormFile photo)
        {
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "AdvertismentPictures");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            photo.CopyTo(new FileStream(filePath, FileMode.Create));

            return uniqueFileName;
        }

        private IActionResult RenderErrorView()
        {
            ViewBag.ErrorTitle = "System Error";
            ViewBag.ErrorMessage = "This advertisment couldn't be deleted\nDevelopers team is already informed, solution will be found soon.";
            return View("Error");
        }
    }
}