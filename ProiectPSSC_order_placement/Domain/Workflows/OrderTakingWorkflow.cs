using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProiectPSSC_order_placement.Domain.Models.Order;

namespace ProiectPSSC_order_placement.Domain.Workflows
{
    public class OrderTakingWorkflow
    {
        public enum OrderResult
        {
            OrderPlaced,
            InvalidOrder
        }

        public OrderResult PlaceOrder(UnvalidatedOrder orderForm)
        {
            var validationResult = new OrderValidator().ValidateOrder(orderForm);
            if (!validationResult.IsValid)
            {
                new OrderLogger().AddToInvalidOrderPile(validationResult.Errors);
                return OrderResult.InvalidOrder;
            }

            var validatedOrder = validationResult.ValidatedOrder;
            var pricedOrder = new OrderPricer().PriceOrder(validatedOrder);
            new AcknowledgmentSender().SendAcknowledgementToCustomer(pricedOrder);
            new OrderLogger().AddToOrderPlacedPile(pricedOrder);
            return OrderResult.OrderPlaced;
        }
    }
}
