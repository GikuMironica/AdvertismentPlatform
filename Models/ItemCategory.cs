using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvertismentPlatform.Models
{
    [Table("items")]
    public abstract class ItemCategory
    {
        [Required]
        public int Id { get; set; }
        public double? Price { get; set; }

        [MaxLength(50)]
        public string? Brand { get; set; }

        [MaxLength(500)]
        [MinLength(20)]
        [Required]
        public string Description { get; set; }

        public int AdvertismentID { get; set; }

        public Advertisment Advertisment { get; set; }
        public abstract string ItemType { get; set; }
    }
}