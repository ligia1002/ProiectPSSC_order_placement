using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProiectPSSC_order_placement.Domain.Models
{
    public class Customer
    {
        public CustomerId Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<Address> Addresses { get; private set; } // Multiple adrese

        public Customer(CustomerId id, string name, string email, List<Address> addresses)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Customer ID cannot be null.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name cannot be empty.");
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                throw new ArgumentException("Invalid email address.");
            if (addresses == null || !addresses.Any())
                throw new ArgumentException("Customer must have at least one address.");

            Id = id;
            Name = name;
            Email = email;
            Addresses = addresses;
        }

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    }

    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        public Address(string street, string city, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty.");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.");
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException("Postal code cannot be empty.");
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty.");

            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
