using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProiectPSSC_order_placement.Domain.Models.Order;

namespace ProiectPSSC_order_placement.Domain.Operations
{
    public class SendAcknowledgmentStep
    {
        public void Send(PricedOrder pricedOrder)
        {
            var acknowledgment = CreateAcknowledgment(pricedOrder);
            DispatchAcknowledgment(pricedOrder.CustomerInfo, acknowledgment);
        }

        private string CreateAcknowledgment(PricedOrder pricedOrder) => "Thank you for your order!";
        private void DispatchAcknowledgment(string customerInfo, string acknowledgment) { }
    }
}
