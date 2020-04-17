using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvertismentPlatform.Models
{
    [Table("items")]
    public abstract class ItemCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double? Price { get; set; }

        public int CurrencyId { get; set; }

#nullable enable
        [MaxLength(50)]
        public string? Brand { get; set; }

        [MaxLength(500,ErrorMessage =" Description can contain maximum 500 characters")]
        [MinLength(20, ErrorMessage = " Description must contain maximum 20 characters")]
        [Required]
        public string? Description { get; set; }

        public int AdvertismentId { get; set; }
        public Advertisment? Advertisment { get; set; }

        [Display(Name = "Production year")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ProductAge { get; set; }

        [StringLength(7)]
        public int? Mileage { get; set; }
                
    }
}