using ProiectPSSC_order_placement.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProiectPSSC_order_placement.Domain.Models.Order;

namespace ProiectPSSC_order_placement.Domain.Operations
{
    public class ValidateOrderStep
    {
        public ValidationResult Validate(UnvalidatedOrder unvalidatedOrder)
        {
            var errors = new List<string>();

           
            if (!IsValidCustomerInfo(unvalidatedOrder.CustomerInfo.CustomerName))
            {
                errors.Add("Invalid customer name.");
            }

            if (!IsValidCustomerInfo(unvalidatedOrder.CustomerInfo.EmailAddress))
            {
                errors.Add("Invalid customer email address.");
            }

           
            if (!IsValidAddress(unvalidatedOrder.ShippingAddress))
            {
                errors.Add("Invalid shipping address.");
            }

            if (!IsValidAddress(unvalidatedOrder.BillingAddress))
            {
                errors.Add("Invalid billing address.");
            }

            var validatedLines = new List<ValidatedOrderLine>();
            foreach (var line in unvalidatedOrder.OrderLines)
            {
                // Validare cod produs
                if (!IsValidProductCode(line.ProductCode))
                {
                    errors.Add($"Invalid product code: {line.ProductCode}");
                    continue;
                }

               
                if (!IsValidQuantity(line.Quantity))
                {
                    errors.Add($"Invalid quantity for product: {line.ProductCode}");
                    continue;
                }

                validatedLines.Add(new ValidatedOrderLine(line.ProductCode, line.Quantity));
            }

           
            if (errors.Any())
            {
                return new ValidationResult(string.Join(", ", errors));
            }

                    return new ValidationResult(new ValidatedOrder(
                    ValidateCustomerInfo(unvalidatedOrder.CustomerInfo),
                    ValidateAddress(unvalidatedOrder.ShippingAddress),
                    ValidateAddress(unvalidatedOrder.BillingAddress),
                     validatedLines
          ));

        }


        private bool IsValidCustomerInfo(string customerInfo) => !string.IsNullOrEmpty(customerInfo);

      
        private bool IsValidAddress(UnvalidatedAddress address) =>
            !string.IsNullOrEmpty(address.Street) &&
            !string.IsNullOrEmpty(address.City) &&
            !string.IsNullOrEmpty(address.PostalCode) &&
            !string.IsNullOrEmpty(address.Country);

      
        private bool IsValidProductCode(string productCode) =>
            (productCode.StartsWith("W") && productCode.Length == 5) ||
            (productCode.StartsWith("G") && productCode.Length == 4);

     
        private bool IsValidQuantity(decimal quantity) =>
            (quantity >= 1 && quantity <= 1000) || (quantity >= 0.05m && quantity <= 100.00m);
    }

}
