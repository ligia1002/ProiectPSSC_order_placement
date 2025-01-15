using System;
using System.Collections.Generic;
using ProiectPSSC_order_placement.Domain.Models;
using static ProiectPSSC_order_placement.Domain.Models.Order;

namespace ProiectPSSC_order_placement.Domain.Models
{
    public static class OrderPlacedEvent
    {
        public interface IOrderPlacedEvent { }

        // Evenimentul de succes
        public record OrderPlacedSucceededEvent : IOrderPlacedEvent
        {
            public Guid OrderId { get; }
            public DateTime PlacedDate { get; }
            public decimal TotalAmount { get; }

            internal OrderPlacedSucceededEvent(Guid orderId, DateTime placedDate, decimal totalAmount)
            {
                OrderId = orderId;
                PlacedDate = placedDate;
                TotalAmount = totalAmount;
            }
        }

        // Evenimentul de eșec
        public record OrderPlacedFailedEvent : IOrderPlacedEvent
        {
            public IEnumerable<string> Reasons { get; }

            internal OrderPlacedFailedEvent(string reason)
            {
                Reasons = new[] { reason };
            }

            internal OrderPlacedFailedEvent(IEnumerable<string> reasons)
            {
                Reasons = reasons;
            }
        }

        // Funcție care transformă stările unei comenzi într-un eveniment
        public static IOrderPlacedEvent ToEvent(this IOrder order) =>
          order switch
          {
              UnvalidatedOrder _ => new OrderPlacedFailedEvent("Unexpected unvalidated state"),
              ValidatedOrder validatedOrder => new OrderPlacedFailedEvent("Unexpected validated state"),
              PricedOrder pricedOrder => new OrderPlacedFailedEvent("Unexpected priced state"),
              AcknowledgedOrder acknowledgedOrder => new OrderPlacedFailedEvent("Unexpected acknowledged state"),
              FinalizedOrder finalizedOrder => new OrderPlacedSucceededEvent(
                  finalizedOrder.OrderId,
                  finalizedOrder.FinalizedDate,
                  finalizedOrder.AcknowledgedOrder.PricedOrder.AmountToBill),  // Accesăm TotalAmount din PricedOrder
              _ => throw new NotImplementedException()
          };


    }
}
