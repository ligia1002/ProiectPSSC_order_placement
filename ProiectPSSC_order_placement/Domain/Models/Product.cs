using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Models
{
    public class Product
    {
        public string ProductCode { get; private set; }
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Product(string productCode, string name, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(productCode)) throw new ArgumentException("Product code cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name cannot be null or empty.");
            if (unitPrice <= 0) throw new ArgumentException("Unit price must be greater than zero.");

            ProductCode = productCode;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
