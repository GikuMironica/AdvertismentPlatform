﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.ViewModels
{
    public class BikeAndCarViewModel
    {
        public CreateBikeViewModel bike { get; set; }
        public CreateCarViewModel car { get; set; }

        [Required]
        public string RecaptchaToken { get; set; }

        [Display(Name = "Currency")]
        public SelectList Currencies { get; set; }
    }
}
