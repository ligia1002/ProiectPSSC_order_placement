using ProiectPSSC_order_placement.Domain.Models;
using ProiectPSSC_order_placement.Domain.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Workflows
{
    public class PlaceOrderWorkflow
    {
        public Order.IOrder Execute(Order.UnvalidatedOrder command)
        {
            // Pasul 1: Validarea comenzii
            ValidateOrderOperation validateOrderOperation = new ValidateOrderOperation();
            Order.IOrder validatedOrder = validateOrderOperation.Transform(command, null);

            if (validatedOrder is Order.InvalidOrder)
            {
                return validatedOrder;
            }

            // Pasul 2: Calcularea prețului comenzii
            CalculatePriceOperation calculatePriceOperation = new CalculatePriceOperation();
            Order.IOrder pricedOrder = calculatePriceOperation.Transform((Order.ValidatedOrder)validatedOrder, null);

            // Pasul 3: Confirmarea comenzii
            ConfirmOrderOperation confirmOrderOperation = new ConfirmOrderOperation();
            Order.IOrder acknowledgedOrder = confirmOrderOperation.Transform((Order.PricedOrder)pricedOrder, null);

            // Pasul 4: Finalizarea comenzii
            FinalizeOrderOperation finalizeOrderOperation = new FinalizeOrderOperation();
            Order.IOrder finalizedOrder = finalizeOrderOperation.Transform((Order.AcknowledgedOrder)acknowledgedOrder, null);

            return finalizedOrder;
        }
    }
}
