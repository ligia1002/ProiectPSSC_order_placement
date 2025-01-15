using ProiectPSSC_order_placement.Domain.Models;
using ProiectPSSC_order_placement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Operations
{
    public class CalculatePriceOperation : DomainOperation<Order.ValidatedOrder, object, Order.IOrder>
    {
        public override Order.IOrder Transform(Order.ValidatedOrder entity, object? state)
        {
            // Calcularea pentru fiecare linie
            var pricedLines = entity.OrderLines
                .Select(line =>
                {
                    decimal unitPrice = line.ProductCode is WidgetCode ? 10 : 20; // Exemplu: Widget = 10, Gizmo = 20
                    decimal linePrice = unitPrice * line.Quantity.Quantity;
                    return new PricedOrderLine(line.ProductCode.Value, line.Quantity.Quantity, linePrice);
                })
                .ToList();

            // Calcularea totalului
            decimal totalAmount = pricedLines.Sum(line => line.LinePrice);

            // Crearea comenzii cu pret calculat
            return new Order.PricedOrder(
                entity.OrderId,
                entity.CustomerInfo,
                entity.ShippingAddress,
                entity.BillingAddress,
                pricedLines,
                totalAmount
            );
        }
    }
}
