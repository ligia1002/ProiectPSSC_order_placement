using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPSSC_order_placement.Domain.Models
{
    public abstract record ProductCode
    {
        public string Value { get; }

        protected ProductCode(string value)
        {
            Value = value;
        }

        public static ProductCode Create(string code) =>
            code.StartsWith("W") && code.Length == 5 ? new WidgetCode(code) :
            code.StartsWith("G") && code.Length == 4 ? new GizmoCode(code) :
            throw new InvalidProductCodeException($"Invalid ProductCode: {code}");
    }

    public record WidgetCode : ProductCode
    {
        public WidgetCode(string value) : base(value)
        {
            if (!value.StartsWith("W") || value.Length != 5 || !int.TryParse(value[1..], out _))
            {
                throw new InvalidProductCodeException($"Invalid WidgetCode: {value}");
            }
        }
    }

    public record GizmoCode : ProductCode
    {
        public GizmoCode(string value) : base(value)
        {
            if (!value.StartsWith("G") || value.Length != 4 || !int.TryParse(value[1..], out _))
            {
                throw new InvalidProductCodeException($"Invalid GizmoCode: {value}");
            }
        }
    }

    public class InvalidProductCodeException : Exception
    {
        public InvalidProductCodeException(string message) : base(message) { }
    }
}
