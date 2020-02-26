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

        public DbSet<ItemCategory> Items;
        public DbSet<Advertisment> advertisments;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // use TBH strategy to map the following models
            modelBuilder.Entity<ItemCategory>()
                .HasDiscriminator(it => it.ItemType)
                .HasValue<AutoItem>("auto_item");

            // Biderectional One to Many relationship between ItemCategory items and Advertisment
            modelBuilder.Entity<Advertisment>()
                .HasOne(a => a.Item)
                .WithOne(it => it.Advertisment)
                .HasForeignKey<ItemCategory>(it => it.AdvertismentID);

        }
    }
}
