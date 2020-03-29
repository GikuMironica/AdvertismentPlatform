using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.ViewModels
{
    public class EditAccountSettingsViewModel
    {
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        public string? City { get; set; }

        [Display(Name ="Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Must be a valid phone number")]
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
