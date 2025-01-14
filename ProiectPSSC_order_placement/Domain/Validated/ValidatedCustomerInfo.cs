using System;
using System.Text.RegularExpressions;

namespace ProiectPSSC_order_placement.Domain.Validation
{
    public record ValidatedCustomerInfo
    {
        public Guid CustomerId { get; }
        public string CustomerName { get; }
        public string EmailAddress { get; }

        private ValidatedCustomerInfo(Guid customerId, string customerName, string emailAddress)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            EmailAddress = emailAddress;
        }

        public static ValidatedCustomerInfo Create(Guid customerId, string customerName, string emailAddress)
        {
            if (customerId == Guid.Empty)
            {
                throw new InvalidCustomerInfoException("Customer ID cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new InvalidCustomerInfoException("Customer name cannot be empty or null.");
            }

            if (!IsValidEmail(emailAddress))
            {
                throw new InvalidCustomerInfoException($"The email '{emailAddress}' is invalid.");
            }

            return new ValidatedCustomerInfo(customerId, customerName, emailAddress);
        }

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        public static bool TryParse(Guid customerId, string customerName, string emailAddress, out ValidatedCustomerInfo? customerInfo)
        {
            customerInfo = null;

            if (customerId == Guid.Empty || string.IsNullOrWhiteSpace(customerName) || !IsValidEmail(emailAddress))
            {
                return false;
            }

            customerInfo = new ValidatedCustomerInfo(customerId, customerName, emailAddress);
            return true;
        }
    }

    public class InvalidCustomerInfoException : Exception
    {
        public InvalidCustomerInfoException(string message) : base(message) { }
    }
}
