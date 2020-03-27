using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.ViewModels
{
    public class CreateCarViewModel
    {
        [MaxLength(50)]
        public string Title { get; set; }

        public string? Picture { get; set; }

        [StringLength(2)]
        [Range(1, 60)]
        public int? Seats { get; set; }

        [Display(Name = "Car Type")]
        public string? Car_Type { get; set; }

        [StringLength(1)]
        [Range(1,9)]
        public int? Doors { get; set; }

        public double? Price { get; set; }

        [MaxLength(50)]
        [Display(Name = "Make, Model")]
        public string? Brand { get; set; }

        [MaxLength(500, ErrorMessage = " Description can contain maximum 500 characters")]
        [MinLength(20, ErrorMessage = " Description must contain maximum 20 characters")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Production year")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProductAge { get; set; }

        [StringLength(7)]
        public int? Mileage { get; set; }

        public SelectList CarTypes { get; set; }
    }
}
