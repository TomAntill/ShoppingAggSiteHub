using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSite.DataModels
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemPrice> ItemPrice { get; set; }
        public DbSet<ItemRating> ItemRating { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.BrandName).IsRequired();
                entity.HasMany(e => e.Stores).WithOne(e => e.Brand).HasForeignKey(x => x.BrandId);
            });


            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.StoreName).IsRequired();
                entity.HasOne(e => e.Location).WithOne(e => e.Store).HasForeignKey<Location>(x => x.Id);
                entity.HasOne(e => e.Brand).WithMany(e => e.Stores).HasForeignKey(x => x.BrandId);
                entity.Property(e => e.Deleted).IsRequired().HasDefaultValue(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.AddressFirstLine).IsRequired();
                entity.Property(e => e.Town).IsRequired();
                entity.Property(e => e.County).IsRequired();
                entity.Property(e => e.PostCode).IsRequired();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.Store).WithMany(e => e.Items).HasForeignKey(x => x.StoreId);
                entity.HasOne(e => e.ItemRating).WithOne(e => e.Item).HasForeignKey<ItemRating>(x => x.ItemId);
                entity.Property(e => e.CurrentPrice).IsRequired();
                entity.Property(e => e.ItemName).IsRequired();
                entity.Property(e => e.Weight).IsRequired();
            });

            modelBuilder.Entity<ItemPrice>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(x => x.Item).WithMany(x => x.HistoricPrices).HasForeignKey(x => x.ItemId);
                entity.Property(e => e.Value).IsRequired();
                entity.Property(e => e.InEffectFrom).IsRequired();
            });

            modelBuilder.Entity<ItemRating>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.RatingDesc).IsRequired();
                entity.Property(e => e.FiveStarRating).IsRequired();
            });
        }
    }

    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public virtual List<Store> Stores { get; set; }
    }

    public class Store
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int LocationId { get; set; }
        public string StoreName { get; set; }
        public string StoreImageUrl { get; set; }
        public bool Deleted { get; set; }
        public Brand Brand { get; set; }
        public Location Location { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Location
    {
        public int Id { get; set; }
        public string AddressFirstLine { get; set; }
        public string AddressSecondLine { get; set; }
        public string AddressThirdLine { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public virtual Store Store { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int QualityRatingId { get; set; }
        public string ItemName { get; set; }
        public string ItemImageUrl { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Weight { get; set; }
        public virtual Store Store { get; set; }
        public virtual ItemRating ItemRating { get; set; }
        public virtual List<ItemPrice> HistoricPrices { get; set; }
    }

    public class ItemPrice
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal Value { get; set; }
        public DateTime InEffectFrom { get; set; }
        public Item Item { get; set; }
    }

    public class ItemRating
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string RatingDesc { get; set; }
        public decimal FiveStarRating { get; set; }
        public virtual Item Item { get; set; }
    }
}
