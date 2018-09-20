using System;

namespace Cryptocore.Net
{
    public class Quote
    {
        public DateTime Timestamp { get; set; }
        
        public decimal Last { get; set; }
        public decimal LastSize { get; set; }
        
        public decimal Bid { get; set; }
        public decimal BidSize { get; set; }
        
        public decimal Ask { get; set; }
        public decimal AskSize { get; set; }

        public override string ToString()
        {
            return $"{Timestamp:yyyy.MM.dd HH:mm:ss} Ask:{Ask}({AskSize}) Bid:{Bid}({BidSize}) Last:{Last}({LastSize})";
        }
    }
}