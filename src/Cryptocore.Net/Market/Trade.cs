using System;

namespace Cryptocore.Net
{
    public class Trade
    {
        public DateTime Timestamp { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Timestamp:yyyy.MM.dd HH:mm:ss} {Price} {Quantity}";
        }
    }
}