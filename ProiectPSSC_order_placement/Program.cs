using ProiectPSSC_order_placement.Domain.Models;
using ProiectPSSC_order_placement.Domain.Services;
using ProiectPSSC_order_placement.Domain.ValueObjects;
using ProiectPSSC_order_placement.Domain.Workflows;
using ProiectPSSC_order_placement.Domain.Operations;
using System;
using System.Collections.Generic;

namespace OrderPlacementTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crează un serviciu fals pentru inventar
            IInventoryService inventoryService = new FakeInventoryService();
            IPriceService priceService = new FakePriceService();
            IProductService productService = new FakeProductService();

            // Creează un coș de cumpărături cu un ID de client
            var cart = new Cart(Guid.NewGuid());

            // Adaugă produse în coș
            cart.AddProduct("W1234", 2, inventoryService);  // Exemplu: Widget
            cart.AddProduct("G123", 1, inventoryService);   // Exemplu: Gizmo

            // Creează datele pentru client și adrese
            var customerInfo = new UnvalidatedCustomerInfo(Guid.NewGuid(), "John Doe", "johndoe@example.com");
            var shippingAddress = new UnvalidatedAddress("123 Main St", "City", "12345", "Country");
            var billingAddress = new UnvalidatedAddress("123 Main St", "City", "12345", "Country");

            // Crează și execută workflow-ul de plasare a comenzii
            var placeOrderWorkflow = new PlaceOrderWorkflow(inventoryService);
            var orderPlacedEvent = placeOrderWorkflow.Execute(cart, customerInfo, shippingAddress, billingAddress);

            // Afișează rezultatul (evenimentul comenzii plasate)
            Console.WriteLine($"Order placed event: {orderPlacedEvent.GetType().Name}");
        }
    }

    // Implementare falsă a IInventoryService pentru testare
    public class FakeInventoryService : IInventoryService
    {
        public bool IsProductInStock(string productCode, decimal quantity)
        {
            // Pentru test, presupunem că toate produsele sunt în stoc
            return true;
        }
    }

    // Implementare falsă a IPriceService pentru testare
    public class FakePriceService : IPriceService
    {
        public decimal GetPrice(string productCode)
        {
            // Prețuri fictive pentru test
            if (productCode.StartsWith("W"))
                return 10m;  // Preț pentru Widget
            if (productCode.StartsWith("G"))
                return 20m;  // Preț pentru Gizmo

            return 0m;
        }
    }

    // Implementare falsă a IProductService pentru testare
    public class FakeProductService : IProductService
    {
        public Product GetProductByCode(string productCode)
        {
            // Creează un produs fals pe baza codului de produs
            return new Product(productCode, "Sample Product", 10m);
        }
    }
}
