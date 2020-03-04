using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    [Table("items")]
    public class AutoItem : ItemCategory
    {
        [StringLength(2)]
        public int? Doors { get; set; }

        [MaxLength(2)]
        public string? Car_Type { get; set; }

        [StringLength(2)]
        public int? Seats { get; set; }
    }
}
