using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Services
{
    public interface IInventoryService
    {
        bool IsProductInStock(string productCode, decimal quantity);
    }
}
