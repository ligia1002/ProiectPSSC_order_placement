using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Models
{
    public record UnvalidatedAddress(string Street, string City, string PostalCode, string Country);
}
