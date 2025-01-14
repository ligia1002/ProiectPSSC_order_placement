using ProiectPSSC_order_placement.Domain.Validation;
using ProiectPSSC_order_placement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProiectPSSC_order_placement.Domain.Models
{
    public interface ICart { }

    public record EmptyCart() : ICart;

    public record UnvalidatedCart(IReadOnlyCollection<UnvalidatedOrderLine> Items) : ICart;

    public record ValidatedCart(IReadOnlyCollection<ValidatedOrderLine> Items) : ICart;

    public record PaidCart(IReadOnlyCollection<ValidatedOrderLine> Items, decimal TotalPrice) : ICart;

    public class Cart
    {
        private ICart _state;

        public Cart()
        {
            _state = new EmptyCart();
        }

        public void AddProduct(string productCode, decimal quantity)
        {
            switch (_state)
            {
                case EmptyCart:
                    var newItems = new List<UnvalidatedOrderLine>
                    {
                        new UnvalidatedOrderLine(productCode, quantity)
                    };
                    _state = new UnvalidatedCart(newItems);
                    break;

                case UnvalidatedCart unvalidatedCart:
                    var updatedItems = new List<UnvalidatedOrderLine>(unvalidatedCart.Items)
                    {
                        new UnvalidatedOrderLine(productCode, quantity)
                    };
                    _state = new UnvalidatedCart(updatedItems);
                    break;

                default:
                    throw new InvalidOperationException("Nu poți adăuga produse în această stare.");
            }
        }
        public void RemoveProduct(string productCode)
        {
            if (_state is UnvalidatedCart unvalidatedCart)
            {
                var updatedItems = unvalidatedCart.Items
                    .Where(item => item.ProductCode != productCode)
                    .ToList();

                if (updatedItems.Any())
                {
                    _state = new UnvalidatedCart(updatedItems);
                }
                else
                {
                    // Dacă nu mai există produse, coșul devine gol
                    _state = new EmptyCart();
                }
            }
            else
            {
                throw new InvalidOperationException("Nu poți elimina produse în această stare.");
            }
        }

        public void ValidateCart()
        {
            if (_state is UnvalidatedCart unvalidatedCart)
            {
                var validatedItems = unvalidatedCart.Items
                    .Select(item =>
                    {
                        var productCode = ProductCode.Create(item.ProductCode);
                        var orderQuantity = OrderQuantity.CreateUnitQuantity(item.Quantity);
                        return new ValidatedOrderLine(productCode, orderQuantity);
                    })
                    .ToList();
                _state = new ValidatedCart(validatedItems);
            }
            else
            {
                throw new InvalidOperationException("Doar un coș nevalidat poate fi validat.");
            }
        }

        public void PayCart(decimal unitPrice = 10m)
        {
            if (_state is ValidatedCart validatedCart)
            {
                var totalPrice = validatedCart.Items.Sum(item => item.Quantity.Quantity * unitPrice);
                _state = new PaidCart(validatedCart.Items, totalPrice);
            }
            else
            {
                throw new InvalidOperationException("Doar un coș validat poate fi plătit.");
            }
        }

        public void DisplayCart()
        {
            switch (_state)
            {
                case EmptyCart:
                    Console.WriteLine("Coșul este gol.");
                    break;

                case UnvalidatedCart unvalidatedCart:
                    Console.WriteLine("Coșul conține următoarele produse (nevalidate):");
                    foreach (var item in unvalidatedCart.Items)
                    {
                        Console.WriteLine($"- Cod produs: {item.ProductCode}, Cantitate: {item.Quantity}");
                    }
                    break;

                case ValidatedCart validatedCart:
                    Console.WriteLine("Coșul conține următoarele produse (validate):");
                    foreach (var item in validatedCart.Items)
                    {
                        Console.WriteLine($"- Cod produs: {item.ProductCode.Value}, Cantitate: {item.Quantity.Quantity}");
                    }
                    break;

                case PaidCart paidCart:
                    Console.WriteLine("Coșul a fost plătit. Produsele sunt:");
                    foreach (var item in paidCart.Items)
                    {
                        Console.WriteLine($"- Cod produs: {item.ProductCode.Value}, Cantitate: {item.Quantity.Quantity}");
                    }
                    Console.WriteLine($"Total de plată: {paidCart.TotalPrice:C}");
                    break;

                default:
                    Console.WriteLine("Stare necunoscută a coșului.");
                    break;
            }
        }
        public void TransitionToValidated()
        {
            if (_state is UnvalidatedCart unvalidatedCart)
            {
                var validatedItems = unvalidatedCart.Items.Select(item =>
                    new ValidatedOrderLine(ProductCode.Create(item.ProductCode), OrderQuantity.CreateUnitQuantity(item.Quantity))
                ).ToList();
                _state = new ValidatedCart(validatedItems);
            }
            else
            {
                throw new InvalidOperationException("Doar un coș nevalidat poate fi validat.");
            }
        }

    }
}
