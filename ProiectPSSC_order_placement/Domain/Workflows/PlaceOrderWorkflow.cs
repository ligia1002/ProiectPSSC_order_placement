using ProiectPSSC_order_placement.Domain.Models;
using ProiectPSSC_order_placement.Domain.Operations;
using ProiectPSSC_order_placement.Domain.Services;
using ProiectPSSC_order_placement.Domain.ValueObjects;
using System;
using static ProiectPSSC_order_placement.Domain.Models.Order;
using static ProiectPSSC_order_placement.Domain.Models.OrderPlacedEvent;

namespace ProiectPSSC_order_placement.Domain.Workflows
{
    public class PlaceOrderWorkflow
    {
        private readonly IInventoryService _inventoryService;

        public PlaceOrderWorkflow(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public IOrderPlacedEvent Execute(
            Cart cart,
            UnvalidatedCustomerInfo customerInfo,
            UnvalidatedAddress shippingAddress,
            UnvalidatedAddress billingAddress)
        {
            // Step 1: Convert Cart to UnvalidatedOrder
            var unvalidatedOrder = new UnvalidatedOrder(
                Guid.NewGuid(),
                customerInfo,
                shippingAddress,
                billingAddress,
                cart.Items.Select(item =>
                    new UnvalidatedOrderLine(item.ProductCode.Value, item.Quantity.Quantity)).ToList()
            );

            // Step 2: Validate Order
            var validateOperation = new ValidateOrderOperation();
            var validatedOrder = validateOperation.Transform(unvalidatedOrder, _inventoryService);
            if (validatedOrder is not ValidatedOrder validOrder)
            {
                return validatedOrder.ToEvent(); // Return InvalidOrder event with reasons
            }

            // Step 3: Calculate Prices
            var calculatePriceOperation = new CalculatePriceOperation();
            var pricedOrder = calculatePriceOperation.Transform(validOrder, null) as PricedOrder;

            // Step 4: Confirm Order
            var confirmOperation = new ConfirmOrderOperation();
            var acknowledgedOrder = confirmOperation.Transform(pricedOrder!, null) as AcknowledgedOrder;

            // Step 5: Finalize Order
            var finalizeOperation = new FinalizeOrderOperation();
            var finalizedOrder = finalizeOperation.Transform(acknowledgedOrder!, null);

            return finalizedOrder.ToEvent();
        }
    }
}
