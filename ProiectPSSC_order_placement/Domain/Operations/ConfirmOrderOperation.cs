using ProiectPSSC_order_placement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Operations
{
    public class ConfirmOrderOperation : DomainOperation<Order.PricedOrder, object, Order.IOrder>
    {
        public override Order.IOrder Transform(Order.PricedOrder entity, object? state)
        {
            var acknowledgmentLetter = $"Dear {entity.CustomerInfo.CustomerName}, your order has been confirmed.";
            var orderPlacedDate = DateTime.UtcNow;

            return new Order.AcknowledgedOrder(
                entity.OrderId,
                entity, 
                acknowledgmentLetter,
                orderPlacedDate
            );
        }
    }
}
