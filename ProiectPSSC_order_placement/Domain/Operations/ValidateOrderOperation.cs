using ProiectPSSC_order_placement.Domain.Models;
using ProiectPSSC_order_placement.Domain.Operations;
using ProiectPSSC_order_placement.Domain.Validation;
using ProiectPSSC_order_placement.Domain.ValueObjects;

public class ValidateOrderOperation : DomainOperation<Order.UnvalidatedOrder, object, Order.IOrder>
{
    public override Order.IOrder Transform(Order.UnvalidatedOrder entity, object? state)
    {
        // Validarea informatiilor despre client
        if (!ValidatedCustomerInfo.TryParse(
            entity.CustomerInfo.CustomerId,
            entity.CustomerInfo.CustomerName,
            entity.CustomerInfo.EmailAddress,
            out var validatedCustomerInfo))
        {
            return new Order.InvalidOrder(new List<string> { "Invalid customer information." });
        }

        // Validarea adreselor
        if (!ValidatedAddress.TryParse(entity.ShippingAddress.Street, entity.ShippingAddress.City, entity.ShippingAddress.PostalCode, entity.ShippingAddress.Country, out var validatedShippingAddress) ||
            !ValidatedAddress.TryParse(entity.BillingAddress.Street, entity.BillingAddress.City, entity.BillingAddress.PostalCode, entity.BillingAddress.Country, out var validatedBillingAddress))
        {
            return new Order.InvalidOrder(new List<string> { "Invalid address information." });
        }

        // Validarea liniilor comenzii
        var validatedLines = entity.OrderLines
            .Select(line =>
            {
                try
                {
                    var productCode = ProductCode.Create(line.ProductCode);
                    var quantity = OrderQuantity.CreateUnitQuantity(line.Quantity);
                    return new ValidatedOrderLine(productCode, quantity);
                }
                catch
                {
                    return null; // Linie invalida
                }
            })
            .Where(line => line != null)
            .Cast<ValidatedOrderLine>()
            .ToList();

        if (!validatedLines.Any())
        {
            return new Order.InvalidOrder(new List<string> { "No valid order lines." });
        }

        // Crearea comenzii validate
        return new Order.ValidatedOrder(
            validatedCustomerInfo,
            validatedShippingAddress,
            validatedBillingAddress,
            validatedLines
        );
    }
}
