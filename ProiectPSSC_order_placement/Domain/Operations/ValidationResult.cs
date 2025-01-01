using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProiectPSSC_order_placement.Domain.Models.Order;

namespace ProiectPSSC_order_placement.Domain.Operations
{
 public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<string> Errors { get; }
    public ValidatedOrder ValidatedOrder { get; }

    public ValidationResult(List<string> errors)
    {
        Errors = errors;
    }

    public ValidationResult(ValidatedOrder validatedOrder)
    {
        Errors = new List<string>();
        ValidatedOrder = validatedOrder;
    }
}
}
