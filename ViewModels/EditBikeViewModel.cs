﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.ViewModels
{
    public class EditBikeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public IFormFile? Picture { get; set; }

        public string? PicturePath { get; set; }
           
        [Required]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Must be a double or integer")]
        public string Price { get; set; }

        [MaxLength(50)]
        [Display(Name = "Make, Model")]
        [Required]
        public string Brand { get; set; }

        [MaxLength(500, ErrorMessage = " Description can contain maximum 500 characters")]
        [MinLength(20, ErrorMessage = " Description must contain maximum 20 characters")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Production Date")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ProductAge { get; set; }

        [StringLength(7)]
        [Required]
        [RegularExpression(@"\d*", ErrorMessage = "Must be a number")]
        public string? Mileage { get; set; }

        [StringLength(3)]
        [Required]
        [RegularExpression(@"\d*", ErrorMessage = "Must be a number")]
        public string? TopSpeed { get; set; }
    }
}
