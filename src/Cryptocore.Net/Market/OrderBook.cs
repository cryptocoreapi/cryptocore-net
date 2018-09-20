using System;
using System.Collections.Generic;

namespace Cryptocore.Net
{
    public class OrderBook
    {
        public DateTime Timestamp { get; set; }

        public IEnumerable<OrderBookLevel> Asks { get; set; }

        public IEnumerable<OrderBookLevel> Bids { get; set; }
    }
}