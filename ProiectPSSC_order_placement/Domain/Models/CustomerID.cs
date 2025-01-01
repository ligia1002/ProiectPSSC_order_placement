using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Models
{
    using System;
    using System.Text.RegularExpressions;

    namespace ProiectPSSC_order_placement.Domain.Models
    {
        public class CustomerId
        {
            private static readonly Regex ValidPattern = new(@"^[C]{1}[0-9]{4}$"); // Exemplu: C0001, C0002, etc.

            public string Value { get; }

            private CustomerId(string value)
            {
                if (ValidPattern.IsMatch(value))
                {
                    Value = value;
                }
                else
                {
                    throw new InvalidCustomerIdException("CustomerId must match the pattern 'C' followed by 4 digits.");
                }
            }

            public static CustomerId Create(string value)
            {
                return new CustomerId(value);
            }

            public override string ToString()
            {
                return Value;
            }
        }

        public class InvalidCustomerIdException : Exception
        {
            public InvalidCustomerIdException(string message) : base(message) { }
        }
    }

}
