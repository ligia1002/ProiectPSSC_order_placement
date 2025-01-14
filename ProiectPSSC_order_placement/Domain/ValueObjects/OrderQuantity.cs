using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.ValueObjects
{
    public abstract record OrderQuantity
    {
        public decimal Quantity { get; }

        protected OrderQuantity(decimal quantity)
        {
            Quantity = quantity;
        }

        public static OrderQuantity CreateUnitQuantity(decimal quantity) =>
            quantity >= 1 && quantity <= 1000 ? new UnitQuantity(quantity) :
            throw new InvalidOrderQuantityException($"Invalid UnitQuantity: {quantity}");

        public static OrderQuantity CreateKilogramQuantity(decimal quantity) =>
            quantity >= 0.05m && quantity <= 100.00m ? new KilogramQuantity(quantity) :
            throw new InvalidOrderQuantityException($"Invalid KilogramQuantity: {quantity}");
    }

    public record UnitQuantity : OrderQuantity
    {
        public UnitQuantity(decimal quantity) : base(quantity) { }
    }

    public record KilogramQuantity : OrderQuantity
    {
        public KilogramQuantity(decimal quantity) : base(quantity) { }
    }

    public class InvalidOrderQuantityException : Exception
    {
        public InvalidOrderQuantityException(string message) : base(message) { }
    }
}
