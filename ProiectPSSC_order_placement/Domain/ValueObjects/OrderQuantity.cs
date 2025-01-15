using ProiectPSSC_order_placement.Domain.Services;

public abstract record OrderQuantity
{
    public decimal Quantity { get; }

    protected OrderQuantity(decimal quantity)
    {
        Quantity = quantity;
    }

    public static OrderQuantity CreateUnitQuantity(decimal quantity, string productCode, IInventoryService inventoryService) =>
        quantity >= 1 && quantity <= 1000 && inventoryService.IsProductInStock(productCode, quantity)
            ? new UnitQuantity(quantity)
            : throw new InvalidOrderQuantityException($"Invalid UnitQuantity or insufficient stock for product: {productCode}");

    public static OrderQuantity CreateKilogramQuantity(decimal quantity, string productCode, IInventoryService inventoryService) =>
        quantity >= 0.05m && quantity <= 100.00m && inventoryService.IsProductInStock(productCode, quantity)
            ? new KilogramQuantity(quantity)
            : throw new InvalidOrderQuantityException($"Invalid KilogramQuantity or insufficient stock for product: {productCode}");
}

public record UnitQuantity : OrderQuantity
{
    public UnitQuantity(decimal quantity) : base(quantity) { }
}

public record KilogramQuantity : OrderQuantity
{
    public KilogramQuantity(decimal quantity) : base(quantity) { }
}

public class InvalidOrderQuantityException : Exception
{
    public InvalidOrderQuantityException(string message) : base(message) { }
}
