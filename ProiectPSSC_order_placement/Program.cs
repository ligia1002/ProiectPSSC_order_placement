using System;
using ProiectPSSC_order_placement.DB;

namespace ProiectPSSC_order_placement
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ShopDbContext())
            {
                Console.WriteLine("Testing database connection...");

                // Adăugare test
                db.ProductEntities.Add(new ProductEntity
                {
                    ProductCode = "P001",
                    ProductName = "Example Product",
                    Price = 99.99M,
                    QuantityType = "pcs"
                });


                db.SaveChanges();

                Console.WriteLine("Product added successfully.");
            }
        }
    }
}
