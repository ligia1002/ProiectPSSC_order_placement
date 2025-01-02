using ProiectPSSC_order_placement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Operations
{
    public class ValidateOrderOperation : DomainOperation<Order.UnvalidatedOrder, object, Order.IOrder>
    {
        public override Order.IOrder Transform(Order.UnvalidatedOrder entity, object? state)
        {
            // Validarea informațiilor despre client
            if (!ValidatedCustomerInfo.TryParse(
                entity.CustomerInfo.CustomerId,
                entity.CustomerInfo.CustomerName,
                entity.CustomerInfo.EmailAddress,
                out var validatedCustomerInfo))
            {
                return new Order.InvalidOrder(new List<string> { "Invalid customer information." });
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
                        return null; // Linie invalidă
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
                entity.ShippingAddress,
                entity.BillingAddress,
                validatedLines
            );
        }
    }
