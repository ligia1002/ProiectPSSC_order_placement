using ProiectPSSC_order_placement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProiectPSSC_order_placement.Domain.Models.Order;

namespace ProiectPSSC_order_placement.Domain.Operations
{
    public class PriceOrderStep
    {
        public PricedOrder Price(ValidatedOrder validatedOrder)
        {
            var pricedLines = new List<PricedOrderLine>();
            decimal totalAmount = 0;

            foreach (var line in validatedOrder.OrderLines)
            {
                var price = GetPrice(line.ProductCode);
                var lineTotal = price * line.Quantity;
                totalAmount += lineTotal;

                pricedLines.Add(new PricedOrderLine(line.ProductCode, line.Quantity, lineTotal));
            }

            return new PricedOrder(
                validatedOrder.CustomerInfo,
                validatedOrder.ShippingAddress,
                validatedOrder.BillingAddress,
                pricedLines,
                totalAmount
            );
        }

        private decimal GetPrice(string productCode) => 10.0m;
    }
}
