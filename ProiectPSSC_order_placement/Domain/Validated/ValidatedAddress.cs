using System;
using System.Text.RegularExpressions;

namespace ProiectPSSC_order_placement.Domain.Validation
{
    public record ValidatedAddress
    {
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        private ValidatedAddress(string street, string city, string postalCode, string country)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        public static ValidatedAddress Create(string street, string city, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new InvalidAddressException("Street cannot be empty or null.");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new InvalidAddressException("City cannot be empty or null.");
            }

            if (!IsValidPostalCode(postalCode))
            {
                throw new InvalidAddressException($"Postal code '{postalCode}' is invalid.");
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                throw new InvalidAddressException("Country cannot be empty or null.");
            }

            return new ValidatedAddress(street, city, postalCode, country);
        }

        private static bool IsValidPostalCode(string postalCode)
        {
            var postalCodeRegex = new Regex(@"^\d{4,6}$"); // Exemplu: 4-6 cifre
            return postalCodeRegex.IsMatch(postalCode);
        }

        public static bool TryParse(string street, string city, string postalCode, string country, out ValidatedAddress? address)
        {
            address = null;

            if (string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(country) || !IsValidPostalCode(postalCode))
            {
                return false;
            }

            address = new ValidatedAddress(street, city, postalCode, country);
            return true;
        }
    }

    public class InvalidAddressException : Exception
    {
        public InvalidAddressException(string message) : base(message) { }
    }
}
