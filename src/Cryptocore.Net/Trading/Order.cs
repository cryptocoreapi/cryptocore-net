using System;

namespace Cryptocore.Net
{
    public class Order
    {
        public string Id { get; set; }

        public Symbol Symbol { get; set; }

        public DateTime CreationTime { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal ExecutedQuantity { get; set; }

        public OrderSide Side { get; set; }

        public OrderType Type { get; set; }

        public OrderStatus Status { get; set; }
    }
}