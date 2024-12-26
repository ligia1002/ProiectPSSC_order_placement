using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Models
{
    public static class Order
    {
        public interface IOrder { }

        public record UnvalidatedOrder(
            UnvalidatedCustomerInfo CustomerInfo,
            UnvalidatedAddress ShippingAddress,
            UnvalidatedAddress BillingAddress,
            IReadOnlyCollection<UnvalidatedOrderLine> OrderLines
        ) : IOrder;

        public record ValidatedOrder(
            ValidatedCustomerInfo CustomerInfo,
            ValidatedAddress ShippingAddress,
            ValidatedAddress BillingAddress,
            IReadOnlyCollection<ValidatedOrderLine> OrderLines
        ) : IOrder;

        public record PricedOrder(
            ValidatedCustomerInfo CustomerInfo,
            ValidatedAddress ShippingAddress,
            ValidatedAddress BillingAddress,
            IReadOnlyCollection<PricedOrderLine> OrderLines,
            decimal AmountToBill
        ) : IOrder;

        public record PlacedOrderAcknowledgment(
            PricedOrder PricedOrder,
            string AcknowledgmentLetter
        ) : IOrder;
    }
}
