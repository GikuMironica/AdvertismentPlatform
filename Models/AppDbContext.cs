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

        public DbSet<ItemCategory> Items { get; set; }
        public DbSet<AutoItem> AutoItems { get; set; }
        public DbSet<BikeItem> BikeItems { get; set; }
        public DbSet<Advertisment> Advertisments { get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the TPH using Fluent.API
            modelBuilder.Entity<ItemCategory>()
                .ToTable("items")
                .HasDiscriminator<string>("item_type")
                .HasValue<AutoItem>("auto_type")
                .HasValue<BikeItem>("bike_type");

            modelBuilder.Entity<Advertisment>()
                .ToTable("advertisments");

            // One - to - One relationship between ItemCategory <-> Advertisment
            modelBuilder.Entity<Advertisment>()
                .HasOne(p => p.Item)
                .WithOne(a => a.Advertisment)
                .HasForeignKey<ItemCategory>(ic => ic.AdvertismentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
