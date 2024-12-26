using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Exceptions
{
    internal class InvalidOrderLineException : Exception
    {
        public InvalidOrderLineException()
        {
        }

        public InvalidOrderLineException(string? message) : base(message)
        {
        }

        public InvalidOrderLineException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
