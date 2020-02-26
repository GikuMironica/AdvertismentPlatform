using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // use TBH strategy to map the following models
            modelBuilder.Entity<ItemCategory>()
                .HasDiscriminator(it => it.ItemType)
                .HasValue<AutoItem>("auto_item");

            // Biderectional One to Many relationship
            modelBuilder.Entity<Advertisment>()
                .HasOne(a => a.Item)
                .WithOne(it => it.Advertisment)
                .HasForeignKey<ItemCategory>(it => it.AdvertismentID);

        }
    }
}
