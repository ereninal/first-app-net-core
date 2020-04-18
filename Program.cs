using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            //AddProducts();
            //GetAllProducts();
            //GetProductById(2);
            //UpdateProduct(1);
            DeleteProduct(5);
            
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
        static void GetAllProducts()
        {
            using(var db = new ShopContext())
            {
                var products = db.Products.Select(p => new {
                    p.Name,
                    p.Price
                }).ToList();
                foreach (var item in products)
                {
                    Console.WriteLine($"Name :{item.Name} Price :{item.Price}");

                }
            }
        }
        static void GetProductById(int id)
        {
            using(var db = new ShopContext())
            {
                string msj ="";
                var result = db.Products.
                Where(p => p.Id == id)
                .FirstOrDefault();//Sorgu sonucu bir eleman döneceği için dönüşte eğer sorgu karlığı boş ise null döndürür
                msj = result == null ? msj ="Not id values" : msj =$"Name :{result.Name} Price :{result.Price}";
                Console.WriteLine(msj);

                
            }
        }
        static void UpdateProduct(int id)
        {
            using(var db = new ShopContext())
            {
                var query = new Product(){Id = id};
                db.Products.Attach(query);
                query.Price =4000;
                db.SaveChanges();
                Console.WriteLine("Entry updated");
            }
        }
        static void DeleteProduct(int id)
        {
            using(var db = new ShopContext())
            {
                var qeury = new Product(){Id = id};
                //db.Products.Remove(qeury);
                db.Entry(qeury).State = EntityState.Deleted;
                db.SaveChanges();
                Console.WriteLine("Entry deleted");
            }
        }
    }
}
