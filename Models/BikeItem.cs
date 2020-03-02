using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    [Table("items")]
    public class BikeItem : ItemCategory
    {
        [StringLength(3)]
        public int? TopSpeed { get; set; }
    }
}
