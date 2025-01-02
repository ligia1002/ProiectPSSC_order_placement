using ProiectPSSC_order_placement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Operations
{
    public class FinalizeOrderOperation : DomainOperation<Order.AcknowledgedOrder, object, Order.IOrder>
    {
        public override Order.IOrder Transform(Order.AcknowledgedOrder entity, object? state)
        {
            var trackingNumber = Guid.NewGuid().ToString();
            var invoiceNumber = Guid.NewGuid().ToString();
            var finalizedDate = DateTime.UtcNow;

            return new Order.FinalizedOrder(
                entity,
                trackingNumber,
                invoiceNumber,
                finalizedDate
            );
        }
    }
}
