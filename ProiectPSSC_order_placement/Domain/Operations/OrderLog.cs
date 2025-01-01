using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProiectPSSC_order_placement.Domain.Models.Order;

namespace ProiectPSSC_order_placement.Domain.Operations
{
    public class OrderLog
    {
        public void LogInvalidOrder(List<string> errors) { }
        public void LogOrderPlaced(PricedOrder pricedOrder) { }
    }
}
