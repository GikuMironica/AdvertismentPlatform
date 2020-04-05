using AdvertismentPlatform.Domain;
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

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh-mm}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

        public string? Picture { get; set; }

        public ItemCategory Item { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        [NotMapped]
        public Currency? Currency { get; set; }
    }
}
