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

        /**
         * Matching formats:
         * 0176/21425679
         * 0157764123813
         * 09621 11 85 45
         * 0172 8622111
         * 08103 / 30 444 30
         * +49 176 3123 8343 
         * 
         * And many other german formats...
         */
        [Display(Name ="Phone Number")]
        [RegularExpression("(\\(?([\\d \\-\\)\\–\\+\\/\\(]+){6,}\\)?([ .\\-–\\/]?)([\\d]+))", ErrorMessage = "Must be a valid phone number")]
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
