using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace first_app_net_core
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products {get; set;}
        public DbSet<Category> Categories { get; set; }
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data Source=Shop.db")
                .UseLoggerFactory(MyLoggerFactory); 
        }

    }
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public decimal? Price { get; set; }


    }
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            AddProducts();
            
        }
        static void AddProducts()
        {
            using(var db = new ShopContext())
            {
                var products = new Product{Name = "Apple X", Price=13000};
                db.Products.Add(products);
                db.SaveChanges();
                Console.WriteLine("Veriler Eklendi.");
            }
        }
    }
}
