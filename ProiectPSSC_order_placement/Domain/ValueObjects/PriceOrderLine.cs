using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.ValueObjects
{
    public record PricedOrderLine(string ProductCode, decimal Quantity, decimal LinePrice);
}
