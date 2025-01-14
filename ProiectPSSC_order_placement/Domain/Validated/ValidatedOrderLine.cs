using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectPSSC_order_placement.Domain.ValueObjects;

namespace ProiectPSSC_order_placement.Domain.Validation
{
    public record ValidatedOrderLine(ProductCode ProductCode, OrderQuantity Quantity);
}

