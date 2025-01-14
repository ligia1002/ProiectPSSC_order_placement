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

        // Comanda nevalidata, similar cu UnvalidatedExam
        public record UnvalidatedOrder(
            UnvalidatedCustomerInfo CustomerInfo,
            UnvalidatedAddress ShippingAddress,
            UnvalidatedAddress BillingAddress,
            IReadOnlyCollection<UnvalidatedOrderLine> OrderLines
        ) : IOrder;

        // Comanda invalida, similar cu InvalidExam, care conține motivele invaliditatii
        public record InvalidOrder(
            IReadOnlyCollection<string> Reasons
        ) : IOrder;

        // Comanda validata, similar cu ValidatedExam
        public record ValidatedOrder(
            ValidatedCustomerInfo CustomerInfo,
            ValidatedAddress ShippingAddress,
            ValidatedAddress BillingAddress,
            IReadOnlyCollection<ValidatedOrderLine> OrderLines
        ) : IOrder;

        // Comanda pretuita, similar cu CalculatedExam
        public record PricedOrder(
            ValidatedCustomerInfo CustomerInfo,
            ValidatedAddress ShippingAddress,
            ValidatedAddress BillingAddress,
            IReadOnlyCollection<PricedOrderLine> OrderLines,
            decimal AmountToBill
        ) : IOrder;

        // Comanda procesata cu confirmare, similar cu PublishedExam
        public record AcknowledgedOrder(
            PricedOrder PricedOrder,
            string AcknowledgmentLetter,
            DateTime OrderPlacedDate
        ) : IOrder;

        // Comanda finalizata, poate include detalii de livrare si facturare
        public record FinalizedOrder(
            AcknowledgedOrder AcknowledgedOrder,
            string TrackingNumber,
            string InvoiceNumber,
            DateTime FinalizedDate
        ) : IOrder;
    }
}

