using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Exceptions
{
    internal class InvalidCustomerInfoException : Exception
    {
        public InvalidCustomerInfoException()
        {
        }

        public InvalidCustomerInfoException(string? message) : base(message)
        {
        }

        public InvalidCustomerInfoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
