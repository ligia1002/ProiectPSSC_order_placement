using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectPSSC_order_placement.Domain.Validation;
using ProiectPSSC_order_placement.Domain.ValueObjects;

namespace ProiectPSSC_order_placement.Domain.Models
{
    public static class Order
    {
        public interface IOrder { }

        // Comanda nevalidata
        public record UnvalidatedOrder(
            Guid OrderId, // ID-ul unic al comenzii
            UnvalidatedCustomerInfo CustomerInfo,
            UnvalidatedAddress ShippingAddress,
            UnvalidatedAddress BillingAddress,
            IReadOnlyCollection<UnvalidatedOrderLine> OrderLines
        ) : IOrder;

        // Comanda invalida, care conține motivele invaliditatii
        public record InvalidOrder(
            Guid OrderId, // ID-ul unic al comenzii
            IReadOnlyCollection<string> Reasons
        ) : IOrder;

        // Comanda validata
        public record ValidatedOrder(
            Guid OrderId, // ID-ul unic al comenzii
            ValidatedCustomerInfo CustomerInfo,
            ValidatedAddress ShippingAddress,
            ValidatedAddress BillingAddress,
            IReadOnlyCollection<ValidatedOrderLine> OrderLines
        ) : IOrder;

        // Comanda pretuita
        public record PricedOrder(
            Guid OrderId, // ID-ul unic al comenzii
            ValidatedCustomerInfo CustomerInfo,
            ValidatedAddress ShippingAddress,
            ValidatedAddress BillingAddress,
            IReadOnlyCollection<PricedOrderLine> OrderLines,
            decimal AmountToBill
        ) : IOrder;

        // Comanda procesata cu confirmare
        public record AcknowledgedOrder(
            Guid OrderId, // ID-ul unic al comenzii
            PricedOrder PricedOrder,
            string AcknowledgmentLetter,
            DateTime OrderPlacedDate
        ) : IOrder;

        // Comanda finalizata
        public record FinalizedOrder(
            Guid OrderId, // ID-ul unic al comenzii
            AcknowledgedOrder AcknowledgedOrder,
            string TrackingNumber,
            string InvoiceNumber,
            DateTime FinalizedDate
        ) : IOrder;
    }
}
