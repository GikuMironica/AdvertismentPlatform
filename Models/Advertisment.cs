using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    [Table("advertisments")]
    public class Advertisment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public DateTime PostDate { get; set; }

        public string? Picture { get; set; }

        [Required]
        public ItemCategory Item { get; set; }
    }
}
