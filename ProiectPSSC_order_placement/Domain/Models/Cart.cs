using ProiectPSSC_order_placement.Domain.Services;
using ProiectPSSC_order_placement.Domain.Validation;
using ProiectPSSC_order_placement.Domain.ValueObjects;

public class Cart
{
    private readonly List<ValidatedOrderLine> _items = new();
    public Guid CustomerId { get; } // Asocierea coșului cu un client

    public IReadOnlyCollection<ValidatedOrderLine> Items => _items.AsReadOnly();

    public Cart(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(customerId), "Customer ID cannot be empty.");
        }
        CustomerId = customerId;
    }

    // Adăugarea unui produs în coș
    public string AddProduct(string productCode, decimal quantity, IInventoryService inventoryService)
    {
        try
        {
            if (!inventoryService.IsProductInStock(productCode, quantity))
            {
                return $"Product {productCode} is out of stock.";
            }

            var validatedProductCode = ProductCode.Create(productCode);
            var validatedQuantity = OrderQuantity.CreateUnitQuantity(quantity, productCode, inventoryService);

            var existingItem = _items.FirstOrDefault(item => item.ProductCode == validatedProductCode);
            if (existingItem != null)
            {
                // Actualizează cantitatea produsului existent
                var updatedQuantity = existingItem.Quantity.Quantity + quantity;
                _items.Remove(existingItem);
                _items.Add(new ValidatedOrderLine(validatedProductCode, OrderQuantity.CreateUnitQuantity(updatedQuantity, productCode, inventoryService)));
            }
            else
            {
                // Adaugă produsul ca unul nou
                _items.Add(new ValidatedOrderLine(validatedProductCode, validatedQuantity));
            }

            return "Product added successfully.";
        }
        catch (Exception ex)
        {
            return $"Error adding product: {ex.Message}";
        }
    }

    public void ClearCart()
    {
        _items.Clear();
    }
}
