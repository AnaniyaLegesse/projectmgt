using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObj
{
    public record Priority
    {
        public int Value { get; }
        public string Label { get; }
        private Priority(int value, string label)
        {
            Value = value;
            Label = label;
        }

        public static Priority Low => new(1, "Low");
        public static Priority Medium => new(2, "Medium");
        public static Priority High => new(3, "High");

        public static Priority FromValue(int value) => value switch
        {
            1 => Low,
            2 => Medium,
            3 => High,
            _ => throw new ArgumentException("Invalid priority value.")
        };

        public static Priority Parse(string label)
        {
            if (label is null) throw new ArgumentNullException(nameof(label));
            return label.Trim().ToLowerInvariant() switch
            {
                "low" => Low,
                "medium" => Medium,
                "high" => High,
                _ => throw new ArgumentException($"Unknown priority label: '{label}'", nameof(label))
            };
        }
    }
}
