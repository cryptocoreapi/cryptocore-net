using System;

namespace Cryptocore.Net
{
    public class Candle
    {
        public DateTime Timestamp { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public decimal Volume { get; set; }

        public override string ToString()
        {
            return $"{Timestamp:yyyy.MM.dd HH:mm:ss} {Open} {High} {Low} {Close} {Volume}";
        }
    }
}