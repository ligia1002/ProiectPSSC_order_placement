using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectPSSC_order_placement.Domain.Models;

namespace ProiectPSSC_order_placement.Domain.Services
{
    public interface IProductService
    {
        Product GetProductByCode(string productCode);
    }
}
